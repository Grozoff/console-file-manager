using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace File_Manager
{
    public class Command
    {
        string currentDirectory = Directory.GetCurrentDirectory(); // TODO: сохранять состояние текущей директории
        string userCommand = string.Empty;
        bool exit = true;

        public static void CommandsName(bool description)
        {
            var commandsDescriprion = new Dictionary<string, string>
            {
                ["cd.."] = " - Переход на каталог выше",
                ["cd"] = " - Переход к указанному каталогу ",
                ["rm"] = " - Удаление файла\\каталога",
                ["mk"] = " - Создание файла",
                ["mkdir"] = " - Создание каталога",
                ["cp"] = " - Копирование файла\\директории",
                ["exit"] = " - Завершить работу файлового менеджера",
                ["help"] = " - Вызвать справку\nВсе пути к файлу или каталогу необходимо заключать в двойные кавычки!"
            };

            if (description == false)
            {
                foreach (var item in commandsDescriprion)
                {
                    Console.Write($"[{item.Key}]    ");
                }
                Console.WriteLine();
            }
            else
            {
                foreach (var item in commandsDescriprion)
                {
                    Console.WriteLine($"{item.Key}{item.Value}");
                }
                Console.WriteLine();
            }
        }

        public static string[] Parser(string userCommand)
        {
            //string[] dirtyCommands = userCommand.Split(new string[] { " \"" }, StringSplitOptions.None);
            string[] clearCommands = new string[4];
            if ((userCommand.Count(c => c == '\"') == 0) && (userCommand.Count(c => c == ' ') <= 2))
            {
                string[] dirtyCommands = Regex.Split(userCommand, " ");

                for (int i = 0; i < dirtyCommands.Length; i++)
                {
                    clearCommands[i] = dirtyCommands[i].Trim();
                    if (clearCommands[i].StartsWith('\\'))
                    {
                        clearCommands[i] = clearCommands[i].TrimStart('\\');
                    }
                }
            }
            else if (userCommand.Count(c => c == '\"') >= 2)
            {
                string[] dirtyCommands = Regex.Split(userCommand, " \"");

                for (int i = 0; i < dirtyCommands.Length; i++)
                {
                    clearCommands[i] = dirtyCommands[i].Trim();
                    clearCommands[i] = Regex.Replace(clearCommands[i], "\"", "");
                }
            }
            else
            {
                Console.WriteLine("Возможно в пути обнаружены пробелы, возьмите путь в двойные кавычки!");
            }

            return clearCommands;
        }

        public void Commands()
        {
            CommandsName(false);

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

                if (clearCommands[0] is null)
                {
                    continue;
                }
                else
                {
                    userCommand = clearCommands[0].ToLower();
                }

                // TODO: ls для пэйджинга и отображения вложений в папках
                // TODO: Отловить исключение с заменой файлов через foreach

                switch (userCommand)
                {
                    case "cd":
                        FileManager.ChangeDirectory(ref currentDirectory, clearCommands[1]);
                        break;
                    case "cd..":
                        FileManager.ParentDirectory(ref currentDirectory);
                        break;
                    case "inf":
                        FileManager.Information(currentDirectory, clearCommands[1]);
                        break;
                    case "rm":
                        FileManager.Remove(ref currentDirectory, clearCommands[1]);
                        break;
                    case "mk":
                        FileManager.MkFile(clearCommands[1]);
                        break;
                    case "mkdir":
                        FileManager.MkDir(currentDirectory, clearCommands[1]);
                        break;
                    case "cp":
                        FileManager.Copy(clearCommands[1], clearCommands[2]);
                        break;
                    case "exit":
                        exit = false;
                        Console.Clear();
                        break;
                    case "help":
                        Console.Clear();
                        CommandsName(true);
                        break;
                    default:
                        Console.WriteLine("Команда не найдена");
                        break;
                }
            }
        }
    }
}
