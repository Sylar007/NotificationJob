using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceWorkerCronJob.Model
{
    public class WorkOrderToEmail
    {
        public int id { get; set; }
        public string wo_no { get; set; }
        public string wo_name { get; set; }
        public string equipment_no { get; set; }
        public string user_name { get; set; }
        public string user_email { get; set; }
        public DateTime dt_start_planned { get; set; }
    }
}
