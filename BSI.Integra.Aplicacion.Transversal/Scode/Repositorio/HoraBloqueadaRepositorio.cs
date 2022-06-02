using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs.Scode;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class HoraBloqueadaRepositorio : BaseRepository<THoraBloqueada, HoraBloqueadaBO>
    {
        #region Metodos Base
        public HoraBloqueadaRepositorio() : base()
        {
        }
        public HoraBloqueadaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<HoraBloqueadaBO> GetBy(Expression<Func<THoraBloqueada, bool>> filter)
        {
            IEnumerable<THoraBloqueada> listado = base.GetBy(filter);
            List<HoraBloqueadaBO> listadoBO = new List<HoraBloqueadaBO>();
            foreach (var itemEntidad in listado)
            {
                HoraBloqueadaBO objetoBO = Mapper.Map<THoraBloqueada, HoraBloqueadaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public HoraBloqueadaBO FirstById(int id)
        {
            try
            {
                THoraBloqueada entidad = base.FirstById(id);
                HoraBloqueadaBO objetoBO = new HoraBloqueadaBO();
                Mapper.Map<THoraBloqueada, HoraBloqueadaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public HoraBloqueadaBO FirstBy(Expression<Func<THoraBloqueada, bool>> filter)
        {
            try
            {
                THoraBloqueada entidad = base.FirstBy(filter);
                HoraBloqueadaBO objetoBO = Mapper.Map<THoraBloqueada, HoraBloqueadaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(HoraBloqueadaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                THoraBloqueada entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<HoraBloqueadaBO> listadoBO)
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

        public bool Update(HoraBloqueadaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                THoraBloqueada entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<HoraBloqueadaBO> listadoBO)
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
        private void AsignacionId(THoraBloqueada entidad, HoraBloqueadaBO objetoBO)
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

        private THoraBloqueada MapeoEntidad(HoraBloqueadaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                THoraBloqueada entidad = new THoraBloqueada();
                entidad = Mapper.Map<HoraBloqueadaBO, THoraBloqueada>(objetoBO,
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
        /// Obtiene todas la horas bloquedas que el asesor puede tener durante el dia para la programaciond e actividades
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public List<HoraBloqueadaRADTO> ObtenerHorasBloquedasReprogramacionPorAsesor(int idPersonal, DateTime fecha)
        {
            try
            {
                DateTime fechatemp = new DateTime(fecha.Year, fecha.Month, fecha.Day, 0, 0, 0);
                string _queryhoras_bloqueadas = "Select Fecha, Hora from com.V_THoraBloqueada_FechaProgramacionAutomatica Where Fecha=@fechatemp and IdPersonal=@IdPersonal";
                var queryhoras_bloqueadas = _dapper.QueryDapper(_queryhoras_bloqueadas, new { fechatemp, IdPersonal = idPersonal });
                List<HoraBloqueadaRADTO> horasBloqueadas = JsonConvert.DeserializeObject<List<HoraBloqueadaRADTO>>(queryhoras_bloqueadas);
                return horasBloqueadas;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
           

        }
    }
}
