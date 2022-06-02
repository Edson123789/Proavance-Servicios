using BSI.Integra.Aplicacion.Maestros.BO;
using BSI.Integra.Servicios.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BSI.Integra.Test.TestObjetos
{
    public class AreaFormacionShould
    {
        public readonly AreaFormacionBO objeto;
        AreaFormacionController _controlador;
        ValidadorAreaFormacionDTO _validadorObjeto;

        public AreaFormacionShould()
        {
            objeto = new AreaFormacionBO()
            {
                Nombre ="Energia"
            };
            _validadorObjeto = new ValidadorAreaFormacionDTO();
        }

        [Fact]
        public void validarNombre_NotEmpty()
        {
            //objeto.Nombre = "Area1";
            Assert.NotEmpty(objeto.Nombre);
        }

        [Fact]
        public void ValidarNombre_NotNull()
        {
            //objeto.Nombre = "Area1";
            Assert.NotNull(objeto.Nombre);
        }

        [Fact]
        public void validarNombre_LenghtBetween1and100()
        {
            //objeto.Nombre = "";//tamaño 0
            //objeto.Nombre = "";//tamaño 1
            //objeto.Nombre = "Curabitur ullamcorper ultricies nisi. Nam eget dui. Etiam rhoncus. Maecenas tempus, tellus eget condimentum rhoncus, sem quam semper libero, sit amet adipiscing sem neque sed ipsum. Nam quam nunc, blandit vel, luctus pulvinar, hendrerit id, lorem. Maecenas nec odio et ante tincidunt tempus. Donec vitae sapien ut libero venenatis faucibus. Nullam quis ante. Etiam sit amet orci eget eros faucibus tincidunt. Duis leo. Sed fringilla mauris sit amet nibh. Donec sodales sagittis magna. Sed consequat, leo eget bibendum sodales, augue velit cursus nunc,";//tamaño mayor a 100
            //objeto.Nombre = "Energia";//tamaño adecuado
            Assert.InRange(objeto.Nombre.Length,1,100);
        }

        [Fact]
        public void validarNombre_SoloLetrasAssert()
        {
            //objeto.Nombre = "Enegia";
            Assert.Matches(@"^[a-zA-Z]+$", objeto.Nombre);
        }

        //[Fact]
        //public void validarNombre_SubStringAssert()
        //{
        //    objeto.Nombre = "Nombre";
        //    Assert.Contains("bre", objeto.Nombre);
        //}
    }
}