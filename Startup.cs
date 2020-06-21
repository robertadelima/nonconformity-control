using System;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NonconformityControl.Api.ViewModels;
using NonconformityControl.Api.ViewModelsValidators;
using NonconformityControl.Infra.Repositories;

namespace NonconformityControl
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
            services.AddDbContext<NonconformityContext>(
                options => options.UseMySql(Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection")));
            
            services.AddControllers()
                    .AddFluentValidation();

            services.AddScoped<NonconformityContext, NonconformityContext>();
            services.AddTransient<NonconformityRepository, NonconformityRepository>();
            services.AddTransient<ActionRepository, ActionRepository>();

            services.AddTransient<IValidator<AddNonconformityViewModel>, AddNonconformityViewModelValidator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
