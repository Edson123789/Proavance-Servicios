namespace BSI.Integra.Aplicacion.DTOs
{
    public class FiltroSegmentoCompuestoDTO
    {
        public int IdAlumno { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public string NombreAlumno { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string NombrePais { get; set; }
        public string NombreCiudad { get; set; }
        public string NombreAreaFormacion { get; set; }
        public string NombreAreaTrabajo { get; set; }
        public string NombreIndustria { get; set; }
        public string NombreCargo { get; set; }
        public bool? EsVentaCruzada { get; set;}

        public string NombreCentroCosto { get; set; }
        public string CodigoMatricula { get; set; }
        public int? IdOportunidad { get; set; }
    }
}
