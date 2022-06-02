using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class MaterialPespecificoSesionRepositorio : BaseRepository<TMaterialPespecificoSesion, MaterialPespecificoSesionBO>
    {
        #region Metodos Base
        public MaterialPespecificoSesionRepositorio() : base()
        {
        }
        public MaterialPespecificoSesionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<MaterialPespecificoSesionBO> GetBy(Expression<Func<TMaterialPespecificoSesion, bool>> filter)
        {
            IEnumerable<TMaterialPespecificoSesion> listado = base.GetBy(filter);
            List<MaterialPespecificoSesionBO> listadoBO = new List<MaterialPespecificoSesionBO>();
            foreach (var itemEntidad in listado)
            {
                MaterialPespecificoSesionBO objetoBO = Mapper.Map<TMaterialPespecificoSesion, MaterialPespecificoSesionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public MaterialPespecificoSesionBO FirstById(int id)
        {
            try
            {
                TMaterialPespecificoSesion entidad = base.FirstById(id);
                MaterialPespecificoSesionBO objetoBO = new MaterialPespecificoSesionBO();
                Mapper.Map<TMaterialPespecificoSesion, MaterialPespecificoSesionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public MaterialPespecificoSesionBO FirstBy(Expression<Func<TMaterialPespecificoSesion, bool>> filter)
        {
            try
            {
                TMaterialPespecificoSesion entidad = base.FirstBy(filter);
                MaterialPespecificoSesionBO objetoBO = Mapper.Map<TMaterialPespecificoSesion, MaterialPespecificoSesionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(MaterialPespecificoSesionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TMaterialPespecificoSesion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<MaterialPespecificoSesionBO> listadoBO)
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

        public bool Update(MaterialPespecificoSesionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TMaterialPespecificoSesion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<MaterialPespecificoSesionBO> listadoBO)
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
        private void AsignacionId(TMaterialPespecificoSesion entidad, MaterialPespecificoSesionBO objetoBO)
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

        private TMaterialPespecificoSesion MapeoEntidad(MaterialPespecificoSesionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TMaterialPespecificoSesion entidad = new TMaterialPespecificoSesion();
                entidad = Mapper.Map<MaterialPespecificoSesionBO, TMaterialPespecificoSesion>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos
                if (objetoBO.ListaMaterialVersion != null && objetoBO.ListaMaterialVersion.Count > 0)
                {
                    foreach (var hijo in objetoBO.ListaMaterialVersion)
                    {
                        TMaterialVersion entidadHijo = new TMaterialVersion();
                        entidadHijo = Mapper.Map<MaterialVersionBO, TMaterialVersion>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TMaterialVersion.Add(entidadHijo);
                    }
                }

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<MaterialPespecificoSesionBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TMaterialPespecificoSesion, bool>>> filters, Expression<Func<TMaterialPespecificoSesion, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TMaterialPespecificoSesion> listado = base.GetFiltered(filters, orderBy, ascending);
            List<MaterialPespecificoSesionBO> listadoBO = new List<MaterialPespecificoSesionBO>();

            foreach (var itemEntidad in listado)
            {
                MaterialPespecificoSesionBO objetoBO = Mapper.Map<TMaterialPespecificoSesion, MaterialPespecificoSesionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion

        /// <summary>
        /// Obtiene los materiales por sesion
        /// </summary>
        /// <param name="idPEspecificoSesion"></param>
        /// <returns></returns>
        public List<MaterialPespecificoSesionBO> ObtenerPorPEspecificoSesion(int idPEspecificoSesion) {
            try
            {
                return this.GetBy(x => x.Estado && x.IdPespecificoSesion == idPEspecificoSesion).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
