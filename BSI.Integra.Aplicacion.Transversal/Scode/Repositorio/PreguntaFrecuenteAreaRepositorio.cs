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
    public class PreguntaFrecuenteAreaRepositorio : BaseRepository<TPreguntaFrecuenteArea, PreguntaFrecuenteAreaBO>
    {
        #region Metodos Base
        public PreguntaFrecuenteAreaRepositorio() : base()
        {
        }
        public PreguntaFrecuenteAreaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PreguntaFrecuenteAreaBO> GetBy(Expression<Func<TPreguntaFrecuenteArea, bool>> filter)
        {
            IEnumerable<TPreguntaFrecuenteArea> listado = base.GetBy(filter);
            List<PreguntaFrecuenteAreaBO> listadoBO = new List<PreguntaFrecuenteAreaBO>();
            foreach (var itemEntidad in listado)
            {
                PreguntaFrecuenteAreaBO objetoBO = Mapper.Map<TPreguntaFrecuenteArea, PreguntaFrecuenteAreaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PreguntaFrecuenteAreaBO FirstById(int id)
        {
            try
            {
                TPreguntaFrecuenteArea entidad = base.FirstById(id);
                PreguntaFrecuenteAreaBO objetoBO = new PreguntaFrecuenteAreaBO();
                Mapper.Map<TPreguntaFrecuenteArea, PreguntaFrecuenteAreaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PreguntaFrecuenteAreaBO FirstBy(Expression<Func<TPreguntaFrecuenteArea, bool>> filter)
        {
            try
            {
                TPreguntaFrecuenteArea entidad = base.FirstBy(filter);
                PreguntaFrecuenteAreaBO objetoBO = Mapper.Map<TPreguntaFrecuenteArea, PreguntaFrecuenteAreaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PreguntaFrecuenteAreaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPreguntaFrecuenteArea entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PreguntaFrecuenteAreaBO> listadoBO)
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

        public bool Update(PreguntaFrecuenteAreaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPreguntaFrecuenteArea entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PreguntaFrecuenteAreaBO> listadoBO)
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
        private void AsignacionId(TPreguntaFrecuenteArea entidad, PreguntaFrecuenteAreaBO objetoBO)
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

        private TPreguntaFrecuenteArea MapeoEntidad(PreguntaFrecuenteAreaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPreguntaFrecuenteArea entidad = new TPreguntaFrecuenteArea();
                entidad = Mapper.Map<PreguntaFrecuenteAreaBO, TPreguntaFrecuenteArea>(objetoBO,
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
        /// Elimina (Actualiza estado a false ) todos los programas PreguntaFrecuenteArea asociados a una PreguntaFrecuente
        /// </summary>
        /// <returns></returns>
        public void EliminacionLogicoPorPreguntaFrecuente(int idPreguntaFrecuente, string usuario, List<int> nuevos)
        {
            try
            {
                var listaBorrar = GetBy(x => x.IdPreguntaFrecuente == idPreguntaFrecuente && x.Estado == true).ToList();
                listaBorrar.RemoveAll(x => nuevos.Any(y => y == x.IdArea));
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
        public List<PreguntaFrecuenteAreaDTO> ObtenerTodoGrid()
        {
            try
            {
                var lista = GetBy(x => true, y => new PreguntaFrecuenteAreaDTO
                {
                    Id = y.Id,
                    IdPreguntaFrecuente = y.IdPreguntaFrecuente,
                    IdArea = y.IdArea,
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
   
