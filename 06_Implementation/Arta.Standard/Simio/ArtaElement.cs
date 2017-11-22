using SimioAPI.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimioAPI;
using System.Drawing;

namespace Simio
{
    class ArtaElement : IElement
    {
        IElementData _data;
        double[] artaCorrelationCoefficients;
        double coefficient1, coefficient2;
        //BaseDistribution distibution;
        
        public ArtaElement(IElementData data)
        {
            _data = data;
            IPropertyReader corr1 = _data.Properties.GetProperty("CorrelationCoefficient1");
            IPropertyReader corr2 = _data.Properties.GetProperty("CorrelationCoefficient1");
            IPropertyReader distributionProperty = _data.Properties.GetProperty("DistributionProperty");



            coefficient1 = corr1.GetDoubleValue(_data.ExecutionContext);
            coefficient2 = corr2.GetDoubleValue(_data.ExecutionContext);
            //Distribution dist = new Distribution();
            //artaCorrelationCoefficients = new double[] { coefficient1, coefficient2 };
            //IArtaProcess arta = ArtaProcessFactory.CreateArtaProcess(distribution, artaCorrelationCoefficients);

        }

        public void Initialize()
        {
            throw new NotImplementedException();
        }

        public void Shutdown()
        {
            throw new NotImplementedException();
        }
    }
}
