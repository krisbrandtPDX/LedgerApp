using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LedgerApp
{
    public static class LedgerAPI
    {
        private static string dataFile = "Ledger.json";
        public static string GetLedger()
        {
            string json = "";
            if (File.Exists(dataFile))
            {
                json = File.ReadAllText(dataFile);
            }
            return json;
        }

        public static void PostLedger(string json)
        {
            File.WriteAllTextAsync(dataFile, json);
        }
    }
}
