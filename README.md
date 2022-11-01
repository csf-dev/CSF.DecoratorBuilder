# Builder for services using the decorator pattern

[![Build status](https://ci.appveyor.com/api/projects/status/bwjci4ua649dp12m/branch/master?svg=true)](https://ci.appveyor.com/project/craigfowler/csf-decoratorbuilder/branch/master)
[![Quality Gate status](https://sonarcloud.io/api/project_badges/measure?project=AutoFac.DecoratorBuilder&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=AutoFac.DecoratorBuilder)
[![Test coverage](https://sonarcloud.io/api/project_badges/measure?project=AutoFac.DecoratorBuilder&metric=coverage)](https://sonarcloud.io/summary/new_code?id=AutoFac.DecoratorBuilder)

This library presents a fluent builder to configure how services are assembled using _the decorator or chain of responsibility patterns_.
Full [documentation is available on the project website].

## Example

Here is a brief example of usage, for a project that uses `Microsoft.Extensions.DependencyInjection`.

```csharp
using Microsoft.Extensions.DependencyInjection;

serviceCollection.AddDecoratorBuilder();

serviceCollection.AddTransient<ServiceImplementation>();
serviceCollection.AddTransient<ServiceDecoratorOne>();
serviceCollection.AddTransient<ServiceDecoratorTwo>();

serviceCollection.AddTransientDecorator<IServiceInterface>(d =>
    d.UsingInitialImpl<ServiceImplementation>()
     .ThenWrapWith<ServiceDecoratorOne>()
     .ThenWrapWith<ServiceDecoratorTwo>()
);
```

This example configures DI such that when a dependency upon `IServiceInterface` is resolved, the object received would be:

* An instance of `ServiceDecoratorTwo` ...
* ... wrapping an instance of `ServiceDecoratorOne` ...
* ... wrapping an instance of `ServiceImplementation`

[documentation is available on the project website]: https://csf-dev.github.io/CSF.DecoratorBuilder/

## Supported environments

This library is built for `netstandard2.0`, offering a wide range of framework support.
It offers packages which support either/both of:

* Microsoft.Extensions.DependencyInjection
    * Includes any DI container which exposes an implementation of `IServiceCollection`
* [Autofac]

[Autofac]: https://autofac.org/
