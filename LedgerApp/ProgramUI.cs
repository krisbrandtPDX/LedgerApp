using System;
using System.Collections.Generic;
using System.Text;

namespace LedgerApp
{
    public static class ProgramUI
    {
        public static string Prompt(string msg, string defaultReturn = "")
        {
            string userInput = "";
            Console.WriteLine(msg);
            userInput = Console.ReadLine().Trim();
            if(userInput == "")
            {
                userInput = defaultReturn;
            }
            return userInput;
        }

        public static void Notify(string msg)
        {
            Console.WriteLine(msg);
        }

    }
}
