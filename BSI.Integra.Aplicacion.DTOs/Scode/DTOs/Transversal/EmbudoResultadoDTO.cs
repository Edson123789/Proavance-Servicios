using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class EmbudoResultadoDTO
    {
        public string Llave { get; set; }
        public int IdEmbudoNivel { get; set; }
        public string NombreEmbudoNivel { get; set; }
        public int IdEmbudoSubNivel { get; set; }
        public string NombreEmbudoSubNivel { get; set; }
        //public int IdTipoCategoriaOrigen { get; set; }
        //public string NombreTipoCategoriaOrigen { get; set; }
        public int Inicial { get; set; }
        public string Entran { get; set; }
        public string Salen { get; set; }
        public int Nuevos { get; set; }
        public int Final { get; set; }
    }

    public class EmbudoDetalleResultadoDTO
    {
        public string Llave { get; set; }
        public int IdEmbudoNivel { get; set; }
        public int IdEmbudoSubNivel { get; set; }
        public int IdTipoCategoriaOrigen { get; set; }
        public string NombreTipoCategoriaOrigen { get; set; }
        public int Cantidad { get; set; }
    }
    public class EmbudoResultadoAgrupadoDTO
    {
        public string Llave { get; set; }
        public int IdEmbudoNivel { get; set; }
        public string NombreEmbudoNivel { get; set; }
        public int IdEmbudoSubNivel { get; set; }
        public string NombreEmbudoSubNivel { get; set; }
        public int IdTipoCategoriaOrigen { get; set; }
        public string NombreTipoCategoriaOrigen { get; set; }
        public int InicialTipoCategoriaOrigen { get; set; }
        public int EntranTipoCategoriaOrigen { get; set; }
        public int SalenTipoCategoriaOrigen { get; set; }
        public int FinalTipoCategoriaOrigen { get; set; }
        public int InicialSubNivel { get; set; }
        public int EntranSubNivel { get; set; }
        public int SalenSubNivel { get; set; }
        public int FinalSubNivel { get; set; }
        public int InicialNivel { get; set; }
        public int EntranNivel { get; set; }
        public int SalenNivel { get; set; }
        public int FinalNivel { get; set; }
    }
}
