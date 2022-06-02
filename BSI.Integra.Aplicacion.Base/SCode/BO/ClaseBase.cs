using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace BSI.Integra.Aplicacion.Base.BO
{
    public class ClaseBase : DynamicObject
    {
        public object this[string propertyName] {
            get { return GetType().GetProperty(propertyName).GetValue(this, null); }
            set { GetType().GetProperty(propertyName).SetValue(this, value, null); }
        }

    }
}
