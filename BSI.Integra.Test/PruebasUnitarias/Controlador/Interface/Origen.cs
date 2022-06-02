using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Test.PruebasUnitarias.Controlador
{
	public interface IOrigen
	{
		OrigenBO FirstById(int id);
		bool Insert(OrigenBO origen);
		bool Update(OrigenBO origen);
		bool Delete(int id, string nombreUsuario);

	}
	public class Origen 
	{
		public OrigenBO FirstById(int id)
		{
			OrigenRepositorio repOrigen = new OrigenRepositorio();
			return repOrigen.FirstById(id);
		}

		public bool Insert(OrigenBO origen)
		{ 
 			OrigenRepositorio repOrigen = new OrigenRepositorio();
			return repOrigen.Insert(origen);
		}

		public bool Update(OrigenBO origen)
		{
			OrigenRepositorio repOrigen = new OrigenRepositorio();
			return repOrigen.Update(origen);
		}
		public bool Delete(int id, string nombreUsuario)
		{
			OrigenRepositorio repOrigen = new OrigenRepositorio();
			return repOrigen.Delete(id, nombreUsuario);
		}

	}
}
