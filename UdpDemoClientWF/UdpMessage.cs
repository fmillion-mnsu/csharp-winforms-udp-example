using System.Text;

namespace UdpDemoClientWF
{
    /// <summary>
    /// Represents a UDP message.
    /// </summary>
    /// <remarks>
    /// All UDP message packets inherit from this abstract class.
    /// </remarks>
    public abstract class DemoUdpMessage
    {
        /// <summary>
        /// Parse a byte array as a UdpMessage packet.
        /// </summary>
        /// <param name="data">Byte[] containing bytes for the packet</param>
        /// <returns>Instance of a class inheriting from DemoUdpMessage</returns>
        /// <exception cref="ArgumentException">Raised when the packet cannot be parsed</exception>
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
                // This utilizes polymorphism to return the correct message type class
                // Since all of these inherit from DemoUdpMessage any of them can be returned.
                case "MSG":
                    return new DemoUdpStringMessage(data_parsed.Substring(4));
                case "SND":
                    return new DemoUdpSoundMessage();
                case "QUIT":
                    return new DemoUdpQuitMessage();
                default:
                    // The command was unknown, so raise an exception.
                    throw new ArgumentException($"Unknown command {command}", nameof(data));
            }
        }

        // This must be implemented in all classes inheriting this abstract class.
        // It should return the byte representation of the UDP message.
        public abstract byte[] ToBytes();

    }

    /// <summary>
    /// Represents a UDP message that contains a string of text.
    /// </summary>
    public class DemoUdpStringMessage : DemoUdpMessage
    {
        /// <summary>
        /// The message accompanying this packet.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Initialize a new instance of DempUdpStringMessage.
        /// </summary>
        /// <param name="message">The message to associate with this packet.</param>
        public DemoUdpStringMessage(string message)
        {
            Message = message;
        }

        /// <summary>
        /// Gets the byte representation of the packet, suitable for sending via UDP.
        /// </summary>
        /// <returns>Byte[] containing packet</returns>
        public override byte[] ToBytes()
        {
            return Encoding.UTF8.GetBytes("MSG " + Message);
        }
    }

    /// <summary>
    /// Represents a UDP message that signals the app to play a sound.
    /// </summary>
    public class DemoUdpSoundMessage : DemoUdpMessage
    {
        /// <summary>
        /// Gets the byte representation of the packet, suitable for sending via UDP.
        /// </summary>
        /// <returns>Byte[] containing packet</returns>
        public override byte[] ToBytes()
        {
            return Encoding.UTF8.GetBytes("SND");
        }
    }

    /// <summary>
    /// Represents a UDP message that signals the app to quit.
    /// </summary>
    public class DemoUdpQuitMessage : DemoUdpMessage
    {
        /// <summary>
        /// Gets the byte representation of the packet, suitable for sending via UDP.
        /// </summary>
        /// <returns>Byte[] containing packet</returns>
        public override byte[] ToBytes()
        {
            return Encoding.UTF8.GetBytes("QUIT");
        }
    }
}
