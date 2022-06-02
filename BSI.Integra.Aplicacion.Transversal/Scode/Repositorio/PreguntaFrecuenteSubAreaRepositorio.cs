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
    public class PreguntaFrecuenteSubAreaRepositorio : BaseRepository<TPreguntaFrecuenteSubArea, PreguntaFrecuenteSubAreaBO>
    {
        #region Metodos Base
        public PreguntaFrecuenteSubAreaRepositorio() : base()
        {
        }
        public PreguntaFrecuenteSubAreaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PreguntaFrecuenteSubAreaBO> GetBy(Expression<Func<TPreguntaFrecuenteSubArea, bool>> filter)
        {
            IEnumerable<TPreguntaFrecuenteSubArea> listado = base.GetBy(filter);
            List<PreguntaFrecuenteSubAreaBO> listadoBO = new List<PreguntaFrecuenteSubAreaBO>();
            foreach (var itemEntidad in listado)
            {
                PreguntaFrecuenteSubAreaBO objetoBO = Mapper.Map<TPreguntaFrecuenteSubArea, PreguntaFrecuenteSubAreaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PreguntaFrecuenteSubAreaBO FirstById(int id)
        {
            try
            {
                TPreguntaFrecuenteSubArea entidad = base.FirstById(id);
                PreguntaFrecuenteSubAreaBO objetoBO = new PreguntaFrecuenteSubAreaBO();
                Mapper.Map<TPreguntaFrecuenteSubArea, PreguntaFrecuenteSubAreaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PreguntaFrecuenteSubAreaBO FirstBy(Expression<Func<TPreguntaFrecuenteSubArea, bool>> filter)
        {
            try
            {
                TPreguntaFrecuenteSubArea entidad = base.FirstBy(filter);
                PreguntaFrecuenteSubAreaBO objetoBO = Mapper.Map<TPreguntaFrecuenteSubArea, PreguntaFrecuenteSubAreaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PreguntaFrecuenteSubAreaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPreguntaFrecuenteSubArea entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PreguntaFrecuenteSubAreaBO> listadoBO)
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

        public bool Update(PreguntaFrecuenteSubAreaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPreguntaFrecuenteSubArea entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PreguntaFrecuenteSubAreaBO> listadoBO)
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
        private void AsignacionId(TPreguntaFrecuenteSubArea entidad, PreguntaFrecuenteSubAreaBO objetoBO)
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

        private TPreguntaFrecuenteSubArea MapeoEntidad(PreguntaFrecuenteSubAreaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPreguntaFrecuenteSubArea entidad = new TPreguntaFrecuenteSubArea();
                entidad = Mapper.Map<PreguntaFrecuenteSubAreaBO, TPreguntaFrecuenteSubArea>(objetoBO,
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
        /// Elimina (Actualiza estado a false ) todos los programas PreguntaFrecuenteSubArea asociados a una PreguntaFrecuente
        /// </summary>
        /// <returns></returns>
        public void EliminacionLogicoPorPreguntaFrecuente(int idPreguntaFrecuente, string usuario, List<int> nuevos)
        {
            try
            {
                var listaBorrar = GetBy(x => x.IdPreguntaFrecuente == idPreguntaFrecuente && x.Estado == true).ToList();
                listaBorrar.RemoveAll(x => nuevos.Any(y => y == x.IdSubArea));
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

        /// <summary>
        /// Obtiene todos los registros sin los campos de auditoría.
        /// </summary>
        /// <returns></returns>
        public List<PreguntaFrecuenteSubAreaDTO> ObtenerTodoGrid()
        {
            try
            {
                var lista = GetBy(x => true, y => new PreguntaFrecuenteSubAreaDTO
                {
                    Id = y.Id,
                    IdPreguntaFrecuente = y.IdPreguntaFrecuente,
                    IdSubArea = y.IdSubArea,
                }).OrderByDescending(x => x.Id).ToList();

                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }

}
   
