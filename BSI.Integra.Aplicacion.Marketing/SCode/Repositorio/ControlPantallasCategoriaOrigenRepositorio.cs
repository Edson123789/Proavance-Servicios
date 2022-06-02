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
    /*public class ControlPantallasCategoriaOrigenRepositorio : BaseRepository<TControlPantallasCategoriaOrigen, ControlPantallasCategoriaOrigenBO>
    {
        #region Metodos Base
        public ControlPantallasCategoriaOrigenRepositorio() : base()
        {
        }
        public ControlPantallasCategoriaOrigenRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ControlPantallasCategoriaOrigenBO> GetBy(Expression<Func<TControlPantallasCategoriaOrigen, bool>> filter)
        {
            IEnumerable<TControlPantallasCategoriaOrigen> listado = base.GetBy(filter);
            List<ControlPantallasCategoriaOrigenBO> listadoBO = new List<ControlPantallasCategoriaOrigenBO>();
            foreach (var itemEntidad in listado)
            {
                ControlPantallasCategoriaOrigenBO objetoBO = Mapper.Map<TControlPantallasCategoriaOrigen, ControlPantallasCategoriaOrigenBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ControlPantallasCategoriaOrigenBO FirstById(int id)
        {
            try
            {
                TControlPantallasCategoriaOrigen entidad = base.FirstById(id);
                ControlPantallasCategoriaOrigenBO objetoBO = new ControlPantallasCategoriaOrigenBO();
                Mapper.Map<TControlPantallasCategoriaOrigen, ControlPantallasCategoriaOrigenBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ControlPantallasCategoriaOrigenBO FirstBy(Expression<Func<TControlPantallasCategoriaOrigen, bool>> filter)
        {
            try
            {
                TControlPantallasCategoriaOrigen entidad = base.FirstBy(filter);
                ControlPantallasCategoriaOrigenBO objetoBO = Mapper.Map<TControlPantallasCategoriaOrigen, ControlPantallasCategoriaOrigenBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ControlPantallasCategoriaOrigenBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TControlPantallasCategoriaOrigen entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ControlPantallasCategoriaOrigenBO> listadoBO)
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

        public bool Update(ControlPantallasCategoriaOrigenBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TControlPantallasCategoriaOrigen entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ControlPantallasCategoriaOrigenBO> listadoBO)
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
        private void AsignacionId(TControlPantallasCategoriaOrigen entidad, ControlPantallasCategoriaOrigenBO objetoBO)
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

        private TControlPantallasCategoriaOrigen MapeoEntidad(ControlPantallasCategoriaOrigenBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TControlPantallasCategoriaOrigen entidad = new TControlPantallasCategoriaOrigen();
                entidad = Mapper.Map<ControlPantallasCategoriaOrigenBO, TControlPantallasCategoriaOrigen>(objetoBO,
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
