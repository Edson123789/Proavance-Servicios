using AutoMapper;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.Repositorio
{
    public class ExamenConfiguracionFormatoRepositorio : BaseRepository<TExamenConfiguracionFormato, ExamenConfiguracionFormatoBO>
    {
        #region Metodos Base
        public ExamenConfiguracionFormatoRepositorio() : base()
        {
        }
        public ExamenConfiguracionFormatoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ExamenConfiguracionFormatoBO> GetBy(Expression<Func<TExamenConfiguracionFormato, bool>> filter)
        {
            IEnumerable<TExamenConfiguracionFormato> listado = base.GetBy(filter);
            List<ExamenConfiguracionFormatoBO> listadoBO = new List<ExamenConfiguracionFormatoBO>();
            foreach (var itemEntidad in listado)
            {
                ExamenConfiguracionFormatoBO objetoBO = Mapper.Map<TExamenConfiguracionFormato, ExamenConfiguracionFormatoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ExamenConfiguracionFormatoBO FirstById(int id)
        {
            try
            {
                TExamenConfiguracionFormato entidad = base.FirstById(id);
                ExamenConfiguracionFormatoBO objetoBO = new ExamenConfiguracionFormatoBO();
                Mapper.Map<TExamenConfiguracionFormato, ExamenConfiguracionFormatoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ExamenConfiguracionFormatoBO FirstBy(Expression<Func<TExamenConfiguracionFormato, bool>> filter)
        {
            try
            {
                TExamenConfiguracionFormato entidad = base.FirstBy(filter);
                ExamenConfiguracionFormatoBO objetoBO = Mapper.Map<TExamenConfiguracionFormato, ExamenConfiguracionFormatoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ExamenConfiguracionFormatoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TExamenConfiguracionFormato entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ExamenConfiguracionFormatoBO> listadoBO)
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

        public bool Update(ExamenConfiguracionFormatoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TExamenConfiguracionFormato entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ExamenConfiguracionFormatoBO> listadoBO)
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
        private void AsignacionId(TExamenConfiguracionFormato entidad, ExamenConfiguracionFormatoBO objetoBO)
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

        private TExamenConfiguracionFormato MapeoEntidad(ExamenConfiguracionFormatoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TExamenConfiguracionFormato entidad = new TExamenConfiguracionFormato();
                entidad = Mapper.Map<ExamenConfiguracionFormatoBO, TExamenConfiguracionFormato>(objetoBO,
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
