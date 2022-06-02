using BSI.Integra.Aplicacion.Planificacion.BO;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BSI.Integra.Test.TestObjetos
{
    public class AmbienteSould
    {
        public readonly AmbienteBO objeto;

        public AmbienteSould()
        {
            objeto = new AmbienteBO();            

        }

        //Nombre
        [Fact]
        public void validarNombre_NotEmpty()
        {
            objeto.Nombre = "Nombre de Envio";

            Assert.NotEmpty(objeto.Nombre);
        }

        [Fact]
        public void validarNombre_SubStringAssert()
        {
            objeto.Nombre = "Nombre";

            Assert.Contains("bre", objeto.Nombre);
        }

        [Fact]
        public void validarNombre_SoloLetrasAssert()
        {
            objeto.Nombre = "Nombre";

            Assert.Matches(@"^[a-zA-Z]+$", objeto.Nombre);
        }

        //Virtual
        [Fact]
        public void validarVirtual_EsVerdaderoAssert()
        {
            objeto.Virtual = true;

            Assert.True(objeto.Virtual, "El Valor debe ser Verdadero");
        }

        [Fact]
        public void validarVirtual_NoVerdaderoAssert()
        {
            objeto.Virtual = false;

            Assert.False(objeto.Virtual, "El Valor debe ser Falso");
        }
        // IdLocacion
        [Fact]
        public void validarIdLocacion_ValorNoCeroAssert()
        {
            objeto.IdLocacion = 1;

            Assert.NotEqual(0, objeto.IdLocacion);
        }

        // IdTipoAmbiente
        [Fact]
        public void validarIdTipoAmbiente_ValorNoCeroAssert()
        {
            objeto.IdTipoAmbiente = 1;

            Assert.NotEqual(0, objeto.IdTipoAmbiente);
        }

        //Capacidad
        [Fact]
        public void validarCapacidad_ValorNoCeroAssert()
        {
            objeto.Capacidad = 1;

            Assert.NotEqual(0, objeto.Capacidad);
        }
    }
}
