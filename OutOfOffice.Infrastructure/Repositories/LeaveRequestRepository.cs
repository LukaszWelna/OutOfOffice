using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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

        public async Task<List<LeaveRequest>> GetAllLeaveRequestsAsync(string searchPhrase, string sortOrder)
        {
            var leaveRequests = _dbContext.LeaveRequests
                .Include(l => l.Employee)
                .Where(l => searchPhrase == null || l.Employee.FullName.ToLower().Contains(searchPhrase.ToLower()));

            switch (sortOrder)
            {
                case "IdDesc":
                    leaveRequests = leaveRequests.OrderByDescending(l => l.Id);
                    break;
                case "employeeAsc":
                    leaveRequests = leaveRequests.OrderBy(l => l.Employee.FullName);
                    break;
                case "employeeDesc":
                    leaveRequests = leaveRequests.OrderByDescending(l => l.Employee.FullName);
                    break;
                case "absenceReasonAsc":
                    leaveRequests = leaveRequests.OrderBy(l => l.AbsenceReason);
                    break;
                case "absenceReasonDesc":
                    leaveRequests = leaveRequests.OrderByDescending(l => l.AbsenceReason);
                    break;
                case "startDateAsc":
                    leaveRequests = leaveRequests.OrderBy(l => l.StartDate);
                    break;
                case "startDateDesc":
                    leaveRequests = leaveRequests.OrderByDescending(l => l.StartDate);
                    break;
                case "endDateAsc":
                    leaveRequests = leaveRequests.OrderBy(l => l.EndDate);
                    break;
                case "endDateDesc":
                    leaveRequests = leaveRequests.OrderByDescending(l => l.EndDate);
                    break;
                case "statusAsc":
                    leaveRequests = leaveRequests.OrderBy(l => l.Status);
                    break;
                case "statusDesc":
                    leaveRequests = leaveRequests.OrderByDescending(l => l.Status);
                    break;
                default:
                    leaveRequests = leaveRequests.OrderBy(l => l.Id);
                    break;
            }

            return await leaveRequests.ToListAsync();
        }

        public async Task<LeaveRequest> GetLeaveRequestByIdAsync(int id)
        {
            var leaveRequest = await _dbContext.LeaveRequests
                .Include(l => l.Employee)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (leaveRequest == null)
            {
                throw new InvalidOperationException("Leave request with specified id doesn't exist.");
            }

            return leaveRequest;
        }

        public async Task Commit()
            => await _dbContext.SaveChangesAsync();
    }
}
