﻿using Microsoft.Data.SqlClient;
using OutOfOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutOfOffice.Domain.Interfaces
{
    public interface ILeaveRequestRepository
    {
        public Task CreateLeaveRequestAsync(LeaveRequest leaveRequest);
        public Task<List<LeaveRequest>> GetAllLeaveRequestsAsync(string searchPhrase, string sortOrder);
        public Task<LeaveRequest> GetLeaveRequestByIdAsync(int id);
        public Task Commit();
    }
}
