using System;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Operaciones.Repositorio
{
    public class DuracionAvanceAcademicoMoodleRepositorio : BaseRepository<TDuracionAvanceAcademicoMoodle, DuracionAvanceAcademicoMoodleBO>
    {
        #region Metodos Base
        public DuracionAvanceAcademicoMoodleRepositorio() : base()
        {
        }
        public DuracionAvanceAcademicoMoodleRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<DuracionAvanceAcademicoMoodleBO> GetBy(Expression<Func<TDuracionAvanceAcademicoMoodle, bool>> filter)
        {
            IEnumerable<TDuracionAvanceAcademicoMoodle> listado = base.GetBy(filter);
            List<DuracionAvanceAcademicoMoodleBO> listadoBO = new List<DuracionAvanceAcademicoMoodleBO>();
            foreach (var itemEntidad in listado)
            {
                DuracionAvanceAcademicoMoodleBO objetoBO = Mapper.Map<TDuracionAvanceAcademicoMoodle, DuracionAvanceAcademicoMoodleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public DuracionAvanceAcademicoMoodleBO FirstById(int id)
        {
            try
            {
                TDuracionAvanceAcademicoMoodle entidad = base.FirstById(id);
                DuracionAvanceAcademicoMoodleBO objetoBO = new DuracionAvanceAcademicoMoodleBO();
                Mapper.Map<TDuracionAvanceAcademicoMoodle, DuracionAvanceAcademicoMoodleBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public DuracionAvanceAcademicoMoodleBO FirstBy(Expression<Func<TDuracionAvanceAcademicoMoodle, bool>> filter)
        {
            try
            {
                TDuracionAvanceAcademicoMoodle entidad = base.FirstBy(filter);
                DuracionAvanceAcademicoMoodleBO objetoBO = Mapper.Map<TDuracionAvanceAcademicoMoodle, DuracionAvanceAcademicoMoodleBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(DuracionAvanceAcademicoMoodleBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TDuracionAvanceAcademicoMoodle entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<DuracionAvanceAcademicoMoodleBO> listadoBO)
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

        public bool Update(DuracionAvanceAcademicoMoodleBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TDuracionAvanceAcademicoMoodle entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<DuracionAvanceAcademicoMoodleBO> listadoBO)
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
        private void AsignacionId(TDuracionAvanceAcademicoMoodle entidad, DuracionAvanceAcademicoMoodleBO objetoBO)
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

        private TDuracionAvanceAcademicoMoodle MapeoEntidad(DuracionAvanceAcademicoMoodleBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TDuracionAvanceAcademicoMoodle entidad = new TDuracionAvanceAcademicoMoodle();
                entidad = Mapper.Map<DuracionAvanceAcademicoMoodleBO, TDuracionAvanceAcademicoMoodle>(objetoBO,
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
