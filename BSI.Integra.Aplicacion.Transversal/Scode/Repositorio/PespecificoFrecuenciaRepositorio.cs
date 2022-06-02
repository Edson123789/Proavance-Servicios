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
    public class PespecificoFrecuenciaRepositorio : BaseRepository<TPespecificoFrecuencia, PespecificoFrecuenciaBO>
    {
        #region Metodos Base
        public PespecificoFrecuenciaRepositorio() : base()
        {
        }
        public PespecificoFrecuenciaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PespecificoFrecuenciaBO> GetBy(Expression<Func<TPespecificoFrecuencia, bool>> filter)
        {
            IEnumerable<TPespecificoFrecuencia> listado = base.GetBy(filter);
            List<PespecificoFrecuenciaBO> listadoBO = new List<PespecificoFrecuenciaBO>();
            foreach (var itemEntidad in listado)
            {
                PespecificoFrecuenciaBO objetoBO = Mapper.Map<TPespecificoFrecuencia, PespecificoFrecuenciaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PespecificoFrecuenciaBO FirstById(int id)
        {
            try
            {
                TPespecificoFrecuencia entidad = base.FirstById(id);
                PespecificoFrecuenciaBO objetoBO = Mapper.Map<TPespecificoFrecuencia, PespecificoFrecuenciaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PespecificoFrecuenciaBO FirstBy(Expression<Func<TPespecificoFrecuencia, bool>> filter)
        {
            try
            {
                TPespecificoFrecuencia entidad = base.FirstBy(filter);
                PespecificoFrecuenciaBO objetoBO = Mapper.Map<TPespecificoFrecuencia, PespecificoFrecuenciaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PespecificoFrecuenciaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPespecificoFrecuencia entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PespecificoFrecuenciaBO> listadoBO)
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

        public bool Update(PespecificoFrecuenciaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPespecificoFrecuencia entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PespecificoFrecuenciaBO> listadoBO)
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
        private void AsignacionId(TPespecificoFrecuencia entidad, PespecificoFrecuenciaBO objetoBO)
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

        private TPespecificoFrecuencia MapeoEntidad(PespecificoFrecuenciaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPespecificoFrecuencia entidad = new TPespecificoFrecuencia();
                entidad = Mapper.Map<PespecificoFrecuenciaBO, TPespecificoFrecuencia>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<PespecificoFrecuenciaBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TPespecificoFrecuencia, bool>>> filters, Expression<Func<TPespecificoFrecuencia, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TPespecificoFrecuencia> listado = base.GetFiltered(filters, orderBy, ascending);
            List<PespecificoFrecuenciaBO> listadoBO = new List<PespecificoFrecuenciaBO>();

            foreach (var itemEntidad in listado)
            {
                PespecificoFrecuenciaBO objetoBO = Mapper.Map<TPespecificoFrecuencia, PespecificoFrecuenciaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion

        public List<DatosProgramaEspecificoFrecuenciaDTO> ObtenerPespecificoFrecuencia(int idPespecifico)
        {
            try
            {
                string _queryFrecuencia = "Select  Id,IdPEspecifico,FechaInicio,Frecuencia,NroSesiones,IdFrecuencia From pla.V_TPEspecificoFrecuenciaByIdEspecifico Where Estado=1 and IdPespecifico=@IdPespecifico";
                var queryFrecuencia = _dapper.QueryDapper(_queryFrecuencia, new { IdPespecifico = idPespecifico });
                return JsonConvert.DeserializeObject<List<DatosProgramaEspecificoFrecuenciaDTO>>(queryFrecuencia);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
           

        }
    }
}
