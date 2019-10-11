# Magic Gradients

Xamarin.Forms control to display complex gradients, inspired by [PancakeView](https://github.com/sthewissen/Xamarin.Forms.PancakeView) and [Magic Gradients](https://www.gradientmagic.com/). You can add unlimited amount of linear gradients with different angles to create uniqe effects. Powered by [SkiaSharp](https://github.com/mono/SkiaSharp).

## Packages

Initial release is available via NuGet as a prerelease version (make sure you have checked `Include prerelase` in NuGet browser).

| Package | Version |
|---|---|
| MagicGradients |[![Nuget](https://img.shields.io/nuget/vpre/MagicGradients)](https://www.nuget.org/packages/MagicGradients)|

## Depencendies

Xamarin.Forms | SkiaSharp
---|---|
![NuGet](https://img.shields.io/badge/Xamarin.Forms-v4.2.848062-green) | ![NuGet](https://img.shields.io/badge/SkiaSharp-v1.68.0-blue)

## Gallery

You can preview some of the gradients from [Gradient Magic](https://www.gradientmagic.com/) in Playground app. Css code is directly copied, without any modifications.

| | | |
|-|-|-|
|![Screenshot](https://raw.githubusercontent.com/mgierlasinski/MagicGradients/master/Assets/Gallery/Gallery-1.png)|![Screenshot](https://raw.githubusercontent.com/mgierlasinski/MagicGradients/master/Assets/Gallery/Gallery-2.png)|![Screenshot](https://raw.githubusercontent.com/mgierlasinski/MagicGradients/master/Assets/Gallery/Gallery-3.png)|
|![Screenshot](https://raw.githubusercontent.com/mgierlasinski/MagicGradients/master/Assets/Gallery/Gallery-4.png)|![Screenshot](https://raw.githubusercontent.com/mgierlasinski/MagicGradients/master/Assets/Gallery/Gallery-5.png)|![Screenshot](https://raw.githubusercontent.com/mgierlasinski/MagicGradients/master/Assets/Gallery/Gallery-6.png)|

## Getting Started

Install via NuGet (make sure you have checked `Include prerelase` in NuGet browser) or include the Magic Gradients project in your solution and add local references in your shared project.

As it requires SkiaSharp, you will also need to ensure you add SkiaSharp.Views.Forms to your shared Xamarin.Forms project. 

## Setting gradient source

You can build gradients manually in Xaml:

``` xml
<magic:GradientView VerticalOptions="FillAndExpand">
    <magic:GradientView.GradientSource>
        <magic:GradientSource>
            <magic:LinearGradient Angle="45">
                <magic:GradientStop Color="Red" Offset="0" />
                <magic:GradientStop Color="Yellow" Offset="1" />
            </magic:LinearGradient>
        </magic:GradientSource>
    </magic:GradientView.GradientSource>
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

Magic Gradients supports parsing CSS function `linear-gradient`, you can find full specification in several sources:

- https://www.w3schools.com/css/css3_gradients.asp
- https://developer.mozilla.org/en-US/docs/Web/CSS/linear-gradient

You can embed inline CSS directly in xaml:

``` xml
<magic:GradientView>
    <magic:GradientView.GradientSource>
        <magic:CssGradientSource>
            <x:String>
                <![CDATA[
                    linear-gradient(242deg, red, green, orange)
                ]]>
            </x:String>
        </magic:CssGradientSource>
    </magic:GradientView.GradientSource>
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

### linear-gradient function syntax

``` css
linear-gradient(direction, color-stop1, color-stop2, ...);
```

Supported directions:
- named directions: to left, to right, to top, to bottom, to left top, to right bottom etc.
- angles in degrees: 135deg

Each color stop should contain color information and optionally it's position along the gradient line, described in percents.

Suppored color formats:
- colors described by channels in RGB or HSL format: rgb(red, green, blue), rgba(red, green, blue, alpha), hsl(hue, saturation, lightness), hsla(hue, saturation, lightness, alpha)
- colors in hex: #ff0000
- named colors: red, green, blue, orange, pink

### Examples

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

---
<div>Icons made by <a href="https://www.flaticon.com/authors/pause08" title="Pause08">Pause08</a> from <a href="https://www.flaticon.com/" title="Flaticon">www.flaticon.com</a></div>