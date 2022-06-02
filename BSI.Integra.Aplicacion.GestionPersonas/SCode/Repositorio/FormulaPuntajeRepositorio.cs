using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.Repositorio
{
    /// Repositorio: FormulaPuntajeRepositorio
    /// Autor: Britsel C., Luis H., Edgar S.
    /// Fecha: 29/01/2021
    /// <summary>
    /// Gestión Formula de Puntajes de Procesos de Seleccion
    /// </summary>
    public class FormulaPuntajeRepositorio : BaseRepository<TFormulaPuntaje, FormulaPuntajeBO>
    {

        #region Metodos Base
        public FormulaPuntajeRepositorio() : base()
        {
        }
        public FormulaPuntajeRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<FormulaPuntajeBO> GetBy(Expression<Func<TFormulaPuntaje, bool>> filter)
        {
            IEnumerable<TFormulaPuntaje> listado = base.GetBy(filter);
            List<FormulaPuntajeBO> listadoBO = new List<FormulaPuntajeBO>();
            foreach (var itemEntidad in listado)
            {
                FormulaPuntajeBO objetoBO = Mapper.Map<TFormulaPuntaje, FormulaPuntajeBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public FormulaPuntajeBO FirstById(int id)
        {
            try
            {
                TFormulaPuntaje entidad = base.FirstById(id);
                FormulaPuntajeBO objetoBO = new FormulaPuntajeBO();
                Mapper.Map<TFormulaPuntaje, FormulaPuntajeBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public FormulaPuntajeBO FirstBy(Expression<Func<TFormulaPuntaje, bool>> filter)
        {
            try
            {
                TFormulaPuntaje entidad = base.FirstBy(filter);
                FormulaPuntajeBO objetoBO = Mapper.Map<TFormulaPuntaje, FormulaPuntajeBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(FormulaPuntajeBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TFormulaPuntaje entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<FormulaPuntajeBO> listadoBO)
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

        public bool Update(FormulaPuntajeBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TFormulaPuntaje entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<FormulaPuntajeBO> listadoBO)
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
        private void AsignacionId(TFormulaPuntaje entidad, FormulaPuntajeBO objetoBO)
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

        private TFormulaPuntaje MapeoEntidad(FormulaPuntajeBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TFormulaPuntaje entidad = new TFormulaPuntaje();
                entidad = Mapper.Map<FormulaPuntajeBO, TFormulaPuntaje>(objetoBO,
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

        public List<FiltroIdNombreDTO> GetFiltroIdNombre()
        {
            var lista = GetBy(x => true, y => new FiltroIdNombreDTO
            {
                Id = y.Id,
                Nombre = y.Nombre
            }).ToList();
            return lista;
        }
    }
}
