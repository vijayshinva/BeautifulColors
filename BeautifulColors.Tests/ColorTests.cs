using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BeautifulColors.Tests
{
    [TestClass]
    public class ColorTests
    {
        [TestMethod]
        public void Color_From_HSL_80ffff()
        {
            var color_80ffff = Color.FromHSL(180, 1, .75);
            Assert.AreEqual(128, color_80ffff.R);
            Assert.AreEqual(255, color_80ffff.G);
            Assert.AreEqual(255, color_80ffff.B);
        }

        [TestMethod]
        public void Color_From_HSL_bfbf00()
        {
            var color_bfbf00 = Color.FromHSL(60, 1, .375);
            Assert.AreEqual(191, color_bfbf00.R);
            Assert.AreEqual(191, color_bfbf00.G);
            Assert.AreEqual(0, color_bfbf00.B);
        }

        [TestMethod]
        public void Color_From_HSV_80ffff()
        {
            var color_80ffff = Color.FromHSV(180, .5, 1);
            Assert.AreEqual(128, color_80ffff.R);
            Assert.AreEqual(255, color_80ffff.G);
            Assert.AreEqual(255, color_80ffff.B);
        }

        [TestMethod]
        public void Color_From_HSV_bfbf00()
        {
            var color_bfbf00 = Color.FromHSV(60, 1, .75);
            Assert.AreEqual(191, color_bfbf00.R);
            Assert.AreEqual(191, color_bfbf00.G);
            Assert.AreEqual(0, color_bfbf00.B);
        }

        [TestMethod]
        public void HSL_From_RGB_186276()
        {
            var color_186276 = Color.FromRgb(24, 98, 118);

            var hsl = color_186276.HSL;

            Assert.AreEqual(193, Math.Round(hsl.Item1));
            Assert.AreEqual(.662, Math.Round(hsl.Item2, 3));
            Assert.AreEqual(.278, Math.Round(hsl.Item3, 3));
        }

        [TestMethod]
        public void HSV_From_RGB_186276()
        {
            var color_186276 = Color.FromRgb(24, 98, 118);

            var hsv = color_186276.HSV;

            Assert.AreEqual(193, Math.Round(hsv.Item1));
            Assert.AreEqual(.797, Math.Round(hsv.Item2, 3));
            Assert.AreEqual(.463, Math.Round(hsv.Item3, 3));
        }
    }
}

