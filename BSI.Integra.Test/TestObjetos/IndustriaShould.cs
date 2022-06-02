using BSI.Integra.Aplicacion.Maestros.BO;
using BSI.Integra.Servicios.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BSI.Integra.Test.TestObjetos
{
    public class IndustriaShould
    {
        public readonly IndustriaBO objeto;
        IndustriaController _controlador;
        ValidadorIndustriaDTO _validadorObjeto;

        public IndustriaShould()
        {
            objeto = new IndustriaBO()
            {
                Nombre = "Editorial/Medios",
                Descripcion = "Ninguna"
            };
            _validadorObjeto = new ValidadorIndustriaDTO();

        }

        //Nombre
        [Fact]
        public void validarNombre_NotEmpty()
        {
            Assert.NotEmpty(objeto.Nombre);
        }

        [Fact]
        public void validarNombre_NotNull()
        {
            Assert.NotNull(objeto.Nombre);
        }

        [Fact]
        public void validarNombre_LenghtBetween1and100()
        {
            Assert.InRange(objeto.Nombre.Length, 1, 100);
        }

        // Descripcion
        [Fact]
        public void validarDescripcion_NotEmpty()
        {
            Assert.NotEmpty(objeto.Descripcion);
        }

        [Fact]
        public void validarDescripcion_NotNull()
        {
            Assert.NotNull(objeto.Descripcion);
        }

        [Fact]
        public void validarDescripcion_MaxLenght200()
        {
            Assert.InRange(objeto.Descripcion.Length, 1, 200);
        }
    }
}
