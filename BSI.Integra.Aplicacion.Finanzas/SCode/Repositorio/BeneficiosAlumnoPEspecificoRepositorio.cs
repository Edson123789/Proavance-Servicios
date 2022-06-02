using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Finanzas.Repositorio
{
    public class BeneficiosAlumnoPEspecificoRepositorio : BaseRepository<TBeneficiosAlumnoPespecifico, BeneficiosAlumnoPEspecificoBO>
    {
        #region Metodos Base
        public BeneficiosAlumnoPEspecificoRepositorio() : base()
        {
        }
        public BeneficiosAlumnoPEspecificoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<BeneficiosAlumnoPEspecificoBO> GetBy(Expression<Func<TBeneficiosAlumnoPespecifico, bool>> filter)
        {
            IEnumerable<TBeneficiosAlumnoPespecifico> listado = base.GetBy(filter);
            List<BeneficiosAlumnoPEspecificoBO> listadoBO = new List<BeneficiosAlumnoPEspecificoBO>();
            foreach (var itemEntidad in listado)
            {
                BeneficiosAlumnoPEspecificoBO objetoBO = Mapper.Map<TBeneficiosAlumnoPespecifico, BeneficiosAlumnoPEspecificoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public BeneficiosAlumnoPEspecificoBO FirstById(int id)
        {
            try
            {
                TBeneficiosAlumnoPespecifico entidad = base.FirstById(id);
                BeneficiosAlumnoPEspecificoBO objetoBO = new BeneficiosAlumnoPEspecificoBO();
                Mapper.Map<TBeneficiosAlumnoPespecifico, BeneficiosAlumnoPEspecificoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public BeneficiosAlumnoPEspecificoBO FirstBy(Expression<Func<TBeneficiosAlumnoPespecifico, bool>> filter)
        {
            try
            {
                TBeneficiosAlumnoPespecifico entidad = base.FirstBy(filter);
                BeneficiosAlumnoPEspecificoBO objetoBO = Mapper.Map<TBeneficiosAlumnoPespecifico, BeneficiosAlumnoPEspecificoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(BeneficiosAlumnoPEspecificoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TBeneficiosAlumnoPespecifico entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<BeneficiosAlumnoPEspecificoBO> listadoBO)
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

        public bool Update(BeneficiosAlumnoPEspecificoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TBeneficiosAlumnoPespecifico entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<BeneficiosAlumnoPEspecificoBO> listadoBO)
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
        private void AsignacionId(TBeneficiosAlumnoPespecifico entidad, BeneficiosAlumnoPEspecificoBO objetoBO)
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

        private TBeneficiosAlumnoPespecifico MapeoEntidad(BeneficiosAlumnoPEspecificoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TBeneficiosAlumnoPespecifico entidad = new TBeneficiosAlumnoPespecifico();
                entidad = Mapper.Map<BeneficiosAlumnoPEspecificoBO, TBeneficiosAlumnoPespecifico>(objetoBO,
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
        /// Obtiene [IdPGeneral, IdPEspecifico, Paquete, IdMatriculaCabecera, IdAlumno] existentes en una lista 
        /// para ser guardalas
        /// </summary>
        /// <returns></returns>
        public List<BeneficiosAlumnoPEspecificoDTO> ObtenerDatosPorCodigoMatricula(string codigoMatricula)
        {
            try
            {
                List<BeneficiosAlumnoPEspecificoDTO> BeneficiosAlumnoPEspecificoes = new List<BeneficiosAlumnoPEspecificoDTO>();
                var _query = string.Empty;
                _query = "SELECT IdPGeneral, IdPEspecifico, Paquete, IdMatriculaCabecera, IdAlumno, IdPais FROM fin.V_ObtenerDatosCodigoMatricula WHERE CodigoMatricula = @CodigoMatricula";
                var BeneficiosAlumnoPEspecificoesDB = _dapper.QueryDapper(_query, new {CodigoMatricula = codigoMatricula });
                if (!string.IsNullOrEmpty(BeneficiosAlumnoPEspecificoesDB) && !BeneficiosAlumnoPEspecificoesDB.Contains("[]"))
                {
                    BeneficiosAlumnoPEspecificoes = JsonConvert.DeserializeObject<List< BeneficiosAlumnoPEspecificoDTO >>(BeneficiosAlumnoPEspecificoesDB);
                }
                return BeneficiosAlumnoPEspecificoes;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene [IdPGeneral, IdPEspecifico, IdMatriculaCabecera, IdAlumno] existentes en una lista cuando no tienne pAquetes
        /// para ser guardalas
        /// </summary>
        /// <returns></returns>
        public List<BeneficiosAlumnoPEspecificoDTO> ObtenerDatosPorCodigoMatriculaSinPaquete(string codigoMatricula)
        {
            try
            {
                List<BeneficiosAlumnoPEspecificoDTO> BeneficiosAlumnoPEspecificoes = new List<BeneficiosAlumnoPEspecificoDTO>();
                var _query = string.Empty;
                _query = "SELECT IdPGeneral, IdPEspecifico, IdMatriculaCabecera, IdAlumno FROM fin.V_ObtenerDatosCodigoMatricula_SinPaquete WHERE CodigoMatricula = @CodigoMatricula";
                var BeneficiosAlumnoPEspecificoesDB = _dapper.QueryDapper(_query, new { CodigoMatricula = codigoMatricula });
                if (!string.IsNullOrEmpty(BeneficiosAlumnoPEspecificoesDB) && !BeneficiosAlumnoPEspecificoesDB.Contains("[]"))
                {
                    BeneficiosAlumnoPEspecificoes = JsonConvert.DeserializeObject<List<BeneficiosAlumnoPEspecificoDTO>>(BeneficiosAlumnoPEspecificoesDB);
                }
                return BeneficiosAlumnoPEspecificoes;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene los beneficios del programa tipo 1 es decira aquellos programas que tienes versiones
        /// </summary>
        /// <returns></returns>
        public List<BeneficiosProgramaTipo1DTO> ObtenerBeneficiosProgramaTipo1(int idPGeneral, int idPais, int? paquete)
        {
            try
            {
                List<BeneficiosProgramaTipo1DTO> BeneficiosAlumnoPEspecificoes = new List<BeneficiosProgramaTipo1DTO>();

                if (paquete == 1)
                {
                    
                    var _query = string.Empty;
                    _query = "SELECT Paquete, Descripcion, OrdenBeneficio FROM pla.V_BeneficiosProgramaTipo1 WHERE Id = @IdPGeneral and CodigoPais = @IdPais AND EstadoMontoPago = 1 AND" +
                        " EstadoMontoPagoSuscripcion = 1 AND EstadoSuscripcionProgramaGeneral = 1 AND Paquete IS NOT NULL and Paquete in (1) and OrdenBeneficio <> 1";
                    var BeneficiosAlumnoPEspecificoesDB = _dapper.QueryDapper(_query, new { IdPGeneral = idPGeneral , IdPais = idPais});
                    if (!string.IsNullOrEmpty(BeneficiosAlumnoPEspecificoesDB) && !BeneficiosAlumnoPEspecificoesDB.Contains("[]"))
                    {
                        BeneficiosAlumnoPEspecificoes = JsonConvert.DeserializeObject<List<BeneficiosProgramaTipo1DTO>>(BeneficiosAlumnoPEspecificoesDB);
                    }
                }
                else if (paquete == 2)
                {
                   
                    var _query = string.Empty;
                    _query = "SELECT Paquete, Descripcion, OrdenBeneficio FROM pla.V_BeneficiosProgramaTipo1 WHERE Id = @IdPGeneral and CodigoPais = @IdPais AND EstadoMontoPago = 1 AND" +
                         " EstadoMontoPagoSuscripcion = 1 AND EstadoSuscripcionProgramaGeneral = 1 AND Paquete IS NOT NULL and Paquete in (1,2) and OrdenBeneficio <> 1";
                    var BeneficiosAlumnoPEspecificoesDB = _dapper.QueryDapper(_query, new { IdPGeneral = idPGeneral, IdPais = idPais });
                    if (!string.IsNullOrEmpty(BeneficiosAlumnoPEspecificoesDB) && !BeneficiosAlumnoPEspecificoesDB.Contains("[]"))
                    {
                        BeneficiosAlumnoPEspecificoes = JsonConvert.DeserializeObject<List<BeneficiosProgramaTipo1DTO>>(BeneficiosAlumnoPEspecificoesDB);
                    }
                }
                else {
                    
                    var _query = string.Empty;
                    _query = "SELECT Paquete, Descripcion, OrdenBeneficio FROM pla.V_BeneficiosProgramaTipo1 WHERE Id = @IdPGeneral and CodigoPais = @IdPais AND EstadoMontoPago = 1 AND" +
                         " EstadoMontoPagoSuscripcion = 1 AND EstadoSuscripcionProgramaGeneral = 1 AND Paquete IS NOT NULL and Paquete in (1,2,3) and OrdenBeneficio <> 1";
                    var BeneficiosAlumnoPEspecificoesDB = _dapper.QueryDapper(_query, new { IdPGeneral = idPGeneral, IdPais = idPais });
                    if (!string.IsNullOrEmpty(BeneficiosAlumnoPEspecificoesDB) && !BeneficiosAlumnoPEspecificoesDB.Contains("[]"))
                    {
                        BeneficiosAlumnoPEspecificoes = JsonConvert.DeserializeObject<List<BeneficiosProgramaTipo1DTO>>(BeneficiosAlumnoPEspecificoesDB);
                    }
                }
                return BeneficiosAlumnoPEspecificoes;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene los beneficios del programa tipo 2 es decir aquellos programas que no tienen versiones
        /// </summary>
        /// <returns></returns>
        public List<BeneficiosProgramaTipo2DTO> ObtenerBeneficiosProgramaTipo2(int idPGeneral)
        {
            try
            {
                List < BeneficiosProgramaTipo2DTO> BeneficiosAlumnoPEspecificoes = new List<BeneficiosProgramaTipo2DTO>();
                var _query = string.Empty;
                _query = "SELECT Titulo FROM pla.V_BeneficiosProgramaTipo2 WHERE TituloDocumentoSeccion = 'Beneficios' AND IdProgramaGeneral = @IdPGeneral AND " +
                    "EstadoDocumentoSeccion = 1 AND EstadoProgramaGeneralDocumento = 1 AND EstadoDocumento = 1 AND EstadoProgramaGeneral = 1";
                var BeneficiosAlumnoPEspecificoesDB = _dapper.QueryDapper(_query, new { IdPGeneral = idPGeneral });
                if (!string.IsNullOrEmpty(BeneficiosAlumnoPEspecificoesDB) && !BeneficiosAlumnoPEspecificoesDB.Contains("[]"))
                {
                    BeneficiosAlumnoPEspecificoes = JsonConvert.DeserializeObject< List<BeneficiosProgramaTipo2DTO>>(BeneficiosAlumnoPEspecificoesDB);
                }
                return BeneficiosAlumnoPEspecificoes;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
