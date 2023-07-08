using TeamsLeague.BLL.Interfaces;
using TeamsLeague.BLL.Services;
using Unity;
using Unity.Lifetime;

namespace TeamsLeague.UI.WPF.Configuration
{
    public static class UnityContainerProvider
    {
        private static readonly UnityContainer Container;

        static UnityContainerProvider()
        {
            Container = new UnityContainer();
            Container.RegisterType<IBufferingService, BufferingService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IUserServices, UserServices>();
            Container.RegisterType<ITeamServices, TeamServices>();
            Container.RegisterType<IMemberServices, MemberServices>();
        }

        public static UnityContainer GetContainer()
        {
            return Container;
        }
    }
}
