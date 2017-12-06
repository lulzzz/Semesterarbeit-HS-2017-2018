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
        private IProperty artaExecution;
        public static double coefficient1, coefficient2;
        public ArtaExecutionContext artaExecutionContext = new ArtaExecutionContext(BaseDistribution.Distribution.ExponentialDistribution, new double[] { coefficient1, coefficient2 });

        public ArtaElement(IElementData data)
        {
            _data = data;
            IPropertyReader corr1 = _data.Properties.GetProperty("CorrelationCoefficient1");
            IPropertyReader corr2 = _data.Properties.GetProperty("CorrelationCoefficient2");
            ArtaExecutionProperty.coefficient1 = corr1.GetDoubleValue(_data.ExecutionContext);
            ArtaExecutionProperty.coefficient2 = corr2.GetDoubleValue(_data.ExecutionContext);
            coefficient1 = corr1.GetDoubleValue(_data.ExecutionContext);
            coefficient2 = corr2.GetDoubleValue(_data.ExecutionContext);

            artaExecution = (IProperty)_data.Properties.GetProperty("ArtaExecutionProperty");
         
            


        }

        public void Initialize()
        {
            
        }

        public void Shutdown()
        {

        }
    }

    public class ArtaExecutionProperty : IProperty
    {
        public static double coefficient1, coefficient2;
        public ArtaExecutionContext artaExecutionContext = new ArtaExecutionContext(BaseDistribution.Distribution.ExponentialDistribution, new double[] { coefficient1, coefficient2 });
        string pipi;

        public string Name => "ArtaExecutionProperty";

        public string Value { get => artaExecutionContext.ArtaProcess.Next().ToString(); set { value = artaExecutionContext.ArtaProcess.Next().ToString();  } }

        public IUnitBase Unit => throw new NotImplementedException();
    }

   
}
