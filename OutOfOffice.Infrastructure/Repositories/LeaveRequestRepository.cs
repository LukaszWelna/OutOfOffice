using OutOfOffice.Domain.Entities;
using OutOfOffice.Domain.Interfaces;
using OutOfOffice.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutOfOffice.Infrastructure.Repositories
{
    public class LeaveRequestRepository : ILeaveRequestRepository
    {
        private readonly OutOfOfficeDbContext _dbContext;

        public LeaveRequestRepository(OutOfOfficeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateLeaveRequestAsync(LeaveRequest leaveRequest)
        {
            _dbContext.Add(leaveRequest);
            await _dbContext.SaveChangesAsync();
        }
    }
}
