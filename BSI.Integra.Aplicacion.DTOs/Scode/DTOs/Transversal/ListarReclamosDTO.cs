using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal
{
    public class ListarReclamosDTO
    {
        public int Id { get; set; }
        public int IdMatricula { get; set; } //IdCabeceraMatricula
        public string CodigoMatricula { get; set; }
        public string DNI { get; set; }
        public string NombreAlumno { get; set; }
        public string PersonalAsignado { get; set; }
        public string Descripcion { get; set; }
        public string CentroCosto { get; set; }
        public string Origen { get; set; }
        public int IdOrigen { get; set; }
        public string EstadoMatricula { get; set; }
        public string ReclamoEstado { get; set; }
        public int IdEstadoReclamo { get; set; }
        public DateTime? FechaUltimaLlamada { get; set; }
        public DateTime? FechaUltimoCorreo { get; set; }
        public DateTime? FechaUltimoWapp { get; set; }
        public int IdTipoReclamoAlumno { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string TipoReclamoAlumno { get; set; }

    }
}
