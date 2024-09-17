using Tarea2;

namespace Tarea2

{
    [TestClass]
    public class ListaDobleTests
    {


        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetMiddle_ListaVacia()
        {
            // Arrange
            var lista = new ListaDoble();

            // Act & Assert
            lista.GetMiddle(); // Debe lanzar una excepción porque la lista está vacía
        }
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetMiddle_ListaNula()
        {
            // Arrange
            ListaDoble lista = null;

            // Act & Assert
            lista.GetMiddle();
        }

        [TestMethod]

        public void GetMiddle_Correcto()
        {
            // Arrange
            ListaDoble lista = new ListaDoble();
            lista.InsertInOrder(1);
            // Act & Assert
            Assert.AreEqual(1, lista.GetMiddle());
        }
        [TestMethod]
        public void GetMiddle_Correcto2()
        {
            // Arrange
            ListaDoble lista = new ListaDoble();
            lista.InsertInOrder(1);
            lista.InsertInOrder(2);
            // Act & Assert
            Assert.AreEqual(2, lista.GetMiddle());
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void InvertNull()
        {
            // Arrange
            ListaDoble lista = null;

            // Act
            lista.Invert();

            
        }
        [TestMethod]
        public void InvertVacia()
        {
            // Arrange
            ListaDoble lista = new ListaDoble();

            // Act
            lista.Invert();

            Assert.AreEqual(0, lista.tamaño);

        }

        [TestMethod]
        public void InvertCorrecto()
        {
            // Arrange
            ListaDoble lista = new ListaDoble();
            lista.AddLast(1);
            lista.AddLast(0);
            lista.AddLast(30);
            lista.AddLast(50);
            lista.AddLast(2);
            // Act
            lista.Invert();

            Assert.AreEqual(2, lista.cabeza.Valor);

        }

        [TestMethod]
        public void InvertCorrecto2()
        {
            // Arrange
            ListaDoble lista = new ListaDoble();
            lista.AddLast(2);
            
            // Act
            lista.Invert();

            Assert.AreEqual(2, lista.cabeza.Valor);

        }
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void MergeSorted_Null()
        {
            ListaDoble listA = null;
            ListaDoble listB = new ListaDoble();
            listB.AddLast(1);
            listB.AddLast(2);
            listA.MergeSorted(listA, listB, SortDirection.Ascendente);

        }
        [TestMethod]
        public void MergeSorted_CombineDosListasAscendente()
        {
            // Arrange
            var listaA = new ListaDoble();
            listaA.InsertInOrder(0);
            listaA.InsertInOrder(2);
            listaA.InsertInOrder(6);
            listaA.InsertInOrder(10);
            listaA.InsertInOrder(25);

            var listaB = new ListaDoble();
            listaB.InsertInOrder(3);
            listaB.InsertInOrder(7);
            listaB.InsertInOrder(11);
            listaB.InsertInOrder(40);
            listaB.InsertInOrder(50);

            var listaC = new ListaDoble();
            listaC.InsertInOrder(0);
            listaC.InsertInOrder(2);
            listaC.InsertInOrder(3);
            listaC.InsertInOrder(6);
            listaC.InsertInOrder(7);
            listaC.InsertInOrder(10);
            listaC.InsertInOrder(11);
            listaC.InsertInOrder(25);
            listaC.InsertInOrder(40);
            listaC.InsertInOrder(50);


            // Act
            listaA.MergeSorted(listaA, listaB, SortDirection.Ascendente);


            // Assert
            Assert.AreEqual(listaA.cabeza.Valor, listaC.cabeza.Valor);

        }
        [TestMethod]
        
        public void MergeSorted_CombineDosListasAVaciaAscendente()
        {
            // Arrange
            var listaA = new ListaDoble();

            var listaB = new ListaDoble();
            listaB.InsertInOrder(9);
            listaB.InsertInOrder(40);
            listaB.InsertInOrder(50);

            // Act
            listaA.MergeSorted(listaA, listaB, SortDirection.Descendente);

            Assert.AreEqual(0,listaA.tamaño);
           

        }
        [TestMethod]
        public void MergeSorted_CombineDosListaBVaciaAscendente()
        {
            // Arrange
            var listaA = new ListaDoble();
            listaA.InsertInOrder(10);
            listaA.InsertInOrder(15);
            var listaB = new ListaDoble();
            

            // Act
            listaA.MergeSorted(listaA, listaB, SortDirection.Ascendente);


            // Assert
            Assert.AreEqual(10, listaA.cabeza.Valor);

        }



        [TestMethod]
        public void MergeSorted_CombineDosListasDescendente()
        {
            // Arrange
            var listaA = new ListaDoble();
            listaA.InsertInOrder(10);
            listaA.InsertInOrder(15);

            var listaB = new ListaDoble();
            listaB.InsertInOrder(9);
            listaB.InsertInOrder(40);
            listaB.InsertInOrder(50);

            var listaC = new ListaDoble();
            listaC.InsertInOrder(50);
            listaC.InsertInOrder(40);
            listaC.InsertInOrder(15);
            listaC.InsertInOrder(10);
            listaC.InsertInOrder(9);

            listaC.Invert();

            listaA.MergeSorted(listaA, listaB, SortDirection.Descendente);




            // Assert
            Assert.AreEqual(15, listaA.GetMiddle());
        }
        
    }
}
