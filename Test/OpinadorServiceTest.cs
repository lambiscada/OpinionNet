using Es.Udc.DotNet.PracticaIS.Model.OpinadorService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Es.Udc.DotNet.PracticaIS.Model;
using System.Collections.Generic;
using System.Xml;
using Es.Udc.DotNet.PracticaIS.Model.ComentarioDao;
using Es.Udc.DotNet.PracticaIS.Model.EtiquetaDao;
using Es.Udc.DotNet.PracticaIS.Model.FavoritoDao;
using Es.Udc.DotNet.PracticaIS.Model.UserProfileDao;
using Es.Udc.DotNet.PracticaIS.Model.ValoracionDao;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System.Configuration;
using System.Transactions;
using Es.Udc.DotNet.PracticaIS.Model.UserService.Util;
using Es.Udc.DotNet.PracticaIS.Test;
using Es.Udc.DotNet.ModelUtil.Exceptions;

namespace Es.Udc.DotNet.PracticaIS.Test
{
    
    
    /// <summary>
    ///Se trata de una clase de prueba para OpinadorServiceTest y se pretende que
    ///contenga todas las pruebas unitarias OpinadorServiceTest.
    ///</summary>
    [TestClass()]
    public class OpinadorServiceTest
    {
        private TestContext testContextInstance;
        private static IUnityContainer container;
        private static IOpinadorService opinadorService;
        TransactionScope transaction;

        private static IUserProfileDao userProfileDao;
        private static IComentarioDao comentarioDao;
        private static IEtiquetaDao etiquetaDao;
        private static IValoracionDao valoracionDao;
        private static IFavoritoDao favoritoDao;

        private const long NON_EXISTENT_ID = -1;
        private const int START_INDEX = 0;
        private const int COUNT = 10;
        private const long PRODUCTO_ID = 0;
        private const string VENDEDOR_ID = "maria";
        private const string NON_EXISTENT_VENDEDOR_ID = "non_existent_vendedor_id";


        /// <summary>
        ///Obtiene o establece el contexto de la prueba que proporciona
        ///la información y funcionalidad para la ejecución de pruebas actual.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Atributos de prueba adicionales


        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {

            container = TestManager.ConfigureUnityContainer("unity");

            opinadorService = container.Resolve<IOpinadorService>();
            userProfileDao = container.Resolve<IUserProfileDao>();
            valoracionDao = container.Resolve<IValoracionDao>();
            comentarioDao = container.Resolve<IComentarioDao>();
            etiquetaDao = container.Resolve<IEtiquetaDao>();
            favoritoDao = container.Resolve<IFavoritoDao>();

        }

        //Use ClassCleanup para ejecutar código después de haber ejecutado todas las pruebas en una clase
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            TestManager.ClearUnityContainer(container);
        }

        //Use TestInitialize para ejecutar código antes de ejecutar cada prueba
        [TestInitialize()]
        public void MyTestInitialize()
        {
            transaction = new TransactionScope();
        }

        // Use TestCleanup para ejecutar código después de que se hayan ejecutado todas las pruebas
        [TestCleanup()]
        public void MyTestCleanup()
        {
            transaction.Dispose();
        }

        #endregion

        [TestMethod]
        public void AddComentarioEtiquetaTest()
        {
            UserProfile userProfile = CreateTestUserProfile();
            ComentarioEtiquetaBlock comEtiBlo = opinadorService.AddComentarioEtiqueta(userProfile.usrId, PRODUCTO_ID, "texto", null);

            Assert.IsTrue(opinadorService.FindComentariosByProductoId(PRODUCTO_ID, START_INDEX, COUNT).Contains(comEtiBlo.Comentario));
            Assert.IsTrue(opinadorService.GetNumberOfComentariosByProductoId(PRODUCTO_ID) == 1);
        }

        [TestMethod]
        public void AddComentarioEtiquetaRepetidaTest()
        {
            UserProfile userProfile = CreateTestUserProfile();
            List<String> tags = new List<string>();
            tags.Add("comedia");
            tags.Add("comedia");
            ComentarioEtiquetaBlock comEtiBlo = opinadorService.AddComentarioEtiqueta(userProfile.usrId, PRODUCTO_ID, "texto", tags);
            List<Etiqueta> etiquetas = opinadorService.FindEtiquetas();
            Assert.IsTrue(etiquetas.Count == 1);
        }
        [TestMethod]
        public void ModifyTextoComentarioTest()
        {
            UserProfile userProfile = CreateTestUserProfile();
            ComentarioEtiquetaBlock comentarioTest = CreateTestComentario(userProfile.usrId);

            opinadorService.ModifyComentarioAndEtiqueta(comentarioTest.Comentario.comentarioId, "Familia", null);
            Assert.AreEqual(comentarioDao.Find(comentarioTest.Comentario.comentarioId).texto, "Familia");

            opinadorService.ModifyComentarioAndEtiqueta(comentarioTest.Comentario.comentarioId, "Campo", null);
            Assert.AreEqual(comentarioDao.Find(comentarioTest.Comentario.comentarioId).texto, "Campo");

        }

        [TestMethod]
        public void ModifyEtiquetasComentarioTest()
        {
            UserProfile userProfile = CreateTestUserProfile();
            ComentarioEtiquetaBlock comentarioTest;
            List<String> tags = new List<String>();
            tags.Add("Perro");
            tags.Add("Motor");
            List<Etiqueta> etiquetas = new List<Etiqueta>();
            Etiqueta e1 = new Etiqueta();
            e1.tag = "Casa";
            etiquetaDao.Create(e1);
            etiquetas.Add(e1);
            Etiqueta e2 = new Etiqueta();
            e2.tag = "Familia";
            etiquetaDao.Create(e2);
            etiquetas.Add(e2);
            comentarioTest = opinadorService.AddComentarioEtiqueta(userProfile.usrId, PRODUCTO_ID, "Familia", tags);
            opinadorService.ModifyComentarioAndEtiqueta(comentarioTest.Comentario.comentarioId, "Familia", etiquetas);
            Comentario comentario = comentarioDao.Find(comentarioTest.Comentario.comentarioId);
            Assert.IsTrue(comentario.Etiquetas.Contains(e1));
            Assert.IsTrue(comentario.Etiquetas.Contains(e2));
            Assert.IsTrue(comentario.Etiquetas.Count == 2);
            etiquetas.Remove(e2);
            opinadorService.ModifyComentarioAndEtiqueta(comentarioTest.Comentario.comentarioId, "Hogar", etiquetas);
            comentario = comentarioDao.Find(comentarioTest.Comentario.comentarioId);
            Assert.IsTrue(comentario.Etiquetas.Count == 1);
            Assert.IsTrue(comentario.texto == "Hogar");
            Assert.IsFalse(comentario.Etiquetas.Contains(e2));
            Assert.IsTrue(comentario.Etiquetas.Contains(e1));
        }
        
        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void ModifyComentarioWithNonExistentComentarioIdTest()
        {
            opinadorService.ModifyComentarioAndEtiqueta(NON_EXISTENT_ID, "Familia", null);
        }
        
        [TestMethod]
        public void DeleteComentarioTest()
        {
            UserProfile userProfile = CreateTestUserProfile();
            ComentarioEtiquetaBlock comentarioTest = CreateTestComentario(userProfile.usrId);
            Assert.IsTrue(opinadorService.FindComentariosByProductoId(PRODUCTO_ID, START_INDEX, COUNT).Contains(comentarioTest.Comentario));
            Assert.IsTrue(opinadorService.GetNumberOfComentariosByProductoId(PRODUCTO_ID) == 1);
            opinadorService.DeleteComentario(comentarioTest.Comentario.comentarioId);
            Assert.IsFalse(opinadorService.FindComentariosByProductoId(PRODUCTO_ID, START_INDEX, COUNT).Contains(comentarioTest.Comentario));
            Assert.IsTrue(opinadorService.GetNumberOfComentariosByProductoId(PRODUCTO_ID) == 0);
        }

        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void DeleteComentarioWithNonExistentComentarioIdTest()
        {
            opinadorService.DeleteComentario(NON_EXISTENT_ID);
        }

        [TestMethod]
        public void FindComentariosByProductoIdTest()
        {
            UserProfile userProfile = CreateTestUserProfile();
            opinadorService.AddComentarioEtiqueta(userProfile.usrId, PRODUCTO_ID, "texto1", null);
            opinadorService.AddComentarioEtiqueta(userProfile.usrId, PRODUCTO_ID, "texto2", null);
            opinadorService.AddComentarioEtiqueta(userProfile.usrId, PRODUCTO_ID, "texto3", null);

            List<Comentario> comments = opinadorService.FindComentariosByProductoId(PRODUCTO_ID, 0, 2);
            Assert.IsTrue(comments.Count == 2);
            comments = opinadorService.FindComentariosByProductoId(PRODUCTO_ID, 2, 10);
            Assert.IsTrue(comments.Count == 1);
        }
       
        [TestMethod]
        public void FindComentariosByEtiquetaIdTest()
        {
            UserProfile userProfile = CreateTestUserProfile();
            List<String> l1 = new List<String>();
            l1.Add("Office");
            l1.Add("General");
            ComentarioEtiquetaBlock comEtiBlo = opinadorService.AddComentarioEtiqueta(userProfile.usrId, PRODUCTO_ID, "texto1", l1);
            opinadorService.AddComentarioEtiqueta(userProfile.usrId, PRODUCTO_ID, "texto2", l1);
            opinadorService.AddComentarioEtiqueta(userProfile.usrId, PRODUCTO_ID, "texto3", null);
           
            List<Comentario> comments = opinadorService.FindComentariosByEtiqueta("Office",START_INDEX,COUNT);
            Assert.IsTrue(comments.Count == 2);
           
        }
            
        
        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void FindComentariosByProductoIdWithNonExistentProductoIdTest()
        {
            UserProfile userProfile = CreateTestUserProfile();
            ComentarioEtiquetaBlock comentarioTest = opinadorService.AddComentarioEtiqueta(userProfile.usrId, NON_EXISTENT_ID, "texto", null);
                    
        }

        [TestMethod]
        public void GetNumberOfComentariosByProductoIdTest()
        {
            UserProfile userProfile = CreateTestUserProfile();
            ComentarioEtiquetaBlock comentarioTest = opinadorService.AddComentarioEtiqueta(userProfile.usrId, PRODUCTO_ID, "texto", null);

            Assert.IsTrue(opinadorService.GetNumberOfComentariosByProductoId(PRODUCTO_ID) == 1);
        }

        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void GetNumberOfComentariosByProductoIdWithNonExistentProductoIdTest()
        {
            UserProfile userProfile = CreateTestUserProfile();
            ComentarioEtiquetaBlock comentarioTest = opinadorService.AddComentarioEtiqueta(userProfile.usrId, NON_EXISTENT_ID, "texto", null);
                  
        }

        [TestMethod]
        public void FindValoracionesAndNoteByVendedorTest()
        {
            UserProfile userProfile = CreateTestUserProfile();
            Valoracion v1 = opinadorService.ValorarUsuario(VENDEDOR_ID, userProfile.usrId, 2, "voto1");
            opinadorService.ValorarUsuario(VENDEDOR_ID, userProfile.usrId, 4, "voto2");
            opinadorService.ValorarUsuario(VENDEDOR_ID, userProfile.usrId, 3, "voto3");

            ValoracionBlock valoracionBlock = opinadorService.FindValoracionesAndNoteByVendedor(VENDEDOR_ID, START_INDEX, COUNT);
            Assert.IsTrue(valoracionBlock.NumValoraciones == 3);
            Assert.IsTrue(valoracionBlock.AverageValoracion == 3);
            Assert.IsTrue(valoracionBlock.Valoraciones.Contains(v1));
        }

   
        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void FindValoracionesByVendedorWithNonExistentVendedorIdTest()
        {
            UserProfile userProfile = CreateTestUserProfile();
            Valoracion valoracion = CreateTestValoracion(userProfile.usrId, 4);
            Valoracion valoracion1 = CreateTestValoracion(userProfile.usrId, 3);

            ValoracionBlock valoracionesBlock = opinadorService.FindValoracionesAndNoteByVendedor(NON_EXISTENT_VENDEDOR_ID, START_INDEX, COUNT);
            Assert.IsTrue(valoracionesBlock.Valoraciones.Count == 0);
            Assert.IsFalse(valoracionesBlock.Valoraciones.Contains(valoracion));
            Assert.IsFalse(valoracionesBlock.Valoraciones.Contains(valoracion1));
        }

        [TestMethod]
        public void GetNumberOfValoracionesByVendedorTest()
        {
            UserProfile userProfile = CreateTestUserProfile();
            Valoracion valoracion = CreateTestValoracion(userProfile.usrId, 4);
            Valoracion valoracion1 = CreateTestValoracion(userProfile.usrId, 3);

            Assert.IsTrue(opinadorService.FindValoracionesAndNoteByVendedor(VENDEDOR_ID, START_INDEX, COUNT).NumValoraciones == 2);
        }

        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void GetNumberOfValoracionesByVendedorWithNonExistentVendedorIdTest()
        {
            UserProfile userProfile = CreateTestUserProfile();
            Valoracion valoracion = CreateTestValoracion(userProfile.usrId, 4);
            Valoracion valoracion1 = CreateTestValoracion(userProfile.usrId, 3);

            Assert.IsTrue(opinadorService.FindValoracionesAndNoteByVendedor(NON_EXISTENT_VENDEDOR_ID, START_INDEX, COUNT).NumValoraciones == 0);
        }

        [TestMethod]
        public void GetValoracionMediaByVendedorTest()
        {
            UserProfile userProfile = CreateTestUserProfile();
            Valoracion valoracion = CreateTestValoracion(userProfile.usrId, 4);
            Valoracion valoracion1 = CreateTestValoracion(userProfile.usrId, 3);
            double valoracionMedia = opinadorService.FindValoracionesAndNoteByVendedor(VENDEDOR_ID,START_INDEX,COUNT).AverageValoracion;
            Assert.AreEqual(valoracionMedia, 3.5);
        }
        
        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void GetValoracionMediaByVendedorWithNonExistentVendedorIdTest()
        {
            double valoracion = opinadorService.FindValoracionesAndNoteByVendedor(NON_EXISTENT_VENDEDOR_ID, START_INDEX, COUNT).AverageValoracion;
        }
        
        [TestMethod]
        public void ValorarUsuarioTest()
        {
            UserProfile userProfile = CreateTestUserProfile();

            Valoracion valoracion = opinadorService.ValorarUsuario(VENDEDOR_ID, userProfile.usrId, 4, "texto");
            List<Valoracion> valoraciones = opinadorService.FindValoracionesAndNoteByVendedor(VENDEDOR_ID, START_INDEX, COUNT).Valoraciones;
            Assert.IsTrue(valoraciones.Count == 1);
            Assert.IsTrue(valoraciones.Contains(valoracion));
        }

        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void ValorarUsuarioWithNonExistentUserProfileIdTest()
        {
            Valoracion valoracion = opinadorService.ValorarUsuario(VENDEDOR_ID, NON_EXISTENT_ID, 4, "texto");
        }

        [TestMethod]
        public void AddFavoritoTest()
        {
            UserProfile userProfile = CreateTestUserProfile();

            Favorito favorito = opinadorService.AddFavorito(userProfile.usrId, PRODUCTO_ID, "bookmark", "interesante");
            List<Favorito> favoritos = opinadorService.FindFavoritosByUsrId(userProfile.usrId, START_INDEX, COUNT);
            Assert.IsTrue(favoritos.Count == 1);
            Assert.IsTrue(favoritos.Contains(favorito));
        }

        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void AddFavoritoWithNonExistentUserProfileIdTest()
        {
            opinadorService.AddFavorito(NON_EXISTENT_ID, PRODUCTO_ID, "bookmark", "interesante");
        }

        [TestMethod]
        public void DeleteFavoritoTest()
        {
            UserProfile userProfile = CreateTestUserProfile();
            Favorito favorito1 = opinadorService.AddFavorito(userProfile.usrId, PRODUCTO_ID, "bookmark", "interesante");
            Favorito favorito2 = opinadorService.AddFavorito(userProfile.usrId, PRODUCTO_ID, "bookmark", "hogar");
            List<Favorito> favoritos = opinadorService.FindFavoritosByUsrId(userProfile.usrId, START_INDEX, COUNT);
            Assert.IsTrue(favoritos.Contains(favorito1));
            Assert.IsTrue(favoritos.Contains(favorito2));
            favoritos.Remove(favorito2);
            Assert.IsTrue(favoritos.Contains(favorito1));
            Assert.IsFalse(favoritos.Contains(favorito2));

        }


        [TestMethod]
        public void GetNumberOfFavoritosByUserProfileIdTest()
        {
            UserProfile userProfile = CreateTestUserProfile();

            Favorito favorito = opinadorService.AddFavorito(userProfile.usrId, PRODUCTO_ID, "bookmark", "interesante");
            Assert.IsTrue(opinadorService.GetNumberOfFavoritosByUsrId(userProfile.usrId) == 1);
        }

        [TestMethod]
        public void GetNumberOfFavoritosByUserProfileIdWithNonExistentUserProfileTest()
        {
            Assert.IsTrue(opinadorService.GetNumberOfFavoritosByUsrId(NON_EXISTENT_ID) == 0);
        }
   
        [TestMethod]
        public void EtiquetarDuplicateTest()
        {
            UserProfile userProfile = CreateTestUserProfile();
            Etiqueta etiquetaTest = new Etiqueta();
            List<String> list = new List<String> ();
            list.Add("test");
            etiquetaTest.tag = "test";
            etiquetaDao.Create(etiquetaTest);
            List<Etiqueta> et = new List<Etiqueta>();
            et.Add(etiquetaTest);
            ComentarioEtiquetaBlock comentarioTest = opinadorService.AddComentarioEtiqueta(userProfile.usrId,PRODUCTO_ID,"",list);
            opinadorService.ModifyComentarioAndEtiqueta(comentarioTest.Comentario.comentarioId, "", et);
            opinadorService.ModifyComentarioAndEtiqueta(comentarioTest.Comentario.comentarioId, "", et);
            Assert.IsTrue(comentarioTest.Etiquetas.Contains(etiquetaTest));
            Assert.IsTrue(etiquetaTest.ocurrencias == 1);
        }

       [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void EtiquetarWithNonExistentComentarioIdTest()
        {
           List<String> tags = new List<String> ();
           tags.Add("Prueba");
           opinadorService.AddComentarioEtiqueta(NON_EXISTENT_ID, PRODUCTO_ID, "", tags);
        }
   
        [TestMethod]
        public void FindEtiquetasFrecuentesTest()
        {
            UserProfile userProfile = CreateTestUserProfile();
            int numEtiquetas = 20;
            List<String> tags = new List<String>();
            for (int i = 0; i < numEtiquetas; i++)
            {
                tags.Add("Tag" + i);
            }
            ComentarioEtiquetaBlock  comentarioTest = opinadorService.AddComentarioEtiqueta(userProfile.usrId, PRODUCTO_ID, "texto", tags);
            ComentarioEtiquetaBlock comentarioTest1 = opinadorService.AddComentarioEtiqueta(userProfile.usrId, PRODUCTO_ID, "texto", tags);
            ComentarioEtiquetaBlock comentarioTest2 = opinadorService.AddComentarioEtiqueta(userProfile.usrId, PRODUCTO_ID, "texto", tags);
            ComentarioEtiquetaBlock comentarioTest3 = opinadorService.AddComentarioEtiqueta(userProfile.usrId, PRODUCTO_ID, "texto", tags);
         
            List<Etiqueta> etiquetas = opinadorService.FindEtiquetas();
            List<Etiqueta> etiFre = new List<Etiqueta>();
            etiFre =  etiquetas.GetRange(0,14);
            opinadorService.ModifyComentarioAndEtiqueta(comentarioTest.Comentario.comentarioId, "texto", etiFre);
            opinadorService.ModifyComentarioAndEtiqueta(comentarioTest1.Comentario.comentarioId, "texto", etiFre);
            opinadorService.ModifyComentarioAndEtiqueta(comentarioTest2.Comentario.comentarioId, "texto", etiFre);
            List<Etiqueta> etiquetasFrecuentes = opinadorService.FindFrequentEtiquetas();
            Assert.IsTrue(etiquetasFrecuentes.Count == 15);
            for (int i = 0; i < 15; i++)
            {
                Assert.IsTrue(etiquetasFrecuentes.Contains(etiquetas[i]));
            }
            Assert.IsFalse(etiquetasFrecuentes.Contains(etiquetas[16]));
        }


        private UserProfile CreateTestUserProfile()
        {
            UserProfile userProfile = UserProfile.CreateUserProfile(-1, "john", "john", "john", "game", "john@game", "es", "ES");
            userProfileDao.Create(userProfile);
            return userProfile;
        }

        private ComentarioEtiquetaBlock CreateTestComentario(long usrId)
        {
            return opinadorService.AddComentarioEtiqueta(usrId, PRODUCTO_ID, "texto", null);
        }

        private Valoracion CreateTestValoracion(long usrId, int voto)
        {
            return opinadorService.ValorarUsuario(VENDEDOR_ID, usrId, voto, "texto");
        }

     
    }
}
