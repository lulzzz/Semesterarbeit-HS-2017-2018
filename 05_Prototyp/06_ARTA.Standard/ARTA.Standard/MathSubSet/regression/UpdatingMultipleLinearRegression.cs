using System;
using System.Collections.Generic;
using System.Text;

namespace MathSubSet.regression
{
    interface UpdatingMultipleLinearRegression
    {
        bool HasIntercept();
        long GetN();

        void AddObservation(double[] paramArrayOfDouble, double paramDouble); //    throws ModelSpecificationException;

        void AddObservations(double[][] paramArrayOfDouble, double[] paramArrayOfDouble1); //    throws ModelSpecificationException;

        void Clear();

        RegressionResults Regress(); //    throws ModelSpecificationException, NoDataException;

        RegressionResults Regress(int[] paramArrayOfInt);// throws ModelSpecificationException, MathIllegalArgumentException;
    }
}
