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
    public class ProgramaGeneralPuntoCorteRepositorio : BaseRepository<TProgramaGeneralPuntoCorte, ProgramaGeneralPuntoCorteBO>
    {
        /// Repositorio: Planificacion/ProgramaGeneral
        /// Autor: Carlos Crispin R.
        /// Fecha: 04/03/2021
        /// <summary>
        /// Permite obtener,insertar,eliminar y demas de las tabla t_pgeneral y tablas asociads
        /// </summary>

        #region Metodos Base
        public ProgramaGeneralPuntoCorteRepositorio() : base()
        {
        }
        public ProgramaGeneralPuntoCorteRepositorio(integraDBContext contexto) : base(contexto)
        {
        }

        public IEnumerable<ProgramaGeneralPuntoCorteBO> GetBy(Expression<Func<TProgramaGeneralPuntoCorte, bool>> filter)
        {
            IEnumerable<TProgramaGeneralPuntoCorte> listado = base.GetBy(filter);
            List<ProgramaGeneralPuntoCorteBO> listadoBO = new List<ProgramaGeneralPuntoCorteBO>();
            foreach (var itemEntidad in listado)
            {
                ProgramaGeneralPuntoCorteBO objetoBO = Mapper.Map<TProgramaGeneralPuntoCorte, ProgramaGeneralPuntoCorteBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ProgramaGeneralPuntoCorteBO FirstById(int id)
        {
            try
            {
                TProgramaGeneralPuntoCorte entidad = base.FirstById(id);
                ProgramaGeneralPuntoCorteBO objetoBO = new ProgramaGeneralPuntoCorteBO();
                Mapper.Map<TProgramaGeneralPuntoCorte, ProgramaGeneralPuntoCorteBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ProgramaGeneralPuntoCorteBO FirstBy(Expression<Func<TProgramaGeneralPuntoCorte, bool>> filter)
        {
            try
            {
                TProgramaGeneralPuntoCorte entidad = base.FirstBy(filter);
                ProgramaGeneralPuntoCorteBO objetoBO = Mapper.Map<TProgramaGeneralPuntoCorte, ProgramaGeneralPuntoCorteBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ProgramaGeneralPuntoCorteBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TProgramaGeneralPuntoCorte entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ProgramaGeneralPuntoCorteBO> listadoBO)
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

        public bool Update(ProgramaGeneralPuntoCorteBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TProgramaGeneralPuntoCorte entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ProgramaGeneralPuntoCorteBO> listadoBO)
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
        private void AsignacionId(TProgramaGeneralPuntoCorte entidad, ProgramaGeneralPuntoCorteBO objetoBO)
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

        private TProgramaGeneralPuntoCorte MapeoEntidad(ProgramaGeneralPuntoCorteBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TProgramaGeneralPuntoCorte entidad = new TProgramaGeneralPuntoCorte();
                entidad = Mapper.Map<ProgramaGeneralPuntoCorteBO, TProgramaGeneralPuntoCorte>(objetoBO,
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

        /// Autor: Carlos Crispin R.
        /// Fecha: 04/03/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene los registros de la tabla pla.t_ProgramaGeneralPuntoCorte
        /// </summary>
        /// <returns>List<ProgramaGeneralPuntoCorteDTO></returns>
        public List<ProgramaGeneralPuntoCorteDTO> ObtenerPuntoCorteGrid()
        {
            try
            {
                List<ProgramaGeneralPuntoCorteDTO> listaPuntoCorte = new List<ProgramaGeneralPuntoCorteDTO>();
                var query = string.Empty;
                query = "SELECT Id, IdProgramaGeneral ,NombreProgramaGeneral, PuntoCorteMedia, PuntoCorteAlta, PuntoCorteMuyAlta, Usuario FROM [pla].[V_ObtenerProgramaGeneralPuntoCorte]";
                var puntosCorte = _dapper.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(puntosCorte) && !puntosCorte.Contains("[]"))
                {
                    listaPuntoCorte = JsonConvert.DeserializeObject<List<ProgramaGeneralPuntoCorteDTO>>(puntosCorte);
                }
                return listaPuntoCorte;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Gian Miranda
        /// Fecha: 06/07/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene la lista de programas generales con su punto de corte respectivo
        /// </summary>
        /// <param name="filtroProgramaGeneralPuntoCorte">Objeto de clase ProgramaGeneralPuntoCorteFiltroDTO</param>
        /// <returns>Lista de objetos de clase ProgramaGeneralPuntoCorteAreaSubAreaDTO</returns>
        public List<ProgramaGeneralPuntoCorteAreaSubAreaDTO> ObtenerListaProgramaGeneralPuntoCorte(ProgramaGeneralPuntoCorteFiltroDTO filtroProgramaGeneralPuntoCorte)
        {
            try
            {
                List<ProgramaGeneralPuntoCorteAreaSubAreaDTO> listaPuntoCorte = new List<ProgramaGeneralPuntoCorteAreaSubAreaDTO>();
                string queryObtenerProgramaGeneralPuntoCorte = "SELECT IdProgramaGeneralPuntoCorte, IdProgramaGeneral, NombreProgramaGeneral, PuntoCorteMedia, PuntoCorteAlta, PuntoCorteMuyAlta," +
                                                                "Usuario, IdAreaCapacitacion, IdSubAreaCapacitacion FROM pla.V_ObtenerProgramaGeneralPuntoCorteAreaSubArea";

                string condicionAdicional = string.Empty;

                condicionAdicional += filtroProgramaGeneralPuntoCorte.ListaIdAreaCapacitacion.Any() ? " AND IdAreaCapacitacion IN @ListaIdAreaCapacitacion" : string.Empty;
                condicionAdicional += filtroProgramaGeneralPuntoCorte.ListaIdSubAreaCapacitacion.Any() ? " AND IdSubAreaCapacitacion IN @ListaIdSubAreaCapacitacion" : string.Empty;
                condicionAdicional += filtroProgramaGeneralPuntoCorte.ListaIdProgramaGeneral.Any() ? " AND IdProgramaGeneral IN @ListaIdProgramaGeneral" : string.Empty;
                condicionAdicional += filtroProgramaGeneralPuntoCorte.ActivoProgramaGeneral != null ? " AND ActivoProgramaGeneral = @ActivoProgramaGeneral" : string.Empty;
                condicionAdicional = condicionAdicional.Length > 0 ? string.Concat(" WHERE", condicionAdicional.Substring(4)) : string.Empty;

                queryObtenerProgramaGeneralPuntoCorte += condicionAdicional;

                var queryRespuesta = _dapper.QueryDapper(queryObtenerProgramaGeneralPuntoCorte, new { filtroProgramaGeneralPuntoCorte.ListaIdAreaCapacitacion, filtroProgramaGeneralPuntoCorte.ListaIdSubAreaCapacitacion, filtroProgramaGeneralPuntoCorte.ListaIdProgramaGeneral, filtroProgramaGeneralPuntoCorte.ActivoProgramaGeneral });

                if (!string.IsNullOrEmpty(queryRespuesta) && !queryObtenerProgramaGeneralPuntoCorte.Contains("[]"))
                {
                    listaPuntoCorte = JsonConvert.DeserializeObject<List<ProgramaGeneralPuntoCorteAreaSubAreaDTO>>(queryRespuesta);
                }
                return listaPuntoCorte;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
