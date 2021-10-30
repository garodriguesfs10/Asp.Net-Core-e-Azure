using DevCompanyRating.API.Consumers;
using DevCompanyRating.API.Persistence;
using DevCompanyRating.API.Repositories;
using DevCompanyRating.API.Services.CognitiveServices;
using DevCompanyRating.API.Services.MessageBus;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace DevCompanyRating.API
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
            services.AddDbContext<DevCompanyRatingDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DevCompanyRatingCs")));
            // services.AddDbContext<DevCompanyRatingDbContext>(options => options.UseInMemoryDatabase("DevCompanyRating"));

            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<ICognitiveService, AzureCognitiveService>();
            services.AddScoped<IMessageBusService, AzureServiceBusService>();

            services.AddSingleton<IAddRatingConsumer, AddRatingConsumer>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DevCompanyRating.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DevCompanyRating.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var bus = app.ApplicationServices.GetRequiredService<IAddRatingConsumer>();
            bus.RegisterHandler();
        }
    }
}
