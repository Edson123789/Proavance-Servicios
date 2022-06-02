using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Servicios.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BSI.Integra.Test.TestObjetos
{
    public class TipoPagoShould
    {
        public readonly TipoPagoBO objeto;
        TipoPagoController _controlador;
        ValidadorTipoPagoDTO _validadorObjeto;

        public TipoPagoShould()
        {
            objeto = new TipoPagoBO()
            {
                Nombre = "Plan Mensual",
                Descripcion = "Plan Mensual",
                Cuotas = 3,
                Suscripciones = "",
                PorDefecto = ""
            };
            _validadorObjeto = new ValidadorTipoPagoDTO();

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

    }
}
