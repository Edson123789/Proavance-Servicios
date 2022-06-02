using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
    public class AsesorCentroCostoOcurrenciaRepositorio : BaseRepository<TAsesorCentroCostoOcurrencia, AsesorCentroCostoOcurrenciaBO>
    {
        #region Metodos Base
        public AsesorCentroCostoOcurrenciaRepositorio() : base()
        {
        }
        public AsesorCentroCostoOcurrenciaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<AsesorCentroCostoBO> GetBy(Expression<Func<TAsesorCentroCostoOcurrencia, bool>> filter)
        {
            IEnumerable<TAsesorCentroCostoOcurrencia> listado = base.GetBy(filter);
            List<AsesorCentroCostoBO> listadoBO = new List<AsesorCentroCostoBO>();
            foreach (var itemEntidad in listado)
            {
                AsesorCentroCostoBO objetoBO = Mapper.Map<TAsesorCentroCostoOcurrencia, AsesorCentroCostoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public AsesorCentroCostoBO FirstById(int id)
        {
            try
            {
                TAsesorCentroCostoOcurrencia entidad = base.FirstById(id);
                AsesorCentroCostoBO objetoBO = new AsesorCentroCostoBO();
                Mapper.Map<TAsesorCentroCostoOcurrencia, AsesorCentroCostoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public AsesorCentroCostoBO FirstBy(Expression<Func<TAsesorCentroCostoOcurrencia, bool>> filter)
        {
            try
            {
                TAsesorCentroCostoOcurrencia entidad = base.FirstBy(filter);
                AsesorCentroCostoBO objetoBO = Mapper.Map<TAsesorCentroCostoOcurrencia, AsesorCentroCostoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(AsesorCentroCostoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TAsesorCentroCostoOcurrencia entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<AsesorCentroCostoBO> listadoBO)
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

        public bool Update(AsesorCentroCostoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TAsesorCentroCostoOcurrencia entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<AsesorCentroCostoBO> listadoBO)
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
        private void AsignacionId(TAsesorCentroCostoOcurrencia entidad, AsesorCentroCostoBO objetoBO)
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

        private TAsesorCentroCostoOcurrencia MapeoEntidad(AsesorCentroCostoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TAsesorCentroCostoOcurrencia entidad = new TAsesorCentroCostoOcurrencia();
                entidad = Mapper.Map<AsesorCentroCostoBO, TAsesorCentroCostoOcurrencia>(objetoBO,
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

        /// <summary>
        /// Obtiene un listado de ocurrencias
        /// </summary>
        /// <returns></returns>
        public List<AsesorCentroCostoOcurrenciaFiltroDTO> ObtenerAsesorCentroCostoOcurrencia() {
            try
            {
                return this.GetBy(x => x.Estado == true, x => new AsesorCentroCostoOcurrenciaFiltroDTO  { Id = x.Id, Nombre= x.Nombre }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
