# API

## Target framework:

- .NET Core 2.2 
- React


## IDE Recommend

Visual Studio 2019

| NuGet                                                 | Version |
| ----------------------------------------------------- | ------- |
| AutoMapper                                            | 9.0.0   |
| AutoMapper.Extensions.Microsoft.DependencyInjection   | 7.0.0   |
| Autofac.Extensions.DependencyInjection                | 5.0.1   |
| FluentValidation.AspNetCore                           | 8.6.0   |
| Microsoft.AspNetCore.Authentication.JwtBearer         | 2.2.0   |
| Microsoft.AspNetCore.Identity                         | 2.2.0   |
| Microsoft.AspNetCore.Identity.EntityFrameworkCore     | 2.2.0   |
| Microsoft.AspNetCore.Mvc                              | 2.2.0   |
| Microsoft.AspNetCore.Mvc.Core                         | 2.2.5   |
| Microsoft.EntityFrameworkCore                         | 2.2.0   |
| Microsoft.EntityFrameworkCore.Design                  | 2.2.0   |
| Microsoft.EntityFrameworkCore.SqlServer               | 2.2.0   |
| Microsoft.Extensions.Configuration.Json               | 2.2.0   |
| Microsoft.Extensions.DependencyInjection.Abstractions | 2.2.0   |
| Newtonsoft.Json                                       | 12.0.3  |
| Swashbuckle.AspNetCore.Swagger                        | 4.0.1   |
| Swashbuckle.AspNetCore.SwaggerGen                     | 4.0.1   |
| Swashbuckle.AspNetCore.SwaggerUi                      | 4.0.1   |

## Start project in local
Open file BOA.sln at \mindx-decor using visual studio
Run npm i at \mindx-decor\Applications\WebAdminApplication\ClientApp to installl package
Run ng build --watch at \mindx-decor\Applications\WebAdminApplication\ClientApp to run Front End

## Deploy step
Open visual studio code at \mindx-decor
### Build the front-end size
Open terminal and redirect to the front-end folder
```
cd .\Applications\WebAdminApplication\ClientApp\
ng build --prod
```
### Publish the API
Open new terminal and redirect to the API folder
```
cd .\Applications\WebAdminApplication\
dotnet publish -c Release -o ./publish
```
If you have error when publish, please click to the error file and close this file. Run again publish command to deploy.
Right click the publish file and click "Deploy to Web App"
