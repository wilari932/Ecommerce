using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ecommerce.Domain
{
   public class BaseEntityIdGuid:BaseEntityId<Guid>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public override Guid Id { get => base.Id; set => base.Id = value; }
    }
}
