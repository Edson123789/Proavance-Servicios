using BSI.Integra.Aplicacion.Maestros.BO;
using BSI.Integra.Servicios.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BSI.Integra.Test.TestObjetos
{
    public class CargoShould
    {
        public readonly CargoBO objeto;
        CargoController _controlador;
        ValidadorCargoDTO _validadorObjeto;

        public CargoShould() {
            objeto = new CargoBO()
            {
                Nombre = "Coordinador/Supervisor",
                Descripcion = "Coordinador/Supervisor"
            };
            _validadorObjeto = new ValidadorCargoDTO();

        }

        [Fact]
        public void validarNombre_NotEmpty()
        {
            Assert.NotEmpty(objeto.Nombre);
        }

        [Fact]
        public void ValidarNombre_NotNull()
        {
            Assert.NotNull(objeto.Nombre);
        }

        [Fact]
        public void validarNombre_LenghtBetween1and100()
        {
            Assert.InRange(objeto.Nombre.Length, 1, 100);
        }

        //[Fact]
        //public void validarNombre_SoloLetrasAssert()
        //{
        //    Assert.Matches(@"^[a-zA-Z]+$", objeto.Nombre);
        //}


        //Descripcion
        //[Fact]
        //public void validarDescripcion_NotEmpty()
        //{
        //    Assert.NotEmpty(objeto.Descripcion);
        //}

        //[Fact]
        //public void ValidarDescripcion_NotNull()
        //{
        //    Assert.NotNull(objeto.Descripcion);
        //}

        [Fact]
        public void validarDescripcion_MaxLenght50()
        {
            Assert.InRange(objeto.Descripcion.Length, 0, 50);
        }

        //[Fact]
        //public void validarDescripcion_SoloLetrasAssert()
        //{
        //    Assert.Matches(@"^[a-zA-Z]+$", objeto.Descripcion);
        //}
    }
}
