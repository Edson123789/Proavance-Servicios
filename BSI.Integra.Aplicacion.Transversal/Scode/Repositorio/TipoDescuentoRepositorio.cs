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
    public class TipoDescuentoRepositorio : BaseRepository<TTipoDescuento, TipoDescuentoBO>
    {
        #region Metodos Base
        public TipoDescuentoRepositorio() : base()
        {
        }
        public TipoDescuentoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<TipoDescuentoBO> GetBy(Expression<Func<TTipoDescuento, bool>> filter)
        {
            IEnumerable<TTipoDescuento> listado = base.GetBy(filter);
            List<TipoDescuentoBO> listadoBO = new List<TipoDescuentoBO>();
            foreach (var itemEntidad in listado)
            {
                TipoDescuentoBO objetoBO = Mapper.Map<TTipoDescuento, TipoDescuentoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public TipoDescuentoBO FirstById(int id)
        {
            try
            {
                TTipoDescuento entidad = base.FirstById(id);
                TipoDescuentoBO objetoBO = new TipoDescuentoBO();
                Mapper.Map<TTipoDescuento, TipoDescuentoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public TipoDescuentoBO FirstBy(Expression<Func<TTipoDescuento, bool>> filter)
        {
            try
            {
                TTipoDescuento entidad = base.FirstBy(filter);
                TipoDescuentoBO objetoBO = Mapper.Map<TTipoDescuento, TipoDescuentoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(TipoDescuentoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TTipoDescuento entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<TipoDescuentoBO> listadoBO)
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

        public bool Update(TipoDescuentoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TTipoDescuento entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<TipoDescuentoBO> listadoBO)
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
        private void AsignacionId(TTipoDescuento entidad, TipoDescuentoBO objetoBO)
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

        private TTipoDescuento MapeoEntidad(TipoDescuentoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TTipoDescuento entidad = new TTipoDescuento();
                entidad = Mapper.Map<TipoDescuentoBO, TTipoDescuento>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos
                if (objetoBO.TipoDescuentoAsesorCoordinadorPw != null && objetoBO.TipoDescuentoAsesorCoordinadorPw.Count > 0)
                {
                    foreach (var hijo in objetoBO.TipoDescuentoAsesorCoordinadorPw)
                    {
                        TTipoDescuentoAsesorCoordinadorPw entidadHijo = new TTipoDescuentoAsesorCoordinadorPw();
                        entidadHijo = Mapper.Map<TipoDescuentoAsesorCoordinadorPwBO, TTipoDescuentoAsesorCoordinadorPw>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TTipoDescuentoAsesorCoordinadorPw.Add(entidadHijo);
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
        /// Obtiene los tipo Descuento(activos) registrado en el sistema
        /// </summary>
        /// <returns></returns>
        public List<TipoDescuentoFiltroDTO> ObtenerTodoFiltro()
        {
            try
            {
                List<TipoDescuentoFiltroDTO> items = new List<TipoDescuentoFiltroDTO>();
                string _query = string.Empty;
                _query = "SELECT Id,Codigo,Descripcion FROM pla.V_TTipoDescuento_Filtro WHERE Estado=1";
                var query = _dapper.QueryDapper(_query, new { });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<TipoDescuentoFiltroDTO>>(query);
                }
                return items;
            }
            catch (Exception E)
            {
                throw new Exception(E.Message);
            }
        }

        /// <summary>
        /// Obtiene todos los registros sin los campos de auditoría.
        /// </summary>
        /// <returns></returns>
        public List<TipoDescuentoDTO> ObtenerTodoGrid()
        {
            try
            {
                var lista = GetBy(x => true, y => new TipoDescuentoDTO
                {
                    Id = y.Id,
                    Codigo = y.Codigo,
                    Descripcion = y.Descripcion,
                    Formula = y.Formula,
                    PorcentajeGeneral = y.PorcentajeGeneral,
                    PorcentajeMatricula = y.PorcentajeMatricula,
                    FraccionesMatricula = y.FraccionesMatricula,
                    PorcentajeCuotas = y.PorcentajeCuotas,
                    CuotasAdicionales = y.CuotasAdicionales,
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
