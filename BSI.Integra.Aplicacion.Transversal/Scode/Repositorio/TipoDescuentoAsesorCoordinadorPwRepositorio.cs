using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class TipoDescuentoAsesorCoordinadorPwRepositorio : BaseRepository<TTipoDescuentoAsesorCoordinadorPw, TipoDescuentoAsesorCoordinadorPwBO>
    {
        #region Metodos Base
        public TipoDescuentoAsesorCoordinadorPwRepositorio() : base()
        {
        }
        public TipoDescuentoAsesorCoordinadorPwRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<TipoDescuentoAsesorCoordinadorPwBO> GetBy(Expression<Func<TTipoDescuentoAsesorCoordinadorPw, bool>> filter)
        {
            IEnumerable<TTipoDescuentoAsesorCoordinadorPw> listado = base.GetBy(filter);
            List<TipoDescuentoAsesorCoordinadorPwBO> listadoBO = new List<TipoDescuentoAsesorCoordinadorPwBO>();
            foreach (var itemEntidad in listado)
            {
                TipoDescuentoAsesorCoordinadorPwBO objetoBO = Mapper.Map<TTipoDescuentoAsesorCoordinadorPw, TipoDescuentoAsesorCoordinadorPwBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public TipoDescuentoAsesorCoordinadorPwBO FirstById(int id)
        {
            try
            {
                TTipoDescuentoAsesorCoordinadorPw entidad = base.FirstById(id);
                TipoDescuentoAsesorCoordinadorPwBO objetoBO = new TipoDescuentoAsesorCoordinadorPwBO();
                Mapper.Map<TTipoDescuentoAsesorCoordinadorPw, TipoDescuentoAsesorCoordinadorPwBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public TipoDescuentoAsesorCoordinadorPwBO FirstBy(Expression<Func<TTipoDescuentoAsesorCoordinadorPw, bool>> filter)
        {
            try
            {
                TTipoDescuentoAsesorCoordinadorPw entidad = base.FirstBy(filter);
                TipoDescuentoAsesorCoordinadorPwBO objetoBO = Mapper.Map<TTipoDescuentoAsesorCoordinadorPw, TipoDescuentoAsesorCoordinadorPwBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(TipoDescuentoAsesorCoordinadorPwBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TTipoDescuentoAsesorCoordinadorPw entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<TipoDescuentoAsesorCoordinadorPwBO> listadoBO)
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

        public bool Update(TipoDescuentoAsesorCoordinadorPwBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TTipoDescuentoAsesorCoordinadorPw entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<TipoDescuentoAsesorCoordinadorPwBO> listadoBO)
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
        private void AsignacionId(TTipoDescuentoAsesorCoordinadorPw entidad, TipoDescuentoAsesorCoordinadorPwBO objetoBO)
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

        private TTipoDescuentoAsesorCoordinadorPw MapeoEntidad(TipoDescuentoAsesorCoordinadorPwBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TTipoDescuentoAsesorCoordinadorPw entidad = new TTipoDescuentoAsesorCoordinadorPw();
                entidad = Mapper.Map<TipoDescuentoAsesorCoordinadorPwBO, TTipoDescuentoAsesorCoordinadorPw>(objetoBO,
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
        /// Obtiene todos los registros sin los campos de auditoría.
        /// </summary>
        /// <returns></returns>
        public List<string> ObtenerAsesorCoordinadorPorTipoDescuentoId(TipoDescuentoDTO objeto)
        {
            try
            {
                var lista = GetBy(x => x.IdTipoDescuento == objeto.Id).Select(x => x.IdAgendaTipoUsuario.ToString()).ToList();

                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Elimina (Actualiza estado a false ) todos los TipoDescuentoAsesorCoordinadorPw asociados a una TipoDescuento
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public void EliminacionLogicoPorTipoDescuento(int idTipoDescuento, string usuario, List<int> nuevos)
        {
            try
            {
                var listaBorrar = GetBy(x => x.IdTipoDescuento == idTipoDescuento && x.Estado == true).ToList();
                listaBorrar.RemoveAll(x => nuevos.Any(y => y == x.IdAgendaTipoUsuario));
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
