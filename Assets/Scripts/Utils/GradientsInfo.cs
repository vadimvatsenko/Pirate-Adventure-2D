namespace Utils
{
    public class GradientsInfo
    {
        public static string colorTextGradient1 { get; } = "#FFE200FF";
        public static string colorTextGradient2 { get;} = "#FF4400FF";

        private static int valuePercent = 100;
        public static string Value => "Value"; 
        public static int ValuePercent 
        {  
            get 
            { 
                return valuePercent; 
            } 

            private set
            {
                valuePercent = value;
            }
        }
    }
}