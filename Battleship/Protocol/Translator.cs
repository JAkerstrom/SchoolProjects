using System;
using System.Collections.Generic;
using System.Text;

namespace johanna_battleship.Protocol
{
    public class Translator
    {
        public string translate(string message, string expectedMessage)
        {
            return " ";
        }

        public string Translate(string message, string expectedMessage, int stage)
        {
            if (message == null || message == " ") return "null";
            else if (message.ToLower().StartsWith(expectedMessage) && stage == 1) return "200";
            else if (message.ToLower() == expectedMessage && stage == 2) return "200";
            else if (message.ToLower().StartsWith(expectedMessage) && message.Length > expectedMessage.Length && stage == 3) return message.Substring(expectedMessage.Length + 1);
            else if (message.ToLower() == "quit") return "quit";
            else if (message.ToLower().Contains(expectedMessage) && !message.ToLower().StartsWith(expectedMessage)) return "501";
            else return "500";
        }
    }
}
