using Arta;
using Arta.Math;
using SimioAPI;
using SimioAPI.Extensions;
using System;

namespace Simio
{
    public class ArtaElement : IElement
    {
        private IElementData _data;
        private static double coefficient1, coefficient2;

        public ArtaElement(IElementData data)
        {
            _data = data;
            IPropertyReader corr1 = _data.Properties.GetProperty("CorrelationCoefficient1");
            IPropertyReader corr2 = _data.Properties.GetProperty("CorrelationCoefficient2");           
            coefficient1 = corr1.GetDoubleValue(_data.ExecutionContext);
            coefficient2 = corr2.GetDoubleValue(_data.ExecutionContext);
        }
        public void Initialize()
        {

        }

        public void Shutdown()
        {

        }
    }
}
