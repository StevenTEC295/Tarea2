using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Tarea2;

namespace Tarea2

{
    public class Nodo
    {
        public int Valor { get; set; }
        public Nodo Siguiente { get; set; }
        public Nodo Anterior { get; set; }

        public Nodo(int valor)
        {
            Valor = valor;
        }
    }
    public class ListaDoble : IList
    {
        

        public Nodo cabeza;
        public Nodo cola;
        public Nodo medio;  // Referencia para manejar el nodo central
        public int tamaño;  // Contador de nodos

        public ListaDoble()
        {
            cabeza = null;
            cola = null;
            medio = null;
            tamaño = 0;
        }

        public void AddLast(int value)
        {
            Nodo newNode = new Nodo(value);
            if (cola == null)
            {
                cabeza = cola = newNode;
            }
            else
            {
                cola.Siguiente = newNode;
                newNode.Anterior = cola;
                cola = newNode;
            }
        }

        // Método para insertar en orden ascendente
        public void InsertInOrder(int value)
        {
            Nodo nuevo = new Nodo(value);
            if (cabeza == null) // Lista vacía
            {
                cabeza = cola = medio = nuevo;
            }
            else if (value < cabeza.Valor) // Insertar al inicio
            {
                nuevo.Siguiente = cabeza;
                cabeza.Anterior = nuevo;
                cabeza = nuevo;
            }
            else if (value >= cola.Valor) // Insertar al final
            {
                nuevo.Anterior = cola;
                cola.Siguiente = nuevo;
                cola = nuevo;
            }
            else // Insertar en el medio
            {
                Nodo actual = cabeza;
                while (actual.Siguiente != null && actual.Siguiente.Valor < value)
                {
                    actual = actual.Siguiente;
                }
                nuevo.Siguiente = actual.Siguiente;
                nuevo.Anterior = actual;
                if (actual.Siguiente != null)
                {
                    actual.Siguiente.Anterior = nuevo;
                }
                actual.Siguiente = nuevo;
            }

            tamaño++;
            ActualizarMedio();
        }

        // Método para eliminar el primer nodo
        public int DeleteFirst()
        {
            if (cabeza == null)
                throw new InvalidOperationException("La lista está vacía.");

            int valor = cabeza.Valor;
            if (cabeza == cola) // Un solo elemento
            {
                cabeza = cola = medio = null;
            }
            else
            {
                cabeza = cabeza.Siguiente;
                cabeza.Anterior = null;
            }

            tamaño--;
            ActualizarMedio();
            return valor;
        }

        // Método para eliminar el último nodo
        public int DeleteLast()
        {
            if (cola == null)
                throw new InvalidOperationException("La lista está vacía.");

            int valor = cola.Valor;
            if (cabeza == cola) // Un solo elemento
            {
                cabeza = cola = medio = null;
            }
            else
            {
                cola = cola.Anterior;
                cola.Siguiente = null;
            }

            tamaño--;
            ActualizarMedio();
            return valor;
        }

        // Método para eliminar un valor específico
        public bool DeleteValue(int value)
        {
            if (cabeza == null)
                return false;

            Nodo actual = cabeza;
            while (actual != null && actual.Valor != value)
            {
                actual = actual.Siguiente;
            }

            if (actual == null) // No encontrado
                return false;

            if (actual == cabeza)
                DeleteFirst();
            else if (actual == cola)
                DeleteLast();
            else // Eliminar nodo en el medio
            {
                actual.Anterior.Siguiente = actual.Siguiente;
                actual.Siguiente.Anterior = actual.Anterior;
            }

            tamaño--;
            ActualizarMedio();
            return true;
        }

        // Método para obtener el nodo central
        public int GetMiddle()
        {
            if (cabeza == null)
                throw new InvalidOperationException("La lista está vacía.");

            return medio.Valor;
        }

        // Método para mezclar dos listas ordenadas
        public ListaDoble MergeSorted(ListaDoble listA, ListaDoble listB, SortDirection direction)
        {
            ListaDoble result = new ListaDoble();
            if (listA.tamaño<0 || listB.tamaño<0) 
            
            {
                

                Nodo currentA = listA.cabeza;
                Nodo currentB = listB.cabeza;

                // Fusionar en orden ascendente
                if (direction == SortDirection.Ascendente)
                {
                    while (currentA != null && currentB != null)
                    {
                        if (currentA.Valor <= currentB.Valor)
                        {
                            result.AddLast(currentA.Valor);
                            currentA = currentA.Siguiente;
                        }
                        else
                        {
                            result.AddLast(currentB.Valor);
                            currentB = currentB.Siguiente;
                        }
                    }
                }
                // Fusionar en orden descendente
                else if (direction == SortDirection.Descendente)
                {
                    while (currentA != null && currentB != null)
                    {
                        if (currentA.Valor >= currentB.Valor)
                        {
                            result.AddLast(currentA.Valor);
                            currentA = currentA.Siguiente;
                        }
                        else
                        {
                            result.AddLast(currentB.Valor);
                            currentB = currentB.Siguiente;
                        }
                    }
                }

                // Si quedan elementos en listA
                while (currentA != null)
                {
                    result.AddLast(currentA.Valor);
                    currentA = currentA.Siguiente;
                }

                // Si quedan elementos en listB
                while (currentB != null)
                {
                    result.AddLast(currentB.Valor);
                    currentB = currentB.Siguiente;
                }
                return result;


            }
            
            return result;
            


        }
            


        /* // Método auxiliar para invertir la lista
         private static ListaDoble Reverse(ListaDoble list)
         {
             ListaDoble reversedList = new ListaDoble();
             Nodo current = list.cola;
             while (current != null)
             {
                 reversedList.AddLast(current.Valor);
                 current = current.Anterior;
             }
             return reversedList;
         }*/
        // Método para invertir la lista
        public void Invert()
        {
            if (cabeza == null)
                return;

            Nodo actual = cabeza;
            Nodo temp = null;

            while (actual != null)
            {
                temp = actual.Anterior;
                actual.Anterior = actual.Siguiente;
                actual.Siguiente = temp;
                actual = actual.Anterior; // Moverse hacia el "siguiente", que es el anterior en la lista invertida
            }

            // Ajustar cabeza y cola
            if (temp != null)
            {
                cabeza = temp.Anterior;
            }

            ActualizarMedio();
        }

        // Método para actualizar la referencia al nodo medio
        private void ActualizarMedio()
        {
            if (tamaño == 0)
            {
                medio = null;
                return;
            }

            Nodo actual = cabeza;
            int pasos = tamaño / 2;
            for (int i = 0; i < pasos; i++)
            {
                actual = actual.Siguiente;
            }
            medio = actual;
        }
    }

}
