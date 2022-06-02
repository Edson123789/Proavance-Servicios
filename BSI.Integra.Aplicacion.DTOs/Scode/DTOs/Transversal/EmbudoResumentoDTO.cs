using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class EmbudoResumentoDTO
    {
        public List<EmbudoNivel> ListaNivel { get; set; }
    }

    public class EmbudoNivel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<EmbudoSubNivel> ListaSubNiveles { get; set; }
        public int Inicial { get; set; }
        public string Entran { get; set; }
        public string Salen { get; set; }
        public int Nuevos { get; set; }
        public int Final { get; set; }
    }
    public class EmbudoSubNivel{
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<EmbudoSubSubNivel> ListaSubSubNivel { get; set; }
        public int Inicial { get; set; }
        public string Entran { get; set; }
        public string Salen { get; set; }
        public int Nuevos { get; set; }
        public int Final { get; set; }
    }
    public class EmbudoSubSubNivel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
    }
}
