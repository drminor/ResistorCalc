using System;

namespace ResistorCalcChallenge
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly OhmValueCalculator _ohmValueCalculator;

        public MainWindowViewModel()
        {
            _ohmValueCalculator = new OhmValueCalculator();
        }

        ElectronicColorCode _bandA; 
        public ElectronicColorCode BandA
        {
            get => _bandA;
            set => SetProperty(ref _bandA, value, "BandA", UpdateRValue);
        }

        ElectronicColorCode _bandB;
        public ElectronicColorCode BandB
        {
            get => _bandB;
            set => SetProperty(ref _bandB, value, "BandB", UpdateRValue);
        }

        ElectronicColorCode _bandC;
        public ElectronicColorCode BandC
        {
            get => _bandC;
            set => SetProperty(ref _bandC, value, "BandC", UpdateRValue);
        }

        ElectronicColorCode _bandD;
        public ElectronicColorCode BandD
        {
            get => _bandD;
            set => SetProperty(ref _bandD, value, "BandD", UpdateRValue);
        }

        string _resistorValue;
        public string ResistorValue
        {
            get => _resistorValue;
            set => SetProperty(ref _resistorValue, value, "ResistorValue");
        }

        string _minValue;
        public string MinValue
        {
            get => _minValue;
            set => SetProperty(ref _minValue, value, "MinValue");
        }

        string _maxValue;
        public string MaxValue
        {
            get => _maxValue;
            set => SetProperty(ref _maxValue, value, "MaxValue");
        }

        private void UpdateRValue()
        {
            Tuple<double, double> valAndTolerance =_ohmValueCalculator.CalculateOhmValue
                (
                BandA?.ElectronicColorCodeEnum ?? ElectronicColorCodeEnum.Black,
                BandB?.ElectronicColorCodeEnum ?? ElectronicColorCodeEnum.Black,
                BandC?.ElectronicColorCodeEnum ?? ElectronicColorCodeEnum.Black,
                BandD?.ElectronicColorCodeEnum ?? ElectronicColorCodeEnum.None
                );

            string valFormat = GetFormat(valAndTolerance.Item1);
            ResistorValue = valAndTolerance.Item1.ToString(valFormat);

            double minVal = valAndTolerance.Item1 - valAndTolerance.Item1 * valAndTolerance.Item2;
            double maxVal = valAndTolerance.Item1 + valAndTolerance.Item1 * valAndTolerance.Item2;

            valFormat = GetFormat(minVal);
            MinValue = minVal.ToString(valFormat);
            MaxValue = maxVal.ToString(valFormat);
        }

        private string GetFormat(double x)
        {
            string result;

            if(x < 1)
            {
                result = "0.0##;;0";
            }
            else if(x < 10)
            {
                result = "##0.0##;;0";
            }
            else
            {
                result = "###,###;;0";
            }

            return result;
        }

    }
}
