using System.Media;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UdpDemoClientWF
{

    public partial class frmMain : Form
    {

        // This object receives UDP communication.
        private UdpClient udp;

        // Store the port selected for listening.
        private int sourcePort = 0;

        // When the application closes, this flag is set to True, so the socket thread will abort.
        private bool isClosing = false;

        // Class initializer
        public frmMain()
        {

            // Stuff that goes in here runs before the form is even displayed.

            InitializeComponent();

            // Select a random source port number between 1024 and 65000 inclusive.
            sourcePort = new Random().Next(1024, 65001);

            // Create a new UdpClient object and bind it to the source port.
            udp = new UdpClient(sourcePort);

        }

        // Load method - fires AFTER the form has initialized but before being displayed
        private void frmMain_Load(object sender, EventArgs e)
        {
            // Wait for the form to be fully loaded before actually listening for UDP packets.
            // This is just in case packets happen to come in before we're all set up.

            // Select a random source port number between 1024 and 65000 inclusive.
            int sourcePort = new Random().Next(1024, 65001);

            // Create a new UdpClient object and bind it to the source port.
            udp = new UdpClient(sourcePort);

            // Set the source port control to the source port selected by default
            numPort.Value = sourcePort;

            // Log the port udp is listening on.
            txtLog.Text += $"Listening for messages on port: {sourcePort}\r\n\r\n";

            // Set the combo box to the initial value
            cbType.SelectedIndex = 0;

            // Start a thread for the UDP client to listen for incoming packets.
            Thread t = new Thread(new ThreadStart(Receive));
            t.Start();

            // Ready to go.
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            // Try to parse the IP address; throw a messagebox up with an error if it's in an incorrect format.
            if (!IPAddress.TryParse(txtIpAddress.Text, out IPAddress ip))
            {
                MessageBox.Show("Invalid IP address.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Setup a UDP client to send the packet
            UdpClient udp = new UdpClient();

            byte[] data = [];

            // Form the packet
            switch (cbType.SelectedIndex)
            {
                case 0:
                    // Send a string message
                    data = Encoding.ASCII.GetBytes("MSG " + txtMessage.Text);
                    break;
                case 1:
                    // send a sound message
                    data = Encoding.ASCII.GetBytes("SND");
                    break;
                case 2:
                    // send exit message
                    data = Encoding.ASCII.GetBytes("QUIT");
                    break;
            }

            // Send the packet
            udp.Send(
                data,
                data.Length,
                txtIpAddress.Text,
                (int)numPort.Value
            );

        }

        private void Receive()
        {
            // This method will run in a separate thread.

            // Loop forever.
            while (true)
            {


                // Wait for a packet to arrive.
                // If no packet received in 1s, continue the loop.
                udp.Client.ReceiveTimeout = 1000;

                IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);

                // Initialize the receive variable so it will exist even if an error occurs.
                byte[] data = [];
                // Try to get a packet.
                try
                {
                    data = udp.Receive(ref remoteEP);
                }
                catch (SocketException ex)
                {
                    if (ex.SocketErrorCode == SocketError.TimedOut)
                    {
                        continue; // This is OK. We will just wait for another packet.
                    }
                }

                // If the form is closing, exit the loop.
                if (isClosing)
                {
                    return;
                }

                string payload = Encoding.ASCII.GetString(data);

                // Parse the payload
                DemoUdpMessage message;
                try { 
                    message = DemoUdpMessage.Parse(data);
                }
                catch (ArgumentException ex)
                {
                    // Log the error
                    txtLog.Invoke((MethodInvoker)delegate
                    {
                        txtLog.AppendText($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:")} Error parsing message: {ex.Message}\r\n");
                    });
                    continue;
                }

                // Switch based on the actual type of message
                switch (message)
                {
                    case DemoUdpStringMessage msg:
                        // Log the message
                        txtLog.Invoke((MethodInvoker)delegate
                        {
                            txtLog.AppendText($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:")} Received message: {msg.Message}\r\n");
                        });
                        continue;
                    case DemoUdpSoundMessage snd:
                        // Log the sound message
                        txtLog.Invoke((MethodInvoker)delegate
                        {
                            txtLog.AppendText($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:")} Received sound message\r\n");
                        });
                        // Play the system default sound
                        SystemSounds.Beep.Play();
                        continue;
                    case DemoUdpQuitMessage quit:
                        // Log the quit message
                        txtLog.Invoke((MethodInvoker)delegate
                        {
                            txtLog.AppendText($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:")} Received quit message\r\n");
                        });
                        // Signal the main thread to close the application
                        this.Invoke((MethodInvoker)delegate
                        {
                            this.Close();
                        });
                        return;
                    default:
                        // Log the error
                        txtLog.Invoke((MethodInvoker)delegate
                        {
                            txtLog.AppendText($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:")} Unknown message type\r\n");
                        });
                        break;
                }

            }
        }

        private void cbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Set the text field to enabled or disabled based on the selected payload type.
            if (cbType.SelectedIndex == 0)
            {
                txtMessage.Enabled = true;
            }
            else
            {
                txtMessage.Enabled = false;
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Form is closing - stop the listening socket and thread so the app can exit.

            // Set the flag to True so the socket thread will abort.
            isClosing = true;

            // Close the socket.
            udp.Close();

        }
    }
}
