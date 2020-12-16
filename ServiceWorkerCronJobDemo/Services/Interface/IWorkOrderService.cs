using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ServiceWorkerCronJob.Entities;

namespace ServiceWorkerCronJob.Services
{
    public interface IWorkOrderService
    {
        int QueryWorkOrder();
    }
}
