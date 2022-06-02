using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Base.Enums;
using BSI.Integra.Aplicacion.Maestros.Repositorio;
using BSI.Integra.Aplicacion.Operaciones.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;

namespace BSI.Integra.Aplicacion.Operaciones.BO
{
    public class FlujoBO : BaseBO
    {
        private FlujoRepositorio _repFlujo;
        private ModalidadCursoRepositorio _repModalidadCurso;
        private ClasificacionUbicacionDocenteRepositorio _repClasificacionUbicacionDocenteRepositorio;
        
        public FlujoBO()
        {
        }
        public FlujoBO(integraDBContext context)
        {
            _repFlujo = new FlujoRepositorio(context);
            _repModalidadCurso = new ModalidadCursoRepositorio(context);
            _repClasificacionUbicacionDocenteRepositorio = new ClasificacionUbicacionDocenteRepositorio(context);
        }

        public int IdModalidadCurso { get; set; }
        public int IdClasificacionUbicacionDocente { get; set; }
        public string Nombre { get; set; }

        public string Modalidad
        {
            get
            {
                if (_repModalidadCurso == null)
                    _repModalidadCurso = new ModalidadCursoRepositorio();
                var modalidad = _repModalidadCurso.FirstById(IdModalidadCurso);
                return modalidad != null ? modalidad.Nombre : "";
            }
        }
        public string ClasificacionUbicacionDocente {
            get
            {
                if (_repClasificacionUbicacionDocenteRepositorio == null)
                    _repClasificacionUbicacionDocenteRepositorio = new ClasificacionUbicacionDocenteRepositorio();
                var clasificacion = _repClasificacionUbicacionDocenteRepositorio.FirstById(IdClasificacionUbicacionDocente);
                return clasificacion != null ? clasificacion.Nombre : "";
            }
        }

        public int CalcularIdFlujo(int idClasificacionPersona, int idPespecifico)
        {
            //int respuesta = 1;
            int respuesta = 0;
            int clasificacionCiudad = 0;
            int modalidad = -1;

            ClasificacionPersonaRepositorio _repoClasificacion = new ClasificacionPersonaRepositorio();
            var clasificacion = _repoClasificacion.FirstBy(w => w.Id == idClasificacionPersona && w.IdTipoPersona == 4); // 4: proveedor
            if (clasificacion != null)
            {
                ProveedorRepositorio _repoProveedor = new ProveedorRepositorio();
                var proveedor = _repoProveedor.FirstById(clasificacion.IdTablaOriginal);
                if (proveedor != null)
                {
                    PespecificoRepositorio _repoPespecifico = new PespecificoRepositorio();
                    var pespecifico = _repoPespecifico.FirstById(idPespecifico);
                    if (pespecifico != null)
                    {
                        if (pespecifico.TipoId != null)
                            modalidad = pespecifico.TipoId.Value;
                        if (pespecifico.IdCiudad != null && proveedor.IdCiudad != null)
                        {
                            if (pespecifico.IdCiudad == proveedor.IdCiudad)
                                clasificacionCiudad = (int) Enums.CalsificacionUbicacionDocente.DocenteLocal;
                            else
                            {
                                CiudadRepositorio _repoCiudad = new CiudadRepositorio();

                                var ciudadPEspecifico = _repoCiudad.FirstById(pespecifico.IdCiudad.Value);
                                var ciudadProveedor = _repoCiudad.FirstById(proveedor.IdCiudad.Value);

                                if (ciudadPEspecifico != null && ciudadProveedor != null && ciudadPEspecifico.IdPais != null && ciudadProveedor.IdPais != null)
                                {
                                    if (ciudadPEspecifico.IdPais == ciudadProveedor.IdPais)
                                        clasificacionCiudad = (int)Enums.CalsificacionUbicacionDocente.DocenteNacional;
                                    else
                                        clasificacionCiudad = (int)Enums.CalsificacionUbicacionDocente.DocenteExtranjero;
                                }
                            }
                        }
                    }
                }
            }

            var flujo = _repFlujo.FirstBy(w =>
                w.IdModalidadCurso == modalidad && w.IdClasificacionUbicacionDocente == clasificacionCiudad);
            if (flujo != null)
                respuesta = flujo.Id;

            return respuesta;
        }
    }
}
