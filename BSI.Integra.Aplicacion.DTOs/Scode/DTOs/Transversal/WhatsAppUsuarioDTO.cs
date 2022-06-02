namespace BSI.Integra.Aplicacion.DTOs
{
    public class WhatsAppUsuarioDTO
    {
        public int IdPersonal { get; set; }
        public int IdPais { get; set; }
        public string UserUsername { get; set; }
        public string UserPassword { get; set; }
        public string ExpiresAfter { get; set; }
    }

    public class WhatsAppDatoUsuarioDTO
    {
        public int Id { get; set; }
        public int IdPersonal { get; set; }
        public string RolUser { get; set; }
        public string UserUsername { get; set; }
        public string UserPassword { get; set; }
        public string UsuarioSistema { get; set; }
    }

    // Creacion de objeto para su recepcion de la respuesta en JSON
    public class userLogeo
    {
        public users[] users { get; set; }
        public meta meta { get; set; }
        public errors[] errors { get; set; }
    }

    public class users
    {
        public string username { get; set; }
        public string token { get; set; }
        public string expires_after { get; set; }
    }

    public class userRegister
    {
        public string username { get; set; }
        public string password { get; set; }
    }

}
