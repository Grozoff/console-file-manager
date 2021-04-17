using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace File_Manager
{
    public class Command
    {
        readonly FileManager fm = new FileManager();
        string currentDirectory = Directory.GetCurrentDirectory(); // TODO: сохранять состояние текущей директории
        string userCommand = string.Empty;
        bool exit = true;

        public string[] Parser(string userCommand)
        {            
            // TODO: добавить визможность писать пути без определения их в кавычки (для путей без пробелов)
            //string[] dirtyCommands = userCommand.Split(new string[] { " \"" }, StringSplitOptions.None);
            string[] dirtyCommands = Regex.Split(userCommand, " \"");
            string[] clearCommands = new string[4];

            for (int i = 0; i < dirtyCommands.Length; i++)
            {
                clearCommands[i] = dirtyCommands[i].Trim();
                clearCommands[i] = Regex.Replace(clearCommands[i], "\"", "");
            }
            return clearCommands;
        }

        public void Commands()
        {
            while (exit)
            {
                Console.WriteLine(string.Format(currentDirectory));
                Console.Write("Command: ");
                userCommand = Console.ReadLine();

                if (string.IsNullOrEmpty(userCommand))
                {
                    continue;
                }

                string[] clearCommands = Parser(userCommand);
                
                userCommand = clearCommands[0].ToLower();

                // TODO: cd для навигации, ls для пэйджинга и отображения вложений в папках
                // TODO: Отловить исключение с заменой файлов через foreach

                switch (userCommand)
                {
                    case "cd":

                        break;
                    case "cd..":
                        fm.ChangeDirrectory(ref currentDirectory);
                        break;
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
