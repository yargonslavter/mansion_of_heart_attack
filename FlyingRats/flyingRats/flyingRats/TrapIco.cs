using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace flyingRats
{
    public class TrapIco
    {

        public Rectangle recIco;
        public Rectangle recIcoMoldura;
        public Rectangle recIcoMolduraGlow;
        public Texture2D texIcoGde;
        public Texture2D texIcoPeq;
        public Consts.TipoTrap tipo;
        public bool Selected = false;
        public ScaringDefense game;
        public bool Glow = false;
        public TrapIco(ScaringDefense Game)
        {
            this.game = Game;
            recIcoMoldura = game.texEffects["selectionframe"].Bounds;
            recIcoMolduraGlow = game.texEffects["selectionglow"].Bounds;            
        }

        public int Custo
        {
            get
            {
                switch (tipo)
                {
                    case Consts.TipoTrap.Mummy: return 100;
                    case Consts.TipoTrap.Vampire: return 200;
                    case Consts.TipoTrap.Wraith: return 350;
                    default: return 0;
                }
            }
        }
        public double CoolDown
        {
            get
            {
                switch (tipo)
                {
                    case Consts.TipoTrap.Mummy: return 3;
                    case Consts.TipoTrap.Vampire: return 4.0;
                    case Consts.TipoTrap.Wraith: return 4.0;
                    default: return 0;
                }
            }
        }
        public int Dano
        {
            get
            {
                switch (tipo)
                {
                    case Consts.TipoTrap.Mummy: return 35;
                    case Consts.TipoTrap.Vampire: return 60;
                    case Consts.TipoTrap.Wraith: return 80;
                    default: return 0;
                }
            }
        }


        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            recIcoMoldura.X = recIco.X - ((recIcoMoldura.Width - recIco.Width) / 2);
            recIcoMoldura.Y = recIco.Y - ((recIcoMoldura.Height - recIco.Height) / 2);

            spriteBatch.Draw(texIcoGde, recIco, Color.White);
            spriteBatch.Draw(game.texEffects["selectionframe"], recIcoMoldura, Color.White);

            

            if (game.SelectedTrapIco != null && game.SelectedTrapIco.tipo == this.tipo)
            {
                recIcoMolduraGlow.X = recIco.X - ((recIcoMolduraGlow.Width - recIco.Width) / 2);
                recIcoMolduraGlow.Y = recIco.Y - ((recIcoMolduraGlow.Height - recIco.Height) / 2);

                spriteBatch.Draw(game.texEffects["selectionglow"], recIcoMolduraGlow, Color.White);
            }
            else if (this.Glow)
            {
                recIcoMolduraGlow.X = recIco.X - ((recIcoMolduraGlow.Width - recIco.Width) / 2);
                recIcoMolduraGlow.Y = recIco.Y - ((recIcoMolduraGlow.Height - recIco.Height) / 2);
                spriteBatch.Draw(game.texEffects["selectionglowweak"], recIcoMolduraGlow, Color.White);
            }
        }

        internal Trap GetTrap(EventPoint holder)
        {
            Trap atrap = new Trap();
            atrap.recTrap = holder.recEventPoint;
            atrap.tipo = this.tipo;
            Dictionary<string, Texture2D> dic = null;

            atrap.Cost = this.Custo;
            atrap.LimitCooldownTime = this.CoolDown;
            atrap.Dano = this.Dano;

            switch (tipo)
            {
                case Consts.TipoTrap.Mummy:
                    dic = game.dicMumia;
                    break;
                case Consts.TipoTrap.Vampire:
                    dic = game.dicVampiro;
                    break;
                case Consts.TipoTrap.Wraith:
                    dic = game.dicFantasma;
                    break;
            }
            // Configurar sprite stand
            atrap.Sprite.DrawPivot = SpriteDrawPivot.Bottom;
            atrap.Sprite.KeyFrames.Add(
                (uint)Consts.TrapState.Up,
                dic["up"]);
            atrap.Sprite.KeyFrames.Add(
                (uint)Consts.TrapState.Down,
                dic["down"]);
            atrap.Sprite.KeyFrames.Add(
                (uint)Consts.TrapState.Left,
                dic["left"]);
            atrap.Sprite.KeyFrames.Add(
                (uint)Consts.TrapState.Right,
                dic["right"]);
            // Hiting
            switch (tipo)
            {
                case Consts.TipoTrap.Vampire:
                case Consts.TipoTrap.Mummy:
                    // HIT
                    atrap.Sprite.KeyFrames.Add(
                        (uint)Consts.TrapState.HitToUp,
                        dic["hit-up"],
                        2,
                        0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0);
                    atrap.Sprite.KeyFrames[(uint)Consts.TrapState.HitToUp].Frames.AddAction(FrameAction.Stop);
                    atrap.Sprite.KeyFrames.Add(
                        (uint)Consts.TrapState.HitToDown,
                        dic["hit-down"],
                        2,
                        0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0);
                    atrap.Sprite.KeyFrames[(uint)Consts.TrapState.HitToDown].Frames.AddAction(FrameAction.Stop);
                    atrap.Sprite.KeyFrames.Add(
                        (uint)Consts.TrapState.HitToLeft,
                        dic["hit-left"],
                        2,
                        0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0);
                    atrap.Sprite.KeyFrames[(uint)Consts.TrapState.HitToLeft].Frames.AddAction(FrameAction.Stop);
                    atrap.Sprite.KeyFrames.Add(
                        (uint)Consts.TrapState.HitToRight,
                        dic["hit-right"],
                        2,
                        0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0);
                    atrap.Sprite.KeyFrames[(uint)Consts.TrapState.HitToRight].Frames.AddAction(FrameAction.Stop);
                    // Configurar sprite de ataque
                    atrap.AttackSprite.DrawPivot = SpriteDrawPivot.Bottom;
                    atrap.AttackSprite.KeyFrames.Add(
                        (uint)Consts.TrapState.HitToUp,
                        dic["attack-up"],
                        2,
                        0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0);
                    atrap.AttackSprite.KeyFrames[(uint)Consts.TrapState.HitToUp].Frames.AddAction(FrameAction.Stop);
                    atrap.AttackSprite.KeyFrames.Add(
                        (uint)Consts.TrapState.HitToDown,
                        dic["attack-down"],
                        2,
                        0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0);
                    atrap.AttackSprite.KeyFrames[(uint)Consts.TrapState.HitToDown].Frames.AddAction(FrameAction.Stop);
                    atrap.AttackSprite.KeyFrames.Add(
                        (uint)Consts.TrapState.HitToLeft,
                        dic["attack-left"],
                        2,
                        0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0);
                    atrap.AttackSprite.KeyFrames[(uint)Consts.TrapState.HitToLeft].Frames.AddAction(FrameAction.Stop);
                    atrap.AttackSprite.KeyFrames.Add(
                        (uint)Consts.TrapState.HitToRight,
                        dic["attack-right"],
                        2,
                        0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0);
                    atrap.AttackSprite.KeyFrames[(uint)Consts.TrapState.HitToRight].Frames.AddAction(FrameAction.Stop);
                    break;
                case Consts.TipoTrap.Wraith:
                    // Normal
                    // HIT
                    atrap.Sprite.KeyFrames.Add(
                        (uint)Consts.TrapState.HitToUp,
                        dic["hit-up"],
                        2,
                        0, 0, 0, 0, 0, 0, 0, 0, 1);
                    atrap.Sprite.KeyFrames[(uint)Consts.TrapState.HitToUp].Frames.AddAction(FrameAction.Stop);
                    atrap.Sprite.KeyFrames.Add(
                        (uint)Consts.TrapState.HitToDown,
                        dic["hit-down"],
                        2,
                        0, 0, 0, 0, 0, 0, 0, 0, 1);
                    atrap.Sprite.KeyFrames[(uint)Consts.TrapState.HitToDown].Frames.AddAction(FrameAction.Stop);
                    atrap.Sprite.KeyFrames.Add(
                        (uint)Consts.TrapState.HitToLeft,
                        dic["hit-left"],
                        2,
                        0, 0, 0, 0, 0, 0, 0, 0, 1);
                    atrap.Sprite.KeyFrames[(uint)Consts.TrapState.HitToLeft].Frames.AddAction(FrameAction.Stop);
                    atrap.Sprite.KeyFrames.Add(
                        (uint)Consts.TrapState.HitToRight,
                        dic["hit-right"],
                        2,
                        0, 0, 0, 0, 0, 0, 0, 0, 1);
                    atrap.Sprite.KeyFrames[(uint)Consts.TrapState.HitToRight].Frames.AddAction(FrameAction.Stop);
                    // Attack
                    atrap.AttackSprite.DrawPivot = SpriteDrawPivot.Bottom;
                    atrap.AttackSprite.KeyFrames.Add(
                        (uint)Consts.TrapState.HitToUp,
                        dic["attack-up"],
                        2,
                        1, 1, 1, 1, 1, 0, 0);
                    atrap.AttackSprite.KeyFrames[(uint)Consts.TrapState.HitToUp].Frames.AddAction(FrameAction.Stop);
                    atrap.AttackSprite.KeyFrames.Add(
                        (uint)Consts.TrapState.HitToDown,
                        dic["attack-down"],
                        2,
                        1, 1, 1, 1, 1, 0, 0);
                    atrap.AttackSprite.KeyFrames[(uint)Consts.TrapState.HitToDown].Frames.AddAction(FrameAction.Stop);
                    atrap.AttackSprite.KeyFrames.Add(
                        (uint)Consts.TrapState.HitToLeft,
                        dic["attack-left"],
                        2,
                        1, 1, 1, 1, 1, 0, 0);
                    atrap.AttackSprite.KeyFrames[(uint)Consts.TrapState.HitToLeft].Frames.AddAction(FrameAction.Stop);
                    atrap.AttackSprite.KeyFrames.Add(
                        (uint)Consts.TrapState.HitToRight,
                        dic["attack-right"],
                        2,
                        1, 1, 1, 1, 1, 0, 0);
                    atrap.AttackSprite.KeyFrames[(uint)Consts.TrapState.HitToRight].Frames.AddAction(FrameAction.Stop);
                    break;
            }

            // Holder
            atrap.Sentido = holder.SentidoAtual;
            atrap.Sprite.Position = holder.Posicao + new Vector2(60, 120);
            atrap.AttackSprite.Position = atrap.Sprite.Position;
            switch (holder.SentidoAtual)
            {
                case Consts.Sentido.Baixo:
                    atrap.recTrigger = new Rectangle(holder.recEventPoint.X, holder.recEventPoint.Y + holder.recEventPoint.Height, holder.recEventPoint.Width, holder.recEventPoint.Height);
                    break;
                case Consts.Sentido.Cima:
                    atrap.recTrigger = new Rectangle(holder.recEventPoint.X, holder.recEventPoint.Y - holder.recEventPoint.Height, holder.recEventPoint.Width, holder.recEventPoint.Height);
                    break;
                case Consts.Sentido.Direita:
                    atrap.recTrigger = new Rectangle(holder.recEventPoint.X + holder.recEventPoint.Width, holder.recEventPoint.Y, holder.recEventPoint.Width, holder.recEventPoint.Height);
                    break;
                case Consts.Sentido.Esquerda:
                    atrap.recTrigger = new Rectangle(holder.recEventPoint.X - holder.recEventPoint.Width, holder.recEventPoint.Y, holder.recEventPoint.Width, holder.recEventPoint.Height);
                    break;

            }
            return atrap;
        }

        
    }
}
