using AutoMapper;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
    public class CampaniaGeneralDetalleResponsableRepositorio : BaseRepository<TCampaniaGeneralDetalleResponsable, CampaniaGeneralDetalleResponsableBO>
    {
        #region Metodos Base
        public CampaniaGeneralDetalleResponsableRepositorio() : base()
        {
        }
        public CampaniaGeneralDetalleResponsableRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CampaniaGeneralDetalleResponsableBO> GetBy(Expression<Func<TCampaniaGeneralDetalleResponsable, bool>> filter)
        {
            IEnumerable<TCampaniaGeneralDetalleResponsable> listado = base.GetBy(filter);
            List<CampaniaGeneralDetalleResponsableBO> listadoBO = new List<CampaniaGeneralDetalleResponsableBO>();
            foreach (var itemEntidad in listado)
            {
                CampaniaGeneralDetalleResponsableBO objetoBO = Mapper.Map<TCampaniaGeneralDetalleResponsable, CampaniaGeneralDetalleResponsableBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CampaniaGeneralDetalleResponsableBO FirstById(int id)
        {
            try
            {
                TCampaniaGeneralDetalleResponsable entidad = base.FirstById(id);
                CampaniaGeneralDetalleResponsableBO objetoBO = new CampaniaGeneralDetalleResponsableBO();
                Mapper.Map<TCampaniaGeneralDetalleResponsable, CampaniaGeneralDetalleResponsableBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CampaniaGeneralDetalleResponsableBO FirstBy(Expression<Func<TCampaniaGeneralDetalleResponsable, bool>> filter)
        {
            try
            {
                TCampaniaGeneralDetalleResponsable entidad = base.FirstBy(filter);
                CampaniaGeneralDetalleResponsableBO objetoBO = Mapper.Map<TCampaniaGeneralDetalleResponsable, CampaniaGeneralDetalleResponsableBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CampaniaGeneralDetalleResponsableBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCampaniaGeneralDetalleResponsable entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CampaniaGeneralDetalleResponsableBO> listadoBO)
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

        public bool Update(CampaniaGeneralDetalleResponsableBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCampaniaGeneralDetalleResponsable entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CampaniaGeneralDetalleResponsableBO> listadoBO)
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
        private void AsignacionId(TCampaniaGeneralDetalleResponsable entidad, CampaniaGeneralDetalleResponsableBO objetoBO)
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

        private TCampaniaGeneralDetalleResponsable MapeoEntidad(CampaniaGeneralDetalleResponsableBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCampaniaGeneralDetalleResponsable entidad = new TCampaniaGeneralDetalleResponsable();
                entidad = Mapper.Map<CampaniaGeneralDetalleResponsableBO, TCampaniaGeneralDetalleResponsable>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<CampaniaGeneralDetalleResponsableBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TCampaniaGeneralDetalleResponsable, bool>>> filters, Expression<Func<TCampaniaGeneralDetalleResponsable, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TCampaniaGeneralDetalleResponsable> listado = base.GetFiltered(filters, orderBy, ascending);
            List<CampaniaGeneralDetalleResponsableBO> listadoBO = new List<CampaniaGeneralDetalleResponsableBO>();

            foreach (var itemEntidad in listado)
            {
                CampaniaGeneralDetalleResponsableBO objetoBO = Mapper.Map<TCampaniaGeneralDetalleResponsable, CampaniaGeneralDetalleResponsableBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion
    }
}
