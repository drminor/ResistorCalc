using System;

namespace ResistorCalcChallenge
{
    public class OhmValueCalculator : IOhmValueCalculator
    {
        public int CalculateOhmValue(string bandAColor, string bandBColor, string bandCColor, string bandDColor)
        {
            if(!TryGetECC(bandAColor, out ElectronicColorCodeEnum eccBandA))
            {
                throw new ArgumentException("bandAColor");
            }

            if (!TryGetECC(bandBColor, out ElectronicColorCodeEnum eccBandB))
            {
                throw new ArgumentException("bandBColor");
            }

            if (!TryGetECC(bandCColor, out ElectronicColorCodeEnum eccBandC))
            {
                throw new ArgumentException("bandCColor");
            }

            ElectronicColorCodeEnum eccBandD;

            if (bandDColor == null)
            {
                eccBandD = ElectronicColorCodeEnum.None;
            }
            else
            {
                if (!TryGetECC(bandDColor, out eccBandD))
                {
                    throw new ArgumentException("bandDColor");
                }
            }

            Tuple<double, double> valueAndTolerance = CalculateOhmValue(eccBandA, eccBandB, eccBandC, eccBandD);

            double value = valueAndTolerance.Item1;
            double tolerance = valueAndTolerance.Item2;

            if (value > int.MaxValue)
            {
                string format = "####,####";
                string fValue = value.ToString(format);
                throw new InvalidOperationException($"The value of this resistor ({fValue} is too large to be reported as an integer.");
            }

            if (value < 0.5)
            {
                string format = "####,####";
                string fValue = value.ToString(format);
                throw new InvalidOperationException($"The value of this resistor ({fValue} is too small to be reported as an integer.");
            }

            int result = (int)Math.Round(value, 0);
            return result;
        }


        public Tuple<double,double> CalculateOhmValue(ElectronicColorCodeEnum eccBandA, ElectronicColorCodeEnum eccBandB, ElectronicColorCodeEnum eccBandC, ElectronicColorCodeEnum eccBandD)
        {
            double value = 0;

            double val1 = GetSignficand(eccBandA);
            double val2 = GetSignficand(eccBandB);
            double multiplier = GetMultiplier(eccBandC);
            double tolerence = GetTolerance(eccBandD);

            val1 *= 10;
            value += val1;

            value += val2;

            value *= multiplier;

            return new Tuple<double, double>(value, tolerence);
        }

        private int GetSignficand(ElectronicColorCodeEnum electronicColorCodeEnum)
        {
            int val = GetValFromECCode(electronicColorCodeEnum);

            if (val < 0) throw new ArgumentOutOfRangeException("Only colors Black - White are allowed for use as a significand.");
            return (int)electronicColorCodeEnum;
        }

        private double GetMultiplier(ElectronicColorCodeEnum electronicColorCodeEnum)
        {
            int val = GetValFromECCode(electronicColorCodeEnum);

            if(electronicColorCodeEnum.ToString() == "Gold")
            {
                System.Diagnostics.Debug.WriteLine("The Multiplier is Gold.");
            }

            double multiplier = Math.Pow(10, val);
            return multiplier;
        }

        public static double GetTolerance(ElectronicColorCodeEnum electronicColorCodeEnum)
        {

            switch (electronicColorCodeEnum)
            {
                case ElectronicColorCodeEnum.None: return 0.2;
                case ElectronicColorCodeEnum.Pink: return ThrowOutOfRange(electronicColorCodeEnum);
                case ElectronicColorCodeEnum.Silver: return 0.1;
                case ElectronicColorCodeEnum.Gold: return 0.05;
                case ElectronicColorCodeEnum.Black: return ThrowOutOfRange(electronicColorCodeEnum);
                case ElectronicColorCodeEnum.Brown: return 0.01;
                case ElectronicColorCodeEnum.Red: return 0.02;
                case ElectronicColorCodeEnum.Orange: return ThrowOutOfRange(electronicColorCodeEnum);
                case ElectronicColorCodeEnum.Yellow: return 0.05;
                case ElectronicColorCodeEnum.Green: return 0.005;
                case ElectronicColorCodeEnum.Blue: return 0.0025;
                case ElectronicColorCodeEnum.Violet: return 0.001;
                case ElectronicColorCodeEnum.Gray: return 0.0005;
                case ElectronicColorCodeEnum.White: return ThrowOutOfRange(electronicColorCodeEnum);
                default:
                    throw new InvalidOperationException($"The color: {electronicColorCodeEnum} is not recognized or is not supported.");
            }

            // Nested Method, available only to parent method (GetTolerance.)
            double ThrowOutOfRange(ElectronicColorCodeEnum ecce)
            {
                throw new ArgumentOutOfRangeException($"{ecce} is not a valid color to indicate tolerance (4th band.)");
            }
        }

        private int GetValFromECCode(ElectronicColorCodeEnum electronicColorCodeEnum)
        {
            return (int)electronicColorCodeEnum;
        }

        private bool TryGetECC(string x, out ElectronicColorCodeEnum electronicColorCodeEnum)
        {
            bool ignoreCase = true;
            return Enum.TryParse<ElectronicColorCodeEnum>(x, ignoreCase, out electronicColorCodeEnum);
        }
    }
}
