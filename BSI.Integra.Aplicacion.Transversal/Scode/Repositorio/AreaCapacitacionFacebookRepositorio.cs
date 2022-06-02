using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class AreaCapacitacionFacebookRepositorio : BaseRepository<TAreaCapacitacionFacebook, AreaCapacitacionFacebookBO>
    {
        #region Metodos Base
        public AreaCapacitacionFacebookRepositorio() : base()
        {
        }
        public AreaCapacitacionFacebookRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<AreaCapacitacionFacebookBO> GetBy(Expression<Func<TAreaCapacitacionFacebook, bool>> filter)
        {
            IEnumerable<TAreaCapacitacionFacebook> listado = base.GetBy(filter);
            List<AreaCapacitacionFacebookBO> listadoBO = new List<AreaCapacitacionFacebookBO>();
            foreach (var itemEntidad in listado)
            {
                AreaCapacitacionFacebookBO objetoBO = Mapper.Map<TAreaCapacitacionFacebook, AreaCapacitacionFacebookBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public AreaCapacitacionFacebookBO FirstById(int id)
        {
            try
            {
                TAreaCapacitacionFacebook entidad = base.FirstById(id);
                AreaCapacitacionFacebookBO objetoBO = new AreaCapacitacionFacebookBO();
                Mapper.Map<TAreaCapacitacionFacebook, AreaCapacitacionFacebookBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public AreaCapacitacionFacebookBO FirstBy(Expression<Func<TAreaCapacitacionFacebook, bool>> filter)
        {
            try
            {
                TAreaCapacitacionFacebook entidad = base.FirstBy(filter);
                AreaCapacitacionFacebookBO objetoBO = Mapper.Map<TAreaCapacitacionFacebook, AreaCapacitacionFacebookBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(AreaCapacitacionFacebookBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TAreaCapacitacionFacebook entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<AreaCapacitacionFacebookBO> listadoBO)
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

        public bool Update(AreaCapacitacionFacebookBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TAreaCapacitacionFacebook entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<AreaCapacitacionFacebookBO> listadoBO)
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
        private void AsignacionId(TAreaCapacitacionFacebook entidad, AreaCapacitacionFacebookBO objetoBO)
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

        private TAreaCapacitacionFacebook MapeoEntidad(AreaCapacitacionFacebookBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TAreaCapacitacionFacebook entidad = new TAreaCapacitacionFacebook();
                entidad = Mapper.Map<AreaCapacitacionFacebookBO, TAreaCapacitacionFacebook>(objetoBO,
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

        public AreaCapacitacionFacebookDescripcionDTO ObtenerDescripcion(int areaPostback)
        {
            string _queryDecripcion = "Select Descripcion From pla.V_TAreaCapacitacionFacebook_ObtenerDescripcion Where Id=@Id and Estado=1";
            var queryDescripcion = _dapper.FirstOrDefault(_queryDecripcion, new { Id = areaPostback });
            return JsonConvert.DeserializeObject<AreaCapacitacionFacebookDescripcionDTO>(queryDescripcion);
        }

        /// <summary>
        /// Se obtiene la descripcion de las Areas
        /// </summary>
        /// <returns></returns>
        public List<AreaCapacitacionFacebookDescripcionDTO> ObtenerAreas()
        {
            try
            {
                var lista = GetBy(x => true, y => new AreaCapacitacionFacebookDescripcionDTO
                {
                    Id = y.Id,
                    Descripcion = y.Descripcion,
                    Orden = y.Orden
                }).OrderBy(x => x.Orden).ToList();
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
