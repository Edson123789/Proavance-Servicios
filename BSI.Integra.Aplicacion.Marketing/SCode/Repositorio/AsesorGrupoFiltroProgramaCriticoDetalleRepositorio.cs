using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
    public class AsesorGrupoFiltroProgramaCriticoDetalleRepositorio : BaseRepository<TAsesorGrupoFiltroProgramaCriticoDetalle, AsesorGrupoFiltroProgramaCriticoDetalleBO>
    {
        #region Metodos Base
        public AsesorGrupoFiltroProgramaCriticoDetalleRepositorio() : base()
        {
        }
        public AsesorGrupoFiltroProgramaCriticoDetalleRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<AsesorGrupoFiltroProgramaCriticoDetalleBO> GetBy(Expression<Func<TAsesorGrupoFiltroProgramaCriticoDetalle, bool>> filter)
        {
            IEnumerable<TAsesorGrupoFiltroProgramaCriticoDetalle> listado = base.GetBy(filter);
            List<AsesorGrupoFiltroProgramaCriticoDetalleBO> listadoBO = new List<AsesorGrupoFiltroProgramaCriticoDetalleBO>();
            foreach (var itemEntidad in listado)
            {
                AsesorGrupoFiltroProgramaCriticoDetalleBO objetoBO = Mapper.Map<TAsesorGrupoFiltroProgramaCriticoDetalle, AsesorGrupoFiltroProgramaCriticoDetalleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public AsesorGrupoFiltroProgramaCriticoDetalleBO FirstById(int id)
        {
            try
            {
                TAsesorGrupoFiltroProgramaCriticoDetalle entidad = base.FirstById(id);
                AsesorGrupoFiltroProgramaCriticoDetalleBO objetoBO = new AsesorGrupoFiltroProgramaCriticoDetalleBO();
                Mapper.Map<TAsesorGrupoFiltroProgramaCriticoDetalle, AsesorGrupoFiltroProgramaCriticoDetalleBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public AsesorGrupoFiltroProgramaCriticoDetalleBO FirstBy(Expression<Func<TAsesorGrupoFiltroProgramaCriticoDetalle, bool>> filter)
        {
            try
            {
                TAsesorGrupoFiltroProgramaCriticoDetalle entidad = base.FirstBy(filter);
                AsesorGrupoFiltroProgramaCriticoDetalleBO objetoBO = Mapper.Map<TAsesorGrupoFiltroProgramaCriticoDetalle, AsesorGrupoFiltroProgramaCriticoDetalleBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(AsesorGrupoFiltroProgramaCriticoDetalleBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TAsesorGrupoFiltroProgramaCriticoDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<AsesorGrupoFiltroProgramaCriticoDetalleBO> listadoBO)
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

        public bool Update(AsesorGrupoFiltroProgramaCriticoDetalleBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TAsesorGrupoFiltroProgramaCriticoDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<AsesorGrupoFiltroProgramaCriticoDetalleBO> listadoBO)
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
        private void AsignacionId(TAsesorGrupoFiltroProgramaCriticoDetalle entidad, AsesorGrupoFiltroProgramaCriticoDetalleBO objetoBO)
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

        private TAsesorGrupoFiltroProgramaCriticoDetalle MapeoEntidad(AsesorGrupoFiltroProgramaCriticoDetalleBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TAsesorGrupoFiltroProgramaCriticoDetalle entidad = new TAsesorGrupoFiltroProgramaCriticoDetalle();
                entidad = Mapper.Map<AsesorGrupoFiltroProgramaCriticoDetalleBO, TAsesorGrupoFiltroProgramaCriticoDetalle>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<AsesorGrupoFiltroProgramaCriticoDetalleBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TAsesorGrupoFiltroProgramaCriticoDetalle, bool>>> filters, Expression<Func<TAsesorGrupoFiltroProgramaCriticoDetalle, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TAsesorGrupoFiltroProgramaCriticoDetalle> listado = base.GetFiltered(filters, orderBy, ascending);
            List<AsesorGrupoFiltroProgramaCriticoDetalleBO> listadoBO = new List<AsesorGrupoFiltroProgramaCriticoDetalleBO>();

            foreach (var itemEntidad in listado)
            {
                AsesorGrupoFiltroProgramaCriticoDetalleBO objetoBO = Mapper.Map<TAsesorGrupoFiltroProgramaCriticoDetalle, AsesorGrupoFiltroProgramaCriticoDetalleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion

        /// <summary>
        /// Obtiene los datos de un filtro programa critico
        /// </summary>
        /// <returns>Lista de todo el personal la informacion respectiva para el modulo de Asesor Centro Costo</returns>
        public List<FiltroBasicoDTO> ObtenerGrupoFiltroProgramaCriticoFiltro()
        {
            try
            {
                List<FiltroBasicoDTO> grupoFiltroProgramaCritico = new List<FiltroBasicoDTO>();
                var query = "SELECT Id, Nombre FROM pla.V_TGrupoFiltroProgramaCritico_Filtro";
                var personalDB = _dapper.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(personalDB))
                {
                    grupoFiltroProgramaCritico = JsonConvert.DeserializeObject<List<FiltroBasicoDTO>>(personalDB);
                }
                return grupoFiltroProgramaCritico;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
