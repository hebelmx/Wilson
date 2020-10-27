﻿using System;
using System.Collections.Generic;

namespace ExxerProject.Accounting.Core.Entities
{
    public class Employee : Entity
    {
        protected Employee()
        {
        }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public bool IsFired { get; private set; }

        public string CompanyId { get; private set; }

        public virtual Company Company { get; private set; }

        public virtual ICollection<Paycheck> Paycheks { get; private set; }

        public override string ToString()
        {
            return this.FirstName + " " + this.LastName;
        }

        public string GetName(Func<Employee, string> format)
        {
            return format(this);
        }        
    }
}
