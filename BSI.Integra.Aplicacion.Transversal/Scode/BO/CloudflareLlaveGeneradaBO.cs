using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class CloudflareLlaveGeneradaBO : BaseBO
    {
        public int IdCloudflareUsuarioLlave { get; set; }
        public string JsonRespuesta { get; set; }
        public string KeyId { get; set; }
        public string KeyPem { get; set; }
        public string KeyJwk { get; set; }
        public string Created { get; set; }
        public bool Success { get; set; }
        public bool Valido { get; set; }
    }

    public class resultadoCloudflareLlaveGeneradaBO
    {
        public respuestaCloudflareLlaveGeneradaBO result { get; set; }
        public bool success { get; set; }
    }

    public class respuestaCloudflareLlaveGeneradaBO
    {
        public string id { get; set; }
        public string pem { get; set; }
        public string jwk { get; set; }
        public string created { get; set; }
    }

    public class listaCloudflareLlaveGeneradaBO
    {
        public int Id { get; set; }
        public int IdCloudflareUsuarioLlave { get; set; }
        public string JsonRespuesta { get; set; }
        public string KeyId { get; set; }
        public string KeyPem { get; set; }
        public string KeyJwk { get; set; }
        public string Created { get; set; }
        public bool Success { get; set; }
        public bool Valido { get; set; }
    }

    public class registroCloudflareLlaveGeneradaBO
    {
        public int Id { get; set; }
        public int IdCloudflareUsuarioLlave { get; set; }
        public string KeyId { get; set; }
        public string KeyPem { get; set; }
        public string Created { get; set; }
        public bool Valido { get; set; }
    }
}
