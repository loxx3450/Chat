using ProtocolLibrary.Core;
using ProtocolLibrary.Message;
using SocketEventLibrary.SocketEventMessageCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary
{
    public class SocketEventProtocolMessage : SocketEventMessage, IRecoverable
    {
        public SocketEventProtocolMessage(MessageType key, ProtocolMessage payload) 
            : base(key, payload)
        { }

        protected override MemoryStream GetDataStream()
        {
            MemoryStream memStream = new MemoryStream();

            //Writes, represented by string, Key 
            using StreamWriter streamWriter = new StreamWriter(memStream, leaveOpen: true);
            streamWriter.WriteLine(Key.ToString());
            streamWriter.Flush();

            //Copies Payload's Stream
            MemoryStream payloadStream = ((ProtocolMessage)Payload).GetStream();
            payloadStream.CopyTo(memStream);

            //Setting positions of streams to start
            memStream.Position = 0;
            payloadStream.Position = 0;

            return memStream;
        }

        public static SocketEventMessage RecoverSocketEventMessage(MemoryStream memoryStream)
        {
            int startPos = (int)memoryStream.Position;

            using StreamReader streamReader = new StreamReader(memoryStream, leaveOpen: true);
            
            string keyStr = streamReader.ReadLine() ?? throw new ArgumentException();                                   //TODO: own exceptions

            //StreamReader sets the position to end, during closing
            memoryStream.Position = startPos + keyStr.Length + 2;                                                       //TODO

            //Converts string representation of type in enum's variable
            MessageType key = (MessageType)Enum.Parse(typeof(MessageType), keyStr);

            //Converts the rest of memStream in payload
            ProtocolMessage payload = StreamExtractor.ExtractAll(memoryStream);

            return new SocketEventProtocolMessage(key, payload);
        }
    }
}
