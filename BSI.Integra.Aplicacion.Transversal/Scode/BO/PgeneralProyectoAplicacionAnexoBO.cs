using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    /// BO: PgeneralProyectoAplicacionAnexoBO
    /// Autor: Lourdes Priscila Pacsi Gamboa
    /// Fecha: 02/07/2021
    /// <summary>
    /// Columnas y funciones de la tabla PgeneralProyectoAplicacionAnexo
    /// </summary>
    public class PgeneralProyectoAplicacionAnexoBO : BaseBO
    {
        public int IdPgeneral { get; set; }
        public string NombreArchivo { get; set; }
        public string RutaArchivo { get; set; }
        public bool EsEnlace { get; set; }
        public bool SoloLectura { get; set; }
    }
}
