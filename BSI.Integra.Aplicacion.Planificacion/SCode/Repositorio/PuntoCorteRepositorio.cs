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
    public class PuntoCorteRepositorio : BaseRepository<TPuntoCorte, PuntoCorteBO>
    {
        #region Metodos Base
        public PuntoCorteRepositorio() : base()
        {
        }
        public PuntoCorteRepositorio(integraDBContext contexto) : base(contexto)
        {
        }

        public IEnumerable<PuntoCorteBO> GetBy(Expression<Func<TPuntoCorte, bool>> filter)
        {
            IEnumerable<TPuntoCorte> listado = base.GetBy(filter);
            List<PuntoCorteBO> listadoBO = new List<PuntoCorteBO>();
            foreach (var itemEntidad in listado)
            {
                PuntoCorteBO objetoBO = Mapper.Map<TPuntoCorte, PuntoCorteBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PuntoCorteBO FirstById(int id)
        {
            try
            {
                TPuntoCorte entidad = base.FirstById(id);
                PuntoCorteBO objetoBO = new PuntoCorteBO();
                Mapper.Map<TPuntoCorte, PuntoCorteBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PuntoCorteBO FirstBy(Expression<Func<TPuntoCorte, bool>> filter)
        {
            try
            {
                TPuntoCorte entidad = base.FirstBy(filter);
                PuntoCorteBO objetoBO = Mapper.Map<TPuntoCorte, PuntoCorteBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PuntoCorteBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPuntoCorte entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PuntoCorteBO> listadoBO)
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

        public bool Update(PuntoCorteBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPuntoCorte entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PuntoCorteBO> listadoBO)
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
        private void AsignacionId(TPuntoCorte entidad, PuntoCorteBO objetoBO)
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

        private TPuntoCorte MapeoEntidad(PuntoCorteBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPuntoCorte entidad = new TPuntoCorte();
                entidad = Mapper.Map<PuntoCorteBO, TPuntoCorte>(objetoBO,
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
