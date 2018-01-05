using Arta;
using ArtaStatistics.View.ViewModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ArtaStatistics.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private int distribution = 0;
        private int iterations = 1000;
        private int lag = 10;
        private double correlationCoefficient = 0.8;

        public ObservableCollection<double> ArtaNumbers { get; set; }

        public ICommand ExecuteCommand { get; set; }
        
        public MainViewModel()
        {
            PropertyChanged += MainViewModel_PropertyChanged;
            ExecuteCommand = new RelayCommand(Execute);
        }

        private void Execute(object obj)
        {
            var context = new ArtaExecutionContext((ArtaExecutionContext.Distribution)Distribution, new double[] { correlationCoefficient });
            var artaProcess = context.ArtaProcess;
            ArtaNumbers = new ObservableCollection<double>();
            for(int i = 0; i < Iterations; i++)
            {
                ArtaNumbers.Add(artaProcess.Next());
            }   
        }
      
        public int Distribution
        {
            get { return distribution; }
            set { SetProperty(ref distribution, value); }
        }
        public double CorrelationCoefficient
        {
            get { return correlationCoefficient; }
            set { SetProperty(ref correlationCoefficient, value); }
        }
        public int Iterations
        {
            get { return iterations; }
            set { SetProperty(ref iterations, value); }
        }

        public int Lag
        {
            get { return lag; }
            set { SetProperty(ref lag, value); }
        }


        private void MainViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

        }

    }
}
