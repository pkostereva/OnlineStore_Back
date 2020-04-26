using Autofac;
using OnlineStore_Back.API.Controllers;
using OnlineStore_Back.DB.Storages;
using OnlineStore_Back.Repository;

namespace OnlineStore_Back.API.Configuration
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProductStorage>().As<IProductStorage>();
            builder.RegisterType<ProductRepository>().As<IProductRepository>();
            builder.RegisterType<ProductController>().As<IProductController>();
            builder.RegisterType<ConfigurationOptions>().As<IConfigurationOptions>();
        }
    }
}
