using Umicom.Domain.Entities;

namespace Umicom.Domain.IRepositories
{
    public interface IMenuRepository : IRepository<Menu>
    {
        /// <summary>
        /// 已知
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Menu GetObject(string code);
    }
}