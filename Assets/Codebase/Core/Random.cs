﻿namespace Codebase.Core
{
    public class Random : IRandom
    {
        public float Range(float min, float max)
        {
            return UnityEngine.Random.Range(min, max);
        }

        public int Range(int min, int max)
        {
            return UnityEngine.Random.Range(min, max);
        }
    }
}