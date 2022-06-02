using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.GestionPersonas.Repositorio
{
    public class PaginaReclutadoraPersonalRepositorio : BaseRepository<TPaginaReclutadoraPersonal, PaginaReclutadoraPersonalBO>
    {
        #region Metodos Base
        public PaginaReclutadoraPersonalRepositorio() : base()
        {
        }
        public PaginaReclutadoraPersonalRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PaginaReclutadoraPersonalBO> GetBy(Expression<Func<TPaginaReclutadoraPersonal, bool>> filter)
        {
            IEnumerable<TPaginaReclutadoraPersonal> listado = base.GetBy(filter);
            List<PaginaReclutadoraPersonalBO> listadoBO = new List<PaginaReclutadoraPersonalBO>();
            foreach (var itemEntidad in listado)
            {
                PaginaReclutadoraPersonalBO objetoBO = Mapper.Map<TPaginaReclutadoraPersonal, PaginaReclutadoraPersonalBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PaginaReclutadoraPersonalBO FirstById(int id)
        {
            try
            {
                TPaginaReclutadoraPersonal entidad = base.FirstById(id);
                PaginaReclutadoraPersonalBO objetoBO = new PaginaReclutadoraPersonalBO();
                Mapper.Map<TPaginaReclutadoraPersonal, PaginaReclutadoraPersonalBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PaginaReclutadoraPersonalBO FirstBy(Expression<Func<TPaginaReclutadoraPersonal, bool>> filter)
        {
            try
            {
                TPaginaReclutadoraPersonal entidad = base.FirstBy(filter);
                PaginaReclutadoraPersonalBO objetoBO = Mapper.Map<TPaginaReclutadoraPersonal, PaginaReclutadoraPersonalBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PaginaReclutadoraPersonalBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPaginaReclutadoraPersonal entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PaginaReclutadoraPersonalBO> listadoBO)
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

        public bool Update(PaginaReclutadoraPersonalBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPaginaReclutadoraPersonal entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PaginaReclutadoraPersonalBO> listadoBO)
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
        private void AsignacionId(TPaginaReclutadoraPersonal entidad, PaginaReclutadoraPersonalBO objetoBO)
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

        private TPaginaReclutadoraPersonal MapeoEntidad(PaginaReclutadoraPersonalBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPaginaReclutadoraPersonal entidad = new TPaginaReclutadoraPersonal();
                entidad = Mapper.Map<PaginaReclutadoraPersonalBO, TPaginaReclutadoraPersonal>(objetoBO,
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
