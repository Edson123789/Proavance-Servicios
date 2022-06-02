using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    /// BO: Marketing/ConfiguracionDatoRemarketingCategoriaOrigen
    /// Autor: Gian Miranda
    /// Fecha: 17/08/2021
    /// <summary>
    /// BO para la logica de Configuracion de datos de Remarketing y su Categoria de Origen
    /// </summary>
    public class ConfiguracionDatoRemarketingCategoriaOrigenBO : BaseBO
    {
        /// Propiedades	                        Significado
        /// -----------	                        ------------
        /// IdConfiguracionDatoRemarketing      Id de la configuracion de dato remarketing (PK de la tabla mkt.T_ConfiguracionDatoRemarketing)
        /// IdCategoriaOrigen                   Id de la categoria de origen (PK de la tabla mkt.T_CategoriaOrigen)
        /// IdMigracion                         Id migracion de V3 (Campo nullable)

        public int IdConfiguracionDatoRemarketing { get; set; }
        public int IdCategoriaOrigen { get; set; }
        public Guid? IdMigracion { get; set; }

        private readonly integraDBContext _integraDBContext;

        private readonly ConfiguracionDatoRemarketingCategoriaOrigenRepositorio _repConfiguracionDatoRemarketingCategoriaOrigen;

        public ConfiguracionDatoRemarketingCategoriaOrigenBO()
        {
            _repConfiguracionDatoRemarketingCategoriaOrigen = new ConfiguracionDatoRemarketingCategoriaOrigenRepositorio();
        }

        public ConfiguracionDatoRemarketingCategoriaOrigenBO(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;

            _repConfiguracionDatoRemarketingCategoriaOrigen = new ConfiguracionDatoRemarketingCategoriaOrigenRepositorio(_integraDBContext);
        }

        /// <summary>
        /// Actualiza lista de configuracion de remarketing Tipo de Dato
        /// </summary>
        /// <param name="idConfiguracionDatoRemarketing">Lista de objetos de clase AnuncioFacebookMetricaDTO</param>
        /// <param name="listaIdCategoriaOrigen">Lista de id de los tipos de datos (PK de la tabla mkt.T_CategoriaOrigen)</param>
        /// <param name="usuario">Usuario que realiza la accion</param>
        /// <returns>bool</returns>
        public bool ActualizarListaConfiguracionDatoRemarketingCategoriaOrigen(int idConfiguracionDatoRemarketing, List<int> listaIdCategoriaOrigen, string usuario)
        {
            try
            {
                var listaExistente = _repConfiguracionDatoRemarketingCategoriaOrigen.GetBy(x => x.IdConfiguracionDatoRemarketing == idConfiguracionDatoRemarketing);

                // Eliminado de datos no existentes
                var listaAEliminar = listaExistente.Where(x => !listaIdCategoriaOrigen.Contains(x.IdCategoriaOrigen));

                if (listaAEliminar.Any())
                {
                    bool resultadoEliminacion = _repConfiguracionDatoRemarketingCategoriaOrigen.Delete(listaAEliminar.Select(s => s.Id), usuario);

                    if (!resultadoEliminacion)
                        throw new Exception("Fallo en la eliminacion de categoria origen anteriores");
                }

                var listaAMantenerCategoriaOrigen = listaExistente.Where(x => listaIdCategoriaOrigen.Contains(x.IdCategoriaOrigen)).Select(s => s.IdCategoriaOrigen);

                // Insercion de nuevos datos
                var listaAInsertar = listaIdCategoriaOrigen.Where(x => !listaAMantenerCategoriaOrigen.Contains(x));

                if (listaAInsertar.Any())
                {
                    bool resultadoInsercion = _repConfiguracionDatoRemarketingCategoriaOrigen.Insert(listaAInsertar.Select(s => new ConfiguracionDatoRemarketingCategoriaOrigenBO()
                    {
                        IdConfiguracionDatoRemarketing = idConfiguracionDatoRemarketing,
                        IdCategoriaOrigen = s,
                        Estado = true,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    }));

                    if (!resultadoInsercion)
                        throw new Exception("Fallo en la insercion de categoria origen nuevos");
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Elimina la lista de configuracion de datos de remarketing de categoria origen
        /// </summary>
        /// <param name="idConfiguracionDatoRemarketing">Id de la configuracion de dato de remarketing (PK de la tabla mkt.T_ConfiguracionDatoRemarketing)</param>
        /// <param name="usuarioResponsable">Usuario que realiza la accion</param>
        /// <returns>bool</returns>
        public bool EliminarListaConfiguracionDatoRemarketingCategoriaOrigen(int idConfiguracionDatoRemarketing, string usuarioResponsable)
        {
            try
            {
                var listaIdAEliminar = _repConfiguracionDatoRemarketingCategoriaOrigen.GetBy(x => x.IdConfiguracionDatoRemarketing == idConfiguracionDatoRemarketing).Select(s => s.Id).ToList();
                bool resultadoEliminado = true;

                if (listaIdAEliminar.Any())
                    resultadoEliminado = _repConfiguracionDatoRemarketingCategoriaOrigen.Delete(listaIdAEliminar, usuarioResponsable);

                if (!resultadoEliminado)
                    throw new Exception("Fallo en el eliminado de categoria origen");

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
