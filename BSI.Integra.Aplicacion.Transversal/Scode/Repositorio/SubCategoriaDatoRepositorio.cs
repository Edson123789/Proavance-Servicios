using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class SubCategoriaDatoRepositorio : BaseRepository<TSubCategoriaDato, SubCategoriaDatoBO>
    {
        #region Metodos Base
        public SubCategoriaDatoRepositorio() : base()
        {
        }
        public SubCategoriaDatoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<SubCategoriaDatoBO> GetBy(Expression<Func<TSubCategoriaDato, bool>> filter)
        {
            IEnumerable<TSubCategoriaDato> listado = base.GetBy(filter);
            List<SubCategoriaDatoBO> listadoBO = new List<SubCategoriaDatoBO>();
            foreach (var itemEntidad in listado)
            {
                SubCategoriaDatoBO objetoBO = Mapper.Map<TSubCategoriaDato, SubCategoriaDatoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public SubCategoriaDatoBO FirstById(int id)
        {
            try
            {
                TSubCategoriaDato entidad = base.FirstById(id);
                SubCategoriaDatoBO objetoBO = new SubCategoriaDatoBO();
                Mapper.Map<TSubCategoriaDato, SubCategoriaDatoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public SubCategoriaDatoBO FirstBy(Expression<Func<TSubCategoriaDato, bool>> filter)
        {
            try
            {
                TSubCategoriaDato entidad = base.FirstBy(filter);
                SubCategoriaDatoBO objetoBO = Mapper.Map<TSubCategoriaDato, SubCategoriaDatoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(SubCategoriaDatoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TSubCategoriaDato entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<SubCategoriaDatoBO> listadoBO)
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

        public bool Update(SubCategoriaDatoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TSubCategoriaDato entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<SubCategoriaDatoBO> listadoBO)
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
        private void AsignacionId(TSubCategoriaDato entidad, SubCategoriaDatoBO objetoBO)
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

        private TSubCategoriaDato MapeoEntidad(SubCategoriaDatoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TSubCategoriaDato entidad = new TSubCategoriaDato();
                entidad = Mapper.Map<SubCategoriaDatoBO, TSubCategoriaDato>(objetoBO,
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
        /// Obtiene todos los registros sin los campos de auditoria
        /// </summary>
        /// <returns></returns>
        public List<SubCategoriaDatoDTO> ObtenerTodoGrid()
        {
            try
            {
                var lista = GetBy(x => true, y => new SubCategoriaDatoDTO
                {
                    Id = y.Id,
                    IdCategoriaOrigen = y.IdCategoriaOrigen,
                    IdTipoFormulario = y.IdTipoFormulario
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
