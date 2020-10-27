﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ExxerProject.Accounting.Data.DataAccess;
using ExxerProject.Web.Areas.Accounting.Services;
using ExxerProject.Web.Events.Interfaces;

namespace ExxerProject.Web.Areas.Accounting.Controllers
{
    [Area(Constants.Areas.Accounting)]
    [Authorize(Roles = Constants.Roles.Administrator + ", " + Constants.Roles.Accouter)]
    public abstract class AccountingBaseController : Controller
    {
        public AccountingBaseController(IAccountingWorkData accountingWorkData, IPayrollService payrollService, IMapper mapper, IEventsFactory eventsFactory)
        {
            this.AccountingWorkData = accountingWorkData;
            this.PayrollService = payrollService;
            this.Mapper = mapper;
            this.EventsFactory = eventsFactory;
        }

        public IAccountingWorkData AccountingWorkData { get; set; }

        public IPayrollService PayrollService { get; set; }

        public IMapper Mapper { get; set; }

        public IEventsFactory EventsFactory { get; set; }
    }
}
