using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class UserLoginModel
    {
        [MaxLength(20)]
        [MinLength(4)]
        [RegularExpression("^(?=.{4,20}$)(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$")]
        public string UserName { get; set; }
       
        public string Password { get; set; }
    }
}
