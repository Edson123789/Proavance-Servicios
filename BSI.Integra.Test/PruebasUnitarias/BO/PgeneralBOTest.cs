
using BSI.Integra.Aplicacion.Comercial.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BSI.Integra.Test.PruebasUnitarias
{
    [TestFixture]
    public class PgeneralBOTest
    {
        [Test]
        public void GenerarProgramaConUrlPortal()
        {
            // Arrange
            var programa = new PgeneralBO();
            var descripcion = "Proyectos";
            var idBusqueda = 73;
            var titulo = "Programa-Internacional-en-Gerencia-de-Proyectos";
            var urlCorrecta = "/" + descripcion + "/" + titulo + "-" + idBusqueda;

            // Act
            var programasGenerales = programa.ObtenerProgramaConUrlPortal();
            var programaComparar = programasGenerales.Where(x=> x.IdBusqueda == idBusqueda).FirstOrDefault();
           
            // Assert
            Assert.AreEqual(programaComparar.Url, urlCorrecta);

        }

    }
}
