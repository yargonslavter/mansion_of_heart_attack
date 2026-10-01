using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace flyingRats
{
    public class Barra
    {
        public List<TrapIco> Icones;
        public Rectangle recBarra;
        public Rectangle recStats;
        public Rectangle recIcoStats;

        public Texture2D texBarra;
        public Texture2D texStats;
        public Texture2D texIcoStats;

        public Vector2 vecLevel;
        public Vector2 vecDinheiro;
        public Vector2 vecBodyCount;

        public Vector2 vecNome;
        public Vector2 vecDano;
        public Vector2 vecCost;

        public Vector2 vecLimitCooldownTime;

        ScaringDefense game;

        public Barra(int DefaultWidth, int DefaultHeight, GraphicsDeviceManager graphics, ContentManager Content, ScaringDefense aGame)
        {
            this.game = aGame;
            int spacerX = 30;
            int spacerY = 35;
            int spacerBig = 40;
            int offSetY = 70;
            texBarra = Content.Load<Texture2D>(@"Textures\Barra\barra_baixo");
            recBarra = new Rectangle(0, DefaultHeight - texBarra.Height, DefaultWidth, texBarra.Height);

            recStats = new Rectangle(recBarra.X + 70, recBarra.Y + offSetY, 340, 155);
            recIcoStats = new Rectangle(recStats.X + 17, recStats.Y, 128, 128);

            vecLevel = new Vector2(recBarra.Width - 180, recIcoStats.Y);
            vecDinheiro = new Vector2(vecLevel.X, vecLevel.Y + spacerBig);
            vecBodyCount = new Vector2(vecDinheiro.X, vecDinheiro.Y + spacerBig);

            vecNome = new Vector2(recIcoStats.X + recIcoStats.Width + spacerX, recIcoStats.Y-3);
            vecDano = new Vector2(vecNome.X, vecNome.Y + spacerY);
            vecCost = new Vector2(vecDano.X, vecDano.Y + spacerY);
            vecLimitCooldownTime = new Vector2(vecCost.X, vecCost.Y + spacerY);

            Icones = new List<TrapIco>();

            #region Inicializa Icones
            TrapIco tiMumia = new TrapIco(this.game);
            tiMumia.texIcoGde = game.dicMumia["icone"];
            tiMumia.texIcoPeq = game.dicMumia["down"];
            tiMumia.tipo = Consts.TipoTrap.Mummy;
            Icones.Add(tiMumia);


            TrapIco tiVampiro = new TrapIco(this.game);
            tiVampiro.texIcoGde = game.dicVampiro["icone"];
            tiVampiro.texIcoPeq = game.dicVampiro["down"];
            tiVampiro.tipo = Consts.TipoTrap.Vampire;
            Icones.Add(tiVampiro);

            TrapIco tiFantasma = new TrapIco(this.game);
            tiFantasma.texIcoGde = game.dicFantasma["icone"];
            tiFantasma.texIcoPeq = game.dicFantasma["down"];
            tiFantasma.tipo = Consts.TipoTrap.Wraith;
            Icones.Add(tiFantasma);

            TrapIco tiMestreSecreto = new TrapIco(this.game);
            tiMestreSecreto.texIcoGde = game.texMistery;
            tiMestreSecreto.texIcoPeq = game.texMistery;
            tiMestreSecreto.tipo = Consts.TipoTrap.Mistery;
            Icones.Add(tiMestreSecreto);

            #endregion

            #region barra

            texStats = new Texture2D(graphics.GraphicsDevice, 1, 1);
            //texStats.SetData(new Color[] { Color.White });

            texIcoStats = new Texture2D(graphics.GraphicsDevice, 1, 1);
            //texIcoStats.SetData(new Color[] { Color.White });

            int IconeIndex = 0;
            foreach (TrapIco ico in Icones)
            {



                ico.recIco = new Rectangle(recBarra.X + recStats.X + recStats.Width + 80 + (ico.texIcoGde.Width * IconeIndex) + (120 * ++IconeIndex),
                                            recBarra.Y + offSetY,
                                            128,
                                            128);
            }
            #endregion

        }

        public void AdicionarIcone(TrapIco icone)
        {
            Icones.Add(icone);
            icone.recIco = new Rectangle();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texBarra, recBarra, Color.White);

            spriteBatch.DrawString(game.sfScore, string.Format("L {0}", game.AuxLevelAtual+1), vecLevel, Color.White);
            spriteBatch.DrawString(game.sfScore, string.Format("$ {0}", game.Dinheiro), vecDinheiro, Color.White);
            spriteBatch.DrawString(game.sfScore, string.Format("D {0}", game.Score), vecBodyCount, Color.White);

            if (game.SelectedTrapIco != null)
            {
                spriteBatch.Draw(game.SelectedTrapIco.texIcoGde, recIcoStats, Color.White);

                if (game.SelectedTrapIco.tipo == Consts.TipoTrap.Mistery)
                {
                    spriteBatch.DrawString(game.sfDetalhes, string.Format("Creature: {0}", game.SelectedTrapIco.tipo), vecNome, Color.White);
                    spriteBatch.DrawString(game.sfDetalhes, "Scare: ???", vecDano, Color.White);
                    spriteBatch.DrawString(game.sfDetalhes, "Cost: ????", vecCost, Color.White);
                    spriteBatch.DrawString(game.sfDetalhes, "Cooldown: ???? /s", vecLimitCooldownTime, Color.White);
                }
                else
                {
                    spriteBatch.DrawString(game.sfDetalhes, string.Format("Creature: {0}", game.SelectedTrapIco.tipo), vecNome, Color.White);
                    spriteBatch.DrawString(game.sfDetalhes, string.Format("Damage: {0}", game.SelectedTrapIco.Dano), vecDano, Color.White);
                    spriteBatch.DrawString(game.sfDetalhes, string.Format("Cost: {0}", game.SelectedTrapIco.Custo), vecCost, Color.White);
                    spriteBatch.DrawString(game.sfDetalhes, string.Format("Cooldown: {0} /s", game.SelectedTrapIco.CoolDown), vecLimitCooldownTime, Color.White);
                }
            }
            else
            {
                if (texStats != null && texIcoStats != null)
                {
                    spriteBatch.Draw(texStats, recStats, Color.White);
                    spriteBatch.Draw(texIcoStats, recIcoStats, Color.White);
                }
            }

            foreach (TrapIco ico in Icones)
            {
                ico.Draw(gameTime, spriteBatch);
            }
        }
    }
}
