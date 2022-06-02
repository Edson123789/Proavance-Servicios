using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class FiltroSegmentoDetalleRepositorio : BaseRepository<TFiltroSegmentoDetalle, FiltroSegmentoDetalleBO>
    {
        #region Metodos Base
        public FiltroSegmentoDetalleRepositorio() : base()
        {
        }
        public FiltroSegmentoDetalleRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<FiltroSegmentoDetalleBO> GetBy(Expression<Func<TFiltroSegmentoDetalle, bool>> filter)
        {
            IEnumerable<TFiltroSegmentoDetalle> listado = base.GetBy(filter);
            List<FiltroSegmentoDetalleBO> listadoBO = new List<FiltroSegmentoDetalleBO>();
            foreach (var itemEntidad in listado)
            {
                FiltroSegmentoDetalleBO objetoBO = Mapper.Map<TFiltroSegmentoDetalle, FiltroSegmentoDetalleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public FiltroSegmentoDetalleBO FirstById(int id)
        {
            try
            {
                TFiltroSegmentoDetalle entidad = base.FirstById(id);
                FiltroSegmentoDetalleBO objetoBO = new FiltroSegmentoDetalleBO();
                Mapper.Map<TFiltroSegmentoDetalle, FiltroSegmentoDetalleBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public FiltroSegmentoDetalleBO FirstBy(Expression<Func<TFiltroSegmentoDetalle, bool>> filter)
        {
            try
            {
                TFiltroSegmentoDetalle entidad = base.FirstBy(filter);
                FiltroSegmentoDetalleBO objetoBO = Mapper.Map<TFiltroSegmentoDetalle, FiltroSegmentoDetalleBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(FiltroSegmentoDetalleBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TFiltroSegmentoDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<FiltroSegmentoDetalleBO> listadoBO)
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

        public bool Update(FiltroSegmentoDetalleBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TFiltroSegmentoDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<FiltroSegmentoDetalleBO> listadoBO)
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
        private void AsignacionId(TFiltroSegmentoDetalle entidad, FiltroSegmentoDetalleBO objetoBO)
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

        private TFiltroSegmentoDetalle MapeoEntidad(FiltroSegmentoDetalleBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TFiltroSegmentoDetalle entidad = new TFiltroSegmentoDetalle();
                entidad = Mapper.Map<FiltroSegmentoDetalleBO, TFiltroSegmentoDetalle>(objetoBO,
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

        /// <summary>
        /// Elimina los registros que no deben persistir en base de datos
        /// </summary>
        /// <param name="filtro"></param>
        public void EliminacionLogica(FiltroSegmentoDTO filtro)
        {
            try
            {
                var listaBorrar = GetBy(x => x.IdFiltroSegmento == filtro.Id && x.Estado == true).ToList();

                listaBorrar.RemoveAll(x => filtro.ListaSesion.Any(y => y.Valor == x.Valor && y.IdOperadorComparacion == x.IdOperadorComparacion && y.IdTiempoFrecuencia == x.IdTiempoFrecuencia && y.CantidadTiempoFrecuencia == x.CantidadTiempoFrecuencia  && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroSesion));
                listaBorrar.RemoveAll(x => filtro.ListaEstadoAcademico.Any(y => y.Valor == x.Valor && y.IdOperadorComparacion == x.IdOperadorComparacion && y.IdTiempoFrecuencia == x.IdTiempoFrecuencia && y.CantidadTiempoFrecuencia == x.CantidadTiempoFrecuencia  && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroEstadoAcademico));
                listaBorrar.RemoveAll(x => filtro.ListaEstadoPago.Any(y => y.Valor == x.Valor && y.IdOperadorComparacion == x.IdOperadorComparacion && y.IdTiempoFrecuencia == x.IdTiempoFrecuencia && y.CantidadTiempoFrecuencia == x.CantidadTiempoFrecuencia  && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroEstadoPago));
                listaBorrar.RemoveAll(x => filtro.ListaEstadoLlamada.Any(y => y.Valor == x.Valor && y.IdOperadorComparacion == x.IdOperadorComparacion && y.IdTiempoFrecuencia == x.IdTiempoFrecuencia && y.CantidadTiempoFrecuencia == x.CantidadTiempoFrecuencia && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroEstadoLlamada));

                listaBorrar.RemoveAll(x => filtro.ListaSesionWebinar.Any(y => y.Valor == x.Valor && y.IdOperadorComparacion == x.IdOperadorComparacion && y.IdTiempoFrecuencia == x.IdTiempoFrecuencia && y.CantidadTiempoFrecuencia == x.CantidadTiempoFrecuencia && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroSesionWebinar));
                listaBorrar.RemoveAll(x => filtro.ListaTrabajoAlumno.Any(y => y.Valor == x.Valor && y.IdOperadorComparacion == x.IdOperadorComparacion && y.IdTiempoFrecuencia == x.IdTiempoFrecuencia && y.CantidadTiempoFrecuencia == x.CantidadTiempoFrecuencia && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroTrabajoAlumno));
                listaBorrar.RemoveAll(x => filtro.ListaTrabajoAlumnoFinal.Any(y => y.Valor == x.Valor && y.IdOperadorComparacion == x.IdOperadorComparacion && y.IdTiempoFrecuencia == x.IdTiempoFrecuencia && y.CantidadTiempoFrecuencia == x.CantidadTiempoFrecuencia && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroTrabajoAlumnoFinal));

                this.Delete(listaBorrar.Select(x => x.Id), filtro.NombreUsuario);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene los detalle por filtro segmento
        /// </summary>
        /// <param name="idFiltroSegmento"></param>
        /// <returns></returns>
        public List<FiltroSegmentoDetalleDTO> ObtenerDetallePorIdFiltroSegmento(int idFiltroSegmento)
        {
            try
            {
                List<FiltroSegmentoDetalleDTO> items = new List<FiltroSegmentoDetalleDTO>();
                string _query = @"
                                SELECT Id, IdCategoriaObjetoFiltro, Valor, IdOperadorComparacion, CantidadTiempoFrecuencia, IdTiempoFrecuencia
                                FROM [mkt].[V_TFiltroSegmentoDetalle_ConfiguracionFiltroSegmento]
                                WHERE Estado = 1 AND IdFiltroSegmento = @idFiltroSegmento";

                string filtroSegmentoDetalleDB = _dapper.QueryDapper(_query, new { idFiltroSegmento });

                items = JsonConvert.DeserializeObject<List<FiltroSegmentoDetalleDTO>>(filtroSegmentoDetalleDB);

                return items;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
