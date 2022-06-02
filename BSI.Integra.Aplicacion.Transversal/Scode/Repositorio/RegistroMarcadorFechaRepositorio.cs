using System;
using System.Collections.Generic;
using System.Linq;
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
    public class RegistroMarcadorFechaRepositorio : BaseRepository<TRegistroMarcadorFecha, RegistroMarcadorFechaBO>
    {
        #region Metodos Base
        public RegistroMarcadorFechaRepositorio() : base()
        {
        }
        public RegistroMarcadorFechaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<RegistroMarcadorFechaBO> GetBy(Expression<Func<TRegistroMarcadorFecha, bool>> filter)
        {
            IEnumerable<TRegistroMarcadorFecha> listado = base.GetBy(filter);
            List<RegistroMarcadorFechaBO> listadoBO = new List<RegistroMarcadorFechaBO>();
            foreach (var itemEntidad in listado)
            {
                RegistroMarcadorFechaBO objetoBO = Mapper.Map<TRegistroMarcadorFecha, RegistroMarcadorFechaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public RegistroMarcadorFechaBO FirstById(int id)
        {
            try
            {
                TRegistroMarcadorFecha entidad = base.FirstById(id);
                RegistroMarcadorFechaBO objetoBO = new RegistroMarcadorFechaBO();
                Mapper.Map<TRegistroMarcadorFecha, RegistroMarcadorFechaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public RegistroMarcadorFechaBO FirstBy(Expression<Func<TRegistroMarcadorFecha, bool>> filter)
        {
            try
            {
                TRegistroMarcadorFecha entidad = base.FirstBy(filter);
                RegistroMarcadorFechaBO objetoBO = Mapper.Map<TRegistroMarcadorFecha, RegistroMarcadorFechaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(RegistroMarcadorFechaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TRegistroMarcadorFecha entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<RegistroMarcadorFechaBO> listadoBO)
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

        public bool Update(RegistroMarcadorFechaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TRegistroMarcadorFecha entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<RegistroMarcadorFechaBO> listadoBO)
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
        private void AsignacionId(TRegistroMarcadorFecha entidad, RegistroMarcadorFechaBO objetoBO)
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

        private TRegistroMarcadorFecha MapeoEntidad(RegistroMarcadorFechaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TRegistroMarcadorFecha entidad = new TRegistroMarcadorFecha();
                entidad = Mapper.Map<RegistroMarcadorFechaBO, TRegistroMarcadorFecha>(objetoBO,
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
