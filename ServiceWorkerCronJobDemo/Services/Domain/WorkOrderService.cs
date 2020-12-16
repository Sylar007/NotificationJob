using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ServiceWorkerCronJob.Entities;
using ServiceWorkerCronJob.Helpers;

namespace ServiceWorkerCronJob.Services
{
    public class WorkOrderService : IWorkOrderService
    {
        private DataContext _context;

        public WorkOrderService(DataContext context)
        {
            _context = context;
        }

        public int QueryWorkOrder()
        {
            try
            {                
                return _context.work_order.Select(x => x.id).First();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
