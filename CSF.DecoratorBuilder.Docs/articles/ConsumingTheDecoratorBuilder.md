# Using the decorator builder

The [static dependency injection technique] for configuring services to be assembled via the decorator pattern is the simplest when _For interface X, I always want implementation Y, wrapped with Z decorator(s), in the same order._
When the above is not applicable then you may gain more flexibility by consuming the decorator builder directly within your own application.

Consuming the decorator builder directly is best accomplished from [a **factory class**].
It is best not to scatter dependencies upon the decorator builder around other business logic, lest it accidentally become [a service locator].
When using a decorator builder, you may write your own arbitrary logic to determine:

* Which primary implementation to use
* Which decorator(s) to use
* The order in which those decorators should wrap one another

[static dependency injection technique]: StaticDependencyInjection.md
[a **factory class**]: https://en.wikipedia.org/wiki/Factory_method_pattern
[a service locator]: https://blog.ploeh.dk/2010/02/03/ServiceLocatorisanAnti-Pattern/

## If possible, rely only upon `IGetsDecoratedService`

When you need to use the decorator builder in your own logic, the advice is to try and use the interface [`IGetsDecoratedService`] if you possibly can.
This interfaces resides in the `CSF.DecoratorBuilder.Abstractions` [NuGet package], which is _a very lightweight dependency_.
You might find this dependency suitable to take as a `<PackageReference>` in your main business logic projects.
Other packages for this library are suitable only as dependencies to projects that deal directly with configuring dependency injection, such as your startup project.

Here is an example of a hypothetical class which uses the decorator builder, with `IGetsDecoratedService`.

```csharp
using CSF.DecoratorBuilder;

public class UserInterfaceFormatterFactory
{
    readonly IGetsDecoratedService decoratorBuilder;
    readonly IGetsAccessibilitySettings accessibilitySettingsProvider;

    public IGetsUserInterfaceFormat GetTaxAmountProvider()
    {
        return decoratorBuilder.GetDecoratedService<IGetsUserInterfaceFormat>(initial => {
            var customiser = initial.UsingInitialImpl<BasicUiFormatter>();
            customiser = customiser.ThenWrapWith<TutorialsUiFormatDecorator>();

            if(accessibilitySettingsProvider.IsColourBlind())
                customiser = customiser.ThenWrapWith<ColourBlindUiFormatDecorator>();

            if(accessibilitySettingsProvider.UsesLargerUi())
                customiser = customiser.ThenWrapWith<LargerUiFormatDecorator>();

            customiser = customiser.ThenWrapWith<ThemeAwareUiFormatDecorator>();

            return customiser;
        });
    }

    public UserInterfaceFormatterFactory(IGetsDecoratedService decoratorBuilder,
                                         IGetsAccessibilitySettings accessibilitySettingsProvider)
    {
        this.decoratorBuilder = decoratorBuilder;
        this.accessibilitySettingsProvider = accessibilitySettingsProvider;
    }
}
```

The example above demonstrates the use of an external (hypothetical) dependency upon an `IGetsAccessibilitySettings` to decide which of two additional decorators to apply to the final service when it is assembled.

[`IGetsDecoratedService`]: xref:CSF.DecoratorBuilder.IGetsDecoratedService
[NuGet package]: NuGetPackages.md

## If required, use `IGetsAutofacDecoratedService`

The alternative to the above is to use the [`IGetsAutofacDecoratedService`] interface.
This is used in the same was way as the example above, but it offers the power of the [Autofac] API, including:

* [Autofac parameters]
* Access to the `IComponentContext` for more complex resolution

The drawbacks to this approach are somewhat obvious:

* You _must be using Autofac_ for your application's DI.
* Projects that make use of `IGetsAutofacDecoratedService` require a NuGet package dependency upon both `CSF.DecoratorBuilder.Autofac` & `Autofac` itself.
These are much _larger and higher-risk dependencies_ than those required by the non-Autofac decorator builder described above.

When using the Autofac decorator builder, it is best to add your factory classes only to projects which deal directly with dependency injection, such as your startup project.
This avoids needing to add large dependencies across your whole solution.

[`IGetsAutofacDecoratedService`]: xref:CSF.DecoratorBuilder.IGetsAutofacDecoratedService
[Autofac]: https://autofac.org/
[Autofac parameters]: https://docs.autofac.org/en/latest/register/parameters.html
