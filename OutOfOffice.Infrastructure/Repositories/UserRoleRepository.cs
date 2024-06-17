using OutOfOffice.Domain.Interfaces;
using OutOfOffice.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutOfOffice.Infrastructure.Repositories
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly OutOfOfficeDbContext _dbContext;
        public UserRoleRepository(OutOfOfficeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task UpdateAsync()
        {
            throw new NotImplementedException();
        }
    }
}
