using BSI.Integra.Aplicacion.Maestros.BO;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Servicios.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BSI.Integra.Test.TestObjetos
{
    public class FaseOportunidadShould
    {

        public readonly FaseOportunidadBO objeto;
        FaseOportunidadController _controlador;
        ValidadorFaseOportunidadDTO _validadorObjeto;

        public FaseOportunidadShould()
        {
            objeto = new FaseOportunidadBO()
            {
                Codigo = "PF",
                Nombre = "Promesa de ficha",
                NroMinutos = 60
            };
            _validadorObjeto = new ValidadorFaseOportunidadDTO();

        }

        //Codigo FO
        [Fact]
        public void validarCodigoFO_NotEmpty()
        {
            Assert.NotEmpty(objeto.Codigo);
        }

        [Fact]
        public void validarCodigoFO_NotNull()
        {
            Assert.NotNull(objeto.Codigo);
        }

        [Fact]
        public void validarCodigoFO_LenghtBetween1and150()
        {
            Assert.InRange(objeto.Codigo.Length, 1, 150);
        }

        // NombreFO
        [Fact]
        public void validarNombreFO_NotEmpty()
        {
            Assert.NotEmpty(objeto.Nombre);
        }

        [Fact]
        public void validarNombreFO_NotNull()
        {
            Assert.NotNull(objeto.Nombre);
        }

        [Fact]
        public void validarNombreFO_LenghtBetween1and200()
        {
            Assert.InRange(objeto.Nombre.Length, 1, 200);
        }

        // Tiempo
        [Fact]
        public void validarNroMinutos_NotEmpty()
        {
            //Assert.NotEmpty( objeto.NroMinutos.ToString());
        }

        [Fact]
        public void validarNroMinutos_NotNull()
        {
            //Assert.NotNull(objeto.NroMinutos);
        }

        [Fact]
        public void validarNroMinutos_Mayor0()
        {
            Assert.True(objeto.NroMinutos > 0);
        }

    }
}
