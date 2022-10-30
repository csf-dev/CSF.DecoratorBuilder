using System;
using System.Reflection;
using Autofac;
using CSF.DecoratorBuilder.Factories;
using CSF.DecoratorBuilder.SampleService;
using NUnit.Framework;

namespace CSF.DecoratorBuilder
{
    [TestFixture, Parallelizable(ParallelScope.Self)]
    public class AutofacIntegrationTests
    {
        IContainer container;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<DecoratorBuilderModule>();
            builder
                .RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(x => x.GetCustomAttribute<TestFixtureAttribute>() == null)
                .AsSelf()
                .AsImplementedInterfaces();
            container = builder.Build();
        }

        [Test]
        public void Static_registration_can_get_decorated_service()
        {
            void CustomiseContainer(ContainerBuilder builder)
            {
                builder.RegisterDecoratedService<IServiceInterface>(d => d.UsingInitialImpl<ServiceImpl1>().ThenWrapWith<ServiceDecorator1>());
            }

            using (var scope = container.BeginLifetimeScope(CustomiseContainer))
            {
                var service = scope.Resolve<IServiceInterface>();
                Assert.That(() => service.ServiceMethod(), Is.EqualTo("ServiceDecorator1\nServiceImpl1"));
            }
        }

        [Test]
        public void Static_registration_can_get_decorated_service_using_nongeneric_api()
        {
            void CustomiseContainer(ContainerBuilder builder)
            {
                builder.RegisterDecoratedServiceType(typeof(IServiceInterface), d => d.UsingInitialImpl<ServiceImpl1>().ThenWrapWith<ServiceDecorator1>());
            }

            using (var scope = container.BeginLifetimeScope(CustomiseContainer))
            {
                var service = scope.Resolve<IServiceInterface>();
                Assert.That(() => service.ServiceMethod(), Is.EqualTo("ServiceDecorator1\nServiceImpl1"));
            }
        }

        [Test]
        public void Static_registration_can_provide_parameter_to_decorated_service()
        {
            void CustomiseContainer(ContainerBuilder builder)
            {
                builder.RegisterDecoratedService<IServiceInterface>(d => 
                    d.UsingInitialImpl<ServiceImpl1>()
                     .ThenWrapWith<ServiceDecorator1>()
                     .ThenWrapWith<ServiceDecorator2>(TypedParameter.From(5))
                );
            }

            using (var scope = container.BeginLifetimeScope(CustomiseContainer))
            {
                var service = scope.Resolve<IServiceInterface>();
                Assert.That(() => service.ServiceMethod(), Is.EqualTo("ServiceDecorator2: 5\nServiceDecorator1\nServiceImpl1"));
            }
        }

        [Test]
        public void Autofac_service_factory_can_get_decorated_service_with_runtime_named_parameter()
        {
            using (var scope = container.BeginLifetimeScope())
            {
                var factory = scope.Resolve<AutofacServiceFactory>();
                var service = factory.GetService(22);
                Assert.That(() => service.ServiceMethod(), Is.EqualTo("ServiceDecorator1\nServiceDecorator2: 22\nServiceImpl2"));
            }
        }

        [Test]
        public void Autofac_nongeneric_service_factory_can_get_decorated_service_with_runtime_named_parameter()
        {
            using (var scope = container.BeginLifetimeScope())
            {
                var factory = scope.Resolve<AutofacNonGenericServiceFactory>();
                var service = factory.GetService(22);

                Assert.That(service, Is.InstanceOf<IServiceInterface>(), "Service must be correct type");
                Assert.That(() => ((IServiceInterface) service).ServiceMethod(), Is.EqualTo("ServiceDecorator1\nServiceDecorator2: 22\nServiceImpl2"));
            }
        }

        [Test]
        public void Static_registration_may_use_decorator_which_uses_resolution_function()
        {
            void CustomiseContainer(ContainerBuilder builder)
            {
                builder.RegisterDecoratedService<IServiceInterface>(d => d.UsingInitialImpl<ServiceImpl1>().ThenWrapWith((impl, ctx, @params) => new ServiceDecorator2(impl, 5)));
            }

            using (var scope = container.BeginLifetimeScope(CustomiseContainer))
            {
                var service = scope.Resolve<IServiceInterface>();
                Assert.That(() => service.ServiceMethod(), Is.EqualTo("ServiceDecorator2: 5\nServiceImpl1"));
            }
        }

        [Test]
        public void Static_registration_may_use_initial_impl_which_uses_resolution_function()
        {
            void CustomiseContainer(ContainerBuilder builder)
            {
                builder.RegisterDecoratedService<IServiceInterface>(d => d.UsingInitialImpl((ctx, @params) => new ServiceImpl3()).ThenWrapWith<ServiceDecorator1>());
            }

            using (var scope = container.BeginLifetimeScope(CustomiseContainer))
            {
                var service = scope.Resolve<IServiceInterface>();
                Assert.That(() => service.ServiceMethod(), Is.EqualTo("ServiceDecorator1\nServiceImpl3"));
            }
        }
    }
}
