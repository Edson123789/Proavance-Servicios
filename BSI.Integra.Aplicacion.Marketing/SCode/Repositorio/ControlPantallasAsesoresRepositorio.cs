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
    /*public class ControlPantallasAsesoresRepositorio : BaseRepository<TControlPantallasAsesores, ControlPantallasAsesoresBO>
    {
        #region Metodos Base
        public ControlPantallasAsesoresRepositorio() : base()
        {
        }
        public ControlPantallasAsesoresRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ControlPantallasAsesoresBO> GetBy(Expression<Func<TControlPantallasAsesores, bool>> filter)
        {
            IEnumerable<TControlPantallasAsesores> listado = base.GetBy(filter);
            List<ControlPantallasAsesoresBO> listadoBO = new List<ControlPantallasAsesoresBO>();
            foreach (var itemEntidad in listado)
            {
                ControlPantallasAsesoresBO objetoBO = Mapper.Map<TControlPantallasAsesores, ControlPantallasAsesoresBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ControlPantallasAsesoresBO FirstById(int id)
        {
            try
            {
                TControlPantallasAsesores entidad = base.FirstById(id);
                ControlPantallasAsesoresBO objetoBO = new ControlPantallasAsesoresBO();
                Mapper.Map<TControlPantallasAsesores, ControlPantallasAsesoresBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ControlPantallasAsesoresBO FirstBy(Expression<Func<TControlPantallasAsesores, bool>> filter)
        {
            try
            {
                TControlPantallasAsesores entidad = base.FirstBy(filter);
                ControlPantallasAsesoresBO objetoBO = Mapper.Map<TControlPantallasAsesores, ControlPantallasAsesoresBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ControlPantallasAsesoresBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TControlPantallasAsesores entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ControlPantallasAsesoresBO> listadoBO)
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

        public bool Update(ControlPantallasAsesoresBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TControlPantallasAsesores entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ControlPantallasAsesoresBO> listadoBO)
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
        private void AsignacionId(TControlPantallasAsesores entidad, ControlPantallasAsesoresBO objetoBO)
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

        private TControlPantallasAsesores MapeoEntidad(ControlPantallasAsesoresBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TControlPantallasAsesores entidad = new TControlPantallasAsesores();
                entidad = Mapper.Map<ControlPantallasAsesoresBO, TControlPantallasAsesores>(objetoBO,
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
