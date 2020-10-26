﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Wilson.Accounting.Data;
using Wilson.Scheduler.Core.Entities;
using Wilson.Web.Events.Interfaces;

namespace Wilson.Web.Events.Handlers
{
    public class PaycheckCreatedOrUpdatedHandler : Handler
    {
        private readonly IMapper _mapper;

        public PaycheckCreatedOrUpdatedHandler(IServiceProvider serviceProvider, IMapper mapper)
     : base(serviceProvider)
        {
            _mapper = mapper;
        }

        public override async Task Handle(IDomainEvent args)
        {
            var eventArgs = args as PaycheckCreatedOrUpdated;
            if (eventArgs == null)
            {
                throw new InvalidCastException();
            }

            using (var serviceScope = this.ServiceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var accDbContext = serviceScope.ServiceProvider.GetService<AccountingDbContext>();

                if (eventArgs.Paychecks != null)
                {
                    var query = await accDbContext.Set<Accounting.Core.Entities.Paycheck>().AsNoTracking().ToListAsync();
                    var mabeExistingPaycheks = query.Where(p => eventArgs.Paychecks.Any(ap => ap.Id == p.Id));

                    var accountingPaycheks = accDbContext.Set<Accounting.Core.Entities.Paycheck>();
                    if (mabeExistingPaycheks.Count() != 0)
                    {
                        var updatedAccountingPaycheks =
                            _mapper.Map<IEnumerable<Paycheck>, IEnumerable<Accounting.Core.Entities.Paycheck>>(
                                eventArgs.Paychecks.Where(p => mabeExistingPaycheks.Any(ap => ap.Id == p.Id)));

                        var newAccountingPaychecks = _mapper.Map<IEnumerable<Paycheck>, IEnumerable<Accounting.Core.Entities.Paycheck>>(
                            eventArgs.Paychecks.Where(p => !mabeExistingPaycheks.Any(ap => ap.Id == p.Id)));

                        accountingPaycheks.UpdateRange(updatedAccountingPaycheks);
                        accountingPaycheks.AddRange(newAccountingPaychecks);
                    }
                    else
                    {
                        var accPaychecks = _mapper.Map<IEnumerable<Paycheck>, IEnumerable<Accounting.Core.Entities.Paycheck>>(eventArgs.Paychecks);
                        accountingPaycheks.AddRange(accPaychecks);
                    }
                }

                if (eventArgs.Paycheck != null)
                {
                    var query = await accDbContext.Set<Accounting.Core.Entities.Paycheck>().AsNoTracking().ToListAsync();
                    var mabePaychek = query.SingleOrDefault(p => p.Id == eventArgs.Paycheck.Id);
                    var accPaycheck = _mapper.Map<Paycheck, Accounting.Core.Entities.Paycheck>(eventArgs.Paycheck);
                    if (mabePaychek != null)
                    {
                        accDbContext.Set<Accounting.Core.Entities.Paycheck>().Update(accPaycheck);
                    }
                    else
                    {
                        await accDbContext.Set<Accounting.Core.Entities.Paycheck>().AddAsync(accPaycheck);
                    }
                }

                await accDbContext.SaveChangesAsync();
            }
        }
    }
}