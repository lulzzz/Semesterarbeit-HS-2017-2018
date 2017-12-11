using Arta;
using Arta.Distribution;
using SimioAPI;
using SimioAPI.Extensions;

namespace Simio
{
    public class ArtaElement : Configuration, IElement
    {
        private IElementData _data;


        public ArtaElement(IElementData data)
        {
            _data = data;
        
        }
        public void Initialize()
        {
            var corr1 = _data.Properties.GetProperty("CorrelationCoefficient1");
            var corr2 = _data.Properties.GetProperty("CorrelationCoefficient2");
            correlationCoefficient1 = corr1.GetDoubleValue(_data.ExecutionContext);
            correlationCoefficient2 = corr2.GetDoubleValue(_data.ExecutionContext);
            artaExecutionContext = new ArtaExecutionContext(BaseDistribution.Distribution.ExponentialDistribution, new double[] { correlationCoefficient1, correlationCoefficient2 });
        }

        public void Shutdown()
        {

        }
    }
}
