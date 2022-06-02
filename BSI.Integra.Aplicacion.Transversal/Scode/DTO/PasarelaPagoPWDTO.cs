using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.DTO
{
    public class PasarelaPagoPWDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdProveedor { get; set; }
        public int IdPais { get; set; }
        public int Prioridad { get; set; }
        public string Usuario { get; set; }
    }

    public class RegistroPasarelaPagoPWDTO
    {
        public int Id { get; set; }
        public int IdProveedor { get; set; }
        public string RazonSocial { get; set; }
        public string Nombre { get; set; }
        public int IdPais { get; set; }
        public string NombrePais { get; set; }
        public int Prioridad { get; set; }
    }

    public class MedioPagoMatriculaCronogramaDTO
    {
        public int Id { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public int IdMedioPago { get; set; }
        public bool Activo { get; set; }
        public string UsuarioCreacion { get; set; }
    }

    public class RegistroMedioPagoMatriculaCronogramaDTO
    {
        public int Id { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public int IdMedioPago { get; set; }
        public bool Activo { get; set; }
        public string Usuario { get; set; }
    }
}
