using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.IO;
using SndLib;
using flyingRats.RunSetup;
using System.Threading;

namespace flyingRats
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class ScaringDefense : Microsoft.Xna.Framework.Game
    {
        public const bool CO_DEBUG_MODE = true;

        public Consts.TelaJogo TelaAtual;

        public int Dinheiro = 0;
        public int Score = 0;
        public int ScoreInterno = 0;

        public SpriteFont sfScore;
        public SpriteFont sfDetalhes;
        public SpriteFont sfMenu;

        int DefaultWidth = 1920;
        int DefaultHeight = 1080;

        int ActualWidth = 960;
        int ActualHeight = 540;

        int TipoVitima = 0;

        public WaveManager manager;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Texture2D texFundo;

        public Texture2D texVida;
        public Texture2D texMulher;
        public Texture2D texHomem;
        public Texture2D texMistery;
        public Texture2D texOverlay;
        public Texture2D texLogo;
        public Texture2D texCredits;
        public Texture2D texGameOver;

        public Rectangle recCredits;
        public Rectangle recLogo;
        public Rectangle recGameOver;
        public Rectangle recIniciar;
        public Rectangle recCreditos;

        public Rectangle recMapa;

        public Barra Bar;
        public ScarMouse sMouse;
        public TrapIco SelectedTrapIco;
        public Level lvlAtual;
        public Dictionary<string, Texture2D> texTiles;
        public Dictionary<string, Texture2D> texEffects;
        public Dictionary<string, Texture2D> dicTexMulher;
        public Dictionary<string, Texture2D> dicTexHomem;
        public Dictionary<string, Texture2D> dicTexHomem2;
        public Dictionary<string, Texture2D> dicVampiro;
        public Dictionary<string, Texture2D> dicMumia;
        public Dictionary<string, Texture2D> dicFantasma;
        public Dictionary<string, Texture2D> dicMistery;
        public Dictionary<string, SndAudio> dicAudio;
        List<string> ListaLevels;
        public List<Pesant> Wave;
        public MouseState msOldMouseState;
        RenderTarget2D rt;
        bool FechouoJogo = false;
        public string GameVersion = string.Empty;

        public ScaringDefense(RunParameters rp)
        {
            
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            InicializarResolucao(graphics, rp);
            GameVersion = rp.GameVersion;

            ListaLevels = new List<string>();
            ListaLevels.Add("map_001.mha");
            ListaLevels.Add("map_002.mha");
            ListaLevels.Add("map_003.mha");
            ListaLevels.Add("map_004.mha");
            ListaLevels.Add("map_005.mha");
            ListaLevels.Add("map_006.mha");

            SndEngine.VaiSom();
        }

        private void InicializaTelas()
        {
            texCredits = Content.Load<Texture2D>(@"Textures\Telas\Credits");
            texLogo = Content.Load<Texture2D>(@"Textures\Telas\abertura");
            texGameOver = Content.Load<Texture2D>(@"Textures\Telas\GameOverTexto");
            sfScore = Content.Load<SpriteFont>(@"Fonte\sfPlacar");
            sfDetalhes = Content.Load<SpriteFont>(@"Fonte\sfDetalhes");
            sfMenu = Content.Load<SpriteFont>(@"Fonte\sfMenu");

            TelaAtual = Consts.TelaJogo.Inicio;
            recLogo = texLogo.Bounds;
            recGameOver = texGameOver.Bounds;
            recIniciar = new Rectangle(0, 0, 120, 90);
            recCreditos = new Rectangle(0, 0, 120, 90); ;

            texOverlay = new Texture2D(graphics.GraphicsDevice, 1, 1);
            texOverlay.SetData(new Color[] { Color.Red });


            recLogo.X += ((DefaultWidth - recLogo.Width) / 2);
            recGameOver.X += ((DefaultWidth - recGameOver.Width) / 2);
            recIniciar.X += ((DefaultWidth - recIniciar.Width) / 2);
            recCreditos.X += ((DefaultWidth - recCreditos.Width) / 2) - 20;

            recLogo.Y += ((DefaultHeight - recLogo.Height) / 2);
            recGameOver.Y += ((DefaultHeight - recGameOver.Height) / 2);

            recIniciar.Y = 700;
            recCreditos.Y = recIniciar.Y + 200;

            recCredits = texCredits.Bounds;
            recCredits.X = (DefaultWidth - texCredits.Bounds.Width) / 2;
            recCredits.Y = (DefaultHeight - texCredits.Bounds.Height) / 2;
        }

        #region Resolucao

        private void InicializarResolucao(GraphicsDeviceManager graphics, RunParameters rp)
        {
            // Setar resolução
            ActualWidth = rp.Width;
            ActualHeight = rp.Height;
            // Passar para o XNA
            graphics.PreferredBackBufferWidth = ActualWidth; // DefaultWidth;
            graphics.PreferredBackBufferHeight = ActualHeight;// DefaultHeight;
            graphics.IsFullScreen = rp.Fullscreen;
        }

        #endregion

        private Dictionary<string, Texture2D> loadAllFromFolder(string folder)
        {
            Dictionary<string, Texture2D> dic = new Dictionary<string, Texture2D>();
            string PathTilesFloor = Path.Combine(Content.RootDirectory, folder);
            foreach (String img in Directory.GetFiles(PathTilesFloor).Where(f => f.Contains(".xnb")))
            {
                string fileName = Path.GetFileNameWithoutExtension(img).ToLower();
                dic.Add(fileName, Content.Load<Texture2D>(Path.Combine(folder, fileName)));
            }
            return dic;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            InicializaTelas();

            //manager = new WaveManager(Content);
            Dinheiro = 400;
            Score = 0;
            ScoreInterno = 0;
            AuxLevelAtual = 0;
            FechouoJogo = false;

            #region Load all dictionaries

            // Textures
            texTiles = loadAllFromFolder(@"Textures\Tiles\Floor");
            texEffects = loadAllFromFolder(@"Textures\Tiles\Effects");
            dicTexMulher = loadAllFromFolder(@"Textures\Tiles\Pesants\Mulher");
            dicTexHomem = loadAllFromFolder(@"Textures\Tiles\Pesants\Homem");
            dicTexHomem2 = loadAllFromFolder(@"Textures\Tiles\Pesants\Homem2");
            dicVampiro = loadAllFromFolder(@"Textures\Tiles\Traps\Vampiro");
            dicMumia = loadAllFromFolder(@"Textures\Tiles\Traps\Mumia");
            dicFantasma = loadAllFromFolder(@"Textures\Tiles\Traps\Fantasma");

            texMistery = Content.Load<Texture2D>(@"Textures\Tiles\Traps\Mistery\icone");

            // Audios
            dicAudio = new Dictionary<string, SndAudio>();
            string PathTilesFloor = Path.Combine(Content.RootDirectory, @"SFX");
            foreach (String f in Directory.GetFiles(PathTilesFloor).Where(f => f.Contains(".mp3")))
            {
                string fileName = Path.GetFileNameWithoutExtension(f).ToLower();
                SndAudio snd = new SndAudio();
                snd.CarregaAiDJ(f);
                dicAudio.Add(fileName, snd);
            }

            #endregion

            #region Rects

            recMapa = new Rectangle(0, 0, DefaultWidth, 840);
            #endregion

            #region Tex

            texVida = new Texture2D(graphics.GraphicsDevice, 1, 1);
            texVida.SetData(new Color[] { Color.Red });

            texFundo = new Texture2D(graphics.GraphicsDevice, 1, 1);
            texFundo.SetData(new Color[] { Color.Black });
            #endregion

            #region Carrega Barra

            Bar = new Barra(DefaultWidth, DefaultHeight, graphics, Content, this);
            sMouse = new ScarMouse(Content, this);
            #endregion

            AtualizarLevel();
            lvlAtual.levelData.spawnTime = 10;
        }

        public void AtualizarLevel()
        {
            IniciarAguardar(1000);
            lvlAtual = new Level(ListaLevels[AuxLevelAtual], this);
            Wave = new List<Pesant>();
            dicAudio["menu-01"].SomNaCaixaDJ();
        }

        bool Aguardar = false;
        double TempoAguardar = 0;
        private void IniciarAguardar(int tempo)
        {
            TempoAguardar = tempo;
            Aguardar = true;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            rt = new RenderTarget2D(graphics.GraphicsDevice, DefaultWidth, DefaultHeight);
            // TODO: use this.Content to load your game content here
        }
        bool Cheater = false;
        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            SndEngine.DeuDeSom();
        }

        int ultSecs = -1;
        public int AuxLevelAtual;
        KeyboardState ksOldKeyboardState;
        bool Paused = false;
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            SndEngine.ContinuaTocandoPorra();

            if (Aguardar)
            {
                if (TempoAguardar <= 0)
                {
                    Aguardar = false;
                    TempoAguardar = 0;
                    return;
                }

                TempoAguardar -= gameTime.ElapsedGameTime.TotalMilliseconds;
                return;
            }

            KeyboardState ksActualKeyboardState = Keyboard.GetState();
            MouseState msActualMouseState = Mouse.GetState();
            // Allows the game to exit
            // FAKE MOUSE
            if (DefaultWidth != ActualWidth || DefaultHeight != ActualHeight)
            {
                MouseState fakeMouse = new MouseState(
                    (int)(((float)msActualMouseState.X / (float)ActualWidth) * (float)DefaultWidth),
                    (int)(((float)msActualMouseState.Y / (float)ActualHeight) * (float)DefaultHeight),
                    msActualMouseState.ScrollWheelValue,
                    msActualMouseState.LeftButton,
                    msActualMouseState.MiddleButton,
                    msActualMouseState.RightButton,
                    msActualMouseState.XButton1,
                    msActualMouseState.XButton2
                    );
                msActualMouseState = fakeMouse;
            }
            if (ksOldKeyboardState != ksActualKeyboardState)
            {
                if (ksActualKeyboardState.IsKeyDown(Keys.Escape))
                {
                    if (TelaAtual == Consts.TelaJogo.Inicio)
                    {
                        this.Exit();
                    }
                    else if (TelaAtual == Consts.TelaJogo.GameOver)
                    {
                        Initialize(); //Jessé corrigir isso futuramente.
                        TelaAtual = Consts.TelaJogo.Inicio;
                    }
                    else
                    {
                        Initialize(); 
                        TelaAtual = Consts.TelaJogo.Inicio;
                        ksOldKeyboardState = ksActualKeyboardState;
                        return;
                    }
                }

                if (ksActualKeyboardState.IsKeyDown(Keys.Space))
                {
                    Paused = !Paused;
                }
            }

            if (Paused)
            {
                ksOldKeyboardState = ksActualKeyboardState;
                return;
            }



            if (TelaAtual == Consts.TelaJogo.Creditos || TelaAtual == Consts.TelaJogo.GameOver)
            {
                sMouse.Selected = false;
                sMouse.BackMouseTexture();
                sMouse.UpdateMouseForMenu(gameTime, msActualMouseState, msOldMouseState);
            }
            else if (TelaAtual == Consts.TelaJogo.Inicio)
            {
                sMouse.Selected = false;
                sMouse.BackMouseTexture();
                sMouse.UpdateMouseForMenu(gameTime, msActualMouseState, msOldMouseState);

                if (msActualMouseState.LeftButton == ButtonState.Pressed)
                {
                    if (sMouse.recMouse.Intersects(recIniciar))
                    {
                        TelaAtual = Consts.TelaJogo.Jogo;
                    }
                    else if (sMouse.recMouse.Intersects(recCreditos))
                    {
                        TelaAtual = Consts.TelaJogo.Creditos;
                    }
                }

                ultSecs = 10;
            }
            else if (TelaAtual == Consts.TelaJogo.Jogo)
            {
                sMouse.UpdateMouse(gameTime, msActualMouseState, msOldMouseState);

                if (ksOldKeyboardState != ksActualKeyboardState)
                {
                    bool mudouFase = false;
                    if (ksActualKeyboardState.IsKeyDown(Keys.PageDown))
                    {
                        AuxLevelAtual++;
                        if (AuxLevelAtual >= ListaLevels.Count)
                        {
                            AuxLevelAtual = 0;
                        }
                        mudouFase = true;
                        Cheater = true;
                    }
                    else if (ksActualKeyboardState.IsKeyDown(Keys.PageUp))
                    {
                        AuxLevelAtual--;

                        if (AuxLevelAtual < 0)
                        {
                            AuxLevelAtual = ListaLevels.Count - 1;
                        }

                        mudouFase = true;
                        Cheater = true;
                    }

                    if (mudouFase)
                    {
                        AtualizarLevel();
                    }
                }
                if (Dinheiro < 0)
                {
                    TelaAtual = Consts.TelaJogo.GameOver;
                }
                else if (!Cheater)
                {
                    //Controla as fases de modo tosco:
                    switch (AuxLevelAtual)
                    {
                        case 0: //Vai pra 2
                            {
                                if (ScoreInterno >= 5)
                                {
                                    AuxLevelAtual++;
                                    AtualizarLevel();
                                    this.Dinheiro += AuxLevelAtual * 100;
                                    if (!FechouoJogo)
                                        lvlAtual.levelData.spawnTime = 8;
                                }
                                break;
                            }

                        case 1: //Vai pra 3
                            {
                                if (ScoreInterno >= 11)
                                {
                                    AuxLevelAtual++;
                                    this.Dinheiro += AuxLevelAtual * 100;
                                    AtualizarLevel();
                                    if (!FechouoJogo)
                                        lvlAtual.levelData.spawnTime = 6;
                                }
                                break;
                            }
                        case 2: //Vai pra 4
                            {
                                if (ScoreInterno >= 18)
                                {
                                    AuxLevelAtual++;
                                    this.Dinheiro += AuxLevelAtual * 100;
                                    AtualizarLevel();
                                    if (!FechouoJogo)
                                        lvlAtual.levelData.spawnTime = 4;
                                }
                                break;
                            }
                        case 3: //Vai pra 5
                            {
                                if (ScoreInterno >= 28)
                                {
                                    AuxLevelAtual++;
                                    this.Dinheiro += AuxLevelAtual * 100;
                                    AtualizarLevel();
                                    if (!FechouoJogo)
                                        lvlAtual.levelData.spawnTime = 3;
                                }
                                break;
                            }
                        case 4: //5 -> Vai pra 6
                            {
                                if (ScoreInterno >= 38)
                                {
                                    AuxLevelAtual++;
                                    this.Dinheiro += AuxLevelAtual * 100;
                                    AtualizarLevel();
                                    if (FechouoJogo)
                                        lvlAtual.levelData.spawnTime = 2;
                                }
                                break;
                            }
                        case 5: //6 -> Vai pra 1
                            {
                                if (ScoreInterno >= 51)
                                {
                                    ScoreInterno = 0;
                                    this.Dinheiro += 600;
                                    AuxLevelAtual = 0;
                                    AtualizarLevel();
                                    FechouoJogo = true;
                                }
                                break;
                            }
                    }

                }


                lvlAtual.Update(gameTime);

                int secs = (int)(gameTime.TotalGameTime.Seconds);

                if (ultSecs != secs && secs % lvlAtual.levelData.spawnTime == 1)
                {
                    ultSecs = secs;

                    if (lvlAtual.TheSpawnPoint != null)
                    {
                        Wave.Add(new Pesant((Consts.TipoPesant)TipoVitima, lvlAtual.TheSpawnPoint.PosicaoCentral, lvlAtual.TheSpawnPoint.SentidoAtual, this));
                        TipoVitima++;
                        if (TipoVitima > Enum.GetNames(typeof(Consts.TipoPesant)).Count() - 1)
                        {
                            TipoVitima = 0;
                        }      
                    }
                }



                //Dinheiro += gameTime.TotalGameTime.Seconds * 2;
                List<Pesant> PeseantRemover = new List<Pesant>();
                foreach (Pesant p in Wave)
                {
                    if (p.Update(gameTime))
                    {
                        PeseantRemover.Add(p);
                    }
                }

                foreach (Pesant p in PeseantRemover)
                {
                    Wave.Remove(p);
                }

                // TODO: Add your update logic here
            }

            msOldMouseState = msActualMouseState;
            ksOldKeyboardState = ksActualKeyboardState;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(rt);
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            if (TelaAtual == Consts.TelaJogo.Inicio)
            {
                spriteBatch.Draw(texLogo, recLogo, Color.White);
                spriteBatch.DrawString(sfMenu, "Start", new Vector2(recIniciar.X, recIniciar.Y), Color.White);
                spriteBatch.DrawString(sfMenu, "Credits", new Vector2(recCreditos.X, recCreditos.Y), Color.White);
            }
            else if (TelaAtual == Consts.TelaJogo.Creditos)
            {
                spriteBatch.Draw(texCredits, recCredits, Color.White);
                spriteBatch.DrawString(sfDetalhes, string.Format("Version: {0}", GameVersion), new Vector2(5, DefaultHeight-22), Color.DarkGray);
            }
            else if (TelaAtual == Consts.TelaJogo.GameOver)
            {
                spriteBatch.Draw(texGameOver, recGameOver, Color.White);
            }
            else if (TelaAtual == Consts.TelaJogo.Jogo)
            {
                //Desenha o Fundo
                spriteBatch.Draw(texFundo, recMapa, Color.White);

                //Desenha o Mapa
                lvlAtual.Draw(gameTime, spriteBatch);

                //Pessoas
                foreach (Pesant p in Wave)
                {
                    p.Draw(gameTime, spriteBatch);
                }

                //Desenha os Detalhes da Barra
                Bar.Draw(gameTime, spriteBatch);
            }

            //Desenha o Mouse
            sMouse.Draw(gameTime, spriteBatch);

            spriteBatch.End();

            Rectangle rect = new Rectangle(0, 0, ActualWidth, ActualHeight);
            Texture2D tex = (Texture2D)rt;
            GraphicsDevice.SetRenderTarget(null);


            spriteBatch.Begin();
            spriteBatch.Draw(tex, rect, Color.White);
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);

        }
    }
}
