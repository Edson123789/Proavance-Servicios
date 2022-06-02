using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    /// BO: ArticuloBO
    /// Autor: 
    /// Fecha: 24/02/2021
    /// <summary>
    /// Definicion Variables ArticuloSeoBO
    /// </summary>
    public class ArticuloSeoBO : BaseBO 
    {
        public string Descripcion { get; set; }
        public int IdArticulo { get; set; }
        public int IdParametroSeo { get; set; }       
        public Guid? IdMigracion { get; set; }
    }
}
