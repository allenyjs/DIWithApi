using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using System.Reflection;
using DIWithApi.Interface;
using DIWithApi.Service;
using System.Collections.ObjectModel;

namespace DIWithApi
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            // autofac


            AutofacRegistration();
        }
        public static void AutofacRegistration()
        {
            var builder = new ContainerBuilder();
            // Controller
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            // Services
            #region GetAssembly
            var assemblies = System.IO.Directory.EnumerateFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll", System.IO.SearchOption.AllDirectories)
           .Select(Assembly.LoadFrom);

            var Services = assemblies.Where(x => x.FullName.StartsWith("DIWithApi.Service"));
            var ServiceTypes = Services.First().DefinedTypes;
            #endregion

            #region 註冊types
            foreach (var serviceType in ServiceTypes)
            {
                builder
                    .RegisterType(serviceType)
                    .AsImplementedInterfaces();
            }
            #endregion

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

        }

        public void GetAssembly()
        {

        }
    }
}
