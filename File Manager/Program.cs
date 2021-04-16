using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace File_Manager
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "File Manager";
            FileManager fm = new FileManager();

            Console.WriteLine("[help]   [cd]    [ls]    [inf]   [rm]    [mkfil]    [mkdir]    [cp]    [exit]\n");

            string currentDirectory = Directory.GetCurrentDirectory(); // TODO: сохранять состояние текущей директории
            string userCommand = string.Empty;
            List<string> parameters = new List<string>();

            #region Заглушки
            string file = @"C:\Users\magnu\source\repos\123.txt";
            string newFile = @"C:\Users\magnu\source\repos\12\123.txt";
            string pathDir = @"C:\Users\magnu\Desktop\123";
            string newPathDir = @"C:\Users\magnu\source\repos\12";
            #endregion

            bool exit = true;

            while (exit)
            {
                Console.WriteLine(string.Format(currentDirectory));

                
                Console.Write("Command: ");
                userCommand = Console.ReadLine();

                if (string.IsNullOrEmpty(userCommand))
                {
                    continue;
                }

                string[] parts1 = userCommand.Split(new string[] { " \"" }, StringSplitOptions.None);
                string[] clearCommands = new string[4];

                for (int i = 0; i < parts1.Length; i++)
                {
                    clearCommands[i] = parts1[i].Trim();
                    clearCommands[i] = Regex.Replace(clearCommands[i], "\"", "");                    
                }
                

                //string[] parts2 = Regex.Split(userCommand, " \"");

                // TODO: парсинг путей с пробелами, пока выход сделать сплит не пробелом а запрещенным символом

                //parameters.RemoveAll(WhiteParameters);

                userCommand = clearCommands[0].ToLower();

                // TODO: cd для навигации, ls для пэйджинга, inf для информации о файле/папки
                // TODO: Ловля исключений
                // TODO: Отловить исключение с заменой файлов через foreach


                switch (userCommand)
                {
                    case "inf":
                        fm.Information(clearCommands[1]);
                        break;
                    case "rm":
                        fm.Remove(clearCommands[1]);
                        break;
                    case "mkfil":
                        fm.MkFile(clearCommands[1]);
                        break;
                    case "mkdir":
                        fm.MkDir(clearCommands[1]);
                        break;
                    case "cp":
                        fm.Copy(file, newFile);
                        break;
                    case "exit":
                        exit = false;
                        Console.Clear();
                        break;
                    case "help":
                        Console.Clear();
                        Console.WriteLine("[help] - Вызвать справку\n[cd] - навигация по каталогам\ncd .. переход в родительский каталог\n" +
                            "cd \\путь к каталогу на текущем диске, например cd \\users[rm] - Удаление файла\\каталога\n" +
                            "[mkfil] - Создание файла\n[mkdir] - Создание каталога\n[cp] - Копирование файла\\директории\n" +
                            "[exit] - Завершить работу файлового менеджера\n");
                        break;
                    default:
                        Console.WriteLine("Команда не найдена");
                        break;
                }
            }           
        }
    }
}
