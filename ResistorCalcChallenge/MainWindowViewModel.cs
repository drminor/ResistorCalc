using System;

namespace ResistorCalcChallenge
{
    public class MainWindowViewModel : ViewModelBase
    {
        OhmValueCalculator _ohmValueCalculator;

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

        public void UpdateRValue()
        {
            Tuple<double, double> valAndTolerance =_ohmValueCalculator.CalculateOhmValue
                (
                BandA?.ElectronicColorCodeEnum ?? ElectronicColorCodeEnum.Black,
                BandB?.ElectronicColorCodeEnum ?? ElectronicColorCodeEnum.Black,
                BandC?.ElectronicColorCodeEnum ?? ElectronicColorCodeEnum.Black,
                BandD?.ElectronicColorCodeEnum ?? ElectronicColorCodeEnum.None
                );

            string valFormat;

            if (valAndTolerance.Item1 < 1)
            {
                valFormat = "0.000;;0";
            }
            else
            {
                valFormat = "###,###;;0";
            }

            ResistorValue = valAndTolerance.Item1.ToString(valFormat);

            MinValue = (valAndTolerance.Item1 - valAndTolerance.Item1 * valAndTolerance.Item2).ToString();

            MaxValue = (valAndTolerance.Item1 + valAndTolerance.Item1 * valAndTolerance.Item2).ToString();
        }

    }
}
