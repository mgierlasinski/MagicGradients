## About

Draw breathtaking backgrounds in your Xamarin.Forms application today, from simple gradients to complex textures. It's a kind of magic.

### Supported platforms
- Android
- iOS
- UWP

### Features

- Linear and radial gradients supported
- Draw multiple gradients with single control, blend them together for unique effects
- Supports CSS gradients, find your ideal background [somewhere on the web](https://www.gradientmagic.com/) and copy + paste into your app
- Paint gradient on any shape or text with clipping masks
- Make your background alive with built-in XAML animations
- Powered by [![Nuget](https://img.shields.io/badge/SkiaSharp-v1.68.1-blue)](https://www.nuget.org/packages/SkiaSharp/)

## Installation 

Install into shared project, no additional setup required.

To start using `Magic Gradients` in XAML import namespace:
``` xml
<ContentPage xmlns:magic="http://magic.com/schemas/gradients" />
```

## Drawing gradient

To draw a gradient add `GradientView` control to your XAML page and create `LinearGradient` or `RadialGradient` as direct content.

``` xml
<magic:GradientView>
    <magic:LinearGradient>
        <magic:GradientStop Color="Red" />
        <magic:GradientStop Color="Yellow" />
    </magic:LinearGradient>
</magic:GradientView>
```

It is also possible to add collection of gradients. You can mix linear and radial gradients, use transparency in color definitions to get blend effect. 

``` xml
<magic:GradientView>
    <magic:GradientCollection>
        <magic:LinearGradient Angle="45">
            <magic:GradientStop Color="Orange" Offset="0" />
            <magic:GradientStop Color="#ff0000" Offset="0.6" />
        </magic:LinearGradient>
        <magic:LinearGradient Angle="90">
            <magic:GradientStop Color="#33ff0000" Offset="0.4" />
            <magic:GradientStop Color="#ff00ff00" Offset="1" />
        </magic:LinearGradient>
    </magic:GradientCollection>
</magic:GradientView>
```

You can also build gradient in C# using `GradientBuilder` with Fluent API:

``` c#
 var gradients = new GradientBuilder()
    .AddLinearGradient(g => g
        .Rotate(45)
        .AddStop(Color.Red, Offset.Prop(0.2))
        .AddStop(Color.Blue, Offset.Prop(0.4)))
    .AddRadialGradient(g => g
        .Circle().At(0.5, 0.5, o => o.Proportional())
        .Radius(200, 200, o => o.Absolute())
        .StretchTo(RadialGradientSize.FarthestSide)
        .Repeat()
        .AddStops(Color.Red, Color.Green, Color.Blue))
    .AddCssGradient("linear-gradient(red, orange)")
    .Build();
```

To apply gradient created in C#, you can use `ToSource()` extension method:

``` c#
var source = new GradientBuilder()
    .AddLinearGradient(g => g
        .Rotate(20)
        .AddStops(Color.Red, Color.Green, Color.Blue))
    .ToSource();

gradientView.GradientSource = source;
```

## Styling with CSS

CSS gradient can be embeded in XAML inline:

``` xml
<magic:GradientView>
    <magic:CssGradientSource>
        <x:String>
            <![CDATA[
                linear-gradient(242deg, red, green, orange)
            ]]>
        </x:String>
    </magic:CssGradientSource>
</magic:GradientView>
```

Or shorter using implicit conversion:

``` xml
<magic:GradientView GradientSource="linear-gradient(242deg, red, green, orange)" />
```

Styling from CSS stylesheet is also possible:

``` xml
<magic:GradientView StyleClass="myGradient" />
```

``` css
.myGradient {
    background: linear-gradient(90deg, rgb(235, 216, 9), rgb(202, 60, 134));
}
```

CSS can be also set via C#:

``` C#
gradientView.GradientSource = CssGradientSource.Parse("linear-gradient(red, green, blue)");
```