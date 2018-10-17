using SeeYouIn.Interfaces.LocalDB;
using SeeYouIn.Interfaces.Notifications;
using SeeYouIn.LocalDB.Repositories;
using SeeYouIn.Services;
using Unity;

namespace SeeYouIn.DI
{
  public static class Injector
  {
    private static UnityContainer _container = null;

    static Injector()
    {
      _container = new UnityContainer();
      _container.RegisterType<IReminderLocalService, ReminderRepository>();
      _container.RegisterType<INotificationLinkLocalService, NotificationLinkRepository>();
      _container.RegisterType<IReminderService, ReminderService>();
      _container.RegisterType<INotificationService, NotificationService>();
    }

    public static IUnityContainer Container { get { return _container; } }
  }
}
