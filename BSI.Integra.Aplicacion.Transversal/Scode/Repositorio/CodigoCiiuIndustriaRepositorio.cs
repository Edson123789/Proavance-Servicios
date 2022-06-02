
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
    public class CodigoCiiuIndustriaRepositorio : BaseRepository<TCodigoCiiuIndustria, CodigoCiiuIndustriaBO>
    {
        #region Metodos Base
        public CodigoCiiuIndustriaRepositorio() : base()
        {
        }
        public CodigoCiiuIndustriaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CodigoCiiuIndustriaBO> GetBy(Expression<Func<TCodigoCiiuIndustria, bool>> filter)
        {
            IEnumerable<TCodigoCiiuIndustria> listado = base.GetBy(filter);
            List<CodigoCiiuIndustriaBO> listadoBO = new List<CodigoCiiuIndustriaBO>();
            foreach (var itemEntidad in listado)
            {
                CodigoCiiuIndustriaBO objetoBO = Mapper.Map<TCodigoCiiuIndustria, CodigoCiiuIndustriaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CodigoCiiuIndustriaBO FirstById(int id)
        {
            try
            {
                TCodigoCiiuIndustria entidad = base.FirstById(id);
                CodigoCiiuIndustriaBO objetoBO = new CodigoCiiuIndustriaBO();
                Mapper.Map<TCodigoCiiuIndustria, CodigoCiiuIndustriaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CodigoCiiuIndustriaBO FirstBy(Expression<Func<TCodigoCiiuIndustria, bool>> filter)
        {
            try
            {
                TCodigoCiiuIndustria entidad = base.FirstBy(filter);
                CodigoCiiuIndustriaBO objetoBO = Mapper.Map<TCodigoCiiuIndustria, CodigoCiiuIndustriaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CodigoCiiuIndustriaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCodigoCiiuIndustria entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CodigoCiiuIndustriaBO> listadoBO)
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

        public bool Update(CodigoCiiuIndustriaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCodigoCiiuIndustria entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CodigoCiiuIndustriaBO> listadoBO)
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
        private void AsignacionId(TCodigoCiiuIndustria entidad, CodigoCiiuIndustriaBO objetoBO)
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

        private TCodigoCiiuIndustria MapeoEntidad(CodigoCiiuIndustriaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCodigoCiiuIndustria entidad = new TCodigoCiiuIndustria();
                entidad = Mapper.Map<CodigoCiiuIndustriaBO, TCodigoCiiuIndustria>(objetoBO,
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
