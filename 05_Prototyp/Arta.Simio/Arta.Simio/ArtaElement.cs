using SimioAPI.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimioAPI;
using System.Drawing;
using System.Collections;

namespace Arta.Standard
{
    class ArtaElementDefinition : IElementDefinition
    {
        public static readonly Guid MY_ID = new Guid("{9985B71B-7852-4F93-8D74-DFC89C9E97F3}");

        public string Name
        {
            get { return "Arta"; }
        }

        public string Description
        {
            get { return "Creates a new Arta-Element"; }
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
            pd = schema.PropertyDefinitions.AddStringProperty("ArtaDistributionProperty", "NormalDistribution");
            pd.DisplayName = "Distribution";
            pd.Required = true;

            pd = schema.PropertyDefinitions.AddRealProperty("CorrelationCoefficent1", 0.4);
            pd.DisplayName = "Correlationcoefficent 1";


            pd = schema.PropertyDefinitions.AddRealProperty("CorrelationCoefficent2", 0.8);
            pd.DisplayName = "Correlationcoefficient 2";

            pd = schema.PropertyDefinitions.AddRealProperty("ArtaRun", 1.0);

        }
    }


    //For each Distribution supported by ARTA.Standard, create a separate Property
    class ArtaRun : IProperty
    {
        public string Name => "ArtaRun";

        public string Value { get { return DateTime.Now.Second.ToString(); } set { value = DateTime.Now.Second.ToString(); } }

        public IUnitBase Unit => throw new NotImplementedException();
    }

    class ArtaElement : IElement
    {
        IElementData _data;
        IProperty _artaRunProp;
        double[] alphas = { 3, 8, 2, 5, 10};
        double c1, c2;

        public ArtaElement(IElementData data)
        {
            _data = data;
            IPropertyReader corr1 = _data.Properties.GetProperty("CorrelationCoefficent1");
            IPropertyReader corr2 = _data.Properties.GetProperty("CorrelationCoefficent2");
            _artaRunProp = (IProperty)_data.Properties.GetProperty("ArtaRun");

            c1 = corr1.GetDoubleValue(_data.ExecutionContext);
            c2 = corr1.GetDoubleValue(_data.ExecutionContext);
        }

        public void Initialize()
        {
            /*
            //CreateArtaProcess()
            for (int i = 0; i < alphas.Length; i++)
            {
                _artaRunProp.Value = alphas[i].ToString();
            }
            */

            _artaRunProp.Value = DateTime.Now.Second.ToString();
            
        }

        public void Shutdown()
        {
            _artaRunProp.Value = "1.0";
        }
    }
}
