using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Domain
{
    [AttributeUsage(AttributeTargets.Property)]
    public  class EditableField: Attribute
    {
    }
}
