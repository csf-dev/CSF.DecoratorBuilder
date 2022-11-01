# Builder for services using the decorator pattern

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

* Microsoft.Extensions.DependencyInjection (and any DI container which may expose an `IServiceCollection`)
* [Autofac]

[Autofac]: https://autofac.org/
