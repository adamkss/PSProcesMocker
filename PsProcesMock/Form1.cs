using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TCPClient;
using WindowsFormsApp1.HelperClasses;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        List<PictureBox> levelPictureBoxes = new List<PictureBox>();
        List<PictureBox> pumpPictureBoxes = new List<PictureBox>();
        List<PictureBox> sensorPictureBoxes = new List<PictureBox>();
        int levelFilled = 0;
        int prevFillLevel = 0;
        int[] upIntervals = new int[] { 0, 1200, 900, 600, 300 };
        bool goingUp = true;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for(int i = 0; i < 18; i++)
            {
                PictureBox tempBox = new PictureBox();
                tempBox.Dock = DockStyle.Fill;
                tempBox.Margin = new Padding(0, 0, 0, 2);
                tempBox.BackColor = Color.Gray;
                levelPictureBoxes.Add(tempBox);

                tableLayoutPanel1.Controls.Add(tempBox);
            }
            levelPictureBoxes.Reverse();
            pumpPictureBoxes.Add(pictureBox1);
            pumpPictureBoxes.Add(pictureBox2);
            pumpPictureBoxes.Add(pictureBox3);
            pumpPictureBoxes.Add(pictureBox4);
            deactivateAllPumps();

            sensorPictureBoxes.Add(pictureBox5);
            sensorPictureBoxes.Add(pictureBox6);
            sensorPictureBoxes.Add(pictureBox7);
            sensorPictureBoxes.Add(pictureBox8);
            sensorPictureBoxes.ForEach(p => p.BackColor = Color.LightBlue);
        }

        private void levelUp()
        {
            if(levelFilled < 18)
                levelPictureBoxes[levelFilled++].BackColor = Color.Red;
            activateAdecvateSensors();
            sendCurrentStatus();
        }
        private void levelDown()
        {
            if(levelFilled > 0)
                levelPictureBoxes[--levelFilled].BackColor = Color.Gray;
            activateAdecvateSensors();
            sendCurrentStatus();
        }
        private void activateAdecvateSensors()
        {
            if(levelFilled >= 2)
            {
                sensorPictureBoxes[0].BackColor = Color.Red;
                if(levelFilled >= 9)
                {
                    sensorPictureBoxes[1].BackColor = Color.Red;
                    if(levelFilled >= 15)
                    {
                        sensorPictureBoxes[2].BackColor = Color.Red;
                        alarmPicture.Visible = true;
                        if(levelFilled == 18)
                        {
                            sensorPictureBoxes[3].BackColor = Color.Red;
                        }
                        else
                        {
                            sensorPictureBoxes[3].BackColor = Color.LightBlue;
                        }
                    }
                    else
                    {
                        sensorPictureBoxes[2].BackColor = Color.LightBlue;
                        alarmPicture.Visible = false;
                    }
                }
                else
                {
                    sensorPictureBoxes[1].BackColor = Color.LightBlue;
                }
            }
            else
            {
                sensorPictureBoxes[0].BackColor = Color.LightBlue;
            }
        }
        private void activatePump(int nrOfPump)
        {
            pumpPictureBoxes[nrOfPump - 1].BackColor = Color.Red;
        }

        private void deactivatePump(int nrOfPump)
        {
            pumpPictureBoxes[nrOfPump - 1].BackColor = Color.Gray;
        }
        private void deactivateAllPumps()
        {
            pumpPictureBoxes.ForEach(p => p.BackColor = Color.Gray);
        }
        private void activateAllPumpsUntil(int lastToActivate)
        {
            deactivateAllPumps();
            for(int i = 0; i < lastToActivate; i++)
            {
                pumpPictureBoxes[i].BackColor = Color.Red;
            }
        }
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            int value = trackBar1.Value;
            if(value == 0)
            {
                upTimer.Stop();
            }
            else
            {
                upTimer.Interval = upIntervals[value];
                upTimer.Start();
            }
            if(goingUp)
            {
                activateAllPumpsUntil(value); 
            }
            
        }

        private void upTimerTick(object sender, EventArgs e)
        {
            if (trackBar2.Value == 1)
                levelDown();
            else
                levelUp();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            if(trackBar2.Value == 1)
            {
                goingUp = false;
                deactivateAllPumps();
            }
            else
            {
                goingUp = true;
                activateAllPumpsUntil(trackBar1.Value);
            }
        }

   

        private async void sendCurrentStatus()
        {
            /*  
             *  
             *  Byte1
             *  0 - I0.0 butonul care stinge toate pompele
             *  1 - I0.7 senzor mijloc
             *  2 - Q0.0 prima pompa
             *  3 - Q0.1 pompa 2
             *  4 - Q0.2 pompa 3
             *  5 - Q0.3 pompa 4
             *  6 - I0.6 senzor alarma      
             *  7 - 0
             *  
             *  
             *  Byte 2
             *  0 - Q0.5 alarma
             *  1 - I0.1 p1
             *  2 - I0.2 p2
             *  3 - I0.3 p3
             *  4 - I0.4 p4
             *  5 - I8.0 sus
             *  6 - I8.1 jos
             *  7 - 1
             */
            byte byte1 = 0, byte2 = 0;
            byte1 = BitHelper.AddBit(byte1, 0, stingeToateButton.Checked);
            byte1 = BitHelper.AddBit(byte1, 1, sensorPictureBoxes[1].BackColor==Color.Red);
            byte1 = BitHelper.AddBit(byte1, 2, pumpPictureBoxes[0].BackColor==Color.Red);
            byte1 = BitHelper.AddBit(byte1, 3, pumpPictureBoxes[1].BackColor == Color.Red);
            byte1 = BitHelper.AddBit(byte1, 4, pumpPictureBoxes[2].BackColor == Color.Red);
            byte1 = BitHelper.AddBit(byte1, 5, pumpPictureBoxes[3].BackColor == Color.Red);
            byte1 = BitHelper.AddBit(byte1, 6, sensorPictureBoxes[2].BackColor==Color.Red);
            byte1 = BitHelper.AddBit(byte1, 7, false);

            byte2 = BitHelper.AddBit(byte2, 0, alarmPicture.Visible);
            byte2 = BitHelper.AddBit(byte2, 1, p1Button.Checked);
            byte2 = BitHelper.AddBit(byte2, 2, p2Button.Checked);
            byte2 = BitHelper.AddBit(byte2, 3, p3Button.Checked);
            byte2 = BitHelper.AddBit(byte2, 4, p4Button.Checked);
            byte2 = BitHelper.AddBit(byte2, 5, sensorPictureBoxes[3].BackColor==Color.Red);
            byte2 = BitHelper.AddBit(byte2, 6, sensorPictureBoxes[0].BackColor == Color.Red);
            byte2 = BitHelper.AddBit(byte2, 7, true);
            await Client.sendByte(byte1);
            await Client.sendByte(byte2);
        }

    }
}
