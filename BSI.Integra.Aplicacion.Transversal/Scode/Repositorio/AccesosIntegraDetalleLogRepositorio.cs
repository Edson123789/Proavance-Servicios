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
using System.Globalization;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class AccesosIntegraDetalleLogRepositorio : BaseRepository<TAccesosIntegraDetalleLog, AccesosIntegraDetalleLogBO>
    {
        #region Metodos Base
        public AccesosIntegraDetalleLogRepositorio() : base()
        {
        }
        public AccesosIntegraDetalleLogRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<AccesosIntegraDetalleLogBO> GetBy(Expression<Func<TAccesosIntegraDetalleLog, bool>> filter)
        {
            IEnumerable<TAccesosIntegraDetalleLog> listado = base.GetBy(filter);
            List<AccesosIntegraDetalleLogBO> listadoBO = new List<AccesosIntegraDetalleLogBO>();
            foreach (var itemEntidad in listado)
            {
                AccesosIntegraDetalleLogBO objetoBO = Mapper.Map<TAccesosIntegraDetalleLog, AccesosIntegraDetalleLogBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public AccesosIntegraDetalleLogBO FirstById(int id)
        {
            try
            {
                TAccesosIntegraDetalleLog entidad = base.FirstById(id);
                AccesosIntegraDetalleLogBO objetoBO = new AccesosIntegraDetalleLogBO();
                Mapper.Map<TAccesosIntegraDetalleLog, AccesosIntegraDetalleLogBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public AccesosIntegraDetalleLogBO FirstBy(Expression<Func<TAccesosIntegraDetalleLog, bool>> filter)
        {
            try
            {
                TAccesosIntegraDetalleLog entidad = base.FirstBy(filter);
                AccesosIntegraDetalleLogBO objetoBO = Mapper.Map<TAccesosIntegraDetalleLog, AccesosIntegraDetalleLogBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(AccesosIntegraDetalleLogBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TAccesosIntegraDetalleLog entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<AccesosIntegraDetalleLogBO> listadoBO)
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

        public bool Update(AccesosIntegraDetalleLogBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TAccesosIntegraDetalleLog entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<AccesosIntegraDetalleLogBO> listadoBO)
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
        private void AsignacionId(TAccesosIntegraDetalleLog entidad, AccesosIntegraDetalleLogBO objetoBO)
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

        private TAccesosIntegraDetalleLog MapeoEntidad(AccesosIntegraDetalleLogBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TAccesosIntegraDetalleLog entidad = new TAccesosIntegraDetalleLog();
                entidad = Mapper.Map<AccesosIntegraDetalleLogBO, TAccesosIntegraDetalleLog>(objetoBO,
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

        public CantidadIpAccesosIntegraDTO ObtenerCantidadIpUsuario(int idAccesosIntegraLog, string fechaActual)
        {
            try
            {
                string fecha = fechaActual;
                string format = "dd/MM/yyyy";

                DateTime FechaDia = DateTime.ParseExact(fecha, format, CultureInfo.InvariantCulture);
                

                CantidadIpAccesosIntegraDTO rpta = new CantidadIpAccesosIntegraDTO();
                var query = "Select IdAccesosIntegraLog, Cantidad from pla.V_TAccesosIntegraDetalleLog_ObtenerCantidadIp where Estado = 1 and IdAccesosIntegraLog = @idAccesosIntegraLog and Fecha=@FechaDia";
                var obtenerCantidadIpUsuarioDB = _dapper.FirstOrDefault(query, new { idAccesosIntegraLog, FechaDia });
                if (!string.IsNullOrEmpty(obtenerCantidadIpUsuarioDB) && !obtenerCantidadIpUsuarioDB.Contains("null"))
                {
                    rpta = JsonConvert.DeserializeObject<CantidadIpAccesosIntegraDTO>(obtenerCantidadIpUsuarioDB);
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
