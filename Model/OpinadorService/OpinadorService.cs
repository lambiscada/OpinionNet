using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaIS.Model.UserProfileDao;
using Es.Udc.DotNet.PracticaIS.Model.ComentarioDao;
using Es.Udc.DotNet.PracticaIS.Model.EtiquetaDao;
using Es.Udc.DotNet.PracticaIS.Model.FavoritoDao;
using Es.Udc.DotNet.PracticaIS.Model.ValoracionDao;
using System.Xml;
using System.Data.Objects.DataClasses;

namespace Es.Udc.DotNet.PracticaIS.Model.OpinadorService
{
    public class OpinadorService : IOpinadorService
    {
        [Dependency]
        public IUserProfileDao UserProfileDao { private get; set; }

        [Dependency]
        public IComentarioDao ComentarioDao { private get; set; }

        [Dependency]
        public IEtiquetaDao EtiquetaDao { private get;  set; }

        [Dependency]
        public IFavoritoDao FavoritoDao { private get;  set; }

        [Dependency]
        public IValoracionDao ValoracionDao { private get; set; }


        public XmlDocument FindProductos(String nombreProducto,int startIndex)
        {
            XmlDocument xmlDocumento = new XmlDocument();
            String url = ("http://localhost:8080/subastador/api/XmlSearchProductsService?name=" + nombreProducto + "&ind=" +startIndex);
            xmlDocumento.Load(url);
            return xmlDocumento;
        }

        /// <exception cref="InstanceNotFoundException">If productoId doesn't exist</exception>
        public ComentarioEtiquetaBlock AddComentarioEtiqueta(long usrId, long productoId, string textoComentario, List<string> tags)
        {
            if (productoId == -1)
            {
                throw new InstanceNotFoundException(productoId, "producto");
            }
            Comentario comentario = Comentario.CreateComentario(-1, textoComentario, DateTime.Now, usrId, productoId);
            comentario.UserProfile = UserProfileDao.Find(usrId);
            ComentarioDao.Create(comentario);
            List<Etiqueta> etiquetas = new List<Etiqueta>();

            if (tags != null )
            {
                int num = tags.Count();

                for (int i = 0; i < num; i++)
                {
                    etiquetas.Add(AddEtiqueta(tags[i]));
                    Etiquetar(comentario.comentarioId, etiquetas[i].etiquetaId);
                }
            }
            else
            {
                etiquetas = null;
            }
            ComentarioEtiquetaBlock comEtiBlock = new ComentarioEtiquetaBlock(comentario, etiquetas);
            return comEtiBlock;

        }

        /// <exception cref="InstanceNotFoundException">If comentarioId doesn't match an existing Comentario</exception>
        public void ModifyComentarioAndEtiqueta(long comentarioId, string texto,List<Etiqueta> etiquetasFinales)
        {
            Comentario comentario = ComentarioDao.Find(comentarioId);
            comentario.texto = texto;
            ComentarioDao.Update(comentario);
            if (etiquetasFinales != null)
            {
                      foreach (Etiqueta e in etiquetasFinales)
                      {
                          if (!comentario.Etiquetas.Contains(e))
                          {
                              Etiquetar(comentarioId, e.etiquetaId);
                          }
                      }
                      EntityCollection<Etiqueta> etiquetasComentario = new EntityCollection<Etiqueta>(); // Al eliminar etiquetas de los comentarios la lista referenciada de etiquetas fallaba.
                          foreach (Etiqueta e in etiquetasComentario.Union(comentario.Etiquetas).ToList())
                      {
                          if (!etiquetasFinales.Contains(e))
                          {
                            Desetiquetar(comentarioId, e.etiquetaId);
                          }
                      }
                  }
        }

        /// <exception cref="InstanceNotFoundException">If comentarioId doesn't match an existing Comentario</exception>
        public void DeleteComentario(long comentarioId)
        {
            ComentarioDao.Remove(comentarioId);
        }

        public List<Comentario> FindComentariosByProductoId(long productoId, int startIndex, int count)
        {
            return ComentarioDao.FindByProductoId(productoId, startIndex, count);
        }

        public int GetNumberOfComentariosByProductoId(long productoId)
        {
            return ComentarioDao.GetNumberByProductoId(productoId);
        }

        public List<Comentario> FindComentariosByEtiqueta(string tag,int start_index, int count)
        {
            Etiqueta et = EtiquetaDao.FindByTag(tag);
            et.Comentarios.Load();
            List<Comentario> comments = new List<Comentario>(et.Comentarios);
            int commentsLength = comments.Count();
            if (count > commentsLength)
            {
                count = commentsLength;
            }
            return comments.GetRange(start_index, count);
        }

       /// <exception cref="InstanceNotFoundException">If usrId doesn't match an existing UserProfile</exception>
        public Valoracion ValorarUsuario(string vendedorId, long usrId, int voto, string text)
        {
            Valoracion valoracion = Valoracion.CreateValoracion(-1, voto, DateTime.Now, usrId, vendedorId);
            if (text != null)
            {
                valoracion.comentario = text;
            }
            valoracion.UserProfile = UserProfileDao.Find(usrId);
            ValoracionDao.Create(valoracion);
            return valoracion;
        }

        /// <exception cref="InstanceNotFoundException">If no Valoracion exist for the given vendedor identifier</exception>
        public ValoracionBlock FindValoracionesAndNoteByVendedor(string vendedorId, int startIndex, int count)
        {
            List<Valoracion> valoraciones = ValoracionDao.FindByVendedorId(vendedorId, startIndex, count);
            int numberValoraciones = ValoracionDao.GetNumberByVendedorId(vendedorId);
            if (ValoracionDao.GetNumberByVendedorId(vendedorId) == 0)
            {
                throw new InstanceNotFoundException(vendedorId, "Valoracion");
            }
            double note = ValoracionDao.GetValoracionMediaByVendedorId(vendedorId);
            ValoracionBlock valoracionBlock = new ValoracionBlock(valoraciones, numberValoraciones,note);
            return valoracionBlock; 
        }

     
        /// <exception cref="InstanceNotFoundException">If usrId doesn't match an existing UserProfile</exception>
        public Favorito AddFavorito(long usrId, long productoId, string bookmark, string comentario)
        {
            Favorito favorito = Favorito.CreateFavorito(-1, bookmark, DateTime.Now, comentario, usrId, productoId);
            favorito.UserProfile = UserProfileDao.Find(usrId);
            FavoritoDao.Create(favorito);
            return favorito;       
        }

        public List<Favorito> FindFavoritosByUsrId(long usrId, int startIndex, int count)
        {
            return FavoritoDao.FindByUsrId(usrId, startIndex,count);
        }

        /// <exception cref=InstanceNotFoundException">If not exist favorito</exception>
        public Favorito RemoveFavorito(long usrId, long favoritoId)
        {
            int start_index = 0;
            int count = 20;
            List<Favorito> favoritos = FavoritoDao.FindByUsrId(usrId, start_index, count);
            Favorito favorito = FavoritoDao.Find(favoritoId);
            int numFavo = FavoritoDao.GetNumberByUsrId(usrId);
            while (numFavo < count)
            {
                if (favoritos.Contains(favorito))
                {
                    FavoritoDao.Remove(favorito.favoritoId);
                    return favorito;
                }
                else
                {
                    start_index = start_index + count;
                    count = count + count;
                    favoritos = FavoritoDao.FindByUsrId(usrId, start_index, count);
                }
            }
            throw new InstanceNotFoundException(favorito.favoritoId, "favorito");

        }

        public int GetNumberOfFavoritosByUsrId(long usrId)
        {
            return FavoritoDao.GetNumberByUsrId(usrId);
        }

        private Etiqueta AddEtiqueta(string tag)
        {
            Etiqueta etiquetaExistente = EtiquetaDao.FindByTag(tag);
            if (etiquetaExistente != null)
                return etiquetaExistente;
            Etiqueta etiqueta = Etiqueta.CreateEtiqueta(-1,tag,0);
            EtiquetaDao.Create(etiqueta);
            return etiqueta;
        }

        /// <exception cref="InstanceNotFoundException">If comentarioId doesn't match an existing Comentario or if etiquetaId doesn't match an existing Etiqueta</exception>
        private void Etiquetar(long comentarioId, long etiquetaId)
        {
            Comentario comentario = ComentarioDao.Find(comentarioId);
            Etiqueta etiqueta = EtiquetaDao.Find(etiquetaId);
            if (!comentario.Etiquetas.Contains(etiqueta)) 
            {
                etiqueta.ocurrencias = etiqueta.ocurrencias + 1;
                comentario.Etiquetas.Add(etiqueta);
                etiqueta.Comentarios.Add(comentario);
                EtiquetaDao.Update(etiqueta);
                ComentarioDao.Update(comentario);
                
            }
        }


        /// <exception cref="InstanceNotFoundException">If comentarioId doesn't match an existing Comentario or if etiquetaId doesn't match an existing Etiqueta</exception>
        private void Desetiquetar(long comentarioId, long etiquetaId)
        {
            Comentario comentario = ComentarioDao.Find(comentarioId);
            Etiqueta etiqueta = EtiquetaDao.Find(etiquetaId);
            

            if (comentario.Etiquetas.Contains(etiqueta))
            {
                comentario.Etiquetas.Remove(etiqueta);
            }
            if (etiqueta.ocurrencias >= 0)
            {
                etiqueta.ocurrencias = etiqueta.ocurrencias - 1;
            }
            etiqueta.Comentarios.Remove(comentario);
            EtiquetaDao.Update(etiqueta);
            ComentarioDao.Update(comentario);
        }
        private void Desetiquetar(Comentario comentario, Etiqueta etiqueta)
        {
            if (etiqueta.ocurrencias >= 0)
            {
                etiqueta.ocurrencias = etiqueta.ocurrencias - 1;
            }

            comentario.Etiquetas.Remove(etiqueta);
            etiqueta.Comentarios.Remove(comentario);
            EtiquetaDao.Update(etiqueta);
            ComentarioDao.Update(comentario);
        }

        public List<Etiqueta> FindEtiquetas()
        {
            return EtiquetaDao.FindEtiquetas();
        }

        public List<Etiqueta> FindFrequentEtiquetas()
        {
            List<Etiqueta> etiquetas = EtiquetaDao.FindEtiquetas();
            List<Etiqueta> sortedEtiquetas = etiquetas.OrderByDescending(o => o.ocurrencias).ToList();
            List<Etiqueta> frequentEtiq = new List<Etiqueta>();
            int mostFreq = 15;

            if (sortedEtiquetas.Count() < mostFreq)
            {
                mostFreq = sortedEtiquetas.Count();
            }
            
                for (int i =0;i<mostFreq; i++)
                {
                    if (sortedEtiquetas[i].ocurrencias > 0)
                    {
                        frequentEtiq.Add(sortedEtiquetas[i]);
                   }
                }

            return frequentEtiq;

        }


    }
}
