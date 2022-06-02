using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    /// BO: Marketing/FaseByPlantilla
    /// Autor: _ _ _ _ _ . 
    /// Fecha: 30/04/2021
    /// <summary>
    /// BO para la logica de FaseByPlantilla
    /// </summary>
    public class FaseByPlantillaBO : BaseBO
    {
        /// Propiedades             Significado
        /// -----------	            ------------
        /// idPlantilla             Id de Plantilla
        /// idFaseOrigen            Id de Fase de Origen
        /// NombreFase              Nombre de Fase
        /// IdMigracion             Id de Migración
        public int idPlantilla { get; set; }
        public int idFaseOrigen { get; set; }
        public string NombreFase { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
