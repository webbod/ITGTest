using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc4;
using ITG.Interfaces;
using ITG.DataSources;
using System.Configuration;
using System.IO;
using System;
using System.Web;

namespace ITG.Web
{
    public static class Bootstrapper
    {
        public static IUnityContainer Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            return container;
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();    

            container.RegisterType<IDataSource, TestDataSource>();

            // get the configuration infromation from web.config
            var fileName = ConfigurationManager.AppSettings["Articles.TestDataSourceFile"];
            var path = Path.Combine(HttpContext.Current.Server.MapPath("~/App_Data"), fileName);
            var pageSize = Int32.Parse(ConfigurationManager.AppSettings["Articles.PageSize"]);

            if (string.IsNullOrEmpty(fileName) || pageSize < 1)
            {
                throw new ConfigurationErrorsException();
            }


            container.RegisterType<IDataSource, TestDataSource>();
            container.Resolve<IDataSource>(new ParameterOverrides { { "path", path }, { "pageSize", pageSize } });
            container.RegisterInstance<TestDataSource>(new TestDataSource(path, pageSize));

            RegisterTypes(container);

            return container;
        }

        public static void RegisterTypes(IUnityContainer container)
        {

        }
    }
}