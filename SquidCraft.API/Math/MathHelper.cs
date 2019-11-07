using System;

namespace SquidCraft.Math
{
    public static class MathHelper
    {
        public const float DegreesToRadians = MathF.PI / 180f;
        public const float RadiansToDegrees = 180f / MathF.PI;

        public static float Clamp01(float value)
        {
            return value > 1 ? 1 : value < 0 ? 0 : value;
        }

        public static int RoundUp(int number, int interval)
        {
            if (interval == 0)
                return 0;

            if (number == 0)
                return interval;

            if (number < 0)
                interval *= -1;

            var i = number % interval;
            return i == 0 ? number : number + interval - i;
        }

        public static bool AreRoughlyTheSame(float a, float b, float threshold = float.Epsilon)
        {
            return MathF.Abs(a - b) < threshold;
        }

        public static float Lerp(float start, float end, float value)
        {
            return start + (end - start) * Clamp01(value);
        }

        public static float LerpAngle(float start, float end, float value)
        {
            return start + ShortestAngleDistance(start, end) * Clamp01(value);
        }

        private static float ShortestAngleDistance(float start, float end)
        {
            const float max = MathF.PI * 2;
            var delta = (end - start) % max;
            return 2 * delta % max - delta;
        }
    }
}