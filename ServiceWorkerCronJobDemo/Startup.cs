using System;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ServiceWorkerCronJob.Helpers;
using ServiceWorkerCronJob.Model;
using ServiceWorkerCronJob.Services;

namespace ServiceWorkerCronJob
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext, SqliteDataContext>();
            services.AddControllers();

            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();

            var notificationMetadata =
                Configuration.GetSection("NotificationMetadata").
                Get<NotificationMetadata>();
            services.AddSingleton(notificationMetadata);
            services.AddControllers();

            services.AddScoped<IMyScopedService, MyScopedService>();
            services.AddScoped<IWorkOrderService, WorkOrderService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddCronJob<MyCronJob1>(c =>
            {
                c.TimeZoneInfo = TimeZoneInfo.Local;
                c.CronExpression = @"*/59 * * * *";
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataContext dataContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
