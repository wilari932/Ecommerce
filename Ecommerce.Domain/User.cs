using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecommerce.Domain
{
 
    public  class User: BaseEntityId<Guid>
    {
        [MaxLength(100)]
        [MinLength(3)]
        [Required]
        [EditableField]
        public string UserName { get; set; }
        [EmailAddress]
        [MaxLength(500)]
        [Required]
        [EditableField]
        public string Email { get; set; }

        [MaxLength(100)]
        [MinLength(3)]
        [Required]
        public string UserNameNormalized { get; set; }

        [EmailAddress]
        [MaxLength(500)]
        [Required]
        public string EmailNormalized { get; set; }

        [MaxLength(1000)]
        [Required]
        [MinLength(3)]
        public string PasswordHash { get; set; }

    }
}
