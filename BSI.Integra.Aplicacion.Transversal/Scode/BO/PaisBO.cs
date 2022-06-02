using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Persistencia.Models;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class PaisBO : BaseBO
    {
        public int Id { get; set; }
        public int CodigoPais { get; set; }
        public string CodigoIso { get; set; }
        public string NombrePais { get; set; }
        public string Moneda { get; set; }
        public decimal ZonaHoraria { get; set; }
        public int EstadoPublicacion { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
        public int? CodigoGoogleId { get; set; }
        public string CodigoPaisMoodle { get; set; }
        public string RutaBandera { get; set; }

        private readonly integraDBContext _integraDBContext;

        public PaisBO()
        {
            _integraDBContext = new integraDBContext();
        }

        public List<PaisFiltroParaComboDTO> ObtenerListaPais()
        {
            PaisRepositorio _repObjeto = new PaisRepositorio(_integraDBContext);

            var _objetoRegistro = _repObjeto.ObtenerPaisFiltro();

            return _objetoRegistro;
        }

    }
}
