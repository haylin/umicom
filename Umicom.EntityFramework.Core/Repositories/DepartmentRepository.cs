using Umicom.Domain.Entities;
using Umicom.Domain.IRepositories;

namespace Umicom.EntityFrameworkCore.Repositories
{
    public class DepartmentRepository : UmicomRepositoryBase<Department>, IDepartmentRepository
    {
        public DepartmentRepository(UmicomContext dbcontext) : base(dbcontext)
        {
        }
    }
}