using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.GestionPersonas.Repositorio
{
    public class MensajeTiempoInactivoRepositorio : BaseRepository<TMensajeTiempoInactivo, MensajeTiempoInactivoBO>
    {
        #region Metodos Base
        public MensajeTiempoInactivoRepositorio() : base()
        {
        }
        public MensajeTiempoInactivoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<MensajeTiempoInactivoBO> GetBy(Expression<Func<TMensajeTiempoInactivo, bool>> filter)
        {
            IEnumerable<TMensajeTiempoInactivo> listado = base.GetBy(filter);
            List<MensajeTiempoInactivoBO> listadoBO = new List<MensajeTiempoInactivoBO>();
            foreach (var itemEntidad in listado)
            {
                MensajeTiempoInactivoBO objetoBO = Mapper.Map<TMensajeTiempoInactivo, MensajeTiempoInactivoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public MensajeTiempoInactivoBO FirstById(int id)
        {
            try
            {
                TMensajeTiempoInactivo entidad = base.FirstById(id);
                MensajeTiempoInactivoBO objetoBO = new MensajeTiempoInactivoBO();
                Mapper.Map<TMensajeTiempoInactivo, MensajeTiempoInactivoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public MensajeTiempoInactivoBO FirstBy(Expression<Func<TMensajeTiempoInactivo, bool>> filter)
        {
            try
            {
                TMensajeTiempoInactivo entidad = base.FirstBy(filter);
                MensajeTiempoInactivoBO objetoBO = Mapper.Map<TMensajeTiempoInactivo, MensajeTiempoInactivoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(MensajeTiempoInactivoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TMensajeTiempoInactivo entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<MensajeTiempoInactivoBO> listadoBO)
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

        public bool Update(MensajeTiempoInactivoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TMensajeTiempoInactivo entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<MensajeTiempoInactivoBO> listadoBO)
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
        private void AsignacionId(TMensajeTiempoInactivo entidad, MensajeTiempoInactivoBO objetoBO)
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

        private TMensajeTiempoInactivo MapeoEntidad(MensajeTiempoInactivoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TMensajeTiempoInactivo entidad = new TMensajeTiempoInactivo();
                entidad = Mapper.Map<MensajeTiempoInactivoBO, TMensajeTiempoInactivo>(objetoBO,
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
