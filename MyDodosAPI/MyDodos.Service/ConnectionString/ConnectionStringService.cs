using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using MyDodos.Domain.Wrapper;
using MyDodos.Domain.Document;
using MyDodos.Domain.HR;
using MyDodos.ViewModel.ServerSearch;
using MyDodos.ViewModel.HR;
using MyDodos.ViewModel.Document;
using MyDodos.Repository.Document;
using MyDodos.Repository.HR;
using System.IO;
using System.Linq;
using System.Text;
using MyDodos.Repository.Auth;
using MyDodos.Repository.AzureStorage;
using MyDodos.Domain.AzureStorage;
using MyDodos.Repository.ConnectionString;

namespace MyDodos.Service.ConnectionString
{
    public class ConnectionStringService : IConnectionStringService
    {
        private readonly IConnectionStringRepository _connectionStringRepository;
        private readonly IConfiguration _configuration;
        public ConnectionStringService(IConnectionStringRepository connectionStringRepository, IConfiguration configuration)
        {
            _connectionStringRepository = connectionStringRepository;
            _configuration = configuration;
        }
    }
}