namespace BSI.Integra.Aplicacion.DTOs
{
    public class AsesorFaseOpoProbabilidadDetalleDTO
    {
        public int Id { get; set; }
        //public int IdAsesorCentroCosto { get; set; }
        public int IdFaseOportunidad { get; set; }
        //public List<int> Probabilidad { get; set; }
        public string Probabilidad { get; set; }
        public string Tipo { get; set; }
    }
}
