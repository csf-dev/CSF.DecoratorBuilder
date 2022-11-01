# Decorator Builder

**CSF.DecoratorBuilder** is a small library to assist developers in making use of the decorator pattern in their own .NET projects.
It provides a structured, easy-to-read & boilerplate-free way to configure how services are created through dependency injection, using the decorator or chain of responsibility patterns.

## TL;DR what's decorator?

The [decorator pattern] is characterised by multiple classes _which implement the same interface_.
A decorator class takes a dependency upon (it wraps) an object of the same interface as it implements.

The functionality of a decorator class is typically _"do whatever the wrapped object would have done, plus the functionality in this class"_.
The decorator pattern is well-used to add functionality to (to "decorate") an existing service, without needing to edit the original class that provides the service.
Thus it helps developers adhere to [open/closed principle].

There is also a good writeup of decorator on [the refactoring guru website].

[decorator pattern]: https://en.wikipedia.org/wiki/Decorator_pattern
[the refactoring guru website]: https://refactoring.guru/design-patterns/decorator
[open/closed principle]: https://en.wikipedia.org/wiki/Open%E2%80%93closed_principle

### Chain of responsibility too

The [chain of responsibility pattern] is structurally the same as decorator except it is characterised by different behaviour.
In a chain of responsibility each class determines _"can I do the work?"_.
If the class can, then it performs whatever functional task is required and/or returns a result.
If it cannot then it does nothing more than return the result from the wrapped object (which has the same interface).

This library can also support chain of responsibility interchangeably with decorator.

[chain of responsibility pattern]: https://en.wikipedia.org/wiki/Chain-of-responsibility_pattern

## How this library is used

There are a few ways to use this library.
Firstly you will want to [choose the right NuGet package(s)]; this library is usable with either or both of [Microsoft.DependencyInjection.Abstractions] or [Autofac], depending upon your dependency injection needs.

This library supports both [static dependency injection logic] & [using the decorator builder] directly, such as within your own factory classes.
These two techniques may also be mixed and used together, as appropriate for the needs of your application.

[choose the right NuGet package(s)]: articles/NuGetPackages.md
[Microsoft.DependencyInjection.Abstractions]: https://www.nuget.org/packages/Microsoft.Extensions.DependencyInjection.Abstractions/
[Autofac]: https://autofac.org/
[static dependency injection logic]: articles/StaticDependencyInjection.md
[using the decorator builder]: articles/ConsumingTheDecoratorBuilder.md