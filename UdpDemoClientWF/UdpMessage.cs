using System;
using System.Text;

namespace UdpDemoClientWF
{

    public abstract class DemoUdpMessage
    {
        public static DemoUdpMessage Parse(byte[] data)
        {
            // Convert the data to a string
            string data_parsed = Encoding.UTF8.GetString(data);

            // Check if the string is empty or null
            if (string.IsNullOrEmpty(data_parsed))
            {
                throw new ArgumentException("Data cannot be empty or null", nameof(data));
            }

            // Get the first word of the string
            string command = data_parsed.Split(' ')[0];

            // Switch based on the packet command
            switch (command)
            {
                case "MSG":
                    return new DemoUdpStringMessage(data_parsed.Substring(4));
                default:
                    throw new ArgumentException("Unknown command", nameof(data));
            }
        }

        // This must be implemented in all classes inheriting this abstract class.
        // It should return the byte representation of the UDP message.
        public abstract byte[] ToBytes();

    }

    public class DemoUdpStringMessage : DemoUdpMessage
    {
        public string Message { get; set; }

        public DemoUdpStringMessage(string message)
        {
            Message = message;
        }

        public override byte[] ToBytes()
        {
            return Encoding.UTF8.GetBytes("MSG " + Message);
        }
    }
}
