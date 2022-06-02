using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class PreCalculadaCambioFaseRepositorio : BaseRepository<TPreCalculadaCambioFase, PreCalculadaCambioFaseBO>
    {
        #region Metodos Base
        public PreCalculadaCambioFaseRepositorio() : base()
        {
        }
        public PreCalculadaCambioFaseRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PreCalculadaCambioFaseBO> GetBy(Expression<Func<TPreCalculadaCambioFase, bool>> filter)
        {
            IEnumerable<TPreCalculadaCambioFase> listado = base.GetBy(filter).ToList();
            List<PreCalculadaCambioFaseBO> listadoBO = new List<PreCalculadaCambioFaseBO>();
            foreach (var itemEntidad in listado)
            {
                PreCalculadaCambioFaseBO objetoBO = Mapper.Map<TPreCalculadaCambioFase, PreCalculadaCambioFaseBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PreCalculadaCambioFaseBO FirstById(int id)
        {
            try
            {
                TPreCalculadaCambioFase entidad = base.FirstById(id);
                PreCalculadaCambioFaseBO objetoBO = Mapper.Map<TPreCalculadaCambioFase, PreCalculadaCambioFaseBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PreCalculadaCambioFaseBO FirstBy(Expression<Func<TPreCalculadaCambioFase, bool>> filter)
        {
            try
            {
                TPreCalculadaCambioFase entidad = base.FirstBy(filter);
                if (entidad == null)
                    return null;
                PreCalculadaCambioFaseBO objetoBO = new PreCalculadaCambioFaseBO();
                objetoBO = Mapper.Map<TPreCalculadaCambioFase, PreCalculadaCambioFaseBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PreCalculadaCambioFaseBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPreCalculadaCambioFase entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PreCalculadaCambioFaseBO> listadoBO)
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

        public bool Update(PreCalculadaCambioFaseBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPreCalculadaCambioFase entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PreCalculadaCambioFaseBO> listadoBO)
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
        private void AsignacionId(TPreCalculadaCambioFase entidad, PreCalculadaCambioFaseBO objetoBO)
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

        private TPreCalculadaCambioFase MapeoEntidad(PreCalculadaCambioFaseBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPreCalculadaCambioFase entidad = new TPreCalculadaCambioFase();
                entidad = Mapper.Map<PreCalculadaCambioFaseBO, TPreCalculadaCambioFase>(objetoBO,
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

        public int ExistePreCalculadaCambioFase(PreCalculadaCambioFaseBO tPre)
        {
            try
            {
                //FirstBy(tPre)
                //TPreCalculadaCambioFase entidad 
                var _rPre = FirstBy(x => x.IdPersonal == tPre.IdPersonal &&
                                                   x.Fecha == tPre.Fecha &&
                                                   x.IdCentroCosto == tPre.IdCentroCosto &&
                                                   x.IdFaseOportunidadOrigen == tPre.IdFaseOportunidadOrigen &&
                                                   x.IdFaseOportunidadDestino == tPre.IdFaseOportunidadDestino &&
                                                   x.IdTipoDato == tPre.IdTipoDato &&
                                                   x.IdOrigen == tPre.IdOrigen &&
                                                   x.IdCategoriaOrigen == tPre.IdCategoriaOrigen &&
                                                   x.IdCampania == tPre.IdCampania);

                return _rPre == null ? 1 : _rPre.Contador + 1;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
