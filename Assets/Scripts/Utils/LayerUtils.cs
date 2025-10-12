using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public static class LayerUtils
    {
        /// Получить все индексы слоёв из LayerMask.
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
        
        /// Получить имена слоёв из LayerMask.
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