# NuGet packages

CSF.DecoratorBuilder consists of three NuGet packages.
Most applications only need to depend upon _a maximum of two of them_.

## If you are using Microsoft.Extensions.DependencyInjection

If the dependency injection for your application is based upon any implementation of [`Microsoft.Extensions.DependencyInjection.IServiceCollection`] then you may use the following two packages:

* [`CSF.DecoratorBuilder.Abstractions`]
* [`CSF.DecoratorBuilder.Extensions.DependencyInjection`]

**The abstractions package** is very small and unlikely to undergo significant changes as versions increment.
If you wish to write factory classes/services within your application logic, outside of projects which deal with dependency injection directly, then this package is quite safe to add as a dependency.
This provides access to the interface [`IGetsDecoratedService`] which may be constructor-injected into your own logic, to build services.

The other package, above, is only required as a reference in your startup project (or wherever dependency injection is configured).
See [the API documentation] for information about how to install the decorator builder into your app.

[`Microsoft.Extensions.DependencyInjection.IServiceCollection`]: https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.iservicecollection
[`CSF.DecoratorBuilder.Abstractions`]: https://www.nuget.org/packages/CSF.DecoratorBuilder.Abstractions
[`CSF.DecoratorBuilder.Extensions.DependencyInjection`]: https://www.nuget.org/packages/CSF.DecoratorBuilder.Extensions.DependencyInjection
[`IGetsDecoratedService`]: xref:CSF.DecoratorBuilder.IGetsDecoratedService
[the API documentation]: ../api/index.md

## If you are using Autofac

If your dependency injection is based upon [Autofac], then the packages which shall be used by your app are:

* [`CSF.DecoratorBuilder.Abstractions`]
* [`CSF.DecoratorBuilder.Autofac`]

Notice that **the abstractions package** is present here also, exactly the same as noted above applies.
You may use the [`IGetsDecoratedService`] interface from that package to create decorator-based services in your own app.

The other package, above, is only required as a reference in your startup project (or wherever dependency injection is configured).
As above [the API documentation] has a writeup of how to install the library into your app.

[Autofac]: https://autofac.org/
[`CSF.DecoratorBuilder.Autofac`]: https://www.nuget.org/packages/CSF.DecoratorBuilder.Autofac
