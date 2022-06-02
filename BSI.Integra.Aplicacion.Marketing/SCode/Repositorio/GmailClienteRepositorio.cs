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
using BSI.Integra.Aplicacion.Transversal.Helper;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
    /// Repositorio: Marketing/GmailCliente
    /// Autor: Luis Huallpa - Jose V.
    /// Fecha: 28/04/2021
    /// <summary>
    /// Repositorio para consultas de gp.T_GmailCliente
    /// </summary>
    public class GmailClienteRepositorio : BaseRepository<TGmailCliente, GmailClienteBO>
    {
        #region Metodos Base
        public GmailClienteRepositorio() : base()
        {
        }
        public GmailClienteRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<GmailClienteBO> GetBy(Expression<Func<TGmailCliente, bool>> filter)
        {
            IEnumerable<TGmailCliente> listado = base.GetBy(filter);
            List<GmailClienteBO> listadoBO = new List<GmailClienteBO>();
            foreach (var itemEntidad in listado)
            {
                GmailClienteBO objetoBO = Mapper.Map<TGmailCliente, GmailClienteBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public GmailClienteBO FirstById(int id)
        {
            try
            {
                TGmailCliente entidad = base.FirstById(id);
                GmailClienteBO objetoBO = new GmailClienteBO();
                Mapper.Map<TGmailCliente, GmailClienteBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public GmailClienteBO FirstBy(Expression<Func<TGmailCliente, bool>> filter)
        {
            try
            {
                TGmailCliente entidad = base.FirstBy(filter);
                GmailClienteBO objetoBO = Mapper.Map<TGmailCliente, GmailClienteBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(GmailClienteBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TGmailCliente entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<GmailClienteBO> listadoBO)
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

        public bool Update(GmailClienteBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TGmailCliente entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<GmailClienteBO> listadoBO)
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
        private void AsignacionId(TGmailCliente entidad, GmailClienteBO objetoBO)
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

        private TGmailCliente MapeoEntidad(GmailClienteBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TGmailCliente entidad = new TGmailCliente();
                entidad = Mapper.Map<GmailClienteBO, TGmailCliente>(objetoBO,
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
        /// Obtiene las credenciales del Asesor, para conectarse al Servicio Imap.
        /// </summary>
        /// <param name="idAsesor"></param>
        /// <returns></returns>
        public CorreoClienteCredencialDTO GetClienteCredencial(int idAsesor)
        {
            try
            {
                string query = "select IdAsesor, EmailAsesor, PasswordCorreo From mkt.V_TGmailCliente_CredencialAsesor Where Estado=1 and IdAsesor=@IdAsesor";
                string respuestaQuery = _dapper.FirstOrDefault(query, new { Idasesor = idAsesor });
                if (respuestaQuery != "null" && !respuestaQuery.Contains("{}") && !respuestaQuery.Contains("[]"))
                {
                    CorreoClienteCredencialDTO correoClienteCrendencialDTO = JsonConvert.DeserializeObject<CorreoClienteCredencialDTO>(respuestaQuery);
                    return correoClienteCrendencialDTO;
                }
                return null;
                //throw new Exception(ErrorSistema.Instance.MensajeError(203));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public GmailClienteGridDTO ObtenerRegistrosPorFiltro(FiltroPaginadorDTO filtro)
        {
            try
            {
                GmailClienteGridDTO gmailClienteGridDTO = new GmailClienteGridDTO();
                gmailClienteGridDTO.lista = new List<GmailClienteDTO>();
                var filtroSQL = "Estado = 1";

                if ((filtro.FiltroKendo != null && filtro.FiltroKendo.Filters.Count > 0))
                {
                    foreach (var filterGrid in filtro.FiltroKendo.Filters)
                    {
                        switch (filterGrid.Field)
                        {
                            case "IdAsesor":
                                filtroSQL = filtroSQL + "AND IdAsesor LIKE '%" + filterGrid.Value + "%'";
                                break;
                            case "NombreAsesor":
                                filtroSQL = filtroSQL + "AND NombreAsesor LIKE '%" + filterGrid.Value + "%'";
                                break;
                            case "EmailAsesor":
                                filtroSQL = filtroSQL + "AND EmailAsesor LIKE '%" + filterGrid.Value + "%'";
                                break;
                        }
                    }
                }

                string query = "SELECT Id, IdAsesor, EmailAsesor, NombreAsesor, IdClient, ClientSecret FROM mkt.V_TGmailCliente_Panel WHERE " + filtroSQL + "ORDER BY Id DESC OFFSET @skip ROWS FETCH NEXT @take ROWS ONLY ";
                string respuestaQuery = _dapper.QueryDapper(query, new { skip = filtro.Skip, take = filtro.Take });
                if (respuestaQuery != "null" && !respuestaQuery.Contains("{}") && !respuestaQuery.Contains("[]"))
                {
                    gmailClienteGridDTO.lista = JsonConvert.DeserializeObject<List<GmailClienteDTO>>(respuestaQuery);
                }

                Dictionary<string, int> diccionario = new Dictionary<string, int>();
                query = "SELECT COUNT(*) AS Cantidad FROM mkt.V_TGmailCliente_Panel WHERE " + filtroSQL;
                respuestaQuery = _dapper.FirstOrDefault(query, null);
                if (respuestaQuery != "null" && !respuestaQuery.Contains("{}") && !respuestaQuery.Contains("[]"))
                {
                    diccionario = JsonConvert.DeserializeObject<Dictionary<string, int>>(respuestaQuery);
                    gmailClienteGridDTO.Total = diccionario.GetValueOrDefault("Cantidad");
                }
                
                return gmailClienteGridDTO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
