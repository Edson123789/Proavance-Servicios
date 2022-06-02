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

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class BandejaPendientePwRepositorio : BaseRepository<TBandejaPendientePw, BandejaPendientePwBO>
    {
        #region Metodos Base
        public BandejaPendientePwRepositorio() : base()
        {
        }
        public BandejaPendientePwRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<BandejaPendientePwBO> GetBy(Expression<Func<TBandejaPendientePw, bool>> filter)
        {
            IEnumerable<TBandejaPendientePw> listado = base.GetBy(filter);
            List<BandejaPendientePwBO> listadoBO = new List<BandejaPendientePwBO>();
            foreach (var itemEntidad in listado)
            {
                BandejaPendientePwBO objetoBO = Mapper.Map<TBandejaPendientePw, BandejaPendientePwBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public BandejaPendientePwBO FirstById(int id)
        {
            try
            {
                TBandejaPendientePw entidad = base.FirstById(id);
                BandejaPendientePwBO objetoBO = new BandejaPendientePwBO();
                Mapper.Map<TBandejaPendientePw, BandejaPendientePwBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public BandejaPendientePwBO FirstBy(Expression<Func<TBandejaPendientePw, bool>> filter)
        {
            try
            {
                TBandejaPendientePw entidad = base.FirstBy(filter);
                BandejaPendientePwBO objetoBO = Mapper.Map<TBandejaPendientePw, BandejaPendientePwBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(BandejaPendientePwBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TBandejaPendientePw entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<BandejaPendientePwBO> listadoBO)
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

        public bool Update(BandejaPendientePwBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TBandejaPendientePw entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<BandejaPendientePwBO> listadoBO)
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
        private void AsignacionId(TBandejaPendientePw entidad, BandejaPendientePwBO objetoBO)
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

        private TBandejaPendientePw MapeoEntidad(BandejaPendientePwBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TBandejaPendientePw entidad = new TBandejaPendientePw();
                entidad = Mapper.Map<BandejaPendientePwBO, TBandejaPendientePw>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<BandejaPendientePwBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TBandejaPendientePw, bool>>> filters, Expression<Func<TBandejaPendientePw, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TBandejaPendientePw> listado = base.GetFiltered(filters, orderBy, ascending);
            List<BandejaPendientePwBO> listadoBO = new List<BandejaPendientePwBO>();

            foreach (var itemEntidad in listado)
            {
                BandejaPendientePwBO objetoBO = Mapper.Map<TBandejaPendientePw, BandejaPendientePwBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion

        /// <summary>
        ///  Elimina (Actualiza estado a false ) todos las registros asociados a IdDocumento
        /// </summary>
        /// <param name="idDocumento"></param>
        /// <param name="usuario"></param>
        /// <param name="nuevos"></param>
        public void EliminacionLogicoPorIdDocumento(int idDocumento, string usuario, List<RevisionNivelPwFiltroIdPlantillaDTO> nuevos)
        {
            try
            {
                var listaBorrar = GetBy(x => x.IdDocumentoPw == idDocumento && x.Estado == true).ToList();
                listaBorrar.RemoveAll(x => nuevos.Any(y => y.Equals(x.IdRevisionNivelPw)));
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

