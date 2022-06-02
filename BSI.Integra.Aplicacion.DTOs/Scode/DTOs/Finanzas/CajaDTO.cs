using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CajaDTO
    {
        public int Id { get; set; }
        public string CodigoCaja { get; set; }
        public int IdEmpresa { get; set; }
        public string Empresa { get; set; }
        public int IdBanco { get; set; }
        public string Banco { get; set; }
        public int IdCuenta { get; set; }
        public string Cuenta { get; set; }
        public int IdMoneda { get; set; }
        public string Moneda { get; set; }
        public int IdPais { get; set; }
        public string Pais { get; set; }
        public int IdCiudad { get; set; }
        public string Ciudad { get; set; }
        public int IdPersonal { get; set; }
        public string Personal { get; set; }
        public string UsuarioModificacion { get; set; }
        public bool Activo { get; set; }
    }
}
