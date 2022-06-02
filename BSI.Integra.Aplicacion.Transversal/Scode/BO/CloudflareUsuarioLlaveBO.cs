using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class CloudflareUsuarioLlaveBO : BaseBO
    {
        public string AuthKey { get; set; }
        public string AuthEmail { get; set; }
        public string AccountId { get; set; }
        public int? IdPersonal { get; set; }
        public bool Activar { get; set; }
    }

    public class listaCloudflareUsuarioLlaveBO
    {
        public int Id { get; set; }
        public string AuthKey { get; set; }
        public string AuthEmail { get; set; }
        public string AccountId { get; set; }
        public int? IdPersonal { get; set; }
        public bool Activar { get; set; }
    }

    public class registroCloudflareUsuarioLlaveBO
    {
        public int Id { get; set; }
        public string AuthKey { get; set; }
        public string AuthEmail { get; set; }
        public string AccountId { get; set; }
        public int? IdPersonal { get; set; }
    }

    public class registroVideoCloudflareConfigurado
    {
        public string VideoId { get; set; }
        public string TotalMinutos { get; set; }
        public int? IdPGeneral { get; set; }
    }
}
