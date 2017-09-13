using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Dark.Core.Serializer
{
    class SerializeHelper
    {

        /// <summary>
        /// 二进制序列化器
        /// </summary>
        public static byte[] BinarySerialize<T>(T entity)
        {

            MemoryStream ms = new MemoryStream();
            //创建内存流对象            
            try
            {
                IFormatter formatter = new BinaryFormatter();
                //定义BinaryFormatter以序列化DataSet对象                  
                formatter.Serialize(ms, entity);
                //把DataSet对象序列化到内存流              
                return ms.GetBuffer();
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                ms.Close();//关闭内存流对象          
                ms.Dispose();//释放资源          
            }
        }

        /// <summary>
        /// 将二进制流转换为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static T BinaryDeserialize<T>(byte[] data)
        {
            if (data.Length == 0)
            {
                return default(T);
            }
            try
            {
                IFormatter formatter = new BinaryFormatter();//定义BinaryFormatter以序列化DataSet对象 
                MemoryStream ms = new MemoryStream(data);//创建内存流对象 
                object obj = formatter.Deserialize(ms);//把DataSet对象序列化到内存流  
                ms.Close();//关闭内存流对象                
                ms.Dispose();//释放资源               
                return (T)obj;
            }
            catch
            {
                return default(T);
            }
        }
        //BinaryFormatter序列化自定义类的对象时，序列化之后的流中带有空字符，以致于无法反序列化，反序列化时总是报错“在分析完成之前就遇到流结尾”（已经调用了stream.Seek(0, SeekOrigin.Begin);）。
        //改用XmlFormatter序列化之后，可见流中没有空字符，从而解决上述问题，但是要求类必须有无参数构造函数，而且各属性必须既能读又能写，即必须同时定义getter和setter，若只定义getter，则反序列化后的得到的各个属性的值都为null。

    }
}
