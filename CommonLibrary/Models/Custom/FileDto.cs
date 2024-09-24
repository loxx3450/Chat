using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Models.Custom
{
    public class FileDto
    {
        public int Id { get; set; }
        public string? FileName { get; set; }
        public byte[] FileContent { get; set; }

        public FileDto(int id, byte[] fileContent, string? fileName = null)
        {
            Id = id;
            FileContent = fileContent;
            FileName = fileName;
        }
    }
}
