using AutoMapper;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
    /// Repositorio: Marketing/AsesorTipoCategoriaOrigenDetalle
    /// Autor: Gian Miranda
    /// Fecha: 21/01/2021
    /// <summary>
    /// Repositorio para la interaccion con la DB mkt.T_AsesorTipoCategoriaOrigenDetalle
    /// </summary>
    public class AsesorTipoCategoriaOrigenDetalleRepositorio : BaseRepository<TAsesorTipoCategoriaOrigenDetalle, AsesorTipoCategoriaOrigenDetalleBO>
    {
        #region Metodos Base
        public AsesorTipoCategoriaOrigenDetalleRepositorio() : base()
        {
        }
        public AsesorTipoCategoriaOrigenDetalleRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<AsesorTipoCategoriaOrigenDetalleBO> GetBy(Expression<Func<TAsesorTipoCategoriaOrigenDetalle, bool>> filter)
        {
            IEnumerable<TAsesorTipoCategoriaOrigenDetalle> listado = base.GetBy(filter);
            List<AsesorTipoCategoriaOrigenDetalleBO> listadoBO = new List<AsesorTipoCategoriaOrigenDetalleBO>();
            foreach (var itemEntidad in listado)
            {
                AsesorTipoCategoriaOrigenDetalleBO objetoBO = Mapper.Map<TAsesorTipoCategoriaOrigenDetalle, AsesorTipoCategoriaOrigenDetalleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public AsesorTipoCategoriaOrigenDetalleBO FirstById(int id)
        {
            try
            {
                TAsesorTipoCategoriaOrigenDetalle entidad = base.FirstById(id);
                AsesorTipoCategoriaOrigenDetalleBO objetoBO = new AsesorTipoCategoriaOrigenDetalleBO();
                Mapper.Map<TAsesorTipoCategoriaOrigenDetalle, AsesorTipoCategoriaOrigenDetalleBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public AsesorTipoCategoriaOrigenDetalleBO FirstBy(Expression<Func<TAsesorTipoCategoriaOrigenDetalle, bool>> filter)
        {
            try
            {
                TAsesorTipoCategoriaOrigenDetalle entidad = base.FirstBy(filter);
                AsesorTipoCategoriaOrigenDetalleBO objetoBO = Mapper.Map<TAsesorTipoCategoriaOrigenDetalle, AsesorTipoCategoriaOrigenDetalleBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(AsesorTipoCategoriaOrigenDetalleBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TAsesorTipoCategoriaOrigenDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<AsesorTipoCategoriaOrigenDetalleBO> listadoBO)
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

        public bool Update(AsesorTipoCategoriaOrigenDetalleBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TAsesorTipoCategoriaOrigenDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<AsesorTipoCategoriaOrigenDetalleBO> listadoBO)
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
        private void AsignacionId(TAsesorTipoCategoriaOrigenDetalle entidad, AsesorTipoCategoriaOrigenDetalleBO objetoBO)
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

        private TAsesorTipoCategoriaOrigenDetalle MapeoEntidad(AsesorTipoCategoriaOrigenDetalleBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TAsesorTipoCategoriaOrigenDetalle entidad = new TAsesorTipoCategoriaOrigenDetalle();
                entidad = Mapper.Map<AsesorTipoCategoriaOrigenDetalleBO, TAsesorTipoCategoriaOrigenDetalle>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<AsesorTipoCategoriaOrigenDetalleBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TAsesorTipoCategoriaOrigenDetalle, bool>>> filters, Expression<Func<TAsesorTipoCategoriaOrigenDetalle, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TAsesorTipoCategoriaOrigenDetalle> listado = base.GetFiltered(filters, orderBy, ascending);
            List<AsesorTipoCategoriaOrigenDetalleBO> listadoBO = new List<AsesorTipoCategoriaOrigenDetalleBO>();

            foreach (var itemEntidad in listado)
            {
                AsesorTipoCategoriaOrigenDetalleBO objetoBO = Mapper.Map<TAsesorTipoCategoriaOrigenDetalle, AsesorTipoCategoriaOrigenDetalleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion
    }
}
