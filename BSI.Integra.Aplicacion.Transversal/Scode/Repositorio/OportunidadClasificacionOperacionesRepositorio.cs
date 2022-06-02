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
    public class OportunidadClasificacionOperacionesRepositorio : BaseRepository<TOportunidadClasificacionOperaciones, OportunidadClasificacionOperacionesBO>
    {
        #region Metodos Base
        public OportunidadClasificacionOperacionesRepositorio() : base()
        {
        }
        public OportunidadClasificacionOperacionesRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<OportunidadClasificacionOperacionesBO> GetBy(Expression<Func<TOportunidadClasificacionOperaciones, bool>> filter)
        {
            IEnumerable<TOportunidadClasificacionOperaciones> listado = base.GetBy(filter);
            List<OportunidadClasificacionOperacionesBO> listadoBO = new List<OportunidadClasificacionOperacionesBO>();
            foreach (var itemEntidad in listado)
            {
                OportunidadClasificacionOperacionesBO objetoBO = Mapper.Map<TOportunidadClasificacionOperaciones, OportunidadClasificacionOperacionesBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public OportunidadClasificacionOperacionesBO FirstById(int id)
        {
            try
            {
                TOportunidadClasificacionOperaciones entidad = base.FirstById(id);
                OportunidadClasificacionOperacionesBO objetoBO = new OportunidadClasificacionOperacionesBO();
                Mapper.Map<TOportunidadClasificacionOperaciones, OportunidadClasificacionOperacionesBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public OportunidadClasificacionOperacionesBO FirstBy(Expression<Func<TOportunidadClasificacionOperaciones, bool>> filter)
        {
            try
            {
                TOportunidadClasificacionOperaciones entidad = base.FirstBy(filter);
                OportunidadClasificacionOperacionesBO objetoBO = Mapper.Map<TOportunidadClasificacionOperaciones, OportunidadClasificacionOperacionesBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(OportunidadClasificacionOperacionesBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TOportunidadClasificacionOperaciones entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<OportunidadClasificacionOperacionesBO> listadoBO)
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

        public bool Update(OportunidadClasificacionOperacionesBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TOportunidadClasificacionOperaciones entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<OportunidadClasificacionOperacionesBO> listadoBO)
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
        private void AsignacionId(TOportunidadClasificacionOperaciones entidad, OportunidadClasificacionOperacionesBO objetoBO)
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

        private TOportunidadClasificacionOperaciones MapeoEntidad(OportunidadClasificacionOperacionesBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TOportunidadClasificacionOperaciones entidad = new TOportunidadClasificacionOperaciones();
                entidad = Mapper.Map<OportunidadClasificacionOperacionesBO, TOportunidadClasificacionOperaciones>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<OportunidadClasificacionOperacionesBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TOportunidadClasificacionOperaciones, bool>>> filters, Expression<Func<TOportunidadClasificacionOperaciones, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TOportunidadClasificacionOperaciones> listado = base.GetFiltered(filters, orderBy, ascending);
            List<OportunidadClasificacionOperacionesBO> listadoBO = new List<OportunidadClasificacionOperacionesBO>();

            foreach (var itemEntidad in listado)
            {
                OportunidadClasificacionOperacionesBO objetoBO = Mapper.Map<TOportunidadClasificacionOperaciones, OportunidadClasificacionOperacionesBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion

        public AvanceAlumnoDTO ObtenerAvanceAlumno(int IdMatriculaCabecera)
        {
            try
            {
                AvanceAlumnoDTO ObtenerEstadoAlumno = new AvanceAlumnoDTO();

                var ObtenerEstadoAlumnoDB = _dapper.QuerySPFirstOrDefault("[pla.SP_ObtenerEstadoAlumno]", new { IdMatriculaCabecera = IdMatriculaCabecera });
                if (!string.IsNullOrEmpty(ObtenerEstadoAlumnoDB) && !ObtenerEstadoAlumnoDB.Contains("[]"))
                {
                    ObtenerEstadoAlumno = JsonConvert.DeserializeObject<AvanceAlumnoDTO>(ObtenerEstadoAlumnoDB);
                }
                return ObtenerEstadoAlumno;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
