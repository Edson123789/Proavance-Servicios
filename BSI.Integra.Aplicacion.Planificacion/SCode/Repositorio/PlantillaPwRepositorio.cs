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
    public class PlantillaPwRepositorio : BaseRepository<TPlantillaPw, PlantillaPwBO>
    {
        #region Metodos Base
        public PlantillaPwRepositorio() : base()
        {
        }
        public PlantillaPwRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PlantillaPwBO> GetBy(Expression<Func<TPlantillaPw, bool>> filter)
        {
            IEnumerable<TPlantillaPw> listado = base.GetBy(filter);
            List<PlantillaPwBO> listadoBO = new List<PlantillaPwBO>();
            foreach (var itemEntidad in listado)
            {
                PlantillaPwBO objetoBO = Mapper.Map<TPlantillaPw, PlantillaPwBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PlantillaPwBO FirstById(int id)
        {
            try
            {
                TPlantillaPw entidad = base.FirstById(id);
                PlantillaPwBO objetoBO = new PlantillaPwBO();
                Mapper.Map<TPlantillaPw, PlantillaPwBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PlantillaPwBO FirstBy(Expression<Func<TPlantillaPw, bool>> filter)
        {
            try
            {
                TPlantillaPw entidad = base.FirstBy(filter);
                PlantillaPwBO objetoBO = Mapper.Map<TPlantillaPw, PlantillaPwBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PlantillaPwBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPlantillaPw entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PlantillaPwBO> listadoBO)
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

        public bool Update(PlantillaPwBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPlantillaPw entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PlantillaPwBO> listadoBO)
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
        private void AsignacionId(TPlantillaPw entidad, PlantillaPwBO objetoBO)
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

        private TPlantillaPw MapeoEntidad(PlantillaPwBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPlantillaPw entidad = new TPlantillaPw();
                entidad = Mapper.Map<PlantillaPwBO, TPlantillaPw>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos
                if (objetoBO.PlantillaRevisionPw != null && objetoBO.PlantillaRevisionPw.Count > 0)
                {
                    foreach (var hijo in objetoBO.PlantillaRevisionPw)
                    {
                        TPlantillaRevisionPw entidadHijo = new TPlantillaRevisionPw();
                        entidadHijo = Mapper.Map<PlantillaRevisionPwBO, TPlantillaRevisionPw>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TPlantillaRevisionPw.Add(entidadHijo);
                    }
                }

                if (objetoBO.PlantillaPlantillaMaestroPw != null && objetoBO.PlantillaPlantillaMaestroPw.Count > 0)
                {
                    foreach (var hijo in objetoBO.PlantillaPlantillaMaestroPw)
                    {
                        TPlantillaPlantillaMaestroPw entidadHijo = new TPlantillaPlantillaMaestroPw();
                        entidadHijo = Mapper.Map<PlantillaPlantillaMaestroPwBO, TPlantillaPlantillaMaestroPw>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TPlantillaPlantillaMaestroPw.Add(entidadHijo);
                    }
                }

                if (objetoBO.SeccionPw != null && objetoBO.SeccionPw.Count > 0)
                {
                    foreach (var hijo in objetoBO.SeccionPw)
                    {
                        TSeccionPw entidadHijo = new TSeccionPw();
                        entidadHijo = Mapper.Map<SeccionPwBO, TSeccionPw>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TSeccionPw.Add(entidadHijo);

                        if (hijo.SeccionTipoDetallePw != null && hijo.SeccionTipoDetallePw.Count > 0)
                        {
                            foreach (var hijo2 in hijo.SeccionTipoDetallePw)
                            {
                                TSeccionTipoDetallePw entidadHijo2 = new TSeccionTipoDetallePw();
                                entidadHijo2 = Mapper.Map<SeccionTipoDetallePwBO, TSeccionTipoDetallePw>(hijo2,
                                    opt => opt.ConfigureMap(MemberList.None));
                                entidadHijo.TSeccionTipoDetallePw.Add(entidadHijo2);
                            }
                        }
                    }
                }

                if (objetoBO.PlantillaPais != null && objetoBO.PlantillaPais.Count > 0)
                {
                    foreach (var hijo in objetoBO.PlantillaPais)
                    {
                        TPlantillaPais entidadHijo = new TPlantillaPais();
                        entidadHijo = Mapper.Map<PlantillaPaisBO, TPlantillaPais>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TPlantillaPais.Add(entidadHijo);
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
        public List<PlantillaPwDTO> ObtenerTodoGrid()
        {
            try
            {
                var lista = GetBy(x => true, y => new PlantillaPwDTO
                {
                    Id = y.Id,
                    Nombre = y.Nombre,
                    Descripcion = y.Descripcion,
                    IdPlantillaMaestroPw = y.IdPlantillaMaestroPw,
                    IdRevisionPw = y.IdRevisionPw,
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
