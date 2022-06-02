using BSI.Integra.Aplicacion.Classes;
using BSI.Integra.Persistencia.SCode.Repository;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class OportunidadInformacionBO
    {
        private int IdAlumno;
        private int IdClasificacionPersona;
        private OportunidadRepositorio _repOportunidad;
        private ProgramaGeneralBeneficioArgumentoRepositorio _repProgramaGeneralBeneficioArgumento;
        private DetalleOportunidadCompetidorRepositorio _repDetalleOportunidadCompetidor;
        private EmpresaRepositorio _repEmpresa;
        public List<OportunidadVentaCruzadaDTO> ListaVentaCruzada;
        public List<OportunidadHistorialDTO> ListaHistorial;
        public ProgramaGeneralPreBenCompuestoDTO ProgramaGeneralPreBen;
        public List<ActividadOportunidadDTO> ActividadesOportunidad;

        public OportunidadInformacionBO()
        {
            _repOportunidad = new OportunidadRepositorio();
            _repProgramaGeneralBeneficioArgumento = new ProgramaGeneralBeneficioArgumentoRepositorio();
            _repDetalleOportunidadCompetidor = new DetalleOportunidadCompetidorRepositorio();
            _repEmpresa = new EmpresaRepositorio();
            ProgramaGeneralPreBen = new ProgramaGeneralPreBenCompuestoDTO();
            ActividadesOportunidad = new List<ActividadOportunidadDTO>();
        }

        public OportunidadInformacionBO(int idAlumno, int idTipoPersona)
        {
            this.IdAlumno = idAlumno;
            this.IdClasificacionPersona = idTipoPersona;
            _repOportunidad = new OportunidadRepositorio();
            ListaVentaCruzada = new List<OportunidadVentaCruzadaDTO>();
            ListaHistorial = new List<OportunidadHistorialDTO>();
            CargarOportunidadVentaCruzada();
            CargarOportunidadHistorial();
        }

        /// <summary>
        /// Carga una lista de oportunidades con venta cruzada por idAlumno
        /// </summary
        private void CargarOportunidadVentaCruzada()
        {
            ListaVentaCruzada = _repOportunidad.ObtenerHistorialOportunidades(this.IdAlumno, this.IdClasificacionPersona);
        }

        /// <summary>
        /// Carga un historial de oportunidades por idAlumno
        /// </summary>
        private void CargarOportunidadHistorial()
        {
            ListaHistorial = _repOportunidad.CargarOportunidadHistorial(this.IdAlumno, this.IdClasificacionPersona);
        }

        /// <summary>
        /// Obtiene los prerequisitos, beneficios y empresa competidora para un Programa General
        /// </summary>
        /// <param name="idOportunidad"></param>
        public void CargarPrerequisitosBeneficios(int idOportunidad)
        {
            int i = 0;
            ProgramaGeneralPreBenCompuestoDTO programaGeneralPreBen = new ProgramaGeneralPreBenCompuestoDTO();
            programaGeneralPreBen.ListaPreGeneral = new List<ProgramaGeneralPrerequisitoOportunidadDTO>();
            programaGeneralPreBen.ListaPreEspecifico = new List<ProgramaGeneralPrerequisitoOportunidadDTO>();
            programaGeneralPreBen.ListaBeneficios = new List<ProgramaGeneralBeneficioOportunidadDTO>();
            programaGeneralPreBen.ListaCompetidores = new List<EmpresaFiltroDTO>();
            programaGeneralPreBen.OportunidadCompetidor = new OportunidadCompetidorDTO();
            programaGeneralPreBen.ListaPreGeneral = _repOportunidad.ObtenerPrerequisitosPorOportunidad(idOportunidad);
            programaGeneralPreBen.ListaPreEspecifico = _repOportunidad.ObtenerPrerequisitosEspecificoPorOportunidad(idOportunidad);
            programaGeneralPreBen.ListaBeneficios = _repOportunidad.ObtenerBeneficiosPorOportunidad(idOportunidad);

            foreach (var item in programaGeneralPreBen.ListaBeneficios)
            {
                programaGeneralPreBen.ListaBeneficios[i].Argumentos = _repProgramaGeneralBeneficioArgumento.ObtenerProgramaGeneralBeneficiosArgumentos(item.IdBeneficio);
                i++;
            }

            var oportunidadCompetidores = _repOportunidad.ObtenerCompetidoresPorOportunidad(idOportunidad);
            var oportunidadCompetidorDTO = (from t1 in oportunidadCompetidores
                             select new OportunidadCompetidorDTO
                             {
                                 Id = t1.Id,
                                 IdOportunidad = t1.IdOportunidad,
                                 Completado = t1.Completado,
                                 OtroBeneficio = t1.OtroBeneficio,
                                 Respuesta = t1.Respuesta,
                             }).FirstOrDefault();
            if (oportunidadCompetidorDTO != null)
            {
                var listaCompetidorDto = _repDetalleOportunidadCompetidor.ObtenerPorOportunidadCompetidor(oportunidadCompetidorDTO.Id);
                programaGeneralPreBen.OportunidadCompetidor = oportunidadCompetidorDTO;

                foreach (var item in listaCompetidorDto)
                {
                    var empresa = _repEmpresa.ObtenerFiltro(item.IdCompetidor);
                    if (empresa.Nombre != null)
                    {
                        var competidor = new EmpresaFiltroDTO
                        {
                            Id = item.IdCompetidor,
                            Nombre = empresa.Nombre
                        };
                        programaGeneralPreBen.ListaCompetidores.Add(competidor);
                    }
                }
            }
            ProgramaGeneralPreBen = programaGeneralPreBen;
        }

    }
}
