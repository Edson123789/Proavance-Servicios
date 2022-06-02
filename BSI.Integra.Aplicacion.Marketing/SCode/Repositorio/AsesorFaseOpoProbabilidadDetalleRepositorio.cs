using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Microsoft.EntityFrameworkCore.Internal;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
    public class AsesorFaseOpoProbabilidadDetalleRepositorio : BaseRepository<TAsesorFaseOpoProbabilidadDetalle, AsesorFaseOpoProbabilidadDetalleBO>
    {
        #region Metodos Base
        public AsesorFaseOpoProbabilidadDetalleRepositorio() : base()
        {
        }
        public AsesorFaseOpoProbabilidadDetalleRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<AsesorFaseOpoProbabilidadDetalleBO> GetBy(Expression<Func<TAsesorFaseOpoProbabilidadDetalle, bool>> filter)
        {
            IEnumerable<TAsesorFaseOpoProbabilidadDetalle> listado = base.GetBy(filter);
            List<AsesorFaseOpoProbabilidadDetalleBO> listadoBO = new List<AsesorFaseOpoProbabilidadDetalleBO>();
            foreach (var itemEntidad in listado)
            {
                AsesorFaseOpoProbabilidadDetalleBO objetoBO = Mapper.Map<TAsesorFaseOpoProbabilidadDetalle, AsesorFaseOpoProbabilidadDetalleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public AsesorFaseOpoProbabilidadDetalleBO FirstById(int id)
        {
            try
            {
                TAsesorFaseOpoProbabilidadDetalle entidad = base.FirstById(id);
                AsesorFaseOpoProbabilidadDetalleBO objetoBO = new AsesorFaseOpoProbabilidadDetalleBO();
                Mapper.Map<TAsesorFaseOpoProbabilidadDetalle, AsesorFaseOpoProbabilidadDetalleBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public AsesorFaseOpoProbabilidadDetalleBO FirstBy(Expression<Func<TAsesorFaseOpoProbabilidadDetalle, bool>> filter)
        {
            try
            {
                TAsesorFaseOpoProbabilidadDetalle entidad = base.FirstBy(filter);
                AsesorFaseOpoProbabilidadDetalleBO objetoBO = Mapper.Map<TAsesorFaseOpoProbabilidadDetalle, AsesorFaseOpoProbabilidadDetalleBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(AsesorFaseOpoProbabilidadDetalleBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TAsesorFaseOpoProbabilidadDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<AsesorFaseOpoProbabilidadDetalleBO> listadoBO)
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

        public bool Update(AsesorFaseOpoProbabilidadDetalleBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TAsesorFaseOpoProbabilidadDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<AsesorFaseOpoProbabilidadDetalleBO> listadoBO)
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
        private void AsignacionId(TAsesorFaseOpoProbabilidadDetalle entidad, AsesorFaseOpoProbabilidadDetalleBO objetoBO)
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

        private TAsesorFaseOpoProbabilidadDetalle MapeoEntidad(AsesorFaseOpoProbabilidadDetalleBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TAsesorFaseOpoProbabilidadDetalle entidad = new TAsesorFaseOpoProbabilidadDetalle();
                entidad = Mapper.Map<AsesorFaseOpoProbabilidadDetalleBO, TAsesorFaseOpoProbabilidadDetalle>(objetoBO,
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
        /// Obtiene la verificacion grid por idAsesorCentroCosto
        /// </summary>
        /// <param name="idAsesorCentroCosto"></param>
        /// <param name="tipo"></param>
        /// <returns></returns>
        public List<GridVerificacioDTO> ObtenerGridVerificacionId(int idAsesorCentroCosto, string tipo) {
            try
            {
                List<GridVerificacioDTO> listadoTotal = new List<GridVerificacioDTO>();
                var listadoAsesorFaseOpoProbabilidad =  this.GetBy(x => x.IdAsesorCentroCosto == idAsesorCentroCosto && x.Tipo.Equals(tipo), x => new GridVerificacioDTO { Id = x.Id, IdFaseOportunidad = x.IdFaseOportunidad, IdProbabilidadRegistroPw = x.IdProbabilidadRegistroPw }).ToList();
                var listadoAsesorFaseOpoProbabilidadTemp = listadoAsesorFaseOpoProbabilidad.Select(x => new { x.IdFaseOportunidad, x.IdProbabilidadRegistroPw }).ToList();


                var listadoAsesorAgrupado = from p in listadoAsesorFaseOpoProbabilidadTemp
                                            group p by  p.IdFaseOportunidad  into g
                                      //select new GridVerificacioDTO { IdFaseOportunidad = g.Key, Probabilidad = string.Join(",", g.ToList()) };
                                            select new TempClassDTO  { IdFaseOportunidad = g.Key, Probabilidad = g.ToList().Select(x => x.IdProbabilidadRegistroPw).ToList() };

                foreach (var item in listadoAsesorAgrupado)
                {
                    //item.Probabilidad = string.Join(",", item.Probabilidad);
                    listadoTotal.Add(new GridVerificacioDTO { Id = 0, IdFaseOportunidad = item.IdFaseOportunidad, IdProbabilidadRegistroPw = 0,Probabilidad = string.Join(",", item.Probabilidad) });
                }
                return listadoTotal;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public class TempClassDTO {
            public int IdFaseOportunidad { get; set; }
            public List<int> Probabilidad { get; set; }
        }
    }
}

