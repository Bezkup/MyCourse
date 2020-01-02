using System;
using System.Data;

namespace src.Models.Services.Infrastructure
{
    public interface IDatabaseAccessor
    {
        DataSet Query(FormattableString query);
    }
}