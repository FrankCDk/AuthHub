using AuthHub.Domain.Entities;
using System.Data.Common;

namespace AuthHub.Application.Interfaces
{
    public interface IHistoryAccessRepository
    {
        Task Register(DbConnection dbConnection, string dbType, HistoryAccess historyAccess);
    }
}
