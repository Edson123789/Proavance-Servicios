using AutoMapper;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.Repositorio
{
    public class PreguntaIntentoRepositorio : BaseRepository<TPreguntaIntento,PreguntaIntentoBO>
    {
        #region Metodos Base
        public PreguntaIntentoRepositorio() : base()
        {
        }
        public PreguntaIntentoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PreguntaIntentoBO> GetBy(Expression<Func<TPreguntaIntento, bool>> filter)
        {
            IEnumerable<TPreguntaIntento> listado = base.GetBy(filter);
            List<PreguntaIntentoBO> listadoBO = new List<PreguntaIntentoBO>();
            foreach (var itemEntidad in listado)
            {
                PreguntaIntentoBO objetoBO = Mapper.Map<TPreguntaIntento, PreguntaIntentoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PreguntaIntentoBO FirstById(int id)
        {
            try
            {
                TPreguntaIntento entidad = base.FirstById(id);
                PreguntaIntentoBO objetoBO = new PreguntaIntentoBO();
                Mapper.Map<TPreguntaIntento, PreguntaIntentoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PreguntaIntentoBO FirstBy(Expression<Func<TPreguntaIntento, bool>> filter)
        {
            try
            {
                TPreguntaIntento entidad = base.FirstBy(filter);
                PreguntaIntentoBO objetoBO = Mapper.Map<TPreguntaIntento, PreguntaIntentoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PreguntaIntentoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPreguntaIntento entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PreguntaIntentoBO> listadoBO)
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

        public bool Update(PreguntaIntentoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPreguntaIntento entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PreguntaIntentoBO> listadoBO)
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
        private void AsignacionId(TPreguntaIntento entidad, PreguntaIntentoBO objetoBO)
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

        private TPreguntaIntento MapeoEntidad(PreguntaIntentoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPreguntaIntento entidad = new TPreguntaIntento();
                entidad = Mapper.Map<PreguntaIntentoBO, TPreguntaIntento>(objetoBO,
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
    }
}
