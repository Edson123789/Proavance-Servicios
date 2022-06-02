using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Operaciones.Repositorio
{
    public class RaRemitenteRepositorio : BaseRepository<TRaRemitente, RaRemitenteBO>
    {
        #region Metodos Base
        public RaRemitenteRepositorio() : base()
        {
        }
        public RaRemitenteRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<RaRemitenteBO> GetBy(Expression<Func<TRaRemitente, bool>> filter)
        {
            IEnumerable<TRaRemitente> listado = base.GetBy(filter);
            List<RaRemitenteBO> listadoBO = new List<RaRemitenteBO>();
            foreach (var itemEntidad in listado)
            {
                RaRemitenteBO objetoBO = Mapper.Map<TRaRemitente, RaRemitenteBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public RaRemitenteBO FirstById(int id)
        {
            try
            {
                TRaRemitente entidad = base.FirstById(id);
                RaRemitenteBO objetoBO = new RaRemitenteBO();
                Mapper.Map<TRaRemitente, RaRemitenteBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public RaRemitenteBO FirstBy(Expression<Func<TRaRemitente, bool>> filter)
        {
            try
            {
                TRaRemitente entidad = base.FirstBy(filter);
                RaRemitenteBO objetoBO = Mapper.Map<TRaRemitente, RaRemitenteBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(RaRemitenteBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TRaRemitente entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<RaRemitenteBO> listadoBO)
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

        public bool Update(RaRemitenteBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TRaRemitente entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<RaRemitenteBO> listadoBO)
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
        private void AsignacionId(TRaRemitente entidad, RaRemitenteBO objetoBO)
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

        private TRaRemitente MapeoEntidad(RaRemitenteBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TRaRemitente entidad = new TRaRemitente();
                entidad = Mapper.Map<RaRemitenteBO, TRaRemitente>(objetoBO,
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
        /// Obtiene el primer remitente por usuario
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public RaRemitenteBO ObtenerPorUsuario(string usuario) {
            try
            {
                return this.FirstBy(x => x.Usuario == usuario);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
