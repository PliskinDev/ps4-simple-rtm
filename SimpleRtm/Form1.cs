using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using libdebug;
namespace SimpleRtm
{
    public partial class Form1 : Form
    {
        private PS4DBG ps4;
        private int pid;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                this.ps4 = new PS4DBG(this.textBox1.Text);
                this.ps4.Connect();

                foreach (libdebug.Process process in ps4.GetProcessList().processes)
                {
                    if (process.name == "eboot.bin")
                    {
                        pid = process.pid;
                        ps4.Notify(222, "Connection!");
                        this.label4.Text = "Connected";
                        this.button1.ForeColor = Color.Green;
                        this.label4.ForeColor = Color.Green;
                        this.label5.Text = "Attached";
                        this.label5.ForeColor = Color.Green;
                        this.ps4.Notify(222, "Connected!");
                        break;
                    }
                }
            }
            catch
            {
                if (!this.ps4.IsConnected)
                {
                    MessageBox.Show("Failed To Connect and Attach!", "Connection Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.label4.Text = "Failed";
                    this.button1.ForeColor = Color.Red;
                    this.label4.ForeColor = Color.Red;
                    this.label5.Text = "Failed";
                    this.label5.ForeColor = Color.Red;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ps4 == null)
                {
                    MessageBox.Show("Not connected to PS4! Exiting", "Connection Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    this.button2.ForeColor = Color.Red;
                    this.label4.Text = "Not connected";
                    this.label4.ForeColor = Color.Red;
                    this.label5.Text = "Not connected";
                    this.label5.ForeColor = Color.Red;
                    return;
                }
            }
            catch
            {
                this.button2.ForeColor = Color.Blue;
                this.label4.Text = "Disconnected";
                this.label4.ForeColor = Color.Blue;
                this.label5.Text = "Disconnected";
                this.label5.ForeColor = Color.Blue;
                this.ps4.Notify(222, "Disconecting!");
                this.ps4.Disconnect();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                byte[] HealthOn = new byte[] { 0xE9, 0x3C, 0x46, 0x2B, 0x00, 0x90, 0x90 };
                this.ps4.WriteMemory(pid, 0x16F1BF, HealthOn);
                ps4.Notify(222, "Infinite Health Enable [Credits Saso]");
            }
            else
            {
                byte[] HealthOn = new byte[] { 0x41, 0x89, 0x86, 0xC0, 0x00, 0x00, 0x00 };
                this.ps4.WriteMemory(pid, 0x16F1BF, HealthOn);
                ps4.Notify(222, "Infinite Health Disable [Credits Saso]");
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                byte[] AmmoOn = new byte[] { 0xE9, 0x75, 0x1A, 0x2B, 0x00, 0x90, 0x90, 0x90 };
                this.ps4.WriteMemory(pid, 0x171D96, AmmoOn);
                ps4.Notify(222, "Infinite Ammo Enable [Credits Saso]");
            }
            else
            {
                byte[] AmmoOff = new byte[] { 0x41, 0xFF, 0x8C, 0x24, 0x00, 0x04, 0x00, 0x00 };
                this.ps4.WriteMemory(pid, 0x171D96, AmmoOff);
                ps4.Notify(222, "Infinite Ammo Disable [Credits Saso]");
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                byte[] FlashLightOn = new byte[] { 0xE9, 0x52, 0x41, 0x2D, 0x00, 0x90, 0x90, 0x90 };
                this.ps4.WriteMemory(pid, 0x14F6CB, FlashLightOn);
                ps4.Notify(222, "Infinite Flash Light Enable [Credits Saso]");
            }
            else
            {
                byte[] FlashLightOff = new byte[] { 0x41, 0x89, 0x94, 0x24, 0x00, 0x04, 0x00, 0x00 };
                this.ps4.WriteMemory(pid, 0x14F6CB, FlashLightOff);
                ps4.Notify(222, "Infinite Flash Light Disable [Credits Saso]");
            }
        }
    }
}

