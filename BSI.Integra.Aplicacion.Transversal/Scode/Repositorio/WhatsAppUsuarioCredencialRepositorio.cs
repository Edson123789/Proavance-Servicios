using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
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
    /// Repositorio: Marketing/WhatsAppUsuarioCredencial
    /// Autor: Jorge Rivera - Gian Miranda
    /// Fecha: 09/02/2021
    /// <summary>
    /// Repositorio para consultas de mkt.T_WhatsAppUsuarioCredencial
    /// </summary>
    public class WhatsAppUsuarioCredencialRepositorio : BaseRepository<TWhatsAppUsuarioCredencial, WhatsAppUsuarioCredencialBO>
    {
        #region Metodos Base
        public WhatsAppUsuarioCredencialRepositorio() : base()
        {
        }
        public WhatsAppUsuarioCredencialRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<WhatsAppUsuarioCredencialBO> GetBy(Expression<Func<TWhatsAppUsuarioCredencial, bool>> filter)
        {
            IEnumerable<TWhatsAppUsuarioCredencial> listado = base.GetBy(filter);
            List<WhatsAppUsuarioCredencialBO> listadoBO = new List<WhatsAppUsuarioCredencialBO>();
            foreach (var itemEntidad in listado)
            {
                WhatsAppUsuarioCredencialBO objetoBO = Mapper.Map<TWhatsAppUsuarioCredencial, WhatsAppUsuarioCredencialBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public WhatsAppUsuarioCredencialBO FirstById(int id)
        {
            try
            {
                TWhatsAppUsuarioCredencial entidad = base.FirstById(id);
                WhatsAppUsuarioCredencialBO objetoBO = new WhatsAppUsuarioCredencialBO();
                Mapper.Map<TWhatsAppUsuarioCredencial, WhatsAppUsuarioCredencialBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public WhatsAppUsuarioCredencialBO FirstBy(Expression<Func<TWhatsAppUsuarioCredencial, bool>> filter)
        {
            try
            {
                TWhatsAppUsuarioCredencial entidad = base.FirstBy(filter);
                WhatsAppUsuarioCredencialBO objetoBO = Mapper.Map<TWhatsAppUsuarioCredencial, WhatsAppUsuarioCredencialBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(WhatsAppUsuarioCredencialBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TWhatsAppUsuarioCredencial entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<WhatsAppUsuarioCredencialBO> listadoBO)
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

        public bool Update(WhatsAppUsuarioCredencialBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TWhatsAppUsuarioCredencial entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<WhatsAppUsuarioCredencialBO> listadoBO)
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
        private void AsignacionId(TWhatsAppUsuarioCredencial entidad, WhatsAppUsuarioCredencialBO objetoBO)
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

        private TWhatsAppUsuarioCredencial MapeoEntidad(WhatsAppUsuarioCredencialBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TWhatsAppUsuarioCredencial entidad = new TWhatsAppUsuarioCredencial();
                entidad = Mapper.Map<WhatsAppUsuarioCredencialBO, TWhatsAppUsuarioCredencial>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion
        /// <summary>
        /// Obtiene los datos del host del whatsapp por idPais 
        /// </summary>
        public CredencialTokenExpiraDTO ValidarCredencialesUsuario(int idPersonal, int idPais)
        {
            try
            {
                CredencialTokenExpiraDTO TokenCredencial = new CredencialTokenExpiraDTO();
                var Query = string.Empty;
                Query = "SELECT top 1 X.IdWhatsAppUsuario, X.UserAuthToken, X.ExpiresAfter "+
                         "FROM mkt.T_WhatsAppUsuarioCredencial X "+
                         "INNER JOIN mkt.T_WhatsAppUsuario Y ON X.IdWhatsAppUsuario=Y.Id "+
                         "INNER JOIN mkt.T_WhatsAppConfiguracion Z ON X.IdWhatsAppConfiguracion=Z.Id "+
                         "WHERE Y.IdPersonal = @idPersonal AND Z.IdPais=@idPais AND X.Estado = 1 AND Y.Estado = 1 ORDER BY X.ExpiresAfter DESC";
                var CredencialTokenExpiraDB = _dapper.FirstOrDefault(Query, new { idPersonal, idPais });
                TokenCredencial = JsonConvert.DeserializeObject<CredencialTokenExpiraDTO>(CredencialTokenExpiraDB);
                return TokenCredencial;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public CredencialTokenExpiraDTO ValidarUsuario(int idPersonal)
        {
            try
            {
                CredencialTokenExpiraDTO tokenCredencial = new CredencialTokenExpiraDTO();
                var _query = string.Empty;
                _query = "SELECT Id AS IdWhatsAppUsuario " +
                         "FROM mkt.T_WhatsAppUsuario " +
                         "WHERE IdPersonal = @idPersonal AND Estado = 1";
                var CredencialTokenExpiraDB = _dapper.FirstOrDefault(_query, new { idPersonal });
                tokenCredencial = JsonConvert.DeserializeObject<CredencialTokenExpiraDTO>(CredencialTokenExpiraDB);
                return tokenCredencial;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene las credenciales de login de un personal especifico
        /// </summary>
        /// <param name="idPersonal">Id del personal (gp.T_Personal)</param>
        /// <returns>Objeto de tipo de CredencialUsuarioLoginDTO</returns>
        public CredencialUsuarioLoginDTO CredencialUsuarioLogin(int idPersonal)
        {
            try
            {
                CredencialUsuarioLoginDTO tokenCredencial = new CredencialUsuarioLoginDTO();
                var _query = string.Empty;
                _query = "SELECT Id AS IdWhatsAppUsuario, UserUsername, UserPassword " +
                         "FROM mkt.T_WhatsAppUsuario " +
                         "WHERE IdPersonal = @idPersonal AND Estado = 1";
                var CredencialTokenExpiraDB = _dapper.FirstOrDefault(_query, new { idPersonal });
                tokenCredencial = JsonConvert.DeserializeObject<CredencialUsuarioLoginDTO>(CredencialTokenExpiraDB);
                return tokenCredencial;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Inserta en mkt.T_WhatsAppUsuarioCredencial
        /// </summary>
        /// <param name="filtro">Objeto de tipo TWhatsAppUsuarioCredencial</param>
        /// <returns>Id de la transaccion</returns>
        public int InsertarWhatsAppUsuarioCredencial(TWhatsAppUsuarioCredencial filtro)
        {
            var resultado = new ValorIntDTO();

            string spQuery = "[mkt].[SP_InsertarWhatsAppUsuarioCredencial]";

            var query = _dapper.QuerySPFirstOrDefault(spQuery, new
            {
                filtro.IdWhatsAppUsuario,
                filtro.IdWhatsAppConfiguracion,
                filtro.UserAuthToken,
                filtro.ExpiresAfter,
                filtro.EsMigracion,
                filtro.UsuarioCreacion,
                filtro.UsuarioModificacion
            });

            if (!string.IsNullOrEmpty(query))
            {
                resultado = JsonConvert.DeserializeObject<ValorIntDTO>(query);
            }

            return resultado.Valor;
        }
    }
}
