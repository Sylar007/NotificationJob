using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ServiceWorkerCronJob.Entities;
using ServiceWorkerCronJob.Helpers;
using ServiceWorkerCronJob.Model;

namespace ServiceWorkerCronJob.Services
{
    public class WorkOrderService : IWorkOrderService
    {
        private DataContext _context;

        public WorkOrderService(DataContext context)
        {
            _context = context;
        }

        public IList<WorkOrderToEmail> QueryWorkOrder()
        {
            try
            {
                var workorders = (from workorder in _context.work_order
                                  join equipment in _context.equipments on workorder.equipment_id equals equipment.id
                                  join user in _context.Users on workorder.assignee_user_id equals user.Id
                                  where workorder.dt_start_planned >= DateTime.Now && workorder.wo_status_id != 0
                                  orderby workorder.dt_start_planned
                                  select new
                                  {
                                      id = workorder.id,
                                      wo_no = workorder.wo_no,
                                      wo_name = workorder.wo_name,
                                      equipment_no = equipment.equipment_no,
                                      user_name = user.FirstName + " " + user.LastName,
                                      user_email = user.Email,
                                      dt_start_planned = workorder.dt_start_planned,
                                      notification_id = workorder.notification_id
                                  }).ToList();

                IList<WorkOrderToEmail> workorderToEmails = new List<WorkOrderToEmail>();
                foreach (var workorder in workorders)
                {
                    var notificationSingle = (from notification in _context.notification_setting
                                              join reminder in _context.reminder_type on notification.reminderType equals reminder.id
                                              join frequency in _context.frequency_type on notification.frequencyType equals frequency.id
                                              where notification.id == workorder.notification_id
                                              select new
                                              {
                                                  reminder = notification.reminder,
                                                  reminderTypeName = reminder.name,
                                                  frequency = notification.frequency,
                                                  frequencyTypeName = frequency.name,
                                              }).FirstOrDefault();

                    DateTime compareTo = DateTime.Parse(workorder.dt_start_planned.ToString());
                    DateTime now = DateTime.Parse(DateTime.Now.ToString());
                    var dateSpan = DateTimeSpan.CompareDates(compareTo, now);                   
                    Boolean validToSend = false;
                    switch (notificationSingle.reminderTypeName)
                    {
                        case "Hour":
                            if (dateSpan.Days == 0)
                            {
                                validToSend = true;
                            }
                            break;
                        case "Day":
                            if ((dateSpan.Days - notificationSingle.reminder <= 0) && (dateSpan.Days - notificationSingle.reminder < -3))
                            {
                                validToSend = true;
                            }
                            break;
                        case "Month":

                            if ((dateSpan.Months - notificationSingle.reminder <= 0) && (dateSpan.Months - notificationSingle.reminder < -2))
                            {
                                validToSend = true;
                            }
                            break;
                        default:
                            // code block
                            break;
                    }
                    int hourNow = DateTime.Now.Hour;
                    int dayNow = DateTime.Now.Day;
                    int monthNow = DateTime.Now.Month;
                    switch (notificationSingle.frequencyTypeName)
                    {
                        case "Hour":
                            int vDivide = 24 / notificationSingle.frequency;
                            double result = (double)hourNow / vDivide;
                            decimal d = new Decimal(result);
                            d = d % 1;

                            if (validToSend == true && d == 0)
                            {
                                WorkOrderToEmail workorderToEmail = new WorkOrderToEmail();
                                workorderToEmail.id = workorder.id;
                                workorderToEmail.wo_no = workorder.wo_no;
                                workorderToEmail.wo_name = workorder.wo_name;
                                workorderToEmail.equipment_no = workorder.equipment_no;
                                workorderToEmail.user_name = workorder.user_name;
                                workorderToEmail.user_email = workorder.user_email;
                                workorderToEmail.dt_start_planned = workorder.dt_start_planned;

                                workorderToEmails.Add(workorderToEmail);
                            }
                            break;
                        case "Day":
                            int vDivideDay = 30 / notificationSingle.frequency;
                            double resultDay = (double)dayNow / vDivideDay;
                            decimal dDay = new Decimal(resultDay);
                            dDay = dDay % 1;

                            if (validToSend == true && dDay == 0)
                            {
                                WorkOrderToEmail workorderToEmail = new WorkOrderToEmail();
                                workorderToEmail.id = workorder.id;
                                workorderToEmail.wo_no = workorder.wo_no;
                                workorderToEmail.wo_name = workorder.wo_name;
                                workorderToEmail.equipment_no = workorder.equipment_no;
                                workorderToEmail.user_name = workorder.user_name;
                                workorderToEmail.user_email = workorder.user_email;
                                workorderToEmail.dt_start_planned = workorder.dt_start_planned;

                                workorderToEmails.Add(workorderToEmail);
                            }
                            break;
                        case "Month":
                            int vDivideMonth = 12 / notificationSingle.frequency;
                            double resultMonth = (double)monthNow / vDivideMonth;
                            decimal dMonth = new Decimal(resultMonth);
                            dMonth = dMonth % 1;

                            if (validToSend == true && dMonth == 0)
                            {
                                WorkOrderToEmail workorderToEmail = new WorkOrderToEmail();
                                workorderToEmail.id = workorder.id;
                                workorderToEmail.wo_no = workorder.wo_no;
                                workorderToEmail.wo_name = workorder.wo_name;
                                workorderToEmail.equipment_no = workorder.equipment_no;
                                workorderToEmail.user_name = workorder.user_name;
                                workorderToEmail.user_email = workorder.user_email;
                                workorderToEmail.dt_start_planned = workorder.dt_start_planned;

                                workorderToEmails.Add(workorderToEmail);
                            }
                            break;
                        default:
                            // code block
                            break;
                    }   
                }
                return workorderToEmails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public struct DateTimeSpan
        {
            public int Years { get; }
            public int Months { get; }
            public int Days { get; }
            public int Hours { get; }
            public int Minutes { get; }
            public int Seconds { get; }
            public int Milliseconds { get; }

            public DateTimeSpan(int years, int months, int days, int hours, int minutes, int seconds, int milliseconds)
            {
                Years = years;
                Months = months;
                Days = days;
                Hours = hours;
                Minutes = minutes;
                Seconds = seconds;
                Milliseconds = milliseconds;
            }

            enum Phase { Years, Months, Days, Done }

            public static DateTimeSpan CompareDates(DateTime date1, DateTime date2)
            {
                if (date2 < date1)
                {
                    var sub = date1;
                    date1 = date2;
                    date2 = sub;
                }

                DateTime current = date1;
                int years = 0;
                int months = 0;
                int days = 0;

                Phase phase = Phase.Years;
                DateTimeSpan span = new DateTimeSpan();
                int officialDay = current.Day;

                while (phase != Phase.Done)
                {
                    switch (phase)
                    {
                        case Phase.Years:
                            if (current.AddYears(years + 1) > date2)
                            {
                                phase = Phase.Months;
                                current = current.AddYears(years);
                            }
                            else
                            {
                                years++;
                            }
                            break;
                        case Phase.Months:
                            if (current.AddMonths(months + 1) > date2)
                            {
                                phase = Phase.Days;
                                current = current.AddMonths(months);
                                if (current.Day < officialDay && officialDay <= DateTime.DaysInMonth(current.Year, current.Month))
                                    current = current.AddDays(officialDay - current.Day);
                            }
                            else
                            {
                                months++;
                            }
                            break;
                        case Phase.Days:
                            if (current.AddDays(days + 1) > date2)
                            {
                                current = current.AddDays(days);
                                var timespan = date2 - current;
                                span = new DateTimeSpan(years, months, days, timespan.Hours, timespan.Minutes, timespan.Seconds, timespan.Milliseconds);
                                phase = Phase.Done;
                            }
                            else
                            {
                                days++;
                            }
                            break;
                    }
                }

                return span;
            }
        }
    }
}
