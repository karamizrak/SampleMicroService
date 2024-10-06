using Microsoft.AspNetCore.Server.HttpSys;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;

namespace SampleMicroService.Product.Settings
{
    public interface IProductDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string CollectionName { get; set; }
    }
}
