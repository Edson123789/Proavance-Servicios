using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    /// BO: ArticuloPgeneralBO
    /// Autor: 
    /// Fecha: 24/02/2021
    /// <summary>
    /// Definicion Variables ArticuloTagBO
    /// </summary>
    public class ArticuloTagBO : BaseBO
    {
        public int IdArticulo { get; set; }
        public int IdTag { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
