using System.IO.Ports;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Demo_COM_port_4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SerialPort _serialPort;
        /// <summary>
        /// test
        /// this is an test to use this commantary
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            _serialPort = new SerialPort();

            cbxComPorts.Items.Add("None");
            foreach (string s in SerialPort.GetPortNames())
                cbxComPorts.Items.Add(s);

            // Zorg dat je data kan ontvangen via de seriële poort.
            _serialPort.DataReceived += _serialPort_DataReceived;

        }

        private void _serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            // Lees alle tekst binnen, tot aan een '\n'.
            string receivedText = _serialPort.ReadLine().Trim();

            //// Foute manier van werken: zet de ontvangen data op het Label.
            //lblReceivedData.Content = receivedText;

            // Goede manier van werken.
            Dispatcher.Invoke(new Action<string>(UpdateLabel), receivedText);
        }

        private void UpdateLabel(string data)
        {
            lblReceivedData.Content = data;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_serialPort != null)
                _serialPort.Dispose();
        }

        private void cbxComPorts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_serialPort != null)
            {
                if (_serialPort.IsOpen)
                    _serialPort.Close();

                if (cbxComPorts.SelectedItem.ToString() != "None")
                {
                    _serialPort.PortName = cbxComPorts.SelectedItem.ToString();
                    _serialPort.Open();
                }
            }
        }

        private void btnsend_Click(object sender, RoutedEventArgs e)
        {
            string mtr = txtwaarde.Text;

            // Controleer of de invoer een geldige waarde is
            if (mtr == "1" || mtr == "2" || mtr == "3" || mtr == "4" || mtr == "vooruit")
            {
                Motor motor1 = new Motor();

                lbl1.Content = "...";  // Hier kun je de bijbehorende actie of bericht tonen

                motor1.MOTOR = mtr; // Zet de waarde van mtr in de MOTOR property

                // Verstuur de waarde naar de seriële poort
                if (_serialPort != null && _serialPort.IsOpen)
                {
                    _serialPort.WriteLine(motor1.MOTOR);
                }
                else
                {
                    lbl1.Content = "Seriële poort is niet open.";
                }
            }
            else
            {
                lbl1.Content = $"Geef een geldige waarde. De waardes zijn: 1, 2, 3, 4 en vooruit. Je gaf de waarde {mtr}.";
            }
        }



    }
}