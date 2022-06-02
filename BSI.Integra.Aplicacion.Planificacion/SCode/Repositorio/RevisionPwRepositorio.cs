using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Planificacion.Repositorio
{
    public class RevisionPwRepositorio : BaseRepository<TRevisionPw, RevisionPwBO>
    {
        #region Metodos Base
        public RevisionPwRepositorio() : base()
        {
        }
        public RevisionPwRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<RevisionPwBO> GetBy(Expression<Func<TRevisionPw, bool>> filter)
        {
            IEnumerable<TRevisionPw> listado = base.GetBy(filter);
            List<RevisionPwBO> listadoBO = new List<RevisionPwBO>();
            foreach (var itemEntidad in listado)
            {
                RevisionPwBO objetoBO = Mapper.Map<TRevisionPw, RevisionPwBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public RevisionPwBO FirstById(int id)
        {
            try
            {
                TRevisionPw entidad = base.FirstById(id);
                RevisionPwBO objetoBO = new RevisionPwBO();
                Mapper.Map<TRevisionPw, RevisionPwBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public RevisionPwBO FirstBy(Expression<Func<TRevisionPw, bool>> filter)
        {
            try
            {
                TRevisionPw entidad = base.FirstBy(filter);
                RevisionPwBO objetoBO = Mapper.Map<TRevisionPw, RevisionPwBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(RevisionPwBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TRevisionPw entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<RevisionPwBO> listadoBO)
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

        public bool Update(RevisionPwBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TRevisionPw entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<RevisionPwBO> listadoBO)
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
        private void AsignacionId(TRevisionPw entidad, RevisionPwBO objetoBO)
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

        private TRevisionPw MapeoEntidad(RevisionPwBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TRevisionPw entidad = new TRevisionPw();
                entidad = Mapper.Map<RevisionPwBO, TRevisionPw>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos
                if (objetoBO.RevisionNivel != null && objetoBO.RevisionNivel.Count > 0)
                {
                    foreach (var hijo in objetoBO.RevisionNivel)
                    {
                        TRevisionNivelPw entidadHijo = new TRevisionNivelPw();
                        entidadHijo = Mapper.Map<RevisionNivelPwBO, TRevisionNivelPw>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TRevisionNivelPw.Add(entidadHijo);
                    }
                }

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
		#endregion
        
        /// <summary>
        /// Obtiene todos los registros sin los campos de auditoría.
        /// </summary>
        /// <returns></returns>
        public List<RevisionPwDTO> ObtenerTodoGrid()
        {
            try
            {
                var lista = GetBy(x => true, y => new RevisionPwDTO
                {
                    Id = y.Id,
                    Nombre = y.Nombre,
                    Descripcion = y.Descripcion,
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
