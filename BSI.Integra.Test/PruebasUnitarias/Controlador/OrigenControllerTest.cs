using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Test.Fixtures;
using Moq;
using Newtonsoft.Json;
//using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using NUnit;
using NUnit.Framework;

//using NUnit.Framework;

namespace BSI.Integra.Test.PruebasUnitarias.Controlador
{
	[TestFixture]
	public class OrigenControllerTest
	{
		private readonly integraDBContext _integraDBContext;
		private readonly TestContextServicios _sut;
		private OrigenRepositorio _repOrigen;
		//private readonly ITestOutputHelper _output;
		//public TestContext TestContext { get; set; }

		public OrigenControllerTest()
		{
			
			_integraDBContext = new integraDBContext();
			_repOrigen = new OrigenRepositorio(_integraDBContext);

			IList<OrigenBO> origen = new List<OrigenBO>
			{
				new OrigenBO
				{
					Nombre = "Origen prueba",
					Descripcion = "Descripción origen prueba",
					Prioridad = 1,
					IdCategoriaOrigen = 5,
					UsuarioCreacion = "Prueba",
					UsuarioModificacion = "Prueba",
					FechaCreacion = DateTime.Now,
					FechaModificacion = DateTime.Now,
					Estado = true
				},
				new OrigenBO
				{
					Nombre = "Origen prueba 2",
					Descripcion = "Descripción origen prueba 2",
					Prioridad = 1,
					IdCategoriaOrigen = 5,
					UsuarioCreacion = "Prueba",
					UsuarioModificacion = "Prueba",
					FechaCreacion = DateTime.Now,
					FechaModificacion = DateTime.Now,
					Estado = true
				},
				new OrigenBO
				{
					Nombre = "Origen prueba 3",
					Descripcion = "Descripción origen prueba 3",
					Prioridad = 1,
					IdCategoriaOrigen = 5,
					UsuarioCreacion = "Prueba",
					UsuarioModificacion = "Prueba",
					FechaCreacion = DateTime.Now,
					FechaModificacion = DateTime.Now,
					Estado = true
				},
			};	
	
			_sut = new TestContextServicios();
			Mock<IOrigen> mockRepositorioOrigen = new Mock<IOrigen>();
			mockRepositorioOrigen.Setup(mr => mr.FirstById(It.IsAny<int>())).Returns((int i) => origen.Where(x => x.Id == i).Single());

			//mock_repOrigen.Setup(mr => (bool)mr.Insert(It.IsAny<OrigenBO>())).Returns(
			//	(OrigenBO target) =>
			//	{
   //                 DateTime now = DateTime.Now;
			//		if (target.Id.AreEquals(default(int)))
			//		{
			//			target.Nombre = "Origen prueba insertar";
			//			target.Descripcion = "Descripción origen prueba insertar";
			//			target.Prioridad = 1;
			//			target.IdTipodato = 1;
			//			target.IdCategoriaOrigen = 5;
			//			target.UsuarioCreacion = "Prueba";
			//			target.UsuarioModificacion = "Prueba";
			//			target.FechaCreacion = DateTime.Now;
			//			target.FechaModificacion = DateTime.Now;
			//			target.Estado = true;
			//			Origen.Add(target);
			//		}
			//		else
			//		{
			//			var original = Origen.Where(
			//				q => q.Id == target.Id).Single();

			//			if (original == null)
			//			{
			//				return false;
			//			}

			//			original.Nombre = target.Nombre;
			//			original.Descripcion = target.Descripcion;
			//			original.Prioridad = target.Prioridad;
			//			original.IdTipodato = target.IdTipodato;
			//			original.IdCategoriaOrigen = target.IdCategoriaOrigen;
			//			original.UsuarioCreacion = target.UsuarioCreacion;
			//			original.UsuarioModificacion = target.UsuarioModificacion;
			//			original.FechaCreacion = target.FechaCreacion;
			//			original.FechaModificacion = target.FechaModificacion;
			//			original.Estado = target.Estado;
			//		}
			//		return true;
			//	});
		}

		

		//[Test]
		//public void UpdateTest()
		//{
		//	// Find a product by id
		//	OrigenBO origenTest = this._repOrigen.FirstById(903);
		//	DateTime time = origenTest.FechaModificacion;

		//	// Change one of its properties
		//	origenTest.Nombre = "Prueba Update";
		//	origenTest.FechaModificacion = DateTime.Now;
		//	origenTest.UsuarioModificacion = "TestUpdate";

		//	// Save our changes.
		//	this._repOrigen.Update(origenTest);

		//	// Verify the change
		//	Assert.AreNotEqual(time, this._repOrigen.FirstById(903).FechaModificacion);
		//}

		//[Test]
		//public void DeleteTest()
		//{
		//	OrigenBO origenTest = this._repOrigen.FirstById(903);
		//	var rpta = this._repOrigen.Delete(origenTest.Id, "TestDelete");
		//	Assert.True(rpta);
		//}
	}

}
