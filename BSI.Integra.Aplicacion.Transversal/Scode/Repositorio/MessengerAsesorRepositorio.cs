using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using BSI.Integra.Aplicacion.DTOs;
using Newtonsoft.Json;
using System.Linq;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class MessengerAsesorRepositorio : BaseRepository<TMessengerAsesor, MessengerAsesorBO>
    {
        #region Metodos Base
        public MessengerAsesorRepositorio() : base()
        {
        }
        public MessengerAsesorRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<MessengerAsesorBO> GetBy(Expression<Func<TMessengerAsesor, bool>> filter)
        {
            IEnumerable<TMessengerAsesor> listado = base.GetBy(filter);
            List<MessengerAsesorBO> listadoBO = new List<MessengerAsesorBO>();
            foreach (var itemEntidad in listado)
            {
                MessengerAsesorBO objetoBO = Mapper.Map<TMessengerAsesor, MessengerAsesorBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public MessengerAsesorBO FirstById(int id)
        {
            try
            {
                TMessengerAsesor entidad = base.FirstById(id);
                MessengerAsesorBO objetoBO = new MessengerAsesorBO();
                Mapper.Map<TMessengerAsesor, MessengerAsesorBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public MessengerAsesorBO FirstBy(Expression<Func<TMessengerAsesor, bool>> filter)
        {
            try
            {
                TMessengerAsesor entidad = base.FirstBy(filter);
                MessengerAsesorBO objetoBO = Mapper.Map<TMessengerAsesor, MessengerAsesorBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(MessengerAsesorBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TMessengerAsesor entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<MessengerAsesorBO> listadoBO)
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

        public bool Update(MessengerAsesorBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TMessengerAsesor entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<MessengerAsesorBO> listadoBO)
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
        private void AsignacionId(TMessengerAsesor entidad, MessengerAsesorBO objetoBO)
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

        private TMessengerAsesor MapeoEntidad(MessengerAsesorBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TMessengerAsesor entidad = new TMessengerAsesor();
                entidad = Mapper.Map<MessengerAsesorBO, TMessengerAsesor>(objetoBO,
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
        /// Obtiene Asesor Por El Area CapacitacionFacebook
        /// </summary>
        /// <param name="idAreaFacebook"></param>
        /// <returns></returns>
        public AsesorPorAreaFacebookDTO ObtenerAsesorPorAreaFacebook(int idAreaFacebook)
        {
            try
            {
                AsesorPorAreaFacebookDTO asesorPorAreaFacebookDTO = new AsesorPorAreaFacebookDTO();
                string _queryAsesor = "Select Conteo,IdPersonal From com.V_TMessengerAsesor_ObtenerAsesor where IdAreaCapacitacionFacebook = @IdAreaFacebook AND EstadoMessengerAsesor = 1 " +
                                        "AND EstadoDetalle = 1 ORDER BY Conteo";
                var queryAsesor = _dapper.FirstOrDefault(_queryAsesor, new { IdAreaFacebook = idAreaFacebook });


                if (!string.IsNullOrEmpty(queryAsesor))
                {
                    asesorPorAreaFacebookDTO = JsonConvert.DeserializeObject<AsesorPorAreaFacebookDTO>(queryAsesor);
                }

                return asesorPorAreaFacebookDTO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        //public ObtenerAreasPorAsesorDTO ObtenerAreaPorAsesor(int idPersonal)
        //{
        //    string _queryAsesor = "Select IdArea,AreaDescripcion,IdAreaFacebook,AreaFacebookDescripcion From com.V_TMessengerAsesor_ObtenerArea where IdPersonal=@IdPersonal";
        //    var queryAsesor = _dapper.FirstOrDefault(_queryAsesor, new { IdPersonal= idPersonal });
        //    return JsonConvert.DeserializeObject<ObtenerAreasPorAsesorDTO>(queryAsesor);
        //}

        public AsesorPorAreaFacebookDTO ObtenerAsesorPorSubArea(int idSubArea)
        {
            string _queryAsesor = "Select IdMessengerAsesores,IdPersonal,Email From com.V_TMessengerAsesor_ObtenerAsesor2 where IdSubAreaCapacitacion=@idSubArea";
            var queryAsesor = _dapper.FirstOrDefault(_queryAsesor, new { idSubArea = idSubArea });
            return JsonConvert.DeserializeObject<AsesorPorAreaFacebookDTO>(queryAsesor);
        }

        public List<MessengerAsesorDTO> ObtenerPanel()
        {
            try
            {
                string _query = "Select Id, IdPersonal, NombreAsesor From com.V_ObtenerPanelMessengerAsesor where Estado = 1";
                var respuesta = _dapper.QueryDapper(_query, new { });
                return JsonConvert.DeserializeObject<List<MessengerAsesorDTO>>(respuesta).OrderBy(x => x.NombreAsesor).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
