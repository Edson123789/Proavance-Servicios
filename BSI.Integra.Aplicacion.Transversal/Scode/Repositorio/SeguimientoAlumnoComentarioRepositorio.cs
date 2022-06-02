using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
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
    public class SeguimientoAlumnoComentarioRepositorio : BaseRepository<TSeguimientoAlumnoComentario, SeguimientoAlumnoComentarioBO>
    {
        #region Metodos Base
        public SeguimientoAlumnoComentarioRepositorio() : base()
        {
        }
        public SeguimientoAlumnoComentarioRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<SeguimientoAlumnoComentarioBO> GetBy(Expression<Func<TSeguimientoAlumnoComentario, bool>> filter)
        {
            IEnumerable<TSeguimientoAlumnoComentario> listado = base.GetBy(filter);
            List<SeguimientoAlumnoComentarioBO> listadoBO = new List<SeguimientoAlumnoComentarioBO>();
            foreach (var itemEntidad in listado)
            {
                SeguimientoAlumnoComentarioBO objetoBO = Mapper.Map<TSeguimientoAlumnoComentario, SeguimientoAlumnoComentarioBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public SeguimientoAlumnoComentarioBO FirstById(int id)
        {
            try
            {
                TSeguimientoAlumnoComentario entidad = base.FirstById(id);
                SeguimientoAlumnoComentarioBO objetoBO = new SeguimientoAlumnoComentarioBO();
                Mapper.Map<TSeguimientoAlumnoComentario, SeguimientoAlumnoComentarioBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public SeguimientoAlumnoComentarioBO FirstBy(Expression<Func<TSeguimientoAlumnoComentario, bool>> filter)
        {
            try
            {
                TSeguimientoAlumnoComentario entidad = base.FirstBy(filter);
                SeguimientoAlumnoComentarioBO objetoBO = Mapper.Map<TSeguimientoAlumnoComentario, SeguimientoAlumnoComentarioBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(SeguimientoAlumnoComentarioBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TSeguimientoAlumnoComentario entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<SeguimientoAlumnoComentarioBO> listadoBO)
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

        public bool Update(SeguimientoAlumnoComentarioBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TSeguimientoAlumnoComentario entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<SeguimientoAlumnoComentarioBO> listadoBO)
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
        private void AsignacionId(TSeguimientoAlumnoComentario entidad, SeguimientoAlumnoComentarioBO objetoBO)
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

        private TSeguimientoAlumnoComentario MapeoEntidad(SeguimientoAlumnoComentarioBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TSeguimientoAlumnoComentario entidad = new TSeguimientoAlumnoComentario();
                entidad = Mapper.Map<SeguimientoAlumnoComentarioBO, TSeguimientoAlumnoComentario>(objetoBO,
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

        public FechaReprogramacionDTO ObtenerFechaReprogramacion(int idOportunidad)
        {
            try
            {
                List<FechaReprogramacionDTO> fechas = new List<FechaReprogramacionDTO>();
                DateTime fechamanhana = DateTime.Now;
                fechas = GetBy(x => x.IdOportunidad == idOportunidad && x.FechaCompromiso > fechamanhana, y => new FechaReprogramacionDTO{
                    FechaCompromiso = y.FechaCompromiso,
                    Comentario = y.Comentario
                }).ToList().OrderBy(x => x.FechaCompromiso).ToList();
                if(fechas.Count() > 0)
                {
                    return fechas.First();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
