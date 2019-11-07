using System;
using SquidCraft.Math;

namespace SquidCraft.MoLang
{
    public class MoLang
    {
        public static readonly MoLangObject MathObject = new MoLangObject(MoLangAccess.Read);

        static MoLang()
        {
            MathObject.AddFunction<float, float>("abs", MathF.Abs);
            MathObject.AddFunction<float, float>("sin", MathF.Sin);
            MathObject.AddFunction<float, float>("cos", MathF.Cos);
            MathObject.AddFunction<float, float>("exp", MathF.Exp);
            MathObject.AddFunction<float, float>("ln", MathF.Log);
            MathObject.AddFunction<float, float, float>("pow", MathF.Pow);
            MathObject.AddFunction<float, float, float>("pow", MathF.Pow);
            MathObject.AddFunction<float, float>("sqrt", MathF.Sqrt);
            MathObject.AddFunction<float, float>("random", MathF.Sqrt);
            MathObject.AddFunction<float, float>("ceil", MathF.Ceiling);
            MathObject.AddFunction<float, float>("round", MathF.Round);
            MathObject.AddFunction<float, float>("trunc", MathF.Truncate);
            MathObject.AddFunction<float, float>("floor", MathF.Floor);
            MathObject.AddFunction<float, float, float>("mod", (f1, f2) => f1 % f2);
            MathObject.AddFunction<float, float, float>("min", MathF.Min);
            MathObject.AddFunction<float, float, float>("max", MathF.Max);
            MathObject.AddFunction<float, float, float, float>("clamp", System.Math.Clamp);
            MathObject.AddFunction<float, float, float, float>("lerp", MathHelper.Lerp);
            MathObject.AddFunction<float, float, float, float>("lerprotate", MathHelper.LerpAngle);
        }
    }
}