using Microsoft.Extensions.DependencyInjection;
using SeeYouIn.Interfaces.LocalDB;
using SeeYouIn.LocalDB.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using Unity;

namespace SeeYouIn.DI
{
    public static class Injector
    {
        private static UnityContainer _container = null;

        static Injector() {
            _container = new UnityContainer();
            _container.RegisterType<IReminderService,ReminderRepository>();
        }

        public static IUnityContainer Container { get { return _container; } }
    }
}
