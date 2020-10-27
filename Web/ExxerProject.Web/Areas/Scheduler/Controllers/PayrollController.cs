﻿using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ExxerProject.Scheduler.Data.DataAccess;
using ExxerProject.Web.Areas.Scheduler.Models.PayrollViewModels;
using ExxerProject.Web.Areas.Scheduler.Services;
using ExxerProject.Web.Events;
using ExxerProject.Web.Events.Interfaces;
using System.Collections.Generic;
using ExxerProject.Scheduler.Core.Entities;

namespace ExxerProject.Web.Areas.Scheduler.Controllers
{
    public class PayrollController : SchedulerBaseController
    {
        public PayrollController(
            ISchedulerWorkData schedulerWorkData,
            IMapper mapper,
            IPayrollService payrollService,
            IEventsFactory eventsFactory)
            : base(schedulerWorkData, mapper, eventsFactory)
        {
            this.PayrollService = payrollService;
            this.PayrollService.SchedulerWorkData = schedulerWorkData;
        }

        public IPayrollService PayrollService { get; set; }

        //
        // GET: Scheduler/Home/Index
        [HttpGet]
        public IActionResult Index(string message)
        {
            return View(IndexViewModel.Create(this.PayrollService, message));
        }

        //
        // POST: Scheduler/Home/PreparePayrolls
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PreparePayrolls([FromForm]string period)
        {
            DateTime paycheckIssueDate = DateTime.Today;

            // If period is null, make sure fromDate is Today. In this case we create paychecks for the current period.
            DateTime fromDate = DateTime.Today;
            if (period != null && !this.PayrollService.TryParsePeriod(period, out fromDate))
            {
                return BadRequest($"Invalid period.");
            }

            if (fromDate.Date > paycheckIssueDate)
            {
                return RedirectToAction(
                    nameof(PayrollController.Index),
                    new { Message = $"Can not create payrolls for period later then {paycheckIssueDate.Month}.{paycheckIssueDate.Year}!" });
            }

            var paycheks = new Stack<Paycheck>();
            var employees = await this.PayrollService.GetEmployees();
            employees.ToList().ForEach(e => paycheks.Push(e.AddOrUpdatePaycheck(paycheckIssueDate, fromDate)));

            await this.SchedulerWorkData.CompleteAsync();
            this.EventsFactory.Raise(new PaycheckCreatedOrUpdated(paycheks));

            return RedirectToAction(nameof(PayrollController.Index), new { Message = Constants.SuccessMessages.DatabaseUpdateSuccess });
        }

        //
        // GET: Scheduler/Home/ReviewPaychecks
        [HttpGet]
        public async Task<IActionResult> ReviewPaychecks()
        {
            return View(await ReviewPaychecksViewModel.CreateAsync(this.PayrollService));
        }

        //
        // POST: Scheduler/Home/ReviewPaychecks
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReviewPaychecks(ReviewPaychecksViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(await ReviewPaychecksViewModel.ReBuildAsync(model, this.PayrollService));
            }

            if (!this.PayrollService.TryParsePeriod(model.From, out DateTime dateFrom) ||
                !this.PayrollService.TryParsePeriod(model.To, out DateTime dateTo, false) ||
                dateFrom.Date >= dateTo.Date)
            {
                ModelState.AddModelError(
                    string.Empty,
                    $"Invalid dates. Make sure the end period date is later then the start date. Try again and if the problem persist contact administrator.");

                return View(await ReviewPaychecksViewModel.ReBuildAsync(model, this.PayrollService));
            }

            return View(await ReviewPaychecksViewModel.CreateAsync(model, dateFrom, dateTo, this.PayrollService, this.Mapper));
        }
    }
}
