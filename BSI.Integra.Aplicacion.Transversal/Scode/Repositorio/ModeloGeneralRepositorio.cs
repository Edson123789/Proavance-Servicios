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

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class ModeloGeneralRepositorio : BaseRepository<TModeloGeneral, ModeloGeneralBO>
    {
        #region Metodos Base
        public ModeloGeneralRepositorio() : base()
        {
        }
        public ModeloGeneralRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ModeloGeneralBO> GetBy(Expression<Func<TModeloGeneral, bool>> filter)
        {
            IEnumerable<TModeloGeneral> listado = base.GetBy(filter);
            List<ModeloGeneralBO> listadoBO = new List<ModeloGeneralBO>();
            foreach (var itemEntidad in listado)
            {
                ModeloGeneralBO objetoBO = Mapper.Map<TModeloGeneral, ModeloGeneralBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ModeloGeneralBO FirstById(int id)
        {
            try
            {
                TModeloGeneral entidad = base.FirstById(id);
                ModeloGeneralBO objetoBO = new ModeloGeneralBO();
                Mapper.Map<TModeloGeneral, ModeloGeneralBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ModeloGeneralBO FirstBy(Expression<Func<TModeloGeneral, bool>> filter)
        {
            try
            {
                TModeloGeneral entidad = base.FirstBy(filter);
                ModeloGeneralBO objetoBO = Mapper.Map<TModeloGeneral, ModeloGeneralBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ModeloGeneralBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TModeloGeneral entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ModeloGeneralBO> listadoBO)
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

        public bool Update(ModeloGeneralBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TModeloGeneral entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ModeloGeneralBO> listadoBO)
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
        private void AsignacionId(TModeloGeneral entidad, ModeloGeneralBO objetoBO)
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

        private TModeloGeneral MapeoEntidad(ModeloGeneralBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TModeloGeneral entidad = new TModeloGeneral();
                entidad = Mapper.Map<ModeloGeneralBO, TModeloGeneral>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos
                if (objetoBO.ModeloGeneralEscala != null && objetoBO.ModeloGeneralEscala.Count > 0)
                {
                    foreach (var hijo in objetoBO.ModeloGeneralEscala)
                    {
                        TModeloGeneralEscala entidadHijo = new TModeloGeneralEscala();
                        entidadHijo = Mapper.Map<ModeloGeneralEscalaBO, TModeloGeneralEscala>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TModeloGeneralEscala.Add(entidadHijo);
                    }
                }
                if (objetoBO.ModeloGeneralCargo != null && objetoBO.ModeloGeneralCargo.Count > 0)
                {
                    foreach (var hijo in objetoBO.ModeloGeneralCargo)
                    {
                        TModeloGeneralCargo entidadHijo = new TModeloGeneralCargo();
                        entidadHijo = Mapper.Map<ModeloGeneralCargoBO, TModeloGeneralCargo>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TModeloGeneralCargo.Add(entidadHijo);
                    }
                }
                if (objetoBO.ModeloGeneralIndustria != null && objetoBO.ModeloGeneralIndustria.Count > 0)
                {
                    foreach (var hijo in objetoBO.ModeloGeneralIndustria)
                    {
                        TModeloGeneralIndustria entidadHijo = new TModeloGeneralIndustria();
                        entidadHijo = Mapper.Map<ModeloGeneralIndustriaBO, TModeloGeneralIndustria>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TModeloGeneralIndustria.Add(entidadHijo);
                    }
                }
                if (objetoBO.ModeloGeneralAFormacion != null && objetoBO.ModeloGeneralAFormacion.Count > 0)
                {
                    foreach (var hijo in objetoBO.ModeloGeneralAFormacion)
                    {
                        TModeloGeneralAformacion entidadHijo = new TModeloGeneralAformacion();
                        entidadHijo = Mapper.Map<ModeloGeneralAFormacionBO, TModeloGeneralAformacion>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TModeloGeneralAformacion.Add(entidadHijo);
                    }
                }
                if (objetoBO.ModeloGeneralATrabajo != null && objetoBO.ModeloGeneralATrabajo.Count > 0)
                {
                    foreach (var hijo in objetoBO.ModeloGeneralATrabajo)
                    {
                        TModeloGeneralAtrabajo entidadHijo = new TModeloGeneralAtrabajo();
                        entidadHijo = Mapper.Map<ModeloGeneralATrabajoBO, TModeloGeneralAtrabajo>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TModeloGeneralAtrabajo.Add(entidadHijo);
                    }
                }
                if (objetoBO.ModeloGeneralCategoriaDato != null && objetoBO.ModeloGeneralCategoriaDato.Count > 0)
                {
                    foreach (var hijo in objetoBO.ModeloGeneralCategoriaDato)
                    {
                        TModeloGeneralCategoriaDato entidadHijo = new TModeloGeneralCategoriaDato();
                        entidadHijo = Mapper.Map<ModeloGeneralCategoriaDatoBO, TModeloGeneralCategoriaDato>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TModeloGeneralCategoriaDato.Add(entidadHijo);
                    }
                }
                if (objetoBO.ModeloGeneralTipoDato != null && objetoBO.ModeloGeneralTipoDato.Count > 0)
                {
                    foreach (var hijo in objetoBO.ModeloGeneralTipoDato)
                    {
                        TModeloGeneralTipoDato entidadHijo = new TModeloGeneralTipoDato();
                        entidadHijo = Mapper.Map<ModeloGeneralTipoDatoBO, TModeloGeneralTipoDato>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TModeloGeneralTipoDato.Add(entidadHijo);
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
        /// Obtiene todos los registros sin los campos de auditoria
        /// </summary>
        /// <returns></returns>
        public List<ModeloGeneralDTO> ObtenerTodoGrid()
        {
            try
            {
                var lista = GetBy(x => true, y => new ModeloGeneralDTO
                {
                    Id = y.Id,
                    Nombre = y.Nombre,
                    PeIntercepto = y.PeIntercepto,
                    PeEstado = y.PeEstado,
                    IdPadre = y.IdPadre,
                    PeVersion = y.PeVersion
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
