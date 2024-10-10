using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleMicroService.EventBusRabbitMQ.Events.Interface
{
    public abstract class  IEvent
    {
        protected IEvent()
        {
            RequestId = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }

        public Guid RequestId { get; private init; }
        public DateTime CreationDate { get; private init; }

    }
}
