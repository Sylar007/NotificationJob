using System;
using System.Collections.Generic;
using System.Linq;
using MailKit.Net.Smtp;
using MimeKit;
using ServiceWorkerCronJob.Entities;
using ServiceWorkerCronJob.Helpers;
using ServiceWorkerCronJob.Model;

namespace ServiceWorkerCronJob.Services
{
    public class NotificationService : INotificationService
    {
        private DataContext _context;

        public NotificationService(DataContext context)
        {
            _context = context;
        }
        private MimeMessage CreateMimeMessageFromEmailMessage(EmailMessage message)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(message.Sender);
            mimeMessage.To.Add(message.Reciever);
            mimeMessage.Subject = message.Subject;
            mimeMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text)
            { Text = message.Content };
            return mimeMessage;
        }
        public bool SendNotification(NotificationMetadata notificationMetadata, IList<WorkOrderToEmail> workorderToEmails)
        {
            try
            {
                foreach (WorkOrderToEmail workorderToEmail in workorderToEmails)
                {
                    EmailMessage message = new EmailMessage();
                    message.Sender = new MailboxAddress(notificationMetadata.Sender);
                    message.Reciever = new MailboxAddress(workorderToEmail.user_email);
                    message.Subject = "Notification Alert from EAMMS - Work Order: "+ workorderToEmail.wo_no;
                    message.Content = "Notification alert for Mr/Mrs "+ workorderToEmail.user_name + " regarding Workorder "+ workorderToEmail.wo_name;
                    var mimeMessage = CreateMimeMessageFromEmailMessage(message);
                    using (SmtpClient smtpClient = new SmtpClient())
                    {
                        smtpClient.Connect(notificationMetadata.SmtpServer,
                        notificationMetadata.Port, true);
                        smtpClient.Authenticate(notificationMetadata.UserName,
                        notificationMetadata.Password);
                        smtpClient.Send(mimeMessage);
                        smtpClient.Disconnect(true);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        //public bool SendNotification()
        //{
        //    using (SmtpClient smtpClient = new SmtpClient())
        //    {
        //        smtpClient.Connect(_notificationMetadata.SmtpServer,
        //        _notificationMetadata.Port, true);
        //        smtpClient.Authenticate(_notificationMetadata.UserName,
        //        _notificationMetadata.Password);
        //        smtpClient.Send(mimeMessage);
        //        smtpClient.Disconnect(true);
        //    }
        //    //    try
        //    //    {
        //    //        return (from notification in _context.notification_setting
        //    //                join reminderType in _context.reminder_type on notification.reminderType equals reminderType.id
        //    //                join frequencyType in _context.frequency_type on notification.frequencyType equals frequencyType.id
        //    //                where notification.status != 0
        //    //                select new
        //    //                {
        //    //                    id = notification.id,
        //    //                    notificationName = notification.name,
        //    //                    notificationReminder = notification.reminder,
        //    //                    notificationReminderTypeId = reminderType.id,
        //    //                    notificationReminderType = reminderType.name,
        //    //                    notificationFrequency = notification.frequency,
        //    //                    notificationFrequencyTypeId = frequencyType.id,
        //    //                    notificationFrequencyType = frequencyType.name,
        //    //                    notificationStatus = (notification.status != null || notification.status != 0  ? "Active" : "Inactive")
        //    //                }).First();
        //    //    }
        //    //    catch (Exception ex)
        //    //    {
        //    //        throw ex;
        //    //    }
        //}
    }
}
