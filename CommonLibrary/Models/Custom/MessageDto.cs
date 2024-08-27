using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Models.Custom
{
    public class MessageDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public DateTime SentAt { get; set; }

        public string? Text { get; set; }

        public List<FileDto>? Files { get; set; }


        public MessageDto(int id, int userId, DateTime sentAt, string? text, List<FileDto>? files = null) 
        { 
            Id = id;
            UserId = userId;
            SentAt = sentAt;
            Text = text;
            Files = files;
        }
    }
}
