using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using BSI.Integra.Aplicacion.DTOs;

namespace BSI.Integra.Aplicacion.Comercial.Repositorio
{
    public class SentinelRepositorio : BaseRepository<TSentinel, SentinelBO>
    {
        #region Metodos Base
        public SentinelRepositorio() : base()
        {
        }
        public SentinelRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<SentinelBO> GetBy(Expression<Func<TSentinel, bool>> filter)
        {
            IEnumerable<TSentinel> listado = base.GetBy(filter);
            List<SentinelBO> listadoBO = new List<SentinelBO>();
            foreach (var itemEntidad in listado)
            {
                SentinelBO objetoBO = Mapper.Map<TSentinel, SentinelBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public SentinelBO FirstById(int id)
        {
            try
            {
                TSentinel entidad = base.FirstById(id);
                SentinelBO objetoBO = new SentinelBO();
                Mapper.Map<TSentinel, SentinelBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public SentinelBO FirstBy(Expression<Func<TSentinel, bool>> filter)
        {
            try
            {
                TSentinel entidad = base.FirstBy(filter);
                SentinelBO objetoBO = Mapper.Map<TSentinel, SentinelBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(SentinelBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TSentinel entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<SentinelBO> listadoBO)
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

        public bool Update(SentinelBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TSentinel entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<SentinelBO> listadoBO)
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
        private void AsignacionId(TSentinel entidad, SentinelBO objetoBO)
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

        private TSentinel MapeoEntidad(SentinelBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TSentinel entidad = new TSentinel();
                entidad = Mapper.Map<SentinelBO, TSentinel>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                if (objetoBO.DniRuc != null && objetoBO.DniRuc.Count > 0)
                {
                    foreach (var hijo in objetoBO.DniRuc)
                    {
                        TSentinelSdtEstandarItem entidadHijo = new TSentinelSdtEstandarItem();
                        entidadHijo = Mapper.Map<SentinelSdtEstandarItemBO, TSentinelSdtEstandarItem>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TSentinelSdtEstandarItem.Add(entidadHijo);
                    }
                }
                if (objetoBO.DatosGenerales != null)
                {
                    TSentinelSdtInfGen entidadHijo = new TSentinelSdtInfGen();
                    entidadHijo = Mapper.Map<SentinelSdtInfGenBO, TSentinelSdtInfGen>(objetoBO.DatosGenerales,
                        opt => opt.ConfigureMap(MemberList.None));
                    entidad.TSentinelSdtInfGen.Add(entidadHijo);
                }
                if (objetoBO.DatosVencidas != null && objetoBO.DatosVencidas.Count > 0)
                {
                    foreach (var hijo in objetoBO.DatosVencidas)
                    {
                        TSentinelSdtResVenItem entidadHijo = new TSentinelSdtResVenItem();
                        entidadHijo = Mapper.Map<SentinelSdtResVenItemBO, TSentinelSdtResVenItem>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TSentinelSdtResVenItem.Add(entidadHijo);
                    }
                }
                if (objetoBO.LineaCredito != null && objetoBO.LineaCredito.Count > 0)
                {
                    foreach (var hijo in objetoBO.LineaCredito)
                    {
                        TSentinelSdtLincreItem entidadHijo = new TSentinelSdtLincreItem();
                        entidadHijo = Mapper.Map<SentinelSdtLincreItemBO, TSentinelSdtLincreItem>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TSentinelSdtLincreItem.Add(entidadHijo);
                    }
                }
                if (objetoBO.PosicionHistoria != null && objetoBO.PosicionHistoria.Count > 0)
                {
                    foreach (var hijo in objetoBO.PosicionHistoria)
                    {
                        TSentinelSdtPoshisItem entidadHijo = new TSentinelSdtPoshisItem();
                        entidadHijo = Mapper.Map<SentinelSdtPoshisItemBO, TSentinelSdtPoshisItem>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TSentinelSdtPoshisItem.Add(entidadHijo);
                    }
                }
                if (objetoBO.Cargo != null && objetoBO.Cargo.Count > 0)
                {
                    foreach (var hijo in objetoBO.Cargo)
                    {
                        TSentinelRepLegItem entidadHijo = new TSentinelRepLegItem();
                        entidadHijo = Mapper.Map<SentinelRepLegItemBO, TSentinelRepLegItem>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TSentinelRepLegItem.Add(entidadHijo);
                    }
                }
                if (objetoBO.Deuda != null && objetoBO.Deuda.Count > 0)
                {
                    foreach (var hijo in objetoBO.Deuda)
                    {
                        TSentinelSdtRepSbsitem entidadHijo = new TSentinelSdtRepSbsitem();
                        entidadHijo = Mapper.Map<SentinelSdtRepSbsitemBO, TSentinelSdtRepSbsitem>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TSentinelSdtRepSbsitem.Add(entidadHijo);
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

        public SentinelDatosContactoDTO ObtenerDastosAlumnoSentinel(int idAlumno)
        {
            try
            {
                string _queryDatoSentinel = "Select DNI,IdSentinel,IdAlumno,TipoDocumento,Nombre,Sexo,FechaNacimiento,Ubigeo,Distrito,Provincia,Departamento,CIIU,ActividadEconomica,Direccion,SemaforoActual,SemaforoPrevio,FechaUltimaActualizacion, NombreAlterno From com.V_DatosSentinelPorIdAlumno Where IdAlumno=@IdAlumno and TipoDocumento='D' and EstadoSentinel=1 and EstadoInf = 1 and EstadoEs = 1";
                var queryDatoSentinel = _dapper.FirstOrDefault(_queryDatoSentinel, new { IdAlumno = idAlumno });
                if (queryDatoSentinel == "null" || queryDatoSentinel == "")
                {
                    string _queryDatoSentinel2 = "Select DNI,IdSentinel,IdAlumno,TipoDocumento,Nombre,Sexo,FechaNacimiento,Ubigeo,Distrito,Provincia,Departamento,CIIU,ActividadEconomica,Direccion,SemaforoActual,SemaforoPrevio,FechaUltimaActualizacion, NombreAlterno From com.V_DatosSentinelPorIdAlumno Where IdAlumno=@IdAlumno and EstadoSentinel=1 and EstadoInf = 1 and EstadoEs = 1";
                    var queryDatoSentinel2 = _dapper.FirstOrDefault(_queryDatoSentinel2, new { IdAlumno = idAlumno });
                    return JsonConvert.DeserializeObject<SentinelDatosContactoDTO>(queryDatoSentinel2);
                    
                }
                else {
                    return JsonConvert.DeserializeObject<SentinelDatosContactoDTO>(queryDatoSentinel);
                }
                
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }

        public IdentificadorDTO ObtenerIdSentinelPorDni(string dni)
        {
            try
            {
                string _querySentinelAlumno = "Select Id From com.V_TSentinel_ObtenerId Where Dni=@DNI and Estado=1";
                var querySentinelAlumno = _dapper.FirstOrDefault(_querySentinelAlumno, new { DNI = dni });
                return JsonConvert.DeserializeObject<IdentificadorDTO>(querySentinelAlumno);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jashin Salazar
        /// Fecha: 18/12/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene la cabecera para la agenda
        /// </summary>
        /// <param name="idSentinel">Id de sentinel</param>
        /// <returns>SentinelDatosCabeceraDTO</returns>

        public SentinelDatosCabeceraDTO ObtenerCabeceraSentinel(int idSentinel)
        {
            try
            {
                string _querySentinelAlumno = "com.SP_ObtenerCabeceraSemaforo";
                var querySentinelAlumno = _dapper.QuerySPFirstOrDefault(_querySentinelAlumno, new { IdSentinel = idSentinel });
                return JsonConvert.DeserializeObject<SentinelDatosCabeceraDTO>(querySentinelAlumno);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
