using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    /// BO: Marketing/ConfiguracionDatoRemarketingProbabilidadRegistro
    /// Autor: Gian Miranda
    /// Fecha: 17/08/2021
    /// <summary>
    /// BO para la logica de Configuracion de datos de Remarketing y su Probabilidad Registro
    /// </summary>
    public class ConfiguracionDatoRemarketingProbabilidadRegistroBO : BaseBO
    {
        /// Propiedades	                        Significado
        /// -----------	                        ------------
        /// IdConfiguracionDatoRemarketing      Id de la configuracion de dato remarketing (PK de la tabla mkt.T_ConfiguracionDatoRemarketing)
        /// IdProbabilidadRegistroPw            Id de la probabilidad de registro (PK de la tabla mkt.T_ProbabilidadRegistro_PW)
        /// IdMigracion                         Id migracion de V3 (Campo nullable)

        public int IdConfiguracionDatoRemarketing { get; set; }
        public int IdProbabilidadRegistroPw { get; set; }
        public Guid? IdMigracion { get; set; }

        private readonly integraDBContext _integraDBContext;

        private readonly ConfiguracionDatoRemarketingProbabilidadRegistroRepositorio _repConfiguracionDatoRemarketingProbabilidadRegistro;

        public ConfiguracionDatoRemarketingProbabilidadRegistroBO()
        {
            _repConfiguracionDatoRemarketingProbabilidadRegistro = new ConfiguracionDatoRemarketingProbabilidadRegistroRepositorio();
        }

        public ConfiguracionDatoRemarketingProbabilidadRegistroBO(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;

            _repConfiguracionDatoRemarketingProbabilidadRegistro = new ConfiguracionDatoRemarketingProbabilidadRegistroRepositorio(_integraDBContext);
        }

        /// <summary>
        /// Actualiza lista de configuracion de remarketing Tipo de Dato
        /// </summary>
        /// <param name="idConfiguracionDatoRemarketing">Lista de objetos de clase AnuncioFacebookMetricaDTO</param>
        /// <param name="listaIdProbabilidadRegistro">Lista de id de los tipos de datos (PK de la tabla mkt.T_CategoriaOrigen)</param>
        /// <param name="usuario">Usuario que realiza la accion</param>
        /// <returns>bool</returns>
        public bool ActualizarListaConfiguracionDatoRemarketingProbabilidadRegistro(int idConfiguracionDatoRemarketing, List<int> listaIdProbabilidadRegistro, string usuario)
        {
            try
            {
                var listaExistente = _repConfiguracionDatoRemarketingProbabilidadRegistro.GetBy(x => x.IdConfiguracionDatoRemarketing == idConfiguracionDatoRemarketing);

                // Eliminado de datos no existentes
                var listaAEliminar = listaExistente.Where(x => !listaIdProbabilidadRegistro.Contains(x.IdProbabilidadRegistroPw));

                if (listaAEliminar.Any())
                {
                    bool resultadoEliminacion = _repConfiguracionDatoRemarketingProbabilidadRegistro.Delete(listaAEliminar.Select(s => s.Id), usuario);

                    if (!resultadoEliminacion)
                        throw new Exception("Fallo en la eliminacion de tipos de probabilidades de registro anteriores");
                }

                var listaAMantenerProbabilidadRegistro = listaExistente.Where(x => listaIdProbabilidadRegistro.Contains(x.IdProbabilidadRegistroPw)).Select(s => s.IdProbabilidadRegistroPw);

                // Insercion de nuevos datos
                var listaAInsertar = listaIdProbabilidadRegistro.Where(x => !listaAMantenerProbabilidadRegistro.Contains(x));

                if (listaAInsertar.Any())
                {
                    bool resultadoInsercion = _repConfiguracionDatoRemarketingProbabilidadRegistro.Insert(listaAInsertar.Select(s => new ConfiguracionDatoRemarketingProbabilidadRegistroBO()
                    {
                        IdConfiguracionDatoRemarketing = idConfiguracionDatoRemarketing,
                        IdProbabilidadRegistroPw = s,
                        Estado = true,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    }));

                    if (!resultadoInsercion)
                        throw new Exception("Fallo en la insercion de tipos de probabilidades de registro nuevos");
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Elimina la lista de configuracion de datos de remarketing de probabilidad de registro
        /// </summary>
        /// <param name="idConfiguracionDatoRemarketing">Id de la configuracion de dato de remarketing (PK de la tabla mkt.T_ConfiguracionDatoRemarketing)</param>
        /// <param name="usuarioResponsable">Usuario que realiza la accion</param>
        /// <returns>bool</returns>
        public bool EliminarListaConfiguracionDatoRemarketingProbabilidadRegistro(int idConfiguracionDatoRemarketing, string usuarioResponsable)
        {
            try
            {
                var listaIdAEliminar = _repConfiguracionDatoRemarketingProbabilidadRegistro.GetBy(x => x.IdConfiguracionDatoRemarketing == idConfiguracionDatoRemarketing).Select(s => s.Id).ToList();
                bool resultadoEliminado = true;

                if (listaIdAEliminar.Any())
                    resultadoEliminado = _repConfiguracionDatoRemarketingProbabilidadRegistro.Delete(listaIdAEliminar, usuarioResponsable);

                if (!resultadoEliminado)
                    throw new Exception("Fallo en el eliminado de probabilidad de registro");

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
