using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class TipoTemaProgramaCapacitacionRepositorio : BaseRepository<TTipoTemaProgramaCapacitacion, TipoTemaProgramaCapacitacionBO>
    {
        #region Metodos Base
        public TipoTemaProgramaCapacitacionRepositorio() : base()
        {
        }
        public TipoTemaProgramaCapacitacionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<TipoTemaProgramaCapacitacionBO> GetBy(Expression<Func<TTipoTemaProgramaCapacitacion, bool>> filter)
        {
            IEnumerable<TTipoTemaProgramaCapacitacion> listado = base.GetBy(filter);
            List<TipoTemaProgramaCapacitacionBO> listadoBO = new List<TipoTemaProgramaCapacitacionBO>();
            foreach (var itemEntidad in listado)
            {
                TipoTemaProgramaCapacitacionBO objetoBO = Mapper.Map<TTipoTemaProgramaCapacitacion, TipoTemaProgramaCapacitacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public TipoTemaProgramaCapacitacionBO FirstById(int id)
        {
            try
            {
                TTipoTemaProgramaCapacitacion entidad = base.FirstById(id);
                TipoTemaProgramaCapacitacionBO objetoBO = new TipoTemaProgramaCapacitacionBO();
                Mapper.Map<TTipoTemaProgramaCapacitacion, TipoTemaProgramaCapacitacionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public TipoTemaProgramaCapacitacionBO FirstBy(Expression<Func<TTipoTemaProgramaCapacitacion, bool>> filter)
        {
            try
            {
                TTipoTemaProgramaCapacitacion entidad = base.FirstBy(filter);
                TipoTemaProgramaCapacitacionBO objetoBO = Mapper.Map<TTipoTemaProgramaCapacitacion, TipoTemaProgramaCapacitacionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(TipoTemaProgramaCapacitacionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TTipoTemaProgramaCapacitacion entidad = MapeoEntidad(objetoBO);

                bool resultado = base.Insert(entidad);
                if (resultado)
                    AsignacionId(entidad, objetoBO);

                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(IEnumerable<TipoTemaProgramaCapacitacionBO> listadoBO)
        {
            try
            {
                foreach (var objetoBO in listadoBO)
                {
                    bool resultado = Insert(objetoBO);
                    if (resultado == false)
                        return false;
                }

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Update(TipoTemaProgramaCapacitacionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TTipoTemaProgramaCapacitacion entidad = MapeoEntidad(objetoBO);

                bool resultado = base.Update(entidad);
                if (resultado)
                    AsignacionId(entidad, objetoBO);

                return resultado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Update(IEnumerable<TipoTemaProgramaCapacitacionBO> listadoBO)
        {
            try
            {
                foreach (var objetoBO in listadoBO)
                {
                    bool resultado = Update(objetoBO);
                    if (resultado == false)
                        return false;
                }

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        private void AsignacionId(TTipoTemaProgramaCapacitacion entidad, TipoTemaProgramaCapacitacionBO objetoBO)
        {
            try
            {
                if (entidad != null && objetoBO != null)
                {
                    objetoBO.Id = entidad.Id;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private TTipoTemaProgramaCapacitacion MapeoEntidad(TipoTemaProgramaCapacitacionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TTipoTemaProgramaCapacitacion entidad = new TTipoTemaProgramaCapacitacion();
                entidad = Mapper.Map<TipoTemaProgramaCapacitacionBO, TTipoTemaProgramaCapacitacion>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion
        
		/// <summary>
        /// Obtiene los TipoTemaProgramaCapacitacion [Id, Nombre] (Usado Para ComboBox)
        /// </summary>
        /// <returns></returns>
        public List<TipoTemaProgramaCapacitacionDTO> ObtenerTodoTipoTemaProgramaCapacitacion()
        {
            try
            {
                List<TipoTemaProgramaCapacitacionDTO> TipoTemaProgramaCapacitaciones = new List<TipoTemaProgramaCapacitacionDTO>();
                var _query = string.Empty;
                _query = "Select  Id,  Nombre  FROM pla.T_TipoTemaProgramaCapacitacion WHERE  Estado = 1";
                var _resultado = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(_resultado) && !_resultado.Contains("[]"))
                {
                    TipoTemaProgramaCapacitaciones = JsonConvert.DeserializeObject<List<TipoTemaProgramaCapacitacionDTO>>(_resultado);
                }
                return TipoTemaProgramaCapacitaciones;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }


}
