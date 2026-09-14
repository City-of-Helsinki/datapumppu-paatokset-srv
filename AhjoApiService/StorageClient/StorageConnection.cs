using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhjoApiService.StorageClient
{
    /// <summary>
    /// Defines a factory for creating <see cref="HttpClient"/> instances configured to communicate with the Datapumppu Storage API.
    /// </summary>
    internal interface IStorageConnection
    {
        /// <summary>
        /// Creates an <see cref="HttpClient"/> with the Storage API base address pre-configured from <c>STORAGE_URL</c>.
        /// </summary>
        /// <returns>A ready-to-use <see cref="HttpClient"/>.</returns>
        HttpClient CreateConnection();
    }

    /// <summary>
    /// Creates <see cref="HttpClient"/> instances for the Datapumppu Storage API using the
    /// <c>STORAGE_URL</c> configuration value as the base address.
    /// </summary>
    internal class StorageConnection : IStorageConnection
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initialises a new instance of <see cref="StorageConnection"/>.
        /// </summary>
        /// <param name="configuration">Application configuration providing <c>STORAGE_URL</c>.</param>
        public StorageConnection(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <inheritdoc />
        public HttpClient CreateConnection()
        {
            var connection = new HttpClient();
            connection.BaseAddress = new Uri(_configuration["STORAGE_URL"]);
            return connection;
        }
    }
}
