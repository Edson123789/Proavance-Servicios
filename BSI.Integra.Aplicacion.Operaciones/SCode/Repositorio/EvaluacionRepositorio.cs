using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Operaciones.Repositorio
{
    public class EvaluacionRepositorio : BaseRepository<TEvaluacion, EvaluacionBO>
    {
        #region Metodos Base
        public EvaluacionRepositorio() : base()
        {
        }
        public EvaluacionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<EvaluacionBO> GetBy(Expression<Func<TEvaluacion, bool>> filter)
        {
            IEnumerable<TEvaluacion> listado = base.GetBy(filter);
            List<EvaluacionBO> listadoBO = new List<EvaluacionBO>();
            foreach (var itemEntidad in listado)
            {
                EvaluacionBO objetoBO = Mapper.Map<TEvaluacion, EvaluacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public EvaluacionBO FirstById(int id)
        {
            try
            {
                TEvaluacion entidad = base.FirstById(id);
                EvaluacionBO objetoBO = new EvaluacionBO();
                Mapper.Map<TEvaluacion, EvaluacionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public EvaluacionBO FirstBy(Expression<Func<TEvaluacion, bool>> filter)
        {
            try
            {
                TEvaluacion entidad = base.FirstBy(filter);
                EvaluacionBO objetoBO = Mapper.Map<TEvaluacion, EvaluacionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(EvaluacionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TEvaluacion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<EvaluacionBO> listadoBO)
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

        public bool Update(EvaluacionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TEvaluacion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<EvaluacionBO> listadoBO)
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
        private void AsignacionId(TEvaluacion entidad, EvaluacionBO objetoBO)
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

        private TEvaluacion MapeoEntidad(EvaluacionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TEvaluacion entidad = new TEvaluacion();
                entidad = Mapper.Map<EvaluacionBO, TEvaluacion>(objetoBO,
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

        public List<EvaluacionListadoDTO> ListadoPorPrograma(int idPespecifico, int grupo)
        {
            try
            {
                return this.GetBy(x => x.Estado == true && x.IdPespecifico == idPespecifico && x.Grupo == grupo,
                    x => new EvaluacionListadoDTO
                    {
                        Id = x.Id, IdPEspecifico = x.IdPespecifico, Grupo = x.Grupo, Nombre = x.Nombre,
                        IdCriterioEvaluacion = x.IdCriterioEvaluacion, Porcentaje = x.Porcentaje
                    }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool InsertarDesdePadre(List<PGeneralCriterioEvaluacionDTO> listadoCriteriosPadre, int idPespecifico, int grupo)
        {
            bool respuesta = false;
            try
            {
                if (listadoCriteriosPadre != null && listadoCriteriosPadre.Count > 0)
                {
                    List<EvaluacionBO> listado = new List<EvaluacionBO>();
                    foreach (var criterio in listadoCriteriosPadre)
                    {
                        EvaluacionBO item = new EvaluacionBO()
                        {
                            Nombre = criterio.Nombre,
                            IdPespecifico = idPespecifico,
                            Grupo = grupo,
                            Porcentaje = criterio.Porcentaje,
                            IdCriterioEvaluacion = criterio.IdCriterioEvaluacion,

                            Estado = true, UsuarioCreacion = "SYSTEM", UsuarioModificacion = "SYSTEM",
                            FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now
                        };
                        listado.Add(item);
                    }

                    respuesta = Insert(listado);
                }

                return respuesta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
