using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class FaseByPlantillaRepositorio : BaseRepository<TFaseByPlantilla, FaseByPlantillaBO>
    {
        #region Metodos Base
        public FaseByPlantillaRepositorio() : base()
        {
        }
        public FaseByPlantillaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<FaseByPlantillaBO> GetBy(Expression<Func<TFaseByPlantilla, bool>> filter)
        {
            IEnumerable<TFaseByPlantilla> listado = base.GetBy(filter);
            List<FaseByPlantillaBO> listadoBO = new List<FaseByPlantillaBO>();
            foreach (var itemEntidad in listado)
            {
                FaseByPlantillaBO objetoBO = Mapper.Map<TFaseByPlantilla, FaseByPlantillaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public FaseByPlantillaBO FirstById(int id)
        {
            try
            {
                TFaseByPlantilla entidad = base.FirstById(id);
                FaseByPlantillaBO objetoBO = new FaseByPlantillaBO();
                Mapper.Map<TFaseByPlantilla, FaseByPlantillaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public FaseByPlantillaBO FirstBy(Expression<Func<TFaseByPlantilla, bool>> filter)
        {
            try
            {
                TFaseByPlantilla entidad = base.FirstBy(filter);
                FaseByPlantillaBO objetoBO = Mapper.Map<TFaseByPlantilla, FaseByPlantillaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(FaseByPlantillaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TFaseByPlantilla entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<FaseByPlantillaBO> listadoBO)
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

        public bool Update(FaseByPlantillaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TFaseByPlantilla entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<FaseByPlantillaBO> listadoBO)
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
        private void AsignacionId(TFaseByPlantilla entidad, FaseByPlantillaBO objetoBO)
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

        private TFaseByPlantilla MapeoEntidad(FaseByPlantillaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TFaseByPlantilla entidad = new TFaseByPlantilla();
                entidad = Mapper.Map<FaseByPlantillaBO, TFaseByPlantilla>(objetoBO,
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
        /// Obtiene todas las fases por Plantilla
        /// </summary>
        /// /// <param name="id"></param>
        /// <returns></returns>
        public List<PlantillaFasesDTO> ObtenerFasesPlantilla(int id)
        {
            try
            {
                var listaFases = GetBy(x => x.IdPlantilla == id, y => new PlantillaFasesDTO
                {
                    IdPlantilla = y.IdPlantilla,
                    IdFaseOrigen = y.IdFaseOrigen,
                    FechaCreacion = y.FechaCreacion

                }).ToList();

                return listaFases;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Elimina (Actualiza estado a false ) todos las Fases Plantilla asociados a una Plantilla
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public void EliminacionLogicoPorPlantilla(int idPlantilla, string usuario, List<int> nuevos)
        {
            try
            {
                var listaBorrar = GetBy(x => x.IdPlantilla == idPlantilla && x.Estado == true).ToList();
                listaBorrar.RemoveAll(x => nuevos.Any(y => y == x.idFaseOrigen));
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