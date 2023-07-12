using TeamsLeague.BLL.Interfaces;
using TeamsLeague.BLL.Services;
using TeamsLeague.UI.WPF.Buffer;
using Unity;
using Unity.Lifetime;
using Unity.Resolution;

namespace TeamsLeague.UI.WPF.Configuration
{
    public static class UnityContainerProvider
    {
        private static readonly UnityContainer Container;

        static UnityContainerProvider()
        {
            Container = new UnityContainer();
            Container.RegisterType<ICashBasket, CashBasket>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IUserServices, UserServices>();
            Container.RegisterType<ITeamService, TeamService>();
            Container.RegisterType<IMemberService, MemberService>();
        }

        public static UnityContainer GetContainer()
        {
            return Container;
        }

        public static T GetNew<T>() => Container.Resolve<T>();
        public static T GetNew<T>(params ParameterOverride[] overrides) => Container.Resolve<T>(overrides);
    }
}
