﻿using System;
using TeamsLeague.BLL.Interfaces;
using TeamsLeague.BLL.Services;
using TeamsLeague.BLL.Services.Builders;
using TeamsLeague.BLL.Services.Generators;
using TeamsLeague.DAL.Context;
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
            Container.RegisterType<GameDBContext>(new ContainerControlledLifetimeManager());
            Container.RegisterType<Random>(new ContainerControlledLifetimeManager());
            Container.RegisterType<ICashBasket, CashBasket>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IGameMech, GameMech>();
            Container.RegisterType<IGameGenerator, GameGenerator>();
            Container.RegisterType<IUserServices, UserServices>();
            Container.RegisterType<ITeamService, TeamService>();
            Container.RegisterType<IMemberService, MemberService>();
            Container.RegisterType<IMatchService, MatchService>();
            Container.RegisterType<IImagesService, ImagesService>();
            Container.RegisterType<ITeamBuilder, TeamBuilder>();
            Container.RegisterType<IMemberBuilder, MemberBuilder>();
            Container.RegisterType<IMatchBuilder, MatchBuilder>();
        }


        public static UnityContainer GetContainer()
        {
            return Container;
        }


        public static T GetNew<T>() => Container.Resolve<T>();
        public static T GetNew<T>(params ParameterOverride[] overrides) => Container.Resolve<T>(overrides);
    }
}
