using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.AspNetCore.Http;

namespace MyDodos.Domain.ConnectionString
{
public class ConnectionStringBO
    {
        public int TenantConnectionID { get; set; }
        public int ProductID { get; set; }
        public int TenantID { get; set; }
        public string ConnectionString { get; set; }
        public string TenantConnectionStatus { get; set; }
    }
}