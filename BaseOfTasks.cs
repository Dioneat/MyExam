using System;
using System.Collections.Generic;
using System.Windows;
using System.Xml;
using System.IO.Compression;
using System.IO;

namespace MyExamApp
{
    public class BaseOfTasks 
    {
        internal static string Subject { get; set; }
        internal static bool isVariantOpen = false;
        internal static string Variant { get; set; }
        internal static List<string> TaskCollection = new List<string>();
        internal static List<string> CorrectAnswer = new List<string>();
        internal static int CountOfPointsInExpandendPart;

        internal static List<string> UserAnswer = new List<string>();
        internal static List<string> Description = new List<string>();
        internal static List<List<string>> Image = new List<List<string>>();


        internal static List<List<string>> DescriptionImage = new List<List<string>>();
        internal static List<string> TextTag = new List<string>
        {
            "i", "sup", "sub", "b"
        };
        public static void SaveTasksInBase(string path, string name)
        {
            try
            {
                using (XmlWriter writer = XmlWriter.Create($"{path}/Варианты/{name}/{name}.xml"))
                {
                    writer.WriteStartElement("variant");
                    for (int i = 0; i < Parser.TaskCollection.Count; i++)
                    {
                        writer.WriteStartElement($"task{i + 1}");
                        WriteOtherVariants(writer, i);
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Отряд обученных обезьян уже отправлен это исправлять.\nИзвините за технические шоколадки" + "\n\n" + $"Лог ошибки: {ex.Message}", "Упс!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        internal static bool CheckerOfActivateVariant()
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите прекратить выполнения заданий?\nВариант не будет учитываться в статистике, а формы будут сброшены", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
                return true;
            else
                return false;
        }
        private static void WriteOtherVariants(XmlWriter writer, int i)
        {
            writer.WriteElementString("text", Parser.TaskCollection[i]);

            writer.WriteElementString("description", Parser.Description[i]);
            
            if (i < Parser.Answer.Count)
            {
                writer.WriteElementString("answer", Parser.Answer[i]);
                for (int j = 0; j < Parser.DescImgDB[i].Count; j++)
                {
                    writer.WriteElementString("desc_img", Parser.DescImgDB[i][j]);
                }
            }
            for (int j = 0; j < Parser.imageDB[i].Count; j++)
            {
                writer.WriteElementString("img", Parser.imageDB[i][j]);
            }

        }
        internal static void ReadTasksFromBase()
        {
            var xDoc = new XmlDocument();
            xDoc.Load($"{Subject}/Варианты/{Variant}.xml");
           
            var xRoot = xDoc.DocumentElement;
            foreach (XmlNode xnode in xRoot.ChildNodes)
            {
                foreach (XmlNode item in xnode.ChildNodes)
                {
                    if (item.Name == "text")
                    {
                        TaskCollection.Add(item.InnerText);
                    }
                }
                    
            }
        }
        internal static void Compress()
        {
            if(File.Exists($"{Subject}\\Варианты\\{Variant}.xml"))
                File.Delete($"{Subject}\\Варианты\\{Variant}.xml");
            
            if (Directory.Exists($"{Subject}\\Варианты\\img_{Variant}"))
                Directory.Delete($"{Subject}\\Варианты\\img_{Variant}", true);
            
            if (Directory.Exists($"{Subject}\\Варианты\\desc_img_{Variant}"))
                Directory.Delete($"{Subject}\\Варианты\\desc_img_{Variant}", true);
                
        }
        internal static void Decompress()
        {
            if (File.Exists($"{Subject}\\Варианты\\{Variant}.xml") is false)
                ZipFile.ExtractToDirectory($"{Subject}\\Варианты\\{Variant}.zip", $"{Subject}\\Варианты");
        }
        internal static void ReadTaskAnswers()
        {
            var xDoc = new XmlDocument();
            xDoc.Load($"{Subject}/Варианты/{Variant}.xml");

            var xRoot = xDoc.DocumentElement;

            foreach (XmlNode xnode in xRoot.ChildNodes)
                foreach (XmlNode item in xnode.ChildNodes)
                    if (item.Name == "answer" && string.IsNullOrWhiteSpace(item.InnerText) is false)
                        CorrectAnswer.Add(item.InnerText);

        }

        internal static void ReadTaskDescriptions()
        {
            var xDoc = new XmlDocument();
            xDoc.Load($"{Subject}/Варианты/{Variant}.xml");

            var xRoot = xDoc.DocumentElement;

            foreach (XmlNode xnode in xRoot.ChildNodes)
            {
                var desc_img = new List<string>();
                foreach (XmlNode item in xnode.ChildNodes)
                {
                    if (item.Name == "description")
                        Description.Add(item.InnerText);
                    else if (item.Name is "desc_img")
                        desc_img.Add(item.InnerText);
                }
                DescriptionImage.Add(desc_img);
            }
 
        }

        internal static void ReadTaskImages()
        {
            var xDoc = new XmlDocument();
            xDoc.Load($"{Subject}/Варианты/{Variant}.xml");

            var xRoot = xDoc.DocumentElement;

            foreach (XmlNode xnode in xRoot.ChildNodes)
            {
                var img = new List<string>();
                foreach (XmlNode item in xnode.ChildNodes)
                {
                    if (item.Name == "img")
                        img.Add(item.InnerText);
                    
                }
                Image.Add(img);
            }
        }
        public static double[] ReadImageSize(string filename)
        {
            var size = new double[2];
            var xDoc = new XmlDocument();
            xDoc.Load(filename);

            var height = xDoc.DocumentElement.GetAttribute("height").Split(new char[] {'.' })[0].Replace("pt", "").Replace("px", "").Replace("mm", "");
            var width = xDoc.DocumentElement.GetAttribute("width").Split(new char[] { '.' })[0].Replace("pt", "").Replace("px", "").Replace("mm", "");
            size[0] = double.Parse(height); size[1] = double.Parse(width);
            return size;
        }   
        public static void LoadResources()
        {
            Decompress();
            ReadTasksFromBase();
            ReadTaskImages();
        }
        public static void ReleaseResources()
        {
            TaskCollection.Clear();
            Image.Clear();
            DescriptionImage.Clear();
            Description.Clear();
            CorrectAnswer.Clear();
        }
    }
}
