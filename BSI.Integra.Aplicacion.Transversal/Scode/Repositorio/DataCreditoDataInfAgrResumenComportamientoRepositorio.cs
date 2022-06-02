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
    public class DataCreditoDataInfAgrResumenComportamientoRepositorio : BaseRepository<
        TDataCreditoDataInfAgrResumenComportamiento, DataCreditoDataInfAgrResumenComportamientoBO>
    {
        #region Metodos Base

        public DataCreditoDataInfAgrResumenComportamientoRepositorio() : base()
        {
        }

        public DataCreditoDataInfAgrResumenComportamientoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }

        public IEnumerable<DataCreditoDataInfAgrResumenComportamientoBO> GetBy(
            Expression<Func<TDataCreditoDataInfAgrResumenComportamiento, bool>> filter)
        {
            IEnumerable<TDataCreditoDataInfAgrResumenComportamiento> listado = base.GetBy(filter);
            List<DataCreditoDataInfAgrResumenComportamientoBO> listadoBO =
                new List<DataCreditoDataInfAgrResumenComportamientoBO>();
            foreach (var itemEntidad in listado)
            {
                DataCreditoDataInfAgrResumenComportamientoBO objetoBO =
                    Mapper
                        .Map<TDataCreditoDataInfAgrResumenComportamiento, DataCreditoDataInfAgrResumenComportamientoBO>(
                            itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }

        public DataCreditoDataInfAgrResumenComportamientoBO FirstById(int id)
        {
            try
            {
                TDataCreditoDataInfAgrResumenComportamiento entidad = base.FirstById(id);
                DataCreditoDataInfAgrResumenComportamientoBO objetoBO =
                    new DataCreditoDataInfAgrResumenComportamientoBO();
                Mapper.Map<TDataCreditoDataInfAgrResumenComportamiento, DataCreditoDataInfAgrResumenComportamientoBO>(
                    entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public DataCreditoDataInfAgrResumenComportamientoBO FirstBy(
            Expression<Func<TDataCreditoDataInfAgrResumenComportamiento, bool>> filter)
        {
            try
            {
                TDataCreditoDataInfAgrResumenComportamiento entidad = base.FirstBy(filter);
                DataCreditoDataInfAgrResumenComportamientoBO objetoBO =
                    Mapper
                        .Map<TDataCreditoDataInfAgrResumenComportamiento, DataCreditoDataInfAgrResumenComportamientoBO>(
                            entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(DataCreditoDataInfAgrResumenComportamientoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TDataCreditoDataInfAgrResumenComportamiento entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<DataCreditoDataInfAgrResumenComportamientoBO> listadoBO)
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

        public bool Update(DataCreditoDataInfAgrResumenComportamientoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TDataCreditoDataInfAgrResumenComportamiento entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<DataCreditoDataInfAgrResumenComportamientoBO> listadoBO)
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

        private void AsignacionId(TDataCreditoDataInfAgrResumenComportamiento entidad,
            DataCreditoDataInfAgrResumenComportamientoBO objetoBO)
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

        private TDataCreditoDataInfAgrResumenComportamiento MapeoEntidad(
            DataCreditoDataInfAgrResumenComportamientoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TDataCreditoDataInfAgrResumenComportamiento entidad = new TDataCreditoDataInfAgrResumenComportamiento();
                entidad = Mapper
                    .Map<DataCreditoDataInfAgrResumenComportamientoBO, TDataCreditoDataInfAgrResumenComportamiento>(
                        objetoBO,
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
