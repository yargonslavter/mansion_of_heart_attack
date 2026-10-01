using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace flyingRats
{
    public class SpriteDrawArgs
    {
        public SpriteBatch SpriteBatch { get; set; }
        public GameTime GameTime { get; set; }
    }

    public class SpriteUpdateArgs
    {
        public GameTime GameTime { get; set; }
    }

    public class SpriteCollection : List<Sprite>
    {
    }

    public enum FrameAction
    {
        ChangeIndex,
        MoveToKey,
        Stop
    }

    public class FrameInfo
    {
        // Constructors
        public FrameInfo()
        {
            Action = FrameAction.ChangeIndex;
        }
        public FrameInfo(uint imageIndex)
            : this(FrameAction.ChangeIndex, imageIndex)
        {
        }
        public FrameInfo(FrameAction action)
            : this(action, 0)
        {
        }
        public FrameInfo(FrameAction action, uint value)
        {
            this.Action = action;
            this.Value = value;
        }

        // Properties
        public FrameAction Action { get; set; }
        public uint Value { get; set; }
    }

    public class FrameCollection : List<FrameInfo>
    {
        // Constructors
        public FrameCollection()
        {
        }
        public FrameCollection(params uint[] frames)
        {
            foreach (var f in frames)
            {
                this.Add(f);
            }
        }

        // Methods
        public void Add(uint imageIndex)
        {
            base.Add(new FrameInfo(imageIndex));
        }
        public void AddAction(FrameAction action)
        {
            base.Add(new FrameInfo(action));
        }
        public void AddAction(FrameAction action, uint value)
        {
            base.Add(new FrameInfo(action, value));
        }
    }

    public class KeyFrame
    {
        // Private
        private TimeSpan _lastUpdate;
        private uint _counter;
        private bool _stopped;
        private bool _reset;

        // Constructor
        public KeyFrame()
        {
            Frames = new FrameCollection();
            Speed = 150;
            ColCount = 1;
            // Reset all
            this.Reset();
        }

        // Methods
        public void Reset()
        {
            _stopped = false;
            _reset = true;
            _counter = 0;
            ImageIndex = 0;
            ActualFrame = 0;
        }
        public void Draw(Sprite sprite, SpriteDrawArgs e)
        {
            if (this.ColCount <= 1)
            {
                Vector2 pos = sprite.Position;
                // Pivot
                switch (sprite.DrawPivot)
                {
                    case SpriteDrawPivot.TopLeft: break; // Nothing
                    case SpriteDrawPivot.Top:
                        pos -= new Vector2(this.Texture.Width / 2, 0);
                        break;
                    case SpriteDrawPivot.TopRight:
                        pos += new Vector2(this.Texture.Width, 0);
                        break;
                    case SpriteDrawPivot.Left:
                        pos += new Vector2(0, this.Texture.Height / 2);
                        break;
                    case SpriteDrawPivot.Center:
                        pos += new Vector2(this.Texture.Width / 2, this.Texture.Height / 2);
                        break;
                    case SpriteDrawPivot.Right:
                        pos -= new Vector2(this.Texture.Width, this.Texture.Height / 2);
                        break;
                    case SpriteDrawPivot.BottomLeft:
                        pos -= new Vector2(0, this.Texture.Height);
                        break;
                    case SpriteDrawPivot.Bottom:
                        pos -= new Vector2(this.Texture.Width / 2, this.Texture.Height);
                        break;
                    case SpriteDrawPivot.BottomRight:
                        pos -= new Vector2(this.Texture.Width, this.Texture.Height);
                        break;
                }
                // Draw
                e.SpriteBatch.Draw(
                    Texture,
                    pos,
                    sprite.Color
                );
            }
            else
            {
                // Dest
                int w = this.Texture.Width / (int)ColCount;
                Rectangle dest = new Rectangle((int)sprite.Position.X, (int)sprite.Position.Y, w, this.Texture.Height);
                switch (sprite.DrawPivot)
                {
                    case SpriteDrawPivot.TopLeft: break; // Nothing
                    case SpriteDrawPivot.Top:
                        break;
                    case SpriteDrawPivot.TopRight:
                        break;
                    case SpriteDrawPivot.Left:
                        break;
                    case SpriteDrawPivot.Center:
                        break;
                    case SpriteDrawPivot.Right:
                        break;
                    case SpriteDrawPivot.BottomLeft:
                        break;
                    case SpriteDrawPivot.Bottom:
                        dest.Offset(-(w / 2), -this.Texture.Height);
                        break;
                    case SpriteDrawPivot.BottomRight:
                        break;
                }
                // DRAW MERMÃO
                e.SpriteBatch.Draw(
                    this.Texture,
                    dest,
                    new Rectangle((int)ImageIndex * w, 0, w, this.Texture.Height),
                    sprite.Color
                    );
            }
        }
        public void Update(Sprite sprite, SpriteUpdateArgs e)
        {
            // Atualizar o counter
            if (this._stopped)
                return;
            // Só tem update se ouver frames pra processar, do contrário nao precisa fazer nada
            if (this.Frames.Count > 0)
            {
                // Testar se esta em modo de reset
                if (!this._reset)
                {
                    long last = (long)this._lastUpdate.TotalMilliseconds;
                    long actual = (long)e.GameTime.TotalGameTime.TotalMilliseconds;
                    this._counter += (uint)(actual - last);
                    // Se o counter for igual ou maior que o speed definido, trocar o quadro
                    if (this._counter >= Speed)
                    {
                        this.ActualFrame = (this.ActualFrame + 1) % (uint)this.Frames.Count;
                        this._counter = this._counter % this.Speed;
                        // Tratar o frame
                        FrameInfo frame = this.Frames[(int)this.ActualFrame];
                        switch (frame.Action)
                        {
                            case FrameAction.ChangeIndex:
                                this.ImageIndex = frame.Value;
                                break;
                            case FrameAction.MoveToKey:
                                sprite.ActualKeyFrame = frame.Value;
                                break;
                            case FrameAction.Stop:
                                this._stopped = true;
                                break;
                        }
                    }
                }
                else
                {
                    // O reset só serve pra atualizar a _lastUpdate, pois senão ela pularia muitos frames caso o keyframe mudasse e retornasse
                    this._reset = false;
                }
                // Atualizar como ultimo update
                _lastUpdate = e.GameTime.TotalGameTime;
            }
        }

        // Properties
        public Texture2D Texture { get; set; }
        public FrameCollection Frames { get; set; }
        public uint ImageIndex { get; set; }
        public uint Speed { get; set; }
        public uint ActualFrame { get; set; }
        public uint ColCount { get; set; }
        public bool IsStopped { get { return this._stopped; } }
    }

    public class KeyFrameCollection : Dictionary<uint, KeyFrame>
    {
        public void Add(uint key, Texture2D texture)
        {
            base.Add(key, new KeyFrame() { Texture = texture });
        }
        public void Add(uint key, Texture2D texture, uint colCount)
        {
            base.Add(key, new KeyFrame() { Texture = texture, ColCount = colCount });
        }
        public void Add(uint key, Texture2D texture, uint colCount, params uint[] frames)
        {
            base.Add(key, new KeyFrame() { Texture = texture, Frames = new FrameCollection(frames), ColCount = colCount });
        }
    }

    public enum SpriteDrawPivot
    {
        // Top
        TopLeft,
        Top,
        TopRight,
        // Center
        Left,
        Center,
        Right,
        // Bottom
        BottomLeft,
        Bottom,
        BottomRight
    }

    public class Sprite
    {
        // Private vars
        private uint _actualKeyFrame;

        // Constructor
        public Sprite()
        {
            this.Color = Color.White;
            this.DrawPivot = SpriteDrawPivot.TopLeft;
            this.KeyFrames = new KeyFrameCollection();
        }

        // Methods
        public virtual void Draw(SpriteDrawArgs e)
        {
            if (KeyFrames.Count == 0)
                return;
            KeyFrames[_actualKeyFrame].Draw(this, e);
        }
        public virtual void Update(SpriteUpdateArgs e)
        {
            KeyFrames[_actualKeyFrame].Update(this, e);
        }

        // Properties
        public uint ActualKeyFrame
        {
            get { return this._actualKeyFrame; }
            set
            {
                //if (this._actualKeyFrame == value)
                //    return;
                // Mudar o keyframe
                /*
                if (value >= this.KeyFrames.Count)
                    this._actualKeyFrame = value - 1;
                else
                    this._actualKeyFrame = value;
                */
                this._actualKeyFrame = value;
                // Resetar as informações do keyframe
                this.KeyFrames[this._actualKeyFrame].Reset();
            }
        }
        public SpriteDrawPivot DrawPivot { get; set; }
        public Color Color { get; set; }
        public KeyFrameCollection KeyFrames { get; protected set; }
        public Vector2 Position { get; set; }
    }
}
