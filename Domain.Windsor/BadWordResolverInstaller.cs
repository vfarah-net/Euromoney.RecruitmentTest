using System;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Domain.Windsor
{
    public class BadWordResolverInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IBadWordResolver>()
                    .ImplementedBy<BadWordResolver>()
                    .LifestyleSingleton());
        }
    }
}
