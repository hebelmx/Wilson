﻿using ExxerProject.Companies.Core.Interfaces;

namespace ExxerProject.Companies.Core.Entities
{
    public class Settings : Entity, ISettings
    {
        protected Settings()
        {
        }

        public bool IsDatabaseInstalled { get; set; }

        public string HomeCompanyId { get; set; }

        public virtual Company HomeCompany { get; set; }

        public static Settings Initialize(string homeCopanyId)
        {
            return new Settings() { IsDatabaseInstalled = true, HomeCompanyId = homeCopanyId };
        }
    }
}
