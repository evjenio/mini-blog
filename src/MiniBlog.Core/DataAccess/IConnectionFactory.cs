using System.Data;
using System.Threading.Tasks;

namespace MiniBlog.Core.DataAccess
{
    public interface IConnectionFactory
    {
        IDbConnection Create();

        Task<IDbConnection> CreateAsync();
    }
}