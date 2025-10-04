using System;

namespace Assets.CubeRain.CodeBase.Infrastructure.Utils.Randomization
{
    public static class Randomizer
    {
        private static Random s_generator = new Random();

        public static float GetRandomFloat(float min = 0, float max = 1) 
        {
            return (float) s_generator.NextDouble() * (max - min) + min;
        }
    }
}
