using System;
using System.Data;
using System.Threading.Tasks;

namespace src.Models.Services.Infrastructure
{
    public interface IDatabaseAccessor
    {
        Task<DataSet> QueryAsync(FormattableString query);
    }
}