using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
    public class AsesorCentroCostoRepositorio : BaseRepository<TAsesorCentroCosto, AsesorCentroCostoBO>
    {
        #region Metodos Base
        public AsesorCentroCostoRepositorio() : base()
        {
        }
        public AsesorCentroCostoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<AsesorCentroCostoBO> GetBy(Expression<Func<TAsesorCentroCosto, bool>> filter)
        {
            IEnumerable<TAsesorCentroCosto> listado = base.GetBy(filter);
            List<AsesorCentroCostoBO> listadoBO = new List<AsesorCentroCostoBO>();
            foreach (var itemEntidad in listado)
            {
                AsesorCentroCostoBO objetoBO = Mapper.Map<TAsesorCentroCosto, AsesorCentroCostoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public AsesorCentroCostoBO FirstById(int id)
        {
            try
            {
                TAsesorCentroCosto entidad = base.FirstById(id);
                AsesorCentroCostoBO objetoBO = new AsesorCentroCostoBO();
                Mapper.Map<TAsesorCentroCosto, AsesorCentroCostoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public AsesorCentroCostoBO FirstBy(Expression<Func<TAsesorCentroCosto, bool>> filter)
        {
            try
            {
                TAsesorCentroCosto entidad = base.FirstBy(filter);
                AsesorCentroCostoBO objetoBO = Mapper.Map<TAsesorCentroCosto, AsesorCentroCostoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

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
                TAsesorCentroCosto entidad = MapeoEntidad(objetoBO);

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
                TAsesorCentroCosto entidad = MapeoEntidad(objetoBO);

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
        private void AsignacionId(TAsesorCentroCosto entidad, AsesorCentroCostoBO objetoBO)
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

        private TAsesorCentroCosto MapeoEntidad(AsesorCentroCostoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TAsesorCentroCosto entidad = new TAsesorCentroCosto();
                entidad = Mapper.Map<AsesorCentroCostoBO, TAsesorCentroCosto>(objetoBO,
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
