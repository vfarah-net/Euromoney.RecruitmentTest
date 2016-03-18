using System;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Domain.Models;
using Domain.Repository;
using Domain.Resolvers;

namespace Domain.Windsor
{
    public class BadWordResolverInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                    Component.For<IBadWordResolver>()
                    .ImplementedBy<BadWordResolver>()
                    .LifestyleSingleton(),
                    Component.For<IRepository<BadWord>>()
                    .ImplementedBy<SimpleRepository<BadWord>>()
                    .LifestyleSingleton()
                    );
        }
    }
}
