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
    public class SubAreaParametroSeoRepositorio : BaseRepository<TSubAreaParametroSeoPw, SubAreaParametroSeoBO>
    {
        #region Metodos Base
        public SubAreaParametroSeoRepositorio() : base()
        {
        }
        public SubAreaParametroSeoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<SubAreaParametroSeoBO> GetBy(Expression<Func<TSubAreaParametroSeoPw, bool>> filter)
        {
            IEnumerable<TSubAreaParametroSeoPw> listado = base.GetBy(filter);
            List<SubAreaParametroSeoBO> listadoBO = new List<SubAreaParametroSeoBO>();
            foreach (var itemEntidad in listado)
            {
                SubAreaParametroSeoBO objetoBO = Mapper.Map<TSubAreaParametroSeoPw, SubAreaParametroSeoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public SubAreaParametroSeoBO FirstById(int id)
        {
            try
            {
                TSubAreaParametroSeoPw entidad = base.FirstById(id);
                SubAreaParametroSeoBO objetoBO = new SubAreaParametroSeoBO();
                Mapper.Map<TSubAreaParametroSeoPw, SubAreaParametroSeoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public SubAreaParametroSeoBO FirstBy(Expression<Func<TSubAreaParametroSeoPw, bool>> filter)
        {
            try
            {
                TSubAreaParametroSeoPw entidad = base.FirstBy(filter);
                SubAreaParametroSeoBO objetoBO = Mapper.Map<TSubAreaParametroSeoPw, SubAreaParametroSeoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(SubAreaParametroSeoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TSubAreaParametroSeoPw entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<SubAreaParametroSeoBO> listadoBO)
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

        public bool Update(SubAreaParametroSeoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TSubAreaParametroSeoPw entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<SubAreaParametroSeoBO> listadoBO)
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
        private void AsignacionId(TSubAreaParametroSeoPw entidad, SubAreaParametroSeoBO objetoBO)
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

        private TSubAreaParametroSeoPw MapeoEntidad(SubAreaParametroSeoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TSubAreaParametroSeoPw entidad = new TSubAreaParametroSeoPw();
                entidad = Mapper.Map<SubAreaParametroSeoBO, TSubAreaParametroSeoPw>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<SubAreaParametroSeoBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TSubAreaParametroSeoPw, bool>>> filters, Expression<Func<TSubAreaParametroSeoPw, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TSubAreaParametroSeoPw> listado = base.GetFiltered(filters, orderBy, ascending);
            List<SubAreaParametroSeoBO> listadoBO = new List<SubAreaParametroSeoBO>();

            foreach (var itemEntidad in listado)
            {
                SubAreaParametroSeoBO objetoBO = Mapper.Map<TSubAreaParametroSeoPw, SubAreaParametroSeoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion

        /// <summary>
        /// Elimina (Actualiza estado a false ) todos las registros asociados a SubAreaCapacitacion
        /// </summary>
        /// <param name="idSubAreaCapacitacion"></param>
        /// <param name="usuario"></param>
        /// <param name="nuevos"></param>
        public void EliminacionLogicoPorSubAreaCapacitacion(int idSubAreaCapacitacion, string usuario, List<ParametroContenidoDTO> nuevos)
        {
            try
            {
                var listaBorrar = GetBy(x => x.IdSubAreaCapacitacion == idSubAreaCapacitacion && x.Estado == true).ToList();
                listaBorrar.RemoveAll(x => nuevos.Any(y => y.Equals(x.IdParametroSeoPw)));
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
