## Planned usage
Here's a sample usage of the decorator builder.

It will be tied to AutoFac, because of the need to use an `IComponentContext` as a parameter. Whilst I could try to make a generalised version, there's presently no need.

```csharp
BuildDecorator.For<TService>(ctx)
    .WithBaseImplementation<TImpl1>()
    .ThenWrapWith<TImpl2>()
    .ThenWrapWith<TImpl3>()
    .Build();
```

## Service type
The service type generic type parameter indicates the interface/base type for which we are creating a decorator stack.

## Base implementation
The base implementation function needs a few overloads. The most commonly used would be the one which takes only a generic type parameter.

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