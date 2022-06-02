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
    public class ModoPagoRepositorio : BaseRepository<TModoPago, ModoPagoBO>
    {
        #region Metodos Base
        public ModoPagoRepositorio() : base()
        {
        }
        public ModoPagoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ModoPagoBO> GetBy(Expression<Func<TModoPago, bool>> filter)
        {
            IEnumerable<TModoPago> listado = base.GetBy(filter);
            List<ModoPagoBO> listadoBO = new List<ModoPagoBO>();
            foreach (var itemEntidad in listado)
            {
                ModoPagoBO objetoBO = Mapper.Map<TModoPago, ModoPagoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ModoPagoBO FirstById(int id)
        {
            try
            {
                TModoPago entidad = base.FirstById(id);
                ModoPagoBO objetoBO = new ModoPagoBO();
                Mapper.Map<TModoPago, ModoPagoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ModoPagoBO FirstBy(Expression<Func<TModoPago, bool>> filter)
        {
            try
            {
                TModoPago entidad = base.FirstBy(filter);
                ModoPagoBO objetoBO = Mapper.Map<TModoPago, ModoPagoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ModoPagoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TModoPago entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ModoPagoBO> listadoBO)
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

        public bool Update(ModoPagoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TModoPago entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ModoPagoBO> listadoBO)
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
        private void AsignacionId(TModoPago entidad, ModoPagoBO objetoBO)
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

        private TModoPago MapeoEntidad(ModoPagoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TModoPago entidad = new TModoPago();
                entidad = Mapper.Map<ModoPagoBO, TModoPago>(objetoBO,
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
        ///  Obtiene la lista de tipos de pagos registrados en el sistema
        ///  con todos sus campos excepto los de auditoria.
        /// </summary>
        /// <returns></returns>
        public List<ModoPagoDTO> ListarModosPagosPanel()
        {
            try
            {
                List<ModoPagoDTO> ModoPagos = new List<ModoPagoDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, Nombre FROM pla.T_ModoPago WHERE Estado = 1";
                var pgeneralDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(pgeneralDB) && !pgeneralDB.Contains("[]"))
                {
                    ModoPagos = JsonConvert.DeserializeObject<List<ModoPagoDTO>>(pgeneralDB);
                }

                return ModoPagos;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
    }
}
