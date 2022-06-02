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
    /*public class ControlPantallasCentroCostosRepositorio : BaseRepository<TControlPantallasCentroCostos, ControlPantallasCentroCostosBO>
    {
        #region Metodos Base
        public ControlPantallasCentroCostosRepositorio() : base()
        {
        }
        public ControlPantallasCentroCostosRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ControlPantallasCentroCostosBO> GetBy(Expression<Func<TControlPantallasCentroCostos, bool>> filter)
        {
            IEnumerable<TControlPantallasCentroCostos> listado = base.GetBy(filter);
            List<ControlPantallasCentroCostosBO> listadoBO = new List<ControlPantallasCentroCostosBO>();
            foreach (var itemEntidad in listado)
            {
                ControlPantallasCentroCostosBO objetoBO = Mapper.Map<TControlPantallasCentroCostos, ControlPantallasCentroCostosBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ControlPantallasCentroCostosBO FirstById(int id)
        {
            try
            {
                TControlPantallasCentroCostos entidad = base.FirstById(id);
                ControlPantallasCentroCostosBO objetoBO = new ControlPantallasCentroCostosBO();
                Mapper.Map<TControlPantallasCentroCostos, ControlPantallasCentroCostosBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ControlPantallasCentroCostosBO FirstBy(Expression<Func<TControlPantallasCentroCostos, bool>> filter)
        {
            try
            {
                TControlPantallasCentroCostos entidad = base.FirstBy(filter);
                ControlPantallasCentroCostosBO objetoBO = Mapper.Map<TControlPantallasCentroCostos, ControlPantallasCentroCostosBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ControlPantallasCentroCostosBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TControlPantallasCentroCostos entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ControlPantallasCentroCostosBO> listadoBO)
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

        public bool Update(ControlPantallasCentroCostosBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TControlPantallasCentroCostos entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ControlPantallasCentroCostosBO> listadoBO)
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
        private void AsignacionId(TControlPantallasCentroCostos entidad, ControlPantallasCentroCostosBO objetoBO)
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

        private TControlPantallasCentroCostos MapeoEntidad(ControlPantallasCentroCostosBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TControlPantallasCentroCostos entidad = new TControlPantallasCentroCostos();
                entidad = Mapper.Map<ControlPantallasCentroCostosBO, TControlPantallasCentroCostos>(objetoBO,
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
