using SimioAPI;
using SimioAPI.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simio
{
    class ArtaElementDefinition : IElementDefinition
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
            
        }
    }
}
