using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Domain
{
   public abstract class BaseEntityId<T> :BaseEntity
    {
        public virtual T Id { get; set; }

    }
}
