using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutOfOffice.Domain.Entities
{
    public class ApprovalRequestStatus
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
    }
}
