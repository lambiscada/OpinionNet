using System;
using Microsoft.Practices.Unity;
using System.Collections.Generic;

using Es.Udc.DotNet.PracticaIS.Model.ComentarioDao;
using Es.Udc.DotNet.PracticaIS.Model.EtiquetaDao;
using Es.Udc.DotNet.PracticaIS.Model.FavoritoDao;
using Es.Udc.DotNet.PracticaIS.Model.UserProfileDao;
using Es.Udc.DotNet.PracticaIS.Model.ValoracionDao;
using Es.Udc.DotNet.PracticaIS.Model.UserService.Exceptions;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using System.Xml;

namespace Es.Udc.DotNet.PracticaIS.Model.OpinadorService
{
    public interface IOpinadorService
    {

        [Dependency]
        IComentarioDao ComentarioDao { set;}

        [Dependency]
        IEtiquetaDao EtiquetaDao { set; }

        [Dependency]
        IFavoritoDao FavoritoDao { set;}

        [Dependency]
        IValoracionDao ValoracionDao { set; }

        [Dependency]
        IUserProfileDao UserProfileDao { set;}

        /// <summary>
        ///Returns a XML Document with all the productos matching with a given producto name.
        /// </summary>
        /// <param name="nombreproducto">the key to find the products</param>
        ///  /// <param name="startIndex">where to start</param>
        ///<returns>the Xml document</returns>
        XmlDocument FindProductos(String nombreProducto,int startIndex);

        ComentarioEtiquetaBlock AddComentarioEtiqueta(long usrId, long productoId, string textoComentario, List<string> tags);


        /// <summary>
        /// Modifies the text pertaining to a given Comentario.
        /// </summary>
        /// <param name="comentarioId">the Comentario identifier</param>
        /// <param name="text">the new comment's text</param>
        /// <param name="etiquetasFinales">the final Etiquetas</param>
        /// <exception cref="InstanceNotFoundException">If comentarioId doesn't match an existing Comentario</exception>
        [Transactional]
        void ModifyComentarioAndEtiqueta(long comentarioId, string texto, List<Etiqueta> etiquetasFinales);
        

        /// <summary>
        /// Deletes the Comentario matching the given comentario identifier.
        /// </summary>
        /// <param name="comentarioId">the Comentario identifier</param>
        /// <exception cref="InstanceNotFoundException">If comentarioId doesn't match an existing Comentario</exception>
        [Transactional]
        void DeleteComentario(long comentarioId);

        /// <summary>
        /// Returns a list of Comentario pertaining to a given producto identifier within the given interval.
        /// </summary>
        /// <param name="productoId">the producto identifier</param>
        /// <param name="startIndex">the start index for the search interval</param>
        /// <param name="count">the total number of objects to be retrieved</param>
        /// <returns>a list of Comentario for the given conditions</returns>
        List<Comentario> FindComentariosByProductoId(long productoId, int startIndex, int count);

        /// <summary>
        /// Returns the number of comentarios pertaining to a given producto.
        /// </summary>
        /// <param name="productoId">the producto identifier</param>
        /// <returns>the number of comentarios</returns>
        int GetNumberOfComentariosByProductoId(long productoId);

        /// <summary>
        /// Returns a list of Comentario pertaining to a given producto identifier within the given interval.
        /// </summary>
        /// <param name="productoId">the producto identifier</param>
        /// <param name="startIndex">the start index for the search interval</param>
        /// <param name="count">the total number of objects to be retrieved</param>
        /// <returns>a list of Comentario for the given conditions</returns>
        List<Comentario> FindComentariosByEtiqueta(string etiqueta, int startIndex, int count);


        /// <summary>
        /// Creates a user's new Valoracion of a given vendedor.
        /// </summary>
        /// <param name="vendedorId">the vendedor identifier</param>
        /// <param name="usrId">the UserProfile identifier</param>
        /// <param name="voto">the vote value</param>
        /// <param name="text">the Valoracion's comment</param>
        /// <returns>the Valoracion</returns>
        /// <exception cref="InstanceNotFoundException">If usrId doesn't match an existing UserProfile</exception>
        [Transactional]
        Valoracion ValorarUsuario(string vendedorId, long usrId, int voto, string text);

        /// <summary>
        /// Returns a list of Valoracion pertaining to a given vendedor identifier within the given interval,
        /// number of Valoraciones and the average note.
        /// </summary>
        /// <param name="vendedorId">the vendedor identifier</param>
        /// <param name="startIndex">the start index for the search interval</param>
        /// <param name="count">the total number of objects to be retrieved</param>
        /// <returns>a list of Valoracion for the given conditions, the number and average of valoraciones</returns>
        ValoracionBlock FindValoracionesAndNoteByVendedor(string vendedorId, int startIndex, int count);         

        /// <summary>
        /// Adds a user's new Favorito for a given producto.
        /// </summary>
        /// <param name="usrId">the UserProfile identifier</param>
        /// <param name="productoId">the Producto identifier</param>
        /// <param name="bookmark">a short text used to remember quickly the Favorito</param>
        /// <param name="comentario">a comment for the new Favorito</param>
        /// <returns>the Favorito</returns>
        /// <exception cref="InstanceNotFoundException">If usrId doesn't match an existing UserProfile</exception>
        [Transactional]
        Favorito AddFavorito(long usrId, long productoId, string bookmark, string comentario);

        /// <summary>
        /// Returns a list of Favorito pertaining to a given UserProfile identifier within the given interval.
        /// </summary>
        /// <param name="usrId">the UserProfile identifier</param>
        /// <param name="startIndex">the start index for the search interval</param>
        /// <param name="count">the total number of objects to be retrieved</param>
        /// <returns>a list of Favorito for the given conditions</returns>
        List<Favorito> FindFavoritosByUsrId(long usrId, int startIndex, int count);

        /// <summary>
        /// Returns the deleted favorito.
        /// </summary>
        /// <param name="usrId">the UserProfile identifier</param>
        /// <param name="count">the idenfier of the favorito to be remove</param>
        /// <returns>the deleted favoritos</returns>
        Favorito RemoveFavorito(long usrId, long favoritoId);

        /// <summary>
        /// Returns the number of favoritos pertaining to a given user.
        /// </summary>
        /// <param name="userId">the user identifier</param>
        /// <returns>the number of favoritos</returns>
        int GetNumberOfFavoritosByUsrId(long usrId);

        /// <summary>
        /// Returns the list with all Etiquetas.
        /// </summary>
        /// <returns>a list with all Etiquetas</returns>
        List<Etiqueta> FindEtiquetas();

        /// <summary>
        /// Returns the list with the 15 most frequent Etiquetas.
        /// </summary>
        /// <returns>a list with the 15 most frequent Etiquetas</returns>
        List<Etiqueta> FindFrequentEtiquetas();
        
    }
}
