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
using BSI.Integra.Aplicacion.Transversal.Helper;
using System.IO;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class PlantillaAsociacionModuloSistemaRepositorio : BaseRepository<TPlantillaAsociacionModuloSistema, PlantillaAsociacionModuloSistemaBO>
    {
        #region Metodos Base
        public PlantillaAsociacionModuloSistemaRepositorio() : base()
        {
        }
        public PlantillaAsociacionModuloSistemaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PlantillaAsociacionModuloSistemaBO> GetBy(Expression<Func<TPlantillaAsociacionModuloSistema, bool>> filter)
        {
            IEnumerable<TPlantillaAsociacionModuloSistema> listado = base.GetBy(filter);
            List<PlantillaAsociacionModuloSistemaBO> listadoBO = new List<PlantillaAsociacionModuloSistemaBO>();
            foreach (var itemEntidad in listado)
            {
                PlantillaAsociacionModuloSistemaBO objetoBO = Mapper.Map<TPlantillaAsociacionModuloSistema, PlantillaAsociacionModuloSistemaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PlantillaAsociacionModuloSistemaBO FirstById(int id)
        {
            try
            {
                TPlantillaAsociacionModuloSistema entidad = base.FirstById(id);
                PlantillaAsociacionModuloSistemaBO objetoBO = new PlantillaAsociacionModuloSistemaBO();
                Mapper.Map<TPlantillaAsociacionModuloSistema, PlantillaAsociacionModuloSistemaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PlantillaAsociacionModuloSistemaBO FirstBy(Expression<Func<TPlantillaAsociacionModuloSistema, bool>> filter)
        {
            try
            {
                TPlantillaAsociacionModuloSistema entidad = base.FirstBy(filter);
                PlantillaAsociacionModuloSistemaBO objetoBO = Mapper.Map<TPlantillaAsociacionModuloSistema, PlantillaAsociacionModuloSistemaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PlantillaAsociacionModuloSistemaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPlantillaAsociacionModuloSistema entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PlantillaAsociacionModuloSistemaBO> listadoBO)
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

        public bool Update(PlantillaAsociacionModuloSistemaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPlantillaAsociacionModuloSistema entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PlantillaAsociacionModuloSistemaBO> listadoBO)
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
        private void AsignacionId(TPlantillaAsociacionModuloSistema entidad, PlantillaAsociacionModuloSistemaBO objetoBO)
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

        private TPlantillaAsociacionModuloSistema MapeoEntidad(PlantillaAsociacionModuloSistemaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPlantillaAsociacionModuloSistema entidad = new TPlantillaAsociacionModuloSistema();
                entidad = Mapper.Map<PlantillaAsociacionModuloSistemaBO, TPlantillaAsociacionModuloSistema>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //if (objetoBO.CursoPespecifico != null)
                //{
                //    TCursoPespecifico entidadHijo = new TCursoPespecifico();
                //    entidadHijo = Mapper.Map<CursoPlantillaAsociacionModuloSistemaBO, TCursoPespecifico>(objetoBO.CursoPespecifico,
                //        opt => opt.ConfigureMap(MemberList.None));
                //    entidad.TCursoPespecifico.Add(entidadHijo);
                //}
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
        /// Obtiene los modulos asociados a una plantilla
        /// </summary>
        /// <param name="idPlantilla"></param>
        /// <returns></returns>
        public List<PlantillaAsociacionModuloSistemaDTO> ObtenerPorPlantlla(int idPlantilla){
            try
            {
                return this.GetBy(x => x.Estado && x.IdPlantilla == idPlantilla, x => new PlantillaAsociacionModuloSistemaDTO { Id = x.Id, IdModuloSistema = x.IdModuloSistema, IdPlantilla = x.IdPlantilla }).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtiene todas los modulos asociados a plantillas
        /// </summary>
        /// <param name="listaIdPlantilla"></param>
        /// <returns></returns>
        public List<PlantillaAsociacionModuloSistemaDTO> ObtenerPorPlantlla(List<int> listaIdPlantilla)
        {
            try
            {
                var listaPlantillaAsociacionModuloSistema = new List<PlantillaAsociacionModuloSistemaDTO>();
                string query = $@"
                                SELECT Id, 
                                       IdPlantilla, 
                                       IdModuloSistema
                                FROM mkt.V_ObtenerPlantillaAsociacionModuloSistema
                                WHERE EstadoPlantillaAsociacionModuloSistema = 1
                                      AND EstadoPlantilla = 1
                                      AND EstadoModuloSistema = 1
                                      AND IdPlantilla IN @listaIdPlantilla
                                ";
                var plantillaAsociacionModuloSistema = _dapper.QueryDapper(query, new { listaIdPlantilla = listaIdPlantilla.ToArray() });

                if (!string.IsNullOrEmpty(plantillaAsociacionModuloSistema) && !plantillaAsociacionModuloSistema.Contains("[]"))
                {
                    listaPlantillaAsociacionModuloSistema = JsonConvert.DeserializeObject<List<PlantillaAsociacionModuloSistemaDTO>>(plantillaAsociacionModuloSistema);
                }
                return listaPlantillaAsociacionModuloSistema;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Elimina de forma fisica los registros asociados a la plantilla
        /// </summary>
        /// <param name="idPlantilla"></param>
        /// <param name="usuario"></param>
        /// <param name="nuevos"></param>
        public void EliminacionLogicoPorPlantilla(int idPlantilla, string usuario, List<int> nuevos)
        {
            try
            {
                var listaBorrar = GetBy(x => x.IdPlantilla == idPlantilla && x.Estado == true).ToList();
                listaBorrar.RemoveAll(x => nuevos.Any(y => y == x.IdModuloSistema));
                foreach (var item in listaBorrar)
                {
                    Delete(item.Id, usuario);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
