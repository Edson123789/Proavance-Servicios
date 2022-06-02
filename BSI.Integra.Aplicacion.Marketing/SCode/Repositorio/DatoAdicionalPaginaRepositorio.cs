using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System.Linq;
namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
    public class DatoAdicionalPaginaRepositorio : BaseRepository<TDatoAdicionalPagina, DatoAdicionalPaginaBO>
    {
        #region Metodos Base
        public DatoAdicionalPaginaRepositorio() : base()
        {
        }
        public DatoAdicionalPaginaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<DatoAdicionalPaginaBO> GetBy(Expression<Func<TDatoAdicionalPagina, bool>> filter)
        {
            IEnumerable<TDatoAdicionalPagina> listado = base.GetBy(filter);
            List<DatoAdicionalPaginaBO> listadoBO = new List<DatoAdicionalPaginaBO>();
            foreach (var itemEntidad in listado)
            {
                DatoAdicionalPaginaBO objetoBO = Mapper.Map<TDatoAdicionalPagina, DatoAdicionalPaginaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public DatoAdicionalPaginaBO FirstById(int id)
        {
            try
            {
                TDatoAdicionalPagina entidad = base.FirstById(id);
                DatoAdicionalPaginaBO objetoBO = new DatoAdicionalPaginaBO();
                Mapper.Map<TDatoAdicionalPagina, DatoAdicionalPaginaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public DatoAdicionalPaginaBO FirstBy(Expression<Func<TDatoAdicionalPagina, bool>> filter)
        {
            try
            {
                TDatoAdicionalPagina entidad = base.FirstBy(filter);
                DatoAdicionalPaginaBO objetoBO = Mapper.Map<TDatoAdicionalPagina, DatoAdicionalPaginaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(DatoAdicionalPaginaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TDatoAdicionalPagina entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<DatoAdicionalPaginaBO> listadoBO)
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

        public bool Update(DatoAdicionalPaginaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TDatoAdicionalPagina entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<DatoAdicionalPaginaBO> listadoBO)
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
        private void AsignacionId(TDatoAdicionalPagina entidad, DatoAdicionalPaginaBO objetoBO)
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

        private TDatoAdicionalPagina MapeoEntidad(DatoAdicionalPaginaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TDatoAdicionalPagina entidad = new TDatoAdicionalPagina();
                entidad = Mapper.Map<DatoAdicionalPaginaBO, TDatoAdicionalPagina>(objetoBO,
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

        public List<DatoAdicionalPaginaDTO> ObtenerAdicionalesPorFormulario(int IdFormulario)
        {
            try
            {
                var lista = GetBy(x => x.IdFormularioLandingPage == IdFormulario, y => new DatoAdicionalPaginaDTO
                {
                    Id = y.Id,
                    IdTitulo = y.IdTitulo,
                    NombreTitulo = y.NombreTitulo,
                    Descripcion = y.Descripcion,
                    NombreImagen = y.NombreImagen,
                    ColorTitulo = y.ColorTitulo,
                    ColorDescripcion = y.ColorDescripcion
                }).ToList();
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        /// <summary>
        /// Elimina (Actualiza estado a false ) todos los Adicionales por FormularioLandingPage
        /// </summary>
        /// <param name="idFormulario"></param>
        /// <returns></returns>
        public void DeleteLogicoPorPrograma(int idFormulario, string usuario, List<DatoAdicionalPaginaDTO> nuevos)
        {
            try
            {
                var listaBorrar = GetBy(x => x.IdFormularioLandingPage == idFormulario, y => new EliminacionIdsDTO
                {
                    Id = y.Id
                }).ToList();

                listaBorrar.RemoveAll(x => nuevos.Any(y => y.Id == x.Id));
                foreach (var item in listaBorrar)
                {
                    Delete(item.Id, usuario);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
