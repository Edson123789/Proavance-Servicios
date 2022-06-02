using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Planificacion.Repositorio
{
    public class PlantillaMaestroPwRepositorio : BaseRepository<TPlantillaMaestroPw, PlantillaMaestroPwBO>
    {
        #region Metodos Base
        public PlantillaMaestroPwRepositorio() : base()
        {
        }
        public PlantillaMaestroPwRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PlantillaMaestroPwBO> GetBy(Expression<Func<TPlantillaMaestroPw, bool>> filter)
        {
            IEnumerable<TPlantillaMaestroPw> listado = base.GetBy(filter);
            List<PlantillaMaestroPwBO> listadoBO = new List<PlantillaMaestroPwBO>();
            foreach (var itemEntidad in listado)
            {
                PlantillaMaestroPwBO objetoBO = Mapper.Map<TPlantillaMaestroPw, PlantillaMaestroPwBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PlantillaMaestroPwBO FirstById(int id)
        {
            try
            {
                TPlantillaMaestroPw entidad = base.FirstById(id);
                PlantillaMaestroPwBO objetoBO = new PlantillaMaestroPwBO();
                Mapper.Map<TPlantillaMaestroPw, PlantillaMaestroPwBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PlantillaMaestroPwBO FirstBy(Expression<Func<TPlantillaMaestroPw, bool>> filter)
        {
            try
            {
                TPlantillaMaestroPw entidad = base.FirstBy(filter);
                PlantillaMaestroPwBO objetoBO = Mapper.Map<TPlantillaMaestroPw, PlantillaMaestroPwBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PlantillaMaestroPwBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPlantillaMaestroPw entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PlantillaMaestroPwBO> listadoBO)
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

        public bool Update(PlantillaMaestroPwBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPlantillaMaestroPw entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PlantillaMaestroPwBO> listadoBO)
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
        private void AsignacionId(TPlantillaMaestroPw entidad, PlantillaMaestroPwBO objetoBO)
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

        private TPlantillaMaestroPw MapeoEntidad(PlantillaMaestroPwBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPlantillaMaestroPw entidad = new TPlantillaMaestroPw();
                entidad = Mapper.Map<PlantillaMaestroPwBO, TPlantillaMaestroPw>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos
                if (objetoBO.SeccionMaestra != null && objetoBO.SeccionMaestra.Count > 0)
                {
                    foreach (var hijo in objetoBO.SeccionMaestra)
                    {
                        TSeccionMaestraPw entidadHijo = new TSeccionMaestraPw();
                        entidadHijo = Mapper.Map<SeccionMaestraPwBO, TSeccionMaestraPw>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TSeccionMaestraPw.Add(entidadHijo);
                    }
                }

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
		#endregion
        
        /// <summary>
        /// Obtiene todos los registros sin los campos de auditoría.
        /// </summary>
        /// <returns></returns>
        public List<PlantillaMaestroPwDTO> ObtenerTodoGrid()
        {
            try
            {
                var lista = GetBy(x => true, y => new PlantillaMaestroPwDTO
                {
                    Id = y.Id,
                    Nombre = y.Nombre,
                    Repeticion = y.Repeticion,
                }).OrderByDescending(x => x.Id).ToList();

                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
     
}
