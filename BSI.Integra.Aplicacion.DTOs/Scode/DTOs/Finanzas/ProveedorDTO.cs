using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ProveedorDTO
    {
        public int Id { get; set; }
        public int IdTipoContribuyente { get; set; }
        public string TipoContribuyente { get; set; }
        public int IdDocumentoIdentidad { get; set; }
        public string DocumentoIdentidad { get; set; }
        public string NroDocumento { get; set; }
        public string Proveedor { get; set; }
        public string RazonSocial { get; set; }
        public string ApePaterno { get; set; }
        public string ApeMaterno { get; set; }
        public string Nombre1 { get; set; }
        public string Nombre2 { get; set; }
        public string Descripcion { get; set; }
        public string Direccion { get; set; }
        public int? IdPais { get; set; }
        public string Pais { get; set; }
        public int? IdCiudad { get; set; }
        public string Ciudad { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Celular1 { get; set; }
        public string Celular2 { get; set; }
        public string Contacto1 { get; set; }
        public string Contacto2 { get; set; }
        public string Alias { get; set; }
        public int? IdPrestacionRegistro { get; set; }
        public int? Criterio1 { get; set; }
        public int? Criterio2 { get; set; }
        public int? Criterio3 { get; set; }
        public int? Criterio4 { get; set; }
        public int? Criterio5 { get; set; }
        public string FechaModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public int? IdImpuesto { get; set; }
        public int? IdRetencion { get; set; }
        public int? IdDetraccion { get; set; }
        public int? IdPersonalAsignado { get; set; }
        public List<ProveedorTipoServicioDTO> ListaProveedorTipoServicio { get; set; }
        public ProveedorDTO(){
            ListaProveedorTipoServicio = new List<ProveedorTipoServicioDTO>();
        }
    }
}
