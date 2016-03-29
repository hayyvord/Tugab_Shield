using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Tugab;

namespace AtA_TuGab_Shield_290316
{
    public partial class MainForm : Form
    {

        private Tugab.Shield shield = new Tugab.Shield("COM3");

        public MainForm()
        {
            InitializeComponent();
        }

        private void btn_Led1_MouseUp(object sender, MouseEventArgs e)
        {
            this.shield.SetLED(1, false);
        }

        private void btn_Led1_MouseDown(object sender, MouseEventArgs e)
        {
            this.shield.SetLED(1, true);
        }

        private void btn_Led2_MouseUp(object sender, MouseEventArgs e)
        {
            this.shield.SetLED(2, false);
        }

        private void btn_Led2_MouseDown(object sender, MouseEventArgs e)
        {
            this.shield.SetLED(2, true);
        }

        private void btn_Led3_MouseUp(object sender, MouseEventArgs e)
        {
            this.shield.SetLED(3, false);
        }

        private void btn_Led3_MouseDown(object sender, MouseEventArgs e)
        {
            this.shield.SetLED(3, true);
        }

        private void btn_1_Click(object sender, EventArgs e)
        {
            this.shield.GetButton(1);
        }

        private void btn_2_Click(object sender, EventArgs e)
        {
            this.shield.GetButton(2);
        }

        private void btn_3_Click(object sender, EventArgs e)
        {
            this.shield.GetButton(3);
        }

        private void btn_Pot1_Click(object sender, EventArgs e)
        {
            this.shield.GetPot(1);
        }

        private void btn_Pot2_Click(object sender, EventArgs e)
        {
            this.shield.GetPot(2);
        }

        private void btn_LRD_Click(object sender, EventArgs e)
        {
            this.shield.GetLightSensor();
        }

        private void btn_Temp_Click(object sender, EventArgs e)
        {
            this.shield.GetTemperaturSensor();
        }

        private void btn_Mic_Click(object sender, EventArgs e)
        {
            this.shield.GetMic();
        }

        private void btn_Buzz_Click(object sender, EventArgs e)
        {
            this.shield.SetBuzzer(true);
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            this.shield = new Tugab.Shield(tb_Ports.Text.ToString());
            this.shield.Connect();
            this.shield.OnMessage += this.shield_OnMessage;
        }

        private void shield_OnMessage(object sender, Tugab.MessageString e)
        {
            tbStatus.BeginInvoke((MethodInvoker)delegate()
                {
                    tbStatus.AppendText(e.Message.ToString());
                });
        }

        private void btnDisconect_Click(object sender, EventArgs e)
        {
            this.shield.Disconnect();
        }

        private void btn_Display_Click(object sender, EventArgs e)
        {
            int value = Int32.Parse(tb_Value.Text);
            this.shield.SetDisplay(value);
        }

    }
}
