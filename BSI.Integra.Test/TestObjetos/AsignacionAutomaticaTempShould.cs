using BSI.Integra.Aplicacion.Marketing.BO;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BSI.Integra.Test.TestObjetos
{
    public class AsignacionAutomaticaTempShould
    {
        public readonly AsignacionAutomaticaTempBO objeto;

        public AsignacionAutomaticaTempShould()
        {
            objeto = new AsignacionAutomaticaTempBO()
            {
                Nombres = "Luis Guillermo",
                Apellidos= "Postigo",
                Correo= "luis_guillermo016@hotmail.com",
                Fijo= "054222262",
                Movil= "959236586",
                IdPais = 51,
                IdCiudad = 4,
                IdAreaFormacion=12,
                IdCargo =12,
                IdAreaTrabajo = 12,
                IdIndustria = 12,
                NombrePrograma = "Diplomado Gerencia de Proyectos",
                IdCentroCosto = 2406,
                CentroCosto = "DIN GPROYECTOS 2016 III AQP",
                IdTipoDato = 12,
                IdFaseOportunidad = 12,
                Origen = "Facebook",
                Procesado = true,
                IdConjuntoAnuncio = 12,
                IdFaseOportunidadPortal = new Guid(),
                FechaRegistroCampania = DateTime.Now,
                IdTiempoCapacitacion = 12,
                IdCategoriaDato = 12,
                IdTipoInteraccion = 12,
                IdInteraccionFormulario = 12,
                UrlOrigen = "http://instagram.com/",
                IdPagina = 1
            };
        }

        //Nombres
        [Fact]
        public void validarNombres_NotEmpty()
        {
            Assert.NotEmpty(objeto.Nombres);
        }
        [Fact]
        public void validarNombres_NotNull()
        {
            Assert.NotNull(objeto.Nombres);
        }

        //apellidos
        [Fact]
        public void validarApellidos_NotEmpty()
        {
            Assert.NotEmpty(objeto.Apellidos);
        }
        [Fact]
        public void validarApellidos_NotNull()
        {
            Assert.NotNull(objeto.Apellidos);
        }
        //correo
        [Fact]
        public void validarCorreo_NotEmpty()
        {
            Assert.NotEmpty(objeto.Correo);
        }
        [Fact]
        public void validarCorreo_NotNull()
        {
            Assert.NotNull(objeto.Correo);
        }
        //fijo
        [Fact]
        public void validarFijo_NotEmpty()
        {
            Assert.NotEmpty(objeto.Fijo);
        }
        [Fact]
        public void validarFijoNotNull()
        {
            Assert.NotNull(objeto.Fijo);
        }
        //movil
        [Fact]
        public void validarMovil_NotEmpty()
        {
            Assert.NotEmpty(objeto.Movil);
        }
        [Fact]
        public void validarMovil_NotNull()
        {
            Assert.NotNull(objeto.Movil);
        }
        //Id pais
        //Id Region
        //Id Area Formacion
        //Id Cargo
        //Id Area Trabajo
        //Id Industria
        //Nombre Programa
        [Fact]
        public void validarNombrePrograma_NotEmpty()
        {
            Assert.NotEmpty(objeto.NombrePrograma);
        }
        [Fact]
        public void validarNombrePrograma_NotNull()
        {
            Assert.NotNull(objeto.NombrePrograma);
        }
        //Id Centro Costo
        //Centro Costo
        [Fact]
        public void validarCentroCosto_NotEmpty()
        {
            Assert.NotEmpty(objeto.CentroCosto);
        }
        [Fact]
        public void validarCentroCosto_NotNull()
        {
            Assert.NotNull(objeto.CentroCosto);
        }
        //Id Tipo Dato
        [Fact]
        public void validarIdTipoDato_NotNull()
        {
            Assert.NotNull(objeto.IdTipoDato);
        }
        //Id Fase Venta
        [Fact]
        public void validarIdFaseVenta_NotNull()
        {
            Assert.NotNull(objeto.IdFaseOportunidad);
        }
        //origen
        [Fact]
        public void validarOrigen_NotEmpty()
        {
            Assert.NotEmpty(objeto.Origen);
        }
        [Fact]
        public void validarOrigen_NotNull()
        {
            Assert.NotNull(objeto.Origen);
        }
        //procesado
        [Fact]
        public void validarProcesado_NotNull()
        {
            Assert.NotNull(objeto.Procesado);
        }
        //Id campania
        [Fact]
        public void validarIdCampania_NotNull()
        {
            Assert.NotNull(objeto.IdConjuntoAnuncio);
        }
        //Id Fase Oportunidad Portal
        [Fact]
        public void validarIdFaseOportunidadPortal_NotNull()
        {
            Assert.NotNull(objeto.IdFaseOportunidadPortal);
        }
        //Fecha Registro Compania
        [Fact]
        public void validarFechaRegistroCampaniaNotNull()
        {
            Assert.NotNull(objeto.FechaRegistroCampania);
        }
        //Id Tiempo Capacitacion}
        [Fact]
        public void validarIdTiempoCapacitacion_NotNull()
        {
            Assert.NotNull(objeto.IdTiempoCapacitacion);
        }
        //Id Categoria Dato
        [Fact]
        public void validarIdCategoriaDato_NotNull()
        {
            Assert.NotNull(objeto.IdCategoriaDato);
        }
        //Id tipo interaccion
        [Fact]
        public void validarIdTipoInteraccion_NotNull()
        {
            Assert.NotNull(objeto.IdTipoInteraccion);
        }
        //Id Interaccion Formulario
        [Fact]
        public void validarIdInteraccionFormulario_NotNull()
        {
            Assert.NotNull(objeto.IdInteraccionFormulario);
        }
        //Url Origen
        [Fact]
        public void validarUrlOrigen_NotEmpty()
        {
            Assert.NotEmpty(objeto.UrlOrigen);
        }
        [Fact]
        public void validarUrlOrigen_NotNull()
        {
            Assert.NotNull(objeto.UrlOrigen);
        }

        //Id pagina
        [Fact]
        public void validarIdPagina_NotNull()
        {
            Assert.NotNull(objeto.IdPagina);
        }
    }
}
