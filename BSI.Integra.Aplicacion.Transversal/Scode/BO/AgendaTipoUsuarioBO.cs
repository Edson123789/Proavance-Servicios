using BSI.Integra.Aplicacion.Classes;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using BSI.Integra.Persistencia.SCode.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.Base.Classes;
using BSI.Integra.Aplicacion.DTOs;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class AgendaTipoUsuarioBO : BaseBO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

    }
    
}
