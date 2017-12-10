using Arta;
using Arta.Distribution;
using Arta.Math;
using SimioAPI;
using SimioAPI.Extensions;
using System;
using System.Drawing;

namespace Simio
{
    public class ArtaElementDefinition : IElementDefinition
    {
        public static readonly Guid MY_ID = new Guid("{48A1ECB4-DE39-4265-ABD9-66D56B4A70F6}");

        public string Name
        {
            get { return "Arta"; }
        }

        public string Description
        {
            get { return "Creates a new Arta Element"; }
        }

        public Image Icon
        {
            get { return null; }
        }

        public Guid UniqueID
        {
            get { return MY_ID; }
        }

        public IElement CreateElement(IElementData data)
        {
            return new ArtaElement(data);
        }

        public void DefineSchema(IElementSchema schema)
        {
            IPropertyDefinition pd;
            pd = schema.PropertyDefinitions.AddRealProperty("CorrelationCoefficient1", 0.0);
            pd = schema.PropertyDefinitions.AddRealProperty("CorrelationCoefficient2", 0.0);
            schema.ElementFunctions.AddSimpleRealNumberFunction("ARTAfunky", "funk funk funk", new SimioSimpleRealNumberFunc(FUCKINGAWESOMEDELEGATE));  
        }

        private static double FUCKINGAWESOMEDELEGATE(object element)
        {
            ArtaExecutionContext artaExecutionContext = new ArtaExecutionContext(BaseDistribution.Distribution.ExponentialDistribution, new double[] { 0.1, 0.2 });
            return artaExecutionContext.ArtaProcess.Next();
        }
    }

  
}
