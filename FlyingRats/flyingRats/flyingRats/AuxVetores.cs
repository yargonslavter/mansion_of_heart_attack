using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace flyingRats
{
    public static class AuxVetores
    {
        private static float ForcaGravidade { get; set; }
        private static Vector2 _gravity;
        public static Vector2 GravityVector
        {
            get
            {
                if (_gravity == null || (_gravity.X == 0 && _gravity.Y == 0))
                {
                    if (ForcaGravidade == 0)
                    {
                        ForcaGravidade = 100;
                    }

                    _gravity = NewByAngle(MathHelper.ToRadians(90));
                    _gravity *= ForcaGravidade;
                }
                return _gravity;
            }
        }


        public static Vector2 CriarVetorDireita()
        {
            Vector2 result = NewByAngle(MathHelper.ToRadians(90));
            return result;
        }

        public static Vector2 CriarVetorEsquerda()
        {
            Vector2 result = NewByAngle(MathHelper.ToRadians(270));
            return result;
        }

        public static Vector2 CriarVetorCima()
        {
            Vector2 result = NewByAngle(MathHelper.ToRadians(0));
            return result;
        }

        public static Vector2 CriarVetorBaixo()
        {
            Vector2 result = NewByAngle(MathHelper.ToRadians(180));
            return result;
        }

        public static Vector2 CriarVetorAnguloGraus(float angle)
        {
            Vector2 result = NewByAngle(MathHelper.ToRadians(angle));
            return result;
        }

        public static float CriarAnguloGrausVetor(Vector2 Direction)
        {            
            float angle = NewAngleByDirectionVector(Direction);
            return MathHelper.ToDegrees(angle);
        }

        private static Vector2 NewByAngle(float angle)
        {
            Vector2 result = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
            result.Normalize();
            return result;
        }

        private static float NewAngleByDirectionVector(Vector2 Direction)
        {
            Direction.Normalize();
            return (float)Math.Atan2(Direction.Y, Direction.X);
        }


        internal static float CriarAnguloRadiansVetor(Vector2 Direction)
        {
            return NewAngleByDirectionVector(Direction);
        }

        static public Vector2 SomaVetores(Vector2 v1, Vector2 v2)
        {
            return v1 + v2;
        }

        static public Vector2 SubtraiVetores(Vector2 v1, Vector2 v2)
        {
            return v1 - v2;
        }

        static public Vector2 NormalizaVetor(Vector2 v1)
        {
            v1.Normalize();
            return v1;
        }

    }

}
