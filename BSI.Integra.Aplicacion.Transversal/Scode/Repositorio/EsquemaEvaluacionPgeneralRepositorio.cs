using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: Planioficacion/EsquemaEvaluacionPgeneral
    /// Autor:--
    /// Fecha: 01/10/2021
    /// <summary>
    /// Repositorio del esquema de evaluacion general
    /// </summary>
    public class EsquemaEvaluacionPgeneralRepositorio : BaseRepository<TEsquemaEvaluacionPgeneral, EsquemaEvaluacionPgeneralBO>
    {
        #region Metodos Base
        public EsquemaEvaluacionPgeneralRepositorio() : base()
        {
        }
        public EsquemaEvaluacionPgeneralRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<EsquemaEvaluacionPgeneralBO> GetBy(Expression<Func<TEsquemaEvaluacionPgeneral, bool>> filter)
        {
            IEnumerable<TEsquemaEvaluacionPgeneral> listado = base.GetBy(filter);
            List<EsquemaEvaluacionPgeneralBO> listadoBO = new List<EsquemaEvaluacionPgeneralBO>();
            foreach (var itemEntidad in listado)
            {
                EsquemaEvaluacionPgeneralBO objetoBO = Mapper.Map<TEsquemaEvaluacionPgeneral, EsquemaEvaluacionPgeneralBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public EsquemaEvaluacionPgeneralBO FirstById(int id)
        {
            try
            {
                TEsquemaEvaluacionPgeneral entidad = base.FirstById(id);
                EsquemaEvaluacionPgeneralBO objetoBO = new EsquemaEvaluacionPgeneralBO();
                Mapper.Map<TEsquemaEvaluacionPgeneral, EsquemaEvaluacionPgeneralBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public EsquemaEvaluacionPgeneralBO FirstBy(Expression<Func<TEsquemaEvaluacionPgeneral, bool>> filter)
        {
            try
            {
                TEsquemaEvaluacionPgeneral entidad = base.FirstBy(filter);
                EsquemaEvaluacionPgeneralBO objetoBO = Mapper.Map<TEsquemaEvaluacionPgeneral, EsquemaEvaluacionPgeneralBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(EsquemaEvaluacionPgeneralBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TEsquemaEvaluacionPgeneral entidad = MapeoEntidad(objetoBO, true, true, true);

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

        public bool Insert(IEnumerable<EsquemaEvaluacionPgeneralBO> listadoBO)
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
        /// Repositorio: EsquemaEvaluacionPgeneralRepositorio
        /// Autor: ----
        /// Fecha: 20/09/2021
        /// Versión: 1.0
        /// <summary>
        /// modifica los esquemas de evaluacion
        /// </summary>
        /// <param name=”objetoBO”>BO esquema evaluacion</param>
        /// <param name=”agregardetalle”>establece si se crearan nuevos detalles</param>
        /// <param name=”agregarModalidad”>establece si se crearan nuevas modalidades/param>
        /// <param name=”agregarProveddor”>establece si se crearan nuevos proveedores/param>
        /// <returns>bool</returns>
        public int Update(EsquemaEvaluacionPgeneralBO objetoBO, bool agregardetalle, bool agregarModalidad, bool agregarProveddor)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TEsquemaEvaluacionPgeneral entidad = MapeoEntidad(objetoBO, agregardetalle, agregarModalidad, agregarProveddor);

                bool resultado = base.Update(entidad);
                if (resultado)
                    AsignacionId(entidad, objetoBO);

                return entidad.Id;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Update(IEnumerable<EsquemaEvaluacionPgeneralBO> listadoBO)
        {
            try
            {
                foreach (var objetoBO in listadoBO)
                {
                    int resultado = Update(objetoBO, true, true, true);
                    if (resultado == 0)
                        return false;
                }

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Repositorio: EsquemaEvaluacionPgeneralRepositorio
        /// Autor: ----
        /// Fecha: 20/09/2021
        /// Versión: 1.0
        /// <summary>
        /// actualiza ,inserta o modifica las  modalidades del esquema
        /// </summary>
        /// <param name=”esquema”>DTO del esquema de evaluacion</param>
        /// <param name=”nuevoEsquema”>determina si el esquemas fue editado o insertado</param>
        /// <returns>bool</returns>
        public bool UpdateEsquemaEvaluacionPgeneralModelo(EsquemaEvaluacion_RegistrarAsignacionDTO esquema, bool nuevoEsquema)
        {
            try
            {
                EsquemaEvaluacionPgeneralModalidadRepositorio _repoModalidad = new EsquemaEvaluacionPgeneralModalidadRepositorio();
                EsquemaEvaluacionPgeneralModalidadBO esquemaModalidadBo = new EsquemaEvaluacionPgeneralModalidadBO();
                List<EsquemaEvaluacionPgeneralModalidadBO> esquemaModalidadBoInsert = new List<EsquemaEvaluacionPgeneralModalidadBO>();
                var listado = _repoModalidad.GetBy(w => w.IdEsquemaEvaluacionPgeneral == esquema.Id,
                    s => new
                    {
                        s.Id,
                        s.IdModalidadCurso
                    });
                var existe = false;
                foreach (var objetoBOOld in listado)
                {
                    foreach (var detalle in esquema.IdModalidad)
                    {
                        if (objetoBOOld.IdModalidadCurso == detalle)
                        {
                            existe = true;
                            break;
                        }
                    }
                    if (existe == false)
                    {
                        _repoModalidad.Delete(objetoBOOld.Id, esquema.NombreUsuario);
                    }
                    existe = false;
                }
                existe = false;
                foreach (var detalle in esquema.IdModalidad)
                {
                    
                    foreach (var objetoBOOld in listado)
                    {
                        if (objetoBOOld.IdModalidadCurso == detalle)
                        {
                            existe = true;
                            if (nuevoEsquema) {

                                esquemaModalidadBo = _repoModalidad.FirstById((int)objetoBOOld.Id);
                                esquemaModalidadBo.IdEsquemaEvaluacionPgeneral = esquema.Id;
                                esquemaModalidadBo.UsuarioModificacion = esquema.NombreUsuario;
                                esquemaModalidadBo.FechaModificacion = DateTime.Now;
                                _repoModalidad.Update(esquemaModalidadBo);
                            }
                            break;
                        }
                    }
                    if (existe == false)
                    {
                        esquemaModalidadBo = new EsquemaEvaluacionPgeneralModalidadBO();
                        esquemaModalidadBo.IdEsquemaEvaluacionPgeneral = esquema.Id;
                        esquemaModalidadBo.IdModalidadCurso = detalle;
                        esquemaModalidadBo.Estado = true;
                        esquemaModalidadBo.UsuarioModificacion = esquema.NombreUsuario;
                        esquemaModalidadBo.UsuarioCreacion = esquema.NombreUsuario;
                        esquemaModalidadBo.FechaCreacion = DateTime.Now;
                        esquemaModalidadBo.FechaModificacion = DateTime.Now;
                        esquemaModalidadBoInsert.Add(esquemaModalidadBo);
                    }
                    existe = false;
                }

                _repoModalidad.Insert(esquemaModalidadBoInsert);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Repositorio: EsquemaEvaluacionPgeneralRepositorio
        /// Autor: ----
        /// Fecha: 20/09/2021
        /// Versión: 1.0
        /// <summary>
        /// actualiza ,inserta o modifica los proveedores del esquema
        /// </summary>
        /// <param name=”esquema”>DTO del esquema de evaluacion</param>
        /// <param name=”nuevoEsquema”>determina si el esquemas fue editado o insertado</param>
        /// <returns>bool</returns>
        public bool UpdateEsquemaEvaluacionPgeneralProveedor(EsquemaEvaluacion_RegistrarAsignacionDTO esquema, bool nuevoEsquema)
        {
            try
            {
                EsquemaEvaluacionPgeneralProveedorRepositorio _repoProveedor = new EsquemaEvaluacionPgeneralProveedorRepositorio();
                EsquemaEvaluacionPgeneralProveedorBO esquemaProveedorBo = new EsquemaEvaluacionPgeneralProveedorBO();
                List<EsquemaEvaluacionPgeneralProveedorBO> esquemaProveedorBoInsert = new List<EsquemaEvaluacionPgeneralProveedorBO>();
                var listado = _repoProveedor.GetBy(w => w.IdEsquemaEvaluacionPgeneral == esquema.Id,
                    s => new
                    {
                        s.Id,
                        s.IdProveedor
                    });
                var existe = false;
                foreach (var objetoBOOld in listado)
                {
                    foreach (var detalle in esquema.IdProveedor)
                    {
                        if (objetoBOOld.IdProveedor == detalle)
                        {
                            existe = true;
                            break;
                        }
                    }
                    if (existe == false)
                    {
                        _repoProveedor.Delete(objetoBOOld.Id, esquema.NombreUsuario);
                    }
                    existe = false;
                }
                existe = false;
                foreach (var detalle in esquema.IdProveedor)
                {

                    foreach (var objetoBOOld in listado)
                    {
                        if (objetoBOOld.IdProveedor == detalle)
                        {
                            existe = true;
                            if (nuevoEsquema)
                            {

                                esquemaProveedorBo = _repoProveedor.FirstById((int)objetoBOOld.Id);
                                esquemaProveedorBo.IdEsquemaEvaluacionPgeneral = esquema.Id;
                                esquemaProveedorBo.UsuarioModificacion = esquema.NombreUsuario;
                                esquemaProveedorBo.FechaModificacion = DateTime.Now;
                                _repoProveedor.Update(esquemaProveedorBo);
                            }
                            break;
                        }
                    }
                    if (existe == false)
                    {
                        esquemaProveedorBo = new EsquemaEvaluacionPgeneralProveedorBO();
                        esquemaProveedorBo.IdEsquemaEvaluacionPgeneral = esquema.Id;
                        esquemaProveedorBo.IdProveedor = detalle;
                        esquemaProveedorBo.Estado = true;
                        esquemaProveedorBo.UsuarioModificacion = esquema.NombreUsuario;
                        esquemaProveedorBo.UsuarioCreacion = esquema.NombreUsuario;
                        esquemaProveedorBo.FechaCreacion = DateTime.Now;
                        esquemaProveedorBo.FechaModificacion = DateTime.Now;
                        esquemaProveedorBoInsert.Add(esquemaProveedorBo);
                    }
                    existe = false;
                }

                _repoProveedor.Insert(esquemaProveedorBoInsert);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Repositorio: EsquemaEvaluacionPgeneralRepositorio
        /// Autor: ----
        /// Fecha: 20/09/2021
        /// Versión: 1.0
        /// <summary>
        /// actualiza ,inserta o modifica los detalles del esquema
        /// </summary>
        /// <param name=”esquema”>DTO del esquema de evaluacion</param>
        /// <param name=”nuevoEsquema”>determina si el esquemas fue editado o insertado</param>
        /// <returns>bool</returns>
        public bool UpdateEsquemaEvaluacionPgeneralDetalle(EsquemaEvaluacion_RegistrarAsignacionDTO esquema, bool nuevoEsquema) {
            try
            {

                EsquemaEvaluacionPgeneralDetalleRepositorio _repodetalleEsquemaAignado = new EsquemaEvaluacionPgeneralDetalleRepositorio();

                EsquemaEvaluacionPgeneralDetalleBO esquemaDetalleBo = new EsquemaEvaluacionPgeneralDetalleBO();
                List<EsquemaEvaluacionPgeneralDetalleBO> esquemaDetalleBoInsert = new List<EsquemaEvaluacionPgeneralDetalleBO>();
                var listado = _repodetalleEsquemaAignado.GetBy(w => w.IdEsquemaEvaluacionPgeneral == esquema.Id,
                     s => new
                     {
                         s.Id,
                         s.IdCriterioEvaluacion,
                         s.IdProveedor,
                         NombreCriterioEvaluacion = s.IdCriterioEvaluacionNavigation.Nombre,
                         s.Nombre,
                         s.UrlArchivoInstrucciones
                     });

                var existe = false;
                foreach (var objetoBOOld in listado)
                {
                    foreach (var detalle in esquema.ListadoDetalleAsignacion)
                    {
                        if (objetoBOOld.Id == detalle.Id)
                        {
                            existe = true;
                            break;
                        }
                    }
                    if (existe == false)
                    {
                        _repodetalleEsquemaAignado.Delete(objetoBOOld.Id, esquema.NombreUsuario);
                    }
                    existe = false;
                }
                
                foreach (var detalle in esquema.ListadoDetalleAsignacion)
                {
                    if (detalle.Id == null)
                    {
                        esquemaDetalleBo = new EsquemaEvaluacionPgeneralDetalleBO();
                        esquemaDetalleBo.IdEsquemaEvaluacionPgeneral = esquema.Id;
                        esquemaDetalleBo.IdCriterioEvaluacion = detalle.IdCriterioEvaluacion;
                        esquemaDetalleBo.Nombre = detalle.Nombre;
                        esquemaDetalleBo.IdProveedor = detalle.IdProveedor;
                        esquemaDetalleBo.UrlArchivoInstrucciones = detalle.UrlArchivoInstrucciones;
                        esquemaDetalleBo.Estado = true;
                        esquemaDetalleBo.UsuarioModificacion = esquema.NombreUsuario;
                        esquemaDetalleBo.UsuarioCreacion = esquema.NombreUsuario;
                        esquemaDetalleBo.FechaCreacion = DateTime.Now;
                        esquemaDetalleBo.FechaModificacion = DateTime.Now;
                        esquemaDetalleBoInsert.Add(esquemaDetalleBo);
                    }
                    else
                    {
                        if (nuevoEsquema)
                        {

                            esquemaDetalleBo = _repodetalleEsquemaAignado.FirstById((int)detalle.Id);
                            esquemaDetalleBo.IdEsquemaEvaluacionPgeneral = esquema.Id;
                            esquemaDetalleBo.IdCriterioEvaluacion = detalle.IdCriterioEvaluacion;
                            esquemaDetalleBo.Nombre = detalle.Nombre;
                            esquemaDetalleBo.IdProveedor = detalle.IdProveedor;
                            esquemaDetalleBo.UrlArchivoInstrucciones = detalle.UrlArchivoInstrucciones;
                            esquemaDetalleBo.UsuarioModificacion = esquema.NombreUsuario;
                            esquemaDetalleBo.FechaModificacion = DateTime.Now;
                            _repodetalleEsquemaAignado.Update(esquemaDetalleBo);
                        }
                        else {
                            foreach (var objetoBOOld in listado)
                            {
                                if (objetoBOOld.Id == detalle.Id)
                                {

                                    if (objetoBOOld.IdCriterioEvaluacion != detalle.IdCriterioEvaluacion || objetoBOOld.IdProveedor != detalle.IdProveedor
                                        || objetoBOOld.Nombre != detalle.Nombre || objetoBOOld.UrlArchivoInstrucciones != detalle.UrlArchivoInstrucciones)
                                    {
                                        esquemaDetalleBo = _repodetalleEsquemaAignado.FirstById((int)detalle.Id);
                                        esquemaDetalleBo.IdEsquemaEvaluacionPgeneral = esquema.Id;
                                        esquemaDetalleBo.IdCriterioEvaluacion = detalle.IdCriterioEvaluacion;
                                        esquemaDetalleBo.Nombre = detalle.Nombre;
                                        esquemaDetalleBo.IdProveedor = detalle.IdProveedor;
                                        esquemaDetalleBo.UrlArchivoInstrucciones = detalle.UrlArchivoInstrucciones;
                                        esquemaDetalleBo.UsuarioModificacion = esquema.NombreUsuario;
                                        esquemaDetalleBo.FechaModificacion = DateTime.Now;
                                        _repodetalleEsquemaAignado.Update(esquemaDetalleBo);
                                    }
                                }
                            }
                           
                        }
                    }
                }

                _repodetalleEsquemaAignado.Insert(esquemaDetalleBoInsert);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Repositorio: EsquemaEvaluacionPgeneralRepositorio
        /// Autor: ----
        /// Fecha: 20/09/2021
        /// Versión: 1.0
        /// <summary>
        /// compara los detalles actuales con los recibidos
        /// </summary>
        /// <param name=”esquema”>DTO del esquema de evaluacion</param>
        /// <returns>bool</returns>
        public bool CompareEsquemaEvaluacionPgeneralDetalle(EsquemaEvaluacion_RegistrarAsignacionDTO esquema) {
            try
            {

                EsquemaEvaluacionPgeneralDetalleRepositorio _repodetalleEsquemaAignado = new EsquemaEvaluacionPgeneralDetalleRepositorio();
                var listado = _repodetalleEsquemaAignado.GetBy(w => w.IdEsquemaEvaluacionPgeneral == esquema.Id,
                    s => new
                    {
                        s.Id,
                        s.IdCriterioEvaluacion,
                        s.IdProveedor,
                        NombreCriterioEvaluacion = s.IdCriterioEvaluacionNavigation.Nombre,
                        s.Nombre,
                        s.UrlArchivoInstrucciones
                    });
                if (esquema.ListadoDetalleAsignacion.Count != listado.Count) {
                    return true;
                }
                var existe = false;
                foreach (var objetoBO in esquema.ListadoDetalleAsignacion)
                {
                    foreach (var objetoBOOld in listado)
                    {
                        if (objetoBOOld.Id == objetoBO.Id)
                        {

                            existe = true;
                            if (objetoBOOld.IdCriterioEvaluacion != objetoBO.IdCriterioEvaluacion || objetoBOOld.IdProveedor != objetoBO.IdProveedor
                                || objetoBOOld.Nombre != objetoBO.Nombre || objetoBOOld.UrlArchivoInstrucciones != objetoBO.UrlArchivoInstrucciones)
                            {
                                return true;
                            }
                        }
                    }
                    if (existe == false) {
                        return true;
                    }
                    existe = false;
                }
                return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Repositorio: EsquemaEvaluacionPgeneralRepositorio
        /// Autor: ----
        /// Fecha: 20/09/2021
        /// Versión: 1.0
        /// <summary>
        /// compara los proveedores actuales con los recibidos
        /// </summary>
        /// <param name=”esquema”>DTO del esquema de evaluacion</param>
        /// <returns>bool</returns>
        public bool CompareEsquemaEvaluacionPgeneralProveedor(EsquemaEvaluacion_RegistrarAsignacionDTO esquema)
        {
            try
            {

                EsquemaEvaluacionPgeneralProveedorRepositorio _repoProveedor = new EsquemaEvaluacionPgeneralProveedorRepositorio();
                var listado = _repoProveedor.GetBy(w => w.IdEsquemaEvaluacionPgeneral == esquema.Id,
                    s => new
                    {
                        s.IdProveedor
                    });


                if (esquema.IdProveedor.Count != listado.Count)
                {
                    return true;
                }
                var existe = false;

                foreach (var objetoBO in esquema.IdProveedor)
                {
                    foreach (var objetoBOOld in listado)
                    {
                        if (objetoBOOld.IdProveedor == objetoBO)
                        {
                            existe = true;
                            break;
                        }
                    }
                    if (existe == false)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Repositorio: EsquemaEvaluacionPgeneralRepositorio
        /// Autor: ----
        /// Fecha: 20/09/2021
        /// Versión: 1.0
        /// <summary>
        /// compara las modalidades actuales con las recibidas
        /// </summary>
        /// <param name=”esquema”>DTO del esquema de evaluacion</param>
        /// <returns>bool</returns>
        public bool CompareEsquemaEvaluacionPgeneralModalidad(EsquemaEvaluacion_RegistrarAsignacionDTO esquema)
        {
            try
            {

                EsquemaEvaluacionPgeneralModalidadRepositorio _repoModalidad = new EsquemaEvaluacionPgeneralModalidadRepositorio();
                var listado = _repoModalidad.GetBy(w => w.IdEsquemaEvaluacionPgeneral == esquema.Id,
                    s => new
                    {
                        s.IdModalidadCurso
                    });

                if (esquema.IdModalidad.Count != listado.Count)
                {
                    return true;
                }
                var existe = false;

                foreach (var objetoBO in esquema.IdModalidad)
                {
                    foreach (var objetoBOOld in listado)
                    {
                        if (objetoBOOld.IdModalidadCurso == objetoBO)
                        {
                            existe = true;
                            break;
                        }
                    }
                    if (existe == false)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        private void AsignacionId(TEsquemaEvaluacionPgeneral entidad, EsquemaEvaluacionPgeneralBO objetoBO)
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
        /// Repositorio: EsquemaEvaluacionPgeneralRepositorio
        /// Autor: ----
        /// Fecha: 20/09/2021
        /// Versión: 1.0
        /// <summary>
        /// mapea los esquemas recibidos asi como sus detalles ,modalidades y proveedores
        /// </summary>
        /// <param name=”objetoBO”>BO esquema evaluacion</param>
        /// <param name=”agregardetalle”>establece si se crearan nuevos detalles</param>
        /// <param name=”agregarModalidad”>establece si se crearan nuevas modalidades/param>
        /// <param name=”agregarProveddor”>establece si se crearan nuevos proveedores/param>
        /// <returns>bool</returns>
        private TEsquemaEvaluacionPgeneral MapeoEntidad(EsquemaEvaluacionPgeneralBO objetoBO, bool agregardetalle, bool agregarModalidad, bool agregarProveddor)
        {
            try
            {
                //crea la entidad padre
                TEsquemaEvaluacionPgeneral entidad = new TEsquemaEvaluacionPgeneral();
                entidad = Mapper.Map<EsquemaEvaluacionPgeneralBO, TEsquemaEvaluacionPgeneral>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos
                if (objetoBO.ListadoDetalle != null && objetoBO.ListadoDetalle.Count > 0 && agregardetalle)
                {
                    foreach (var hijo in objetoBO.ListadoDetalle)
                    {
                        TEsquemaEvaluacionPgeneralDetalle entidadHijo = new TEsquemaEvaluacionPgeneralDetalle();
                        entidadHijo = Mapper.Map<EsquemaEvaluacionPgeneralDetalleBO, TEsquemaEvaluacionPgeneralDetalle>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TEsquemaEvaluacionPgeneralDetalle.Add(entidadHijo);

                        //mapea al hijo interno
                    }
                }

                if (objetoBO.ListadoModalidad != null && objetoBO.ListadoModalidad.Count > 0 && agregarModalidad)
                {
                    foreach (var hijo in objetoBO.ListadoModalidad)
                    {
                        TEsquemaEvaluacionPgeneralModalidad entidadHijo = new TEsquemaEvaluacionPgeneralModalidad();
                        entidadHijo = Mapper.Map<EsquemaEvaluacionPgeneralModalidadBO, TEsquemaEvaluacionPgeneralModalidad>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TEsquemaEvaluacionPgeneralModalidad.Add(entidadHijo);

                        //mapea al hijo interno
                    }
                }

                if (objetoBO.ListadoProveedor != null && objetoBO.ListadoProveedor.Count > 0 && agregarProveddor)
                {
                    foreach (var hijo in objetoBO.ListadoProveedor)
                    {
                        TEsquemaEvaluacionPgeneralProveedor entidadHijo = new TEsquemaEvaluacionPgeneralProveedor();
                        entidadHijo = Mapper.Map<EsquemaEvaluacionPgeneralProveedorBO, TEsquemaEvaluacionPgeneralProveedor>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TEsquemaEvaluacionPgeneralProveedor.Add(entidadHijo);

                        //mapea al hijo interno
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
    }
}
