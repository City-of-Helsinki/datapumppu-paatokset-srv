using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhjoApiService.StorageClient
{
    internal interface IStorageConnection
    {
        HttpClient CreateConnection();
    }

    internal class StorageConnection : IStorageConnection
    {
        private readonly IConfiguration _configuration;

        public StorageConnection(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public HttpClient CreateConnection()
        {
            var connection = new HttpClient();
            connection.BaseAddress = new Uri(_configuration["Storage:url"]);
            return connection;
        }
    }
}
