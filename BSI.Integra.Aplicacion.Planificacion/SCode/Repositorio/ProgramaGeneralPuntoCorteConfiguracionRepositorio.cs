using AutoMapper;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Planificacion.Repositorio
{
    public class ProgramaGeneralPuntoCorteConfiguracionRepositorio : BaseRepository<TProgramaGeneralPuntoCorteConfiguracion, ProgramaGeneralPuntoCorteConfiguracionBO>
    {
        #region Metodos Base
        public ProgramaGeneralPuntoCorteConfiguracionRepositorio() : base()
        {
        }
        public ProgramaGeneralPuntoCorteConfiguracionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }

        public IEnumerable<ProgramaGeneralPuntoCorteConfiguracionBO> GetBy(Expression<Func<TProgramaGeneralPuntoCorteConfiguracion, bool>> filter)
        {
            IEnumerable<TProgramaGeneralPuntoCorteConfiguracion> listado = base.GetBy(filter);
            List<ProgramaGeneralPuntoCorteConfiguracionBO> listadoBO = new List<ProgramaGeneralPuntoCorteConfiguracionBO>();
            foreach (var itemEntidad in listado)
            {
                ProgramaGeneralPuntoCorteConfiguracionBO objetoBO = Mapper.Map<TProgramaGeneralPuntoCorteConfiguracion, ProgramaGeneralPuntoCorteConfiguracionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ProgramaGeneralPuntoCorteConfiguracionBO FirstById(int id)
        {
            try
            {
                TProgramaGeneralPuntoCorteConfiguracion entidad = base.FirstById(id);
                ProgramaGeneralPuntoCorteConfiguracionBO objetoBO = new ProgramaGeneralPuntoCorteConfiguracionBO();
                Mapper.Map<TProgramaGeneralPuntoCorteConfiguracion, ProgramaGeneralPuntoCorteConfiguracionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ProgramaGeneralPuntoCorteConfiguracionBO FirstBy(Expression<Func<TProgramaGeneralPuntoCorteConfiguracion, bool>> filter)
        {
            try
            {
                TProgramaGeneralPuntoCorteConfiguracion entidad = base.FirstBy(filter);
                ProgramaGeneralPuntoCorteConfiguracionBO objetoBO = Mapper.Map<TProgramaGeneralPuntoCorteConfiguracion, ProgramaGeneralPuntoCorteConfiguracionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ProgramaGeneralPuntoCorteConfiguracionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TProgramaGeneralPuntoCorteConfiguracion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ProgramaGeneralPuntoCorteConfiguracionBO> listadoBO)
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

        public bool Update(ProgramaGeneralPuntoCorteConfiguracionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TProgramaGeneralPuntoCorteConfiguracion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ProgramaGeneralPuntoCorteConfiguracionBO> listadoBO)
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
        private void AsignacionId(TProgramaGeneralPuntoCorteConfiguracion entidad, ProgramaGeneralPuntoCorteConfiguracionBO objetoBO)
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

        private TProgramaGeneralPuntoCorteConfiguracion MapeoEntidad(ProgramaGeneralPuntoCorteConfiguracionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TProgramaGeneralPuntoCorteConfiguracion entidad = new TProgramaGeneralPuntoCorteConfiguracion();
                entidad = Mapper.Map<ProgramaGeneralPuntoCorteConfiguracionBO, TProgramaGeneralPuntoCorteConfiguracion>(objetoBO,
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
