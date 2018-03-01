using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace ResistorCalcChallenge
{
    public class ColorCodeDataSource
    {
        public static IList<ElectronicColorCode> ColorCodes = new List<ElectronicColorCode>();

        static ColorCodeDataSource()
        {
            IEnumerable<ElectronicColorCodeEnum> eccValues = (ElectronicColorCodeEnum[])Enum.GetValues(typeof(ElectronicColorCodeEnum));

            foreach(ElectronicColorCodeEnum eccVal in eccValues)
            {
                ColorCodes.Add(new ElectronicColorCode(eccVal, GetColor(eccVal.ToString())));
            }
        }

        public static IList<ElectronicColorCode> GetForDigits()
        {
            return GetLargerThan((int)ElectronicColorCodeEnum.Gold);
        }

        public static IList<ElectronicColorCode> GetForMultiplier()
        {
            return GetLargerThan((int)ElectronicColorCodeEnum.None);
        }

        public static IList<ElectronicColorCode> GetForTolerance()
        {
            return GetAllExcept(new int[] { -3, 0, 3, 9 });
        }

        public static IList<ElectronicColorCode> GetAll()
        {
            IList<ElectronicColorCode> result = ColorCodes.OrderBy(x => x.Key).ToList();
            return result;
        }

        public static IList<ElectronicColorCode> GetAllExcept(int[] skipVals)
        {
            IList<ElectronicColorCode> result = ColorCodes.Where(x => !skipVals.Contains(x.Key)).OrderBy(y => y.Key).ToList();
            return result;
        }

        public static IList<ElectronicColorCode> GetLargerThan(int threshold)
        {
            IList<ElectronicColorCode> result = ColorCodes.Where(x => x.Key > threshold).OrderBy(y => y.Key).ToList();
            return result;
        }


        public static SolidColorBrush GetColor(string colorName)
        {
            System.Drawing.Color drawingColor;

            if (colorName.ToLower() == "none")
            {
                drawingColor = System.Drawing.Color.FromName("White");
            }
            else
            {
                drawingColor = System.Drawing.Color.FromName(colorName);
            }

            Color mColor = Color.FromRgb(drawingColor.R, drawingColor.G, drawingColor.B);

            return new SolidColorBrush(mColor);
        }
    }
}
