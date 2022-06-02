using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: AccesoIntegraLogRepositorio
    /// Autor: Edgar S.
    /// Fecha: 29/01/2021
    /// <summary>
    /// Gestión de Usuario y Acceso a Integra
    /// </summary>
    public class AccesosIntegraLogRepositorio : BaseRepository<TAccesosIntegraLog, AccesosIntegraLogBO>
    {
        #region Metodos Base
        public AccesosIntegraLogRepositorio() : base()
        {
        }
        public AccesosIntegraLogRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<AccesosIntegraLogBO> GetBy(Expression<Func<TAccesosIntegraLog, bool>> filter)
        {
            IEnumerable<TAccesosIntegraLog> listado = base.GetBy(filter);
            List<AccesosIntegraLogBO> listadoBO = new List<AccesosIntegraLogBO>();
            foreach (var itemEntidad in listado)
            {
                AccesosIntegraLogBO objetoBO = Mapper.Map<TAccesosIntegraLog, AccesosIntegraLogBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public AccesosIntegraLogBO FirstById(int id)
        {
            try
            {
                TAccesosIntegraLog entidad = base.FirstById(id);
                AccesosIntegraLogBO objetoBO = new AccesosIntegraLogBO();
                Mapper.Map<TAccesosIntegraLog, AccesosIntegraLogBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public AccesosIntegraLogBO FirstBy(Expression<Func<TAccesosIntegraLog, bool>> filter)
        {
            try
            {
                TAccesosIntegraLog entidad = base.FirstBy(filter);
                AccesosIntegraLogBO objetoBO = Mapper.Map<TAccesosIntegraLog, AccesosIntegraLogBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(AccesosIntegraLogBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TAccesosIntegraLog entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<AccesosIntegraLogBO> listadoBO)
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

        public bool Update(AccesosIntegraLogBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TAccesosIntegraLog entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<AccesosIntegraLogBO> listadoBO)
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
        private void AsignacionId(TAccesosIntegraLog entidad, AccesosIntegraLogBO objetoBO)
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

        private TAccesosIntegraLog MapeoEntidad(AccesosIntegraLogBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TAccesosIntegraLog entidad = new TAccesosIntegraLog();
                entidad = Mapper.Map<AccesosIntegraLogBO, TAccesosIntegraLog>(objetoBO,
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

        ///Repositorio: AccesoIntegraLogRepositorio
        ///Autor: Edgar S.
        ///Fecha: 25/01/2021
        /// <summary>
        /// Se obtiene ip cookie por el usuario
        /// </summary>
        /// <param name="usuario"> Nombre de Usuario </param>
        /// <returns> Obtiene Ip y Cookie por Usuario </returns>
        /// <returns> ObjetoDTO : UsuarioAccesosIntegraDTO </returns>
        public UsuarioAccesosIntegraDTO ObtenerUsuarioAccesoIntegralog(string usuario)
        {
            try
            {
                UsuarioAccesosIntegraDTO rpta = new UsuarioAccesosIntegraDTO();
                var query = "Select Id, Usuario, IpUsuario, Cookie, Habilitado from pla.V_TAccesosIntegraLog_ObtenerUsuario where Estado = 1 and Usuario = @usuario";
                var obtenerUsuarioAccesoIntegraLogDB = _dapper.FirstOrDefault(query, new { usuario});
                if (!string.IsNullOrEmpty(obtenerUsuarioAccesoIntegraLogDB) && !obtenerUsuarioAccesoIntegraLogDB.Contains("null"))
                {
                    rpta = JsonConvert.DeserializeObject<UsuarioAccesosIntegraDTO>(obtenerUsuarioAccesoIntegraLogDB);
                }
                return rpta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


    }
}
