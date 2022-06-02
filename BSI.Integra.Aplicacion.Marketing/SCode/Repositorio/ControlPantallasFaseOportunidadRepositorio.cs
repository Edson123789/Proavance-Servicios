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
    /*public class ControlPantallasFaseOportunidadRepositorio : BaseRepository<TControlPantallasFaseOportunidad, ControlPantallasFaseOportunidadBO>
    {
        #region Metodos Base
        public ControlPantallasFaseOportunidadRepositorio() : base()
        {
        }
        public ControlPantallasFaseOportunidadRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ControlPantallasFaseOportunidadBO> GetBy(Expression<Func<TControlPantallasFaseOportunidad, bool>> filter)
        {
            IEnumerable<TControlPantallasFaseOportunidad> listado = base.GetBy(filter);
            List<ControlPantallasFaseOportunidadBO> listadoBO = new List<ControlPantallasFaseOportunidadBO>();
            foreach (var itemEntidad in listado)
            {
                ControlPantallasFaseOportunidadBO objetoBO = Mapper.Map<TControlPantallasFaseOportunidad, ControlPantallasFaseOportunidadBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ControlPantallasFaseOportunidadBO FirstById(int id)
        {
            try
            {
                TControlPantallasFaseOportunidad entidad = base.FirstById(id);
                ControlPantallasFaseOportunidadBO objetoBO = new ControlPantallasFaseOportunidadBO();
                Mapper.Map<TControlPantallasFaseOportunidad, ControlPantallasFaseOportunidadBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ControlPantallasFaseOportunidadBO FirstBy(Expression<Func<TControlPantallasFaseOportunidad, bool>> filter)
        {
            try
            {
                TControlPantallasFaseOportunidad entidad = base.FirstBy(filter);
                ControlPantallasFaseOportunidadBO objetoBO = Mapper.Map<TControlPantallasFaseOportunidad, ControlPantallasFaseOportunidadBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ControlPantallasFaseOportunidadBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TControlPantallasFaseOportunidad entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ControlPantallasFaseOportunidadBO> listadoBO)
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

        public bool Update(ControlPantallasFaseOportunidadBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TControlPantallasFaseOportunidad entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ControlPantallasFaseOportunidadBO> listadoBO)
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
        private void AsignacionId(TControlPantallasFaseOportunidad entidad, ControlPantallasFaseOportunidadBO objetoBO)
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

        private TControlPantallasFaseOportunidad MapeoEntidad(ControlPantallasFaseOportunidadBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TControlPantallasFaseOportunidad entidad = new TControlPantallasFaseOportunidad();
                entidad = Mapper.Map<ControlPantallasFaseOportunidadBO, TControlPantallasFaseOportunidad>(objetoBO,
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
