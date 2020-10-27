﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using ExxerProject.Scheduler.Core.Entities;
using ExxerProject.Web.Areas.Scheduler.Models.SharedViewModels;
using ExxerProject.Web.Areas.Scheduler.Services;
using System.Linq;

namespace ExxerProject.Web.Areas.Scheduler.Models.PayrollViewModels
{
    public class ReviewPaychecksViewModel
    {
        [StringLength(36)]
        [Display(Name = "Employee")]
        public string EmployeeId { get; set; }

        [Required]
        [StringLength(7)]
        [Display(Name = "From")]
        public string From { get; set; }

        [Required]
        [StringLength(7)]
        [Display(Name = "To")]
        public string To { get; set; }

        public IEnumerable<SelectListItem> EmployeeOptions { get; set; }

        public IEnumerable<SelectListItem> PeriodOptions { get; set; }

        public IEnumerable<EmployeeViewModel> Employees { get; set; }

        public async static Task<ReviewPaychecksViewModel> CreateAsync(IPayrollService services)
        {
            return new ReviewPaychecksViewModel()
            {
                EmployeeOptions = await services.GetShdeduleEmployeeOptions(),
                PeriodOptions = services.GetPeriodsOptions()
            };
        }

        public async static Task<ReviewPaychecksViewModel> ReBuildAsync(ReviewPaychecksViewModel model, IPayrollService services)
        {
            model.EmployeeOptions = await services.GetShdeduleEmployeeOptions();
            model.PeriodOptions = services.GetPeriodsOptions();

            return model;
        }

        public async static Task<ReviewPaychecksViewModel> CreateAsync(
            ReviewPaychecksViewModel model,
            DateTime from,
            DateTime to,
            IPayrollService services,
            IMapper mapper)
        {
            var employees = await services.GetEmployees();
            var employeeModels = mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
            employeeModels.ToList().ForEach(e => 
                e.Paychecks = e.Paychecks.Where(p => from <= p.Period.From && to >= p.Period.To).OrderBy(p => p.Period.From));

            model.EmployeeOptions = await services.GetShdeduleEmployeeOptions();
            model.PeriodOptions = services.GetPeriodsOptions();
            model.Employees = employeeModels;

            return model;
        }
    }
}
