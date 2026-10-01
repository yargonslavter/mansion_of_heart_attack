using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace flyingRats
{
    public class Pesant
    {
        private Consts.Sentido _sentido;

        public ScaringDefense game;
        public float frequenciaCardiaca;
        public Consts.Sentido sentido
        {
            get { return _sentido; }
            set
            {
                _sentido = value;
            }
        }
        public Consts.TipoPesant tipo;
        public Vector2 VectorCenterPoint;
        public float Velocidade;
        public int Vida;
        public Sprite sprPesant;
        public Consts.PesantState State // fast conversion
        {
            get { return (Consts.PesantState)sprPesant.ActualKeyFrame; }
            set { sprPesant.ActualKeyFrame = (uint)value; }
        }

        public Rectangle recPivo
        {
            get
            {
                return new Rectangle((int)this.sprPesant.Position.X - 30, (int)this.sprPesant.Position.Y - 30, 60, 60);
            }
        }

        public Pesant(Consts.TipoPesant Tipo, Vector2 PosicaoInicial, Consts.Sentido SentidoInicial, ScaringDefense aGame)
        {
            Vida = 100;
            game = aGame;
            this.tipo = Tipo;
            this.sentido = SentidoInicial;
            this.VectorCenterPoint = new Vector2(30, 30);
            switch (tipo)
            {
                case Consts.TipoPesant.Mulher_1:
                    {
                        Velocidade = 120;
                        // sprite
                        sprPesant = new Sprite();
                        sprPesant.KeyFrames.Add(
                            (uint)Consts.PesantState.Walk_Up,
                            game.dicTexMulher["walk-up"],
                            4,
                            0, 1, 2, 3
                            );
                        sprPesant.KeyFrames.Add(
                            (uint)Consts.PesantState.Walk_Down,
                            game.dicTexMulher["walk-down"],
                            4,
                            0, 1, 2, 3
                            );
                        sprPesant.KeyFrames.Add(
                            (uint)Consts.PesantState.Walk_Left,
                            game.dicTexMulher["walk-left"],
                            4,
                            0, 1, 2, 3
                            );
                        sprPesant.KeyFrames.Add(
                            (uint)Consts.PesantState.Walk_Right,
                            game.dicTexMulher["walk-right"],
                            4,
                            0, 1, 2, 3
                            );
                        sprPesant.KeyFrames.Add(
                            (uint)Consts.PesantState.Hit,
                            game.dicTexMulher["hit"],
                            3,
                            0, 1, 2, 2, 2, 2
                            );
                        sprPesant.KeyFrames[(uint)Consts.PesantState.Hit].Frames.AddAction(FrameAction.Stop);
                        sprPesant.KeyFrames.Add(
                            (uint)Consts.PesantState.HitToDeath,
                            game.dicTexMulher["hit"],
                            3,
                            0, 1, 2, 2
                            );
                        sprPesant.KeyFrames[(uint)Consts.PesantState.HitToDeath].Frames.AddAction(FrameAction.MoveToKey, (uint)Consts.PesantState.Death);
                        sprPesant.KeyFrames.Add(
                            (uint)Consts.PesantState.Death,
                            game.dicTexMulher["death"],
                            7,
                            0, 1, 1, 1, 1, 1, 2, 2, 3, 4, 5, 6
                            );
                        sprPesant.KeyFrames[(uint)Consts.PesantState.Death].Frames.AddAction(FrameAction.Stop);
                        break;
                    }
                case Consts.TipoPesant.Homem_1:
                    {
                        Velocidade = 120;
                        // sprite
                        sprPesant = new Sprite();
                        sprPesant.KeyFrames.Add(
                            (uint)Consts.PesantState.Walk_Up,
                            game.dicTexHomem["walk-up"],
                            4,
                            0, 1, 2, 3
                            );
                        sprPesant.KeyFrames.Add(
                            (uint)Consts.PesantState.Walk_Down,
                            game.dicTexHomem["walk-down"],
                            4,
                            0, 1, 2, 3
                            );
                        sprPesant.KeyFrames.Add(
                            (uint)Consts.PesantState.Walk_Left,
                            game.dicTexHomem["walk-left"],
                            4,
                            0, 1, 2, 3
                            );
                        sprPesant.KeyFrames.Add(
                            (uint)Consts.PesantState.Walk_Right,
                            game.dicTexHomem["walk-right"],
                            4,
                            0, 1, 2, 3
                            );
                        sprPesant.KeyFrames.Add(
                            (uint)Consts.PesantState.Hit,
                            game.dicTexHomem["hit"],
                            3,
                            0, 1, 2, 2, 2
                            );
                        sprPesant.KeyFrames[(uint)Consts.PesantState.Hit].Frames.AddAction(FrameAction.Stop);
                        sprPesant.KeyFrames.Add(
                            (uint)Consts.PesantState.HitToDeath,
                            game.dicTexHomem["hit"],
                            3,
                            0, 1, 2, 2
                            );
                        sprPesant.KeyFrames[(uint)Consts.PesantState.HitToDeath].Frames.AddAction(FrameAction.MoveToKey, (uint)Consts.PesantState.Death);
                        sprPesant.KeyFrames.Add(
                            (uint)Consts.PesantState.Death,
                            game.dicTexHomem["death"],
                            7,
                            0, 1, 0, 1, 0, 1, 0, 1, 2, 2, 3, 4, 5, 6
                            );
                        sprPesant.KeyFrames[(uint)Consts.PesantState.Death].Frames.AddAction(FrameAction.Stop);
                        break;
                    }
                case Consts.TipoPesant.Homem_2:
                    {
                        Velocidade = 120;
                        // sprite
                        sprPesant = new Sprite();
                        sprPesant.KeyFrames.Add(
                            (uint)Consts.PesantState.Walk_Up,
                            game.dicTexHomem2["walk-up"],
                            4,
                            0, 1, 2, 3
                            );
                        sprPesant.KeyFrames.Add(
                            (uint)Consts.PesantState.Walk_Down,
                            game.dicTexHomem2["walk-down"],
                            4,
                            0, 1, 2, 3
                            );
                        sprPesant.KeyFrames.Add(
                            (uint)Consts.PesantState.Walk_Left,
                            game.dicTexHomem2["walk-left"],
                            4,
                            0, 1, 2, 3
                            );
                        sprPesant.KeyFrames.Add(
                            (uint)Consts.PesantState.Walk_Right,
                            game.dicTexHomem2["walk-right"],
                            4,
                            0, 1, 2, 3
                            );
                        sprPesant.KeyFrames.Add(
                            (uint)Consts.PesantState.Hit,
                            game.dicTexHomem2["hit"],
                            3,
                            0, 1, 2, 2, 2
                            );
                        sprPesant.KeyFrames[(uint)Consts.PesantState.Hit].Frames.AddAction(FrameAction.Stop);
                        sprPesant.KeyFrames.Add(
                            (uint)Consts.PesantState.HitToDeath,
                            game.dicTexHomem2["hit"],
                            3,
                            0, 1, 2, 2
                            );
                        sprPesant.KeyFrames[(uint)Consts.PesantState.HitToDeath].Frames.AddAction(FrameAction.MoveToKey, (uint)Consts.PesantState.Death);
                        sprPesant.KeyFrames.Add(
                            (uint)Consts.PesantState.Death,
                            game.dicTexHomem2["death"],
                            7,
                            0, 1, 0, 1, 0, 1, 0, 1, 2, 2, 3, 4, 5, 6
                            );
                        sprPesant.KeyFrames[(uint)Consts.PesantState.Death].Frames.AddAction(FrameAction.Stop);
                        break;
                    }
            }
            this.sprPesant.DrawPivot = SpriteDrawPivot.Bottom;
            this.sprPesant.Position = PosicaoInicial;
            switch (this.sentido)
            {
                case Consts.Sentido.Direita: sprPesant.ActualKeyFrame = (uint)Consts.PesantState.Walk_Right; break;
                case Consts.Sentido.Esquerda: sprPesant.ActualKeyFrame = (uint)Consts.PesantState.Walk_Left; break;
                case Consts.Sentido.Cima: sprPesant.ActualKeyFrame = (uint)Consts.PesantState.Walk_Up; break;
                case Consts.Sentido.Baixo: sprPesant.ActualKeyFrame = (uint)Consts.PesantState.Walk_Down; break;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (ScaringDefense.CO_DEBUG_MODE)
            {
                Rectangle rectEnergia = new Rectangle((int)this.sprPesant.Position.X + 10,
                                                      (int)(this.sprPesant.Position.Y - 50),
                                                      (int)(Vida / 2),
                                                      5);

                //spriteBatch.Draw(game.texVida, rectEnergia, Color.White);
            }

            //spriteBatch.Draw(this.texPesant, this.posicao, null, Color.White, this.angle, VectorCenterPoint, 1, SpriteEffects.None, 0.0f);
            sprPesant.Draw(new SpriteDrawArgs() { GameTime = gameTime, SpriteBatch = spriteBatch });
        }
        public bool Update(GameTime gameTime)
        {
            bool result = false;

            // Testar se o personagem tá tomando hit
            bool hiting = State == Consts.PesantState.Hit && !sprPesant.KeyFrames[(uint)State].IsStopped;
            if (Vida > 0 && !hiting)
            {
                // Hit to walk
                if (State == Consts.PesantState.Hit)
                {
                    switch (this.sentido)
                    {
                        case Consts.Sentido.Direita: sprPesant.ActualKeyFrame = (uint)Consts.PesantState.Walk_Right; break;
                        case Consts.Sentido.Esquerda: sprPesant.ActualKeyFrame = (uint)Consts.PesantState.Walk_Left; break;
                        case Consts.Sentido.Cima: sprPesant.ActualKeyFrame = (uint)Consts.PesantState.Walk_Up; break;
                        case Consts.Sentido.Baixo: sprPesant.ActualKeyFrame = (uint)Consts.PesantState.Walk_Down; break;
                    }
                }
                // MOVE IT, NOW!
                switch (this.sentido)
                {
                    case Consts.Sentido.Baixo:
                        //this.posicao.Y += this.Velocidade * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        sprPesant.Position = sprPesant.Position + new Vector2(0, this.Velocidade * (float)gameTime.ElapsedGameTime.TotalSeconds);
                        break;
                    case Consts.Sentido.Cima:
                        //this.posicao.Y -= this.Velocidade * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        sprPesant.Position = sprPesant.Position - new Vector2(0, this.Velocidade * (float)gameTime.ElapsedGameTime.TotalSeconds);
                        break;
                    case Consts.Sentido.Direita:
                        //this.posicao.X += this.Velocidade * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        sprPesant.Position = sprPesant.Position + new Vector2(this.Velocidade * (float)gameTime.ElapsedGameTime.TotalSeconds, 0);
                        break;
                    case Consts.Sentido.Esquerda:
                        //this.posicao.X -= this.Velocidade * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        sprPesant.Position = sprPesant.Position - new Vector2(this.Velocidade * (float)gameTime.ElapsedGameTime.TotalSeconds, 0);
                        break;
                }

                if (game.lvlAtual.TheEndPoint.recEventPoint.Intersects(this.recPivo))
                {
                    game.Dinheiro -= 100;
                    result = true;
                }
                else
                {
                    foreach (EventPoint ep in game.lvlAtual.eventList)
                    {
                        if (ep.recEventPoint.Contains(this.recPivo))
                        {
                            //this.sentido = ep.SentidoAtual;
                            if (ep.Sentidos.Count == 1)
                            {
                                ep.SentidoAtual = ep.Sentidos[0];
                            }
                            else
                            {
                                if (ep.SentidoAtual == ep.Sentidos[0])
                                {
                                    ep.SentidoAtual = ep.Sentidos[1];
                                }
                                else
                                {
                                    ep.SentidoAtual = ep.Sentidos[0];
                                }
                            }
                            this.sentido = ep.SentidoAtual;
                            // spr
                            switch (this.sentido)
                            {
                                case Consts.Sentido.Direita: sprPesant.ActualKeyFrame = (uint)Consts.PesantState.Walk_Right; break;
                                case Consts.Sentido.Esquerda: sprPesant.ActualKeyFrame = (uint)Consts.PesantState.Walk_Left; break;
                                case Consts.Sentido.Cima: sprPesant.ActualKeyFrame = (uint)Consts.PesantState.Walk_Up; break;
                                case Consts.Sentido.Baixo: sprPesant.ActualKeyFrame = (uint)Consts.PesantState.Walk_Down; break;
                            }
                            //rotaciona
                            break;
                        }
                    }

                    foreach (Trap tr in game.lvlAtual.trapList)
                    {
                        if (tr.recTrigger.Contains(this.recPivo))
                        {
                            if (tr.ActualCooldownTime == 0)
                            {
                                // HIT
                                this.Vida -= tr.Dano;
                                tr.ActualCooldownTime = tr.LimitCooldownTime;
                                //
                                switch (tr.Sentido)
                                {
                                    case Consts.Sentido.Direita:
                                        tr.State = Consts.TrapState.HitToRight;
                                        break;
                                    case Consts.Sentido.Esquerda:
                                        tr.State = Consts.TrapState.HitToLeft;
                                        break;
                                    case Consts.Sentido.Cima:
                                        tr.State = Consts.TrapState.HitToUp;
                                        break;
                                    case Consts.Sentido.Baixo:
                                        tr.State = Consts.TrapState.HitToDown;
                                        break;
                                }
                                // Dano
                                if (this.Vida <= 0)
                                {
                                    game.Dinheiro += 50;
                                    game.Score++;
                                    game.ScoreInterno++;
                                    // Hit com morte
                                    this.Vida = 0;
                                    sprPesant.ActualKeyFrame = (uint)Consts.PesantState.HitToDeath;
                                    //result = true;
                                    switch (tipo)
                                    {
                                        case Consts.TipoPesant.Mulher_1:
                                            game.dicAudio["scream-woman"].SomNaCaixaDJ();
                                            break;
                                        case Consts.TipoPesant.Homem_1:
                                        case Consts.TipoPesant.Homem_2:
                                            game.dicAudio["scream-man"].SomNaCaixaDJ();
                                            break;
                                    }
                                }
                                else
                                {
                                    // apenas hit
                                    sprPesant.ActualKeyFrame = (uint)Consts.PesantState.Hit;
                                    //
                                    switch (tr.tipo)
                                    {
                                        case Consts.TipoTrap.Vampire:
                                            game.dicAudio["vamp-01"].SomNaCaixaDJ();
                                            break;
                                        case Consts.TipoTrap.Mummy:
                                            game.dicAudio["mummy-01"].SomNaCaixaDJ();
                                            break;
                                        case Consts.TipoTrap.Wraith:
                                            game.dicAudio["ghost-01"].SomNaCaixaDJ();
                                            break;
                                    }
                                }
                            }
                            break;
                        }
                    }
                }
            }
            sprPesant.Update(new SpriteUpdateArgs() { GameTime = gameTime });
            return result;
        }
    }
}
