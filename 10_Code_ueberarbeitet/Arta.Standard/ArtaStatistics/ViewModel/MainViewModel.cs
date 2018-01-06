using Arta;
using Arta.Fitting;
using Arta.Math;
using ArtaStatistics.View.ViewModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using System.Text;
using System.IO;
using CsvHelper;
using Microsoft.Win32;

namespace ArtaStatistics.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private int distribution = 0;
        private int iterations = 0;
        private int lag = 10;
        private int order;
        private string errorMessage;
        private double correlationCoefficient = 0.8;
        private double[] artaNumbers;
        private double[] acfs;
        private double[] pacfs;
        private bool exportIsEnabled = false;

        private Visibility errorIsVisible = Visibility.Hidden;

        public ObservableCollection<ListHelper> ArtaNumbers { get; set; }
        public ObservableCollection<ListHelper> Acfs { get; set; }
        public ObservableCollection<ListHelper> Pacfs { get; set; }

        public ICommand ExecuteCommand { get; set; }
        public ICommand ExportCommand { get; set; }

        public MainViewModel()
        {
            ArtaNumbers = new ObservableCollection<ListHelper>();
            Acfs = new ObservableCollection<ListHelper>();
            Pacfs = new ObservableCollection<ListHelper>();
            ExecuteCommand = new RelayCommand(Execute);
            ExportCommand = new RelayCommand(ExportToCSV);
        }


        public bool ExportIsEnabled
        {
            get { return exportIsEnabled; }
            set { SetProperty(ref exportIsEnabled, value); }
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
        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }
        public Visibility ErrorIsVisible
        {
            get { return errorIsVisible; }
            set { SetProperty(ref errorIsVisible, value); }
        }
        public int Order
        {
            get { return order; }
            set { SetProperty(ref order, value); }
        }

        private void Execute(object obj)
        {
            ErrorIsVisible = Visibility.Hidden;
            if (!InputIsValid()) { return; }
            ArtaNumbers.Clear();
            Acfs.Clear();
            Pacfs.Clear();

            try
            {
                var context = new ArtaExecutionContext((ArtaExecutionContext.Distribution)Distribution, new double[] { correlationCoefficient });
                var artaProcess = context.ArtaProcess;
                artaNumbers = new double[iterations];
                artaNumbers = new double[iterations];

                for (var i = 0; i < Iterations; i++)
                {
                    artaNumbers[i] = artaProcess.Next();
                    ArtaNumbers.Add(new ListHelper(artaNumbers[i], 0, 0));
                }

                acfs = AutoCorrelation.CalculateAcfs(artaNumbers, lag);
                foreach (var item in acfs)
                {
                    Acfs.Add(new ListHelper(0, item, 0));
                }

                pacfs = AutoCorrelation.CalculatePacfs(acfs);
                foreach (var item in pacfs)
                {
                    Pacfs.Add(new ListHelper(0, 0, item));
                }
                Order = OrderEstimator.EstimateOrder(artaNumbers, lag);
                ExportIsEnabled = true;
            }
            catch (Exception exception)
            {
                ErrorMessage = exception.Message;
                ErrorIsVisible = Visibility.Visible;
            }

        }

        private bool InputIsValid()
        {
            if (correlationCoefficient > 1 || correlationCoefficient < -1) { CorrelationCoefficient = 0; ErrorMessage = "Correlation coefficient has to be between -1 and 1"; ErrorIsVisible = Visibility.Visible; return false; }
            if (Iterations < 1000) { Iterations = 0; ErrorMessage = "Iterations has to be higher than 999"; ErrorIsVisible = Visibility.Visible; return false; }
            return true;
        }

        private void ExportToCSV(object obj)
        {
            ErrorIsVisible = Visibility.Hidden;
            try
            {
                var stringBuilder = new StringBuilder();
                stringBuilder.AppendLine(string.Format(string.Format("{0};{1};{2};Order = {3}", "ArtaNumber", "Pacf", "Acf", Order)));

                for (int i = 0; i < Iterations; i++)
                {

                    stringBuilder.AppendLine(string.Format(string.Format("{0};{1};{2}", ArtaNumbers[i].ArtaNumber, i < Lag ? Pacfs[i].Pacf.ToString() : "", i < Lag ? Acfs[i].Acf.ToString() : "")));

                }
                if(!Directory.Exists(Directory.GetCurrentDirectory() + @"\ArtaStatistics\")){
                    Directory.CreateDirectory(Directory.GetCurrentDirectory() +  @"\ArtaStatistics\");
                }

                File.WriteAllText(Directory.GetCurrentDirectory() + "/ArtaStatistics/ArtaStatistics.csv", stringBuilder.ToString());
             //   File.Open(Directory.GetCurrentDirectory() + "/ArtaStatistics/ArtaStatistics.csv", FileMode.Open);

                var openFileDialog = new OpenFileDialog();
                openFileDialog.InitialDirectory = Directory.GetCurrentDirectory() + @"\ArtaStatistics\";
                openFileDialog.ShowDialog();
            }
            catch(Exception e)
            {
                ErrorIsVisible = Visibility.Visible;
                ErrorMessage = e.Message;
            }
          
        }

   


    }

    public class ListHelper
    {
        public ListHelper(double artaNumber, double acfs, double pacfs)
        {
            ArtaNumber = artaNumber;
            Acf = acfs;
            Pacf = pacfs;
        }
        public double ArtaNumber { get; set; }
        public double Acf { get; set; }
        public double Pacf { get; set; }
    }
}
