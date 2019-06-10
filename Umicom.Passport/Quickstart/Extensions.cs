using System.Threading.Tasks;
using IdentityServer4.Stores;

namespace Umicom.Passport.Quickstart.UI
{
    public static class Extensions
    {
        /// <summary>
        /// �ͻ����Ƿ�����Ϊʹ��PKCE.
        /// </summary>
        /// <param name="store">�洢��֤��.</param>
        /// <param name="client_id">�ͻ���Iid.</param>
        /// <returns></returns>
        public static async Task<bool> IsPkceClientAsync(this IClientStore store, string client_id)
        {
            if (!string.IsNullOrWhiteSpace(client_id))
            {
                var client = await store.FindEnabledClientByIdAsync(client_id);
                return client?.RequirePkce == true;
            }

            return false;
        }
    }
}
