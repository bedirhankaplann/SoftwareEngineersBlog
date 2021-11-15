using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SoftwareEngineersBlog.Data.Abstract;
using SoftwareEngineersBlog.Data.Concrete;
using SoftwareEngineersBlog.Data.Concrete.EntityFramework.Contexts;
using SoftwareEngineersBlog.Services.Abstract;
using SoftwareEngineersBlog.Services.Concrete;

namespace SoftwareEngineersBlog.Services.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection LoadMyServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContext<SoftwareEngineersBlogContext>();
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
            serviceCollection.AddScoped<ICategoryService, CategoryManager>();
            serviceCollection.AddScoped<IArticleService, ArticleManager>();

            return serviceCollection;
        }
    }
}
