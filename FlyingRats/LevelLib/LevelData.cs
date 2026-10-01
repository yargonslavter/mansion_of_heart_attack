using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace flyingRats.Interfaces
{
    public class LevelData
    {
        public int spawnTime;
        public int Altura;
        public int Largura;
        public int TileSize;
        public List<string> NomesTiles = new List<string>();
        public List<string> NomesEventos = new List<string>();
        public Int32[,] MatrixTiles = new Int32[0, 0];
        public Int32[,] MatrixEventos = new Int32[0, 0];
        private string[] sep = new string[] { "," };

        public void CarregarDataFromFile(string s)
        {
            
            var lines = File.ReadAllLines(s);
            Altura = Convert.ToInt32(lines[0]);
            Largura = Convert.ToInt32(lines[1]);
            TileSize = Convert.ToInt32(lines[2]);
            //NomesTiles = lines[3].Split(sep, StringSplitOptions.None).ToList();
            //NomesTiles.Insert(0, string.Empty);

            //NomesEventos = lines[4].Split(sep, StringSplitOptions.None).ToList();
            //NomesEventos.Insert(0, string.Empty);

            MatrixTiles = new Int32[Largura, Altura];
            int x = 0,
                y = 0;
            var arr = lines[5].Split(sep, StringSplitOptions.None);
            foreach (var item in arr)
            {
                MatrixTiles[x, y++] = Convert.ToInt32(item);
                if (y == Altura)
                {
                    x++;
                    y = 0;
                }
            }

            MatrixEventos = new Int32[Largura, Altura];
            x = 0;
            y = 0;
            arr = lines[6].Split(sep, StringSplitOptions.None);
            foreach (var item in arr)
            {
                MatrixEventos[x, y++] = Convert.ToInt32(item);
                if (y == Altura)
                {
                    x++;
                    y = 0;
                }
            }            
        }


        public void CarregarDataFromFileB(string s)
        {

            var lines = File.ReadAllLines(s);
            Altura = Convert.ToInt32(lines[0]);
            Largura = Convert.ToInt32(lines[1]);
            TileSize = Convert.ToInt32(lines[2]);
            NomesTiles = lines[3].Split(sep, StringSplitOptions.None).ToList();
            NomesTiles.Insert(0, string.Empty);

            NomesEventos = lines[4].Split(sep, StringSplitOptions.None).ToList();
            NomesEventos.Insert(0, string.Empty);

            MatrixTiles = new Int32[Largura, Altura];
            int x = 0,
                y = 0;
            var arr = lines[5].Split(sep, StringSplitOptions.None);
            foreach (var item in arr)
            {
                MatrixTiles[x, y++] = Convert.ToInt32(item);
                if (y == Altura)
                {
                    x++;
                    y = 0;
                }
            }

            MatrixEventos = new Int32[Largura, Altura];
            x = 0;
            y = 0;
            arr = lines[6].Split(sep, StringSplitOptions.None);
            foreach (var item in arr)
            {
                MatrixEventos[x, y++] = Convert.ToInt32(item);
                if (y == Altura)
                {
                    x++;
                    y = 0;
                }
            }

        }

        public void SalvarDataToFile(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return;
            }
            //NomesTiles.Insert(0, string.Empty);

            File.WriteAllText(s, string.Format("{0}\r\n", Altura));
            File.AppendAllText(s, string.Format("{0}\r\n", Largura));
            File.AppendAllText(s, string.Format("{0}\r\n", TileSize));            
            File.AppendAllText(s, string.Join(",", NomesTiles) + "\r\n");            
            File.AppendAllText(s, string.Join(",", NomesEventos) + "\r\n");

            List<string> lstAux = new List<string>();
            foreach (var i in MatrixTiles)
            {
                lstAux.Add(i.ToString());
            }
            File.AppendAllText(s, string.Join(",", lstAux) + "\r\n");

            lstAux.Clear();
            foreach (var i in MatrixEventos)
            {
                lstAux.Add(i.ToString());
            }
            File.AppendAllText(s, string.Join(",", lstAux) + "\r\n");
        }
    }

    //public enum TipoEvento : int
    //{
    //    None = 0,
    //    SpawnPoint = 1,
    //    EndPoint = 2,
    //    Direita = 4,
    //    Baixo = 8,
    //    Esquerda = 16,
    //    Cima = 32,
    //}
}
