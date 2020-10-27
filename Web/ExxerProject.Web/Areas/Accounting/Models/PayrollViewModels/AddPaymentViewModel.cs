﻿using System;
using System.ComponentModel.DataAnnotations;
using ExxerProject.Web.Areas.Accounting.Models.SharedViewModels;

namespace ExxerProject.Web.Areas.Accounting.Models.PayrollViewModels
{
    public class AddPaymentViewModel
    {
        [Required]
        [DataType(DataType.Date, ErrorMessage = "Select From date.")]
        public DateTime From { get; set; }

        [Required]
        [DataType(DataType.Date, ErrorMessage = "Select To date.")]
        public DateTime To { get; set; }
        
        [StringLength(36)]
        public string EmployeeId { get; set; }
        
        [StringLength(36)]
        public string PaycheckId { get; set; }

        public PaymentViewModel Payment { get; set; }

        public static AddPaymentViewModel Create(DateTime from, DateTime to, string employeeId, string paycheckId)
        {
            return new AddPaymentViewModel()
            {
                From = from,
                To = to,
                EmployeeId = employeeId,
                PaycheckId = paycheckId,
                Payment = PaymentViewModel.Create()
            };
        }
        public static AddPaymentViewModel ReBuild(AddPaymentViewModel model)
        {
            return model;
        }
    }
}
