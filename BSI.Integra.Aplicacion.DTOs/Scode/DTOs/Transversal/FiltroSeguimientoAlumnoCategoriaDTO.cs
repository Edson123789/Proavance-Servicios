namespace BSI.Integra.Aplicacion.DTOs
{
    public class FiltroSeguimientoAlumnoCategoriaDTO
    {
        public int Id { get;set; }
        public string Nombre { get;set; }
        public int IdTipoSeguimientoAlumnoCategoria { get;set; }
        public bool AplicaModalidadOnline { get;set; }
        public bool AplicaModalidadAonline { get;set; }
        public bool AplicaModalidadPresencial { get;set; }
    }
}
