using System;
using System.Collections.Generic;
using MyDodos.Domain.ConnectionString;

namespace MyDodos.Repository.ConnectionString
{
    public interface IConnectionStringRepository
    {
        List<ConnectionStringBO> GetTenantConnectionString(int tenantConnectionId, int productId, int tenantId);
    }
}