using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutoMapper;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using BSI.Integra.Aplicacion.DTOs;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Planificacion.Repositorio
{
    
    public class ProgramaCapacitacionPorPGeneralRepositorio : BaseRepository<TProgramaCapacitacionPorPgeneral, ProgramaCapacitacionPorPGeneralBO>
    {
        #region Metodos Base
        public ProgramaCapacitacionPorPGeneralRepositorio() : base()
        {
        }
        public ProgramaCapacitacionPorPGeneralRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ProgramaCapacitacionPorPGeneralBO> GetBy(Expression<Func<TProgramaCapacitacionPorPgeneral, bool>> filter)
        {
            IEnumerable<TProgramaCapacitacionPorPgeneral> listado = base.GetBy(filter);
            List<ProgramaCapacitacionPorPGeneralBO> listadoBO = new List<ProgramaCapacitacionPorPGeneralBO>();
            foreach (var itemEntidad in listado)
            {
                ProgramaCapacitacionPorPGeneralBO objetoBO = Mapper.Map<TProgramaCapacitacionPorPgeneral, ProgramaCapacitacionPorPGeneralBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ProgramaCapacitacionPorPGeneralBO FirstById(int id)
        {
            try
            {
                TProgramaCapacitacionPorPgeneral entidad = base.FirstById(id);
                ProgramaCapacitacionPorPGeneralBO objetoBO = new ProgramaCapacitacionPorPGeneralBO();
                Mapper.Map<TProgramaCapacitacionPorPgeneral, ProgramaCapacitacionPorPGeneralBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ProgramaCapacitacionPorPGeneralBO FirstBy(Expression<Func<TProgramaCapacitacionPorPgeneral, bool>> filter)
        {
            try
            {
                TProgramaCapacitacionPorPgeneral entidad = base.FirstBy(filter);
                ProgramaCapacitacionPorPGeneralBO objetoBO = Mapper.Map<TProgramaCapacitacionPorPgeneral, ProgramaCapacitacionPorPGeneralBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ProgramaCapacitacionPorPGeneralBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TProgramaCapacitacionPorPgeneral entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ProgramaCapacitacionPorPGeneralBO> listadoBO)
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

        public bool Update(ProgramaCapacitacionPorPGeneralBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TProgramaCapacitacionPorPgeneral entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ProgramaCapacitacionPorPGeneralBO> listadoBO)
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
        private void AsignacionId(TProgramaCapacitacionPorPgeneral entidad, ProgramaCapacitacionPorPGeneralBO objetoBO)
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

        private TProgramaCapacitacionPorPgeneral MapeoEntidad(ProgramaCapacitacionPorPGeneralBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TProgramaCapacitacionPorPgeneral entidad = new TProgramaCapacitacionPorPgeneral();
                entidad = Mapper.Map<ProgramaCapacitacionPorPGeneralBO, TProgramaCapacitacionPorPgeneral>(objetoBO,
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
