using BSI.Integra.Aplicacion.Classes;
using BSI.Integra.Persistencia.SCode.Repository;
using BSI.Integra.Persistencia.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.DTOs;
using System.Linq;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public class PrioridadMailChimpListaCorreoBO : BaseBO
    {
        public int IdPrioridadMailChimpLista { get; set; }
        public int IdAlumno { get; set; }
        public string Email1 { get; set; }
        public string Nombre1 { get; set; }
        public string ApellidoPaterno { get; set; }
        public int IdCodigoPais
        {
            get
            {
                return _idCodigoPais;
            }
            set
            {
                if (value == 51 || value == 57)
                {
                    _idCodigoPais = value;
                }
                _idCodigoPais = 0;
            }
        }
        public int IdCampaniaMailing { get; set; }
        public bool? EsSubidoCorrectamente { get; set; }
        public string ObjetoSerializado { get; set; }
        public string EstadoSuscripcionMailChimp { get; set; }
        public int? IdCampaniaGeneral { get; set; }

        private int _idCodigoPais;

        public PrioridadMailChimpListaCorreoBO()
        {

        }
        public string ObtenerEtiqueta(string Etiqueta, List<CampaniasMailingPrecioDTO> credito, List<CampaniasMailingPrecioDTO> contado)
        {
            string rpta = "-";

            if (Etiqueta.Contains("PGCON_"))
            {//contado
                rpta = GetPrecioContadoContactoPrograma(Etiqueta, contado);
            }
            if (Etiqueta.Contains("PGCRE_"))
            {//credito
                rpta = GetPrecioCreditoContactoPrograma(Etiqueta, credito);
            }

            return rpta;
        }
        public string ObtenerEtiquetaGeneral(string Etiqueta, List<CampaniasGeneralPrecioDTO> credito, List<CampaniasGeneralPrecioDTO> contado)
        {
            string rpta = "-";

            if (Etiqueta.Contains("PGCON_"))
            {//contado
                rpta = GetPrecioContadoContactoProgramaGeneral(Etiqueta, contado);
            }
            if (Etiqueta.Contains("PGCRE_"))
            {//credito
                rpta = GetPrecioCreditoContactoProgramaGeneral(Etiqueta, credito);
            }

            return rpta;
        }
        private string GetPrecioContadoContactoPrograma(string Etiqueta, List<CampaniasMailingPrecioDTO> PreciosContado)
        {
            string precio = " ";
            var data = PreciosContado.Where(x => x.EtiquetaPrecio.Equals(Etiqueta) && x.CodigoPais == this.IdCodigoPais).OrderByDescending(x => x.Version).FirstOrDefault();
            if (data != null)
            {
                precio = data.Inversion;
            }

            return precio;
        }
        private string GetPrecioContadoContactoProgramaGeneral(string Etiqueta, List<CampaniasGeneralPrecioDTO> PreciosContado)
        {
            string precio = " ";
            var data = PreciosContado.Where(x => x.EtiquetaPrecio.Equals(Etiqueta) && x.CodigoPais == this.IdCodigoPais).OrderByDescending(x => x.Version).FirstOrDefault();
            if (data != null)
            {
                precio = data.Inversion;
            }

            return precio;
        }
        private string GetPrecioCreditoContactoPrograma(string Etiqueta, List<CampaniasMailingPrecioDTO> PreciosCredito)
        {
            string precio = " ";
            var data = PreciosCredito.Where(x => x.EtiquetaPrecio.Equals(Etiqueta) && x.CodigoPais == this.IdCodigoPais).OrderByDescending(x => x.Version).FirstOrDefault();
            if (data != null)
            {
                precio = data.Inversion;
            }

            return precio;
        }

        private string GetPrecioCreditoContactoProgramaGeneral(string Etiqueta, List<CampaniasGeneralPrecioDTO> PreciosCredito)
        {
            string precio = " ";
            var data = PreciosCredito.Where(x => x.EtiquetaPrecio.Equals(Etiqueta) && x.CodigoPais == this.IdCodigoPais).OrderByDescending(x => x.Version).FirstOrDefault();
            if (data != null)
            {
                precio = data.Inversion;
            }

            return precio;
        }

    }
}
