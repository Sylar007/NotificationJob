using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceWorkerCronJob.Entities
{
    public class notification_setting
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int reminder { get; set; }
        public int reminderType { get; set; }
        public string name { get; set; }
        public int frequency { get; set; }
        public int frequencyType { get; set; }
        public int remindBeforeAfter { get; set; }
        public int status { get; set; }
    }
}
