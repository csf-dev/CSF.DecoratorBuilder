# Decorator builder
This is a development plan for a service which will make it easier to flexibly build services which make use of [the decorator pattern] via [AutoFac].

[the decorator pattern]: https://en.wikipedia.org/wiki/Decorator_pattern
[AutoFac]: https://autofac.org/

I have decided to only support AutoFac directly within this project, because there is little to gain in abstracting away from AutoFac. Whilst it would be possible to abstract away (particularly via the dotnet core dependency injection abstractions), much of the power of the DI framework would be lost, such as named, typed and positional parameters.

## Desired requirements
The following indicates how I'd like to use this service, under a few different scenarios.

### Static registration
In this scenario, there is *only one way* to put 'the decorator stack' together. This is similar in concept to `Register(...).As<ISomeService>()`. The registration will always be used to fulfill that service type.

In this scenario we have direct access to a `ContainerBuilder` type and we are probably 'inside' an AutoFac module type. In this case I would like to make **extension methods** available from a `ContainerBuilder` type. This will provide some shortcuts toward writing the registration.

Here is a simple example of planned usage.

```csharp
// builder is an AutoFac ContainerBuilder
builder
  .RegisterDecoratedService<IMyService>(b => {
    return b.UsingBaseImpl<BaseImplementation>()
      .ThenWrapWith<Decorator1>()
      .ThenWrapWith<Decorator2>();
  });
```

### Creating from an AutoFac factory
In this scenario we have dependency-injected a decorator-builder as a service. Importantly, though, we are doing this *from an assembly/project which references AutoFac*. This means that we have access to the AutoFac types in our method signatures and may supply 'native' parameters and the like.

In this case, we are probably within a dependency-injection-configuration project, perhaps within a registration method or a factory class.

Here is a simple example of planned usage.

```csharp
// provider is an injected IGetsAutofacDecoratedService
var service = provider
  .GetDecoratedService<IMyService>(b => {
    return b.UsingBaseImpl<BaseImplementation>()
      .ThenWrapWith<Decorator1>()
      .ThenWrapWith<Decorator2>();
  });
```

### Creation from a non-AutoFac factory
This scenario applies when the builder is being used from a project/assembly *which does not reference AutoFac*. In this case we cannot use AutoFac types within our method signatures because the consumer has no access to them.

In this case we are being consumed, likely by a factory class, in a project which does not receive AutoFac at all.

This scenario will come with some limitations on usage because we will not be able to offer direct access to AutoFac functionality. Instead we will have to leverage technologies such as *delegate factories* in order to resolve components sensibly.

Here is a simple example of planned usage.

```csharp
// provider is an injected IGetsDecoratedService
var service = provider
  .GetDecoratedService<IMyService>(b => {
    return b.UsingInitialImpl<BaseImplementation>()
      .ThenWrapWith<Decorator1>()
      .ThenWrapWith<Decorator2>();
  });
```

*Notice that this example is identical to the one above* except for the name of the provider interface. What is important about this is that advanced features such as directly supplying AutoFac parameters etc will not be available in this version.

## Registering the builder itself
I would like to provide **an AutoFac module** in order to register the builder and its components.

## Desired functionality
### Choosing relevant types
Most of the time we will be choosing these three types via generic type parameters:

* The service/interface type
* The initial implementation type
* The decorator implementation types

I would like it to be possible, though, to choose these types via a normal method parameter of `System.Type`. It will be almost certain that if this occurs, other related functionality may be compromised because of the lack of generic type safety and inference.

### Providing parameters
I would like two points at which parameters may be provided for the purpose of resolving the implementation types.


# Outdated past this point
Everything that follows are old notes, for revision.




```csharp
public interface ISelectsBaseImplementation
{
    IWrapsWithDecorators<TService> WithBaseImplementation<TImpl>()
        where TImpl : class,TService;
    
    IWrapsWithDecorators<TService> WithBaseImplementation(Type implType);

    IWrapsWithDecorators<TService> WithBaseImplementation<TImpl>(Func<IComponentContext,TImpl> factory)
        where TImpl : class,TService;
}
```

## Parameters
Each of the functions: `WithBaseImplementation` & `ThenWrapWith` (with the exception of the overload which takes a factory function) will also take a `params` array of AutoFac parameters. These will be extra parameters passed to the resolution of the component.