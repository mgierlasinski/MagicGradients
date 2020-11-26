<img width="80" height="80" src="https://raw.githubusercontent.com/mgierlasinski/MagicGradients/master/Assets/icon.png" />

# Magic Gradients

Draw breathtaking backgrounds in your Xamarin.Forms application. You can add unlimited number of complex linear and radial gradients to create uniqe effects. It's a kind of magic. Control inspired by [PancakeView](https://github.com/sthewissen/Xamarin.Forms.PancakeView) and [Gradient Magic](https://www.gradientmagic.com/). Powered by [SkiaSharp](https://github.com/mono/SkiaSharp).

Install from NuGet:

[![Nuget](https://img.shields.io/nuget/vpre/MagicGradients)](https://www.nuget.org/packages/MagicGradients)

| Supported platforms |
|---|
| :heavy_check_mark: Android |
| :heavy_check_mark: iOS |
| :heavy_check_mark: UWP |

## Gallery

You can preview some of the gradients from [Gradient Magic](https://www.gradientmagic.com/) in Playground app. To use the examples from the gallery, you need just copy Gradient CSS and paste into your project.

(Video on YouTube ⬇️)

[![Gallery](https://img.youtube.com/vi/PFSlubz6_ps/0.jpg)](https://www.youtube.com/watch?v=PFSlubz6_ps)


![GIF](./Assets/MagicGradientsGallery.gif)

## Getting Started

Install via [NuGet](https://www.nuget.org/packages/MagicGradients) or include the Magic Gradients project in your solution and add local references in your shared project.

As it requires SkiaSharp, you will also need to ensure you add SkiaSharp.Views.Forms to your shared Xamarin.Forms project. 

## Setting gradient source

You can build gradients manually in XAML. To draw single gradient just create `LinearGradient` or `RadialGradient` as direct content of `GradientView`.

``` xml
<magic:GradientView>
    <magic:LinearGradient Angle="45">
        <magic:GradientStop Color="Red" />
        <magic:GradientStop Color="Yellow" Offset="20px" />
        <magic:GradientStop Color="Green" Offset="0.5" />
    </magic:LinearGradient>
</magic:GradientView>
```

Offset types:
- `0.3`, `30%` - proportional, `Offset.Prop(0.3)` in code
- `200px` - absolute, `Offset.Abs(200)` in code
- leave blank - each undefined offset will be calculated like in CSS

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

Magic Gradients supports [CSS functions](https://www.w3schools.com/css/css3_gradients.asp): 

- [`linear-gradient`](https://developer.mozilla.org/en-US/docs/Web/CSS/linear-gradient) 
- [`repeating-linear-gradient`](https://developer.mozilla.org/en-US/docs/Web/CSS/repeating-linear-gradient) 
- [`radial-gradient`](https://developer.mozilla.org/en-US/docs/Web/CSS/radial-gradient)
- [`repeating-radial-gradient`](https://developer.mozilla.org/en-US/docs/Web/CSS/repeating-radial-gradient)

You can embed CSS inline:

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

Or style from CSS stylesheet:

``` xml
<magic:GradientView StyleClass="myGradient" />
```

``` css
.myGradient {
    background: linear-gradient(90deg, rgb(235, 216, 9),rgb(202, 60, 134));
}
```

### CSS previewer

You can test CSS gradient code live within Playground application.

![Paste CSS](https://raw.githubusercontent.com/mgierlasinski/MagicGradients/master/Assets/paste-css.gif)

### Linear gradient function

``` css
linear-gradient(direction, color-stop1, color-stop2, ...);
```

Supported directions:
- named directions: `to left`, `to right`, `to top`, `to bottom`, `to left top`, `to right bottom` etc.
- angles in degrees: `135deg`
- angle proportional: `0.45turn` (range should be between 0-1)

Each color stop should contain color information and optionally it's position along the gradient line, described in percents.

Suppored color formats:
- colors described by channels in RGB or HSL format: `rgb(red, green, blue)`, `rgba(red, green, blue, alpha)`, `hsl(hue, saturation, lightness)`, `hsla(hue, saturation, lightness, alpha)`
- colors in hex: `#ff0000`
- named colors: `red`, `green`, `blue`, `orange`, `pink`

### Radial gradient function

``` css
radial-gradient(shape size at position, start-color, ..., last-color);
```

- supported shapes: `circle`, `ellipse`
- suppored sizes: `closest-side`, `closest-corner`\*, `farthest-side`, `farthest-corner`\* 
- supported sizes: in pixels (`px`), propoertional (`%`) and named directions (`left`, `right`, `top`, `bottom`, `center`)
- suppored color formats: (see linear gradient)

\* _currently ellipse shape supports only side points, you can use corner variants but there is no difference in rendering_

### Examples

``` css
linear-gradient(45deg, blue, red);
linear-gradient(to left top, blue, red);
linear-gradient(0deg, blue, green 40%, red);
radial-gradient(cyan 0%, transparent 20%, salmon 40%);
radial-gradient(farthest-corner at 40px 40px, #f35 0%, #43e 100%);
```

## Advanced drawing

### Backgroud size and position

`GradientView` let's you specify size of the background with `GradientSize` property:

```xml
<GradientView GradientSource="..." GradientSize="0.6,0.6">
<GradientView GradientSource="..." GradientSize="200px,200px">
```

Proportional and absolute values are supported. Size can also be set from CSS:

``` css
.myGradient {
    background: ...;
    background-size: 60px 60px;
}
```

When size is set, gradient will be tiled to fill available space. You can change tile mode with `GradientRepeat` property. Supported values:
- `Repeat`, `repeat`
- `RepeatX`, `repeat-x`
- `RepeatY`, `repeat-y`
- `NoRepeat`, `no-repeat`

Repeat mode can be set from CSS as well:

``` css
.myGradient {
    background: ...;
    background-size: 60px 60px;
    background-repeat: repeat-xy;
}
```

# Articles
- [Gradient Background for your Xamarin.Forms App - blog post](https://medium.com/@benetskyybogdan/gradient-background-for-your-xamarin-forms-app-6d7e46fba558)
- [XF Shell Gradient Flyout with Magic Gradients - blog post](https://medium.com/@benetskyybogdan/xf-shell-gradient-flyout-with-magic-gradients-e9f0eb46bae0)
- [How we extended Xamarin.Forms CSS to style GradientView - blog post](https://medium.com/@benetskyybogdan/xamarin-forms-custom-css-properties-d75872bea20e)
- [Xamarin.Forms Gradient Background For All Pages in 1 minute - blog post](https://medium.com/@benetskyybogdan/xamarin-forms-gradient-background-for-all-pages-in-1-minute-9e172d986618)

---
<div>Icons made by <a href="https://www.flaticon.com/authors/icongeek26" title="Icongeek26">Icongeek26</a> from <a href="https://www.flaticon.com/" title="Flaticon">www.flaticon.com</a></div>
