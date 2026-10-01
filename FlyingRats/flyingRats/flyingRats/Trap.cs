using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace flyingRats
{
    public class Trap
    {
        private Consts.Sentido _sentido;

        public string name;
        public int Dano;
        public int Cost;
        public int SellPrice;
        public double LimitCooldownTime;
        public double ActualCooldownTime;

        public Rectangle recTrap;
        //public Texture2D texTrap;
        public Consts.TipoDano tipoDano;
        public Consts.TipoTrap tipo;
        public Consts.Sentido Sentido
        {
            get { return _sentido; }
            set
            {
                if (value == _sentido)
                    return;
                _sentido = value;
                switch (_sentido)
                {
                    case Consts.Sentido.Direita:
                        Sprite.ActualKeyFrame = (uint)Consts.TrapState.Right;
                        break;
                    case Consts.Sentido.Esquerda:
                        Sprite.ActualKeyFrame = (uint)Consts.TrapState.Left;
                        break;
                    case Consts.Sentido.Cima:
                        Sprite.ActualKeyFrame = (uint)Consts.TrapState.Up;
                        break;
                    case Consts.Sentido.Baixo:
                        Sprite.ActualKeyFrame = (uint)Consts.TrapState.Down;
                        break;
                }
            }
        }
        public Point pMatrixLocation;

        public Rectangle recTrigger;

        public Sprite Sprite { get; set; }
        public Sprite AttackSprite { get; set; }
        public Consts.TrapState State // Fast cast
        {
            get { return (Consts.TrapState)Sprite.ActualKeyFrame; }
            set
            {
                //if ((uint)value == Sprite.ActualKeyFrame)
                //    return;
                Sprite.ActualKeyFrame = (uint)value;
                AttackSprite.ActualKeyFrame = (uint)value;
                switch (value)
                {
                    case Consts.TrapState.HitToUp:
                    case Consts.TrapState.HitToDown:
                    case Consts.TrapState.HitToLeft:
                    case Consts.TrapState.HitToRight:
                        //
                        switch (Sentido)
                        {
                            case Consts.Sentido.Direita:
                                AttackSprite.Position = Sprite.Position + new Vector2(30, 0);
                                break;
                            case Consts.Sentido.Esquerda:
                                AttackSprite.Position = Sprite.Position - new Vector2(30, 0);
                                break;
                            case Consts.Sentido.Cima:
                                AttackSprite.Position = Sprite.Position - new Vector2(0, 30);
                                break;
                            case Consts.Sentido.Baixo:
                                AttackSprite.Position = Sprite.Position + new Vector2(0, 30);
                                break;
                        }
                        break;
                }
            }
        }

        public Trap()
        {
            Sprite = new Sprite();
            AttackSprite = new Sprite();
        }

        public void ResetCooldown()
        {
            ActualCooldownTime = 0;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            SpriteDrawArgs e = new SpriteDrawArgs() { GameTime = gameTime, SpriteBatch = spriteBatch };
            Sprite.Draw(e);
            switch (State)
            {
                case Consts.TrapState.HitToUp:
                case Consts.TrapState.HitToDown:
                case Consts.TrapState.HitToLeft:
                case Consts.TrapState.HitToRight:
                    AttackSprite.Draw(e);
                    break;
            }
        }
        public void Update(GameTime gameTime)
        {
            SpriteUpdateArgs e = new SpriteUpdateArgs() { GameTime = gameTime };
            Sprite.Update(e);
            switch (State)
            {
                case Consts.TrapState.HitToUp:
                case Consts.TrapState.HitToDown:
                case Consts.TrapState.HitToLeft:
                case Consts.TrapState.HitToRight:
                    AttackSprite.Update(e);
                    break;
            }
            if (ActualCooldownTime > 0)
            {
                ActualCooldownTime -= gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (ActualCooldownTime <= 0)
            {
                ActualCooldownTime = 0;
            }
        }
    }
}
