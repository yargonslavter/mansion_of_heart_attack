using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using flyingRats.Interfaces;
using System.IO;

namespace flyingRats
{
    public class Level
    {
        public ScaringDefense game;
        public LevelData levelData;
        public List<EventPoint> eventHolderList;
        public List<EventPoint> eventList;
        public List<Trap> trapList; //Armadilhas ocupadas.
        public EventPoint TheSpawnPoint;
        public EventPoint TheEndPoint;

        public Level(string FileName, ScaringDefense aGame)
        {
            //".\\Content\\Maps\\Mapa_beta.txt"
            game = aGame;
            levelData = new LevelData();
            levelData.CarregarDataFromFileB(@"Content\Maps\" + FileName);
            //levelData.spawnTime = 10;
            levelData.TileSize = 120;

            eventList = new List<EventPoint>();
            eventHolderList = new List<EventPoint>();
            trapList = new List<Trap>();

            Dictionary<string, Consts.Eventos> dicEventos = new Dictionary<string, Consts.Eventos>();
                        
            foreach(string nomeEnumerador in Enum.GetNames(typeof(Consts.Eventos)))
            {
                dicEventos.Add(nomeEnumerador.ToLower(), (Consts.Eventos)Enum.Parse(typeof(Consts.Eventos), nomeEnumerador));
            }
            
            for (int x = 0; x < levelData.Largura; x++)
            {
                for (int y = 0; y < levelData.Altura; y++)
                {
                    if (levelData.MatrixEventos[x, y] > 0)
                    {
                        int i = levelData.MatrixEventos[x, y];
                        string TileName = Path.GetFileNameWithoutExtension(levelData.NomesEventos[i]).ToLower();

                        if (dicEventos.ContainsKey(TileName))
                        {
                            EventPoint p = new EventPoint();

                            p.tipoEvento = dicEventos[TileName];


                            switch (p.tipoEvento)
                            {
                                case Consts.Eventos.Baixo:
                                case Consts.Eventos.SpawnPoint_Baixo:
                                case Consts.Eventos.PlaceHolder_Baixo:
                                    {
                                        p.Sentidos.Add(Consts.Sentido.Baixo);
                                        p.SentidoAtual = Consts.Sentido.Baixo;
                                        break;
                                    }
                                case Consts.Eventos.Cima:
                                case Consts.Eventos.SpawnPoint_Cima:
                                case Consts.Eventos.PlaceHolder_Cima:
                                    {
                                        p.Sentidos.Add(Consts.Sentido.Cima);
                                        p.SentidoAtual = Consts.Sentido.Cima;
                                        break;
                                    }
                                case Consts.Eventos.Esquerda:
                                case Consts.Eventos.SpawnPoint_Esq:
                                case Consts.Eventos.PlaceHolder_Esq:
                                    {
                                        p.Sentidos.Add(Consts.Sentido.Esquerda);
                                        p.SentidoAtual = Consts.Sentido.Esquerda;
                                        break;
                                    }
                                case Consts.Eventos.Direita:
                                case Consts.Eventos.SpawnPoint_Dir:
                                case Consts.Eventos.PlaceHolder_Dir:
                                    {
                                        p.Sentidos.Add(Consts.Sentido.Direita);
                                        p.SentidoAtual = Consts.Sentido.Direita;
                                        break;
                                    }
                                case Consts.Eventos.bifurcacaoL1:
                                    {
                                        p.Sentidos.Add(Consts.Sentido.Esquerda);
                                        p.Sentidos.Add(Consts.Sentido.Baixo);
                                        break;
                                    }
                                case Consts.Eventos.bifurcacaoL2:
                                    {
                                        p.Sentidos.Add(Consts.Sentido.Baixo);
                                        p.Sentidos.Add(Consts.Sentido.Direita);
                                        break;
                                    }
                                case Consts.Eventos.bifurcacaoL3:
                                    {
                                        p.Sentidos.Add(Consts.Sentido.Direita);
                                        p.Sentidos.Add(Consts.Sentido.Cima);
                                        break;
                                    }
                                case Consts.Eventos.bifurcacaoL4:
                                    {
                                        p.Sentidos.Add(Consts.Sentido.Cima);
                                        p.Sentidos.Add(Consts.Sentido.Esquerda);
                                        break;
                                    }
                                default:
                                    {
                                        p.Sentidos.Add(Consts.Sentido.None);
                                        break;
                                    }
                            }
                            
                            if (p.tipoEvento == Consts.Eventos.Baixo ||
                                p.tipoEvento == Consts.Eventos.Cima ||
                                p.tipoEvento == Consts.Eventos.Esquerda ||
                                p.tipoEvento == Consts.Eventos.Direita)
                            {
                                int QuarterSize = levelData.TileSize / 4;
                                p.Posicao = new Vector2(levelData.TileSize * x, game.recMapa.Y + (levelData.TileSize * y));
                                p.recEventPoint = new Rectangle((int)p.Posicao.X + QuarterSize, (int)p.Posicao.Y + QuarterSize, levelData.TileSize / 2, levelData.TileSize / 2);
                                p.PosicaoCentral = new Vector2(p.Posicao.X + (levelData.TileSize / 2), p.Posicao.Y + (levelData.TileSize / 2));
                            }
                            else 
                            {
                                p.Posicao = new Vector2(levelData.TileSize * x, game.recMapa.Y + (levelData.TileSize * y));
                                p.recEventPoint = new Rectangle((int)p.Posicao.X , (int)p.Posicao.Y , levelData.TileSize , levelData.TileSize );
                                p.PosicaoCentral = new Vector2(p.Posicao.X + (levelData.TileSize / 2), p.Posicao.Y + (levelData.TileSize / 2));
                            }

                            
                            if (p.tipoEvento == Consts.Eventos.EndPoint)
                            {
                                TheEndPoint = p;
                            }
                            else if (p.tipoEvento == Consts.Eventos.SpawnPoint_Baixo ||
                                    p.tipoEvento == Consts.Eventos.SpawnPoint_Cima ||
                                    p.tipoEvento == Consts.Eventos.SpawnPoint_Dir ||
                                    p.tipoEvento == Consts.Eventos.SpawnPoint_Esq)
                            {
                                TheSpawnPoint = p;
                            }
                            else if (p.tipoEvento == Consts.Eventos.PlaceHolder_Baixo ||
                                p.tipoEvento == Consts.Eventos.PlaceHolder_Cima ||
                                p.tipoEvento == Consts.Eventos.PlaceHolder_Dir ||
                                p.tipoEvento == Consts.Eventos.PlaceHolder_Esq)
                            {                                
                                eventHolderList.Add(p);
                            }
                            else
                            {
                                eventList.Add(p);
                            }
                        }
                        /*
                        if (game.texTiles.ContainsKey(TileName))
                        {
                            Texture2D tex = game.texTiles[TileName];

                            Rectangle recPosicao = new Rectangle(levelData.TileSize * x, game.recMapa.Y + (levelData.TileSize * y), levelData.TileSize, levelData.TileSize);
                            spriteBatch.Draw(tex, recPosicao, Color.White);
                        }
                        */
                    }
                }
            }

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //Desenha o Floor
            for (int x = 0; x < levelData.Largura; x++)
            {
                for (int y = 0; y < levelData.Altura; y++)
                {
                    if (levelData.MatrixTiles[x, y] > 0)
                    {
                        string TileName = Path.GetFileNameWithoutExtension(levelData.NomesTiles[levelData.MatrixTiles[x, y]]).ToLower();
                        if (game.texTiles.ContainsKey(TileName))
                        {
                            Texture2D tex = game.texTiles[TileName];

                            Rectangle recPosicao = new Rectangle(levelData.TileSize * x, game.recMapa.Y + (levelData.TileSize * y), levelData.TileSize, levelData.TileSize);
                            spriteBatch.Draw(tex, recPosicao, Color.White);
                        }
                    }
                }
            }

            foreach (Trap atrap in trapList)
            {
                atrap.Draw(gameTime, spriteBatch);
            }

            //Desenha os placeholders destacados
            //desenha o destaque dos holders vazios
            if (game.sMouse.BeginDockTrap)
            {
                foreach (EventPoint holder in this.eventHolderList.Where(h => h.HolderOcupado == false))
                {
                    Texture2D tex = game.texEffects["holder_glow"];
                    spriteBatch.Draw(tex, holder.recEventPoint, Color.White);
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (Trap tr in trapList)
            {
                tr.Update(gameTime);
            }
        }
    }
}