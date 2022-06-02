using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class SeguimientoPreProcesoListaWhatsAppRepositorio : BaseRepository<TSeguimientoPreProcesoListaWhatsApp, SeguimientoPreProcesoListaWhatsAppBO>
    {
        #region Metodos Base
        public SeguimientoPreProcesoListaWhatsAppRepositorio() : base()
        {
        }
        public SeguimientoPreProcesoListaWhatsAppRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<SeguimientoPreProcesoListaWhatsAppBO> GetBy(Expression<Func<TSeguimientoPreProcesoListaWhatsApp, bool>> filter)
        {
            IEnumerable<TSeguimientoPreProcesoListaWhatsApp> listado = base.GetBy(filter);
            List<SeguimientoPreProcesoListaWhatsAppBO> listadoBO = new List<SeguimientoPreProcesoListaWhatsAppBO>();
            foreach (var itemEntidad in listado)
            {
                SeguimientoPreProcesoListaWhatsAppBO objetoBO = Mapper.Map<TSeguimientoPreProcesoListaWhatsApp, SeguimientoPreProcesoListaWhatsAppBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public SeguimientoPreProcesoListaWhatsAppBO FirstById(int id)
        {
            try
            {
                TSeguimientoPreProcesoListaWhatsApp entidad = base.FirstById(id);
                SeguimientoPreProcesoListaWhatsAppBO objetoBO = new SeguimientoPreProcesoListaWhatsAppBO();
                Mapper.Map<TSeguimientoPreProcesoListaWhatsApp, SeguimientoPreProcesoListaWhatsAppBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public SeguimientoPreProcesoListaWhatsAppBO FirstBy(Expression<Func<TSeguimientoPreProcesoListaWhatsApp, bool>> filter)
        {
            try
            {
                TSeguimientoPreProcesoListaWhatsApp entidad = base.FirstBy(filter);
                SeguimientoPreProcesoListaWhatsAppBO objetoBO = Mapper.Map<TSeguimientoPreProcesoListaWhatsApp, SeguimientoPreProcesoListaWhatsAppBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(SeguimientoPreProcesoListaWhatsAppBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TSeguimientoPreProcesoListaWhatsApp entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<SeguimientoPreProcesoListaWhatsAppBO> listadoBO)
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

        public bool Update(SeguimientoPreProcesoListaWhatsAppBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TSeguimientoPreProcesoListaWhatsApp entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<SeguimientoPreProcesoListaWhatsAppBO> listadoBO)
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
        private void AsignacionId(TSeguimientoPreProcesoListaWhatsApp entidad, SeguimientoPreProcesoListaWhatsAppBO objetoBO)
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

        private TSeguimientoPreProcesoListaWhatsApp MapeoEntidad(SeguimientoPreProcesoListaWhatsAppBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TSeguimientoPreProcesoListaWhatsApp entidad = new TSeguimientoPreProcesoListaWhatsApp();
                entidad = Mapper.Map<SeguimientoPreProcesoListaWhatsAppBO, TSeguimientoPreProcesoListaWhatsApp>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<SeguimientoPreProcesoListaWhatsAppBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TSeguimientoPreProcesoListaWhatsApp, bool>>> filters, Expression<Func<TSeguimientoPreProcesoListaWhatsApp, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TSeguimientoPreProcesoListaWhatsApp> listado = base.GetFiltered(filters, orderBy, ascending);
            List<SeguimientoPreProcesoListaWhatsAppBO> listadoBO = new List<SeguimientoPreProcesoListaWhatsAppBO>();

            foreach (var itemEntidad in listado)
            {
                SeguimientoPreProcesoListaWhatsAppBO objetoBO = Mapper.Map<TSeguimientoPreProcesoListaWhatsApp, SeguimientoPreProcesoListaWhatsAppBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion

        /// <summary>
        /// Inserta SeguimientoPreProcesoListaWhatsAppBO mediante un SP para llamarlo desde replica
        /// </summary>
        /// <param name="nuevoSeguimientoPreProcesoListaWhatsApp">Objeto de tipo SeguimientoPreProcesoListaWhatsAppBO</param>
        /// <returns>Entero con el Id del scope</returns>
        public int InsertarSeguimientoPreProcesoCampaniaGeneral(SeguimientoPreProcesoListaWhatsAppBO nuevoSeguimientoPreProcesoListaWhatsApp)
        {
            try
            {
                SeguimientoPreProcesoListaWhatsAppBO objResultado = new SeguimientoPreProcesoListaWhatsAppBO();

                string spQuery = "[mkt].[SP_InsertarSeguimientoPreProcesoCampaniaGeneral]";
                var query = _dapper.QuerySPFirstOrDefault(spQuery, new
                {
                    nuevoSeguimientoPreProcesoListaWhatsApp.IdEstadoSeguimientoPreProcesoListaWhatsApp,
                    nuevoSeguimientoPreProcesoListaWhatsApp.IdConjuntoLista,
                    nuevoSeguimientoPreProcesoListaWhatsApp.IdCampaniaGeneralDetalle,
                    nuevoSeguimientoPreProcesoListaWhatsApp.UsuarioCreacion,
                    nuevoSeguimientoPreProcesoListaWhatsApp.UsuarioModificacion
                });
                if (!string.IsNullOrEmpty(query))
                {
                    objResultado = JsonConvert.DeserializeObject<SeguimientoPreProcesoListaWhatsAppBO>(query);
                }

                return objResultado.Id;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Actualiza Campania Mailing detalle, datos (CantidadContactos, UsuarioModificacion, FechaModificacion)
        /// </summary>
        /// <param name="campaniaGeneralDetalleActualizacion">Objeto de tipo CampaniaGeneralDetalleActualizacionDTO</param>
        /// <returns></returns>
        public void ActualizarSeguimientoPreProcesoCampaniaGeneral(int idSeguimientoPreProceso, int idEstadoSeguimientoPreProcesoListaWhatsApp)
        {
            try
            {
                string spQuery = "[mkt].[SP_ActualizarSeguimientoPreProcesoListaWhatsApp]";
                var query = _dapper.QuerySPDapper(spQuery, new { IdEstadoSeguimientoPreProcesoListaWhatsApp = idEstadoSeguimientoPreProcesoListaWhatsApp, Id = idSeguimientoPreProceso });
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Eliminar Preproceso anterior de la misma campaniaGeneralDetalle
        /// </summary>
        /// <param name="idCampaniaGeneralDetalle">Id del detalle de la campania general (PK de la tabla mkt.T_CampaniaGeneralDetalle)</param>
        /// <returns></returns>
        public void EliminarSeguimientoPreProcesoCampaniaGeneral(int idCampaniaGeneralDetalle)
        {
            try
            {
                string spQuery = "[mkt].[SP_EliminarSeguimientoPreProcesoPorIdCampaniaGeneralDetalle]";
                var query = _dapper.QuerySPDapper(spQuery, new { IdCampaniaGeneralDetalle = idCampaniaGeneralDetalle });
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
