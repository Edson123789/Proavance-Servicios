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
    /*public class ControlPantallasTipoDatoRepositorio : BaseRepository<TControlPantallasTipoDato, ControlPantallasTipoDatoBO>
    {
        #region Metodos Base
        public ControlPantallasTipoDatoRepositorio() : base()
        {
        }
        public ControlPantallasTipoDatoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ControlPantallasTipoDatoBO> GetBy(Expression<Func<TControlPantallasTipoDato, bool>> filter)
        {
            IEnumerable<TControlPantallasTipoDato> listado = base.GetBy(filter);
            List<ControlPantallasTipoDatoBO> listadoBO = new List<ControlPantallasTipoDatoBO>();
            foreach (var itemEntidad in listado)
            {
                ControlPantallasTipoDatoBO objetoBO = Mapper.Map<TControlPantallasTipoDato, ControlPantallasTipoDatoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ControlPantallasTipoDatoBO FirstById(int id)
        {
            try
            {
                TControlPantallasTipoDato entidad = base.FirstById(id);
                ControlPantallasTipoDatoBO objetoBO = new ControlPantallasTipoDatoBO();
                Mapper.Map<TControlPantallasTipoDato, ControlPantallasTipoDatoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ControlPantallasTipoDatoBO FirstBy(Expression<Func<TControlPantallasTipoDato, bool>> filter)
        {
            try
            {
                TControlPantallasTipoDato entidad = base.FirstBy(filter);
                ControlPantallasTipoDatoBO objetoBO = Mapper.Map<TControlPantallasTipoDato, ControlPantallasTipoDatoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ControlPantallasTipoDatoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TControlPantallasTipoDato entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ControlPantallasTipoDatoBO> listadoBO)
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

        public bool Update(ControlPantallasTipoDatoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TControlPantallasTipoDato entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ControlPantallasTipoDatoBO> listadoBO)
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
        private void AsignacionId(TControlPantallasTipoDato entidad, ControlPantallasTipoDatoBO objetoBO)
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

        private TControlPantallasTipoDato MapeoEntidad(ControlPantallasTipoDatoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TControlPantallasTipoDato entidad = new TControlPantallasTipoDato();
                entidad = Mapper.Map<ControlPantallasTipoDatoBO, TControlPantallasTipoDato>(objetoBO,
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
