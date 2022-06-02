using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class OportunidadCompetidorRepositorio : BaseRepository<TOportunidadCompetidor, OportunidadCompetidorBO>
    {
        #region Metodos Base
        public OportunidadCompetidorRepositorio() : base()
        {
        }
        public OportunidadCompetidorRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<OportunidadCompetidorBO> GetBy(Expression<Func<TOportunidadCompetidor, bool>> filter)
        {
            IEnumerable<TOportunidadCompetidor> listado = base.GetBy(filter);
            List<OportunidadCompetidorBO> listadoBO = new List<OportunidadCompetidorBO>();
            foreach (var itemEntidad in listado)
            {
                OportunidadCompetidorBO objetoBO = Mapper.Map<TOportunidadCompetidor, OportunidadCompetidorBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public OportunidadCompetidorBO FirstById(int id)
        {
            try
            {
                TOportunidadCompetidor entidad = base.FirstById(id);
                OportunidadCompetidorBO objetoBO = new OportunidadCompetidorBO();
                Mapper.Map<TOportunidadCompetidor, OportunidadCompetidorBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public OportunidadCompetidorBO FirstBy(Expression<Func<TOportunidadCompetidor, bool>> filter)
        {
            try
            {
                TOportunidadCompetidor entidad = base.FirstBy(filter);
                OportunidadCompetidorBO objetoBO = Mapper.Map<TOportunidadCompetidor, OportunidadCompetidorBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(OportunidadCompetidorBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TOportunidadCompetidor entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<OportunidadCompetidorBO> listadoBO)
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

        public bool Update(OportunidadCompetidorBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TOportunidadCompetidor entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<OportunidadCompetidorBO> listadoBO)
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
        private void AsignacionId(TOportunidadCompetidor entidad, OportunidadCompetidorBO objetoBO)
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

        private TOportunidadCompetidor MapeoEntidad(OportunidadCompetidorBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TOportunidadCompetidor entidad = new TOportunidadCompetidor();
				entidad = Mapper.Map<OportunidadCompetidorBO, TOportunidadCompetidor>(objetoBO,
					opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos
                if (objetoBO.ListaPrerequisitoGeneral != null && objetoBO.ListaPrerequisitoGeneral.Count > 0)
                {
                    foreach (var hijo in objetoBO.ListaPrerequisitoGeneral)
                    {
                        TOportunidadPrerequisitoGeneral entidadHijo = new TOportunidadPrerequisitoGeneral();
						entidadHijo = Mapper.Map<OportunidadPrerequisitoGeneralBO, TOportunidadPrerequisitoGeneral>(
							hijo,
							opt => opt.ConfigureMap(MemberList.None));
                        entidad.TOportunidadPrerequisitoGeneral.Add(entidadHijo);
                    }
                }
                if (objetoBO.ListaPrerequisitoEspecifico != null && objetoBO.ListaPrerequisitoEspecifico.Count > 0)
                {
                    foreach (var hijo in objetoBO.ListaPrerequisitoEspecifico)
                    {
                        TOportunidadPrerequisitoEspecifico entidadHijo = new TOportunidadPrerequisitoEspecifico();
						entidadHijo = Mapper.Map<OportunidadPrerequisitoEspecificoBO, TOportunidadPrerequisitoEspecifico>(
							hijo,
							opt => opt.ConfigureMap(MemberList.None));

                        entidad.TOportunidadPrerequisitoEspecifico.Add(entidadHijo);
                    }
                }
                if (objetoBO.ListaBeneficio != null && objetoBO.ListaBeneficio.Count > 0)
                {
                    foreach (var hijo in objetoBO.ListaBeneficio)
                    {
                        TOportunidadBeneficio entidadHijo = new TOportunidadBeneficio();
						entidadHijo = Mapper.Map<OportunidadBeneficioBO, TOportunidadBeneficio>(
							hijo,
							opt => opt.ConfigureMap(MemberList.None));
                        entidad.TOportunidadBeneficio.Add(entidadHijo);
                    }
                }
                if (objetoBO.ListaCompetidor != null && objetoBO.ListaCompetidor.Count > 0)
                {
                    foreach (var hijo in objetoBO.ListaCompetidor)
                    {
                        TDetalleOportunidadCompetidor entidadHijo = new TDetalleOportunidadCompetidor();
						entidadHijo = Mapper.Map<DetalleOportunidadCompetidorBO, TDetalleOportunidadCompetidor>(
							hijo,
							opt => opt.ConfigureMap(MemberList.None));
                        entidad.TDetalleOportunidadCompetidor.Add(entidadHijo);
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
