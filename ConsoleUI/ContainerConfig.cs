using Autofac;
using DemoLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ConsoleUI
{
    public static class Containerconfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<Application>().As<IApplication>();
            builder.RegisterType<BusinessLogic>().As<IBusinessLogic>();

            /* 
             * This part registers ALL classes in namespace Utilities to prevent
             * writing/wiring every class one by one.
             * 
             * i represents the interfaces, t represents the classes 
             */
            builder.RegisterAssemblyTypes(Assembly.Load(nameof(DemoLibrary)))
                .Where( t=> t.Namespace.Contains("Utilities"))
                .As( t => t.GetInterfaces().FirstOrDefault( i => i.Name == "I" + t.Name ));

            /* For example, if i have an Oracle implementation, like DataAccessOracle, etc, i should be 
             *
             * builder.RegisterAssemblyTypes(Assembly.Load(nameof(DemoLibrary)))
                .Where( t=> t.Namespace.Contains("Utilities"))
                .As( t => t.GetInterfaces().FirstOrDefault( i => i.Name == "I" + t.Name.Replace("Oracle","") ));
             */


            return builder.Build();
        }
    }
}
