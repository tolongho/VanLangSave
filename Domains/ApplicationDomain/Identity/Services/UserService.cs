﻿
using ApplicationDomain.BOA.Entities;
using ApplicationDomain.BOA.IRepositories;
using ApplicationDomain.Core.Entities;
using ApplicationDomain.Core.IRepositories;
using ApplicationDomain.Helper;
using ApplicationDomain.Identity.Entities;
using ApplicationDomain.Identity.IRepositories;
using ApplicationDomain.Identity.IServices;
using ApplicationDomain.Identity.Models;
using AspNetCore.AutoGenerate;
using AspNetCore.Common.Exceptions;
using AspNetCore.Common.Identity;
using AspNetCore.Common.Messages;
using AspNetCore.EmailSender;
using AspNetCore.UnitOfWork;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationDomain.Identity.Services
{
    public class UserService : ServiceBase, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IEmailSender _emailSender;
        private readonly IEmailRepository _emailTemplateRepository;
        private readonly UserManager<User> _userManagement;
        private readonly RoleManager<Role> _roleManager;
        public UserService(
            IMapper mapper,
            IUnitOfWork uow,
            ISupplierRepository supplierRepository,
            IUserRepository userRepository,
            IUserProfileRepository userProfileRepository,
            IRoleRepository roleRepository,
            UserManager<User> userManagement,
            IEmailSender emailSender,
            RoleManager<Role> roleManager,
            IEmailRepository emailTemplateRepository
            ) : base(mapper, uow)
        {
            _supplierRepository = supplierRepository;
            _userRepository = userRepository;
            _userProfileRepository = userProfileRepository;
            _userManagement = userManagement;
            _emailSender = emailSender;
            _roleManager = roleManager;
            _roleRepository = roleRepository;
            _emailTemplateRepository = emailTemplateRepository;
        }

        public IEnumerable<UserModel> GetListUsers()
        {
            try
            {
                return _userRepository.GetUsers().Cast<UserModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IList<string>> GetRoleByUser(int userId)
        {
            try
            {
                var identity = await _userManagement.FindByIdAsync(userId.ToString());
                var user = await _userManagement.GetRolesAsync(identity);
                return user;
            }
            catch (Exception e )
            {
                Console.WriteLine(e );
                throw;
            }
        }

        public async Task<IList<string>> GetRoleByUser(User user)
        {
            try
            {
                var roles = await _userManagement.GetRolesAsync(user);
                return roles;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<UserModel> GetUserById(int id)
        {
            var user = await _userManagement.FindByIdAsync(id.ToString());
            var result = _mapper.Map<UserModel>(user);
            return result;
        }

        public async Task<int> CreateUserAsync(CreatedUserRq model, UserIdentity<int> issuer = null)
        {
            try
            {
                model.Status = true;
                model.UserName = model.Email;
               
                var user = _mapper.Map<User>(model);
                if (issuer != null)
                {
                    user.CreateBy(issuer).UpdateBy(issuer);
                }
                string password = model.Password;
                var identityResult = await _userManagement.CreateAsync(user, password);

                if (!identityResult.Succeeded)
                {
                    throw CreateException(identityResult.Errors);
                }

                await _userManagement.AddToRoleAsync(user, ROLE_CONSTANT.NORMAL_USER);
             

                var userProfileEntity = new UserProfile()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    CreatedByUserId = user.Id,
                    UserId = user.Id,
                    CreatedByUserName = user.UserName,
                    BirthDay= model.BirthDay,
                    CreatedDate = DateTimeOffset.Now,
                    AvatarURL = "https://www.dropbox.com/s/x05w3wuhjlzbdk2/bmyntj4e.xkj.jpg?dl=1",
                };

                _userProfileRepository.Create(userProfileEntity);
                await _uow.SaveChangesAsync();
                return user.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private Exception CreateException(IEnumerable<IdentityError> errors)
        {
            var exception = new UserDefinedException();
            exception.UserDefinedMessage = new ExceptionMessage();
            exception.UserDefinedMessage.Details = new List<ExceptionMessage>();

            foreach (var error in errors)
            {
                exception.UserDefinedMessage.Details.Add(new ExceptionMessage
                {
                    Message = error.Description
                });
            }
            exception.UserDefinedMessage.Message = exception.UserDefinedMessage.Details.First().Message;

            return exception;
        }

        public async Task<int> UpdateUserAsync(int id, UpdatedUserRq model, UserIdentity<int> issuer)
        {
            var user = await _userManagement.FindByIdAsync(id.ToString());
            _mapper.Map(model, user);
            user.UpdateBy(issuer);
            await _userManagement.UpdateAsync(user);
            await _uow.SaveChangesAsync();
            return user.Id;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _userManagement.FindByIdAsync(id.ToString());

            if (user == null)
            {
                return false;
            }
            _userRepository.Delete(user);

            await _uow.SaveChangesAsync();

            return true;
        }

        public async Task<bool> AddRoleToUserAsync(UpdateUserRoleModelRq model)
        {
            try
            {
                var user = await _userManagement.FindByIdAsync(model.UserId.ToString());
                if (user == null)
                {
                    return false;
                }
                var rs = await _userManagement.AddToRoleAsync(user, model.RoleName);
                if (rs.Succeeded)
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> RemoveRoleToUserAsync(UpdateUserRoleModelRq model)
        {
            try
            {
                var user = await _userManagement.FindByIdAsync(model.UserId.ToString());
                if (user == null)
                {
                    return false;
                }
                var roles = await _userManagement.GetRolesAsync(user);
                if (roles.Count > 1)
                {
                    var rs = await _userManagement.RemoveFromRoleAsync(user, model.RoleName);
                    if (rs.Succeeded)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> CheckEmailAsync(string email)
        {
            try
            {
                var user = await _userManagement.FindByEmailAsync(email);
                return user != null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> AddRolesToUser(AddRolesToUserModelRq model)
        {
            try
            {
                var user = await _userManagement.FindByIdAsync(model.UserId.ToString());
                if (user == null)
                {
                    return false;
                }
                foreach (var item in model.Roles)
                {
                    var rs = await _userManagement.AddToRoleAsync(user, item.Name);
                    if (!rs.Succeeded)
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

   

        public async Task<IEnumerable<User>> GetManagerUsersAsync()
        {
            try
            {
                return await _userRepository.GetManagerUsers().ToListAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        } 

        public async Task<IEnumerable<User>> GetDirectorUsersAsync()
        {
            try
            {
                return await _userRepository.GetDirectorUsers().ToListAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        } 
        public async Task<IEnumerable<User>> GetEmployeeUsersAsync()
        {
            try
            {
                return await _userRepository.GetEmployeeUsers().ToListAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<int> CreateUserRegistrationAsync([FromBody]CreatedUserRq model)
        {
            try
            {
                var entity = new User()
                {
                    UserName = model.UserName,
                    PasswordHash = model.Password,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber
                };
                string password = model.Password;

                var identityResult = await _userManagement.CreateAsync(entity, password);

                if (!identityResult.Succeeded)
                {
                    throw CreateException(identityResult.Errors);
                }
                var rs = await _userManagement.AddToRoleAsync(entity, model.Role);
                return entity.Id;
            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}