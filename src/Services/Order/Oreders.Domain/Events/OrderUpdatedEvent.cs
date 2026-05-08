using System;
using System.Collections.Generic;
using System.Text;

namespace Orders.Domain.Events
{
    public record OrderUpdatedEvent(Order order) : IDomainEvent;
  
}
