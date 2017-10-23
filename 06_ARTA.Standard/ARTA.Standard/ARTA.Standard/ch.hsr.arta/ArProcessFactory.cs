using ARTA.core.ch.hsr.math;
using Math3.distribution;
using Math3.linear;
using Math3.random;
using System;

namespace ARTA.core.ch.hsr.arta
{
    internal class ArProcessFactory
    {
        private ArProcessFactory()
        {
            //no instantiation
        }

        public static ArProcess CreateArProcess(double[] arAutocorrealtions)
        {
            return CreateArProcess(arAutocorrealtions, new Well19937c());
        }

        ///<summary>
        ///Erzeugt einen AR-Prozess mit den gegebenen Korrelationskoeffizienten.
        ///Passt die errechneten Alpha-Werte in eine Normalverteilung ein, mit dem Mittelwert 0 und der Varianz kleiner 1
        ///</summary>

        public static ArProcess CreateArProcess(double[] arAutocorrelations, RandomGenerator random)
        {
                //Erzeugt eine Korrelationsmatrix und gibt die Reihe mit Index 0 als double[] zurück
                double[] alphas = ArAutocorrelationsToAlphas(arAutocorrelations);
                
                //Errechnet die Varianz aus den gegebenen Korrelationskoeffizienten und den erzeugten Alpha-Werten
                double variance = CalculateVariance(arAutocorrelations, alphas);

                //Erzeugt eine Normalverteilung der zufällig erzeugten Werte des Zufallszahlengenerators, untere Grenze 0.0, obere Grenze @variance
                //Wendet die Umkehrfunktion der Normalverteilung an um die gewünschte Randverteilung zu erhalten.
                NormalDistribution whiteNoiseProcess = new NormalDistribution(random, 0.0, Math.Sqrt(variance), 
                                                        NormalDistribution.DEFAULT_INVERSE_ABSOLUTE_ACCURACY);
          
            return new ArProcess(alphas, whiteNoiseProcess);
        }

        /**
         * Determines the coefficients (alpha) for the ARTA-Process.
         * */
        public static double[] ArAutocorrelationsToAlphas(double[] arAutocorrelations)
        {
            int dim = arAutocorrelations.Length;
            double[] alphas = new double[dim];

            RealMatrix psi = AutoCorrelation.GetCorrelationMatrix(arAutocorrelations);
            RealMatrix r = new Array2DRowRealMatrix(arAutocorrelations).transpose();
            RealMatrix a = r.multiply(new CholeskyDecomposition(psi).getSolver().getInverse());
            alphas = a.getRow(0);
            return alphas;
        }

        public static double CalculateVariance(double[] arAutocorrelations, double[] alphas)
        {
            double variance = 0;
            for (int i = 0; i < alphas.Length; i++)
            {
                variance = variance - alphas[i] * arAutocorrelations[i];
            }
            return variance;
        }
    }
}
