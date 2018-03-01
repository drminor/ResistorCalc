using System.Windows;
using System;

namespace ResistorCalcChallenge
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            MainWindowViewModel vm = new MainWindowViewModel();
            DataContext = vm;

            InitializeComponent();

            this.BandA.SelectedIndex = 0;
            this.BandB.SelectedIndex = 1;
            this.BandC.SelectedIndex = 3;
            this.BandD.SelectedIndex = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
