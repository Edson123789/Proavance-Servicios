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
    public class PreguntaFrecuenteTipoRepositorio : BaseRepository<TPreguntaFrecuenteTipo, PreguntaFrecuenteTipoBO>
    {
        #region Metodos Base
        public PreguntaFrecuenteTipoRepositorio() : base()
        {
        }
        public PreguntaFrecuenteTipoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PreguntaFrecuenteTipoBO> GetBy(Expression<Func<TPreguntaFrecuenteTipo, bool>> filter)
        {
            IEnumerable<TPreguntaFrecuenteTipo> listado = base.GetBy(filter);
            List<PreguntaFrecuenteTipoBO> listadoBO = new List<PreguntaFrecuenteTipoBO>();
            foreach (var itemEntidad in listado)
            {
                PreguntaFrecuenteTipoBO objetoBO = Mapper.Map<TPreguntaFrecuenteTipo, PreguntaFrecuenteTipoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PreguntaFrecuenteTipoBO FirstById(int id)
        {
            try
            {
                TPreguntaFrecuenteTipo entidad = base.FirstById(id);
                PreguntaFrecuenteTipoBO objetoBO = new PreguntaFrecuenteTipoBO();
                Mapper.Map<TPreguntaFrecuenteTipo, PreguntaFrecuenteTipoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PreguntaFrecuenteTipoBO FirstBy(Expression<Func<TPreguntaFrecuenteTipo, bool>> filter)
        {
            try
            {
                TPreguntaFrecuenteTipo entidad = base.FirstBy(filter);
                PreguntaFrecuenteTipoBO objetoBO = Mapper.Map<TPreguntaFrecuenteTipo, PreguntaFrecuenteTipoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PreguntaFrecuenteTipoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPreguntaFrecuenteTipo entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PreguntaFrecuenteTipoBO> listadoBO)
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

        public bool Update(PreguntaFrecuenteTipoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPreguntaFrecuenteTipo entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PreguntaFrecuenteTipoBO> listadoBO)
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
        private void AsignacionId(TPreguntaFrecuenteTipo entidad, PreguntaFrecuenteTipoBO objetoBO)
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

        private TPreguntaFrecuenteTipo MapeoEntidad(PreguntaFrecuenteTipoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPreguntaFrecuenteTipo entidad = new TPreguntaFrecuenteTipo();
                entidad = Mapper.Map<PreguntaFrecuenteTipoBO, TPreguntaFrecuenteTipo>(objetoBO,
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
        /// Elimina (Actualiza estado a false ) todos los programas PreguntaFrecuenteTipo asociados a una PreguntaFrecuente
        /// </summary>
        /// <returns></returns>
        public void EliminacionLogicoPorPreguntaFrecuente(int idPreguntaFrecuente, string usuario, List<int> nuevos)
        {
            try
            {
                var listaBorrar = GetBy(x => x.IdPreguntaFrecuente == idPreguntaFrecuente && x.Estado == true).ToList();
                listaBorrar.RemoveAll(x => nuevos.Any(y => y == x.IdTipo));
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
        public List<PreguntaFrecuenteTipoDTO> ObtenerTodoGrid()
        {
            try
            {
                var lista = GetBy(x => true, y => new PreguntaFrecuenteTipoDTO
                {
                    Id = y.Id,
                    IdPreguntaFrecuente = y.IdPreguntaFrecuente,
                    IdTipo = y.IdTipo,
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
   
