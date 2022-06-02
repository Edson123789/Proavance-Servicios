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
    public class AsesorRepositorio : BaseRepository<TAsesor, AsesorBO>
    {
        #region Metodos Base
        public AsesorRepositorio() : base()
        {
        }
        public AsesorRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<AsesorBO> GetBy(Expression<Func<TAsesor, bool>> filter)
        {
            IEnumerable<TAsesor> listado = base.GetBy(filter);
            List<AsesorBO> listadoBO = new List<AsesorBO>();
            foreach (var itemEntidad in listado)
            {
                AsesorBO objetoBO = Mapper.Map<TAsesor, AsesorBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public AsesorBO FirstById(int id)
        {
            try
            {
                TAsesor entidad = base.FirstById(id);
                AsesorBO objetoBO = new AsesorBO();
                Mapper.Map<TAsesor, AsesorBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public AsesorBO FirstBy(Expression<Func<TAsesor, bool>> filter)
        {
            try
            {
                TAsesor entidad = base.FirstBy(filter);
                AsesorBO objetoBO = Mapper.Map<TAsesor, AsesorBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(AsesorBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TAsesor entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<AsesorBO> listadoBO)
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

        public bool Update(AsesorBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TAsesor entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<AsesorBO> listadoBO)
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
        private void AsignacionId(TAsesor entidad, AsesorBO objetoBO)
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

        private TAsesor MapeoEntidad(AsesorBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TAsesor entidad = new TAsesor();
                entidad = Mapper.Map<AsesorBO, TAsesor>(objetoBO,
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

        /// <summary>
        /// Obtiene el Id,idPersonal de los asesores
        /// </summary>
        /// <returns></returns>
        public List<AsesorDTO> ObtenerTodoAsesoresFiltro()
        {
            try
            {
                var listaAsesores = GetBy(x => true, y => new AsesorDTO
                {
                    Id = y.Id,
                    IdPersonal = y.IdPersonal

                }).OrderByDescending(x => x.Id).ToList();

                return listaAsesores;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        
    }
}
