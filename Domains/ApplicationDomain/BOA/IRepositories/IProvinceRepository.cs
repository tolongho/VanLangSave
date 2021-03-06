using ApplicationDomain.BOA.Entities;
using AspNetCore.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationDomain.BOA.IRepositories
{
    public interface IProvinceRepository : IGenericRepository<Province, int>
    {
        IQueryable GetProvinces();
        IQueryable GetProvinceById(int id);
        Task<bool> CheckCodeExistsAsync(string code);
    }
}
