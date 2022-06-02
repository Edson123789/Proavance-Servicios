using BSI.Integra.Aplicacion.Comercial.BO;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BSI.Integra.Test.TestObjetos
{
    public class InteraccionChatIntegraShould
    {
        public readonly InteraccionChatIntegraBO Objeto;
        public InteraccionChatIntegraShould() {
            Objeto = new InteraccionChatIntegraBO()
            {
                IdChatIntegraHistorialAsesor = 112,
                IdAlumno = 200,
                IdTipoInteraccion = 12,
                IdPgeneral = 12,
                IdSubAreaCapacitacion = 12,
                IdAreaCapacitacion = 12,
                Ip = "192.168.0.1",
                Pais = "Ecuador",
                Region = "Guayas",
                Ciudad = "Cantón Guayaquil",
                Duracion = 1,
                NroMensajes = 12,
                NroPalabrasVisitor = 12,
                NroPalabrasAgente = 12,
                UsuarioTiempoRespuestaMaximo = 12,
                UsuarioTiempoRespuestaPromedio = 12,
                FechaInicio = DateTime.Now,
                FechaFin = DateTime.Now,
                Leido = false,
                Plataforma = "plataforma1",
                Navegador = "google chrome",
                UrlFrom = "http://m.facebook.com",
                UrlTo = "https://bsgrupo.com/ti/Microsoft-MCSA-SQL-Server-2014-236",
                Tipo= "Offline",
                IdConjuntoAnuncio = 12
                //IdChatSesion= 12,
                //IdFaseOportunidadPortal = 12
            };
        }

        //Id asesor
        [Fact]
        public void validarIdChatIntegraHistorialAsesor_NotNull()
        {
            Assert.NotNull(Objeto.IdChatIntegraHistorialAsesor);
        }
        //Id Alumno
        [Fact]
        public void validarIdAlumno_NotNull()
        {
            Assert.NotNull(Objeto.IdAlumno);
        }

        //IdCookie
        [Fact]
        public void validarIdCookie_NotNull()
        {
            Assert.NotNull(Objeto.IdContactoPortalSegmento);
        }
        //Id Tipo Interaccion
        [Fact]
        public void validarIdTipoInteraccion_NotNull()
        {
            Assert.NotNull(Objeto.IdTipoInteraccion);
        }
        //Id PG
        [Fact]
        public void validarIdPG_NotNull()
        {
            Assert.NotNull(Objeto.IdPgeneral);
        }
        //Id Sub Area
        [Fact]
        public void validarIdSubArea_NotNull()
        {
            Assert.NotNull(Objeto.IdSubAreaCapacitacion);
        }
        //Id Area
        [Fact]
        public void validarIdArea_NotNull()
        {
            Assert.NotNull(Objeto.IdAreaCapacitacion);
        }
        //Ip
        [Fact]
        public void ValidarIp_NotNull()
        {

            Assert.NotNull(Objeto.Ip);
        }

        [Fact]
        public void ValidarIp_NotEmpty()
        {

            Assert.NotEmpty(Objeto.Ip);
        }
        //Pais
        [Fact]
        public void ValidarPais_NotNull()
        {

            Assert.NotNull(Objeto.Pais);
        }

        [Fact]
        public void ValidarPais_NotEmpty()
        {
            Assert.NotEmpty(Objeto.Pais);
        }
        //Region
        [Fact]
        public void ValidarRegion_NotNull()
        {

            Assert.NotNull(Objeto.Region);
        }

        [Fact]
        public void ValidarRegion_NotEmpty()
        {

            Assert.NotEmpty(Objeto.Region);
        }
        //Ciudad
        [Fact]
        public void ValidarCiudad_NotNull()
        {
            Assert.NotNull(Objeto.Ciudad);
        }

        [Fact]
        public void ValidarCiudad_NotEmpty()
        {
            Assert.NotEmpty(Objeto.Ciudad);
        }
        //Duracion
        [Fact]
        public void validarDuracion_NotNull()
        {
            Assert.NotNull(Objeto.Duracion);
        }

        //NMensajes
        [Fact]
        public void validarNMensajes_NotNull()
        {
            Assert.NotNull(Objeto.NroMensajes);
        }
        //NPalabrasVisitor
        [Fact]
        public void validarNPalabrasVisitor_NotNull()
        {
            Assert.NotNull(Objeto.NroPalabrasVisitor);
        }
        //NPalabrasAgente
        [Fact]
        public void validarNPalabrasAgente_NotNull()
        {
            Assert.NotNull(Objeto.NroPalabrasAgente);
        }
        //Usuario tiempo respuesta maximo
        [Fact]
        public void validarUsuarioTiempoRespuestaMaximo_NotNull()
        {
            Assert.NotNull(Objeto.UsuarioTiempoRespuestaMaximo);
        }
        //Usuario tiempo respuesta promedio
        [Fact]
        public void validarUsuarioTiempoRespuestaPromedio_NotNull()
        {
            Assert.NotNull(Objeto.UsuarioTiempoRespuestaPromedio);
        }
        //Inicio
        [Fact]
        public void validarInicio_NotNull()
        {
            Assert.NotNull(Objeto.FechaInicio);
        }
        //Fin
        [Fact]
        public void validarFin_NotNull()
        {
            Assert.NotNull(Objeto.FechaFin);
        }
        //Leido
        [Fact]
        public void validarLeido_NotNull()
        {
            Assert.NotNull(Objeto.Leido);
        }
        //Plataforma
        [Fact]
        public void ValidarPlataforma_NotNull()
        {
            Assert.NotNull(Objeto.Plataforma);
        }

        [Fact]
        public void ValidarPlataforma_NotEmpty()
        {
            Assert.NotEmpty(Objeto.Plataforma);
        }
        //Navegador
        [Fact]
        public void ValidarNavegador_NotNull()
        {
            Assert.NotNull(Objeto.Navegador);
        }

        [Fact]
        public void ValidarNavegador_NotEmpty()
        {
            Assert.NotEmpty(Objeto.Navegador);
        }
        //urlFrom
        [Fact]
        public void ValidarUrlFrom_NotNull()
        {
            Assert.NotNull(Objeto.UrlFrom);
        }

        [Fact]
        public void ValidarUrlFrom_NotEmpty()
        {
            Assert.NotEmpty(Objeto.UrlFrom);
        }
        //UrlTo
        [Fact]
        public void ValidarUrlTo_NotNull()
        {
            Assert.NotNull(Objeto.UrlTo);
        }

        [Fact]
        public void ValidarUrlTo_NotEmpty()
        {
            Assert.NotEmpty(Objeto.UrlTo);
        }
        //Tipo
        [Fact]
        public void ValidarTipo_NotNull()
        {
            Assert.NotNull(Objeto.Tipo);
        }

        [Fact]
        public void ValidarTipo_NotEmpty()
        {
            Assert.NotEmpty(Objeto.Tipo);
        }
        //Id Campania
        [Fact]
        public void validarIdCampania_NotNull()
        {
            Assert.NotNull(Objeto.IdConjuntoAnuncio);
        }
        //Id Chat Sesion
        [Fact]
        public void validarIdChatSesion_NotNull()
        {
            Assert.NotNull(Objeto.IdChatSession);
        }
        //Id Fase Oportunidad
        [Fact]
        public void validarIdFaseOportunidad_NotNull()
        {
            Assert.NotNull(Objeto.IdFaseOportunidadPortalWeb);
        }
    }
}
