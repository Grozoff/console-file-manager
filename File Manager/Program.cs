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
            // TODO: закинуть комманды в отдельный класс и в Main создавать экземпляр класса комманд
            FileManager fm = new FileManager();

            Console.WriteLine("[help]   [cd]    [ls]    [inf]   [rm]    [mkfil]    [mkdir]    [cp]    [exit]\n");

            string currentDirectory = Directory.GetCurrentDirectory(); // TODO: сохранять состояние текущей директории
            string userCommand = string.Empty;

            bool exit = true;

            while (exit)
            {
                Console.WriteLine(string.Format(currentDirectory));
               
                // TODO: закинуть парсер комманд в отдельный метод в классе комманд
                Console.Write("Command: ");
                userCommand = Console.ReadLine();

                if (string.IsNullOrEmpty(userCommand))
                {
                    continue;
                }
                // TODO: добавить визможность писать пути без определения их в кавычки (для путей без пробелов)
                //string[] dirtyCommands = userCommand.Split(new string[] { " \"" }, StringSplitOptions.None);
                string[] dirtyCommands = Regex.Split(userCommand, " \"");
                string[] clearCommands = new string[4];

                for (int i = 0; i < dirtyCommands.Length; i++)
                {
                    clearCommands[i] = dirtyCommands[i].Trim();
                    clearCommands[i] = Regex.Replace(clearCommands[i], "\"", "");                    
                }
                               
                userCommand = clearCommands[0].ToLower();
                
                // TODO: cd для навигации, ls для пэйджинга
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
                        fm.Copy(clearCommands[1], clearCommands[2]);
                        break;
                    case "exit":
                        exit = false;
                        Console.Clear();
                        break;
                    case "help":
                        Console.Clear();
                        Console.WriteLine("[help] - Вызвать справку\n[cd] - навигация по каталогам\ncd.. - переход на каталог выше\n" +
                            "cd \"путь к каталогу на текущем диске\"\n[rm] - Удаление файла\\каталога\n" +
                            "[mkfil] - Создание файла\n[mkdir] - Создание каталога\n[cp] - Копирование файла\\директории\n" +
                            "[exit] - Завершить работу файлового менеджера\nВсе пути к файлу или каталогу необходимо заключать в двойные кавычки!\n");
                        break;
                    default:
                        Console.WriteLine("Команда не найдена");
                        break;
                }
            }           
        }
    }
}
