﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Tarea2;

namespace Tarea2
{
    public class ListaDoble : IList
    {
        private class Nodo
        {
            public int Valor { get; set; }
            public Nodo Siguiente { get; set; }
            public Nodo Anterior { get; set; }

            public Nodo(int valor)
            {
                Valor = valor;
            }
        }

        private Nodo cabeza;
        private Nodo cola;
        private Nodo medio;  // Referencia para manejar el nodo central
        private int tamaño;  // Contador de nodos

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
        public void MergeSorted(IList listA, IList listB, SortDirection direction)
        {
            if (listA == null || listB == null)
                throw new InvalidOperationException("Una o ambas listas son nulas.");
            // Implementar lógica de mezcla aquí (combinar listA y listB en la dirección indicada)
            Nodo currentA = ((ListaDoble)listA).cabeza;//acceder a la cabeza de listA
            Nodo currentB = ((ListaDoble)listB).cabeza;//acceder a la cabeza de listB

            ListaDoble mergedList = new ListaDoble();
            // Lógica para fusionar en orden ascendente
            while (currentA != null || currentB != null)
            {
                if (currentA == null) // Si lista A está vacía
                {
                    mergedList.InsertInOrder(currentB!.Valor);
                    currentB = currentB.Siguiente;
                }
                else if (currentB == null || (direction == SortDirection.Ascendente && currentA.Valor <= currentB.Valor) ||
                        (direction == SortDirection.Descendente && currentA.Valor >= currentB.Valor))
                {
                    mergedList.InsertInOrder(currentA!.Valor);
                    currentA = currentA.Siguiente;
                }
                else
                {
                    mergedList.InsertInOrder(currentB!.Valor);
                    currentB = currentB.Siguiente;
                }
            }

            // Actualiza listA con la lista fusionada
            ((ListaDoble)listA).cabeza = mergedList.cabeza;
            ((ListaDoble)listA).cola = mergedList.cola;
            ((ListaDoble)listA).tamaño = mergedList.tamaño;
            ((ListaDoble)listA).ActualizarMedio(); // Actualiza nodo central
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
