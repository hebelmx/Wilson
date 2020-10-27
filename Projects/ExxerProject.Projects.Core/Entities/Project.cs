﻿using System;

namespace ExxerProject.Projects.Core.Entities
{
    public class Project : Entity
    {
        public string Name { get; private set; }

        public DateTime StartDate { get; private set; }

        public DateTime EndDate { get; private set; }

        public DateTime? ActualEndDate { get; private set; }

        public int GuaranteePeriodInMonths { get; private set; }        

        public bool IsActive { get; private set; }

        public string CustomerId { get; private set; }

        public string ManagerId { get; private set; }

        public virtual Company Customer { get; private set; }

        public virtual Employee Manager { get; private set; }

        public static Project Create(string name, DateTime startDate, DateTime endDate, string managerId, string customerId)
        {
            return new Project()
            {
                Name = name,
                StartDate = startDate,
                EndDate = endDate,
                ManagerId = managerId,
                CustomerId = customerId,
                IsActive = true
            };
        }

        public void SetGuaranteePeriod(int months)
        {
            this.GuaranteePeriodInMonths = months;
        }

        public void Close(DateTime actualEndDate)
        {
            if (this.ActualEndDate.HasValue)
            {
                throw new InvalidOperationException(string.Format("The project is already closed at {0}", this.ActualEndDate));
            }

            this.ActualEndDate = actualEndDate;
            this.IsActive = false;
        }
    }
}
