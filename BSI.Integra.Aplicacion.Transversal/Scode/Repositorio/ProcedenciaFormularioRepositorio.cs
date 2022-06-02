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
    public class ProcedenciaFormularioRepositorio : BaseRepository<TProcedenciaFormulario, ProcedenciaFormularioBO>
    {
        #region Metodos Base
        public ProcedenciaFormularioRepositorio() : base()
        {
        }
        public ProcedenciaFormularioRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ProcedenciaFormularioBO> GetBy(Expression<Func<TProcedenciaFormulario, bool>> filter)
        {
            IEnumerable<TProcedenciaFormulario> listado = base.GetBy(filter);
            List<ProcedenciaFormularioBO> listadoBO = new List<ProcedenciaFormularioBO>();
            foreach (var itemEntidad in listado)
            {
                ProcedenciaFormularioBO objetoBO = Mapper.Map<TProcedenciaFormulario, ProcedenciaFormularioBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ProcedenciaFormularioBO FirstById(int id)
        {
            try
            {
                TProcedenciaFormulario entidad = base.FirstById(id);
                ProcedenciaFormularioBO objetoBO = new ProcedenciaFormularioBO();
                Mapper.Map<TProcedenciaFormulario, ProcedenciaFormularioBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ProcedenciaFormularioBO FirstBy(Expression<Func<TProcedenciaFormulario, bool>> filter)
        {
            try
            {
                TProcedenciaFormulario entidad = base.FirstBy(filter);
                ProcedenciaFormularioBO objetoBO = Mapper.Map<TProcedenciaFormulario, ProcedenciaFormularioBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ProcedenciaFormularioBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TProcedenciaFormulario entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ProcedenciaFormularioBO> listadoBO)
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

        public bool Update(ProcedenciaFormularioBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TProcedenciaFormulario entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ProcedenciaFormularioBO> listadoBO)
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
        private void AsignacionId(TProcedenciaFormulario entidad, ProcedenciaFormularioBO objetoBO)
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

        private TProcedenciaFormulario MapeoEntidad(ProcedenciaFormularioBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TProcedenciaFormulario entidad = new TProcedenciaFormulario();
                entidad = Mapper.Map<ProcedenciaFormularioBO, TProcedenciaFormulario>(objetoBO,
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
