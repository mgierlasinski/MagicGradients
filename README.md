<img width="80" height="80" src="https://raw.githubusercontent.com/mgierlasinski/MagicGradients/master/Assets/icon.png" />

# Magic Gradients

Draw breathtaking backgrounds in your Xamarin.Forms application. You can add unlimited number of complex linear and radial gradients to create uniqe effects. It's a kind of magic. Control inspired by [PancakeView](https://github.com/sthewissen/Xamarin.Forms.PancakeView) and [Gradient Magic](https://www.gradientmagic.com/). Powered by [SkiaSharp](https://github.com/mono/SkiaSharp).

## Packages

| Package | Version | Dependencies |
|---|---|---|
| MagicGradients | [![Nuget](https://img.shields.io/nuget/vpre/MagicGradients)](https://www.nuget.org/packages/MagicGradients) | [![Nuget](https://img.shields.io/badge/Xamarin.Forms-v4.4-green)](https://www.nuget.org/packages/Xamarin.Forms/) [![Nuget](https://img.shields.io/badge/SkiaSharp-v1.68.1-blue)](https://www.nuget.org/packages/SkiaSharp/) [![Nuget](https://img.shields.io/badge/SkiaSharp.Views.Forms-v1.68.1-blue)](https://www.nuget.org/packages/SkiaSharp.Views.Forms/)

## Gallery

You can preview some of the gradients from [Gradient Magic](https://www.gradientmagic.com/) in Playground app. To use the examples from the gallery, you need just copy Gradient CSS and paste into your project.

(Video on YouTube ⬇️)

[![Gallery](https://img.youtube.com/vi/PFSlubz6_ps/0.jpg)](https://www.youtube.com/watch?v=PFSlubz6_ps)


![GIF](./Assets/MagicGradientsGallery.gif)

## Getting Started

Install via [NuGet](https://www.nuget.org/packages/MagicGradients) or include the Magic Gradients project in your solution and add local references in your shared project.

As it requires SkiaSharp, you will also need to ensure you add SkiaSharp.Views.Forms to your shared Xamarin.Forms project. 

## Setting gradient source

You can build gradients manually in Xaml. To draw single gradient just create `LinearGradient` or `RadialGradient` as the child of `GradientView` control.

``` xml
<magic:GradientView>
    <magic:LinearGradient Angle="45">
        <magic:GradientStop Color="Red" />
        <magic:GradientStop Color="Yellow" />
    </magic:LinearGradient>
</magic:GradientView>
```

There is also possibility to add collection of gradients. You can mix linear and radial gradients, use transparency in color definitions to get blend effect. 

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
    .AddLinearGradient(45)
        .AddStop(Color.Green, 0.2f)
        .AddStop(Color.Aqua, 0.9f)
    .AddLinearGradient(90)
        .AddStop(Color.Blue)
        .AddStop(Color.DeepPink)
    .Build();
```

By using `GradientBuilder`, some of the construction process is automated, there is also validation performed that looks after undefined stop offsets and set them automatically. `GradientBuilder` is used under the hood when CSS code is parsed.

## Styling with CSS

Magic Gradients supports parsing CSS functions `linear-gradient`, `repeating-linear-gradient`, `radial-gradient`, `repeating-radial-gradient`, you can find full specification in several sources:

- https://www.w3schools.com/css/css3_gradients.asp
- https://developer.mozilla.org/en-US/docs/Web/CSS/linear-gradient
- https://developer.mozilla.org/en-US/docs/Web/CSS/repeating-linear-gradient
- https://developer.mozilla.org/en-US/docs/Web/CSS/radial-gradient
- https://developer.mozilla.org/en-US/docs/Web/CSS/repeating-radial-gradient

You can embed inline CSS directly in xaml:

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

Styling with CSS styleshet is also supported:

``` xml
<magic:GradientView StyleClass="gradientStyledWithCss" />
```

``` css
.gradientStyledWithCss {
    background: linear-gradient(90deg, rgb(235, 216, 9),rgb(202, 60, 134));
}
```

### CSS parser

You can test CSS gradient code live within Playground application.

![Paste CSS](https://raw.githubusercontent.com/mgierlasinski/MagicGradients/master/Assets/paste-css.gif)

### Linear gradient function syntax

Single linear gradient
``` css
linear-gradient(direction, color-stop1, color-stop2, ...);
```

Repeating linear gradient
``` css
releating-linear-gradient(direction, color-stop1, color-stop2, ...);
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

### Radial gradient function syntax

Single radial gradient
``` css
radial-gradient(shape size at position, start-color, ..., last-color);
```

Repeating radial gradient
``` css
repeating-radial-gradient(shape size at position, start-color, ..., last-color);
```

- supported shapes: `circle`, `ellipse`
- suppored sizes: `closest-side`, `closest-corner`\*, `farthest-side`, `farthest-corner`\* 
- supported sizes: in pixels (`px`), propoertional (`%`) and named directions (`left`, `right`, `top`, `bottom`, `center`)
- suppored color formats: (see linear gradient)

\* _currently ellipse shape supports only side points, you can use corner variants but there is no difference in rendering_

### Examples

Linear gradient 
``` css
/* A gradient tilted 45 degrees,
   starting blue and finishing red */
linear-gradient(45deg, blue, red);

/* A gradient going from the bottom right to the top left corner,
   starting blue and finishing red */
linear-gradient(to left top, blue, red);

/* Color stop: A gradient going from the bottom to top,
   starting blue, turning green at 40% of its length,
   and finishing red */
linear-gradient(0deg, blue, green 40%, red);
```

Radial gradient
``` css
/* Simple gradient */
radial-gradient(cyan 0%, transparent 20%, salmon 40%);

/* Non-centered gradient */
radial-gradient(farthest-corner at 40px 40px, #f35 0%, #43e 100%);
```

# Tutorials
- [Gradient Background for your Xamarin.Forms App - blog post](https://medium.com/@benetskyybogdan/gradient-background-for-your-xamarin-forms-app-6d7e46fba558)
- [XF Shell Gradient Flyout with Magic Gradients - blog post](https://medium.com/@benetskyybogdan/xf-shell-gradient-flyout-with-magic-gradients-e9f0eb46bae0)
- [How we extended Xamarin.Forms CSS to style GradientView - blog post](https://medium.com/@benetskyybogdan/xamarin-forms-custom-css-properties-d75872bea20e)
- [Xamarin.Forms Gradient Background For All Pages in 1 minute - blog post](https://medium.com/@benetskyybogdan/xamarin-forms-gradient-background-for-all-pages-in-1-minute-9e172d986618)

---
<div>Icons made by <a href="https://www.flaticon.com/authors/icongeek26" title="Icongeek26">Icongeek26</a> from <a href="https://www.flaticon.com/" title="Flaticon">www.flaticon.com</a></div>
