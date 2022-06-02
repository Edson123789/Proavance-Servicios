using BSI.Integra.Aplicacion.Comercial.BO;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BSI.Integra.Test.TestObjetos
{
    public class AsesorChatShould
    {
        public readonly AsesorChatBO objeto;

        public AsesorChatShould()
        {
            objeto = new AsesorChatBO()
            {
                IdPersonal = 1.ToString(),
                NombreAsesor = "Carmen cantoral"
            };

        } 

        //Id Asesor
        [Fact]
        public void validarIdAsesor_NotNull()
        {
            Assert.NotNull(objeto.IdPersonal);
        }

        //Nombre asesor
        [Fact]
        public void validarNombreAsesor_NotEmpty()
        {
            Assert.NotEmpty(objeto.NombreAsesor);
        }
        [Fact]
        public void validarNombreAsesor_NotNull()
        {
            Assert.NotNull(objeto.NombreAsesor);
        }
    }
}
