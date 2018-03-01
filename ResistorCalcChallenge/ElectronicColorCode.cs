using System;
using System.Windows.Media;

namespace ResistorCalcChallenge
{
    public class ElectronicColorCode
    {
        public ElectronicColorCode(ElectronicColorCodeEnum enumVal)
        {
            ElectronicColorCodeEnum = enumVal;

            Key = (int)enumVal;
            ColorName = enumVal.ToString();
            Color = ColorCodeDataSource.GetColor(ColorName);
        }

        public ElectronicColorCode(ElectronicColorCodeEnum enumVal, SolidColorBrush color)
        {
            ElectronicColorCodeEnum = enumVal;

            Key = (int)enumVal;
            ColorName = enumVal.ToString();
            Color = color ?? throw new ArgumentNullException(nameof(color));
        }

        public ElectronicColorCodeEnum ElectronicColorCodeEnum {get;}
        public int Key { get; }
        public string ColorName { get; }
        public SolidColorBrush Color { get; }

        public string Multiplier => Math.Pow(10, Key).ToString();

        public string Tolerance => (OhmValueCalculator.GetTolerance(ElectronicColorCodeEnum) * 100).ToString();

        public override string ToString()
        {
            return ColorName;
        }
    }
}
