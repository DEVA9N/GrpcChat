using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Client
{
    internal sealed class ConsoleUser
    {
        public static String GetRandomUserName()
        {
            var userNames = new[] { "Alice", "Bob", "Horst", "Tanja" };
            var randomIndex = new Random(DateTime.Now.Millisecond).Next(userNames.Length);

            return userNames[randomIndex];
        }
    }
}
