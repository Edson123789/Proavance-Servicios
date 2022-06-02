using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class DatoPersonalDTO
    {
        public int Id { get; set; }
        public string NombreCompleto { get; set; }
    }
    public class DatoPersonalDiscadorDTO
    {
        public int Id { get; set; }
        public string Personal { get; set; }
        public Nullable<bool> Discador { get; set; }

    }
    public class AccesoPersonal
    {
        public string Url { get; set; }
        public int IdPersonal { get; set; }
    }
    public class PersonalJefaturaDTO
    {
        public int IdPersonal { get; set; }
        public string Personal { get; set; }
        public string PuestoTrabajo { get; set; }
        public int? IdJefeInmediato { get; set; }
    }
    public class PersonalJefaturaAgrupadoDTO
    {
        public int? IdJefeInmediato { get; set; }
        public List<PersonalJefaturaAsociadoDTO> PersonalACargo { get; set; }
    }
    public class PersonalJefaturaAsociadoDTO
    {
        public int? IdPersonal { get; set; }
        public string Personal { get; set; }
        public string PuestoTrabajo { get; set; }
    }
    public class PersonalJefaturaIteradorDTO
    {
        public int IdPersonal { get; set; }
        public string Personal { get; set; }
        public string PuestoTrabajo { get; set; }
        public List<PersonalJefaturaIteradorDTO> PersonalACargo { get; set; }
    }
    public class FiltroPersonalJefaturaDTO
    {
        public List<int> ListaPersonal { get; set; }
        public int? ListaAreaTrabajo { get; set; }
        public int? Estado { get; set; }
    }
    public class FiltroPersonalJefaturaFiltroDTO
    {
        public string PersonalAreaTrabajo { get; set; }
        public string Personal { get; set; }
        public string PersonalPuestoTrabajo { get; set; }
        public int PersonasACargo { get; set; }
        public string Estado { get; set; }
        public DateTime? FechaInicioPuesto { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime? FechaCese { get; set; }
        public string JefeInmediato { get; set; }
        public string PuestoJefeInmediato { get; set; }
    }

    public class PersonalConfiguracionOpenVoxDTO
    {
        public int IdPais { get; set; }
        public string Prefijo { get; set; }
        public string Anexo { get; set; }
    }
}
