using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: BloqueHorarioProcesaOportunidad
    /// Autor: Gian Miranda
    /// Fecha: 08/02/2021
    /// <summary>
    /// Repositorio para la gestion de los bloques horarios para el procesamiento de oportunidades
    /// </summary>
    public class BloqueHorarioProcesaOportunidadRepositorio : BaseRepository<TBloqueHorarioProcesaOportunidad, BloqueHorarioProcesaOportunidadBO>
    {
        #region Metodos Base
        public BloqueHorarioProcesaOportunidadRepositorio() : base()
        {
        }
        public BloqueHorarioProcesaOportunidadRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<BloqueHorarioProcesaOportunidadBO> GetBy(Expression<Func<TBloqueHorarioProcesaOportunidad, bool>> filter)
        {
            IEnumerable<TBloqueHorarioProcesaOportunidad> listado = base.GetBy(filter);
            List<BloqueHorarioProcesaOportunidadBO> listadoBO = new List<BloqueHorarioProcesaOportunidadBO>();
            foreach (var itemEntidad in listado)
            {
                BloqueHorarioProcesaOportunidadBO objetoBO = Mapper.Map<TBloqueHorarioProcesaOportunidad, BloqueHorarioProcesaOportunidadBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public BloqueHorarioProcesaOportunidadBO FirstById(int id)
        {
            try
            {
                TBloqueHorarioProcesaOportunidad entidad = base.FirstById(id);
                BloqueHorarioProcesaOportunidadBO objetoBO = new BloqueHorarioProcesaOportunidadBO();
                Mapper.Map<TBloqueHorarioProcesaOportunidad, BloqueHorarioProcesaOportunidadBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public BloqueHorarioProcesaOportunidadBO FirstBy(Expression<Func<TBloqueHorarioProcesaOportunidad, bool>> filter)
        {
            try
            {
                TBloqueHorarioProcesaOportunidad entidad = base.FirstBy(filter);
                BloqueHorarioProcesaOportunidadBO objetoBO = Mapper.Map<TBloqueHorarioProcesaOportunidad, BloqueHorarioProcesaOportunidadBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(BloqueHorarioProcesaOportunidadBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TBloqueHorarioProcesaOportunidad entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<BloqueHorarioProcesaOportunidadBO> listadoBO)
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

        public bool Update(BloqueHorarioProcesaOportunidadBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TBloqueHorarioProcesaOportunidad entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<BloqueHorarioProcesaOportunidadBO> listadoBO)
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
        private void AsignacionId(TBloqueHorarioProcesaOportunidad entidad, BloqueHorarioProcesaOportunidadBO objetoBO)
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

        private TBloqueHorarioProcesaOportunidad MapeoEntidad(BloqueHorarioProcesaOportunidadBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TBloqueHorarioProcesaOportunidad entidad = new TBloqueHorarioProcesaOportunidad();
                entidad = Mapper.Map<BloqueHorarioProcesaOportunidadBO, TBloqueHorarioProcesaOportunidad>(objetoBO,
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
        /// Obtiene la configuracion de un bloque horario, filtrado por dia
        /// </summary>
        /// <param name="dia">Cadena con el nombre del dia</param>
        /// <returns>Objeto de clase BloqueHorarioProcesaOportunidadBO</returns>
        public BloqueHorarioProcesaOportunidadBO ObtenerConfiguracion(string dia)
        {
            var bloqueHorarioProcesarOportunidad = new BloqueHorarioProcesaOportunidadBO();
            try
            {
                var query = "SELECT Id, Activo, Prelanzamiento, Descripcion, Sede, Dia, TurnoM, HoraInicioM, HoraFinM, TurnoT, HoraInicioT, HoraFinT, ProbabilidadOportunidad FROM mkt.V_ObtenerTodoBloqueHorarioProcesaOportunidad WHERE Estado = 1 AND Dia = @dia";
                var bloqueHorarioProcesarOportunidadDb = _dapper.FirstOrDefault(query, new { dia });
                bloqueHorarioProcesarOportunidad = JsonConvert.DeserializeObject<BloqueHorarioProcesaOportunidadBO>(bloqueHorarioProcesarOportunidadDb);
                return bloqueHorarioProcesarOportunidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene todos los registros de T_BloqueHorarioProcesaOportunidad (estado=1) para llenar la grilla de su CRUD
        /// </summary>
        /// <returns>Lista de objetos de clase BloqueHorarioProcesaOportunidadDTO</returns>
        public List<BloqueHorarioProcesaOportunidadDTO> ObtenerTodoParaGrilla()
        {
            try
            {
                List<BloqueHorarioProcesaOportunidadDTO> data = new List<BloqueHorarioProcesaOportunidadDTO>();
                var _query = "SELECT Id,IdDiaSemana,NombreDiaSemana,ProbabilidadOportunidad,TurnoM,NombreTurnoM,TurnoT,NombreTurnoT,HoraInicioM,HoraFinM,HoraInicioT,HoraFinT  " +
                    "FROM [mkt].[V_TBloqueHorarioProcesaOportunidad] ";
                var dataDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(dataDB) && !dataDB.Contains("[]"))
                {
                    data = JsonConvert.DeserializeObject<List<BloqueHorarioProcesaOportunidadDTO>>(dataDB);
                }
                return data;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
