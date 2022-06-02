using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class CursoPespecificoRepositorio : BaseRepository<TCursoPespecifico, CursoPespecificoBO>
    {
        #region Metodos Base
        public CursoPespecificoRepositorio() : base()
        {
        }
        public CursoPespecificoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CursoPespecificoBO> GetBy(Expression<Func<TCursoPespecifico, bool>> filter)
        {
            IEnumerable<TCursoPespecifico> listado = base.GetBy(filter);
            List<CursoPespecificoBO> listadoBO = new List<CursoPespecificoBO>();
            foreach (var itemEntidad in listado)
            {
                CursoPespecificoBO objetoBO = Mapper.Map<TCursoPespecifico, CursoPespecificoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CursoPespecificoBO FirstById(int id)
        {
            try
            {
                TCursoPespecifico entidad = base.FirstById(id);
                CursoPespecificoBO objetoBO = new CursoPespecificoBO();
                Mapper.Map<TCursoPespecifico, CursoPespecificoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CursoPespecificoBO FirstBy(Expression<Func<TCursoPespecifico, bool>> filter)
        {
            try
            {
                TCursoPespecifico entidad = base.FirstBy(filter);
                CursoPespecificoBO objetoBO = Mapper.Map<TCursoPespecifico, CursoPespecificoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CursoPespecificoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCursoPespecifico entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CursoPespecificoBO> listadoBO)
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

        public bool Update(CursoPespecificoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCursoPespecifico entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CursoPespecificoBO> listadoBO)
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
        private void AsignacionId(TCursoPespecifico entidad, CursoPespecificoBO objetoBO)
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

        private TCursoPespecifico MapeoEntidad(CursoPespecificoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCursoPespecifico entidad = new TCursoPespecifico();
                entidad = Mapper.Map<CursoPespecificoBO, TCursoPespecifico>(objetoBO,
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
    }
}
