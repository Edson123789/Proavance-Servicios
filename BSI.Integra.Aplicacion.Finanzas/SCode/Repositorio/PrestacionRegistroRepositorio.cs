using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Finanzas.Repositorio
{
    public class PrestacionRegistroRepositorio : BaseRepository<TPrestacionRegistro, PrestacionRegistroBO>
    {
        #region Metodos Base
        public PrestacionRegistroRepositorio() : base()
        {
        }
        public PrestacionRegistroRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PrestacionRegistroBO> GetBy(Expression<Func<TPrestacionRegistro, bool>> filter)
        {
            IEnumerable<TPrestacionRegistro> listado = base.GetBy(filter);
            List<PrestacionRegistroBO> listadoBO = new List<PrestacionRegistroBO>();
            foreach (var itemEntidad in listado)
            {
                PrestacionRegistroBO objetoBO = Mapper.Map<TPrestacionRegistro, PrestacionRegistroBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PrestacionRegistroBO FirstById(int id)
        {
            try
            {
                TPrestacionRegistro entidad = base.FirstById(id);
                PrestacionRegistroBO objetoBO = new PrestacionRegistroBO();
                Mapper.Map<TPrestacionRegistro, PrestacionRegistroBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PrestacionRegistroBO FirstBy(Expression<Func<TPrestacionRegistro, bool>> filter)
        {
            try
            {
                TPrestacionRegistro entidad = base.FirstBy(filter);
                PrestacionRegistroBO objetoBO = Mapper.Map<TPrestacionRegistro, PrestacionRegistroBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PrestacionRegistroBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPrestacionRegistro entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PrestacionRegistroBO> listadoBO)
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

        public bool Update(PrestacionRegistroBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPrestacionRegistro entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PrestacionRegistroBO> listadoBO)
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
        private void AsignacionId(TPrestacionRegistro entidad, PrestacionRegistroBO objetoBO)
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

        private TPrestacionRegistro MapeoEntidad(PrestacionRegistroBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPrestacionRegistro entidad = new TPrestacionRegistro();
                entidad = Mapper.Map<PrestacionRegistroBO, TPrestacionRegistro>(objetoBO,
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
        public List<FiltroDTO> obtenerPrestacionRegistro()
        {
            try
            {
                List<FiltroDTO> listaPrestacionRegistro = this.GetBy(x => x.Estado == true, x => new FiltroDTO { Id = x.Id, Nombre = x.Nombre.ToUpper() }).ToList();
                return listaPrestacionRegistro;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
