using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Dal
{
    public static class BuildXmlFromDs    
    {
        public static void BuildXmlFromDataSource()
        {

            string localPath;
            string str = Assembly.GetExecutingAssembly().Location;
            localPath = Path.GetDirectoryName(str);
            localPath = Path.GetDirectoryName(localPath);
            //localPath = Path.GetDirectoryName(localPath);

            localPath += @"\xml";
            string productPath =localPath+ @"\ProductXml.xml";
            string orderPath = localPath+@"\OrderXml.xml";
            string orderItemPath = localPath+@"\OrderItemXml.xml";


            creatXmls(DataSource._products, productPath, "Products");
            creatXmls(DataSource._orders, orderPath, "Orders");
            creatXmls(DataSource._orderItems, orderItemPath, "OrderItems");
            SaveListToXMLSerializer(DataSource._orders, orderPath);
            SaveListToXMLSerializer(DataSource._orderItems,orderItemPath);
            SaveListToXMLSerializer(DataSource._products, productPath);
            //SaveListToXMLSerializer(DalObject.Instance.PowerConsumptionByDrone().ToList(), configPath);
        }
        public static XElement CreateElement<T>(T? obj)
        {
            var res = new XElement(obj.GetType().Name);
            foreach (PropertyInfo prop in obj.GetType().GetProperties())
            {
                res.Add(new XElement(prop.Name, prop.GetValue(obj)));
            }
            return res;
        }

        static void creatXmls<T>(List<T> list, string path, string name)
        {
            XElement root = new XElement(name);
            foreach (var item in list)
            {
                root.Add(CreateElement(item));
            }
            root.Save(path);
        }

        public static XElement LoadListFromXMLElement(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    return XElement.Load(filePath);
                }
                else
                {
                    XElement rootElem = new XElement(filePath);
                    rootElem.Save(filePath);
                    return rootElem;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(filePath);
            }
        }

        public static List<T> LoadListFromXMLSerializer<T>(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    List<T> list;
                    XmlSerializer x = new XmlSerializer(typeof(List<T>));
                    FileStream file = new FileStream(filePath, FileMode.Open);
                    list = (List<T>)x.Deserialize(file);
                    file.Close();
                    return list;
                }
                else
                    return new List<T>();
            }
            catch (Exception ex)
            {
                throw new Exception(filePath);
            }
        }

        public static void SaveListToXMLSerializer<T>(List<T> list, string filePath)
        {
            try
            {
                FileStream file = new FileStream(filePath, FileMode.Create);
                XmlSerializer x = new XmlSerializer(list.GetType());
                x.Serialize(file, list);
                file.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(filePath);
            }
        }

        public static void SaveListToXMLElement(XElement rootElem, string filePath)
        {
            try
            {
                rootElem.Save(filePath);
            }
            catch (Exception ex)
            {
                throw new Exception(filePath);
            }
        }
    }
}


        