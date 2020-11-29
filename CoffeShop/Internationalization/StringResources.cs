using System;
using System.Windows;
using static CoffeShop.Enums.ALL_ENUM;

namespace CoffeShop.Internationalization
{
    public class StringResources
    {
        private static string ClassName = "StringResources";
        private static ResourceDictionary Dict = new ResourceDictionary();
        public static int curLanguage = -1;
        public static string Find(string key)
        {
            return Dict.Contains(key) ? Dict[key].ToString() : "";
        }

        public static string Find(string key, params object[] list)
        {
            if (list.Length > 0)
                return Dict.Contains(key) ? string.Format(Dict[key].ToString(), list) : "";
            return Dict.Contains(key) ? Dict[key].ToString() : "";
        }

        public static void ApplyLanguage(LANGUAGE language)
        {
            if (Dict == null)
                Dict = new ResourceDictionary();
            if (curLanguage == (int)language)
                return;

            curLanguage = (int)language;

            if (Application.Current.Resources.MergedDictionaries.Contains(Dict))
                Application.Current.Resources.MergedDictionaries.Remove(Dict);
            switch (language)
            {
                case LANGUAGE.EN:
                    Dict.Source = new Uri("Internationalization\\StringResource_EN.xaml", UriKind.Relative);
                    break;
                case LANGUAGE.VN:
                    Dict.Source = new Uri("Internationalization\\StringResource_VI.xaml", UriKind.Relative);//Vietnamese
                    break;
                default: break;
            }
            Application.Current.Resources.MergedDictionaries.Add(Dict);
        }
    }
}
