using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using BSI.Integra.Aplicacion.Comercial.BO;

namespace BSI.Integra.Test.TestObjetos
{
    public class ActividadCabeceraSould
    {
        public readonly ActividadCabeceraBO objeto;

        public ActividadCabeceraSould()
        {
            objeto = new ActividadCabeceraBO()
            {
                Nombre = "Primer contacto cliente probabilidad media",
                Descripcion = "Primer contacto BNC media",
                DuracionEstimada = 12
            };
        }
        //Nombre
        [Fact]
        public void validarNombre_NotEmpty()
        {
            Assert.NotEmpty(objeto.Nombre);
        }

        [Fact]
        public void validarNombre_SubStringAssert()
        {
            Assert.Contains("imer", objeto.Nombre);
        }

        //[Fact]
        //public void validarNombre_SoloLetrasAssert()
        //{
        //    Assert.Matches(@"^[a-zA-Z]+$", objeto.Nombre);
        //}

        //Descripcion
        [Fact]
        public void validarDescripcion_NotEmpty()
        {
            Assert.NotEmpty(objeto.Descripcion);
        }

        [Fact]
        public void validarDescripcion_SubStringAssert()
        {
            Assert.Contains("imer", objeto.Descripcion);
        }

        //[Fact]
        //public void validarDescripcion_SoloLetrasAssert()
        //{
        //    Assert.Matches(@"^[a-zA-Z]+$", objeto.Descripcion);
        //}

        //DuracionEstimada
        [Fact]
        public void ValidarDuracionEstimada_ValorNoCeroAssert()
        {
            Assert.NotEqual(0, objeto.DuracionEstimada);
        }

        
    }
}
