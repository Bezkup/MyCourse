using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using src.Models.Options;

namespace MyCourse.Models.Services.Infrastructure
{
    public class SqliteDatabaseAccessor : IDatabaseAccessor
    {
        private readonly ILogger<SqliteDatabaseAccessor> logger;

        public SqliteDatabaseAccessor(ILogger<SqliteDatabaseAccessor> logger, IOptionsMonitor<ConnectionStringsOptions> connectionStringsOption)
        {
            this.logger = logger;
            ConnectionStringsOption = connectionStringsOption;
        }

        public IOptionsMonitor<ConnectionStringsOptions> ConnectionStringsOption { get; }

        public async Task<DataSet> QueryAsync(FormattableString formattableQuery)
        {
            logger.LogInformation(formattableQuery.Format, formattableQuery.GetArguments());
            var queryArguments = formattableQuery.GetArguments();
            var sqliteParameters = new List<SqliteParameter>();

            for(var i = 0; i < queryArguments.Length; i++){
                var parameters = new SqliteParameter(i.ToString(), queryArguments[i]);
                sqliteParameters.Add(parameters);
                queryArguments[i] = "@" + i;
            }          
            
            string query = formattableQuery.ToString();
            
            string connectionString = ConnectionStringsOption.CurrentValue.Default;
            using(var conn = new SqliteConnection(connectionString)){
                await conn.OpenAsync();
                using(var cmd = new SqliteCommand(query, conn)){
                    cmd.Parameters.AddRange(sqliteParameters);
                    using(var reader = await cmd.ExecuteReaderAsync()){
                        
                        var dataSet = new DataSet();
                        
                        do{
                        var dataTable = new DataTable();
                        dataSet.Tables.Add(dataTable);
                        dataTable.Load(reader);
                        } while(!reader.IsClosed);

                        return dataSet;
                    }
                }
            }
        }
    }
}