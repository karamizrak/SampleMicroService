namespace SampleMicroService.Product.Settings
{
    public class ProductDatabaseSettings:IProductDatabaseSettings
    {
        public ProductDatabaseSettings()
        {
            
        }

        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string CollectionName { get; set; }
    }
}
