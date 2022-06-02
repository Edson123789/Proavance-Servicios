using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BSI.Integra.Aplicacion.Finanzas.BO
{
    public class ProveedorCalificacionBO : BaseBO
    {
        public int IdProveedor { get; set; }
        public int IdProveedorSubCriterioCalificacion { get; set; }
        public int? IdMigracion { get; set; }



        private ProveedorCalificacionRepositorio _repProveedorCalificacionRep;
        private ProveedorRepositorio _repProveedorRep;
        public ProveedorCalificacionBO()
        {
        }

        public ProveedorCalificacionBO(integraDBContext _integraDBContext)
        {
            _repProveedorCalificacionRep = new ProveedorCalificacionRepositorio(_integraDBContext);
            _repProveedorRep = new ProveedorRepositorio(_integraDBContext);
        }


        /// <summary> 
        /// Elimina Todos los subcriterios de calificacion en estado para un proveedor e inserta nuevos criterios para el mismo proveedor y actualiza en Proveedor el id seleccionado de Prestacion de Registro
        /// </summary>
        /// <param name="Calificacion"></param>
        /// <returns></returns>
        public bool InsertarCriterioCalificacion(ProveedorCalificacionDTO Calificacion)
        {
            try
            {

                var objProveedor = _repProveedorRep.FirstById(Calificacion.IdProveedor);

                if (objProveedor == null)
                    throw new Exception("No se encontro el registro de 'CajaPorRendir' que se quiere actualizar");
                objProveedor.IdPrestacionRegistro = Calificacion.IdPrestacionRegistro;
                objProveedor.FechaModificacion = DateTime.Now;
                objProveedor.UsuarioModificacion = Calificacion.UsuarioModificacion;

                _repProveedorRep.Update(objProveedor);


                var listaIdCriterio = _repProveedorCalificacionRep.GetBy(x => x.Estado == true && x.IdProveedor == Calificacion.IdProveedor, x => new { Id = x.Id }).ToList();
                foreach (var CriterioCalificacion in listaIdCriterio)
                {
                    if (_repProveedorCalificacionRep.Exist(CriterioCalificacion.Id))
                    {
                        _repProveedorCalificacionRep.Delete(CriterioCalificacion.Id, Calificacion.UsuarioModificacion);
                    }
                    else
                    {
                        return false;
                    }
                }

                for (int i = 0; i < Calificacion.ListaIdSubCriterioCalificacion.Length; i++)
                {
                    ProveedorCalificacionBO objProveedorCalificacion = new ProveedorCalificacionBO();
                    objProveedorCalificacion.IdProveedor = Calificacion.IdProveedor;
                    objProveedorCalificacion.IdProveedorSubCriterioCalificacion = Calificacion.ListaIdSubCriterioCalificacion[i];
                    objProveedorCalificacion.Estado = true;
                    objProveedorCalificacion.FechaCreacion = DateTime.Now;
                    objProveedorCalificacion.FechaModificacion = DateTime.Now;
                    objProveedorCalificacion.UsuarioCreacion = Calificacion.UsuarioModificacion;
                    objProveedorCalificacion.UsuarioModificacion = Calificacion.UsuarioModificacion;

                    _repProveedorCalificacionRep.Insert(objProveedorCalificacion);
                }   
                

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
