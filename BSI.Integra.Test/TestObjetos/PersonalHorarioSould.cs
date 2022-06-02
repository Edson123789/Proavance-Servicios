using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Servicios.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BSI.Integra.Test.TestObjetos
{
    public class PersonalHorarioSould
    {
        public readonly PersonalHorarioBO objeto;

        public PersonalHorarioSould()
        {
            objeto = new PersonalHorarioBO()
            {
                IdPersonal = 12
            };
        }

        //IdPersonal
        [Fact]
        public void validarIdPersona_ValorNoCero()
        {
            Assert.NotEqual(0, objeto.IdPersonal);
        }
    }
}
