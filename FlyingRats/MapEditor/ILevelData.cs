using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace flyingRats.Interfaces
{
    interface ILevelData
    {
        public int Altura;
        public int Largura;
        public int TileSize;
        List<string> NomesTiles;        
        Int32[,] MatrixTiles;
        Int32[,] MatrixTEventos;

        public void CarregarDataFromFile(string s);
    }

    public enum TipoEvento: int
    {
        None = 0,
        SpawnPoint = 1,
        EndPoint = 2,         
        Direita = 4,
        Baixo = 8,
        Esquerda = 16,
        Cima = 32,
    }
}
