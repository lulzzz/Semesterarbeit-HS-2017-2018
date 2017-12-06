using SimioAPI.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimioAPI;
using System.Drawing;
using Arta.Math;

namespace Simio
{
    class ArtaElement : IElement
    {
        ArtaExecutionContext arta;
        IElementData _data;
        double[] artaCorrelationCoefficients = new double[] { 0, 0 };
        double coefficient1, coefficient2;
        ArtaExecutionProperty artaExecutionProperty;
        
        public ArtaElement(IElementData data)
        {
            _data = data;
            IPropertyReader corr1 = _data.Properties.GetProperty("CorrelationCoefficient1");
            IPropertyReader corr2 = _data.Properties.GetProperty("CorrelationCoefficient1");
            IPropertyReader distributionProperty = _data.Properties.GetProperty("DistributionProperty");

            coefficient1 = corr1.GetDoubleValue(_data.ExecutionContext);
            coefficient2 = corr2.GetDoubleValue(_data.ExecutionContext);
            arta = new ArtaExecutionContext(BaseDistribution.Distribution.ExponentialDistribution, artaCorrelationCoefficients);

        }

        public void Initialize()
        {
            artaExecutionProperty.Value = arta.Arta.Next().ToString();
        }

        public void Shutdown()
        {
            throw new NotImplementedException();
        }
    }
}
