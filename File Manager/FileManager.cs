using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace File_Manager
{
    public class FileManager
    {
        public void Information(string path)
        {
            FileInfo fileInf = new FileInfo(path);
            DirectoryInfo dirInf = new DirectoryInfo(path);
            try
            {
                if (dirInf.Exists)
                {
                    Console.WriteLine(string.Format("Information about directory:\nName: {0}\nSize: {1} bytes\nLast modified on {2}\nCreation time on {3}",
                        dirInf.Name, dirInf.EnumerateFiles("*", SearchOption.AllDirectories).Sum(fi => fi.Length), dirInf.LastWriteTime, dirInf.CreationTime));
                }
                else if (fileInf.Exists)
                {
                    Console.WriteLine(string.Format("Information about file:\nName: {0}\nSize: {1} bytes\nLast modified on {2}\nCreation time on {3}",
                        fileInf.Name, fileInf.Length, fileInf.LastWriteTime, fileInf.CreationTime));
                }
                else
                {
                    Console.WriteLine("Папка или файл не найдены!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void Remove(string path)
        {
            FileInfo fileInf = new FileInfo(path);
            DirectoryInfo dirInf = new DirectoryInfo(path);
            try
            {
                if (dirInf.Exists)
                {
                    dirInf.Delete(true);
                    Console.WriteLine("Папка удалена.");
                }
                else if (fileInf.Exists)
                {
                    fileInf.Delete();
                    Console.WriteLine("Файл удален.");
                }
                else
                {
                    Console.WriteLine("Папка или файл не найдены!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void MkFile(string fileName)
        {
            try
            {
                if (!File.Exists(fileName))
                {
                    File.Create(fileName).Close();
                    Console.WriteLine("Файл создан");
                }
                else
                {
                    Console.WriteLine("Файл с таким именем уже существует");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void MkDir(string dirName)
        {
            try
            {
                if (!Directory.Exists(dirName))
                {
                    Directory.CreateDirectory(dirName);
                    Console.WriteLine("Папка создана");
                }
                else
                {
                    Console.WriteLine("Папка с таким именем уже существует");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void Copy(string path, string newPath)
        {
            // TODO: упростить!!!
            bool overwrite = false;
            FileInfo fileInf = new FileInfo(path);

            if (File.Exists(path))
            {
                while (true)
                {
                    try
                    {
                        if (fileInf.Exists && overwrite == true)
                        {
                            File.Copy(path, newPath, overwrite);
                            Console.WriteLine("Файл скопирован с заменой");
                            break;
                        }
                        else if (fileInf.Exists && overwrite == false)
                        {
                            File.Copy(path, newPath, overwrite);
                            Console.WriteLine("Файл скопирован");
                            break;
                        }
                        else
                        {
                            Console.Write("Файл не найден.");
                            break;
                        }
                    }
                    catch
                    {
                        Console.Write("Файл с таким именем уже существует в заданной директории. Заменить? Y/N :  ");
                        // TODO: переделать момент с заменой файлов
                        string userAnswer = Console.ReadLine().ToUpper();
                        if (userAnswer == "Y")
                        {
                            overwrite = true;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            else if (Directory.Exists(path))
            {
                DirectoryInfo dirInf = new DirectoryInfo(path);
                DirectoryInfo[] dirs = dirInf.GetDirectories();

                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }

                FileInfo[] files = dirInf.GetFiles();
                foreach (FileInfo file in files)
                {
                    string temppath = Path.Combine(newPath, file.Name);
                    file.CopyTo(temppath, true);
                }

                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(newPath, subdir.Name);
                    Copy(subdir.FullName, temppath);
                }
                Console.WriteLine("Папка с файлами скопирована"); // TODO: Исправить вывод сообщения (сейчас выходит много раз)
            }
            else
            {
                Console.WriteLine("Нужно указать корректный путь к файлу или директории");
            }
        }
    }
}
