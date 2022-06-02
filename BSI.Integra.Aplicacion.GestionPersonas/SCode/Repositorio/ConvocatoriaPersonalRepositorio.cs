using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.GestionPersonas;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.GestionPersonas.Repositorio
{
    public class ConvocatoriaPersonalRepositorio : BaseRepository<TConvocatoriaPersonal, ConvocatoriaPersonalBO>
    {
        #region Metodos Base
        public ConvocatoriaPersonalRepositorio() : base()
        {
        }
        public ConvocatoriaPersonalRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ConvocatoriaPersonalBO> GetBy(Expression<Func<TConvocatoriaPersonal, bool>> filter)
        {
            IEnumerable<TConvocatoriaPersonal> listado = base.GetBy(filter);
            List<ConvocatoriaPersonalBO> listadoBO = new List<ConvocatoriaPersonalBO>();
            foreach (var itemEntidad in listado)
            {
                ConvocatoriaPersonalBO objetoBO = Mapper.Map<TConvocatoriaPersonal, ConvocatoriaPersonalBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ConvocatoriaPersonalBO FirstById(int id)
        {
            try
            {
                TConvocatoriaPersonal entidad = base.FirstById(id);
                ConvocatoriaPersonalBO objetoBO = new ConvocatoriaPersonalBO();
                Mapper.Map<TConvocatoriaPersonal, ConvocatoriaPersonalBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ConvocatoriaPersonalBO FirstBy(Expression<Func<TConvocatoriaPersonal, bool>> filter)
        {
            try
            {
                TConvocatoriaPersonal entidad = base.FirstBy(filter);
                ConvocatoriaPersonalBO objetoBO = Mapper.Map<TConvocatoriaPersonal, ConvocatoriaPersonalBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ConvocatoriaPersonalBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TConvocatoriaPersonal entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ConvocatoriaPersonalBO> listadoBO)
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

        public bool Update(ConvocatoriaPersonalBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TConvocatoriaPersonal entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ConvocatoriaPersonalBO> listadoBO)
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
        private void AsignacionId(TConvocatoriaPersonal entidad, ConvocatoriaPersonalBO objetoBO)
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

        private TConvocatoriaPersonal MapeoEntidad(ConvocatoriaPersonalBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TConvocatoriaPersonal entidad = new TConvocatoriaPersonal();
                entidad = Mapper.Map<ConvocatoriaPersonalBO, TConvocatoriaPersonal>(objetoBO,
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

        /// Repositorio: ConvocatoriaPersonalRepositorio
        /// Autor: Luis H, Britsel C.
        /// Fecha: 24/04/2021
        /// <summary>
        /// Este método obtiene la lista de convocatorias registrados en la base de datos
        /// </summary>
        /// <returns> List<ConvocatoriaPersonalDTO> </returns>
        public List<ConvocatoriaPersonalDTO> ObtenerConvocatoriasRegistradas() 
        {
            try
            {
                List<ConvocatoriaPersonalDTO> listaConvocatoriaPersonal = new List<ConvocatoriaPersonalDTO>();
                var query = @"SELECT 
                                Id, 
                                Nombre,
                                NombreProcesoSeleccion,
                                Codigo,
                                FechaInicio,
                                FechaFin,
                                CuerpoConvocatoria,
                                IdSedeTrabajo,
                                SedeTrabajo,
                                IdPersonal,
                                PersonalEncargado,
                                IdProveedor,
                                Proveedor,
                                IdArea,
                                Area,
                                Activo,
                                UrlAviso
                            FROM gp.V_TConvocatoriaPersonal_ObtenerProcesoSeleccion";
                var respuesta = _dapper.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
                {
                    listaConvocatoriaPersonal = JsonConvert.DeserializeObject<List<ConvocatoriaPersonalDTO>>(respuesta);
                }
                return listaConvocatoriaPersonal;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
