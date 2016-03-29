using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;


namespace Tugab
{
    public class Shield
    {

        #region Varibles

        private bool isConnected = false;

        private SerialPort AtmelPort;

        private string portName = String.Empty;

        private DateTime time = new DateTime();

        #endregion

        #region Events

        public event EventHandler<MessageString> OnMessage;

        #endregion

        #region Public IsConnected
        public bool IsConnected
        {
            get
            {
                return this.isConnected;
            }
        }

        #endregion

        #region Constructor
        public Shield(string portName)
        {
            this.portName = portName;
        }

        #endregion

        #region Public Methods
        public void Connect()
        {
            while (!this.isConnected)
            {
                try
                {
                    if (!this.isConnected)
                    {
                        this.AtmelPort = new SerialPort(this.portName);
                        this.AtmelPort.BaudRate = 19200;
                        this.AtmelPort.DataBits = 8;
                        this.AtmelPort.StopBits = StopBits.One;
                        this.AtmelPort.Parity = Parity.None;
                        this.AtmelPort.Open();
                        this.AtmelPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

                        this.isConnected = true;
                    }
                }
                catch (Exception exception)
                {
                    this.isConnected = false;
                }
            }

        }

        public void Disconnect()
        {
            if (this.isConnected)
            {
                this.AtmelPort.Close();
            }
            this.isConnected = false;
        }

        public void SendRequest(string command)
        {
            if (this.isConnected && this.AtmelPort.IsOpen)
            {
                this.AtmelPort.WriteLine(command);
            }
        }

        #region Public
        public void Reset()
        {
            if (this.isConnected)
            {
                this.AtmelPort.DtrEnable = true;
                Thread.Sleep(200);
                this.AtmelPort.DtrEnable = false;
            }
        }

        public void SetDisplay(int value)
        {
            if (value > 99)
            {
                value = 99;
            }

            if (value < 0)
            {
                value = 0;
            }

            if (this.isConnected)
            {
                string command = String.Format("?DISPLAY{0:D2}", value);
                this.SendRequest(command);
            }
        }
        public void SetLED(int index, bool value)
        {
            if (index > 3)
            {
                index = 2;
            }

            if (index < 1)
            {
                index = 0;
            }

            if (this.isConnected)
            {
                string command = String.Format("?LED{0}{1}", index, value ? 1 : 0);
                this.SendRequest(command);
            }
        }

        public void SetBuzzer(bool value)
        {
            if (this.isConnected)
            {
                string command = String.Format("?BUZZER{0}", value ? 1 : 0);
                this.SendRequest(command);
            }
        }

        public void GetPot(int index)
        {
            if (this.isConnected)
            {
                if (index > 2)
                {
                    index = 2;
                }

                if (index < 1)
                {
                    index = 1;
                }

                string command = String.Format("?POT{0}", index);
                this.SendRequest(command);
            }
        }
        public void GetLightSensor()
        {
            if (this.isConnected)
            {
                string command = String.Format("?LIGHT");
                this.SendRequest(command);
            }
        }
        public void GetTemperaturSensor()
        {
            if (this.isConnected)
            {
                string command = String.Format("?TEMP");
                this.SendRequest(command);
            }
        }
        public void GetMic()
        {
            if (this.isConnected)
            {
                string command = String.Format("?MIC");
                this.SendRequest(command);
            }
        }
        public void GetButton(int index)
        {
            if (this.isConnected)
            {
                if (index > 3)
                {
                    index = 3;
                }

                if (index < 1)
                {
                    index = 1;
                }

                string command = String.Format("?BUTTON{0}", index);
                this.SendRequest(command);
            }
        }

        #endregion

        #endregion

        #region Private ReadPort
        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            Thread.Sleep(50);

            if (sender != null)
            {
                // Make serial port to get data from.
                SerialPort sp = (SerialPort)sender;

                //string indata = sp.ReadLine();
                string inData = sp.ReadExisting();

                this.OnMessage(this, new Tugab.MessageString(inData));

                // Discart the duffer.
                sp.DiscardInBuffer();
            }
            
        }

        #endregion

    }
}
