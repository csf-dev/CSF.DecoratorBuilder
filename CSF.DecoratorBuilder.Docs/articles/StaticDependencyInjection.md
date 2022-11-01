# Static dependency injection

Static dependency injection refers to configuring your dependency injection such that when your service is resolved, typically via an interface, it always uses the same configuration of primary implementation class and decorator(s).
This differs to [using the decorator builder directly] where instances are created via a factory and the implementation classes, including which decorators are used and in which order they wrap one another, may differ each time.

Note that the two examples shown below _are functionally equivalent_.
The Autofac API is more powerful than the Microsoft.Extensions.DependencyInjection API, at the cost of being compatible only with Autofac.
The Microsoft.Extensions.DependencyInjection API is compatible with any DI container that exposes an implementation of `IServiceCollection`.

[using the decorator builder directly]: ConsumingTheDecoratorBuilder.md

## With Microsoft.Extensions.DependencyInjection

When using any implementation of [`Microsoft.Extensions.DependencyInjection.IServiceCollection`] to configure your DI, you will want to use the extension methods present upon [`DecoratorServiceCollectionExtensions`] to configure your services.
Here is an example of the technique; _remember to also install the library into your dependency injection_ as described in [the API docs].

```csharp
using Microsoft.Extensions.DependencyInjection;

// All three of these classes are assumed to implement IServiceInterface.
// Both the 'decorator' classes would take a constructor dependency upon an
// instance of IServiceInterface in order to wrap its functionality.
serviceCollection.AddTransient<ServiceImplementation>();
serviceCollection.AddTransient<ServiceDecoratorOne>();
serviceCollection.AddTransient<ServiceDecoratorTwo>();

serviceCollection.AddTransientDecorator<IServiceInterface>(d =>
    d.UsingInitialImpl<ServiceImplementation>()
     .ThenWrapWith<ServiceDecoratorOne>()
     .ThenWrapWith<ServiceDecoratorTwo>()
);
```

Notice how each of the three classes `ServiceImplementation`, `ServiceDecoratorOne` & `ServiceDecoratorTwo` is added individually first, but without adding them 'as' their interface.
Then, the `AddTransientDecorator` method is to used to add the assembled service, built via the decorator pattern, 'as' the interface.

Following the logic of this example, if `IServiceInterface` were injected as a constructor dependency elsewhere in the app, the object received as a dependency would be:

* An instance of `ServiceDecoratorTwo` ...
* ... wrapping an instance of `ServiceDecoratorOne` ...
* ... wrapping an instance of `ServiceImplementation`

[`Microsoft.Extensions.DependencyInjection.IServiceCollection`]: https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.iservicecollection
[`DecoratorServiceCollectionExtensions`]: xref:Microsoft.Extensions.DependencyInjection.DecoratorServiceCollectionExtensions
[the API docs]: ../api/index.md

## With Autofac

When using [Autofac] to configure your DI, you will want to use the extension methods present upon [`DecoratorContainerBuilderExtensions`] to configure your services.
Here is an example of the technique; _remember to also install the library into your dependency injection_ as described in [the API docs].

```csharp
using Autofac;

// All three of these classes are assumed to implement IServiceInterface.
// Both the 'decorator' classes would take a constructor dependency upon an
// instance of IServiceInterface in order to wrap its functionality.
builder.RegisterType<ServiceImplementation>();
builder.RegisterType<ServiceDecoratorOne>();
builder.RegisterType<ServiceDecoratorTwo>();

builder.RegisterDecoratedService<IServiceInterface>(d =>
    d.UsingInitialImpl<ServiceImplementation>()
     .ThenWrapWith<ServiceDecoratorOne>()
     .ThenWrapWith<ServiceDecoratorTwo>()
);
```

As with the previous example, each of the three classes `ServiceImplementation`, `ServiceDecoratorOne` & `ServiceDecoratorTwo` are registered individually, without `.As<IServiceInterface>()`.
The `RegisterDecoratedService` method then handles the registstration for the interface, returning a service assembled via the decorator pattern.

This example would produce the same outcome as the example above, if `IServiceInterface` were injected as a constructor dependency elsewhere in the app.

[`DecoratorContainerBuilderExtensions`]: xref:Autofac.DecoratorContainerBuilderExtensions