using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CoffeShop.ExtentionCommon
{
    public static class MyExtention
    {
        public static T CloneData<T>(this object source)
        {
            var target = (T)Activator.CreateInstance(typeof(T));

            Type objTypeBase = source.GetType();
            Type objTypeTarget = target.GetType();

            PropertyInfo _propinfo = null;
            var propInfos = objTypeBase.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (var propInfo in propInfos)
            {
                try
                {
                    _propinfo = objTypeTarget.GetProperty(propInfo.Name, BindingFlags.Instance | BindingFlags.Public);
                    if (_propinfo != null)
                    {
                        _propinfo.SetValue(target, propInfo.GetValue(source));
                    }
                }
                catch (ArgumentException aex) { if (!string.IsNullOrEmpty(aex.Message)) continue; }
                catch (Exception ex) { if (!string.IsNullOrEmpty(ex.Message)) return default(T); }
            }
            return target;
        }
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
        public static T GetAttributeFrom<T>(this object instance, string propertyName) where T : Attribute
        {
            var attrType = typeof(T);
            var property = instance.GetType().GetProperty(propertyName);
            return (T)property.GetCustomAttributes(attrType, false).First();
        }
        public static string ConvertImageToBase64(string path)
        {
            byte[] imageArray = System.IO.File.ReadAllBytes(path);
            return Convert.ToBase64String(imageArray);
        }
        public static BitmapImage Base64ToImageSource(string dataBase64)
        {
            byte[] binaryData = Convert.FromBase64String(dataBase64);
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.StreamSource = new MemoryStream(binaryData);
            bi.EndInit();
            return bi;
        }
    }
    public static class ObjectCopier
    {
        public static T Clone<T>(this T source)
        {
            if (Object.ReferenceEquals(source, null))
            {
                return default(T);
            }

            var deserializeSettings = new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace };

            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(source), deserializeSettings);
        }
    }
    
}
