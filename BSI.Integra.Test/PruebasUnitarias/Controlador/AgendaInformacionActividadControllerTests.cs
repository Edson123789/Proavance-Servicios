using BSI.Integra.Test.Fixtures;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;


namespace BSI.Integra.Test.PruebasUnitarias.Controlador
{
    [TestFixture]
    public class AgendaInformacionActividadControllerTests
    {
        private readonly TestContextServicios _sut;

        public AgendaInformacionActividadControllerTests()
        {
            _sut = new TestContextServicios();
        }
        #region EnviarMensajeTexto
        [Test]
        public async Task EnviarMensajeTexto_TipoParametrosIncorrectos()
        {
            string IdOportunidad = "notInt";
            string IdAlumno = "notInt";
            string Usuario = "UsuarioPrueba";
      
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("IdOportunidad", IdOportunidad);
            data.Add("IdAlumno", IdAlumno);
            data.Add("Usuario", Usuario);
            string url = "api/AgendaInformacionActividad/EnviarMensajeTexto/";
            var response = await _sut.PostDiccionario(url, data);
            var content = await response.Content.ReadAsStringAsync();
        
            var status = response.StatusCode;
            Assert.That(content.Contains(IdOportunidad));
            Assert.AreEqual(status, HttpStatusCode.BadRequest);

        }
        [Test]
        public async Task EnviarMensajeTexto_ParametroIdAlumno_Invalido()
        {
            string IdOportunidad = "12";
            string IdAlumno = "0";
            string Usuario = "UsuarioPrueba";

            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("IdOportunidad", IdOportunidad);
            data.Add("IdAlumno", IdAlumno);
            data.Add("Usuario", Usuario);
            string url = "api/AgendaInformacionActividad/EnviarMensajeTexto/";
            var response = await _sut.PostDiccionario(url, data);
            var content = await response.Content.ReadAsStringAsync();
          
            var status = response.StatusCode;

            Assert.AreEqual(content, "No Existe Alumno con Identificador "+ IdAlumno);
            Assert.AreEqual(status, HttpStatusCode.BadRequest);


        }
        [Test]
        public async Task EnviarMensajeTexto_ParametroIdOportunidad_Invalido()
        {
            string IdOportunidad = "0";
            string IdAlumno = "20";
            string Usuario = "UsuarioPrueba";

            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("IdOportunidad", IdOportunidad);
            data.Add("IdAlumno", IdAlumno);
            data.Add("Usuario", Usuario);
            string url = "api/AgendaInformacionActividad/EnviarMensajeTexto/";
            var response = await _sut.PostDiccionario(url, data);
            var content = await response.Content.ReadAsStringAsync();
 
            var status = response.StatusCode;

            Assert.AreEqual(content, "No Existe un Codigo Matricula para la Oportunidad con Identificador " + IdOportunidad);
            Assert.AreEqual(status, HttpStatusCode.BadRequest);


        }
        #endregion
    }
}
