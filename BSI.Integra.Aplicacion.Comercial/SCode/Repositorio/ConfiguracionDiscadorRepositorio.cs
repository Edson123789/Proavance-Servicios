using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Comercial.Repositorio
{
    public class ConfiguracionDiscadorRepositorio : BaseRepository<TConfiguracionDiscador, ConfiguracionDiscadorBO>
    {
        #region Metodos Base
        public ConfiguracionDiscadorRepositorio() : base()
        {
        }
        public ConfiguracionDiscadorRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ConfiguracionDiscadorBO> GetBy(Expression<Func<TConfiguracionDiscador, bool>> filter)
        {
            IEnumerable<TConfiguracionDiscador> listado = base.GetBy(filter);
            List<ConfiguracionDiscadorBO> listadoBO = new List<ConfiguracionDiscadorBO>();
            foreach (var itemEntidad in listado)
            {
                ConfiguracionDiscadorBO objetoBO = Mapper.Map<TConfiguracionDiscador, ConfiguracionDiscadorBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        ///Repositorio: PersonalRepositorio
        ///Autor: Edgar Serruto
        ///Fecha: 22/07/2021
        /// <summary>
        /// Obtiene Personal Registrado
        /// </summary>
        /// <returns>List<PersonalGridDTO></returns>
        public List<ConfiguracionDiscadorDTO> ObtenerGrid()
        {
            try
            {
                var lista = GetBy(x => true, y => new ConfiguracionDiscadorDTO
                {
                    Id = y.Id,
                    IdEstadoOcurrencia = y.IdEstadoOcurrencia,
                    ContestaLlamada = y.ContestaLlamada,
                    IdOperadorComparacionTimbradoSegundos = y.IdOperadorComparacionTimbradoSegundos,
                    TiempoTimbrado = y.TiempoTimbrado,
                    IdOperadorComparacionEfectivoSegundos = y.IdOperadorComparacionEfectivoSegundos,
                    TiempoEfectivo = y.TiempoEfectivo,
                    CantidadIntentosContacto = y.CantidadIntentosContacto,
                    TiempoEsperaLlamadaSegundos = y.TiempoEsperaLlamadaSegundos,
                    DesvioLlamada = y.DesvioLlamada,
                    BuzonVoz = y.BuzonVoz,
                    NoConectaLlamada = y.NoConectaLlamada,
                    TelefonoApagado = y.TelefonoApagado,
                    NumeroNoExiste = y.NumeroNoExiste,
                    NumeroSuspendido = y.NumeroSuspendido,
                }).OrderByDescending(x => x.Id).ToList();
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ConfiguracionDiscadorBO FirstById(int id)
        {
            try
            {
                TConfiguracionDiscador entidad = base.FirstById(id);
                ConfiguracionDiscadorBO objetoBO = new ConfiguracionDiscadorBO();
                Mapper.Map<TConfiguracionDiscador, ConfiguracionDiscadorBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ConfiguracionDiscadorBO FirstBy(Expression<Func<TConfiguracionDiscador, bool>> filter)
        {
            try
            {
                TConfiguracionDiscador entidad = base.FirstBy(filter);
                ConfiguracionDiscadorBO objetoBO = Mapper.Map<TConfiguracionDiscador, ConfiguracionDiscadorBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ConfiguracionDiscadorBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TConfiguracionDiscador entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ConfiguracionDiscadorBO> listadoBO)
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

        public bool Update(ConfiguracionDiscadorBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TConfiguracionDiscador entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ConfiguracionDiscadorBO> listadoBO)
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
        private void AsignacionId(TConfiguracionDiscador entidad, ConfiguracionDiscadorBO objetoBO)
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

        private TConfiguracionDiscador MapeoEntidad(ConfiguracionDiscadorBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TConfiguracionDiscador entidad = new TConfiguracionDiscador();
                entidad = Mapper.Map<ConfiguracionDiscadorBO, TConfiguracionDiscador>(objetoBO,
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
