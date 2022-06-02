using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class FeriadoEspecialRepositorio : BaseRepository<TFeriadoEspecial,FeriadoEspecialBO>
    {
        #region Metodos Base
        public FeriadoEspecialRepositorio() : base()
        {
        }
        public FeriadoEspecialRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<FeriadoEspecialBO> GetBy(Expression<Func<TFeriadoEspecial, bool>> filter)
        {
            IEnumerable<TFeriadoEspecial> listado = base.GetBy(filter);
            List<FeriadoEspecialBO> listadoBO = new List<FeriadoEspecialBO>();
            foreach (var itemEntidad in listado)
            {
                FeriadoEspecialBO objetoBO = Mapper.Map<TFeriadoEspecial, FeriadoEspecialBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public FeriadoEspecialBO FirstById(int id)
        {
            try
            {
                TFeriadoEspecial entidad = base.FirstById(id);
                FeriadoEspecialBO objetoBO = new FeriadoEspecialBO();
                Mapper.Map<TFeriadoEspecial, FeriadoEspecialBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public FeriadoEspecialBO FirstBy(Expression<Func<TFeriadoEspecial, bool>> filter)
        {
            try
            {
                TFeriadoEspecial entidad = base.FirstBy(filter);
                FeriadoEspecialBO objetoBO = Mapper.Map<TFeriadoEspecial, FeriadoEspecialBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(FeriadoEspecialBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TFeriadoEspecial entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<FeriadoEspecialBO> listadoBO)
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

        public bool Update(FeriadoEspecialBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TFeriadoEspecial entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<FeriadoEspecialBO> listadoBO)
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
        private void AsignacionId(TFeriadoEspecial entidad, FeriadoEspecialBO objetoBO)
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

        private TFeriadoEspecial MapeoEntidad(FeriadoEspecialBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TFeriadoEspecial entidad = new TFeriadoEspecial();
                entidad = Mapper.Map<FeriadoEspecialBO, TFeriadoEspecial>(objetoBO,
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
