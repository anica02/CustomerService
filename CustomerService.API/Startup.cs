using CustomerService.API.DTO;
using CustomerService.API.Extensions;
using CustomerService.API.Jwt;
using CustomerService.API.Jwt.TokenStorage;
using CustomerService.API.Middleware;
using CustomerService.Application;
using CustomerService.Application.Logging;
using CustomerService.Application.UseCaseHandiling;
using CustomerService.Application.UseCases.Commands;
using CustomerService.DataAccess;
using CustomerService.Implementation.Logging;
using CustomerService.Implementation.UseCases.Commands;
using CustomerService.Implementation.Validators;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerService.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            var appSettings = new AppSettings();
            Configuration.Bind(appSettings);

            services.AddTransient<ITokenStorage, InMemoryTokenStorage>();

            services.AddTransient<JwtManager>(x =>
            {
                var context = x.GetService<CustomerServiceContext>();
                var tokenStorage = x.GetService<ITokenStorage>();
                return new JwtManager(context, appSettings.Jwt.Issuer, appSettings.Jwt.SecretKey, appSettings.Jwt.DurationSeconds, tokenStorage);
            });

            services.AddJwt(appSettings);

            services.AddTransient<CustomerServiceContext>(x =>
            {
                DbContextOptionsBuilder builder = new DbContextOptionsBuilder();
                builder.UseSqlServer(@"Data Source=.\SQLEXPRESS; Initial Catalog = CustomerService; Integrated Security = true");
                return new CustomerServiceContext(builder.Options);
            });

            services.AddLogger();

            services.AddScoped<IApplicationActor>(x =>
            {
                var accessor = x.GetService<IHttpContextAccessor>();
                var header = accessor.HttpContext.Request.Headers["Authorization"];

                var data = header.ToString().Split("Bearer ");

                if (data.Length < 2)
                {
                    return new UnauthorizedActor();
                }

                var handler = new JwtSecurityTokenHandler();

                var tokenObj = handler.ReadJwtToken(data[1].ToString());

                var claims = tokenObj.Claims;

                var email = claims.First(x => x.Type == "Email").Value;
                var id = claims.First(x => x.Type == "Id").Value;
                var username = claims.First(x => x.Type == "Username").Value;
                var useCases = claims.First(x => x.Type == "UseCases").Value;
                var roleName = claims.First(x => x.Type == "Role").Value;
                List<int> useCaseIds = JsonConvert.DeserializeObject<List<int>>(useCases);

                return new JwtActor
                {
                    Email = email,
                    AllowedUseCases = useCaseIds,
                    Id = int.Parse(id),
                    Username = username,
                    RoleName = roleName
                };
            });

            services.AddTransient<IQueryHandler>(x =>
            {
                var actor = x.GetService<IApplicationActor>();
                var logger = x.GetService<IUseCaseLogger>();
                var queryHandler = new QueryHandler();
                var timeTrackingHandler = new TimeTrackingQueryHandeler(queryHandler);
                var loggingHandler = new LoggingQueryHanler(timeTrackingHandler, actor, logger);
                var decoration = new AuthorizationQueryHandler(actor, loggingHandler);

                return decoration;
            });

            services.AddTransient<QueryHandler>();

            services.AddTransient<ICommandHandler, CommandHandler>();


            services.AddTransient<IUseCaseLogger, EfUseCaseLogger>();

            services.AddTransient<ICreateCustomerDiscountCommand, EfCreateCustomerDiscountCommand>();
            services.AddTransient<CreateCustomerDiscountValidator>();
            services.AddHttpContextAccessor();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CustomerService.API", Version = "v1" });
            });

          

            services.AddScoped<MyDataProcessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CustomerService.API v1"));
            

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<ExceptionHandalingMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
