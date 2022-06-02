using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System.Linq;
using System.Linq.Expressions;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class MessengerAsesorDetalleRepositorio : BaseRepository<TMessengerAsesorDetalle, MessengerAsesorDetalleBO>
    {
        #region Metodos Base
        public MessengerAsesorDetalleRepositorio() : base()
        {
        }
        public MessengerAsesorDetalleRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<MessengerAsesorDetalleBO> GetBy(Expression<Func<TMessengerAsesorDetalle, bool>> filter)
        {
            IEnumerable<TMessengerAsesorDetalle> listado = base.GetBy(filter);
            List<MessengerAsesorDetalleBO> listadoBO = new List<MessengerAsesorDetalleBO>();
            foreach (var itemEntidad in listado)
            {
                MessengerAsesorDetalleBO objetoBO = Mapper.Map<TMessengerAsesorDetalle, MessengerAsesorDetalleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public MessengerAsesorDetalleBO FirstById(int id)
        {
            try
            {
                TMessengerAsesorDetalle entidad = base.FirstById(id);
                MessengerAsesorDetalleBO objetoBO = new MessengerAsesorDetalleBO();
                Mapper.Map<TMessengerAsesorDetalle, MessengerAsesorDetalleBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public MessengerAsesorDetalleBO FirstBy(Expression<Func<TMessengerAsesorDetalle, bool>> filter)
        {
            try
            {
                TMessengerAsesorDetalle entidad = base.FirstBy(filter);
                MessengerAsesorDetalleBO objetoBO = Mapper.Map<TMessengerAsesorDetalle, MessengerAsesorDetalleBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(MessengerAsesorDetalleBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TMessengerAsesorDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<MessengerAsesorDetalleBO> listadoBO)
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

        public bool Update(MessengerAsesorDetalleBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TMessengerAsesorDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<MessengerAsesorDetalleBO> listadoBO)
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
        private void AsignacionId(TMessengerAsesorDetalle entidad, MessengerAsesorDetalleBO objetoBO)
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

        private TMessengerAsesorDetalle MapeoEntidad(MessengerAsesorDetalleBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TMessengerAsesorDetalle entidad = new TMessengerAsesorDetalle();
                entidad = Mapper.Map<MessengerAsesorDetalleBO, TMessengerAsesorDetalle>(objetoBO,
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

        public List<MessengerAsesorDetalleDTO> ObtenerAreasPorMessengerAsesor(int idMessengerAsesor)
        {
            try
            {
                var lista = GetBy(x => x.IdMessengerAsesor == idMessengerAsesor, y => new MessengerAsesorDetalleDTO
                {
                    IdMessengerAsesor = y.IdMessengerAsesor,
                    IdAreaCapacitacionFacebook = y.IdAreaCapacitacionFacebook
                }).ToList();
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
