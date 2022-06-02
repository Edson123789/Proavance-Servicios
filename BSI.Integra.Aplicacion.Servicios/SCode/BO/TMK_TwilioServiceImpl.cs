using System;
using System.Collections.Generic;
using System.Text;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace BSI.Integra.Aplicacion.Servicios.BO
{
    public class TMK_TwilioServiceImpl
    {
        private const string _accountSid = "AC789efd819d71fc20cb7817c404b21794";
        private const string _authToken = "2ed88f13596c956bea8451d13e47b8ee";
        private const string _origenNumber = "+14327550058";
        public TMK_TwilioServiceImpl()
        {
            TwilioClient.Init(_accountSid, _authToken);
        }
        public string EnviarMensajeTexto(string mensaje, string numeroDestino)
        {
            try
            {
                var message = MessageResource.Create(body: mensaje,
                                                            from: new Twilio.Types.PhoneNumber(_origenNumber),
                                                            to: new Twilio.Types.PhoneNumber(numeroDestino),
                                                            pathAccountSid: _accountSid);
                return message.Sid;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }
    }
}
