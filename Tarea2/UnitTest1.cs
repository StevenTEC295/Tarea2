using Tarea2;

namespace Tarea2

{
    [TestClass]
    public class ListaDobleTests
    {


        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetMiddle_ListaVacia_DebeLanzarExcepcion()
        {
            // Arrange
            var lista = new ListaDoble();

            // Act & Assert
            lista.GetMiddle(); // Debe lanzar una excepción porque la lista está vacía
        }

        [TestMethod]
        public void Invert_InvierteLaListaCorrectamente()
        {
            // Arrange
            var lista = new ListaDoble();
            lista.InsertInOrder(1);
            lista.InsertInOrder(2);
            lista.InsertInOrder(3);

            // Act
            lista.Invert();

            // Assert
            Assert.AreEqual(2, lista.GetMiddle(), "Después de invertir, el valor medio debería ser 2");
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
            //var resultado = listaA.MergeSorted(listaA, listaB, SortDirection.Ascendente);


            // Assert
            //Assert.AreEqual(resultado.GetMiddle(), listaC.GetMiddle());

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





            // Assert
            //Assert.AreEqual(resultado.cabeza, listaC.cabeza);
        }

    }
}
