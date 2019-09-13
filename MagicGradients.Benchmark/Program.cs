using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using MagicGradients.Parser;
using Xamarin.Forms.Internals;

namespace MagicGradients.Benchmark
{
    public class LinearGradientBenchmark
    {
        private readonly CssLinearGradientParser _parser = new CssLinearGradientParser();

        private readonly List<string> _simpleCss = new List<string>(new[]
        {
            "linear-gradient(45deg, hsl(124,79%,62%) 0%,hsl(169,79%,62%) 12%,hsl(214,79%,62%) 39%,hsl(259,79%,62%) 49%,hsl(304,79%,62%) 73%,hsl(349,79%,62%) 81%,hsl(34,79%,62%) 90%,hsl(79,79%,62%) 100%)",
            "linear-gradient(90deg, hsl(259,61%,55%),hsl(331,61%,55%),hsl(43,61%,55%),hsl(115,61%,55%),hsl(187,61%,55%))",
            "linear-gradient(135deg, rgb(32, 186, 230),rgb(110, 6, 173))",
            "linear-gradient(45deg, rgb(102, 6, 88) 0%,rgb(196, 240, 233) 35%,rgb(158, 46, 222) 100%)",
            "linear-gradient(90deg, rgb(93, 44, 178),rgb(236, 233, 105))"

        });
        private readonly List<string> _complexCss = new List<string>(new[]
        {
            "linear-gradient(56deg, rgba(254, 254, 254, 0.05) 0%, rgba(254, 254, 254, 0.05) 69%,rgba(160, 160, 160, 0.05) 69%, rgba(160, 160, 160, 0.05) 100%),linear-gradient(194deg, rgba(102, 102, 102, 0.02) 0%, rgba(102, 102, 102, 0.02) 60%,rgba(67, 67, 67, 0.02) 60%, rgba(67, 67, 67, 0.02) 100%),linear-gradient(76deg, rgba(169, 169, 169, 0.06) 0%, rgba(169, 169, 169, 0.06) 89%,rgba(189, 189, 189, 0.06) 89%, rgba(189, 189, 189, 0.06) 100%),linear-gradient(326deg, rgba(213, 213, 213, 0.04) 0%, rgba(213, 213, 213, 0.04) 45%,rgba(66, 66, 66, 0.04) 45%, rgba(66, 66, 66, 0.04) 100%),linear-gradient(183deg, rgba(223, 223, 223, 0.01) 0%, rgba(223, 223, 223, 0.01) 82%,rgba(28, 28, 28, 0.01) 82%, rgba(28, 28, 28, 0.01) 100%),linear-gradient(3deg, rgba(20, 20, 20, 0.06) 0%, rgba(20, 20, 20, 0.06) 62%,rgba(136, 136, 136, 0.06) 62%, rgba(136, 136, 136, 0.06) 100%),linear-gradient(200deg, rgba(206, 206, 206, 0.09) 0%, rgba(206, 206, 206, 0.09) 58%,rgba(6, 6, 6, 0.09) 58%, rgba(6, 6, 6, 0.09) 100%),linear-gradient(304deg, rgba(162, 162, 162, 0.07) 0%, rgba(162, 162, 162, 0.07) 27%,rgba(24, 24, 24, 0.07) 27%, rgba(24, 24, 24, 0.07) 100%),linear-gradient(186deg, rgba(166, 166, 166, 0.04) 0%, rgba(166, 166, 166, 0.04) 5%,rgba(210, 210, 210, 0.04) 5%, rgba(210, 210, 210, 0.04) 100%),linear-gradient(90deg, rgb(26, 118, 64),rgb(32, 207, 121),rgb(78, 196, 128))",
            "linear-gradient(126deg, rgba(101, 101, 101, 0.09) 0%, rgba(101, 101, 101, 0.09) 68%,rgba(200, 200, 200, 0.09) 68%, rgba(200, 200, 200, 0.09) 100%),linear-gradient(164deg, rgba(238, 238, 238, 0.03) 0%, rgba(238, 238, 238, 0.03) 90%,rgba(14, 14, 14, 0.03) 90%, rgba(14, 14, 14, 0.03) 100%),linear-gradient(27deg, rgba(214, 214, 214, 0.04) 0%, rgba(214, 214, 214, 0.04) 34%,rgba(104, 104, 104, 0.04) 34%, rgba(104, 104, 104, 0.04) 100%),linear-gradient(175deg, rgba(20, 20, 20, 0.01) 0%, rgba(20, 20, 20, 0.01) 4%,rgba(9, 9, 9, 0.01) 4%, rgba(9, 9, 9, 0.01) 100%),linear-gradient(257deg, rgba(14, 14, 14, 0.01) 0%, rgba(14, 14, 14, 0.01) 28%,rgba(164, 164, 164, 0.01) 28%, rgba(164, 164, 164, 0.01) 100%),linear-gradient(311deg, rgba(68, 68, 68, 0.07) 0%, rgba(68, 68, 68, 0.07) 33%,rgba(213, 213, 213, 0.07) 33%, rgba(213, 213, 213, 0.07) 100%),linear-gradient(244deg, rgba(43, 43, 43, 0.02) 0%, rgba(43, 43, 43, 0.02) 80%,rgba(161, 161, 161, 0.02) 80%, rgba(161, 161, 161, 0.02) 100%),linear-gradient(130deg, rgba(255, 255, 255, 0.04) 0%, rgba(255, 255, 255, 0.04) 49%,rgba(105, 105, 105, 0.04) 49%, rgba(105, 105, 105, 0.04) 100%),linear-gradient(90deg, rgb(190, 33, 111),rgb(97, 3, 0))",
            "linear-gradient(45deg, rgba(188, 164, 6, 0.5) 0%, rgba(188, 164, 6, 0.5) 12.5%,rgba(165, 158, 18, 0.5) 12.5%, rgba(165, 158, 18, 0.5) 25%,rgba(142, 152, 31, 0.5) 25%, rgba(142, 152, 31, 0.5) 37.5%,rgba(119, 146, 43, 0.5) 37.5%, rgba(119, 146, 43, 0.5) 50%,rgba(96, 140, 56, 0.5) 50%, rgba(96, 140, 56, 0.5) 62.5%,rgba(73, 134, 68, 0.5) 62.5%, rgba(73, 134, 68, 0.5) 75%,rgba(50, 128, 81, 0.5) 75%, rgba(50, 128, 81, 0.5) 87.5%,rgba(27, 122, 93, 0.5) 87.5%, rgba(27, 122, 93, 0.5) 100%),linear-gradient(135deg, rgb(197, 183, 45) 0%, rgb(197, 183, 45) 12.5%,rgb(177, 167, 44) 12.5%, rgb(177, 167, 44) 25%,rgb(158, 152, 44) 25%, rgb(158, 152, 44) 37.5%,rgb(138, 136, 43) 37.5%, rgb(138, 136, 43) 50%,rgb(118, 121, 42) 50%, rgb(118, 121, 42) 62.5%,rgb(98, 105, 41) 62.5%, rgb(98, 105, 41) 75%,rgb(79, 90, 41) 75%, rgb(79, 90, 41) 87.5%,rgb(59, 74, 40) 87.5%, rgb(59, 74, 40) 100%)",
            "linear-gradient(230deg, rgba(13, 13, 13, 0.02) 0%, rgba(13, 13, 13, 0.02) 50%,rgba(255, 255, 255, 0.02) 50%, rgba(255, 255, 255, 0.02) 100%),linear-gradient(44deg, rgba(191, 191, 191, 0.03) 0%, rgba(191, 191, 191, 0.03) 50%,rgba(20, 20, 20, 0.03) 50%, rgba(20, 20, 20, 0.03) 100%),linear-gradient(197deg, rgba(229, 229, 229, 0.03) 0%, rgba(229, 229, 229, 0.03) 50%,rgba(39, 39, 39, 0.03) 50%, rgba(39, 39, 39, 0.03) 100%),linear-gradient(352deg, rgba(160, 160, 160, 0.01) 0%, rgba(160, 160, 160, 0.01) 50%,rgba(98, 98, 98, 0.01) 50%, rgba(98, 98, 98, 0.01) 100%),linear-gradient(75deg, rgba(36, 36, 36, 0.03) 0%, rgba(36, 36, 36, 0.03) 50%,rgba(238, 238, 238, 0.03) 50%, rgba(238, 238, 238, 0.03) 100%),linear-gradient(188deg, rgba(59, 59, 59, 0.03) 0%, rgba(59, 59, 59, 0.03) 50%,rgba(163, 163, 163, 0.03) 50%, rgba(163, 163, 163, 0.03) 100%),linear-gradient(208deg, rgba(33, 33, 33, 0.03) 0%, rgba(33, 33, 33, 0.03) 50%,rgba(160, 160, 160, 0.03) 50%, rgba(160, 160, 160, 0.03) 100%),linear-gradient(331deg, rgba(92, 92, 92, 0.02) 0%, rgba(92, 92, 92, 0.02) 50%,rgba(6, 6, 6, 0.02) 50%, rgba(6, 6, 6, 0.02) 100%),linear-gradient(290deg, rgba(16, 16, 16, 0.02) 0%, rgba(16, 16, 16, 0.02) 50%,rgba(163, 163, 163, 0.02) 50%, rgba(163, 163, 163, 0.02) 100%),linear-gradient(90deg, rgb(76, 21, 98),rgb(166, 10, 148))",
            "linear-gradient(292deg, rgba(150, 150, 150, 0.03) 0%, rgba(150, 150, 150, 0.03) 20%,rgba(151, 151, 151, 0.03) 20%, rgba(151, 151, 151, 0.03) 40%,rgba(215, 215, 215, 0.03) 40%, rgba(215, 215, 215, 0.03) 60%,rgba(254, 254, 254, 0.03) 60%, rgba(254, 254, 254, 0.03) 80%,rgba(112, 112, 112, 0.03) 80%, rgba(112, 112, 112, 0.03) 100%),linear-gradient(62deg, rgba(34, 34, 34, 0.03) 0%, rgba(34, 34, 34, 0.03) 20%,rgba(171, 171, 171, 0.03) 20%, rgba(171, 171, 171, 0.03) 40%,rgba(206, 206, 206, 0.03) 40%, rgba(206, 206, 206, 0.03) 60%,rgba(210, 210, 210, 0.03) 60%, rgba(210, 210, 210, 0.03) 80%,rgba(69, 69, 69, 0.03) 80%, rgba(69, 69, 69, 0.03) 100%),linear-gradient(314deg, rgba(235, 235, 235, 0.03) 0%, rgba(235, 235, 235, 0.03) 20%,rgba(254, 254, 254, 0.03) 20%, rgba(254, 254, 254, 0.03) 40%,rgba(178, 178, 178, 0.03) 40%, rgba(178, 178, 178, 0.03) 60%,rgba(211, 211, 211, 0.03) 60%, rgba(211, 211, 211, 0.03) 80%,rgba(73, 73, 73, 0.03) 80%, rgba(73, 73, 73, 0.03) 100%),linear-gradient(32deg, rgba(182, 182, 182, 0.01) 0%, rgba(182, 182, 182, 0.01) 12.5%,rgba(208, 208, 208, 0.01) 12.5%, rgba(208, 208, 208, 0.01) 25%,rgba(178, 178, 178, 0.01) 25%, rgba(178, 178, 178, 0.01) 37.5%,rgba(143, 143, 143, 0.01) 37.5%, rgba(143, 143, 143, 0.01) 50%,rgba(211, 211, 211, 0.01) 50%, rgba(211, 211, 211, 0.01) 62.5%,rgba(92, 92, 92, 0.01) 62.5%, rgba(92, 92, 92, 0.01) 75%,rgba(56, 56, 56, 0.01) 75%, rgba(56, 56, 56, 0.01) 87.5%,rgba(253, 253, 253, 0.01) 87.5%, rgba(253, 253, 253, 0.01) 100%),linear-gradient(247deg, rgba(103, 103, 103, 0.02) 0%, rgba(103, 103, 103, 0.02) 12.5%,rgba(240, 240, 240, 0.02) 12.5%, rgba(240, 240, 240, 0.02) 25%,rgba(18, 18, 18, 0.02) 25%, rgba(18, 18, 18, 0.02) 37.5%,rgba(38, 38, 38, 0.02) 37.5%, rgba(38, 38, 38, 0.02) 50%,rgba(246, 246, 246, 0.02) 50%, rgba(246, 246, 246, 0.02) 62.5%,rgba(9, 9, 9, 0.02) 62.5%, rgba(9, 9, 9, 0.02) 75%,rgba(167, 167, 167, 0.02) 75%, rgba(167, 167, 167, 0.02) 87.5%,rgba(86, 86, 86, 0.02) 87.5%, rgba(86, 86, 86, 0.02) 100%),linear-gradient(90deg, hsl(194,0%,10%),hsl(194,0%,10%))"

    });

        [Benchmark]
        public void SimpleCss() => _simpleCss.ForEach(s => _parser.ParseCss(s));

        [Benchmark]
        public void ComplexCss() => _complexCss.ForEach(s => _parser.ParseCss(s));

    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<LinearGradientBenchmark>();
        }
    }
}
