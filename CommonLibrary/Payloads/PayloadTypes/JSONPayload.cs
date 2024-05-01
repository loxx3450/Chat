using ProtocolLibrary.Payload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CommonLibrary.Payloads.PayloadTypes
{
    public class JSONPayload : IPayload
    {
        public string GetPayloadType()
        {
            return "json";
        }

        public MemoryStream GetStream()
        {
            byte[] bytes = UTF8Encoding.UTF8.GetBytes(GetJson());

            MemoryStream stream = new MemoryStream();

            stream.Write(bytes, 0, bytes.Length);

            stream.Position = 0;

            return stream;
        }

        public string GetJson()
        {
            return JsonSerializer.Serialize(this, this.GetType());
        }

        public static object GetPayload(MemoryStream memoryStream, Type returnType)
        {
            using StreamReader reader = new StreamReader(memoryStream, leaveOpen: true);
            string json = reader.ReadToEnd();

            memoryStream.Position = 0;

            return JsonSerializer.Deserialize(json, returnType) ?? throw new Exception();
        }
    }
}
