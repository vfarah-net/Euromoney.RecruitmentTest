using System;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Domain.Windsor;

namespace ContentConsole
{
    public static class BootstrapConfig
    {
        public static WindsorContainer Register()
        {
            var windsorContainer = new WindsorContainer();
            windsorContainer.Kernel.Resolver.AddSubResolver(new ArrayResolver(windsorContainer.Kernel));
            windsorContainer.Kernel.Resolver.AddSubResolver(new CollectionResolver(windsorContainer.Kernel));
            windsorContainer.Register(Component.For<IWindsorContainer>().Instance(windsorContainer));
            windsorContainer.Install(new BadWordResolverInstaller());
            return windsorContainer;
        }
    }
}
