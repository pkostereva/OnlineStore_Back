using Autofac;
using OnlineStoreBack.API.Controllers;
using OnlineStoreBack.DB.Storages;
using OnlineStoreBack.Repository;

namespace OnlineStoreBack.API.Configuration
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProductStorage>().As<IProductStorage>();
            builder.RegisterType<OrderStorage>().As<IOrderStorage>();
            builder.RegisterType<ReportStorage>().As<IReportStorage>();
            builder.RegisterType<ProductRepository>().As<IProductRepository>();
            builder.RegisterType<OrderRepository>().As<IOrderRepository>();
            builder.RegisterType<ReportRepository>().As<IReportRepository>();
            builder.RegisterType<ProductController>().As<IProductController>();
            builder.RegisterType<ReportController>().As<IReportController>();
            builder.RegisterType<ConfigurationOptions>().As<IConfigurationOptions>();
        }
    }
}
