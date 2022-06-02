using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using BSI.Integra.Aplicacion.DTOs;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Planificacion.Repositorio
{
    public class PerfilContactoProgramaColumnaRepositorio : BaseRepository<TPerfilContactoProgramaColumna, PerfilContactoProgramaColumnaBO>
    {
        #region Metodos Base
        public PerfilContactoProgramaColumnaRepositorio() : base()
        {
        }
        public PerfilContactoProgramaColumnaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PerfilContactoProgramaColumnaBO> GetBy(Expression<Func<TPerfilContactoProgramaColumna, bool>> filter)
        {
            IEnumerable<TPerfilContactoProgramaColumna> listado = base.GetBy(filter);
            List<PerfilContactoProgramaColumnaBO> listadoBO = new List<PerfilContactoProgramaColumnaBO>();
            foreach (var itemEntidad in listado)
            {
                PerfilContactoProgramaColumnaBO objetoBO = Mapper.Map<TPerfilContactoProgramaColumna, PerfilContactoProgramaColumnaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PerfilContactoProgramaColumnaBO FirstById(int id)
        {
            try
            {
                TPerfilContactoProgramaColumna entidad = base.FirstById(id);
                PerfilContactoProgramaColumnaBO objetoBO = new PerfilContactoProgramaColumnaBO();
                Mapper.Map<TPerfilContactoProgramaColumna, PerfilContactoProgramaColumnaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PerfilContactoProgramaColumnaBO FirstBy(Expression<Func<TPerfilContactoProgramaColumna, bool>> filter)
        {
            try
            {
                TPerfilContactoProgramaColumna entidad = base.FirstBy(filter);
                PerfilContactoProgramaColumnaBO objetoBO = Mapper.Map<TPerfilContactoProgramaColumna, PerfilContactoProgramaColumnaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PerfilContactoProgramaColumnaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPerfilContactoProgramaColumna entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PerfilContactoProgramaColumnaBO> listadoBO)
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

        public bool Update(PerfilContactoProgramaColumnaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPerfilContactoProgramaColumna entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PerfilContactoProgramaColumnaBO> listadoBO)
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
        private void AsignacionId(TPerfilContactoProgramaColumna entidad, PerfilContactoProgramaColumnaBO objetoBO)
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

        private TPerfilContactoProgramaColumna MapeoEntidad(PerfilContactoProgramaColumnaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPerfilContactoProgramaColumna entidad = new TPerfilContactoProgramaColumna();
                entidad = Mapper.Map<PerfilContactoProgramaColumnaBO, TPerfilContactoProgramaColumna>(objetoBO,
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
        /// Obtiene el Id,Nombre de la Columnas para el Perfil Contacto Programa
        /// </summary>
        /// <returns></returns>
        public List<PerfilContactoProgramaColumnaFiltroDTO> ObtenerTodoFiltro()
        {
            try
            {
                List<PerfilContactoProgramaColumnaFiltroDTO> areasCapacitacionFiltro = new List<PerfilContactoProgramaColumnaFiltroDTO>();
                string _queryFiltroAreaCapacitacion = string.Empty;
                _queryFiltroAreaCapacitacion = "SELECT Id,Nombre FROM pla.V_RegistrosFiltroPerfilContactoProgramaColumna WHERE Estado=1";
                var listaRegistros = _dapper.QueryDapper(_queryFiltroAreaCapacitacion, null);
                if (!string.IsNullOrEmpty(listaRegistros) && !listaRegistros.Contains("[]"))
                {
                    areasCapacitacionFiltro = JsonConvert.DeserializeObject<List<PerfilContactoProgramaColumnaFiltroDTO>>(listaRegistros);
                }
                return areasCapacitacionFiltro;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
