using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: AsignacionOportunidadLogRepositorio
    /// Autor: Edgar S.
    /// Fecha: 08/02/2021
    /// <summary>
    /// Gestión de Lod de Asignacion de Oportunidades
    /// </summary>
    public class AsignacionOportunidadLogRepositorio : BaseRepository<TAsignacionOportunidadLog, AsignacionOportunidadLogBO>
    {
        #region Metodos Base
        public AsignacionOportunidadLogRepositorio() : base()
        {
        }
        public AsignacionOportunidadLogRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<AsignacionOportunidadLogBO> GetBy(Expression<Func<TAsignacionOportunidadLog, bool>> filter)
        {
            IEnumerable<TAsignacionOportunidadLog> listado = base.GetBy(filter);
            List<AsignacionOportunidadLogBO> listadoBO = new List<AsignacionOportunidadLogBO>();
            foreach (var itemEntidad in listado)
            {
                AsignacionOportunidadLogBO objetoBO = Mapper.Map<TAsignacionOportunidadLog, AsignacionOportunidadLogBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public AsignacionOportunidadLogBO FirstById(int id)
        {
            try
            {
                TAsignacionOportunidadLog entidad = base.FirstById(id);
                AsignacionOportunidadLogBO objetoBO = new AsignacionOportunidadLogBO();
                Mapper.Map<TAsignacionOportunidadLog, AsignacionOportunidadLogBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public AsignacionOportunidadLogBO FirstBy(Expression<Func<TAsignacionOportunidadLog, bool>> filter)
        {
            try
            {
                TAsignacionOportunidadLog entidad = base.FirstBy(filter);
                AsignacionOportunidadLogBO objetoBO = Mapper.Map<TAsignacionOportunidadLog, AsignacionOportunidadLogBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(AsignacionOportunidadLogBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TAsignacionOportunidadLog entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<AsignacionOportunidadLogBO> listadoBO)
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

        public bool Update(AsignacionOportunidadLogBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TAsignacionOportunidadLog entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<AsignacionOportunidadLogBO> listadoBO)
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
        private void AsignacionId(TAsignacionOportunidadLog entidad, AsignacionOportunidadLogBO objetoBO)
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

        private TAsignacionOportunidadLog MapeoEntidad(AsignacionOportunidadLogBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TAsignacionOportunidadLog entidad = new TAsignacionOportunidadLog();
                entidad = Mapper.Map<AsignacionOportunidadLogBO, TAsignacionOportunidadLog>(objetoBO,
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
    }
}
