
using BSI.Integra.Aplicacion.Comercial.BO;
using BSI.Integra.Aplicacion.Transversal.BO;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Test.UnitTests
{
    [TestFixture]
    public class CampaniaMailingDetalleTest
    {
        [Test]
        public void GenerarEtiquetas()
        {
            // Arrange
            var _etiqueta = new CampaniaMailingDetalleBO();
            var cadena = "<strong>Inversi&oacute;n al contado:</strong>&nbsp;*|PGCON_PP1|*</span></div> <div><strong>Inversi&oacute;n ;*|FNAME|*en cuotras:</strong>&nbsp;*|PGCRE_PP1|*</span></div> <p>&nbsp;*|PGCON_PP2|*</span></div> ;*|PGCRE_PP2|*</span></div> <p>&nbsp;</p> <strong>Inversi&oacute;n al contado:</strong>&nbsp;*|PGCON_PP3|*</span></div> <div><span style=font - size: 10pt; font - family: tahoma, arial, helvetica, sans - serif; >;*|PGCRE_PP3|*</span></div> <p>&nbsp;&nbsp;";

            // Act
            var etiqueta = _etiqueta.ObtenerEtiquetas(cadena);
            
            // Assert
            Assert.AreEqual(etiqueta[0], "PGCON_PP1");
            Assert.AreEqual(etiqueta[1], "PGCRE_PP1");
            Assert.AreEqual(etiqueta[2], "PGCON_PP2");
            Assert.AreEqual(etiqueta[3], "PGCRE_PP2");
            Assert.AreEqual(etiqueta[4], "PGCON_PP3");
            Assert.AreEqual(etiqueta[5], "PGCRE_PP3");
            
        }

        [Test]
        public void GenerarFiltroFecha()
        {
            // Arrange
            var _fecha = new CampaniaMailingDetalleBO();
            var cantidad = 2;
            var descripcion = "Dias";
            var descripcion1 = "Semanas";
            var descripcion2 = "Todos";
            var descripcion3 = "Pruebas";
            var _fecha2 = DateTime.Now;

            // Act
            var fecha = _fecha.GetFechaFiltro(cantidad, descripcion2);
            var fecha2 = _fecha.GetFechaFiltro(cantidad, descripcion1);
            var fecha3 = _fecha.GetFechaFiltro(cantidad, descripcion);
            var fecha4 = _fecha.GetFechaFiltro(cantidad, descripcion3);

            // Assert
            Assert.AreEqual(fecha.Day, _fecha2.Day);
            Assert.AreEqual(fecha.Month, _fecha2.Month);
            Assert.AreEqual(fecha.Year, _fecha2.Year-20);

            Assert.AreEqual(fecha2.Day, _fecha2.Day - (cantidad*7));
            Assert.AreEqual(fecha3.Day, _fecha2.Day - cantidad);
            Assert.AreEqual(fecha4.Day, _fecha2.Day);
        }
    }
}
