using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Classes;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.SCode.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{

    public class CategoriaOrigenBO : BaseBO
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdTipoDato { get; set; }
        public int IdTipoCategoriaOrigen { get; set; }
        public int Meta { get; set; }
        public int? IdProveedorCampaniaIntegra { get; set; }
        public int? IdFormularioProcedencia { get; set; }
        public bool Considerar { get; set; }
        public string CodigoOrigen { get; set; }

        private CategoriaOrigenRepositorio _repCategoriaOrigen;

        public CategoriaOrigenBO()
        {
            _repCategoriaOrigen = new CategoriaOrigenRepositorio();
        }  
        /// <summary>
        /// Obtiene todas las categoria origen para filtro
        /// </summary>
        /// <returns></returns>
        public List<CategoriaOrigenFiltroDTO> ObtenerTodoFiltro()
        {
            return _repCategoriaOrigen.ObtenerCategoriaFiltro();
        }
        
        /// <summary>
        /// Obtener todos los datos de todas las categorias origen con estado 1
        /// </summary>
        /// <returns></returns>
        public List<CategoriaOrigenDTO> ObtenerTodo()
        {
            return _repCategoriaOrigen.ObtenerTodo();
        }

        /// <summary>
        /// Obtiene el id, nombre de un origen filtrado por nombre
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        public List<CategoriaOrigenFiltroDTO> ObtenerCategoriaOrigenPorNombre(string nombre)
        {
            return _repCategoriaOrigen.ObtenerCategoriaOrigenPorNombre(nombre);
        }
    }
    
}
