using System;
using System.Collections.Generic;
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
    public class LlamadaWebphoneCruceCentralRepositorio : BaseRepository<TLlamadaWebphoneCruceCentral, LlamadaWebphoneCruceCentralBO>
    {
        #region Metodos Base
        public LlamadaWebphoneCruceCentralRepositorio() : base()
        {
        }
        public LlamadaWebphoneCruceCentralRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<LlamadaWebphoneCruceCentralBO> GetBy(Expression<Func<TLlamadaWebphoneCruceCentral, bool>> filter)
        {
            IEnumerable<TLlamadaWebphoneCruceCentral> listado = base.GetBy(filter);
            List<LlamadaWebphoneCruceCentralBO> listadoBO = new List<LlamadaWebphoneCruceCentralBO>();
            foreach (var itemEntidad in listado)
            {
                LlamadaWebphoneCruceCentralBO objetoBO = Mapper.Map<TLlamadaWebphoneCruceCentral, LlamadaWebphoneCruceCentralBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public LlamadaWebphoneCruceCentralBO FirstById(int id)
        {
            try
            {
                TLlamadaWebphoneCruceCentral entidad = base.FirstById(id);
                LlamadaWebphoneCruceCentralBO objetoBO = new LlamadaWebphoneCruceCentralBO();
                Mapper.Map<TLlamadaWebphoneCruceCentral, LlamadaWebphoneCruceCentralBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public LlamadaWebphoneCruceCentralBO FirstBy(Expression<Func<TLlamadaWebphoneCruceCentral, bool>> filter)
        {
            try
            {
                TLlamadaWebphoneCruceCentral entidad = base.FirstBy(filter);
                LlamadaWebphoneCruceCentralBO objetoBO = Mapper.Map<TLlamadaWebphoneCruceCentral, LlamadaWebphoneCruceCentralBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(LlamadaWebphoneCruceCentralBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TLlamadaWebphoneCruceCentral entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<LlamadaWebphoneCruceCentralBO> listadoBO)
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

        public bool Update(LlamadaWebphoneCruceCentralBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TLlamadaWebphoneCruceCentral entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<LlamadaWebphoneCruceCentralBO> listadoBO)
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
        private void AsignacionId(TLlamadaWebphoneCruceCentral entidad, LlamadaWebphoneCruceCentralBO objetoBO)
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

        private TLlamadaWebphoneCruceCentral MapeoEntidad(LlamadaWebphoneCruceCentralBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TLlamadaWebphoneCruceCentral entidad = new TLlamadaWebphoneCruceCentral();
                entidad = Mapper.Map<LlamadaWebphoneCruceCentralBO, TLlamadaWebphoneCruceCentral>(objetoBO,
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

        public bool BloqueoInsertar()
        {
            try
            {
                string _queryCentralWebphone = "Select Id FROM com.T_LlamadaWebphoneCruceCentral Where Estado=0";
                var queryCentralWebphone = _dapper.QueryDapper(_queryCentralWebphone, null);
                if (!string.IsNullOrEmpty(queryCentralWebphone) && !queryCentralWebphone.Contains("[]"))
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<IdLlamadaWebphoneCruceCentralDTO> ObtenerListaIdLlamadaWebphone(int? Id)
        {
            try
            {
                List<IdLlamadaWebphoneCruceCentralDTO> centralWebphone = new List<IdLlamadaWebphoneCruceCentralDTO>();
                if (Id != null)
                {
                    string _queryCentralWebphone = "Select IdLlamadaWebPhone "+
                                               "From com.V_IdLlamadasWebphoneCruceCentral ";
                    var queryCentralWebphone = _dapper.QueryDapper(_queryCentralWebphone, new { Id });
                    centralWebphone = JsonConvert.DeserializeObject<List<IdLlamadaWebphoneCruceCentralDTO>>(queryCentralWebphone);
                }
                
                return centralWebphone;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool RegularizarPorNumeroFecha()
        {
            try
            {
                //var FechaLimite = (DateTime.Now).AddMinutes(-30);

                string _queryCentralWebphone = "com.SP_RegularizarLLamadaPorNumeroFecha";
                var queryCentralWebphone = _dapper.QuerySPFirstOrDefault(_queryCentralWebphone,null);
                
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool RegularizarPorNumeroFechaAnexo()
        {
            try
            {
                //var FechaLimite = (DateTime.Now).AddMinutes(-30);

                string _queryCentralWebphone = "com.SP_RegularizarLLamadaPorNumeroFechaAnexo";
                var queryCentralWebphone = _dapper.QuerySPFirstOrDefault(_queryCentralWebphone, null);
                
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<CentralCruceCentralDTO> ObtenerListaLlamadaWebphoneCruceCentral(List<int> IdLlamadaWebphone)
        {
            try
            {
                List<CentralCruceCentralDTO> centralWebphone = new List<CentralCruceCentralDTO>();
                
                string _queryCentralWebphone = "Select Id,IdLlamadaWebPhone,IdLlamadaCentral,FechaIncioLlamadaWebPhone,FechaFinLlamadaWebPhone" +
                                            ",FechaIncioLlamadaCentral,FechaFinLlamadaCentral,AnexoWebPhone,AnexoCentral,DuracionTimbradoWebPhone," +
                                            "DuracionContestoWebPhone,DuracionTimbradoCentral,DuracionContestoCentral,IdAlumno,IdActividadDetalle," +
                                            "TelefonoDestinoWebPhone,TelefonoDestinoCentral,IdLlamadaWebPhoneEstado,EstadoLlamadaCentral,SubEstadoLlamadaCentral,UrlAudio, " +
                                            "UsuarioCreacion,UsuarioModificacion " +
                                            "From com.V_LlamadasWebPhoneCruceCentral "+
                                            "Where IdLlamadaWebPhone in @IdLlamadaWebphone";
                var queryCentralWebphone = _dapper.QueryDapper(_queryCentralWebphone, new { IdLlamadaWebphone });
                centralWebphone = JsonConvert.DeserializeObject<List<CentralCruceCentralDTO>>(queryCentralWebphone);
                
                return centralWebphone;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Inserta en T_LlamadaWebphoneCruceCentral
        /// </summary>
        /// <param name="IdLlamadaWebphone"></param>
        /// <returns></returns>
        public bool InsertarLlamadaWebphoneCruceCentral(string IdLlamadaWebphone)
        {
            try
            {
                List<CentralCruceCentralDTO> centralWebphone = new List<CentralCruceCentralDTO>();

                string _queryInsertar = "com.SP_InsertarLlamadaWebphoneCruceCentral";
                var queryCentralWebphone = _dapper.QuerySPDapper(_queryInsertar, new { IdLlamadaWebphone });
                //centralWebphone = JsonConvert.DeserializeObject<List<CentralCruceCentralDTO>>(queryCentralWebphone);
                
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public LlamadaWebphoneCruceCentralBO ObtenerLlamadaWebphoneCruceCentral(int Id)
        {
            try
            {
                string _queryCentralWebphone = "Select Id,IdLlamadaWebPhone,IdLlamadaCentral,FechaIncioLlamadaWebPhone,FechaFinLlamadaWebPhone" +
                                               ",FechaIncioLlamadaCentral,FechaFinLlamadaCentral,AnexoWebPhone,AnexoCentral,DuracionTimbradoWebPhone," +
                                               "DuracionContestoWebPhone,DuracionTimbradoCentral,DuracionContestoCentral,IdAlumno,IdActividadDetalle," +
                                               "TelefonoDestinoWebPhone,TelefonoDestinoCentral,IdLlamadaWebPhoneEstado,EstadoLlamadaCentral,SubEstadoLlamadaCentral, " +
                                               "Estado,FechaCreacion,FechaModificacion,UsuarioCreacion,UsuarioModificacion,RowVersion,IdMigracion,UrlAudio " +
                                               "From com.V_ObtenerLlamadasWebPhoneCruceCentral " +
                                               " WHERE Id=@Id";
                var queryCentralWebphone = _dapper.FirstOrDefault(_queryCentralWebphone, new { Id});
                if (queryCentralWebphone != null && !string.IsNullOrEmpty(queryCentralWebphone))
                {
                    var centralWebphone = JsonConvert.DeserializeObject<LlamadaWebphoneCruceCentralBO>(queryCentralWebphone);
                    return centralWebphone;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int? ObtenerUltimoRegistro()
        {
            try
            {
                string _queryCentralWebphone = "Select top 1 Id " +
                                               "From com.T_LlamadaWebphoneCruceCentral ";
                var queryCentralWebphone = _dapper.FirstOrDefault(_queryCentralWebphone, null);
                if (queryCentralWebphone != "null" && !string.IsNullOrEmpty(queryCentralWebphone))
                {
                    var centralWebphone = JsonConvert.DeserializeObject<Dictionary<string,int>>(queryCentralWebphone);
                    return centralWebphone["Id"];
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
