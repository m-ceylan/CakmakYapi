using Cakmak.Yapi.Repository.Application;
using Cakmak.Yapi.Repository.Definition;
using Cakmak.Yapi.Repository.ProjectUser;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cakmak.Yapi.Helpers
{
   public static class ServiceExtensions
    {
        public static void ConfigureRepositories(this IServiceCollection services, string connectionString)
        {
            string baseDBName = "betayapi";

            #region Application  
            services.AddSingleton(x => new FeedBackRepository(connectionString, baseDBName, "feedBack"));
            services.AddSingleton(x => new AboutRepository(connectionString, baseDBName, "about"));
            #endregion
            #region Definition 
            services.AddSingleton(x => new CatalogRepository(connectionString, baseDBName, "catalog"));
            services.AddSingleton(x => new ServicesRepository(connectionString, baseDBName, "services"));
            services.AddSingleton(x => new WorkRepository(connectionString, baseDBName, "work"));
            #endregion
            #region Project User 
            services.AddSingleton(x => new UserRepository(connectionString, baseDBName, "user"));
            #endregion
        }
    }
}
