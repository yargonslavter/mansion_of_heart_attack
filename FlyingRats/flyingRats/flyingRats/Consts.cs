using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace flyingRats
{
    public class Consts
    {
        public enum TipoDano : int
        {
            Generic = 0,
            Infernal = 1,
            Monstro = 2,
            Beast = 3
        }

        public enum TelaJogo
        {
            Inicio,
            Creditos,
            Jogo,
            GameOver
        }

        public enum TipoTrap : int
        {
            Vampire,
            Mummy,
            Skeleton,
            Death,
            Wraith,
            Mistery
        }

        public enum TipoPesant : int
        {
            Mulher_1 = 0,
            Homem_1 = 1,
            Homem_2 = 2
        }


        public enum Sentido : int
        {
            None = 0,
            Direita = 1,
            Esquerda = 2,
            Cima = 3,
            Baixo = 4
        }

        public enum Eventos
        {
            Baixo,
            bifurcacao,
            bifurcacao1,
            bifurcacao2,
            bifurcacaoT1,
            bifurcacaoT2,
            bifurcacaoT3,
            bifurcacaoT4,
            Cima,
            Direita,
            EndPoint,
            Esquerda,
            PlaceHolder_Baixo,
            PlaceHolder_Cima,
            PlaceHolder_Dir,
            PlaceHolder_Esq,
            SpawnPoint_Baixo,
            SpawnPoint_Cima,
            SpawnPoint_Dir,
            SpawnPoint_Esq,
            WallSlot1,
            WallSlot2,
            WallSlot3,
            WallSlot4,
            bifurcacaoL1,
            bifurcacaoL2,
            bifurcacaoL3,
            bifurcacaoL4,
        }

        public enum PesantState : uint
        {
            Walk_Up = 0,
            Walk_Down = 1,
            Walk_Left = 2,
            Walk_Right = 3,
            Hit = 4,
            HitToDeath = 5,
            Death = 6
        }

        public enum TrapState : uint
        {
            // Static
            Up = 1,
            Down = 0,
            Left = 2,
            Right = 3,
            // Hit
            HitToUp = 4,
            HitToDown = 5,
            HitToLeft = 6,
            HitToRight = 7,
            // Back
            BackUp = 8,
            BackDown = 9,
            BackLeft = 10,
            BackRight = 11
        }

    }
}
