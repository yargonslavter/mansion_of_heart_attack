using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using flyingRats.Interfaces;

namespace flyingRats
{
    public class EventPoint
    {
        public Consts.Eventos tipoEvento;
        public Vector2 Posicao;
        public Consts.Sentido SentidoAtual;
        public List<Consts.Sentido> Sentidos = new List<Consts.Sentido>();
        public Rectangle recEventPoint;
        public Vector2 PosicaoCentral;
        public bool HolderOcupado = false;
    }
}
