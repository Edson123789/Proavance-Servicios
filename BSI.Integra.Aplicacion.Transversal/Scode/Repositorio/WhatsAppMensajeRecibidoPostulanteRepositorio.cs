using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
	public class WhatsAppMensajeRecibidoPostulanteRepositorio : BaseRepository<TWhatsAppMensajeRecibidoPostulante, WhatsAppMensajeRecibidoPostulanteBO>
	{

		#region Metodos Base
		public WhatsAppMensajeRecibidoPostulanteRepositorio() : base()
		{
		}
		public WhatsAppMensajeRecibidoPostulanteRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<WhatsAppMensajeRecibidoPostulanteBO> GetBy(Expression<Func<TWhatsAppMensajeRecibidoPostulante, bool>> filter)
		{
			IEnumerable<TWhatsAppMensajeRecibidoPostulante> listado = base.GetBy(filter);
			List<WhatsAppMensajeRecibidoPostulanteBO> listadoBO = new List<WhatsAppMensajeRecibidoPostulanteBO>();
			foreach (var itemEntidad in listado)
			{
				WhatsAppMensajeRecibidoPostulanteBO objetoBO = Mapper.Map<TWhatsAppMensajeRecibidoPostulante, WhatsAppMensajeRecibidoPostulanteBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public WhatsAppMensajeRecibidoPostulanteBO FirstById(int id)
		{
			try
			{
				TWhatsAppMensajeRecibidoPostulante entidad = base.FirstById(id);
				WhatsAppMensajeRecibidoPostulanteBO objetoBO = new WhatsAppMensajeRecibidoPostulanteBO();
				Mapper.Map<TWhatsAppMensajeRecibidoPostulante, WhatsAppMensajeRecibidoPostulanteBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public WhatsAppMensajeRecibidoPostulanteBO FirstBy(Expression<Func<TWhatsAppMensajeRecibidoPostulante, bool>> filter)
		{
			try
			{
				TWhatsAppMensajeRecibidoPostulante entidad = base.FirstBy(filter);
				WhatsAppMensajeRecibidoPostulanteBO objetoBO = Mapper.Map<TWhatsAppMensajeRecibidoPostulante, WhatsAppMensajeRecibidoPostulanteBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(WhatsAppMensajeRecibidoPostulanteBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TWhatsAppMensajeRecibidoPostulante entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<WhatsAppMensajeRecibidoPostulanteBO> listadoBO)
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

		public bool Update(WhatsAppMensajeRecibidoPostulanteBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TWhatsAppMensajeRecibidoPostulante entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<WhatsAppMensajeRecibidoPostulanteBO> listadoBO)
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
		private void AsignacionId(TWhatsAppMensajeRecibidoPostulante entidad, WhatsAppMensajeRecibidoPostulanteBO objetoBO)
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

		private TWhatsAppMensajeRecibidoPostulante MapeoEntidad(WhatsAppMensajeRecibidoPostulanteBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TWhatsAppMensajeRecibidoPostulante entidad = new TWhatsAppMensajeRecibidoPostulante();
				entidad = Mapper.Map<WhatsAppMensajeRecibidoPostulanteBO, TWhatsAppMensajeRecibidoPostulante>(objetoBO,
					opt => opt.ConfigureMap(MemberList.None));

				return entidad;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		#endregion

		public string guardarArchivos(byte[] archivo, string carpetaArchivo, string tipo, string nombreArchivo, int IdPais)
		{
			try
			{
				string _nombreLink = string.Empty;

				try
				{
					string _azureStorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=repositorioweb;AccountKey=JurvlnvFAqg4dcGqcDHEj9bkBLoLV3Z/EIxA+8QkdTcuCWTm1iZfgqUOfUOwmDMfnrmrie7Nkkho5mPyVTvIpA==;EndpointSuffix=core.windows.net";

					string _direccionBlob = @"repositorioweb/whatsapp/";

					switch (IdPais)
					{
						case 51: // Peru
							_direccionBlob += "peru/" + carpetaArchivo;
							break;
						case 57: // Colombia
							_direccionBlob += "colombia/" + carpetaArchivo;
							break;
						case 591: // Bolivia
							_direccionBlob += "bolivia/" + carpetaArchivo;
							break;
						case 0: // Internacional
							_direccionBlob += "internacional/" + carpetaArchivo;
							break;
						default:
							_direccionBlob += "pruebas/" + carpetaArchivo;
							break;
					}

					//Generar entrada al blob storage
					CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_azureStorageConnectionString);
					CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
					CloudBlobContainer container = blobClient.GetContainerReference(_direccionBlob);
					nombreArchivo = QuitarAcentos(nombreArchivo);
					CloudBlockBlob blockBlob = container.GetBlockBlobReference(nombreArchivo);
					blockBlob.Properties.ContentType = tipo;
					blockBlob.Metadata["filename"] = nombreArchivo;
					blockBlob.Metadata["filemime"] = tipo;
					Stream stream = new MemoryStream(archivo);
					//AsyncCallback UploadCompleted = new AsyncCallback(OnUploadCompleted);
					blockBlob.UploadFromStreamAsync(stream);

					_nombreLink = "https://repositorioweb.blob.core.windows.net/" + _direccionBlob + "/" + nombreArchivo.Replace(" ", "%20");



				}
				catch (Exception ex)
				{
					//Logger.Error(ex.ToString());
				}
				return _nombreLink;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public string guardarArchivos(byte[] archivo, string tipo, string nombreArchivo)
		{
			try
			{
				string _nombreLink = string.Empty;

				try
				{
					string _azureStorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=repositorioweb;AccountKey=JurvlnvFAqg4dcGqcDHEj9bkBLoLV3Z/EIxA+8QkdTcuCWTm1iZfgqUOfUOwmDMfnrmrie7Nkkho5mPyVTvIpA==;EndpointSuffix=core.windows.net";

					string _direccionBlob = @"repositorioweb/whatsapp/enviados/";

					//Generar entrada al blob storage
					CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_azureStorageConnectionString);
					CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
					CloudBlobContainer container = blobClient.GetContainerReference(_direccionBlob);

					CloudBlockBlob blockBlob = container.GetBlockBlobReference(nombreArchivo);
					blockBlob.Properties.ContentType = tipo;
					nombreArchivo = QuitarAcentos(nombreArchivo);
					blockBlob.Metadata["filename"] = nombreArchivo;
					blockBlob.Metadata["filemime"] = tipo;
					Stream stream = new MemoryStream(archivo);
					//AsyncCallback UploadCompleted = new AsyncCallback(OnUploadCompleted);
					var objRegistrado = blockBlob.UploadFromStreamAsync(stream);

					objRegistrado.Wait();
					var correcto = objRegistrado.IsCompletedSuccessfully;

					if (correcto)
					{
						_nombreLink = "https://repositorioweb.blob.core.windows.net/" + _direccionBlob + nombreArchivo.Replace(" ", "%20");
					}
					else
					{
						_nombreLink = "";
					}


					return _nombreLink;

				}
				catch (Exception ex)
				{
					return "";
				}

			}
			catch (Exception e)
			{
				//throw new Exception(e.Message);
				return "";
			}
		}

		public WhatsAppMensajeRecibidoPostulanteBO ObtenerWhatsAppMensajeRecibidoPostulantePorWaId(string waId)
		{
			try
			{
				var query = "SELECT * FROM gp.T_WhatsAppMensajeRecibidoPostulante WHERE WaId = @WaId ORDER BY FechaCreacion Desc";
				var res = _dapper.FirstOrDefault(query, new { WaId = waId });
				var rpta = JsonConvert.DeserializeObject<WhatsAppMensajeRecibidoPostulanteBO>(res);
				return rpta;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		private string QuitarAcentos(string inputString)
		{
			Regex a = new Regex("[á|à|ä|â|Á|À|Ä|Â]", RegexOptions.Compiled);
			Regex e = new Regex("[é|è|ë|ê|É|È|Ë|Ê]", RegexOptions.Compiled);
			Regex i = new Regex("[í|ì|ï|î|Í|Ì|Ï|Î]", RegexOptions.Compiled);
			Regex o = new Regex("[ó|ò|ö|ô|Ó|Ò|Ö|Ô]", RegexOptions.Compiled);
			Regex u = new Regex("[ú|ù|ü|û|Ú|Ù|Ü|Û]", RegexOptions.Compiled);
			Regex n = new Regex("[ñ|Ñ]", RegexOptions.Compiled);
			inputString = a.Replace(inputString, "a");
			inputString = e.Replace(inputString, "e");
			inputString = i.Replace(inputString, "i");
			inputString = o.Replace(inputString, "o");
			inputString = u.Replace(inputString, "u");
			inputString = n.Replace(inputString, "n");
			return inputString;
		}
	}
}
