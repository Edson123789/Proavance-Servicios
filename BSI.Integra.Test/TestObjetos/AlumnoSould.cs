using BSI.Integra.Aplicacion.Transversal.BO;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BSI.Integra.Test.TestObjetos
{
    public class AlumnoSould
    {
        public readonly AlumnoBO objeto;

        public AlumnoSould()
        {
            objeto = new AlumnoBO();
        }

        //Nombre1
        [Fact]
        public void validarNombre1_NotEmpty()
        {
            objeto.Nombre1 = "Nombre";

            Assert.NotEmpty(objeto.Nombre1);
        }

        [Fact]
        public void validarNombre1_SubStringAssert()
        {
            objeto.Nombre1 = "Nombre";

            Assert.Contains("bre", objeto.Nombre1);
        }

        [Fact]
        public void validarNombre1_SoloLetrasAssert()
        {
            objeto.Nombre1 = "Nombre";

            Assert.Matches(@"^[a-zA-Z]+$", objeto.Nombre1);
        }

        //Nombre2
        [Fact]
        public void validarNombre2_NotEmpty()
        {
            objeto.Nombre2 = "Nombre";

            Assert.NotEmpty(objeto.Nombre2);
        }

        [Fact]
        public void validarNombre2_SubStringAssert()
        {
            objeto.Nombre2 = "Nombre";

            Assert.Contains("bre", objeto.Nombre2);
        }

        [Fact]
        public void validarNombre2_SoloLetrasAssert()
        {
            objeto.Nombre2 = "Nombre";

            Assert.Matches(@"^[a-zA-Z]+$", objeto.Nombre2);
        }

        //ApellidoPaterno
        [Fact]
        public void validarApepaterno_NotEmpty()
        {
            objeto.ApellidoPaterno = "Nombre";

            Assert.NotEmpty(objeto.ApellidoPaterno);
        }

        [Fact]
        public void validarApepaterno_SubStringAssert()
        {
            objeto.ApellidoPaterno = "Nombre";

            Assert.Contains("bre", objeto.ApellidoPaterno);
        }

        [Fact]
        public void validarApepaterno_SoloLetrasAssert()
        {
            objeto.ApellidoPaterno = "Nombre";

            Assert.Matches(@"^[a-zA-Z]+$", objeto.ApellidoPaterno);
        }

        //ApellidoMaterno
        [Fact]
        public void validarApematerno_NotEmpty()
        {
            objeto.ApellidoMaterno = "Nombre";

            Assert.NotEmpty(objeto.ApellidoMaterno);
        }

        [Fact]
        public void validarApematerno_SubStringAssert()
        {
            objeto.ApellidoMaterno = "Nombre";

            Assert.Contains("bre", objeto.ApellidoMaterno);
        }

        [Fact]
        public void validarApematerno_SoloLetrasAssert()
        {
            objeto.ApellidoMaterno = "Nombre";

            Assert.Matches(@"^[a-zA-Z]+$", objeto.ApellidoMaterno);
        }

        //CodigoPais
        [Fact]
        public void validarIdCodigoPais_ValorNoCeroAssert()
        {
            objeto.IdCodigoPais = 1;

            Assert.NotEqual(0, objeto.IdCodigoPais);
        }

        //CodigoCiudad
        [Fact]
        public void validarCodigoCiudad_ValorNoCeroAssert()
        {
            objeto.IdCiudad = 1;

            Assert.NotEqual(0, objeto.IdCiudad);
        }

    }
}
