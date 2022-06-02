
using BSI.Integra.Aplicacion.Comercial.BO;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Aplicacion.Transversal.Repositorio;

namespace BSI.Integra.Test.PruebasUnitarias.BO
{
    [TestFixture]
    public class PersonalHorarioBOTest
    {
        private readonly integraDBContext _integraDBContext;        

        public PersonalHorarioBOTest()
        {
            _integraDBContext = new integraDBContext();
            
        }
        [Test]
        public void HorarioPorIdPersonalExisteConContexto()
        {
            // Arrange
            var personalHorario = new PersonalHorarioBO(_integraDBContext);
            personalHorario.IdPersonal = 29; 

            // Act
            var horario = personalHorario.GetHorarioAsTable();
            var prueba = horario.Count;

            // Assert
            Assert.AreNotEqual(0, prueba);

        }
        [Test]
        public void HorarioPorIdPersonalExisteSinContexto()
        {
            // Arrange
            var personalHorario = new PersonalHorarioBO();
            personalHorario.IdPersonal = 29;

            // Act
            var horario = personalHorario.GetHorarioAsTable();
            var prueba = horario.Count;

            // Assert
            Assert.AreNotEqual(0, prueba);

        }
        [Test]
        public void HorarioPorIdPersonalNoExiste()
        {
            //Arrange
            var personalHorario = new PersonalHorarioBO(_integraDBContext);
            personalHorario.IdPersonal = 1;

            //Act
            var error = Assert.Throws<Exception>(() => personalHorario.GetHorarioAsTable());

            //Assert
            Assert.AreEqual("No existe Horario para el PersonalId " + personalHorario.IdPersonal, error.Message);

            
        }

        [Test]
        public void HorarioPorIdPersonalSinIdPersonal()
        {
            //Arrange
            var personalHorario = new PersonalHorarioBO(_integraDBContext);

            //Act
            var error = Assert.Throws<Exception>(() => personalHorario.GetHorarioAsTable());
            var a = 1;
            //Assert
            Assert.AreEqual("No existe Horario para el PersonalId " + personalHorario.IdPersonal, error.Message);


        }

    }
}
