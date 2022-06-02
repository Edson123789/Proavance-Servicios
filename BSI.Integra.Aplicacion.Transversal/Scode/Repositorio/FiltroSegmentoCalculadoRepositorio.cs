using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class FiltroSegmentoCalculadoRepositorio : BaseRepository<TFiltroSegmentoCalculado, FiltroSegmentoCalculadoBO>
    {
        #region Metodos Base
        public FiltroSegmentoCalculadoRepositorio() : base()
        {
        }
        public FiltroSegmentoCalculadoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<FiltroSegmentoCalculadoBO> GetBy(Expression<Func<TFiltroSegmentoCalculado, bool>> filter)
        {
            IEnumerable<TFiltroSegmentoCalculado> listado = base.GetBy(filter).ToList();
            List<FiltroSegmentoCalculadoBO> listadoBO = new List<FiltroSegmentoCalculadoBO>();
            foreach (var itemEntidad in listado)
            {
                FiltroSegmentoCalculadoBO objetoBO = Mapper.Map<TFiltroSegmentoCalculado, FiltroSegmentoCalculadoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public FiltroSegmentoCalculadoBO FirstById(int id)
        {
            try
            {
                TFiltroSegmentoCalculado entidad = base.FirstById(id);
                FiltroSegmentoCalculadoBO objetoBO = new FiltroSegmentoCalculadoBO();
                Mapper.Map<TFiltroSegmentoCalculado, FiltroSegmentoCalculadoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public FiltroSegmentoCalculadoBO FirstBy(Expression<Func<TFiltroSegmentoCalculado, bool>> filter)
        {
            try
            {
                TFiltroSegmentoCalculado entidad = base.FirstBy(filter);
                FiltroSegmentoCalculadoBO objetoBO = Mapper.Map<TFiltroSegmentoCalculado, FiltroSegmentoCalculadoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(FiltroSegmentoCalculadoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TFiltroSegmentoCalculado entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<FiltroSegmentoCalculadoBO> listadoBO)
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

        public bool Update(FiltroSegmentoCalculadoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TFiltroSegmentoCalculado entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<FiltroSegmentoCalculadoBO> listadoBO)
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
        private void AsignacionId(TFiltroSegmentoCalculado entidad, FiltroSegmentoCalculadoBO objetoBO)
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

        private TFiltroSegmentoCalculado MapeoEntidad(FiltroSegmentoCalculadoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TFiltroSegmentoCalculado entidad = new TFiltroSegmentoCalculado();
                entidad = Mapper.Map<FiltroSegmentoCalculadoBO, TFiltroSegmentoCalculado>(objetoBO,
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
        /// Elimina los registros que pertenecen a filtro segmento
        /// </summary>
        /// <param name="idFiltroSegmento"></param>
        /// <returns></returns>
        public bool EliminarPorFiltroSegmento(int idFiltroSegmento, string nombreUsuario)
        {
            try
            {
                this._dapper.QuerySPDapper("mkt.SP_EliminarFiltroSegmentoCalculado", new { idFiltroSegmento, nombreUsuario });
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
