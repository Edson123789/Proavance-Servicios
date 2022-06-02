using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Planificacion.Repositorio
{
    public class TestimonioProgramaRepositorio : BaseRepository<TTestimonioPrograma, TestimonioProgramaBO>
    {
        #region Metodos Base
        public TestimonioProgramaRepositorio() : base()
        {
        }
        public TestimonioProgramaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<TestimonioProgramaBO> GetBy(Expression<Func<TTestimonioPrograma, bool>> filter)
        {
            IEnumerable<TTestimonioPrograma> listado = base.GetBy(filter);
            List<TestimonioProgramaBO> listadoBO = new List<TestimonioProgramaBO>();
            foreach (var itemEntidad in listado)
            {
                TestimonioProgramaBO objetoBO = Mapper.Map<TTestimonioPrograma, TestimonioProgramaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public TestimonioProgramaBO FirstById(int id)
        {
            try
            {
                TTestimonioPrograma entidad = base.FirstById(id);
                TestimonioProgramaBO objetoBO = new TestimonioProgramaBO();
                Mapper.Map<TTestimonioPrograma, TestimonioProgramaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public TestimonioProgramaBO FirstBy(Expression<Func<TTestimonioPrograma, bool>> filter)
        {
            try
            {
                TTestimonioPrograma entidad = base.FirstBy(filter);
                TestimonioProgramaBO objetoBO = Mapper.Map<TTestimonioPrograma, TestimonioProgramaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(TestimonioProgramaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TTestimonioPrograma entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<TestimonioProgramaBO> listadoBO)
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

        public bool Update(TestimonioProgramaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TTestimonioPrograma entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<TestimonioProgramaBO> listadoBO)
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
        private void AsignacionId(TTestimonioPrograma entidad, TestimonioProgramaBO objetoBO)
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

        private TTestimonioPrograma MapeoEntidad(TestimonioProgramaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TTestimonioPrograma entidad = new TTestimonioPrograma();
                entidad = Mapper.Map<TestimonioProgramaBO, TTestimonioPrograma>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<TestimonioProgramaBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TTestimonioPrograma, bool>>> filters, Expression<Func<TTestimonioPrograma, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TTestimonioPrograma> listado = base.GetFiltered(filters, orderBy, ascending);
            List<TestimonioProgramaBO> listadoBO = new List<TestimonioProgramaBO>();

            foreach (var itemEntidad in listado)
            {
                TestimonioProgramaBO objetoBO = Mapper.Map<TTestimonioPrograma, TestimonioProgramaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion

        /// <summary>
        ///  Obtiene los testimoniosPrograma filtrados por el IdProgramaGeneral
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public List<TestimonioProgramaDTO> ObtenerTestimonioPorPGeneral(int idPGeneral)
        {
            try
            {
                return GetBy(x => x.IdPgeneral == idPGeneral && x.Estado == true, x => new TestimonioProgramaDTO
                {
                    Id = x.Id,
                    IdPgeneral = x.IdPgeneral,
                    CursoMoodleId = x.CursoMoodleId,
                    UsuarioMoodleId = x.UsuarioMoodleId,
                    Testimonio = x.Testimonio,
                    Pregunta = x.Pregunta,
                    Alumno = x.Alumno,
                    Autoriza = x.Autoriza
                }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
