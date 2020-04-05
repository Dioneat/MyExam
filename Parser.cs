using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net;
using System.IO.Compression;
using HtmlAgilityPack;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyExamApp
{
    class Parser
    {
        internal static List<string> TaskCollection = new List<string>();
        internal static List<string> Description = new List<string>();
        internal static List<string> Answer = new List<string>();
        internal static Dictionary<string, string> NameSubjects = new Dictionary<string, string>();
        internal static List<List<string>> imageDB = new List<List<string>>();
        internal static List<List<string>> DescImgDB = new List<List<string>>();
        internal static Task Parsing(bool ZipArchiving, bool parseReshuEGE)
        {
            if (parseReshuEGE is true)
                return Task.Run(() => Parse(ZipArchiving));
            else
                return Task.Run(() => ParseYandexLessons(ZipArchiving));
        }
        private static void ParseYandexLessons(bool ZipArchiving)
        {

        }
        private static void Parse(bool ZipArchiving)
        {
            try
            {
                var doc = new HtmlDocument();
                var web = new WebClient
                {
                    Encoding = Encoding.UTF8
                };
                if (NameSubjects.Count != 0)
                {
                    foreach (var subject in NameSubjects)
                    {
                        string[] files;
                        int numVariant = 1;
                        CreateSubjectFolder(subject.Key, numVariant);
                        files = subject.Key is "Базовый уровень" | subject.Key is "Профильный уровень" ?
                            Directory.GetFiles($@"{Directory.GetCurrentDirectory()}\\Математика\\{subject.Key}\\Варианты", "*.zip", SearchOption.AllDirectories) : Directory.GetFiles($@"{Directory.GetCurrentDirectory()}\\{subject.Key}\\Варианты", "*.zip", SearchOption.AllDirectories);

                        if (files.Length != 15)
                        {
                            if (subject.Key == "Итоговое сочинение")
                                doc.LoadHtml(web.DownloadString("https://rus-ege.sdamgia.ru/page/it_soch05"));
                            else
                                doc.LoadHtml(web.DownloadString($"https://{subject.Value}-ege.sdamgia.ru/"));
                            if (subject.Key == "Итоговое сочинение")
                            {
                                // TODO: work
                            }
                            else
                            {
                                var linkToVariant = new List<string>();
                                var nodes = doc.DocumentNode.SelectNodes("//html/body/div[1]/div[5]/div[2]/table").ToList()[0].InnerHtml.Trim(new char[] { '"' });
                                string[] href = nodes.Split(new string[] { "<a href=", ">" }, StringSplitOptions.None);
                                foreach (var link in href)
                                {
                                    if (link.Contains("/test?"))
                                        linkToVariant.Add(link);

                                }

                                for (int i = 0; i < linkToVariant.Count; i++)
                                {
                                    string name = $"variant{i + 1}";
                                    linkToVariant[i] = linkToVariant[i].Trim(new char[] { '"' });
                                    doc.LoadHtml(web.DownloadString($"https://{subject.Value}-ege.sdamgia.ru{linkToVariant[i]}"));

                                    CreateSubjectFolder(subject.Key, numVariant);
                                    ParsingOtherSubjectAgility(doc, i, subject, web);
                                    if (subject.Key == "Базовый уровень" || subject.Key == "Профильный уровень")
                                        BaseOfTasks.SaveTasksInBase($"Математика/{subject.Key}", name);
                                    else
                                        BaseOfTasks.SaveTasksInBase(subject.Key, name);
                                    Garbage_Collector();
                                    if (ZipArchiving is true)
                                    {
                                        if (subject.Key == "Базовый уровень" || subject.Key == "Профильный уровень")
                                            ZipFile.CreateFromDirectory($"Математика/{subject.Key}/Варианты/variant{i + 1}", $"Математика/{subject.Key}/Варианты/variant{i + 1}.zip");
                                        else
                                            ZipFile.CreateFromDirectory($"{subject.Key}/Варианты/variant{i + 1}", $"{subject.Key}/Варианты/variant{i + 1}.zip");
                                    }

                                    numVariant++;
                                    TaskCollection.Clear();
                                    Description.Clear();
                                    Answer.Clear();
                                    DescImgDB.Clear();
                                    imageDB.Clear();
                                }
                            }
                            for (int i = 1; i <= 15; i++)
                            {
                                if (subject.Key == "Базовый уровень" || subject.Key == "Профильный уровень")
                                    Directory.Delete($"Математика/{subject.Key}/Варианты/variant{i}", true);
                                else
                                    Directory.Delete($"{subject.Key}/Варианты/variant{i}", true);
                            }

                        }


                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        private static void ParsingOtherSubjectAgility(HtmlDocument doc, int variant, KeyValuePair<string, string> subject, WebClient web)
        {
            var listImages = new List<List<string>>();
            var descListImages = new List<List<string>>();
            var TaskText = new List<string>();
            var links = new List<string>();
            foreach (var link in doc.DocumentNode.SelectNodes("//span[@class = 'prob_nums']/a[@href]"))
            {
                links.Add(link.InnerText);
            }
            for (int i = 0; i < links.Count; i++)
            {
                var img = new List<string>();
                var desc_img = new List<string>();
                var link = links[i];
                doc.LoadHtml(web.DownloadString($"https://{subject.Value}-ege.sdamgia.ru/problem?id={link}"));


                var pbody = doc.DocumentNode.SelectNodes("//div[@class = 'pbody']");
                string probtext = "";
                if(subject.Key is "Русский язык")
                    probtext = doc?.DocumentNode?.SelectNodes("//div[@class = 'probtext']")?[0]?.InnerText;

                var imgFromHtml = pbody[0].Descendants("img").ToList();
                foreach (var src in imgFromHtml)
                {
                    img.Add(src.GetAttributeValue("src", " "));
                }
                if(subject.Key is "Русский язык")
                    TaskText.Add(ParserReplacer.HTMLtoText(pbody[0].InnerHtml + "\n<probtext>\n" + probtext, new List<HtmlNode>(pbody[0].Descendants("img"))));
                else
                    TaskText.Add(ParserReplacer.HTMLtoText(pbody[0].InnerHtml, new List<HtmlNode>(pbody[0].Descendants("img"))));
                Description.Add(ParserReplacer.HTMLtoText(doc.GetElementbyId($"sol{link}").InnerHtml, new List<HtmlNode>(pbody[1].Descendants("img"))));
                var descImgFromHtml = doc.GetElementbyId($"sol{link}").Descendants("img").ToList();
                foreach (var src in descImgFromHtml)
                {
                    desc_img.Add(src.GetAttributeValue("src", " "));
                }
                string answer = doc?.DocumentNode?.SelectSingleNode("//div[@class= 'answer']/span")?.InnerText?.Replace("Ответ: ", "");
                Answer.Add(answer);
                listImages.Add(img);
                descListImages.Add(desc_img);
            }

            TaskCollection.AddRange(TaskText);
            DownloadImage(listImages, subject, "img");
            DownloadImage(descListImages, subject, "desc_img");

            string foldersName = $"img_variant{variant + 1}";
            var dirInfo = new DirectoryInfo(foldersName);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }

            foreach (var image in imageDB)
            {
                for (int i = 0; i < image.Count; i++)
                {
                    string file = Path.GetFileName(image[i]);
                    string newFilePath = $@"{Directory.GetCurrentDirectory()}\{foldersName}";
                    File.Move(image[i], newFilePath + "\\" + file);
                }

            }

            string oldPath = $@"{Directory.GetCurrentDirectory()}\{foldersName}";
            string newPath;
            newPath = subject.Key.Equals("Базовый уровень") || subject.Key.Equals("Профильный уровень") ? 
                $@"{Directory.GetCurrentDirectory()}\Математика\{subject.Key}\Варианты\variant{variant + 1}\{foldersName}" : $@"{Directory.GetCurrentDirectory()}\{subject.Key}\Варианты\variant{variant + 1}\{foldersName}";

            Directory.Move(oldPath, newPath);

            foldersName = $"desc_img_variant{variant + 1}";
            dirInfo = new DirectoryInfo(foldersName);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
            foreach (var image in DescImgDB)
            {
                for (int i = 0; i < image.Count; i++)
                {
                    string file = Path.GetFileName(image[i]);
                    string newFilePath = $@"{Directory.GetCurrentDirectory()}\{foldersName}";
                    File.Move(image[i], newFilePath + "\\" + file);
                }
            }
            oldPath = $@"{Directory.GetCurrentDirectory()}\{foldersName}";
            newPath = subject.Key.Equals("Базовый уровень") || subject.Key.Equals("Профильный уровень") ?
                $@"{Directory.GetCurrentDirectory()}\Математика\{subject.Key}\Варианты\variant{variant + 1}\{foldersName}" : $@"{Directory.GetCurrentDirectory()}\{subject.Key}\Варианты\variant{variant + 1}\{foldersName}";
            
            Directory.Move(oldPath, newPath);
                        
        }

        private static void DownloadImage(List<List<string>> images, KeyValuePair<string, string> subject, string filename)
        {
            
            for (int i = 0; i < images.Count; i++)
            {
                var img = new List<string>();
                for (int j = 0; j < images[i].Count; j++)
                {
                    string saveloc = $"{filename}{i+1}_{j+1}";
                    using (var wc = new WebClient())
                    {
                        byte[] fileBytes;
                        fileBytes = images[i][j].Contains("https") ?
                            wc.DownloadData(images[i][j]) : wc.DownloadData($"https://{subject.Value}-ege.sdamgia.ru{images[i][j]}");
                        string fileType = wc.ResponseHeaders[HttpResponseHeader.ContentType];

                        if (fileType != null)
                        {
                            switch (fileType)
                            {
                                case "image/jpeg":
                                    saveloc += ".jpg";
                                    break;
                                case "image/svg+xml":
                                    saveloc += ".svg";
                                    break;
                                case "image/png":
                                    saveloc += ".png";
                                    break;
                            }

                            File.WriteAllBytes(saveloc, fileBytes);
                            img.Add(saveloc);
                        }
                        wc.Dispose();
                    }
                }
                if(filename is "img")
                    imageDB.Add(img);
                else
                    DescImgDB.Add(img);
                    
            }

        }

        private static void CreateSubjectFolder(string subject, int numVariant)
        {
            if (subject.Equals("Базовый уровень") || subject.Equals("Профильный уровень"))
            {
                var directoryInfo = new DirectoryInfo("Математика");
                if (directoryInfo.Exists is false)
                    directoryInfo.Create();

                var subdirectory = new DirectoryInfo($"Математика/{subject}");
                var directory = subdirectory.GetFiles();
                if(subdirectory.Exists is false || directory.Length.Equals(0))
                {
                    subdirectory.Create();
                    var folders = new DirectoryInfo($"Математика/{subject}");
                    folders.CreateSubdirectory("Все задания");
                    folders.CreateSubdirectory("Варианты");

                    
                }
                var variantsDirectory = new DirectoryInfo($"Математика/{subject}/Варианты/variant{numVariant}");
                if (variantsDirectory.Exists is false)
                    variantsDirectory.Create();
            }
            else
            {
                var directory = new DirectoryInfo(subject);
                if (directory.Exists is false)
                {
                    directory.Create();
                    directory.CreateSubdirectory("Все задания");
                    directory.CreateSubdirectory("Варианты");

                }
                var variantsDirectory = new DirectoryInfo($"{subject}/Варианты/variant{numVariant}");
                if (variantsDirectory.Exists is false)
                    variantsDirectory.Create();
            }
        }
        private static void Garbage_Collector()
        {
            string[] PNGfiles = Directory.GetFiles(Directory.GetCurrentDirectory()).Where(x => x.EndsWith(".svg") | x.EndsWith(".png") | x.EndsWith(".mp3")).ToArray();
            for (int i = 0; i < PNGfiles.Length; i++)
            {
                FileInfo file = new FileInfo(PNGfiles[i]);
                string path = file.FullName;
                File.Delete(path);
            }
        }
    }
    
}


    


