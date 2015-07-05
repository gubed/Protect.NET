using System;
using System.Text;

namespace Protect.NET.Helpers
{
    public static class Generator
    {
        public static sType type = sType.Foreign;
        public enum sType
        {
            Standard,
            Foreign,
            Normal,
            Special,
            Underscore,
            Unreadable
        }
        public static Random rand = new Random();
        public static string Main = "";
        public static string Module = "";
        public static string ModuleConstructor = "";
        public static string getName()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(randomString(rand.Next(3, 10), type));
            return stringBuilder.ToString();
        }
        private static string GenerateString(int length, int minCharCode, int maxCharCode)
        {
            StringBuilder stringBuilder = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                stringBuilder.Append((char)rand.Next(minCharCode, maxCharCode));
            }
            return stringBuilder.ToString();
        }
        public static string getMainName()
        {
            Main = getName();
            return Main;
        }
        public static string getModuleName()
        {
            Module = getName();
            return Module;
        }
        public static string getConstructorName()
        {
            ModuleConstructor = getName();
            return ModuleConstructor;
        }

        public static string randomString(int len, sType s)
        {
            string text;
            switch (s)
            {
                case sType.Standard:
                    text = "₠₡₢₣₤₥₦₧₨₩₹₸₷₶₵₴₳₲₱₰₯₮₭€₫₺⃰₽℅ℓ№™ⱠⱡⱢⱣⱤⱥⱦⱧⱩﬁⱻⱾⱿⱯⱮⱱⱲⱳⱴⱵƩƱƲƳƴǍǎǢǣǤǥǭȄȜȞȣȮփռպֆӸӂҿҧ";
                    break;
                case sType.Special:
                    text = "ƀƁƂƄƅƆƈƉƋƍƎƏƐƑƒƓƔƕƖƗƘƙƜƛơƣƥƪƩƱƲƳƴǍǎǢǣǤǥǭȄȜȞȣȮփռպֆӸӂҿҧͲͳʹͶͷͻͼͽΆΙΘΗΖΕΔΓΒΑΐΏΎΌΉΈΛάέήίΰαβϡϠϝϞϜϛϦФЯЮПЎЍЌЫЬЭЮйЩШЙЉЊϪϚѤѬѭѮѰѹ҈҉ҘѾүҾӁ";
                    break;
                case sType.Foreign:
                    text = "䂥䂀䂁䂀䂀䁿䁾䁽䂌䂋䂎䃞䃝䃜䃛䃫䃬䃮䃰䃱䃲䃳䃴䃵䃶䃷䃸䃹䃺䃪䃩䃙䄣䄢䄡䄠䄟䒐䒏䒎䒍䒌䒋䑬䑽䑼䑾䒀䒂䑳䄞䄝䄜䄛䄋䃻䃫䃬䃭䃮䃯䃰䃱䃲䃳䃴䃵䃶䃷䃸䃹䃺䆴䇀䇁䆱䇟䇮䇯䇱䇝䇜䇍䉶䉱䉲䉢䉣䊃䊅䊖䊦䊥䊐䊞䊟䌵䌴䌳䍦䍥䍷䍷䍹";
                    break;
                case sType.Normal:
                    text = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    break;
                case sType.Unreadable:
                    text = "\u202e\u202f\u202a";
                    break;
                default:
                    throw new InvalidOperationException();
            }
            char[] array = new char[len];
            for (int i = 0; i < len; i++)
            {
                array[i] = text[rand.Next(text.Length)];
            }
            return new string(array);
        }
        public static int randomInt(int Length)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i <= Length; i++)
            {
                stringBuilder.Append(rand.Next(0, 9).ToString());
            }
            string value = stringBuilder.ToString();
            return Convert.ToInt32(value);
        }
    }
}
