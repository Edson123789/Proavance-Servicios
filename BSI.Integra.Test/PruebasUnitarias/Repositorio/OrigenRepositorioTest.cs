using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Newtonsoft.Json;
using BSI.Integra.Aplicacion.Transversal.BO;
using System.Linq;
using BSI.Integra.Test.Fixtures;

namespace BSI.Integra.Test.PruebasUnitarias.Repositorio
{
    [TestFixture]
    public class _repOrigenTest
    {
        private readonly TestContextServicios injection;
        private readonly integraDBContext _integraDBContext;
        private OrigenRepositorio _repOrigen;
        public _repOrigenTest()
        {
            _integraDBContext = new integraDBContext();
            _repOrigen = new OrigenRepositorio(_integraDBContext);
            injection = new TestContextServicios();
        }
        [Test]
        public void FirstByIdTest()
        {
            //Arrange
            int idOrigen = 797;

            //Act
            var origen = this._repOrigen.FirstById(idOrigen);

            //Assert
            Assert.NotNull(origen); 
            Assert.IsInstanceOf(typeof(OrigenBO), origen); 
            Assert.AreEqual("LANCOLFBK-DES1-RE", origen.Nombre);
        }

        [Test]
        public void InsertCorrecto()
        {

            //Arrange
            OrigenBO origenInsert = new OrigenBO
            {
                Nombre = "Origen prueba insertar..",
                Descripcion = "Descripción origen prueba insertar",
                Prioridad = 1,
                IdTipodato = 1,
                IdCategoriaOrigen = 5,
                UsuarioCreacion = "Prueba",
                UsuarioModificacion = "Prueba",
                FechaCreacion = DateTime.Now,
                FechaModificacion = DateTime.Now,
                Estado = true,
            };

            int cantidadOrigenInicial = this._repOrigen.GetAll().Count();

            //Act
            this._repOrigen.Insert(origenInsert);
            
            //Assert
            var origenContador = this._repOrigen.GetAll().Count();
            Assert.AreEqual(cantidadOrigenInicial + 1, origenContador); 

            
            OrigenBO testOrigen = _repOrigen.GetBy(x => x.Nombre.Contains("Origen prueba insertar..")).FirstOrDefault();
            Assert.NotNull(testOrigen); 
            Assert.IsInstanceOf(typeof(OrigenBO), testOrigen); 
                                                               
        }
        [Test]
        public void InsertIncorrecto()
        {

            //Arrange
            OrigenBO origenInsert = new OrigenBO
            {
                Nombre = "Origen prueba insertar..",
                Descripcion = "Descripción origen prueba insertar",
                Prioridad = 1,
                UsuarioCreacion = "Prueba",
                UsuarioModificacion = "Prueba",
                FechaModificacion = DateTime.Now,
                Estado = true,
            };

            int cantidadOrigenInicial = this._repOrigen.GetAll().Count();

            //Act
            
            var error = Assert.Throws<Exception>(() => this._repOrigen.Insert(origenInsert));

            //Assert
            Assert.IsNotNull(error.Message);
            
        }

        [Test]
        public void ObtenerTodoFiltroExiste()
        {
            

            //Act

            var filtroOrigen = _repOrigen.ObtenerTodoFiltro();

            //Assert

            //Assert.True(filtroOrigen.Count > 0);
            Assert.AreNotEqual(0,(int)filtroOrigen.Count);
        }

        [Test]
        public void ObtenerTodoFiltroCorrecto()
        {
            

            OrigenFiltroDTO origenFiltrocomparar = new OrigenFiltroDTO()
            {
                Id = 123,
                Nombre = "Visita oficina"
            };           

            var jsonComparar = JsonConvert.SerializeObject(origenFiltrocomparar);

            //Act
            var filtroOrigen = _repOrigen.ObtenerTodoFiltro();

            OrigenFiltroDTO filtroOrigenSeleccion = filtroOrigen.Find(w => w.Id == 123);

            //var a = origenFiltrocomparar.AreEquals((OrigenFiltroDTO)filtroOrigenSeleccion);

            //Assert
            var jsonfiltroSeleccion = JsonConvert.SerializeObject(filtroOrigenSeleccion);
            Assert.AreEqual(jsonComparar, jsonfiltroSeleccion);
        }

        //[Test]
        //public void ObtenerTodoFiltroError()
        //{
        //    //Arrange
        //    _repOrigen _repOrigen = new _repOrigen();
            
        //    //Act
        //    Action action = () => _repOrigen.ObtenerTodoFiltro();

        //    ArgumentException exception = Assert.Throws<ArgumentException>(action);

        //    //Assert
        //    Assert.NotEmpty(exception.Message);
        //}

        [Test]
        public void ObtenerIdCategoriaOrigenPorOrigenNoExiste()
        {
            //Arrange
           
            OrigenIdCategoriaOrigenDTO categoriaComparar = new OrigenIdCategoriaOrigenDTO()
            {
                Id = 0,
                IdCategoriaOrigen = 0
            };

            int idOrigen = 0;

            var jsonComparar = JsonConvert.SerializeObject(categoriaComparar);

            //Act

            var categoriaFiltrada =  _repOrigen.ObtenerIdCategoriaOrigenPorOrigen(idOrigen);

            //Assert
            var jsonFiltrada = JsonConvert.SerializeObject(categoriaFiltrada);
            Assert.IsNull(categoriaFiltrada);
        }

        [Test]
        public void ObtenerIdCategoriaOrigenPorOrigenExiste()
        {
            //Arrange
            

            OrigenIdCategoriaOrigenDTO categoriaComparar = new OrigenIdCategoriaOrigenDTO()
            {
                Id = 5,
                IdCategoriaOrigen = 187
            };

            int idOrigen = 5;

            var jsonComparar = JsonConvert.SerializeObject(categoriaComparar);
            
            //Act
            var categoriaFiltrada = _repOrigen.ObtenerIdCategoriaOrigenPorOrigen(idOrigen);

            //Assert         

            var jsonFiltrado = JsonConvert.SerializeObject(categoriaFiltrada);

            Assert.AreEqual(jsonComparar, jsonFiltrado);
        }

        //[Test]
        //public void ObtenerIdCategoriaOrigenPorOrigenError()
        //{
        //    //Arrange
        //    _repOrigen _repOrigen = new _repOrigen(_integraDBContext);

        //    int idOrigen = -1;

            
        //    //Act
        //    Action action = () => _repOrigen.ObtenerIdCategoriaOrigenPorOrigen(idOrigen);

        //    ArgumentException exception = Assert.Throws<ArgumentException>(action);

        //    //Assert
        //    Assert.NotEmpty(exception.Message);

        //}

    }
}
