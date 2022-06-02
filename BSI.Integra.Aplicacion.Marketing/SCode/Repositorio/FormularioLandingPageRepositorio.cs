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
    public class FormularioLandingPageRepositorio : BaseRepository<TFormularioLandingPage, FormularioLandingPageBO>
    {
        #region Metodos Base
        public FormularioLandingPageRepositorio() : base()
        {
        }
        public FormularioLandingPageRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<FormularioLandingPageBO> GetBy(Expression<Func<TFormularioLandingPage, bool>> filter)
        {
            IEnumerable<TFormularioLandingPage> listado = base.GetBy(filter);
            List<FormularioLandingPageBO> listadoBO = new List<FormularioLandingPageBO>();
            foreach (var itemEntidad in listado)
            {
                FormularioLandingPageBO objetoBO = Mapper.Map<TFormularioLandingPage, FormularioLandingPageBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public FormularioLandingPageBO FirstById(int id)
        {
            try
            {
                TFormularioLandingPage entidad = base.FirstById(id);
                FormularioLandingPageBO objetoBO = new FormularioLandingPageBO();
                Mapper.Map<TFormularioLandingPage, FormularioLandingPageBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public FormularioLandingPageBO FirstBy(Expression<Func<TFormularioLandingPage, bool>> filter)
        {
            try
            {
                TFormularioLandingPage entidad = base.FirstBy(filter);
                FormularioLandingPageBO objetoBO = Mapper.Map<TFormularioLandingPage, FormularioLandingPageBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(FormularioLandingPageBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TFormularioLandingPage entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<FormularioLandingPageBO> listadoBO)
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

        public bool Update(FormularioLandingPageBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TFormularioLandingPage entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<FormularioLandingPageBO> listadoBO)
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
        private void AsignacionId(TFormularioLandingPage entidad, FormularioLandingPageBO objetoBO)
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

        private TFormularioLandingPage MapeoEntidad(FormularioLandingPageBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TFormularioLandingPage entidad = new TFormularioLandingPage();
                entidad = Mapper.Map<FormularioLandingPageBO, TFormularioLandingPage>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos
                if (objetoBO.DatoAdicionalPagina != null && objetoBO.DatoAdicionalPagina.Count > 0)
                {

                    foreach (var hijo in objetoBO.DatoAdicionalPagina)
                    {
                        TDatoAdicionalPagina entidadHijo = new TDatoAdicionalPagina();
                        entidadHijo = Mapper.Map<DatoAdicionalPaginaBO, TDatoAdicionalPagina>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TDatoAdicionalPagina.Add(entidadHijo);
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
        /// Obtiene los registros para la Pagina del Grid en la Vista.
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public FormularioLandingPageGridDTO ObtenerPagina(FiltroPaginadorDTO filtro)
        {
            try
            {
                FormularioLandingPageGridDTO grid = new FormularioLandingPageGridDTO();
                if (filtro.FiltroKendo != null && filtro.FiltroKendo.Filters != null)
                {
                    grid.ListaFormulario = GetBy(x => x.Nombre.Contains(filtro.FiltroKendo.Filters[0].Value), y => new FormularioLandingPageDTO
                    {
                        Id = y.Id,
                        Nombre = y.Nombre,
                        Codigo = y.Codigo,
                        IdFormularioSolicitud = y.IdFormularioSolicitud,
                        IdPlantillaLandingPage = y.IdPlantillaLandingPage,
                        IdPespecifico = y.IdPespecifico,
                        TituloPopup = y.TituloPopup,
                        Cita3Texto = y.Cita3Texto,
                        Cita4Texto = y.Cita4Texto,
                        TesteoAb = y.TesteoAb
                    }).OrderByDescending(x => x.Id).Skip(filtro.Skip).Take(filtro.Take).ToList();
                    grid.Total = GetBy(x => x.Nombre.Contains(filtro.FiltroKendo.Filters[0].Value)).Count();
                }
                else
                {
                    grid.ListaFormulario = GetBy(x => true, y => new FormularioLandingPageDTO
                    {
                        Id = y.Id,
                        Nombre = y.Nombre,
                        Codigo = y.Codigo,
                        IdFormularioSolicitud = y.IdFormularioSolicitud,
                        IdPlantillaLandingPage = y.IdPlantillaLandingPage,
                        IdPespecifico = y.IdPespecifico,
                        TituloPopup = y.TituloPopup,
                        Cita3Texto = y.Cita3Texto,
                        Cita4Texto = y.Cita4Texto,
                        TesteoAb = y.TesteoAb
                    }).Where(x => !x.Nombre.Equals("")).OrderByDescending(x => x.Id).Skip(filtro.Skip).Take(filtro.Take).ToList();
                    grid.Total = GetAll().Count();
                }
                return grid;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Insertar Formulario en  el Portal
        /// </summary>
        /// <param name="IdFormulario"></param>
        /// <param name="Usuario"></param>
        public void InsertarFormularioPortal (int IdFormulario, string Usuario, int IdPlantillaLandingPage)
        {
            try
            {
                _dapper.QuerySPFirstOrDefault("mkt.SP_InsertarDatoLandingPange", new { IdFormularioLandingPage = IdFormulario, Usuario = Usuario, IdPlantillaLandingPage = IdPlantillaLandingPage });
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Actualizar Formulario en  el Portal
        /// </summary>
        /// <param name="IdFormulario"></param>
        /// <param name="Usuario"></param>
        public void ActualizarFormularioPortal(int IdFormulario, int IdPlantillaLandingPage)
        {
            try
            {
                _dapper.QuerySPFirstOrDefault("mkt.SP_ActualizarDatoLandingPange", new { IdFormularioLandingPage = IdFormulario, IdPlantillaLandingPage = IdPlantillaLandingPage });
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Elimina un Formulario en  el Portal
        /// </summary>
        /// <param name="IdFormulario"></param>
        /// <param name="Usuario"></param>
        public void EliminarFormularioPortal(int IdFormulario, string Usuario)
        {
            try
            {
                _dapper.QuerySPFirstOrDefault("mkt.SP_EliminarDatoLandingPange", new { IdFormularioLandingPage = IdFormulario, Usuario = Usuario });
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
