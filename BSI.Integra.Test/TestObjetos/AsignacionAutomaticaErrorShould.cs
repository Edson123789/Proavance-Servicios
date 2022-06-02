using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using BSI.Integra.Aplicacion.Transversal.BO;


namespace BSI.Integra.Test.TestObjetos
{
    public class AsignacionAutomaticaErrorShould
    {
        public readonly AsignacionAutomaticaErrorBO Objeto;

        public AsignacionAutomaticaErrorShould()
        {
            Objeto = new AsignacionAutomaticaErrorBO()
            {
                Campo = "email",
                Descripcion = "Email Ya existe",
                IdContacto = 73678,
                IdAsignacionAutomatica = 12,
                IdAsignacionAutomaticaTipoError = 12
            };
        }

        //campo
        [Fact]
        public void validarCampo_NotNull()
        {
            Assert.NotNull(Objeto.Campo);
        }

        [Fact]
        public void validarCampo_NotEmpty()
        {
            Assert.NotEmpty(Objeto.Campo);
        }
        //descripcion
        [Fact]
        public void validarDescripcion_NotNull()
        {
            Assert.NotNull(Objeto.Descripcion);
        }

        [Fact]
        public void validarDescripcion_NotEmpty()
        {
            Assert.NotEmpty(Objeto.Descripcion);
        }
        //IdContacto
        [Fact]
        public void validarIdContacto_NotNull()
        {
            Assert.NotNull(Objeto.IdContacto);
        }
        //IdAsignacionAutomatica
        [Fact]
        public void validarIdAsignacionAutomatica_NotNull()
        {
            Assert.NotNull(Objeto.IdAsignacionAutomatica);
        }
        //IdTipoError
        [Fact]
        public void validarIdTipoError_NotNull()
        {
            Assert.NotNull(Objeto.IdAsignacionAutomaticaTipoError);
        }
    }
}
