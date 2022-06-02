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
    public class ContadorBicRepositorio : BaseRepository<TContadorBic, ContadorBicBO>
    {
        #region Metodos Base
        public ContadorBicRepositorio() : base()
        {
        }
        public ContadorBicRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ContadorBicBO> GetBy(Expression<Func<TContadorBic, bool>> filter)
        {
            IEnumerable<TContadorBic> listado = base.GetBy(filter);
            List<ContadorBicBO> listadoBO = new List<ContadorBicBO>();
            foreach (var itemEntidad in listado)
            {
                ContadorBicBO objetoBO = Mapper.Map<TContadorBic, ContadorBicBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ContadorBicBO FirstById(int id)
        {
            try
            {
                TContadorBic entidad = base.FirstById(id);
                ContadorBicBO objetoBO = new ContadorBicBO();
                Mapper.Map<TContadorBic, ContadorBicBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ContadorBicBO FirstBy(Expression<Func<TContadorBic, bool>> filter)
        {
            try
            {
                TContadorBic entidad = base.FirstBy(filter);
                ContadorBicBO objetoBO = Mapper.Map<TContadorBic, ContadorBicBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ContadorBicBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TContadorBic entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ContadorBicBO> listadoBO)
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

        public bool Update(ContadorBicBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TContadorBic entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ContadorBicBO> listadoBO)
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
        private void AsignacionId(TContadorBic entidad, ContadorBicBO objetoBO)
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

        private TContadorBic MapeoEntidad(ContadorBicBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TContadorBic entidad = new TContadorBic();
                entidad = Mapper.Map<ContadorBicBO, TContadorBic>(objetoBO,
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
