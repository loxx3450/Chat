using ProtocolLibrary.Payload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
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

            MemoryStream stream = new MemoryStream(bytes);

            return stream;
        }

        public string GetJson()
        {
            var options = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            return JsonSerializer.Serialize(this, this.GetType(), options);
        }

        public static object GetPayload(MemoryStream memoryStream, Type returnType)
        {
            string json = null!;

            using (StreamReader reader = new StreamReader(memoryStream, leaveOpen: true))
            {
                json = reader.ReadToEnd();
            }

            memoryStream.Position = 0;

            return JsonSerializer.Deserialize(json, returnType) ?? throw new Exception();
        }
    }
}
