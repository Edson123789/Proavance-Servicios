using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Planificacion.BO;
using Xunit;

namespace BSI.Integra.Test.TestObjetos
{
    public class LocacionSould
    {
        public readonly LocacionBO objeto;

        public LocacionSould()
        {
            objeto = new LocacionBO()
            {
                Nombre = "Nombre",
                IdPais = 2,
                IdRegion = 3,
                IdCiudad = 3,
                Direccion = "Direccion"
            };
        }
        //Nombre
        [Fact]
        public void validarNombre_NotEmpty()
        {
            Assert.NotEmpty(objeto.Nombre);
        }

        [Fact]
        public void validadNombre_SubStringAssert()
        {
            Assert.Contains("omb", objeto.Nombre);
        }
        //Idpais
        [Fact]
        public void validarIdPais_ValorNoCeroAssert()
        {
            Assert.NotEqual(0, objeto.IdPais);
        }

        //IdRegion
        [Fact]
        public void validarIdRegion_ValorNoCeroAssert()
        {
            Assert.NotEqual(0, objeto.IdRegion);
        }

        //IdCiudad
        [Fact]
        public void validarIdCiudad_ValorNoCeroAssert()
        {
            Assert.NotEqual(0, objeto.IdCiudad);
        }

        //Direccion
        [Fact]
        public void validarDireccion_NotEmpty()
        {
            Assert.NotEmpty(objeto.Direccion);
        }

        [Fact]
        public void validarDireccion_SubStringAssert()
        {
            Assert.Contains("rec", objeto.Direccion);
        }


    }
}
