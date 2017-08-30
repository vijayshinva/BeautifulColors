# BeautifulColors

A .NET Core Library for working with colors

[![Build status](https://ci.appveyor.com/api/projects/status/8jk4l4b38m8bdasx/branch/master?svg=true)](https://ci.appveyor.com/project/vijayshinva/beautifulcolors/branch/master)
[![CodeFactor](https://www.codefactor.io/repository/github/vijayshinva/beautifulcolors/badge/master)](https://www.codefactor.io/repository/github/vijayshinva/beautifulcolors/overview/master)
[![NuGet version](https://badge.fury.io/nu/BeautifulColors.svg)](https://badge.fury.io/nu/BeautifulColors)

## Overview
- [Installation](#installation)
- [Usage](#usage)
- [Examples](#examples)
- [License](#license)

## Installation

Install the package **BeautifulColors** from [NuGet](https://www.nuget.org/packages/BeautifulColors/) 
or install it from the [Package Manager Console](https://docs.microsoft.com/en-us/nuget/tools/package-manager-console):

```
PM> Install-Package BeautifulColors
```

## Usage

```C#
using BeautifulColors;
...
...
// Colors can be instantiated in a number of ways
var color_80ffff = Color.FromHSL(180, 1, .75);
var color_bfbf00 = Color.FromHSV(60, 1, .75);
var color_186276 = Color.FromRgb(24, 98, 118);
var color_fff700 = Color.FromNamedColor(NamedColors.YellowSunshine);

// The ToString() function takes a format string
Console.WriteLine(color_fff700); // FF-F7-00
Console.WriteLine(color_fff700.ToString("HSV", null)); // 58.1176470588235,1,1
Console.WriteLine(color_fff700.ToString("HSL", null)); // 58.1176470588235,1,0.5
Console.WriteLine(color_fff700.ToString("RGB", null)); // 255,247,0
Console.WriteLine(color_fff700.ToString("RGBA", null)); // 255,247,0,255
Console.WriteLine(color_fff700.ToString("HEX", null)); // #FFF700
```
```C#
// ColorFactory can be used to generate random colors and pallets
var colorFactory = new ColorFactory();
...
...
var randomColors = colorFactory.Random(15); 
// RandomBeautiful generates bright colors (Saturation > .5 and Value > .7)
var beautifulRandomColors = colorFactory.RandomBeautiful(5);
...
...
// You can also pass a Hue to generate colors within a 10 degree range
var randomColors = colorFactory.Random(count:10, hue:NamedColors.Azure); 
var beautifulRandomColors = colorFactory.RandomBeautiful(count:10, hue:NamedColors.YellowSunshine);
var randomNamedColors = colorFactory.RandomNamed(1);
```

## Examples
Demos in the Examples folder.

#### BeautifulWeb
This simple demo shows 256 randomly generated colors.

![Beautiful Web](https://github.com/vijayshinva/BeautifulColors/tree/master/Examples/BeautifulWeb/BeautifulWeb.png)

## Reference
- https://en.wikipedia.org/wiki/HSL_and_HSV
- https://en.wikipedia.org/wiki/Lists_of_colors

## License
[![license](https://img.shields.io/github/license/vijayshinva/beautifulcolors.svg)](https://github.com/vijayshinva/BeautifulColors/blob/master/LICENSE)
[![FOSSA Status](https://app.fossa.io/api/projects/git%2Bhttps%3A%2F%2Fgithub.com%2Fvijayshinva%2FBeautifulColors.svg?type=shield)](https://app.fossa.io/projects/git%2Bhttps%3A%2F%2Fgithub.com%2Fvijayshinva%2FBeautifulColors?ref=badge_shield)


[![FOSSA Status](https://app.fossa.io/api/projects/git%2Bhttps%3A%2F%2Fgithub.com%2Fvijayshinva%2FBeautifulColors.svg?type=large)](https://app.fossa.io/projects/git%2Bhttps%3A%2F%2Fgithub.com%2Fvijayshinva%2FBeautifulColors?ref=badge_large)
