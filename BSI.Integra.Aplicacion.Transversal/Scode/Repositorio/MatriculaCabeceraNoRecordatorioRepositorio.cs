using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class MatriculaCabeceraNoRecordatorioRepositorio : BaseRepository<TMatriculaCabeceraNoRecordatorio, MatriculaCabeceraNoRecordatorioBO>
    {
        #region Metodos Base
        public MatriculaCabeceraNoRecordatorioRepositorio() : base()
        {
        }
        public MatriculaCabeceraNoRecordatorioRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<MatriculaCabeceraNoRecordatorioBO> GetBy(Expression<Func<TMatriculaCabeceraNoRecordatorio, bool>> filter)
        {
            IEnumerable<TMatriculaCabeceraNoRecordatorio> listado = base.GetBy(filter);
            List<MatriculaCabeceraNoRecordatorioBO> listadoBO = new List<MatriculaCabeceraNoRecordatorioBO>();
            foreach (var itemEntidad in listado)
            {
                MatriculaCabeceraNoRecordatorioBO objetoBO = Mapper.Map<TMatriculaCabeceraNoRecordatorio, MatriculaCabeceraNoRecordatorioBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public MatriculaCabeceraNoRecordatorioBO FirstById(int id)
        {
            try
            {
                TMatriculaCabeceraNoRecordatorio entidad = base.FirstById(id);
                MatriculaCabeceraNoRecordatorioBO objetoBO = new MatriculaCabeceraNoRecordatorioBO();
                Mapper.Map<TMatriculaCabeceraNoRecordatorio, MatriculaCabeceraNoRecordatorioBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public MatriculaCabeceraNoRecordatorioBO FirstBy(Expression<Func<TMatriculaCabeceraNoRecordatorio, bool>> filter)
        {
            try
            {
                TMatriculaCabeceraNoRecordatorio entidad = base.FirstBy(filter);
                MatriculaCabeceraNoRecordatorioBO objetoBO = Mapper.Map<TMatriculaCabeceraNoRecordatorio, MatriculaCabeceraNoRecordatorioBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(MatriculaCabeceraNoRecordatorioBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TMatriculaCabeceraNoRecordatorio entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<MatriculaCabeceraNoRecordatorioBO> listadoBO)
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

        public bool Update(MatriculaCabeceraNoRecordatorioBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TMatriculaCabeceraNoRecordatorio entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<MatriculaCabeceraNoRecordatorioBO> listadoBO)
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
        private void AsignacionId(TMatriculaCabeceraNoRecordatorio entidad, MatriculaCabeceraNoRecordatorioBO objetoBO)
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

        private TMatriculaCabeceraNoRecordatorio MapeoEntidad(MatriculaCabeceraNoRecordatorioBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TMatriculaCabeceraNoRecordatorio entidad = new TMatriculaCabeceraNoRecordatorio();
                entidad = Mapper.Map<MatriculaCabeceraNoRecordatorioBO, TMatriculaCabeceraNoRecordatorio>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos
                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<MatriculaCabeceraNoRecordatorioBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TMatriculaCabeceraNoRecordatorio, bool>>> filters, Expression<Func<TMatriculaCabeceraNoRecordatorio, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TMatriculaCabeceraNoRecordatorio> listado = base.GetFiltered(filters, orderBy, ascending);
            List<MatriculaCabeceraNoRecordatorioBO> listadoBO = new List<MatriculaCabeceraNoRecordatorioBO>();

            foreach (var itemEntidad in listado)
            {
                MatriculaCabeceraNoRecordatorioBO objetoBO = Mapper.Map<TMatriculaCabeceraNoRecordatorio, MatriculaCabeceraNoRecordatorioBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion
    }
}
