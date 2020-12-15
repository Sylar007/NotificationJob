using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace ServiceWorkerCronJobDemo.Services
{
    public interface IMyScopedService
    {
        Task DoWork(CancellationToken cancellationToken);
    }

    public class MyScopedService : IMyScopedService
    {
        private readonly ILogger<MyScopedService> _logger;
        private IWorkOrderService _workorderService;
        public MyScopedService(ILogger<MyScopedService> logger, IWorkOrderService workorderService)
        {
            _logger = logger;
            _workorderService = workorderService;
        }

        public async Task DoWork(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{DateTime.Now:hh:mm:ss} MyScopedService is working.");
            int returnId = _workorderService.QueryWorkOrder();
            await Task.Delay(1000 * 1, cancellationToken);
        }
    }
}
