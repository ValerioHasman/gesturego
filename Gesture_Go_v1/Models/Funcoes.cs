using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Gesture_Go_v1.Models
{
    public class Funcoes
    {
        public static string HashTexto(string texto, string nomeHash)
        {
            if (texto == null)
            {
                return "";
            }
            else
            {
                HashAlgorithm algoritmo = HashAlgorithm.Create(nomeHash);
                if (algoritmo == null)
                {
                    throw new ArgumentException("Nome de Hash incorreto", "nomeHash");
                }
                byte[] hash = algoritmo.ComputeHash(Encoding.UTF8.GetBytes(texto));
                return Convert.ToBase64String(hash);
            }

        }

        public static List<T> Randomize<T>(List<T> list)
        {
            List<T> randomizedList = new List<T>();
            Random rnd = new Random();
            while (list.Count > 0)
            {
                int index = rnd.Next(0, list.Count); //pick a random item from the master list
                randomizedList.Add(list[index]); //place it at the end of the randomized list
                list.RemoveAt(index);
            }
            return randomizedList;
        }

        public static int ConverteSegudos(DateTime tempo)
        {
            int segundos = 0; 
            segundos += tempo.Second;
            segundos += tempo.Minute * 60;
            segundos += tempo.Hour * 3600;

            return segundos;
        }

    }

}
