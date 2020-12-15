using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceWorkerCronJobDemo.Entities
{
    public class work_order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string wo_name { get; set; }
        public string wo_no { get; set; }
        public int wo_type_id { get; set; }
        public int equipment_id { get; set; }
        public int wo_status_id { get; set; }
        public int wo_action_id { get; set; }
        public int assignee_user_id { get; set; }
        public int approve_user_id { get; set; }
        public string remarks { get; set; }
        public string action_taken { get; set; }
        public string reason_rejected { get; set; }
        public int wo_priority_id { get; set; }
        public int freq_period_id { get; set; }
        public int freq_total { get; set; }
        public Nullable<int> notification_id { get; set; }
        public int reminder_total { get; set; }
        public int reminder_period_id { get; set; }
        public System.DateTime dt_start_planned { get; set; }
        public System.DateTime dt_end_planned { get; set; }
        public Nullable<System.DateTime> dt_start_actual { get; set; }
        public Nullable<System.DateTime> dt_end_actual { get; set; }
        public Nullable<int> series_seq { get; set; }
        public Nullable<int> series_no { get; set; }
        public Nullable<int> daily_no { get; set; }
        public string ex_photo_data { get; set; }
        public string ex_sign_data { get; set; }
        public sbyte is_deleted { get; set; }
        public Nullable<System.DateTime> dt_created { get; set; }
        public Nullable<System.DateTime> dt_modified { get; set; }
        public Nullable<System.DateTime> dt_deleted { get; set; }
        public Nullable<int> created_by { get; set; }
        public Nullable<int> modified_by { get; set; }
        public Nullable<int> deleted_by { get; set; }
    }
}
