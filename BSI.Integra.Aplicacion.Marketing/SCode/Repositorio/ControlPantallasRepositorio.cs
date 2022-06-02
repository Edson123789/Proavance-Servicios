using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
    /*public class ControlPantallasRepositorio : BaseRepository<TControlPantallas, ControlPantallasBO>
    {
        #region Metodos Base
        public ControlPantallasRepositorio() : base()
        {
        }
        public ControlPantallasRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ControlPantallasBO> GetBy(Expression<Func<TControlPantallas, bool>> filter)
        {
            IEnumerable<TControlPantallas> listado = base.GetBy(filter);
            List<ControlPantallasBO> listadoBO = new List<ControlPantallasBO>();
            foreach (var itemEntidad in listado)
            {
                ControlPantallasBO objetoBO = Mapper.Map<TControlPantallas, ControlPantallasBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ControlPantallasBO FirstById(int id)
        {
            try
            {
                TControlPantallas entidad = base.FirstById(id);
                ControlPantallasBO objetoBO = new ControlPantallasBO();
                Mapper.Map<TControlPantallas, ControlPantallasBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ControlPantallasBO FirstBy(Expression<Func<TControlPantallas, bool>> filter)
        {
            try
            {
                TControlPantallas entidad = base.FirstBy(filter);
                ControlPantallasBO objetoBO = Mapper.Map<TControlPantallas, ControlPantallasBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ControlPantallasBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TControlPantallas entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ControlPantallasBO> listadoBO)
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

        public bool Update(ControlPantallasBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TControlPantallas entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ControlPantallasBO> listadoBO)
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
        private void AsignacionId(TControlPantallas entidad, ControlPantallasBO objetoBO)
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

        private TControlPantallas MapeoEntidad(ControlPantallasBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TControlPantallas entidad = new TControlPantallas();
                entidad = Mapper.Map<ControlPantallasBO, TControlPantallas>(objetoBO,
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

        
    }*/
}
