using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace Cross_Platform_Project
{
    //creating path and Serialize and Deserialize accounts.json
    public static class AccountStorage
    {
        static string path =
            Path.Combine(FileSystem.AppDataDirectory, "accounts.json");

        public static List<Account> LoadAccounts()
        {
            if (!File.Exists(path))
                return new List<Account>();

            return JsonSerializer.Deserialize<List<Account>>(
                File.ReadAllText(path)) ?? new();
        }

        public static void SaveAccounts(List<Account> accounts)
        {
            File.WriteAllText(
                path,
                JsonSerializer.Serialize(accounts));
        }
    }
}
