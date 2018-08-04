using System;
using System.Collections.Generic;
using Autofac;
using System.Text;
using CardinalPOS.ViewModels;
using CardinalPOS.Services;
using CardinalPOS.Services.Interfaces;
using Xamarin.Forms;

namespace CardinalPOS
{
    public static class AutoFacContainerBuilder
    {
        public static IContainer CreateContainer()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<InitialViewModel>().SingleInstance();
            containerBuilder.RegisterType<MainPageViewModel>().SingleInstance();
            containerBuilder.RegisterType<TicketAccessControlViewModel>().SingleInstance();

            containerBuilder.RegisterType<UnAuthenticatedRequestService>().As<IRequestService>().SingleInstance();
            containerBuilder.RegisterType<SinglePageNavigationService>().As<INavigationService>().SingleInstance();

            containerBuilder.RegisterInstance(DependencyService.Get<INFCService>()).AsImplementedInterfaces().SingleInstance();

            //containerBuilder.RegisterType<TabsHubService>().As<ITabsHubService>().SingleInstance();

            return containerBuilder.Build();
        }
    }
}
