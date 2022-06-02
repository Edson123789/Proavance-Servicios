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
    public class PgeneralTipoDescuentoRepositorio : BaseRepository<TPgeneralTipoDescuento, PgeneralTipoDescuentoBO>
    {
        #region Metodos Base
        public PgeneralTipoDescuentoRepositorio() : base()
        {
        }
        public PgeneralTipoDescuentoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PgeneralTipoDescuentoBO> GetBy(Expression<Func<TPgeneralTipoDescuento, bool>> filter)
        {
            IEnumerable<TPgeneralTipoDescuento> listado = base.GetBy(filter);
            List<PgeneralTipoDescuentoBO> listadoBO = new List<PgeneralTipoDescuentoBO>();
            foreach (var itemEntidad in listado)
            {
                PgeneralTipoDescuentoBO objetoBO = Mapper.Map<TPgeneralTipoDescuento, PgeneralTipoDescuentoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PgeneralTipoDescuentoBO FirstById(int id)
        {
            try
            {
                TPgeneralTipoDescuento entidad = base.FirstById(id);
                PgeneralTipoDescuentoBO objetoBO = new PgeneralTipoDescuentoBO();
                Mapper.Map<TPgeneralTipoDescuento, PgeneralTipoDescuentoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PgeneralTipoDescuentoBO FirstBy(Expression<Func<TPgeneralTipoDescuento, bool>> filter)
        {
            try
            {
                TPgeneralTipoDescuento entidad = base.FirstBy(filter);
                PgeneralTipoDescuentoBO objetoBO = Mapper.Map<TPgeneralTipoDescuento, PgeneralTipoDescuentoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PgeneralTipoDescuentoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPgeneralTipoDescuento entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PgeneralTipoDescuentoBO> listadoBO)
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

        public bool Update(PgeneralTipoDescuentoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPgeneralTipoDescuento entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PgeneralTipoDescuentoBO> listadoBO)
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
        private void AsignacionId(TPgeneralTipoDescuento entidad, PgeneralTipoDescuentoBO objetoBO)
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

        private TPgeneralTipoDescuento MapeoEntidad(PgeneralTipoDescuentoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPgeneralTipoDescuento entidad = new TPgeneralTipoDescuento();
                entidad = Mapper.Map<PgeneralTipoDescuentoBO, TPgeneralTipoDescuento>(objetoBO,
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
        ///  Obtiene la lista de Tipo de descuentos (activos) por programa General
        ///  para un programa.
        /// </summary>
        /// <returns></returns>
        public List<int> ObtenerDescuentosPorPrograma(int idPgeneral)
        {
            try
            {
                var result = GetBy(x => x.IdPgeneral == idPgeneral).Select(x => x.IdTipoDescuento.Value).ToList();
                return result;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        /// <summary>
        ///  Obtiene la lista de Programas por IdTipoDescuento
        ///  para un programa.
        /// </summary>
        /// <returns></returns>
        public List<int> ObtenerProgramaporDescuento(int IdTipoDescuento)
        {
            try
            {
                var result = GetBy(x => x.IdTipoDescuento == IdTipoDescuento).Select(x => x.IdPgeneral).ToList();
                return result;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        /// <summary>
        /// Elimina (Actualiza estado a false ) todos los Descuentos asociados a un programa por IdTipoDescuento
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public void EliminacionLogicoPorPrograma(int idPGeneral, string usuario, List<int> nuevos)
        {
            try
            {
                var listaBorrar = GetBy(x => x.IdPgeneral == idPGeneral && x.Estado == true).ToList();
                listaBorrar.RemoveAll(x => nuevos.Any(y => y == x.IdTipoDescuento));
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
        /// <summary>
        /// Elimina (Actualiza estado a false ) todos los Descuentos asociados a un TipoDescuento por programa
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public void EliminacionLogicoTipoDescuento(int IdTipoDescuento, string usuario, List<int> nuevos)
        {
            try
            {
                var listaBorrar = GetBy(x => x.IdTipoDescuento == IdTipoDescuento && x.Estado == true).ToList();
                listaBorrar.RemoveAll(x => nuevos.Any(y => y == x.IdPgeneral));
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
