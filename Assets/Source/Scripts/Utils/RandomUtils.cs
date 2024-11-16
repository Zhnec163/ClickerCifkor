using UnityEngine;

namespace Scripts.Utils
{
    public static class RandomUtils
    {
        public static Vector3 GetRandomDirection() =>
            new Vector3(Random.Range(-1F, 1F), Random.Range(-1F, 1F)).normalized;
    }
}