using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ServiceWorkerCronJob.Model;

namespace ServiceWorkerCronJob.Services
{
    public interface IMyScopedService
    {
        Task DoWork(CancellationToken cancellationToken);
    }

    public class MyScopedService : IMyScopedService
    {
        private readonly ILogger<MyScopedService> _logger;
        private IWorkOrderService _workorderService;
        private INotificationService _notificationService;
        private NotificationMetadata _notificationMetadata;
        public MyScopedService(ILogger<MyScopedService> logger, IWorkOrderService workorderService, INotificationService notificationService,NotificationMetadata notificationMetadata)
        {
            _logger = logger;
            _workorderService = workorderService;
            _notificationService = notificationService;
            _notificationMetadata = notificationMetadata;
        }

        public async Task DoWork(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{DateTime.Now:hh:mm:ss} MyScopedService is working.");
            IList<WorkOrderToEmail> workorderToEmails = _workorderService.QueryWorkOrder();
            bool sendEmail = _notificationService.SendNotification(_notificationMetadata, workorderToEmails);
            await Task.Delay(1000 * 1, cancellationToken);
        }
        
    }
}
