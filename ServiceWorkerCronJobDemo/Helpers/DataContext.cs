using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ServiceWorkerCronJob.Entities;

namespace ServiceWorkerCronJob.Helpers
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server database
            options.UseSqlServer(Configuration.GetConnectionString("WebApiDatabase"));
        }
        public virtual DbSet<equipment> equipments { get; set; }
        public virtual DbSet<frequency_type> frequency_type { get; set; }
        public virtual DbSet<notification_setting> notification_setting { get; set; }
        public virtual DbSet<notification_type> notification_type { get; set; }        
        public virtual DbSet<reminder_type> reminder_type { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<work_order> work_order { get; set; }
    }
}