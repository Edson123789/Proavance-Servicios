using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class DominioBO : BaseBO
    {
        public string Nombre { get; set; }
        public string IpPublico { get; set; }
        public string IpPrivado { get; set; }
    }

    public class listaDominioBO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string IpPublico { get; set; }
        public string IpPrivado { get; set; }
    }

    public class filtroDominioBO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }

}
