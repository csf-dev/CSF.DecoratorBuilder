# API documentation

This is the API documentation for the CSF.DecoratorBuilder library.

## Installing the library

Install this library into your application dependency injection using whichever of the following techniques is appropriate.
If you are using **Microsoft.Extensions.DependencyInjection** for your application's DI then use the following.

```csharp
// The project where your dependency injection is configured requires a <PackageReference>
// for CSF.DecoratorBuilder.Extensions.DependencyInjection.
// `serviceCollection` is the IServiceCollection used to configure your DI.
using Microsoft.Extensions.DependencyInjection;

serviceCollection.AddDecoratorBuilder();
```

Alternatively, if you are using **Autofac** as your application's DI container, then instead the following.

```csharp
// The project where your dependency injection is configured requires a <PackageReference>
// for CSF.DecoratorBuilder.Autofac.
// `builder` is the ContainerBuilder used to configure your DI.
using CSF.DecoratorBuilder;

builder.RegisterModule<DecoratorBuilderModule>();
```

## Commonly-used types

Common entry points to the library are:

* **[`IGetsDecoratedService`]** - a factory which creates services using the decorator pattern.
    * This service requires only a direct dependency upon the package `CSF.DecoratorBuilder.Abstractions`.  Typical usage could include any projects in your application.
    * Inject this interface as a constructor dependency into your own types (such as factory classes) and use it to build services.
* **[`IGetsAutofacDecoratedService`]** - a factory which creates services using the decorator pattern and offers the full flexibility of Autofac.
    * This service requires a direct dependency upon both the packages `CSF.DecoratorBuilder.Autofac` & `Autofac`.  Typical usages are limited to projects which deal with configuring dependency injection.
    * Inject this interface as a constructor dependency into your own types (such as factory classes) and use it to build services.
* The extension methods of the **[`DecoratorServiceCollectionExtensions`]** class, to add services directly to dependency injection with the decorator pattern
    * These require a direct dependency upon the package `CSF.DecoratorBuilder.Extensions.DependencyInjection`.  Typical usages are limited to projects which deal with configuring dependency injection.
    * Use these directly alongside adding other services for dependency injection.
* The extension methods of the **[`DecoratorContainerBuilderExtensions`]** class, to add services directly to dependency injection with the decorator pattern, with the power of Autofac.
    * This service requires a direct dependency upon both the packages `CSF.DecoratorBuilder.Autofac` & `Autofac`.  Typical usages are limited to projects which deal with configuring dependency injection.
    * Use these directly alongside adding other services for dependency injection, such as within your own `Module` classes.

[`IGetsDecoratedService`]: xref:CSF.DecoratorBuilder.IGetsDecoratedService
[`IGetsAutofacDecoratedService`]: xref:CSF.DecoratorBuilder.IGetsAutofacDecoratedService
[`DecoratorServiceCollectionExtensions`]: xref:Microsoft.Extensions.DependencyInjection.DecoratorServiceCollectionExtensions
[`DecoratorContainerBuilderExtensions`]: xref:Autofac.DecoratorContainerBuilderExtensions
