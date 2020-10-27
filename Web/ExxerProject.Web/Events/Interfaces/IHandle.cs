﻿using System.Threading.Tasks;

namespace ExxerProject.Web.Events.Interfaces
{
    public interface IHandle<T> where T : IDomainEvent
    {
        Task Handle(T args);
    }
}
