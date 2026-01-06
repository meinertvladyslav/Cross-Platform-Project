using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cross_Platform_Project;

public static class AppState
{
    public static List<Account> Accounts { get; set; } = new();
    public static Account CurrentAccount { get; set; }
}
