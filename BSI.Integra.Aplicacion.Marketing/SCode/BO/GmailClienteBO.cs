using BSI.Integra.Aplicacion.Classes;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using BSI.Integra.Persistencia.SCode.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    ///BO: GmailClienteBO
    ///Autor: Edgar S.
    ///Fecha: 27/01/2021
    ///<summary>
    ///Columnas tabla T_GmailCliente
    ///</summary>
    public class GmailClienteBO : BaseBO
    {
        ///Propiedades		    Significado
        ///-------------	    -----------------------
        ///IdAsesor             Fk de T_Personal
        ///EmailAsesor		    Email de Asesor
        ///PasswordCorreo       Password de Correo
        ///NombreAsesor         Nombre de Asesor
        ///IdClient             Id de Cliente
        ///ClientSecret         Clave de Correo
        ///AliasEmailAsesor     Alisa de Email Asesor
        ///IdMigracion          Id Migración
        public int? IdAsesor { get; set; }
        public string EmailAsesor { get; set; }
        public string PasswordCorreo { get; set; }
        public string NombreAsesor { get; set; }
        public string IdClient { get; set; }
        public string ClientSecret { get; set; }
        public string AliasEmailAsesor { get; set; }
        public Guid? IdMigracion { get; set; }

        public GmailClienteBO()
        {
            ActualesErrores = new Dictionary<string, List<Base.Classes.ErrorInfo>>();
        }
    }
}
