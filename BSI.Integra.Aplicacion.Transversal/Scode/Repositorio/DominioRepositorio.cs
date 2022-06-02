using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class DominioRepositorio : BaseRepository<TDominio, DominioBO>
    {
        #region Metodos Base
        public DominioRepositorio() : base()
        {
        }
        public DominioRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<DominioBO> GetBy(Expression<Func<TDominio, bool>> filter)
        {
            IEnumerable<TDominio> listado = base.GetBy(filter);
            List<DominioBO> listadoBO = new List<DominioBO>();
            foreach (var itemEntidad in listado)
            {
                DominioBO objetoBO = Mapper.Map<TDominio, DominioBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public DominioBO FirstById(int id)
        {
            try
            {
                TDominio entidad = base.FirstById(id);
                DominioBO objetoBO = new DominioBO();
                Mapper.Map<TDominio, DominioBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public DominioBO FirstBy(Expression<Func<TDominio, bool>> filter)
        {
            try
            {
                TDominio entidad = base.FirstBy(filter);
                DominioBO objetoBO = Mapper.Map<TDominio, DominioBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(DominioBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TDominio entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<DominioBO> listadoBO)
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

        public bool Update(DominioBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TDominio entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<DominioBO> listadoBO)
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
        private void AsignacionId(TDominio entidad, DominioBO objetoBO)
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

        private TDominio MapeoEntidad(DominioBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TDominio entidad = new TDominio();
                entidad = Mapper.Map<DominioBO, TDominio>(objetoBO,
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

        public List<listaDominioBO> ObtenerListaDominio()
        {
            try
            {
                List<listaDominioBO> capitulosFiltro = new List<listaDominioBO>();
                var _queryfiltrocapitulo = "Select Id, Nombre, IpPublico, IpPrivado FROM pla.T_Dominio WHERE Estado=1";
                var SubfiltroCapitulo = _dapper.QueryDapper(_queryfiltrocapitulo, new { });
                if (!string.IsNullOrEmpty(SubfiltroCapitulo) && !SubfiltroCapitulo.Contains("[]"))
                {
                    capitulosFiltro = JsonConvert.DeserializeObject<List<listaDominioBO>>(SubfiltroCapitulo);
                }
                return capitulosFiltro;
            }
            catch (Exception E)
            {
                throw new Exception(E.Message);
            }

        }

        public List<filtroDominioBO> ObtenerFiltroDominio()
        {
            try
            {
                List<filtroDominioBO> capitulosFiltro = new List<filtroDominioBO>();
                var _queryfiltrocapitulo = "Select Id, Nombre FROM pla.T_Dominio WHERE Estado=1";
                var SubfiltroCapitulo = _dapper.QueryDapper(_queryfiltrocapitulo, new { });
                if (!string.IsNullOrEmpty(SubfiltroCapitulo) && !SubfiltroCapitulo.Contains("[]"))
                {
                    capitulosFiltro = JsonConvert.DeserializeObject<List<filtroDominioBO>>(SubfiltroCapitulo);
                }
                return capitulosFiltro;
            }
            catch (Exception E)
            {
                throw new Exception(E.Message);
            }

        }
    }
}
