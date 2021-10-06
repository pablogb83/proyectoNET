using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Shared.ModeloDeDominio;
using System.Data.Common;
using System.Threading;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace DataAccessLayer.Interceptors
{
    public class DiscriminatorColumnInterceptor : DbCommandInterceptor
    {
        private readonly TenantInfo tenantInfo;
        public DiscriminatorColumnInterceptor(TenantInfo tenantInfo)
        {
            this.tenantInfo = tenantInfo;
        }

        public override InterceptionResult<DbDataReader> ReaderExecuting(DbCommand command,
            CommandEventData eventData,
            InterceptionResult<DbDataReader> result)
        {
            command.CommandText = $"USE ProyectoNET2 {command.CommandText}";

            if(!command.CommandText.Contains("[Instituciones]")) //dudoso muy dudoso... 
            {
                if (command.CommandText.Contains("WHERE"))
                {
                    command.CommandText += $" AND institucionId = '{tenantInfo.Id}'";
                }
                else
                {
                    command.CommandText += $" WHERE institucionId = '{tenantInfo.Id}'";
                }
            }

            return base.ReaderExecuting(command, eventData, result);
        }
    }
}
