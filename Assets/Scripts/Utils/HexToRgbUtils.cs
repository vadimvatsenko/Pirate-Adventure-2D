using UnityEngine;

namespace Utils
{
    public class HexToRgbUtils
    {
        public static Color HexToRGB(string hex)
        {
            if (ColorUtility.TryParseHtmlString(hex, out Color color))
            {
                return color;
            }
            else
            {
                return Color.black; 
            }
        }
    }
}