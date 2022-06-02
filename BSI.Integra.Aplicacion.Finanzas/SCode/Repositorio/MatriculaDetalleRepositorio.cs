using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Finanzas.Repositorio
{
    public class MatriculaDetalleRepositorio : BaseRepository<TMatriculaDetalle, MatriculaDetalleBO>
    {
        #region Metodos Base
        public MatriculaDetalleRepositorio() : base()
        {
        }
        public MatriculaDetalleRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<MatriculaDetalleBO> GetBy(Expression<Func<TMatriculaDetalle, bool>> filter)
        {
            IEnumerable<TMatriculaDetalle> listado = base.GetBy(filter);
            List<MatriculaDetalleBO> listadoBO = new List<MatriculaDetalleBO>();
            foreach (var itemEntidad in listado)
            {
                MatriculaDetalleBO objetoBO = Mapper.Map<TMatriculaDetalle, MatriculaDetalleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public MatriculaDetalleBO FirstById(int id)
        {
            try
            {
                TMatriculaDetalle entidad = base.FirstById(id);
                MatriculaDetalleBO objetoBO = new MatriculaDetalleBO();
                Mapper.Map<TMatriculaDetalle, MatriculaDetalleBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public MatriculaDetalleBO FirstBy(Expression<Func<TMatriculaDetalle, bool>> filter)
        {
            try
            {
                TMatriculaDetalle entidad = base.FirstBy(filter);
                MatriculaDetalleBO objetoBO = Mapper.Map<TMatriculaDetalle, MatriculaDetalleBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(MatriculaDetalleBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TMatriculaDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<MatriculaDetalleBO> listadoBO)
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

        public bool Update(MatriculaDetalleBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TMatriculaDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<MatriculaDetalleBO> listadoBO)
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
        private void AsignacionId(TMatriculaDetalle entidad, MatriculaDetalleBO objetoBO)
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

        private TMatriculaDetalle MapeoEntidad(MatriculaDetalleBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TMatriculaDetalle entidad = new TMatriculaDetalle();
                entidad = Mapper.Map<MatriculaDetalleBO, TMatriculaDetalle>(objetoBO,
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
