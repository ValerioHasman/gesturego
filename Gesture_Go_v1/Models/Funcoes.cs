using System;
using System.Collections.Generic;
using System.IO;
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

        //upload funções
        public static bool CriarDiretorio()
        {
            string dir = HttpContext.Current.Request.PhysicalApplicationPath + "Uploads\\";
            if (!Directory.Exists(dir))
            {
                //Caso não exista devermos criar
                Directory.CreateDirectory(dir);
                return true;
            }
            else
                return false;
        }

        public static bool ExcluirArquivo(string arq)
        {
            if (File.Exists(arq))
            {
                File.Delete(arq);
                return true;
            }
            else
                return false;
        }

        public static string UploadArquivo(HttpPostedFileBase flpUpload, string nome)
        {
            try
            {
                double permitido = 2000;
                if (flpUpload != null)
                {
                    string arq = Path.GetFileName(flpUpload.FileName);
                    double tamanho = Convert.ToDouble(flpUpload.ContentLength) / 1024;
                    string extensao = Path.GetExtension(flpUpload.FileName).ToLower();
                    string diretorio = HttpContext.Current.Request.PhysicalApplicationPath + "Uploads\\" + nome;
                    if (tamanho > permitido)
                        return "Tamanho Máximo permitido é de " + permitido + " kb!";
                    else if ((extensao != ".png" && extensao != ".jpg"))
                        return "Extensão inválida, só são permitidas .png e .jpg!";
                    else
                    {
                        flpUpload.SaveAs(diretorio);
                        return "sucesso";
                    }
                }
                else
                    return "Erro no Upload!";
            }
            catch { 
                return "Erro no Upload"; 
            }

        }

    }

}
