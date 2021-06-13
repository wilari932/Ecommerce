using System;
using System.Collections.Generic;
using System.Text;

namespace Login.Ecommerce
{
  public  class DefaultClaims
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public ICollection<string> Roles = new List<string>();
        public string UserId { get; set; }
        public bool IsValidClaim => IsEmailSet && IsUserNameSet;
        public bool IsIdSet => !string.IsNullOrWhiteSpace(UserId);
        public bool IsEmailSet => !string.IsNullOrWhiteSpace(Email);
        public bool IsUserNameSet => !string.IsNullOrWhiteSpace(UserName);
    }
}
