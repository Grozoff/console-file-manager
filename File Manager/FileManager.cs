using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace File_Manager
{
    public class FileManager
    {
        public void Information(string path)
        {
            FileInfo fileInf = new FileInfo(path);
            Console.WriteLine(string.Format("Information about {0} :\nSize = {1} bytes\nLast modified on {2}\nCreation time on {3}", fileInf.Name, fileInf.Length, fileInf.LastWriteTime, fileInf.CreationTime));
        }

        public void Remove(string path)
        {
            // TODO: упростить!!!!
            FileInfo fileInf = new FileInfo(path);
            DirectoryInfo dirInf = new DirectoryInfo(path);
            if (Directory.Exists(path))
            {
                if (dirInf.Exists)
                {
                    dirInf.Delete(true);
                    Console.WriteLine("Папка удалена.");
                }
                else
                {
                    Console.WriteLine("Папка не найдена.");
                }
            }
            else if (File.Exists(path))
            {
                if (fileInf.Exists)
                {
                    fileInf.Delete();
                    Console.WriteLine("Файл удален.");
                }
                else
                {
                    Console.WriteLine("Файл не найден.");
                }
            }
        }

        public void MkFile(string fileName)
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
            catch
            {
                Console.WriteLine("Невозможно создать папку с таким именем");
            }

        }

        public void Copy(string path, string newPath)
        {
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
                Console.WriteLine("Нужно указать пути файла или директории");
            }
        }
    }
}
