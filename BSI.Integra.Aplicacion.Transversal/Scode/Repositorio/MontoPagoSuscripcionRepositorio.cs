using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class MontoPagoSuscripcionRepositorio : BaseRepository<TMontoPagoSuscripcion, MontoPagoSuscripcionBO>
    {
        #region Metodos Base
        public MontoPagoSuscripcionRepositorio() : base()
        {
        }
        public MontoPagoSuscripcionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<MontoPagoSuscripcionBO> GetBy(Expression<Func<TMontoPagoSuscripcion, bool>> filter)
        {
            IEnumerable<TMontoPagoSuscripcion> listado = base.GetBy(filter);
            List<MontoPagoSuscripcionBO> listadoBO = new List<MontoPagoSuscripcionBO>();
            foreach (var itemEntidad in listado)
            {
                MontoPagoSuscripcionBO objetoBO = Mapper.Map<TMontoPagoSuscripcion, MontoPagoSuscripcionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public MontoPagoSuscripcionBO FirstById(int id)
        {
            try
            {
                TMontoPagoSuscripcion entidad = base.FirstById(id);
                MontoPagoSuscripcionBO objetoBO = new MontoPagoSuscripcionBO();
                Mapper.Map<TMontoPagoSuscripcion, MontoPagoSuscripcionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public MontoPagoSuscripcionBO FirstBy(Expression<Func<TMontoPagoSuscripcion, bool>> filter)
        {
            try
            {
                TMontoPagoSuscripcion entidad = base.FirstBy(filter);
                MontoPagoSuscripcionBO objetoBO = Mapper.Map<TMontoPagoSuscripcion, MontoPagoSuscripcionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(MontoPagoSuscripcionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TMontoPagoSuscripcion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<MontoPagoSuscripcionBO> listadoBO)
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

        public bool Update(MontoPagoSuscripcionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TMontoPagoSuscripcion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<MontoPagoSuscripcionBO> listadoBO)
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
        private void AsignacionId(TMontoPagoSuscripcion entidad, MontoPagoSuscripcionBO objetoBO)
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

        private TMontoPagoSuscripcion MapeoEntidad(MontoPagoSuscripcionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TMontoPagoSuscripcion entidad = new TMontoPagoSuscripcion();
                entidad = Mapper.Map<MontoPagoSuscripcionBO, TMontoPagoSuscripcion>(objetoBO,
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
        /// Obtiene las plataformas de Pago(activos) segun el Monto Pago 
        /// </summary>
        /// <param name="idMontoPago"></param>
        /// <returns></returns>
        public List<int> ObtenerMontoPagoPlataformaPorFiltro(int idMontoPago)
        {
            try
            {
                var lista = GetBy(x => x.Estado == true && x.IdMontoPago == idMontoPago).Select(y => y.IdSuscripcionProgramaGeneral).ToList();
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Elimina (Actualiza estado a false ) todos las suscripciones asociados a un modo de pago por IdSuscripcion
        /// </summary>
        /// <param name="idMontoPago"></param>
        /// <returns></returns>
        public void EliminacionLogicoPorMontoPago(int idMontoPago, string usuario, List<int> nuevos)
        {
            try
            {
                var listaBorrar = GetBy(x => x.IdMontoPago == idMontoPago && x.Estado == true).ToList();
                listaBorrar.RemoveAll(x => nuevos.Any(y => y == x.IdSuscripcionProgramaGeneral));
                foreach (var item in listaBorrar)
                {
                    Delete(item.Id, usuario);
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
