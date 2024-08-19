using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Models
{
    public class Message
    {
        public string? Text { get; set; }
        public DateTime SentAt { get; set; }
        public bool HasFiles { get; set; }

        public Message(string? text, DateTime sentAt, bool hasFiles = false)
        {
            Text = text;
            SentAt = sentAt;
            HasFiles = hasFiles;
        }
    }
}
