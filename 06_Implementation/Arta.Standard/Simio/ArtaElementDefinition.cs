using SimioAPI;
using SimioAPI.Extensions;
using System;
using System.Drawing;

namespace Simio
{
    public class ArtaElementDefinition : Configuration, IElementDefinition
    {
        public static readonly Guid MY_ID = new Guid("{48A1ECB4-DE39-4265-ABD9-66D56B4A70F6}");

        public string Name
        {
            get { return "Arta"; }
        }

        public string Description
        {
            get { return "Contains an ArtaProcess which generates \"Autoregressiv To Anything\" numbers. "; }
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
            pd = schema.PropertyDefinitions.AddRealProperty("CorrelationCoefficient1", 0.5);
            pd = schema.PropertyDefinitions.AddRealProperty("CorrelationCoefficient2", 0.5);
            schema.ElementFunctions.AddSimpleRealNumberFunction("Process", "Returns \"Autoregressiv To Anything\" numbers with the given Correlationcoefficients.", new SimioSimpleRealNumberFunc(ArtaProcess));
        }

        private static double ArtaProcess(object element)
        {
            return artaExecutionContext.ArtaProcess.Next();
        }
    }


}
