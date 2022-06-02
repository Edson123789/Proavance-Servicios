using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.Repository;
using Newtonsoft.Json;
using BSI.Integra.Aplicacion.Transversal.Validador;
using BSI.Integra.Aplicacion.Transversal.DTO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using System.Text.RegularExpressions;
using BSI.Integra.Aplicacion.Transversal.Tools;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal;
using System.Globalization;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.Helper;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class CentralLlamadaDireccionBO : BaseBO
    {
        
        public string Nombre { get; set; }
        public string DireccionIp { get; set; }        
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
