using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class OcurrenciaPorActividadPadreDTO
    {
        public int  Id { get; set; } 
        public string  Nombre { get; set; } 
        public int  IdOcurrencia { get; set; } 
        public int  IdActividadCabecera { get; set; } 
        public int  IdOcurrenciaActividad_Padre { get; set; } 
        public bool  NodoPadre { get; set; } 
        public byte[]  RowVersion { get; set; } 
        public bool  Estado { get; set; } 
        public DateTime  FechaCreacion { get; set; } 
        public DateTime  FechaModificacion { get; set; } 
        public string  UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
    }
}
