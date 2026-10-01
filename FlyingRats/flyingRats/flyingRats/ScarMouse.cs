using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace flyingRats
{
    public class ScarMouse
    {
        public Rectangle recMouse;
        public Texture2D texMouse;
        public Texture2D texMouseDefault;
        public Texture2D texMouseSelected;
        public bool Selected = false;
        public bool TiledMode = false;
        //Action ExecutarOnClic = null;
        public ScaringDefense game;

        public bool BeginDockTrap = false;


        public ScarMouse(ContentManager Content, ScaringDefense aGame)
        {
            this.game = aGame;
            texMouseDefault = Content.Load<Texture2D>(@"Textures\Mouse\Mouse");
            texMouse = Content.Load<Texture2D>(@"Textures\Mouse\Mouse");
            texMouseSelected = Content.Load<Texture2D>(@"Textures\Mouse\mouse_selected");
            recMouse = new Rectangle(0, 0, texMouse.Width, texMouse.Height);
        }

        public void ChangeMouseTexture(Texture2D texNewTexture, Action ExecuteOnClick)
        {
            texMouse = texNewTexture;
            //recMouse = new Rectangle(0, 0, texMouse.Width, texMouse.Height);
            recMouse = new Rectangle(0, 0, 80, 80);
        }

        public void BackMouseTexture()
        {
            //this.ExecutarOnClic = null;
            texMouse = texMouseDefault;
            recMouse = new Rectangle(0, 0, texMouse.Width, texMouse.Height);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Selected)
            {
                spriteBatch.Draw(texMouse, recMouse, Color.White);
                spriteBatch.Draw(texMouseSelected, recMouse, Color.White);
            }
            else
            {
                spriteBatch.Draw(texMouse, recMouse, Color.White);
            }
        }

        public void UpdateMouseForMenu(GameTime gameTime, MouseState msActualMouseState, MouseState msOldMouseState)
        {
            recMouse.X = msActualMouseState.X - (recMouse.Width / 2);
            recMouse.Y = msActualMouseState.Y - (recMouse.Height / 2);
        }

        public void UpdateMouse(GameTime gameTime, MouseState msActualMouseState, MouseState msOldMouseState)
        {
            if (TiledMode)
            {

            }
            else
            {
                recMouse.X = msActualMouseState.X - (recMouse.Width / 2);
                recMouse.Y = msActualMouseState.Y - (recMouse.Height / 2);
            }

            if (msOldMouseState != msActualMouseState)
            {

                if (game.Bar.recBarra.Intersects(recMouse))
                {
                    //Descobre se está colidindo com algum ícone...
                    foreach (TrapIco ti in game.Bar.Icones)
                    {
                        if (ti.recIco.Intersects(recMouse))
                        {
                            ti.Glow = true;
                        }
                        else
                        {
                            ti.Glow = false;
                        }
                    }
                }

                if (msOldMouseState.LeftButton == ButtonState.Pressed && msActualMouseState.LeftButton == ButtonState.Released)
                {
                    //Primeiro Verifica se o Mouse está no Mapa ou na Barra
                    if (game.Bar.recBarra.Intersects(recMouse))
                    {
                        //Descobre se está colidindo com algum ícone...
                        foreach (TrapIco ti in game.Bar.Icones)
                        {
                            if (ti.tipo != Consts.TipoTrap.Mistery && ti.recIco.Intersects(recMouse))
                            {
                                if (Selected)
                                {
                                    CancelAllActions();

                                    if (this.game.SelectedTrapIco == ti)
                                    {
                                        this.game.SelectedTrapIco = null;
                                    }
                                }
                                else
                                {
                                    this.game.SelectedTrapIco = ti;
                                    ti.Selected = true;
                                    if (ti.Custo <= game.Dinheiro)
                                    {
                                        Selected = true;
                                        BeginDockTrap = true;
                                        this.ChangeMouseTexture(ti.texIcoPeq, null);
                                    }
                                }
                                break;
                            }
                        }
                    }
                    else if (BeginDockTrap)
                    {
                        foreach (EventPoint holder in game.lvlAtual.eventHolderList.Where(h => h.HolderOcupado == false))
                        {
                            if (this.recMouse.Intersects(holder.recEventPoint))
                            {
                                holder.HolderOcupado = true;
                                Trap t = game.SelectedTrapIco.GetTrap(holder);
                                if (t.tipo != Consts.TipoTrap.Mistery)
                                {
                                    this.game.SelectedTrapIco.Selected = false;
                                    game.lvlAtual.trapList.Add(t);
                                }
                                game.Dinheiro -= t.Cost;
                                CancelAllActions();
                                break;
                            }
                        }

                    }
                }

                if (msActualMouseState.RightButton == ButtonState.Pressed)
                {                    
                    CancelAllActions();

                }
            }

        }

        private void CancelAllActions()
        {
            if (this.game.SelectedTrapIco != null)
            {
                this.game.SelectedTrapIco.Selected = false;
            }
            Selected = false;
            BeginDockTrap = false;
            this.BackMouseTexture();

        }
    }
}
