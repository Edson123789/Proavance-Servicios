using BSI.Integra.Aplicacion.Marketing.BO;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BSI.Integra.Test.TestObjetos
{
    public class FormularioIncompletoProcesadoShould
    {
        public readonly FormularioIncompletoProcesadoBO objeto;

        public FormularioIncompletoProcesadoShould()
        {
            objeto = new FormularioIncompletoProcesadoBO()
            {
                IdContacto = 0,
                Nombre1 = "Jorge",
                Nombre2 = "Wilson",
                ApellidoPaterno = "Medina",
                ApellidoMaterno = "Lozano",
                Telefono= "0000000",
                Celular = "976033809",
                Email = "Jw.medina71@gmail.com",
                IdCentroCosto = 2208,
                NombrePrograma = "Curso Selección de Parámetros y Eficiencia en la  Perforación y Voladura de Rocas",
                IdTipoDato = 1,
                IdOrigen = 1,
                IdFaseOportunidad = 1,
                IdAreaFormacion = 1,
                IdAreaTrabajo = 2,
                IdIndustria = 2,
                IdCargo = 2,
                IdPais =51,
                IdCiudad =6,
                Validado = true,
                Corregido = true,
                OrigenCampania = "Portal Web",
                IdCampania = 2,
                IdCategoriaOrigen=2,
                IdAsignacionAutomaticaOrigen =2,
                IdCampaniaScoring=2,
                FechaProgramada = DateTime.Now,
                FechaRegistroCampania = DateTime.Now,
                IdFaseOportunidadPortalTemp = 2,
                IdOportunidad =2,
                IdPersonal =2,
                //TiempoCapacitacion= 2,
                IdCategoriaDato =2,
                IdTipoInteraccion=2,
                IdSubCategoriaDato = 2,
                IdInteraccionFormulario=2,
                UrlOrigen = "",
                IdPagina =2
            };
        }

        //Id Contacto
        [Fact]
        public void validarIdContacto_NotNull()
        {
            Assert.NotNull(objeto.IdContacto);
        }
        //Nombre 1
        [Fact]
        public void validarNombre1_NotNull()
        {
            Assert.NotNull(objeto.Nombre1);
        }
        [Fact]
        public void validarNombre1_NotEmpty()
        {
            Assert.NotEmpty(objeto.Nombre1);
        }
        //Nombre 2
        [Fact]
        public void validarNombre2_NotNull()
        {
            Assert.NotNull(objeto.Nombre2);
        }
        [Fact]
        public void validarNombre2_NotEmpty()
        {
            Assert.NotEmpty(objeto.Nombre2);
        }
        //Apellido Paterno
        [Fact]
        public void validarApePaterno_NotNull()
        {
            Assert.NotNull(objeto.ApellidoPaterno);
        }
        [Fact]
        public void validarApePaterno_NotEmpty()
        {
            Assert.NotEmpty(objeto.ApellidoPaterno);
        }
        //Apellido Materno
        [Fact]
        public void validarApeMaterno_NotNull()
        {
            Assert.NotNull(objeto.ApellidoMaterno);
        }
        [Fact]
        public void validarApeMaterno_NotEmpty()
        {
            Assert.NotEmpty(objeto.ApellidoMaterno);
        }
        //Telefono
        [Fact]
        public void validarTelefono_NotNull()
        {
            Assert.NotNull(objeto.Telefono);
        }
        [Fact]
        public void validarTelefono_NotEmpty()
        {
            Assert.NotEmpty(objeto.Telefono);
        }
        //Celular
        [Fact]
        public void validarCelular_NotNull()
        {
            Assert.NotNull(objeto.Celular);
        }
        [Fact]
        public void validarCelular_NotEmpty()
        {
            Assert.NotEmpty(objeto.Celular);
        }
        //Email
        [Fact]
        public void validarEmail_NotNull()
        {
            Assert.NotNull(objeto.Email);
        }
        [Fact]
        public void validarEmail_NotEmpty()
        {
            Assert.NotEmpty(objeto.Email);
        }
        //Id Centro Costo
        [Fact]
        public void validarIdCentroCosto_NotNull()
        {
            Assert.NotNull(objeto.IdCentroCosto);
        }
        //Nombre Programa
        [Fact]
        public void validarNombrePrograma_NotNull()
        {
            Assert.NotNull(objeto.NombrePrograma);
        }
        [Fact]
        public void validarNombrePrograma_NotEmpty()
        {
            Assert.NotEmpty(objeto.NombrePrograma);
        }
        //Id Tipo Dato
        [Fact]
        public void validarIdTipoDato_NotNull()
        {
            Assert.NotNull(objeto.IdTipoDato);
        }
        //Id Origen
        [Fact]
        public void validarIdOrigen_NotNull()
        {
            Assert.NotNull(objeto.IdOrigen);
        }
        //Id Fase Oportunidad
        [Fact]
        public void validarIdFaseOportunidad_NotNull()
        {
            Assert.NotNull(objeto.IdFaseOportunidad);
        }
        //Id Area Formacion
        [Fact]
        public void validarIdAreaFormacion_NotNull()
        {
            Assert.NotNull(objeto.IdAreaFormacion);
        }
        //Id Area Trabajo
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
        //Id Cargo
        [Fact]
        public void validarIdCargo_NotNull()
        {
            Assert.NotNull(objeto.IdCargo);
        }
        //Id Pais
        [Fact]
        public void validarIdPais_NotNull()
        {
            Assert.NotNull(objeto.IdPais);
        }
        //Id Ciudad
        [Fact]
        public void validarIdCiudad_NotNull()
        {
            Assert.NotNull(objeto.IdCiudad);
        }
        //validado
        [Fact]
        public void validarValidado_NotNull()
        {
            Assert.NotNull(objeto.Validado);
        }
        //corregido
        [Fact]
        public void validarCorregido_NotNull()
        {
            Assert.NotNull(objeto.Corregido);
        }
        //Origen Compania
        [Fact]
        public void validarOrigenCampania_NotNull()
        {
            Assert.NotNull(objeto.OrigenCampania);
        }
        [Fact]
        public void validarOrigenCampania_NotEmpty()
        {
            Assert.NotEmpty(objeto.OrigenCampania);
        }
        //Id Campania
        [Fact]
        public void validarIdCampania_NotNull()
        {
            Assert.NotNull(objeto.IdCampania);
        }
        //Id Categoria Origen
        [Fact]
        public void validarIdCategoriaOrigen_NotNull()
        {
            Assert.NotNull(objeto.IdCategoriaOrigen);
        }
        //IdAsignacionAutomaticaOrigen
        [Fact]
        public void validarIdAsignacionAutomaticaOrigen_NotNull()
        {
            Assert.NotNull(objeto.IdAsignacionAutomaticaOrigen);
        }
        //IdCampaniaScoring
        [Fact]
        public void validarIdCampaniaScoring_NotNull()
        {
            Assert.NotNull(objeto.IdCampaniaScoring);
        }
        //FechaProgramada
        [Fact]
        public void validarFechaProgramada_NotNull()
        {
            Assert.NotNull(objeto.FechaProgramada);
        }
        //FechaRegistroCampania
        [Fact]
        public void validarFechaRegistroCampania_NotNull()
        {
            Assert.NotNull(objeto.FechaRegistroCampania);
        }
        //IdFaseOportunidadPortalTemp
        [Fact]
        public void validarIdFaseOportunidadPortalTemp_NotNull()
        {
            Assert.NotNull(objeto.IdFaseOportunidadPortalTemp);
        }
        //IdOportunidad
        [Fact]
        public void validarIdOportunidad_NotNull()
        {
            Assert.NotNull(objeto.IdOportunidad);
        }
        //IdPersonal
        [Fact]
        public void validarIdPersonal_NotNull()
        {
            Assert.NotNull(objeto.IdPersonal);
        }
        //TiempoCapacitacion
        [Fact]
        public void validarTiempoCapacitacion_NotNull()
        {
            //Assert.NotNull(objeto.TiempoCapacitacion);
        }
        //IdCategoriaDato
        [Fact]
        public void validarIdCategoriaDato_NotNull()
        {
            Assert.NotNull(objeto.IdCategoriaDato);
        }
        //IdTipoInteraccion
        [Fact]
        public void validarIdTipoInteraccion_NotNull()
        {
            Assert.NotNull(objeto.IdTipoInteraccion);
        }
        //IdSubCategoriaDato
        [Fact]
        public void validarIdSubCategoriaDato_NotNull()
        {
            Assert.NotNull(objeto.IdSubCategoriaDato);
        }
        //IdInteraccionFormulario
        [Fact]
        public void validarIdInteraccionFormulario_NotNull()
        {
            Assert.NotNull(objeto.IdInteraccionFormulario);
        }
        //UrlOrigen
        [Fact]
        public void validarUrlOrigen_NotNull()
        {
            Assert.NotNull(objeto.UrlOrigen);
        }
        //IdPagina
        [Fact]
        public void validarIdPagina_NotNull()
        {
            Assert.NotNull(objeto.IdPagina);
        }


    }
}
