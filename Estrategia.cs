
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Reflection;
using tp1;

namespace tpfinal
{
    public class Estrategia
    {
        public String Consulta1(List<string> datos)
        {
            string resutl = "Implementar";
            return resutl;
        }

        public String Consulta2(List<string> datos)
        {
            string result = "Implementar";
            return result;
        }

        public String Consulta3(List<string> datos)
        {
            string result = "Implementar";
            return result;
        }

        public void BuscarConOtro(List<string> datos, int cantidad, List<Dato> collected)
        {
            Dictionary<string, int> ocurrencias = new Dictionary<string, int>();
            foreach (string nombre in datos)
            {
                if (!ocurrencias.ContainsKey(nombre))
                    ocurrencias.Add(nombre, 1);
                else
                    ocurrencias[nombre]++;
            }

            List<Dato> listaOrdenada = new List<Dato>();
            foreach (var item in ocurrencias)
            {
                listaOrdenada.Add(new Dato(item.Value, item.Key));
            }

            SelectionSort(listaOrdenada);

            for (int i = 0; i < cantidad && i < listaOrdenada.Count; i++)
            {
                collected.Add(listaOrdenada[i]);
            }
        }

        private void SelectionSort(List<Dato> lista)
        {
            int n = lista.Count;
            for (int i = 0; i < n - 1; i++)
            {
                int maxIndex = i;
                for (int j = i + 1; j < n; j++)
                {
                    if (lista[j].ocurrencia > lista[maxIndex].ocurrencia)
                    {
                        maxIndex = j;
                    }
                }
                Dato temp = lista[maxIndex];
                lista[maxIndex] = lista[i];
                lista[i] = temp;
            }
        }

        public void BuscarConHeap(List<string> datos, int cantidad, List<Dato> collected)
        {
            Dictionary<string, int> ocurrencias = new Dictionary<string, int>();

            foreach (string nombre in datos)
            {
                if (!ocurrencias.ContainsKey(nombre))
                {
                    ocurrencias.Add(nombre, 1);
                }
                else
                {
                    ocurrencias[nombre]++;
                }
            }

            List<Dato> heap = new List<Dato>();

            foreach (var item in ocurrencias)
            {
                Dato dato = new Dato(item.Value, item.Key);
                heap.Add(dato);
                Subir(heap, heap.Count - 1);
            }

            for (int i = 0; i < cantidad && heap.Count > 0; i++)
            {
                collected.Add(ExtraerMaximo(heap));
            }
        }

        private void Subir(List<Dato> heap, int indice)
        {
            while (indice > 0)
            {
                int padre = (indice - 1) / 2;

                if (heap[indice].ocurrencia <= heap[padre].ocurrencia)
                {
                    break;
                }

                Intercambiar(heap, indice, padre);
                indice = padre;
            }
        }

        private void Bajar(List<Dato> heap, int indice)
        {
            int ultimoIndice = heap.Count - 1;

            while (true)
            {
                int izquierdo = 2 * indice + 1;
                int derecho = 2 * indice + 2;
                int mayor = indice;

                if (izquierdo <= ultimoIndice &&
                    heap[izquierdo].ocurrencia > heap[mayor].ocurrencia)
                {
                    mayor = izquierdo;
                }

                if (derecho <= ultimoIndice &&
                    heap[derecho].ocurrencia > heap[mayor].ocurrencia)
                {
                    mayor = derecho;
                }

                if (mayor == indice)
                {
                    break;
                }

                Intercambiar(heap, indice, mayor);
                indice = mayor;
            }
        }

        private Dato ExtraerMaximo(List<Dato> heap)
        {
            Dato max = heap[0];

            heap[0] = heap[heap.Count - 1];
            heap.RemoveAt(heap.Count - 1);

            if (heap.Count > 0)
            {
                Bajar(heap, 0);
            }

            return max;
        }

        private void Intercambiar(List<Dato> heap, int a, int b)
        {
            Dato temporal = heap[a];
            heap[a] = heap[b];
            heap[b] = temporal;
        }
    }
}