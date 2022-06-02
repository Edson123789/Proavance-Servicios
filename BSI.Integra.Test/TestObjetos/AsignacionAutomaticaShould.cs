using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Servicios.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BSI.Integra.Test.TestObjetos
{
    public class AsignacionAutomaticaShould
    {
        public readonly AsignacionAutomaticaBO objeto;

        AsignacionAutomaticaController _controlador;
        ValidadorAsignacionAutomaticaDTO _validaObjeto;

        public AsignacionAutomaticaShould()
        {
            objeto = new AsignacionAutomaticaBO()
            {

                Nombre1 = "Nombre",
                Nombre2 = "Nombre2",
                ApellidoPaterno = "ApePaterno",
                ApellidoMaterno = "ApeMaterno"
            };
            _validaObjeto = new ValidadorAsignacionAutomaticaDTO();
        }
        
        //Nombre 1 
        [Fact]
        public void validarNombre1_NotEmpty()
        {
            Assert.NotEmpty(objeto.Nombre1);
        }
        //Nombre 2
        [Fact]
        public void validarNombre2_NotEmpty()
        {
            Assert.NotEmpty(objeto.Nombre2);
        }

        //apellido paterno
        [Fact]
        public void validarApellidoPaterno_NotEmpty()
        {
            Assert.NotEmpty(objeto.ApellidoPaterno);
        }

        //apellido materno
        [Fact]
        public void validarApellidoMaterno_NotEmpty()
        {
            Assert.NotEmpty(objeto.ApellidoMaterno);
        }

        //Insertar
        [Fact]
        public void validarInsertarAsignacionAutomatica()
        {

        }


    }
}
