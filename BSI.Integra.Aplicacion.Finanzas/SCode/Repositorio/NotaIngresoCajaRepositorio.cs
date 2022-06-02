using System;
using System.Collections.Generic;
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
    public class NotaIngresoCajaRepositorio : BaseRepository<TNotaIngresoCaja, NotaIngresoCajaBO>
    {
        #region Metodos Base
        public NotaIngresoCajaRepositorio() : base()
        {
        }
        public NotaIngresoCajaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<NotaIngresoCajaBO> GetBy(Expression<Func<TNotaIngresoCaja, bool>> filter)
        {
            IEnumerable<TNotaIngresoCaja> listado = base.GetBy(filter);
            List<NotaIngresoCajaBO> listadoBO = new List<NotaIngresoCajaBO>();
            foreach (var itemEntidad in listado)
            {
                NotaIngresoCajaBO objetoBO = Mapper.Map<TNotaIngresoCaja, NotaIngresoCajaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public NotaIngresoCajaBO FirstById(int id)
        {
            try
            {
                TNotaIngresoCaja entidad = base.FirstById(id);
                NotaIngresoCajaBO objetoBO = new NotaIngresoCajaBO();
                Mapper.Map<TNotaIngresoCaja, NotaIngresoCajaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public NotaIngresoCajaBO FirstBy(Expression<Func<TNotaIngresoCaja, bool>> filter)
        {
            try
            {
                TNotaIngresoCaja entidad = base.FirstBy(filter);
                NotaIngresoCajaBO objetoBO = Mapper.Map<TNotaIngresoCaja, NotaIngresoCajaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(NotaIngresoCajaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TNotaIngresoCaja entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<NotaIngresoCajaBO> listadoBO)
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

        public bool Update(NotaIngresoCajaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TNotaIngresoCaja entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<NotaIngresoCajaBO> listadoBO)
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
        private void AsignacionId(TNotaIngresoCaja entidad, NotaIngresoCajaBO objetoBO)
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

        private TNotaIngresoCaja MapeoEntidad(NotaIngresoCajaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TNotaIngresoCaja entidad = new TNotaIngresoCaja();
                entidad = Mapper.Map<NotaIngresoCajaBO, TNotaIngresoCaja>(objetoBO,
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

        public List<NotaIngresoCajaDTO> ObtenerCajaNIC(int? idCaja)
        {
            try
            {                
                var _query = "";
                var camposTabla = "Id,CodigoNic,IdCaja,CodigoCaja,ResponsableCaja,IdOrigenIngresoCaja,OrigenIngresoCaja,IdPersonalEmitido,PersonalEmitido,Monto,FechaGiro,Concepto,FechaCobro";

                List< NotaIngresoCajaDTO > listaNIC = new List<NotaIngresoCajaDTO>();
                if (idCaja == null)
                {
                    _query = "SELECT " + camposTabla + " FROM FIN.V_ObtenerNotaIngresoCaja order by CodigoCaja,Id Asc";
                }
                else {
                    _query = "SELECT " + camposTabla + " FROM FIN.V_ObtenerNotaIngresoCaja where IdCaja=@idCaja order by CodigoCaja,Id asc ";
                }
                var cajaNicDB = _dapper.QueryDapper(_query, new { idCaja});
                if (!cajaNicDB.Contains("[]") && !string.IsNullOrEmpty(cajaNicDB))
                {
                    listaNIC = JsonConvert.DeserializeObject<List<NotaIngresoCajaDTO>>(cajaNicDB);
                }
                return listaNIC;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<NotaIngresoCajaDTO> ObtenerCajaIngresoByFecha(DateTime? FechaInicial, DateTime? FechaFinal, int IdCaja)
        {
            try
            {
                var _query = "";
                var cajaNicDB = "";
                var camposTabla = "Id,CodigoNic,IdCaja,CodigoCaja,ResponsableCaja,IdOrigenIngresoCaja,OrigenIngresoCaja,IdPersonalEmitido,PersonalEmitido,Monto,FechaGiro,Concepto,FechaCobro,Moneda,IdMoneda  ";

                List<NotaIngresoCajaDTO> listaNIC = new List<NotaIngresoCajaDTO>();
                if (!FechaFinal.HasValue && !FechaFinal.HasValue)
                {
                    _query = "SELECT " + camposTabla + " FROM FIN.V_ObtenerNotaIngresoCaja where IdCaja=@idCaja order by  CodigoCaja,Id Asc";
                    cajaNicDB = _dapper.QueryDapper(_query, new {idCaja= IdCaja });
                }
                else if (FechaFinal.HasValue && FechaFinal.HasValue)
                {
                    _query = "SELECT " + camposTabla+ " FROM FIN.V_ObtenerNotaIngresoCaja WHERE IdCaja=@idCaja and Convert(Date,FechaGiro)>=@fechaInicial and Convert(Date, FechaGiro)  <= @fechaFinal Order By  CodigoCaja,Id Asc";
                    cajaNicDB = _dapper.QueryDapper(_query, new { fechaInicial = FechaInicial.Value.Date, fechaFinal = FechaFinal.Value.Date,idCaja= IdCaja });

                }
                else if (FechaFinal.HasValue && !FechaFinal.HasValue)
                {
                    FechaFinal = DateTime.Now;
                    cajaNicDB = _dapper.QueryDapper(_query, null);
                }
                if (!string.IsNullOrEmpty(cajaNicDB) && !cajaNicDB.Contains("[]"))
                {
                    listaNIC = JsonConvert.DeserializeObject<List<NotaIngresoCajaDTO>>(cajaNicDB);
                }

                return listaNIC;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<NotaIngresoCajaDatosPdfDTO> ObtenerDatosCajaIngreso(int[] IdIngresoCaja)
        {
            try
            {
                var _query = "";
                var cajaNicDB = "";
                var camposTabla = "SELECT IdNotaIngresoCaja,CodigoNic,CodigoCaja,RazonSocial,Direccion,Ruc,Central,CuentaCaja,Origen,FechaGiro,PersonalEmitido,Concepto,Monto,Moneda,PersonalResponsable,DniResponsable ";

                List<NotaIngresoCajaDatosPdfDTO> listaNIC = new List<NotaIngresoCajaDatosPdfDTO>();
                    _query = camposTabla + " FROM FIN.V_ObtenerDatosCajaIngresoPDF where IdNotaIngresoCaja IN @IdsNic";
                    cajaNicDB = _dapper.QueryDapper(_query, new { IdsNic = IdIngresoCaja });

                if (!string.IsNullOrEmpty(cajaNicDB) && !cajaNicDB.Contains("[]"))
                {
                    listaNIC = JsonConvert.DeserializeObject<List<NotaIngresoCajaDatosPdfDTO>>(cajaNicDB);
                }

                return listaNIC;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
