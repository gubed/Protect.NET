using System;


namespace Protect.NET.Helpers
{
    class Utils
    {
        private static Random rand = new Random((int)DateTime.Now.Ticks);
        public static string GenerateKey(int length)
        {
            
            string text = string.Empty;
            char[] array = @"qwertyuiopasdfghjklzxcvbnm0123456789~!@#$%^&*()-_[{]};:\',<.>/?\|".ToCharArray();
            for (int i = 0; i < length; i++)
            {
                text += array[rand.Next(0, array.Length)];
            }
            return text;
        }
        public static int randomInt(int min, int max)
        {
            return rand.Next(min, max);
        }
    }
}
