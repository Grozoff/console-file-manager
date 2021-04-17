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
            Console.WriteLine("[help]   [cd]    [ls]    [inf]   [rm]    [mkfil]    [mkdir]    [cp]    [exit]\n");
            Command command = new Command();
            command.Commands();                      
        }
    }
}
