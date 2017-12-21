using Arta;
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
            var corr1 = _data.Properties.GetProperty("CorrelationCoefficient");
            correlationCoefficient = corr1.GetDoubleValue(_data.ExecutionContext);
            artaExecutionContext = new ArtaExecutionContext(ArtaExecutionContext.Distribution.ExponentialDistribution, new double[] { correlationCoefficient });
        }

        public void Shutdown()
        {

        }
    }
}
