using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Planificacion.Repositorio
{
    public class TipoDatoMetaRepositorio : BaseRepository<TTipoDatoMeta, TipoDatoMetaBO>
    {
        #region Metodos Base
        public TipoDatoMetaRepositorio() : base()
        {
        }
        public TipoDatoMetaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<TipoDatoMetaBO> GetBy(Expression<Func<TTipoDatoMeta, bool>> filter)
        {
            IEnumerable<TTipoDatoMeta> listado = base.GetBy(filter);
            List<TipoDatoMetaBO> listadoBO = new List<TipoDatoMetaBO>();
            foreach (var itemEntidad in listado)
            {
                TipoDatoMetaBO objetoBO = Mapper.Map<TTipoDatoMeta, TipoDatoMetaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public TipoDatoMetaBO FirstById(int id)
        {
            try
            {
                TTipoDatoMeta entidad = base.FirstById(id);
                TipoDatoMetaBO objetoBO = new TipoDatoMetaBO();
                Mapper.Map<TTipoDatoMeta, TipoDatoMetaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public TipoDatoMetaBO FirstBy(Expression<Func<TTipoDatoMeta, bool>> filter)
        {
            try
            {
                TTipoDatoMeta entidad = base.FirstBy(filter);
                TipoDatoMetaBO objetoBO = Mapper.Map<TTipoDatoMeta, TipoDatoMetaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(TipoDatoMetaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TTipoDatoMeta entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<TipoDatoMetaBO> listadoBO)
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

        public bool Update(TipoDatoMetaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TTipoDatoMeta entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<TipoDatoMetaBO> listadoBO)
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
        private void AsignacionId(TTipoDatoMeta entidad, TipoDatoMetaBO objetoBO)
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

        private TTipoDatoMeta MapeoEntidad(TipoDatoMetaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TTipoDatoMeta entidad = new TTipoDatoMeta();
                entidad = Mapper.Map<TipoDatoMetaBO, TTipoDatoMeta>(objetoBO,
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
        /// Obtiene todos los registros sin los campos de auditoría.
        /// </summary>
        /// <returns></returns>
        public List<TipoDatoMetaDTO> ObtenerTodoGrid()
        {
            try
            {
                var lista = GetBy(x => true, y => new TipoDatoMetaDTO
                {
                    Id = y.Id,
                    IdFaseOportunidadDestino = y.IdFaseOportunidadDestino,
                    IdFaseOportunidadOrigen = y.IdFaseOportunidadOrigen,
                    IdTipoDato = y.IdTipoDato,
                    Meta = y.Meta,
                    MetaGlobal = y.MetaGlobal,
                }).OrderByDescending(x => x.Id).ToList();

                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
      
    }
     
}
