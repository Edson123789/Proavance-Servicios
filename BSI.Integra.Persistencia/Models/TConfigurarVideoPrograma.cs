using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TConfigurarVideoPrograma
    {
        public TConfigurarVideoPrograma()
        {
            TSesionConfigurarVideo = new HashSet<TSesionConfigurarVideo>();
        }

        public int Id { get; set; }
        public int IdPgeneral { get; set; }
        public int IdDocumentoSeccionPw { get; set; }
        public string VideoId { get; set; }
        public string TotalMinutos { get; set; }
        public string Archivo { get; set; }
        public string NroDiapositivas { get; set; }
        public bool Configurado { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
        public bool? ConImagenVideo { get; set; }
        public string ImagenVideoNombre { get; set; }
        public string ImagenVideoAncho { get; set; }
        public string ImagenVideoAlto { get; set; }
        public bool? ConImagenDiapositiva { get; set; }
        public string ImagenDiapositivaNombre { get; set; }
        public string ImagenDiapositivaAncho { get; set; }
        public string ImagenDiapositivaAlto { get; set; }
        public int? NumeroFila { get; set; }
        public string Token { get; set; }
        public int? ImagenVideoPosicionX { get; set; }
        public int? ImagenVideoPosicionY { get; set; }
        public int? ImagenDiapositivaPosicionX { get; set; }
        public int? ImagenDiapositivaPosicionY { get; set; }
        public string VideoIdBrightcove { get; set; }

        public virtual TDocumentoSeccionPw IdDocumentoSeccionPwNavigation { get; set; }
        public virtual TPgeneral IdPgeneralNavigation { get; set; }
        public virtual ICollection<TSesionConfigurarVideo> TSesionConfigurarVideo { get; set; }
    }
}
