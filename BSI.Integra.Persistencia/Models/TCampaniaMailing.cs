using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TCampaniaMailing
    {
        public TCampaniaMailing()
        {
            TCampaniaMailingDetalle = new HashSet<TCampaniaMailingDetalle>();
            TCampaniaMailingValorTipo = new HashSet<TCampaniaMailingValorTipo>();
            TPrioridadMailChimpLista = new HashSet<TPrioridadMailChimpLista>();
            TPrioridadMailChimpListaCorreo = new HashSet<TPrioridadMailChimpListaCorreo>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public int PrincipalValor { get; set; }
        public string PrincipalValorTiempo { get; set; }
        public int SecundarioValor { get; set; }
        public string SecundarioValorTiempo { get; set; }
        public int ActivaValor { get; set; }
        public string ActivaValorTiempo { get; set; }
        public int IdParaConjuntoAnuncios { get; set; }
        public int IdCategoriaOrigen { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
        public DateTime? FechaInicioExcluirPorEnviadoMismoProgramaGeneralPrincipal { get; set; }
        public DateTime? FechaFinExcluirPorEnviadoMismoProgramaGeneralPrincipal { get; set; }

        public virtual ICollection<TCampaniaMailingDetalle> TCampaniaMailingDetalle { get; set; }
        public virtual ICollection<TCampaniaMailingValorTipo> TCampaniaMailingValorTipo { get; set; }
        public virtual ICollection<TPrioridadMailChimpLista> TPrioridadMailChimpLista { get; set; }
        public virtual ICollection<TPrioridadMailChimpListaCorreo> TPrioridadMailChimpListaCorreo { get; set; }
    }
}
