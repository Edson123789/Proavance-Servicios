using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class PagoFinalRepositorio : BaseRepository<TPagoFinal, PagoFinalBO>
    {
        #region Metodos Base
        public PagoFinalRepositorio() : base()
        {
        }
        public PagoFinalRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PagoFinalBO> GetBy(Expression<Func<TPagoFinal, bool>> filter)
        {
            IEnumerable<TPagoFinal> listado = base.GetBy(filter);
            List<PagoFinalBO> listadoBO = new List<PagoFinalBO>();
            foreach (var itemEntidad in listado)
            {
                PagoFinalBO objetoBO = Mapper.Map<TPagoFinal, PagoFinalBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PagoFinalBO FirstById(int id)
        {
            try
            {
                TPagoFinal entidad = base.FirstById(id);
                PagoFinalBO objetoBO = new PagoFinalBO();
                Mapper.Map<TPagoFinal, PagoFinalBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PagoFinalBO FirstBy(Expression<Func<TPagoFinal, bool>> filter)
        {
            try
            {
                TPagoFinal entidad = base.FirstBy(filter);
                PagoFinalBO objetoBO = Mapper.Map<TPagoFinal, PagoFinalBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PagoFinalBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPagoFinal entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PagoFinalBO> listadoBO)
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

        public bool Update(PagoFinalBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPagoFinal entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PagoFinalBO> listadoBO)
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
        private void AsignacionId(TPagoFinal entidad, PagoFinalBO objetoBO)
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

        private TPagoFinal MapeoEntidad(PagoFinalBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPagoFinal entidad = new TPagoFinal();
                entidad = Mapper.Map<PagoFinalBO, TPagoFinal>(objetoBO,
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
