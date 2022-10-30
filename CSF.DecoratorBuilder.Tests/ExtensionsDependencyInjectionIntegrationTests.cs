using CSF.DecoratorBuilder.Factories;
using CSF.DecoratorBuilder.SampleService;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace CSF.DecoratorBuilder
{
    [TestFixture, Parallelizable(ParallelScope.Fixtures)]
    public class ExtensionsDependencyInjectionIntegrationTests
    {
        IServiceCollection services;

        [SetUp]
        public void Setup()
        {
            services = new ServiceCollection();
            services.AddDecoratorBuilder();
            services.AddTransient<ServiceDecorator1>();
            services.AddTransient<ServiceDecorator2>();
            services.AddTransient<ServiceImpl1>();
            services.AddTransient<ServiceImpl2>();
            services.AddTransient<ServiceImpl3>();
            services.AddTransient<NonAutofacServiceFactory>();
            services.AddTransient<NonAutofacNonGenericServiceFactory>();
        }

        [Test]
        public void Static_registration_can_get_decorated_service()
        {
            services.AddTransientDecorator<IServiceInterface>(d => d.UsingInitialImpl<ServiceImpl1>().ThenWrapWith<ServiceDecorator1>());

            using (var serviceProvider = services.BuildServiceProvider())
            {
                var service = serviceProvider.GetRequiredService<IServiceInterface>();
                Assert.That(() => service.ServiceMethod(), Is.EqualTo("ServiceDecorator1\nServiceImpl1"));
            }
        }

        [Test]
        public void Static_registration_can_provide_parameter_to_decorated_service()
        {
            services.AddTransientDecorator<IServiceInterface>(d => 
                d.UsingInitialImpl<ServiceImpl1>()
                    .ThenWrapWith<ServiceDecorator1>()
                    .ThenWrapWith<ServiceDecorator2>(TypedParam.From(5))
            );

            using (var serviceProvider = services.BuildServiceProvider())
            {
                var service = serviceProvider.GetRequiredService<IServiceInterface>();
                Assert.That(() => service.ServiceMethod(), Is.EqualTo("ServiceDecorator2: 5\nServiceDecorator1\nServiceImpl1"));
            }
        }

        [Test]
        public void Non_autofac_service_factory_can_get_decorated_service_with_runtime_typed_parameter()
        {
            using (var serviceProvider = services.BuildServiceProvider())
            {
                var factory = serviceProvider.GetRequiredService<NonAutofacServiceFactory>();
                var service = factory.GetService(66);
                Assert.That(() => service.ServiceMethod(), Is.EqualTo("ServiceDecorator1\nServiceDecorator2: 66\nServiceImpl2"));
            }
        }

        [Test]
        public void Non_autofac_nongeneric_service_factory_can_get_decorated_service_with_runtime_typed_parameter()
        {
            using (var serviceProvider = services.BuildServiceProvider())
            {
                var factory = serviceProvider.GetRequiredService<NonAutofacNonGenericServiceFactory>();
                var service = factory.GetService(66);

                Assert.That(service, Is.InstanceOf<IServiceInterface>(), "Service must be correct type");
                Assert.That(() => ((IServiceInterface) service).ServiceMethod(), Is.EqualTo("ServiceDecorator1\nServiceDecorator2: 66\nServiceImpl2"));
            }
        }

        [Test]
        public void Static_registration_may_use_decorator_which_uses_resolution_function()
        {
            services.AddTransientDecorator<IServiceInterface>(d => d.UsingInitialImpl<ServiceImpl1>().ThenWrapWith((impl, sp, @params) => new ServiceDecorator2(impl, 5)));

            using (var serviceProvider = services.BuildServiceProvider())
            {
                var service = serviceProvider.GetRequiredService<IServiceInterface>();
                Assert.That(() => service.ServiceMethod(), Is.EqualTo("ServiceDecorator2: 5\nServiceImpl1"));
            }
        }

        [Test]
        public void Static_registration_may_use_initial_impl_which_uses_resolution_function()
        {
            services.AddTransientDecorator<IServiceInterface>(d => d.UsingInitialImpl((s, @params) => new ServiceImpl3()).ThenWrapWith<ServiceDecorator1>());

            using (var serviceProvider = services.BuildServiceProvider())
            {
                var service = serviceProvider.GetRequiredService<IServiceInterface>();
                Assert.That(() => service.ServiceMethod(), Is.EqualTo("ServiceDecorator1\nServiceImpl3"));
            }
        }

        [Test]
        public void Static_registration_may_use_decorator_which_uses_resolved_parameter()
        {
            services.AddTransientDecorator<IServiceInterface>(d => d.UsingInitialImpl<ServiceImpl1>().ThenWrapWith<ServiceDecorator2>(new ResolvedType(typeof(int), s => 5)));

            using (var serviceProvider = services.BuildServiceProvider())
            {
                var service = serviceProvider.GetRequiredService<IServiceInterface>();
                Assert.That(() => service.ServiceMethod(), Is.EqualTo("ServiceDecorator2: 5\nServiceImpl1"));
            }
        }
    }
}