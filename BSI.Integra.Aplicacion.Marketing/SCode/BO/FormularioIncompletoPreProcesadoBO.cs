using BSI.Integra.Aplicacion.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public class FormularioIncompletoPreprocesadoBO : BaseEntity
    {

        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public string Fijo { get; set; }
        public string Movil { get; set; }
        public int IdPais { get; set; }
        public int IdRegion { get; set; }
        public int IdAreaFormacion { get; set; }
        public int IdCargo { get; set; }
        public int IdAreaTrabajo { get; set; }
        public int IdIndustria { get; set; }
        public string NombrePrograma { get; set; }
        public int IdCentroCosto { get; set; }
        //public string CentroCosto { get; set; }
        public int IdTipoDato { get; set; }
        public int IdFaseVenta { get; set; }
        public string Origen { get; set; }
        public bool Procesado { get; set; }
        public int IdCampania { get; set; }
        public int IdFaseOportunidadPortalTemp { get; set; }
        public DateTime FechaRegistroCampania { get; set; }
        public int IdCategoriaDato { get; set; }
        public int IdTipoInteraccion { get; set; }
        public int IdInteraccionFormulario { get; set; }
        public string UrlOrigen { get; set; }
        public int IdPagina { get; set; }

        public byte[] RowVersion { get; set; }
    }
}
