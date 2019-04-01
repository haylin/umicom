using Umicom.Domain.Entities;
using Umicom.Domain.IRepositories;
using  System.Linq;

namespace Umicom.EntityFrameworkCore.Repositories
{
    public class MenuRepository : UmicomRepositoryBase<Menu>, IMenuRepository
    {
        public MenuRepository(UmicomContext dbcontext) : base(dbcontext)
        {
        }
        public Menu GetObject(string code)
        {
            return  _dbContext.Set<Menu>().FirstOrDefault(it => it.Code == code);
        }
    }
}