﻿using System.Collections.Generic;
using ExxerProject.Web.Areas.Scheduler.Models.HomeViewModels;
using ExxerProject.Web.Areas.Scheduler.Models.PayrollViewModels;

namespace ExxerProject.Web.Areas.Scheduler.Models.SharedViewModels
{
    public class EmployeeViewModel
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ScheduleViewModel NewSchedule { get; set; } = new ScheduleViewModel();

        public PaycheckViewModel NewPaycheck { get; set; } = new PaycheckViewModel();

        public IEnumerable<ScheduleViewModel> Schedules { get; set; }

        public IEnumerable<PaycheckViewModel> Paychecks { get; set; }

        public override string ToString()
        {
            return FirstName + " " + LastName; 
        }
    }
}
