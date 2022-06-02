using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Transversal.Scode.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal;
using System.Linq;
using Newtonsoft.Json;
using BSI.Integra.Aplicacion.Transversal.DTO;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: EstructuraEspecificaCapitulo
    /// Autor: Lourdes Priscila Pacsi Gamboa
    /// Fecha:21/09/2021
    /// <summary>
    /// Repositorio para consultas de ope.T_EstructuraEspecificaCapitulo
    /// </summary>
    public class EstructuraEspecificaCapituloRepositorio : BaseRepository<TEstructuraEspecificaCapitulo, EstructuraEspecificaCapituloBO>
    {
        #region Metodos Base
        public EstructuraEspecificaCapituloRepositorio() : base()
        {
        }
        public EstructuraEspecificaCapituloRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<EstructuraEspecificaCapituloBO> GetBy(Expression<Func<TEstructuraEspecificaCapitulo, bool>> filter)
        {
            IEnumerable<TEstructuraEspecificaCapitulo> listado = base.GetBy(filter);
            List<EstructuraEspecificaCapituloBO> listadoBO = new List<EstructuraEspecificaCapituloBO>();
            foreach (var itemEntidad in listado)
            {
                EstructuraEspecificaCapituloBO objetoBO = Mapper.Map<TEstructuraEspecificaCapitulo, EstructuraEspecificaCapituloBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public EstructuraEspecificaCapituloBO FirstById(int id)
        {
            try
            {
                TEstructuraEspecificaCapitulo entidad = base.FirstById(id);
                EstructuraEspecificaCapituloBO objetoBO = new EstructuraEspecificaCapituloBO();
                Mapper.Map<TEstructuraEspecificaCapitulo, EstructuraEspecificaCapituloBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public EstructuraEspecificaCapituloBO FirstBy(Expression<Func<TEstructuraEspecificaCapitulo, bool>> filter)
        {
            try
            {
                TEstructuraEspecificaCapitulo entidad = base.FirstBy(filter);
                EstructuraEspecificaCapituloBO objetoBO = Mapper.Map<TEstructuraEspecificaCapitulo, EstructuraEspecificaCapituloBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(EstructuraEspecificaCapituloBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TEstructuraEspecificaCapitulo entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<EstructuraEspecificaCapituloBO> listadoBO)
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

        public bool Update(EstructuraEspecificaCapituloBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TEstructuraEspecificaCapitulo entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<EstructuraEspecificaCapituloBO> listadoBO)
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
        private void AsignacionId(TEstructuraEspecificaCapitulo entidad, EstructuraEspecificaCapituloBO objetoBO)
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

        private TEstructuraEspecificaCapitulo MapeoEntidad(EstructuraEspecificaCapituloBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TEstructuraEspecificaCapitulo entidad = new TEstructuraEspecificaCapitulo();
                entidad = Mapper.Map<EstructuraEspecificaCapituloBO, TEstructuraEspecificaCapitulo>(objetoBO,
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
