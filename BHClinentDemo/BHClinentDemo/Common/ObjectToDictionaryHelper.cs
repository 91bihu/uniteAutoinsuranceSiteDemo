using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BHClinentDemo.Common
{
    public static class ObjectToDictionaryHelper
    {
        //public static IDictionary<string, object> ToDictionary(this object source)
        //{
        //    return source.ToDictionary<object>();
        //}

        /// <summary>
        /// 将实体数据转换为Dictionary对象， 再转换为queryString
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToQueryString(this object source)
        {
            if (source == null)
            {
                ThrowExceptionWhenSourceArgumentIsNull();
            }

            return source.ToDictionary().ToQueryString();
        }

        public static Dictionary<string, string> ToDictionary(this object source, TransformSetting setting = TransformSetting.NoNullAndBeValue)
        {
            if (source == null)
            {
                ThrowExceptionWhenSourceArgumentIsNull();
            }

            var dictionary = new Dictionary<string, string>();

            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(source))
            {
                AddPropertyToDictionary(property, source, dictionary, setting);
            }

            return dictionary;
        }

        private static void AddPropertyToDictionary(
            PropertyDescriptor property,
            object source,
            Dictionary<string, string> dictionary,
            TransformSetting setting)
        {
            object value = property.GetValue(source);
            if (value != null)
            {
                if (setting == TransformSetting.NoNullAndBeValue)
                {
                    FittingDic(property, dictionary, value);
                }

                if (setting == TransformSetting.NoNullAndValue)
                {
                    FittingValueDic(property, dictionary, value);
                }

                if (setting == TransformSetting.NullAndValue)
                {
                    FittingValueDic(property, dictionary, value);
                }

                
            }
        }

        private static void FittingDic(PropertyDescriptor property, Dictionary<string, string> dictionary, object value)
        {
            if (property.PropertyType == typeof(string))
            {
                if (!string.IsNullOrWhiteSpace(value.ToString().Trim()))
                {
                    dictionary.Add(property.Name, value.ToString().Trim());
                }
            }

            if (property.PropertyType == typeof(int))
            {
                int v = Convert.ToInt32(value);
                if (v > 0)
                {
                    dictionary.Add(property.Name, value.ToString());
                }
            }
            if (property.PropertyType == typeof(double))
            {
                double v = Convert.ToDouble(value);
                if (v > 0)
                {
                    dictionary.Add(property.Name, v.ToString());
                }
            }
            if (property.PropertyType == typeof(Int64))
            {
                long v = Convert.ToInt64(value);
                if (v > -1)
                {
                    dictionary.Add(property.Name, v.ToString());
                }
            }
        }


        private static void FittingValueDic(PropertyDescriptor property, Dictionary<string, string> dictionary, object value)
        {
            if (property.PropertyType == typeof(string))
            {
                if (!string.IsNullOrWhiteSpace(value.ToString().Trim()))
                {
                    dictionary.Add(property.Name, value.ToString().Trim());
                }
            }

            if (property.PropertyType == typeof(int))
            {
                int v = Convert.ToInt32(value);
                dictionary.Add(property.Name, value.ToString());
            }
            if (property.PropertyType == typeof(double))
            {
                double v = Convert.ToDouble(value);
                dictionary.Add(property.Name, v.ToString());
            }
            if (property.PropertyType == typeof(Int64))
            {
                long v = Convert.ToInt64(value);
                dictionary.Add(property.Name, v.ToString());
            }
        }


        private static bool IsOfType<T>(object value)
        {
            return value is T;
        }

        private static void ThrowExceptionWhenSourceArgumentIsNull()
        {
            throw new ArgumentNullException("source", "Unable to convert object to a dictionary. The source object is null.");
        }
    }

    /// <summary>
    /// Object To Dictionary
    /// </summary>
    public enum TransformSetting
    {
        /// <summary>
        /// 非空并且大于零
        /// </summary>
        NoNullAndBeValue,

        /// <summary>
        /// 过滤空，不过滤值
        /// </summary>
        NoNullAndValue,

        /// <summary>
        /// 包含空和一般值
        /// </summary>
        NullAndValue
    }
}