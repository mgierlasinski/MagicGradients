# How To Use Gradients From Web

In web we have a lot of different and cool gradients. We all time enhance Playground Gallery with new and awesome examples of gradients. But sometimes you want to copy, some, that not exist in Playground Gallery. In such a case, you may get some problems, because of the flexibility of CSS. We try to support all, but it's really hard to achieve and sometimes. Even if it's supported, some other modification may just work better 😉

## What Should I do if copied gradient is not working

First of all, if you get some bugs in the UI of gradient or Exception will be thrown - please, **Submit New Issue** 😲 with details, we as fast as can let you know how to fix it 😃

Also, you may check at this document useful modifications of gradient CSS, it should cover most common cases 🤯

## Linear Repeating Gradients

Most Linear Repeating Gradients use `px` as gradient stops and it's not correctly translated in our Liner Gradient(when `IsRepeating="True"`). We support it correctly in Radial Gradients for with Linear Gradients(when `IsRepeating="False"`), but it's really hard to correctly determinate repeating in `px` at the moment, you should use `%`.

Here is an example of converting one of our gradients from `px` to `%`:

<img src="..\Assets\RepeatedAngularGradient.png" height="280">

It's created from four `repeating-linear-gradient` and one `linear-gradient`. All repeating gradients created with `px` as gradient stops.

We will focus on the **first** one because correction procedure is the same for all `repeating-linear-gradients`

<img src="..\Assets\PartOfRepeatedAngularGradient.png" height="280">

```css
repeating-linear-gradient(0deg, 
    rgba(0, 0, 0, 0.11) 0px, 
    rgba(0, 0, 0, 0.11) 12px, 
    rgba(1, 1, 1, 0.16) 12px, 
    rgba(1, 1, 1, 0.16) 24px, 
    rgba(0, 0, 0, 0.14) 24px, 
    rgba(0, 0, 0, 0.14) 36px, 
    rgba(0, 0, 0, 0.23) 36px, 
    rgba(0, 0, 0, 0.23) 48px, 
    rgba(0, 0, 0, 0.12) 48px, 
    rgba(0, 0, 0, 0.12) 60px, 
    rgba(1, 1, 1, 0.07) 60px, 
    rgba(1, 1, 1, 0.07) 72px, 
    rgba(0, 0, 0, 0.21) 72px, 
    rgba(0, 0, 0, 0.21) 84px, 
    rgba(0, 0, 0, 0.24) 84px, 
    rgba(0, 0, 0, 0.24) 96px, 
    rgba(1, 1, 1, 0.23) 96px, 
    rgba(1, 1, 1, 0.23) 108px, 
    rgba(1, 1, 1, 0.07) 108px, 
    rgba(1, 1, 1, 0.07) 120px, 
    rgba(0, 0, 0, 0.01) 120px, 
    rgba(0, 0, 0, 0.01) 132px, 
    rgba(1, 1, 1, 0.22) 132px, 
    rgba(1, 1, 1, 0.22) 144px, 
    rgba(1, 1, 1, 0.24) 144px, 
    rgba(1, 1, 1, 0.24) 156px, 
    rgba(0, 0, 0, 0) 156px, 
    rgba(0, 0, 0, 0) 168px, 
    rgba(0, 0, 0, 0.12) 168px, 
    rgba(0, 0, 0, 0.12) 180px
)
```

As we can see it use `px` from `0` up to `180`. Not Repeating Linear Gradient use `100%` of color positions(stops) in **Skia Sharp**. If our linear gradient has flag repeated and still have points up to `100%` it will be again Not Repeating Linear Gradient but with strange and ugly color stop positions 😮 😥 

We need to have it less than `100%`, so I would suggest you to change it from `px` to `%` by yourself 😁

Changed formula is easy: 
1. Take `px` value `d*px` where `d*` is any digits
2. Replace last digit and `px` with `%` 
3. And it's all 👻

P.S. if you need more precision you may not remove last digit but add `.` before it.

Examples:
* 180px = 18%
* 108px = 10% || 10.8%
* 12px = 1% || 1.2%

```css
repeating-linear-gradient(0deg, 
    rgba(0, 0, 0, 0.11) 0%, 
    rgba(0, 0, 0, 0.11) 1%, 
    rgba(1, 1, 1, 0.16) 1%, 
    rgba(1, 1, 1, 0.16) 2%, 
    rgba(0, 0, 0, 0.14) 2%, 
    rgba(0, 0, 0, 0.14) 3%, 
    rgba(0, 0, 0, 0.23) 3%, 
    rgba(0, 0, 0, 0.23) 4%, 
    rgba(0, 0, 0, 0.12) 4%, 
    rgba(0, 0, 0, 0.12) 6%, 
    rgba(1, 1, 1, 0.07) 6%, 
    rgba(1, 1, 1, 0.07) 7%, 
    rgba(0, 0, 0, 0.21) 7%, 
    rgba(0, 0, 0, 0.21) 8%, 
    rgba(0, 0, 0, 0.24) 8%, 
    rgba(0, 0, 0, 0.24) 9%, 
    rgba(1, 1, 1, 0.23) 9%, 
    rgba(1, 1, 1, 0.23) 10%, 
    rgba(1, 1, 1, 0.07) 10%, 
    rgba(1, 1, 1, 0.07) 12%, 
    rgba(0, 0, 0, 0.01) 12%, 
    rgba(0, 0, 0, 0.01) 13%, 
    rgba(1, 1, 1, 0.22) 13%, 
    rgba(1, 1, 1, 0.22) 14%, 
    rgba(1, 1, 1, 0.24) 14%, 
    rgba(1, 1, 1, 0.24) 15%, 
    rgba(0, 0, 0, 0) 15%, 
    rgba(0, 0, 0, 0) 16%, 
    rgba(0, 0, 0, 0.12) 16%, 
    rgba(0, 0, 0, 0.12) 18%
)
```

## HSL and HSLA colors with double values

In a moment Xamarin.Forms can't convert HSL and HSLA color with double values. [Fix to that](https://github.com/xamarin/Xamarin.Forms/pull/8114) already merged to Xamarin.Forms `master` branch but NuGet package is not updated in a moment.

This part will be updated right after that fix will be applied to **Magic Gradients**, but right now if you don't want to catch runtime parse exception from Xamarin.Forms please search in your gradients with next Regex "`\w*\(\d+\.\d+`". At all places where you find that, please just remove decimal places and that should work.



