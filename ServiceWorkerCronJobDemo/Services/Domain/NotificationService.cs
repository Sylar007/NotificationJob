using System;
using System.Collections.Generic;
using System.Linq;
using ServiceWorkerCronJobDemo.Entities;
using ServiceWorkerCronJobDemo.Helpers;

namespace ServiceWorkerCronJobDemo.Services
{
    public class NotificationService : INotificationService
    {
        private DataContext _context;

        public NotificationService(DataContext context)
        {
            _context = context;
        }      
        //public bool SendNotification()
        //{
        //    try
        //    {
        //        return (from notification in _context.notification_setting
        //                join reminderType in _context.reminder_type on notification.reminderType equals reminderType.id
        //                join frequencyType in _context.frequency_type on notification.frequencyType equals frequencyType.id
        //                where notification.status != 0
        //                select new
        //                {
        //                    id = notification.id,
        //                    notificationName = notification.name,
        //                    notificationReminder = notification.reminder,
        //                    notificationReminderTypeId = reminderType.id,
        //                    notificationReminderType = reminderType.name,
        //                    notificationFrequency = notification.frequency,
        //                    notificationFrequencyTypeId = frequencyType.id,
        //                    notificationFrequencyType = frequencyType.name,
        //                    notificationStatus = (notification.status != null || notification.status != 0  ? "Active" : "Inactive")
        //                }).First();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}
