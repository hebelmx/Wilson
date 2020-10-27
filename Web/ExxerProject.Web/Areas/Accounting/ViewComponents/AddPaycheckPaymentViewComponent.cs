﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using ExxerProject.Accounting.Data.DataAccess;
using ExxerProject.Web.Areas.Accounting.Models.PayrollViewModels;
using ExxerProject.Web.Areas.Accounting.Services;
using ExxerProject.Web.Events.Interfaces;

namespace ExxerProject.Web.Areas.Accounting.ViewComponents
{
    public class AddPaycheckPaymentViewComponent : BaseViewComponent
    {
        public AddPaycheckPaymentViewComponent(
            IAccountingWorkData accountingWorkData, 
            IPayrollService payrollService, 
            IMapper mapper, 
            IEventsFactory eventsFactory) 
            : base(accountingWorkData, payrollService, mapper, eventsFactory)
        {
        }

        public IViewComponentResult Invoke(DateTime from, DateTime to, string employeeId, string paycheckId)
        {
            return View(AddPaymentViewModel.Create(from, to, employeeId, paycheckId));
        }
    }
}
