using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dark.Core.Extension
{
    public static class XMLEntension
    {
        /// <summary>
        /// 将对象保存为XML文档
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="rootNodeName"></param>
        /// <returns></returns>
        public static string ParseXml<T>(this T entity)
        {
            if (entity == null) return string.Empty;

            //通过反射得到对象的所有属性
            Type type = typeof(T);
            PropertyInfo[] props = type.GetProperties();
            XElement rootElement = new XElement(type.Name);
            foreach (var item in props)
            {
                XElement proElement = new XElement(item.Name, item.GetValue(entity, null));
                rootElement.Add(proElement);
            }
            XDocument xDoc = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                rootElement
            );
            return xDoc.ToString();
        }

        /// <summary>
        /// 将list 集合转换为xml格式字段
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="fatherName"></param>
        /// <returns></returns>
        public static string ParseXml<T>(this List<T> list, string fatherName = "")
        {
            if (list.Count() == 0) return string.Empty;
            Type type = typeof(T);
            if (string.IsNullOrEmpty(fatherName))
            {
                fatherName = "list-" + type.Name;
            }
            XElement xRoot = new XElement(fatherName);
            //创建节点
            foreach (var item in list)
            {
                XElement xe = new XElement(type.Name);
                foreach (var property in type.GetProperties())
                {
                    xe.Add(new XElement(property.Name, property.GetValue(item)));
                }
                xRoot.Add(xe);
            }
            return xRoot.ToString();
        }
        /// <summary>
        /// 将字符串格式转换为类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strXml"></param>
        /// <returns></returns>
        public static T XmlToModel<T>(this string strXml) where T : class, new()
        {
            T model = new T();
            if (string.IsNullOrEmpty(strXml))
            {
                return default(T);
            }

            XElement xElement = XElement.Parse(strXml);

            Type type = typeof(T);

            foreach (var xe in xElement.Elements())
            {
                //通过name找到属性,接着赋值
                PropertyInfo property = type.GetProperty(xe.Name.ToString());
                if (property == null) continue;
                property.SetValue(model, Convert.ChangeType(xe.Value, property.PropertyType));
            }

            return model;
        }

        /// <summary>
        /// 将xml文档转换为 list集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strXml"></param>
        /// <returns></returns>
        public static List<T> XmlToList<T>(this string strXml) where T : class, new()
        {
            if (string.IsNullOrEmpty(strXml)) return null;

            List<T> list = new List<T>();
            XElement xe = XElement.Parse(strXml);
            //接着找节点
            Type type = typeof(T);
            //遍历 类名为type.Name的节点
            foreach (var xeType in xe.Elements(type.Name))
            {
                T model = new T();
                //设置属性
                foreach (var item in xeType.Elements())
                {
                    //通过name找到属性,接着赋值
                    PropertyInfo property = type.GetProperty(item.Name.ToString());
                    if (property == null) continue;
                    property.SetValue(model, Convert.ChangeType(item.Value, property.PropertyType));
                }
                list.Add(model);
            }
            return list;
        }
    }
}
