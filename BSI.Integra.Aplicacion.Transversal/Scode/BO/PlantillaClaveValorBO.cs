using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    ///BO: PlantillaClaveValorBO
    ///Autor: Edgar S.
    ///Fecha: 30/04/2021
    ///<summary>
    ///Columnas y funciones de la tabla T_PlantillaClaveValor
    ///</summary>
    public class PlantillaClaveValorBO : BaseBO
    {
        /// Propiedades             Significado
        /// -------------	        -----------------------
        /// Clave                   Clave de plantilla
        /// Valor                   Valor de plantilla
        /// Etiquetas               Etiquetas de plantilla
        /// IdPlantilla             Id de plantilla
        /// IdMigracion             Id de migración
        public string Clave { get; set; }
        public string Valor { get; set; }
        public string Etiquetas { get; set; }
        public int IdPlantilla { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
