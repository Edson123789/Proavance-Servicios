using System;
using System.Collections.Generic;
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
    public class PespecificoFrecuenciaDetalleRepositorio : BaseRepository<TPespecificoFrecuenciaDetalle, PespecificoFrecuenciaDetalleBO>
    {
        #region Metodos Base
        public PespecificoFrecuenciaDetalleRepositorio() : base()
        {
        }
        public PespecificoFrecuenciaDetalleRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PespecificoFrecuenciaDetalleBO> GetBy(Expression<Func<TPespecificoFrecuenciaDetalle, bool>> filter)
        {
            IEnumerable<TPespecificoFrecuenciaDetalle> listado = base.GetBy(filter);
            List<PespecificoFrecuenciaDetalleBO> listadoBO = new List<PespecificoFrecuenciaDetalleBO>();
            foreach (var itemEntidad in listado)
            {
                PespecificoFrecuenciaDetalleBO objetoBO = Mapper.Map<TPespecificoFrecuenciaDetalle, PespecificoFrecuenciaDetalleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PespecificoFrecuenciaDetalleBO FirstById(int id)
        {
            try
            {
                TPespecificoFrecuenciaDetalle entidad = base.FirstById(id);
                PespecificoFrecuenciaDetalleBO objetoBO = new PespecificoFrecuenciaDetalleBO();
                Mapper.Map<TPespecificoFrecuenciaDetalle, PespecificoFrecuenciaDetalleBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PespecificoFrecuenciaDetalleBO FirstBy(Expression<Func<TPespecificoFrecuenciaDetalle, bool>> filter)
        {
            try
            {
                TPespecificoFrecuenciaDetalle entidad = base.FirstBy(filter);
                PespecificoFrecuenciaDetalleBO objetoBO = Mapper.Map<TPespecificoFrecuenciaDetalle, PespecificoFrecuenciaDetalleBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PespecificoFrecuenciaDetalleBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPespecificoFrecuenciaDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PespecificoFrecuenciaDetalleBO> listadoBO)
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

        public bool Update(PespecificoFrecuenciaDetalleBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPespecificoFrecuenciaDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PespecificoFrecuenciaDetalleBO> listadoBO)
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
        private void AsignacionId(TPespecificoFrecuenciaDetalle entidad, PespecificoFrecuenciaDetalleBO objetoBO)
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

        private TPespecificoFrecuenciaDetalle MapeoEntidad(PespecificoFrecuenciaDetalleBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPespecificoFrecuenciaDetalle entidad = new TPespecificoFrecuenciaDetalle();
                entidad = Mapper.Map<PespecificoFrecuenciaDetalleBO, TPespecificoFrecuenciaDetalle>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<PespecificoFrecuenciaDetalleBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TPespecificoFrecuenciaDetalle, bool>>> filters, Expression<Func<TPespecificoFrecuenciaDetalle, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TPespecificoFrecuenciaDetalle> listado = base.GetFiltered(filters, orderBy, ascending);
            List<PespecificoFrecuenciaDetalleBO> listadoBO = new List<PespecificoFrecuenciaDetalleBO>();

            foreach (var itemEntidad in listado)
            {
                PespecificoFrecuenciaDetalleBO objetoBO = Mapper.Map<TPespecificoFrecuenciaDetalle, PespecificoFrecuenciaDetalleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion
        /// <summary>
        /// Obtiene Id de ProgramaEspecificofrecuenciadetalle
        /// </summary>
        /// <param name="idPespecificoFrecuencia"></param>
        /// <returns></returns>
        public List<DatosPespecificoFrecuenciaDetalleDTO> ObtenerFrecuenciaDetallePorIdPespecificoFrecuencia(int idPespecificoFrecuencia)
        {
            string _queryFrecuenciaDetalle = "Select Id,IdPespecificoFrecuencia,DiaSemana,HoraDia,Duracion From pla.V_TPEspecificoFrecuenciadetallePorIdPespecificoFrecuencia Where Estado=1 and IdPEspecificoFrecuencia=@IdPEspecificoFrecuencia";
            var queryFrecuenciaDetalle = _dapper.QueryDapper(_queryFrecuenciaDetalle, new { IdPespecificoFrecuencia = idPespecificoFrecuencia });
            return JsonConvert.DeserializeObject<List<DatosPespecificoFrecuenciaDetalleDTO>>(queryFrecuenciaDetalle);
        }
    }
}
