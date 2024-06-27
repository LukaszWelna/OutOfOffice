using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutOfOffice.Application.ApplicationUser
{
    public class CurrentUser
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        public CurrentUser(string id, string email, string role)
        {
            Id = id;
            Email = email;
            Role = role;
        }
    }
}
