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

    /// Repositorio: Planioficacion/EsquemaEvaluacionPgeneralProveedor
    /// Autor:--
    /// Fecha: 01/10/2021
    /// <summary>
    /// Repositorio de los proveedores del esquema de evaluacion general
    /// </summary>
    public class EsquemaEvaluacionPgeneralProveedorRepositorio : BaseRepository<TEsquemaEvaluacionPgeneralProveedor, EsquemaEvaluacionPgeneralProveedorBO>
    {
        #region Metodos Base
        public EsquemaEvaluacionPgeneralProveedorRepositorio() : base()
        {
        }
        public EsquemaEvaluacionPgeneralProveedorRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<EsquemaEvaluacionPgeneralProveedorBO> GetBy(Expression<Func<TEsquemaEvaluacionPgeneralProveedor, bool>> filter)
        {
            IEnumerable<TEsquemaEvaluacionPgeneralProveedor> listado = base.GetBy(filter);
            List<EsquemaEvaluacionPgeneralProveedorBO> listadoBO = new List<EsquemaEvaluacionPgeneralProveedorBO>();
            foreach (var itemEntidad in listado)
            {
                EsquemaEvaluacionPgeneralProveedorBO objetoBO = Mapper.Map<TEsquemaEvaluacionPgeneralProveedor, EsquemaEvaluacionPgeneralProveedorBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public EsquemaEvaluacionPgeneralProveedorBO FirstById(int id)
        {
            try
            {
                TEsquemaEvaluacionPgeneralProveedor entidad = base.FirstById(id);
                EsquemaEvaluacionPgeneralProveedorBO objetoBO = new EsquemaEvaluacionPgeneralProveedorBO();
                Mapper.Map<TEsquemaEvaluacionPgeneralProveedor, EsquemaEvaluacionPgeneralProveedorBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public EsquemaEvaluacionPgeneralProveedorBO FirstBy(Expression<Func<TEsquemaEvaluacionPgeneralProveedor, bool>> filter)
        {
            try
            {
                TEsquemaEvaluacionPgeneralProveedor entidad = base.FirstBy(filter);
                EsquemaEvaluacionPgeneralProveedorBO objetoBO = Mapper.Map<TEsquemaEvaluacionPgeneralProveedor, EsquemaEvaluacionPgeneralProveedorBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(EsquemaEvaluacionPgeneralProveedorBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TEsquemaEvaluacionPgeneralProveedor entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<EsquemaEvaluacionPgeneralProveedorBO> listadoBO)
        {
            try
            {
                foreach (var objetoBO in listadoBO)
                {
                    objetoBO.Id = 0;
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

        public bool Update(EsquemaEvaluacionPgeneralProveedorBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TEsquemaEvaluacionPgeneralProveedor entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<EsquemaEvaluacionPgeneralProveedorBO> listadoBO)
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
        private void AsignacionId(TEsquemaEvaluacionPgeneralProveedor entidad, EsquemaEvaluacionPgeneralProveedorBO objetoBO)
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

        private TEsquemaEvaluacionPgeneralProveedor MapeoEntidad(EsquemaEvaluacionPgeneralProveedorBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TEsquemaEvaluacionPgeneralProveedor entidad = new TEsquemaEvaluacionPgeneralProveedor();
                entidad = Mapper.Map<EsquemaEvaluacionPgeneralProveedorBO, TEsquemaEvaluacionPgeneralProveedor>(objetoBO,
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
