using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Servicios.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BSI.Integra.Test.TestObjetos
{
    public class CategoriaProgramaShould
    {

        public readonly CategoriaProgramaBO objeto;
        CategoriaProgramaController _controlador;
        ValidadorCategoriaProgramaDTO _validadorObjeto;

        public CategoriaProgramaShould()
        {
            objeto = new CategoriaProgramaBO()
            {
                Categoria = "cat pro",
                Visible = false
            };
            _validadorObjeto = new ValidadorCategoriaProgramaDTO();

        }

        // Nombre
        [Fact]
        public void validarNombre_NotEmpty()
        {
            Assert.NotEmpty(objeto.Categoria);
        }

        [Fact]
        public void validarNombre_NotNull()
        {
            Assert.NotNull(objeto.Categoria);
        }

        [Fact]
        public void validarNombre_LenghtBetween1and100()
        {
            Assert.InRange(objeto.Categoria.Length, 1, 100);
        }

        //Visible
        //[Fact]
        //public void validarVisible_NotEmpty()
        //{
        //    Assert.NotEmpty(objeto.Visible.ToString());
        //}

        //[Fact]
        //public void validarVisible_NotNull()
        //{
        //    Assert.NotNull(objeto.Visible);
        //}

        [Fact]
        public void validarVisible_TypeBool()
        {
            Assert.IsType<bool>(objeto.Visible);
        }

    }
}
