using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceWorkerCronJob.Entities
{
    public partial class equipment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string equipment_no { get; set; }
        public int equipment_model_id { get; set; }
        public string serial_no { get; set; }
        public Nullable<int> mfg_year { get; set; }
        public string sales_contact_name { get; set; }
        public string sales_contact_no { get; set; }
        public string support_contact_name { get; set; }
        public string support_contact_no { get; set; }
        public System.DateTime dt_acquisition { get; set; }
        public Nullable<System.DateTime> dt_warranty_exp { get; set; }
        public System.DateTime dt_site_delivery { get; set; }
        public System.DateTime dt_installation { get; set; }
        public System.DateTime dt_commissioning { get; set; }
        public string cert_no { get; set; }
        public Nullable<System.DateTime> dt_cert { get; set; }
        public string remark { get; set; }
        public string location { get; set; }
        public string longitude { get; set; }
        public string latitude { get; set; }
        public decimal horsepower { get; set; }
        public int equipment_status_id { get; set; }
       // public virtual equipment_status equipment_status { get; set; }
        public int media_id { get; set; }
        public sbyte is_deleted { get; set; }
        public Nullable<System.DateTime> dt_deleted { get; set; }
        public Nullable<System.DateTime> dt_created { get; set; }
        public Nullable<System.DateTime> dt_modified { get; set; }
        public Nullable<int> created_by { get; set; }
        public Nullable<int> modified_by { get; set; }
        public Nullable<int> deleted_by { get; set; }
    }
}
