
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
    public class CalculoOportunidadRn2Repositorio : BaseRepository<TCalculoOportunidadRn2, CalculoOportunidadRn2BO>
    {
        #region Metodos Base
        public CalculoOportunidadRn2Repositorio() : base()
        {
        }
        public CalculoOportunidadRn2Repositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CalculoOportunidadRn2BO> GetBy(Expression<Func<TCalculoOportunidadRn2, bool>> filter)
        {
            IEnumerable<TCalculoOportunidadRn2> listado = base.GetBy(filter);
            List<CalculoOportunidadRn2BO> listadoBO = new List<CalculoOportunidadRn2BO>();
            foreach (var itemEntidad in listado)
            {
                CalculoOportunidadRn2BO objetoBO = Mapper.Map<TCalculoOportunidadRn2, CalculoOportunidadRn2BO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CalculoOportunidadRn2BO FirstById(int id)
        {
            try
            {
                TCalculoOportunidadRn2 entidad = base.FirstById(id);
                CalculoOportunidadRn2BO objetoBO = new CalculoOportunidadRn2BO();
                Mapper.Map<TCalculoOportunidadRn2, CalculoOportunidadRn2BO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CalculoOportunidadRn2BO FirstBy(Expression<Func<TCalculoOportunidadRn2, bool>> filter)
        {
            try
            {
                TCalculoOportunidadRn2 entidad = base.FirstBy(filter);
                CalculoOportunidadRn2BO objetoBO = Mapper.Map<TCalculoOportunidadRn2, CalculoOportunidadRn2BO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CalculoOportunidadRn2BO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCalculoOportunidadRn2 entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CalculoOportunidadRn2BO> listadoBO)
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

        public bool Update(CalculoOportunidadRn2BO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCalculoOportunidadRn2 entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CalculoOportunidadRn2BO> listadoBO)
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
        private void AsignacionId(TCalculoOportunidadRn2 entidad, CalculoOportunidadRn2BO objetoBO)
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

        private TCalculoOportunidadRn2 MapeoEntidad(CalculoOportunidadRn2BO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCalculoOportunidadRn2 entidad = new TCalculoOportunidadRn2();
                entidad = Mapper.Map<CalculoOportunidadRn2BO, TCalculoOportunidadRn2>(objetoBO,
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
