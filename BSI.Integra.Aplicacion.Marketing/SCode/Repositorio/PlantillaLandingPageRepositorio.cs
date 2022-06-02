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
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
    public class PlantillaLandingPageRepositorio : BaseRepository<TPlantillaLandingPage, PlantillaLandingPageBO>
    {
        #region Metodos Base
        public PlantillaLandingPageRepositorio() : base()
        {
        }
        public PlantillaLandingPageRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PlantillaLandingPageBO> GetBy(Expression<Func<TPlantillaLandingPage, bool>> filter)
        {
            IEnumerable<TPlantillaLandingPage> listado = base.GetBy(filter);
            List<PlantillaLandingPageBO> listadoBO = new List<PlantillaLandingPageBO>();
            foreach (var itemEntidad in listado)
            {
                PlantillaLandingPageBO objetoBO = Mapper.Map<TPlantillaLandingPage, PlantillaLandingPageBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PlantillaLandingPageBO FirstById(int id)
        {
            try
            {
                TPlantillaLandingPage entidad = base.FirstById(id);
                PlantillaLandingPageBO objetoBO = new PlantillaLandingPageBO();
                Mapper.Map<TPlantillaLandingPage, PlantillaLandingPageBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        

        public PlantillaLandingPageBO FirstBy(Expression<Func<TPlantillaLandingPage, bool>> filter)
        {
            try
            {
                TPlantillaLandingPage entidad = base.FirstBy(filter);
                PlantillaLandingPageBO objetoBO = Mapper.Map<TPlantillaLandingPage, PlantillaLandingPageBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PlantillaLandingPageBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPlantillaLandingPage entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PlantillaLandingPageBO> listadoBO)
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

        public bool Update(PlantillaLandingPageBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPlantillaLandingPage entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PlantillaLandingPageBO> listadoBO)
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
        private void AsignacionId(TPlantillaLandingPage entidad, PlantillaLandingPageBO objetoBO)
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

        private TPlantillaLandingPage MapeoEntidad(PlantillaLandingPageBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPlantillaLandingPage entidad = new TPlantillaLandingPage();
                entidad = Mapper.Map<PlantillaLandingPageBO, TPlantillaLandingPage>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos
                if (objetoBO.PGeneralAdicional != null && objetoBO.PGeneralAdicional.Count > 0)
                {
                    foreach (var hijo in objetoBO.PGeneralAdicional)
                    {
                        TPlantillaLandingPagePgeneralAdicional entidadHijo = new TPlantillaLandingPagePgeneralAdicional();
                        entidadHijo = Mapper.Map<PlantillaLandingPagePgeneralAdicionalBO, TPlantillaLandingPagePgeneralAdicional>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TPlantillaLandingPagePgeneralAdicional.Add(entidadHijo);
                    }
                }

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion
        /// <summary>
        /// Obtiene Todos Los Registros 
        /// </summary>
        /// <returns></returns>
        public List<PlantillaLandingPageDTO> ObtenerTodoGrilla()
        {
            try
            {
                string _queryPlantilla = "SELECT Id,Nombre,Cita1Texto,Cita1Color,Cita2Texto,Cita2Color,Cita3Texto,Cita3Color,UrlImagenPrincipal,PorDefecto,Cita4Texto,Cita4Color,ColorPopup,ColorTitulo,ColorTextoBoton,ColorFondoBoton,ColorDescripcion,ColorFondoHeader,Cita1Despues,MuestraPrograma,ColorPlaceHolder,Plantilla2,TipoPlantilla,IdListaPlantilla,FormularioTituloTamanhio,FormularioTituloFormato,FormularioBotonTamanhio,FormularioBotonFormato,FormularioTextoTamanhio,FormularioTextoFormato,TituloTituloTamanhio,TituloTituloFormato,TituloTextoTamanhio,TituloTextoFormato,TextoTituloTamanhio,TextoTituloFormato,TextoTextoTamanhio,TextoTextoFormato,FormularioBotonPosicion"
                                        + " FROM mkt.V_TPlantillaLandingPage WHERE Estado=1 order by FechaCreacion desc";
                var queryPlantilla = _dapper.QueryDapper(_queryPlantilla, null);
                return JsonConvert.DeserializeObject<List<PlantillaLandingPageDTO>>(queryPlantilla);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<PlantillaLandingPageFiltroDTO> GetFiltroIdNombre()
        {
            var lista = GetBy(x => true, y => new PlantillaLandingPageFiltroDTO
            {
                Id = y.Id,
                Nombre = y.Nombre
            }).ToList();
            return lista;
        }

    }
}
