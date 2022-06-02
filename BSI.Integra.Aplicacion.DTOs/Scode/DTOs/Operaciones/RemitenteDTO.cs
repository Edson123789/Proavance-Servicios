using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class RemitenteDTO
    {
        public string Nombre;
        public string Correo;
        public string AliasCorreo;
        public string Clave;
        public string Firma;

        public RemitenteDTO(string _nombre = "Mod Pru", string _correo = "modpru@bsginstitute.com", string _clave = "", string _firma = "")
        {
            Nombre = _nombre;
            Correo = _correo;
            AliasCorreo = _correo;
            Clave = _clave;
            Firma = _firma;
        }
    }
}
