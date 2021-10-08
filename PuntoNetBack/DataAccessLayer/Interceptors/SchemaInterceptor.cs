using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Shared.ModeloDeDominio;

namespace DataAccessLayer.Interceptors
{
    public class SchemaInterceptor : DbCommandInterceptor
    {
        private readonly TenantInfo tenantInfo;
        public SchemaInterceptor(TenantInfo tenantInfo)
        {
            this.tenantInfo = tenantInfo;
        }

        public override InterceptionResult<DbDataReader> ReaderExecuting(DbCommand command,
            CommandEventData eventData,
            InterceptionResult<DbDataReader> result)
        {
            command.CommandText = $"USE TenantPerSchemaDb {command.CommandText}";
            command.CommandText = command.CommandText
                .Replace("FROM ", $" FROM {tenantInfo.Id}.")
                .Replace("JOIN ", $" JOIN {tenantInfo.Id}.")
            ;

            return base.ReaderExecuting(command, eventData, result);
        }
    }
}