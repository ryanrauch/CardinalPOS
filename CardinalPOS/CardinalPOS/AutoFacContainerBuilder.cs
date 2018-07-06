using System;
using System.Collections.Generic;
using Autofac;
using System.Text;
using CardinalPOS.ViewModels;

namespace CardinalPOS
{
    public static class AutoFacContainerBuilder
    {
        public static IContainer CreateContainer()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<InitialViewModel>().SingleInstance();

            return containerBuilder.Build();
        }
    }
}
