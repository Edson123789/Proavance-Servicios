using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class AsistenciaRepositorio : BaseRepository<TAsistencia, AsistenciaBO>
    {
        #region Metodos Base
        public AsistenciaRepositorio() : base()
        {
        }
        public AsistenciaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<AsistenciaBO> GetBy(Expression<Func<TAsistencia, bool>> filter)
        {
            IEnumerable<TAsistencia> listado = base.GetBy(filter);
            List<AsistenciaBO> listadoBO = new List<AsistenciaBO>();
            foreach (var itemEntidad in listado)
            {
                AsistenciaBO objetoBO = Mapper.Map<TAsistencia, AsistenciaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public AsistenciaBO FirstById(int id)
        {
            try
            {
                TAsistencia entidad = base.FirstById(id);
                AsistenciaBO objetoBO = new AsistenciaBO();
                Mapper.Map<TAsistencia, AsistenciaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public AsistenciaBO FirstBy(Expression<Func<TAsistencia, bool>> filter)
        {
            try
            {
                TAsistencia entidad = base.FirstBy(filter);
                AsistenciaBO objetoBO = Mapper.Map<TAsistencia, AsistenciaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(AsistenciaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TAsistencia entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<AsistenciaBO> listadoBO)
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

        public bool Update(AsistenciaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TAsistencia entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<AsistenciaBO> listadoBO)
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
        private void AsignacionId(TAsistencia entidad, AsistenciaBO objetoBO)
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

        private TAsistencia MapeoEntidad(AsistenciaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TAsistencia entidad = new TAsistencia();
                entidad = Mapper.Map<AsistenciaBO, TAsistencia>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                if (objetoBO.ListaMaterialEntrega != null && objetoBO.ListaMaterialEntrega.Count > 0)
                {
                    foreach (var hijo in objetoBO.ListaMaterialEntrega)
                    {
                        TMaterialEntrega entidadHijo = new TMaterialEntrega();
                        entidadHijo = Mapper.Map<MaterialEntregaBO, TMaterialEntrega>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TMaterialEntrega.Add(entidadHijo);
                    }
                }
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
