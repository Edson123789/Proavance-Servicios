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
    public class FurTipoSolicitudRepositorio : BaseRepository<TFurTipoSolicitud, FurTipoSolicitudBO>
    {
        #region Metodos Base
        public FurTipoSolicitudRepositorio() : base()
        {
        }
        public FurTipoSolicitudRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<FurTipoSolicitudBO> GetBy(Expression<Func<TFurTipoSolicitud, bool>> filter)
        {
            IEnumerable<TFurTipoSolicitud> listado = base.GetBy(filter);
            List<FurTipoSolicitudBO> listadoBO = new List<FurTipoSolicitudBO>();
            foreach (var itemEntidad in listado)
            {
                FurTipoSolicitudBO objetoBO = Mapper.Map<TFurTipoSolicitud, FurTipoSolicitudBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public FurTipoSolicitudBO FirstById(int id)
        {
            try
            {
                TFurTipoSolicitud entidad = base.FirstById(id);
                FurTipoSolicitudBO objetoBO = new FurTipoSolicitudBO();
                Mapper.Map<TFurTipoSolicitud, FurTipoSolicitudBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public FurTipoSolicitudBO FirstBy(Expression<Func<TFurTipoSolicitud, bool>> filter)
        {
            try
            {
                TFurTipoSolicitud entidad = base.FirstBy(filter);
                FurTipoSolicitudBO objetoBO = Mapper.Map<TFurTipoSolicitud, FurTipoSolicitudBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(FurTipoSolicitudBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TFurTipoSolicitud entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<FurTipoSolicitudBO> listadoBO)
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

        public bool Update(FurTipoSolicitudBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TFurTipoSolicitud entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<FurTipoSolicitudBO> listadoBO)
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
        private void AsignacionId(TFurTipoSolicitud entidad, FurTipoSolicitudBO objetoBO)
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

        private TFurTipoSolicitud MapeoEntidad(FurTipoSolicitudBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TFurTipoSolicitud entidad = new TFurTipoSolicitud();
                entidad = Mapper.Map<FurTipoSolicitudBO, TFurTipoSolicitud>(objetoBO,
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
        ///  Obtiene todos los FurTipoSolicitud.
        /// </summary>
        /// <returns></returns>
        public List<FurTipoSolicitudDTO> ObtenerFurTipoSolicitud()
        {
            try
            {
                var listaFurTipoSolicitud = GetBy(x => x.Estado == true , x => new FurTipoSolicitudDTO {
                    Id = x.Id,
                    Nombre = x.Nombre,
                }).ToList();

                return listaFurTipoSolicitud;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    } 
}
