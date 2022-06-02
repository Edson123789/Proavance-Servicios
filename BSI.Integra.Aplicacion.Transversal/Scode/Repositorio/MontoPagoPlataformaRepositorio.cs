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
    public class MontoPagoPlataformaRepositorio : BaseRepository<TMontoPagoPlataforma, MontoPagoPlataformaBO>
    {
        #region Metodos Base
        public MontoPagoPlataformaRepositorio() : base()
        {
        }
        public MontoPagoPlataformaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<MontoPagoPlataformaBO> GetBy(Expression<Func<TMontoPagoPlataforma, bool>> filter)
        {
            IEnumerable<TMontoPagoPlataforma> listado = base.GetBy(filter);
            List<MontoPagoPlataformaBO> listadoBO = new List<MontoPagoPlataformaBO>();
            foreach (var itemEntidad in listado)
            {
                MontoPagoPlataformaBO objetoBO = Mapper.Map<TMontoPagoPlataforma, MontoPagoPlataformaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public MontoPagoPlataformaBO FirstById(int id)
        {
            try
            {
                TMontoPagoPlataforma entidad = base.FirstById(id);
                MontoPagoPlataformaBO objetoBO = new MontoPagoPlataformaBO();
                Mapper.Map<TMontoPagoPlataforma, MontoPagoPlataformaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public MontoPagoPlataformaBO FirstBy(Expression<Func<TMontoPagoPlataforma, bool>> filter)
        {
            try
            {
                TMontoPagoPlataforma entidad = base.FirstBy(filter);
                MontoPagoPlataformaBO objetoBO = Mapper.Map<TMontoPagoPlataforma, MontoPagoPlataformaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(MontoPagoPlataformaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TMontoPagoPlataforma entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<MontoPagoPlataformaBO> listadoBO)
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

        public bool Update(MontoPagoPlataformaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TMontoPagoPlataforma entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<MontoPagoPlataformaBO> listadoBO)
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
        private void AsignacionId(TMontoPagoPlataforma entidad, MontoPagoPlataformaBO objetoBO)
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

        private TMontoPagoPlataforma MapeoEntidad(MontoPagoPlataformaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TMontoPagoPlataforma entidad = new TMontoPagoPlataforma();
                entidad = Mapper.Map<MontoPagoPlataformaBO, TMontoPagoPlataforma>(objetoBO,
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
        /// Obtiene  las plataforma de pagos(activo) por un IdMontoPago registradas en el sistema
        /// </summary>
        /// <returns></returns>
        public List<int> ObtenerMontoPagoPlataformaPorFiltro(int idMontoPago)
        {
            try
            {
                var lista = GetBy(x => x.Estado == true && x.IdMontoPago == idMontoPago).Select(y => y.IdPlataformaPago).ToList();
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Elimina (Actualiza estado a false ) todos las Plataformas asociados a un modo de pago por IdPlataformaPago
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public void EliminacionLogicoPorMontoPago(int idMontoPago, string usuario, List<int> nuevos)
        {
            try
            {
                var listaBorrar = GetBy(x => x.IdMontoPago == idMontoPago && x.Estado == true).ToList();
                listaBorrar.RemoveAll(x => nuevos.Any(y => y == x.IdPlataformaPago));
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
