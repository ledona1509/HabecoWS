using System;
using System.IO;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace Helper
{
    /// <summary>
    /// Help to Save OR Load object from xml file
    /// </summary>
    public class SerializeObjectHelper
    {
        public SerializeObjectHelper(){}

        /// <summary>
        /// Serializes an object.
        /// </summary>
        /// <typeparam name="T">Generic type</typeparam>
        /// <param name="serializableObject">serializableObject</param>
        /// <param name="filePath">File path</param>
        public static void SerializeObject<T>(T serializableObject, string filePath)
        {
            if (serializableObject == null)
            {
                return;
            }
            try
            {
                var xmlDocument = new XmlDocument();
                var serializer = new XmlSerializer(serializableObject.GetType());
                using (var stream = new MemoryStream())
                {
                    serializer.Serialize(stream, serializableObject);
                    stream.Position = 0;
                    xmlDocument.Load(stream);
                    xmlDocument.Save(HttpContext.Current.Server.MapPath(filePath));
                    stream.Close();
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// Deserializes an xml file into an object list
        /// </summary>
        /// <typeparam name="T">Generic type</typeparam>
        /// <param name="filePath">File path</param>
        /// <returns>Generic type</returns>
        public static T DeserializeObject<T>(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return default(T);
            }
            var objectOut = default(T);
            try
            {
                var xmlDocument = new XmlDocument();
                xmlDocument.Load(HttpContext.Current.Server.MapPath(filePath));
                var xmlString = xmlDocument.OuterXml;
                using (var read = new StringReader(xmlString))
                {
                    var outType = typeof(T);
                    var serializer = new XmlSerializer(outType);
                    using (var reader = new XmlTextReader(read))
                    {
                        objectOut = (T)serializer.Deserialize(reader);
                        reader.Close();
                    }
                    read.Close();
                }
            }
            catch (Exception ex)
            {

            }
            return objectOut;
        }
    }
}
