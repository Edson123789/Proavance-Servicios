namespace BSI.Integra.Aplicacion.DTOs
{
    public class AsesorPaisDetalleDTO
    {
        public int Id { get; set; }
        public int IdAsesorCentroCosto { get; set; }
        public int IdPais { get; set; }
        public int Prioridad { get; set; }
    }
    public class AsesorAreaDetalleDTO
    {
        public int Id { get; set; }
        public int IdAsesorCentroCosto { get; set; }
        public int IdAreaCapacitacion { get; set; }
        public int Prioridad { get; set; }
    }
    public class AsesorSubAreaDetalleDTO
    {
        public int Id { get; set; }
        public int IdAsesorCentroCosto { get; set; }
        public int IdSubAreaCapacitacion { get; set; }
        public int Prioridad { get; set; }
    }
    public class AsesorPGeneralDetalleDTO
    {
        public int Id { get; set; }
        public int IdAsesorCentroCosto { get; set; }
        public int IdProgramaGeneral { get; set; }
        public int Prioridad { get; set; }
    }
    public class AsesorGrupoFiltroPCriticoDetalleDTO
    {
        public int Id { get; set; }
        public int IdAsesorCentroCosto { get; set; }
        public int IdGrupoProgramaCritico { get; set; }
        public int Prioridad { get; set; }
    }
    public class AsesorGrupoDetalleDTO
    {
        public int Id { get; set; }
        public int IdAsesorCentroCosto { get; set; }
        public int IdGrupo { get; set; }
        public int Prioridad { get; set; }
    }
}
