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
    /// Repositorio: WhatsAppUsuarioRepositorio
    /// Autor: Jose Villena
    /// Fecha: 03/05/2021
    /// <summary>
    /// Repositorio para consultas de T_WhatsAppUsuario
    /// </summary>
    public class WhatsAppUsuarioRepositorio : BaseRepository<TWhatsAppUsuario, WhatsAppUsuarioBO>
    {
        #region Metodos Base
        public WhatsAppUsuarioRepositorio() : base()
        {
        }
        public WhatsAppUsuarioRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<WhatsAppUsuarioBO> GetBy(Expression<Func<TWhatsAppUsuario, bool>> filter)
        {
            IEnumerable<TWhatsAppUsuario> listado = base.GetBy(filter);
            List<WhatsAppUsuarioBO> listadoBO = new List<WhatsAppUsuarioBO>();
            foreach (var itemEntidad in listado)
            {
                WhatsAppUsuarioBO objetoBO = Mapper.Map<TWhatsAppUsuario, WhatsAppUsuarioBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public WhatsAppUsuarioBO FirstById(int id)
        {
            try
            {
                TWhatsAppUsuario entidad = base.FirstById(id);
                WhatsAppUsuarioBO objetoBO = new WhatsAppUsuarioBO();
                Mapper.Map<TWhatsAppUsuario, WhatsAppUsuarioBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public WhatsAppUsuarioBO FirstBy(Expression<Func<TWhatsAppUsuario, bool>> filter)
        {
            try
            {
                TWhatsAppUsuario entidad = base.FirstBy(filter);
                WhatsAppUsuarioBO objetoBO = Mapper.Map<TWhatsAppUsuario, WhatsAppUsuarioBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(WhatsAppUsuarioBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TWhatsAppUsuario entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<WhatsAppUsuarioBO> listadoBO)
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

        public bool Update(WhatsAppUsuarioBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TWhatsAppUsuario entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<WhatsAppUsuarioBO> listadoBO)
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
        private void AsignacionId(TWhatsAppUsuario entidad, WhatsAppUsuarioBO objetoBO)
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

        private TWhatsAppUsuario MapeoEntidad(WhatsAppUsuarioBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TWhatsAppUsuario entidad = new TWhatsAppUsuario();
                entidad = Mapper.Map<WhatsAppUsuarioBO, TWhatsAppUsuario>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion

        public List<WhatsAppUsuariosDTO> ObtnerCredencialesUsuario()
        {
            try
            {
                List<WhatsAppUsuariosDTO> usuarioswhatsApp = new List<WhatsAppUsuariosDTO>();
                var _query = string.Empty;
                _query = "SELECT WU.Id,WU.IdPersonal,WU.RolUser,WU.UserUsername,WU.UserPassword,CONCAT(PE.Nombres,' ',pe.ApellidoPaterno,' ',PE.ApellidoMaterno) AS Nombres " +
                         "FROM mkt.T_WhatsAppUsuario AS WU " +
                         "INNER JOIN gp.T_Personal AS PE ON WU.IdPersonal=PE.Id " +
                         "WHERE WU.Estado=1";
                var usuariosWhatsAppDB = _dapper.QueryDapper(_query, null);
                usuarioswhatsApp = JsonConvert.DeserializeObject<List<WhatsAppUsuariosDTO>>(usuariosWhatsAppDB);
                return usuarioswhatsApp;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public WhatsAppUsuariosDTO ObtnerCredencialUsuarioPorId(int idUsuario)
        {
            try
            {
                WhatsAppUsuariosDTO usuarioswhatsApp = new WhatsAppUsuariosDTO();
                var _query = string.Empty;
                _query = "SELECT WU.Id,WU.IdPersonal,WU.RolUser,WU.UserUsername,WU.UserPassword,CONCAT(PE.Nombres,' ',pe.ApellidoPaterno,' ',PE.ApellidoMaterno) AS Nombres " +
                         "FROM mkt.T_WhatsAppUsuario AS WU " +
                         "INNER JOIN gp.T_Personal AS PE ON WU.IdPersonal=PE.Id " +
                         "WHERE WU.Estado=1 AND WU.Id=@idUsuario";
                var usuariosWhatsAppDB = _dapper.FirstOrDefault(_query, new { idUsuario = idUsuario});
                usuarioswhatsApp = JsonConvert.DeserializeObject<WhatsAppUsuariosDTO>(usuariosWhatsAppDB);
                return usuarioswhatsApp;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<WhatsAppPersonalDTO> ObtnerListaPersonal()
        {
            try
            {
                List<WhatsAppPersonalDTO> listaPersonal = new List<WhatsAppPersonalDTO>();
                var _query = string.Empty;
                _query = "SELECT PE.Id, CONCAT(PE.Apellidos, ' ', PE.Nombres) AS Nombres, PE.Rol, US.UserName " +
                         "FROM gp.T_Personal AS PE " +
                         "INNER JOIN conf.T_Integra_AspNetUsers AS US ON PE.Id=US.PerId " +
                         "WHERE PE.Estado=1 AND PE.Activo=1";
                var usuariosPersonal = _dapper.QueryDapper(_query, null);
                listaPersonal = JsonConvert.DeserializeObject<List<WhatsAppPersonalDTO>>(usuariosPersonal);
                return listaPersonal;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Jose Villena
        /// Fecha: 03/05/2021
        /// Version: 1.0
        /// <summary>
        /// Credenciales Usuario WhatsApp
        /// </summary>
        /// <param name="idPersonal"> Id del Personal </param>     
        /// <returns>Respuesta: WhatsAppUsuariosDTO </returns> 
        public WhatsAppUsuariosDTO UsuarioWhatsAppValido(int idPersonal)
        {
            try
            {
                WhatsAppUsuariosDTO usuarioswhatsApp = new WhatsAppUsuariosDTO();
                var query = string.Empty;
                query = "SELECT WU.Id,WU.IdPersonal,WU.RolUser,WU.UserUsername,WU.UserPassword,CONCAT(PE.Nombres,' ',pe.ApellidoPaterno,' ',PE.ApellidoMaterno) AS Nombres " +
                         "FROM mkt.T_WhatsAppUsuario AS WU " +
                         "INNER JOIN gp.T_Personal AS PE ON WU.IdPersonal=PE.Id " +
                         "WHERE WU.Estado=1 AND WU.IdPersonal=@idPersonal";
                var usuariosWhatsAppDB = _dapper.FirstOrDefault(query, new { idPersonal = idPersonal });
                usuarioswhatsApp = JsonConvert.DeserializeObject<WhatsAppUsuariosDTO>(usuariosWhatsAppDB);
                return usuarioswhatsApp;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
