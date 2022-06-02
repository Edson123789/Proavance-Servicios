using System;
using System.Collections.Generic;
using System.Linq;
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
    public class MatriculaCabeceraControlCondicionesComisionRepositorio : BaseRepository<TMatriculaCabeceraControlCondicionesComision, MatriculaCabeceraControlCondicionesComisionBO>
    {
        #region Metodos Base
        public MatriculaCabeceraControlCondicionesComisionRepositorio() : base()
        {
        }
        public MatriculaCabeceraControlCondicionesComisionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<MatriculaCabeceraControlCondicionesComisionBO> GetBy(Expression<Func<TMatriculaCabeceraControlCondicionesComision, bool>> filter)
        {
            IEnumerable<TMatriculaCabeceraControlCondicionesComision> listado = base.GetBy(filter);
            List<MatriculaCabeceraControlCondicionesComisionBO> listadoBO = new List<MatriculaCabeceraControlCondicionesComisionBO>();
            foreach (var itemEntidad in listado)
            {
                MatriculaCabeceraControlCondicionesComisionBO objetoBO = Mapper.Map<TMatriculaCabeceraControlCondicionesComision, MatriculaCabeceraControlCondicionesComisionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public MatriculaCabeceraControlCondicionesComisionBO FirstById(int id)
        {
            try
            {
                TMatriculaCabeceraControlCondicionesComision entidad = base.FirstById(id);
                MatriculaCabeceraControlCondicionesComisionBO objetoBO = new MatriculaCabeceraControlCondicionesComisionBO();
                Mapper.Map<TMatriculaCabeceraControlCondicionesComision, MatriculaCabeceraControlCondicionesComisionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public MatriculaCabeceraControlCondicionesComisionBO FirstBy(Expression<Func<TMatriculaCabeceraControlCondicionesComision, bool>> filter)
        {
            try
            {
                TMatriculaCabeceraControlCondicionesComision entidad = base.FirstBy(filter);
                MatriculaCabeceraControlCondicionesComisionBO objetoBO = Mapper.Map<TMatriculaCabeceraControlCondicionesComision, MatriculaCabeceraControlCondicionesComisionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(MatriculaCabeceraControlCondicionesComisionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TMatriculaCabeceraControlCondicionesComision entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<MatriculaCabeceraControlCondicionesComisionBO> listadoBO)
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

        public bool Update(MatriculaCabeceraControlCondicionesComisionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TMatriculaCabeceraControlCondicionesComision entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<MatriculaCabeceraControlCondicionesComisionBO> listadoBO)
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
        private void AsignacionId(TMatriculaCabeceraControlCondicionesComision entidad, MatriculaCabeceraControlCondicionesComisionBO objetoBO)
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

        private TMatriculaCabeceraControlCondicionesComision MapeoEntidad(MatriculaCabeceraControlCondicionesComisionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TMatriculaCabeceraControlCondicionesComision entidad = new TMatriculaCabeceraControlCondicionesComision();
                entidad = Mapper.Map<MatriculaCabeceraControlCondicionesComisionBO, TMatriculaCabeceraControlCondicionesComision>(objetoBO,
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
