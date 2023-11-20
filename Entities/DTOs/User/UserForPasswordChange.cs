using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.User
{
    public class UserForPasswordChange
    {
        public string Username { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword{ get; set; }
    }
}
