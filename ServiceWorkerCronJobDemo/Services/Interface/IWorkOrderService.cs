using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ServiceWorkerCronJob.Entities;
using ServiceWorkerCronJob.Model;

namespace ServiceWorkerCronJob.Services
{
    public interface IWorkOrderService
    {
        IList<WorkOrderToEmail> QueryWorkOrder();
    }
}
