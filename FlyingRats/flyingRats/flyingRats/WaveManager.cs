using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace flyingRats
{
    public class WaveManager
    {
        public List<string> Mapas;
        public string NomeMapa;
        public int WaveAtual;
        public int TempoBeforeWave;
        public int QuantidadeInimigos;
        public int InimigosRestantes;
        public int InimigosNecessariosParaPassar;
        public int TempoEntreSpawns;
        public ScaringDefense game;

        public WaveManager(ContentManager Content, ScaringDefense aGame)
        {
            
            this.game = aGame;
            string PathTilesFloor = Path.Combine(Content.RootDirectory, "Maps");
            //Mapas.AddRange(Directory.GetFiles(PathTilesFloor).Where(f => f.Contains(".mha")));
            
            WaveAtual = 1;
            TempoBeforeWave = 5; //segundos
            QuantidadeInimigos = 10;
            InimigosNecessariosParaPassar = 7;
            TempoEntreSpawns = 5;
        }

        public void VerificaWave()
        {
        }
    }
}
