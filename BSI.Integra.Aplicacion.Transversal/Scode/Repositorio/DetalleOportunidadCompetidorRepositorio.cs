using System;
using System.Collections.Generic;
using System.Linq;
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
    public class DetalleOportunidadCompetidorRepositorio : BaseRepository<TDetalleOportunidadCompetidor, DetalleOportunidadCompetidorBO>
    {
        #region Metodos Base
        public DetalleOportunidadCompetidorRepositorio() : base()
        {
        }
        public DetalleOportunidadCompetidorRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<DetalleOportunidadCompetidorBO> GetBy(Expression<Func<TDetalleOportunidadCompetidor, bool>> filter)
        {
            IEnumerable<TDetalleOportunidadCompetidor> listado = base.GetBy(filter).ToList();
            List<DetalleOportunidadCompetidorBO> listadoBO = new List<DetalleOportunidadCompetidorBO>();
            foreach (var itemEntidad in listado)
            {
                DetalleOportunidadCompetidorBO objetoBO = Mapper.Map<TDetalleOportunidadCompetidor, DetalleOportunidadCompetidorBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public DetalleOportunidadCompetidorBO FirstById(int id)
        {
            try
            {
                TDetalleOportunidadCompetidor entidad = base.FirstById(id);
                DetalleOportunidadCompetidorBO objetoBO = new DetalleOportunidadCompetidorBO();
                Mapper.Map<TDetalleOportunidadCompetidor, DetalleOportunidadCompetidorBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public DetalleOportunidadCompetidorBO FirstBy(Expression<Func<TDetalleOportunidadCompetidor, bool>> filter)
        {
            try
            {
                TDetalleOportunidadCompetidor entidad = base.FirstBy(filter);
                DetalleOportunidadCompetidorBO objetoBO = Mapper.Map<TDetalleOportunidadCompetidor, DetalleOportunidadCompetidorBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(DetalleOportunidadCompetidorBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TDetalleOportunidadCompetidor entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<DetalleOportunidadCompetidorBO> listadoBO)
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

        public bool Update(DetalleOportunidadCompetidorBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TDetalleOportunidadCompetidor entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<DetalleOportunidadCompetidorBO> listadoBO)
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
        private void AsignacionId(TDetalleOportunidadCompetidor entidad, DetalleOportunidadCompetidorBO objetoBO)
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

        private TDetalleOportunidadCompetidor MapeoEntidad(DetalleOportunidadCompetidorBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TDetalleOportunidadCompetidor entidad = new TDetalleOportunidadCompetidor();
                entidad = Mapper.Map<DetalleOportunidadCompetidorBO, TDetalleOportunidadCompetidor>(objetoBO,
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
        /// Obtiene una lista de detalle oportunidad competidor por idOportunidadCompetidor
        /// </summary>
        /// <param name="idOportunidadCompetidor"></param>
        /// <returns></returns>
        public List<DetalleOportunidadCompetidorDTO> ObtenerPorOportunidadCompetidor(int idOportunidadCompetidor)
        {
            try
            {
                List<DetalleOportunidadCompetidorDTO> detalleOportunidadCompetidor = new List<DetalleOportunidadCompetidorDTO>();
                string _query = "SELECT Id, IdOportunidadCompetidor, IdCompetidor FROM com.T_DetalleOportunidadCompetidor WHERE  IdOportunidadCompetidor = @idOportunidadCompetidor AND Estado = 1";
                var detalleOportunidadCompetidorDB = _dapper.QueryDapper(_query, new { idOportunidadCompetidor });
                if (!string.IsNullOrEmpty(detalleOportunidadCompetidorDB) && !detalleOportunidadCompetidorDB.Contains("[]"))
                {
                    detalleOportunidadCompetidor = JsonConvert.DeserializeObject<List<DetalleOportunidadCompetidorDTO>>(detalleOportunidadCompetidorDB);
                }
                return detalleOportunidadCompetidor;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Elimina (Actualiza estado a false ) los competidores que ya no estan en la lista nueva.
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public void DeleteLogicoPorOportunidadCompetidor(int idOportunidadCompetidor, string usuario, List<int> nuevos)
        {
            try
            {
                List<EliminacionOportunidadCompetidorDTO> listaBorrar = new List<EliminacionOportunidadCompetidorDTO>();
                listaBorrar = GetBy(x => x.IdOportunidadCompetidor == idOportunidadCompetidor, y => new EliminacionOportunidadCompetidorDTO()
                {
                    Id = y.Id,
                    IdCompetidor = y.IdCompetidor
                }).ToList();
                listaBorrar.RemoveAll(x => nuevos.Any(y => y == x.IdCompetidor));
                this.Delete(listaBorrar.Select(x => x.Id), usuario);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
