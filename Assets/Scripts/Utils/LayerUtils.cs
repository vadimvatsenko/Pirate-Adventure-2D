namespace Utils
{
    using System.Collections.Generic;
    using UnityEngine;

    public static class LayerUtils
    {
        /// <summary>
        /// Получить все индексы слоёв из LayerMask.
        /// </summary>
        public static List<int> GetLayerIndices(LayerMask mask)
        {
            List<int> indices = new List<int>();

            for (int i = 0; i < 32; i++) // максимум 32 слоя
            {
                if ((mask.value & (1 << i)) != 0)
                {
                    indices.Add(i);
                }
            }

            return indices;
        }

        /// <summary>
        /// Получить имена слоёв из LayerMask.
        /// </summary>
        public static List<string> GetLayerNames(LayerMask mask)
        {
            List<string> names = new List<string>();

            foreach (int i in GetLayerIndices(mask))
            {
                string name = LayerMask.LayerToName(i);
                if (!string.IsNullOrEmpty(name))
                    names.Add(name);
            }

            return names;
        }
    }
}