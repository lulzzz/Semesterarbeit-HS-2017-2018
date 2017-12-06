using SimioAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simio
{
    class ArtaExecutionProperty : IProperty
    {
        string _value;
        public string Name => "ArtaExecutionProperty";

        public string Value { get { return _value; } set { _value = value; } }

        public IUnitBase Unit => throw new NotImplementedException();
    }
}
