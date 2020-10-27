﻿using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using ExxerProject.Accounting.Data.DataAccess;

namespace ExxerProject.Web.Areas.Accounting.Services
{
    public interface IService
    {
        IMapper Mapper { get; set; }

        /// <summary>
        /// Gets or Sets the Scheduler work context.
        /// </summary>
        IAccountingWorkData AccountingWorkData { get; set; }

        /// <summary>
        /// Asynchronous method that creates Collection of Employees as options for drop-down lists.
        /// </summary>
        /// <returns><see cref="List{T}"/> where {T} is <see cref="SelectListItem"/> with 
        /// value the employee Id and text the employee Name</returns>
        Task<List<SelectListItem>> GetEmployeeOptions();
    }
}
