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

        public frmMain()
        {

            // Stuff that goes in here runs before the form is even displayed.

            InitializeComponent();

            // Select a random source port number between 1024 and 65000 inclusive.
            sourcePort = new Random().Next(1024, 65001);

            // Create a new UdpClient object and bind it to the source port.
            udp = new UdpClient(sourcePort);

        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            // Wait for the form to be fully loaded before actually listening for UDP packets.
            // This is just in case packets happen to come in before we're all set up.

            // Select a random source port number between 1024 and 65000 inclusive.
            int sourcePort = new Random().Next(1024, 65001);

            // Create a new UdpClient object and bind it to the source port.
            udp = new UdpClient(sourcePort);

            // Log the port udp is listening on.
            txtLog.Text += $"Listening for messages on port: {sourcePort}\r\n\r\n";

            // Set the combo box to the initial value
            cbType.SelectedIndex = 0;

            // Start a thread for the UDP client to listen for incoming packets.
            Thread t = new Thread(new ThreadStart(Receive));
            t.Start();

            // Ready to go.
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
                string logMessage = $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:")} Received {data.Length} bytes from {remoteEP.Address}:{remoteEP.Port}:\r\n{payload}\r\n\r\n";

                // Update the TextBox on the UI thread.
                // We need to use Invoke to instruct the main UI thread to update the textbox, since we can't update it
                // directly on this thread.
                txtLog.Invoke((MethodInvoker)delegate
                {
                    txtLog.AppendText(logMessage);
                });

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Try to parse the IP address; throw a messagebox up with an error if it's in an incorrect format.
            if (!IPAddress.TryParse(txtIpAddress.Text, out IPAddress ip))
            {
                MessageBox.Show("Invalid IP address.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Setup a UDP client to send the packet
            UdpClient udp = new UdpClient();
            // Send the packet
            udp.Send(
                Encoding.ASCII.GetBytes(txtMessage.Text),
                txtMessage.Text.Length,
                txtIpAddress.Text,
                (int)numPort.Value
            );

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

        private void frmMain_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            // Form is closing - stop the listening socket and thread so the app can exit.

            // Set the flag to True so the socket thread will abort.
            isClosing = true;

            // Close the socket.
            udp.Close();

        }
    }
}
