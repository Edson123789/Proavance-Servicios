using BSI.Integra.Aplicacion.Marketing.BO;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BSI.Integra.Test.TestObjetos
{
    public class FormularioIncompletoPreprocesadoShould
    {
        public readonly FormularioIncompletoPreprocesadoBO objeto;

        public FormularioIncompletoPreprocesadoShould()
        {
            objeto = new FormularioIncompletoPreprocesadoBO()
            {
                Nombres = "Andrea",
                Apellidos = "Roblexo",
                Correo = "Info@andrearobledo.com",
                Fijo = "1132305077",
                Movil = "1132305077",
                IdPais =21,
                IdRegion = 12,
                IdAreaFormacion = 12,
                IdCargo= 12,
                IdAreaTrabajo = 12,
                IdIndustria =12,
                NombrePrograma = "Curso de Preparacion para la Certificación Certified Quality Engineer - CQE ",
                IdCentroCosto = 8716,
                //CentroCosto = "",
                IdTipoDato = 12,
                IdFaseVenta = 12,
                Origen = "Facebook",
                Procesado = false,
                IdCampania = 12,
                IdFaseOportunidadPortalTemp = 12,
                FechaRegistroCampania = DateTime.Now,
                IdCategoriaDato = 12,
                IdTipoInteraccion = 12,
                IdInteraccionFormulario = 12,
                UrlOrigen = "Origenprueba",
                IdPagina = 12
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
        //Apellidos
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
        //Correo
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
        //Fijo
        [Fact]
        public void validarFijo_NotEmpty()
        {
            Assert.NotEmpty(objeto.Fijo);
        }

        [Fact]
        public void validarFijo_NotNull()
        {
            Assert.NotNull(objeto.Fijo);
        }
        //Movil
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

        //Id Pais
        [Fact]
        public void validarIdPais_NotNull()
        {
            Assert.NotNull(objeto.IdPais);
        }
        //Id Region
        [Fact]
        public void validarIdRegion_NotNull()
        {
            Assert.NotNull(objeto.IdRegion);
        }
        //Id Area Formacion
        [Fact]
        public void validarIdAreaFormacion_NotNull()
        {
            Assert.NotNull(objeto.IdAreaFormacion);
        }
        //Id Cargo
        [Fact]
        public void validarIdCargo_NotNull()
        {
            Assert.NotNull(objeto.IdCargo);
        }
        //Id area trabajo
        [Fact]
        public void validarIdAreaTrabajo_NotNull()
        {
            Assert.NotNull(objeto.IdAreaTrabajo);
        }
        //Id Industria
        [Fact]
        public void validarIdIndustria_NotNull()
        {
            Assert.NotNull(objeto.IdIndustria);
        }
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
        [Fact]
        public void validarIdCentroCosto_NotNull()
        {
            Assert.NotNull(objeto.IdCentroCosto);
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
            Assert.NotNull(objeto.IdFaseVenta);
        }
        //Origen
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
        //Procesado
        [Fact]
        public void validarProcesado_NotNull()
        {
            Assert.NotNull(objeto.Procesado);
        }
        //Id Campania
        [Fact]
        public void validarIdCampania_NotNull()
        {
            Assert.NotNull(objeto.IdCampania);
        }
        //Id Fase Oportunidad Portal Temp
        [Fact]
        public void validarIdFaseOportunidadPortalTemp_NotNull()
        {
            Assert.NotNull(objeto.IdFaseOportunidadPortalTemp);
        }
        //Fecha Registro Campania
        [Fact]
        public void validarFechaRegistroCampania_NotNull()
        {
            Assert.NotNull(objeto.FechaRegistroCampania);
        }
        //Id Categoria Dato
        [Fact]
        public void validarIdCategoriaDato_NotNull()
        {
            Assert.NotNull(objeto.IdCategoriaDato);
        }
        //Id Tipo Interaccion
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
