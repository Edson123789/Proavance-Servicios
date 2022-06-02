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
    public class CarreraProfesionalRepositorio : BaseRepository<TCarreraProfesional, CarreraProfesionalBO>
    {
        #region Metodos Base
        public CarreraProfesionalRepositorio() : base()
        {
        }
        public CarreraProfesionalRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CarreraProfesionalBO> GetBy(Expression<Func<TCarreraProfesional, bool>> filter)
        {
            IEnumerable<TCarreraProfesional> listado = base.GetBy(filter);
            List<CarreraProfesionalBO> listadoBO = new List<CarreraProfesionalBO>();
            foreach (var itemEntidad in listado)
            {
                CarreraProfesionalBO objetoBO = Mapper.Map<TCarreraProfesional, CarreraProfesionalBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CarreraProfesionalBO FirstById(int id)
        {
            try
            {
                TCarreraProfesional entidad = base.FirstById(id);
                CarreraProfesionalBO objetoBO = new CarreraProfesionalBO();
                Mapper.Map<TCarreraProfesional, CarreraProfesionalBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CarreraProfesionalBO FirstBy(Expression<Func<TCarreraProfesional, bool>> filter)
        {
            try
            {
                TCarreraProfesional entidad = base.FirstBy(filter);
                CarreraProfesionalBO objetoBO = Mapper.Map<TCarreraProfesional, CarreraProfesionalBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CarreraProfesionalBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCarreraProfesional entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CarreraProfesionalBO> listadoBO)
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

        public bool Update(CarreraProfesionalBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCarreraProfesional entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CarreraProfesionalBO> listadoBO)
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
        private void AsignacionId(TCarreraProfesional entidad, CarreraProfesionalBO objetoBO)
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

        private TCarreraProfesional MapeoEntidad(CarreraProfesionalBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCarreraProfesional entidad = new TCarreraProfesional();
                entidad = Mapper.Map<CarreraProfesionalBO, TCarreraProfesional>(objetoBO,
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
