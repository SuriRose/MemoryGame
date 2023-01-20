using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MemoryGame
{
    public partial class frmMemoryGame1 : Form
    {

        //user clicks once, user clicks again
        //or player1 clicks once, player1 clicks twice, player2 clicks once, player2 clicks twice
        //CheckWinner
        bool gameactive = false;
        private bool buttonClicked = false;
        string currentturn = " ";
        string firstplayer = "Player 1";
        string secoundplayer = "Player 2";
        Random rnd = new Random();
        List<String> lstColorName;
        List<List<String>> StringList = new List<List<String>>();
        List<Button> lstall;
        List<List<Button>> ButtonList = new List<List<Button>>();
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        int time = 60;
        //System.Timers.Timer timer = new System.Timers.Timer();
        Button FirstClick, SecondClick;
        public frmMemoryGame1()
        {
            
                InitializeComponent();
                lstall = new List<Button>() { btnA1, btnA2, btnA3, btnA4, btnB1, btnB2, btnB3, btnB4, btnC1, btnC2, btnC3, btnC4, btnD1, btnD2, btnD3, btnD4 };

                btnPlay.Click += BtnPlay_Click;
                //foreach(b =>  ) 
                AssignColorToButtons();
                btnA1.Click += MemoryButton_Click;
                btnA2.Click += MemoryButton_Click;
                btnA3.Click += MemoryButton_Click;
                btnA4.Click += MemoryButton_Click;
                btnB1.Click += MemoryButton_Click;
                btnB2.Click += MemoryButton_Click;
                btnB3.Click += MemoryButton_Click;
                btnB4.Click += MemoryButton_Click;
                btnC1.Click += MemoryButton_Click;
                btnC2.Click += MemoryButton_Click;
                btnC3.Click += MemoryButton_Click;
                btnC4.Click += MemoryButton_Click;
                btnD1.Click += MemoryButton_Click;
                btnD2.Click += MemoryButton_Click;
                btnD3.Click += MemoryButton_Click;
                btnD4.Click += MemoryButton_Click;
                timer.Interval = 1000;
                timer.Tick += Timer_Tick;
                txtWinner.Click += DoTurn_Click;
                optSolo.Click += PlayingOptions_Click;
                optTwoPlayer.Click += PlayingOptions_Click; ;
                PlayGame();
                StartGame();
            }
            private void AssignColorToButtons()
            {   //displaymessage.random.text
                // lstColorName.Add(btn.ToString)
                foreach (Button btn in lstall) { btn.Text = "Red"; }
                //Text.ToString();
                int RandomNum;
                //loop through all buttons in btnall
                //in foreacheach loop: get random color 

                lstColorName.ForEach(btn => btn.ToString());
                {
                    // RandomNum = Random.Next( 0, lstColorName.Count);
                    // Text = lstColorName[RandomNum];
                    // StringList.RemoveAt(RandomNum);
                }
            }
            private void DoTurn_Click(object? sender, EventArgs e)
            {

                Button btn = (Button)sender;
                if (txtWinner.Text == "Player1")
                {
                    txtWinner.Text = "Player2";
                }
                else
                {
                    txtWinner.Text = "Player1";
                }
                DisplayCurrentTurn();

            }

            private void SetCurrentTurn()
            {
                if (txtWinner.Text == firstplayer)
                {
                    txtWinner.Text = "Player2";
                }
                else
                {
                    txtWinner.Text = "Player1";
                }

                txtWinner.Text = "Current Turn: " + firstplayer;
            }

            private
                bool btnClicked = false;
            private void btnClicked_Click(object sender, EventArgs e)
            {
                btnClicked = true;
            }

            private void MemoryButton_Click(object? sender, EventArgs e)
            {
                lstColorName = new List<String>() { "Red", "Red", "Orange", "Orange", "Yellow", "Yellow", "Green", "Green", "Blue", "Blue", "Purple", "Purple", "Pink", "Pink", "Aqua", "Aqua" };
                Random rnd = new Random();
                Button btn = (Button)sender;
                btn.BackColor = Color.Black;
                //btnText = lstColorName();
                //btn.Text = lstColorName;
                //btn.Enabled = false;

                btn.Text = StringList.ToString();
                if (btn.Text == "Yellow")
                {
                    btn.ForeColor = Color.Aqua;
                }
                else if (btn.Text == "Red")
                {
                    btn.ForeColor = Color.Blue;
                }
                else if (btn.Text == "Blue")
                {
                    btn.ForeColor = Color.Purple;
                }
                else if (btn.Text == "Purple")
                {
                    btn.ForeColor = Color.Orange;
                }
                else if (btn.Text == "Orange")
                {
                    btn.ForeColor = Color.Yellow;
                }
                else if (btn.Text == "Green")
                {
                    btn.ForeColor = Color.Pink;
                }
                else if (btn.Text == "Aqua")
                {
                    btn.ForeColor = Color.Red;
                }
                else if (btn.Text == "Pink")
                {
                    btn.ForeColor = Color.Green;
                }
            }
            private void DisplayMessage(string value)
            {
                lblTimer.Text = value;
            }
            private void BtnPlay_Click(object? sender, EventArgs e)
            {
                PlayGame();
            }
            private void PlayGame()
            {
                txtWinner.Text = "";
                timer.Start();
                gameactive = true;
                //foreach(btn =>  )

                timer.Start();
                lstall.ForEach(b =>
                {
                    b.Text = "";
                    b.BackColor = DefaultBackColor;
                    //if (b.Checked == true) { b.Text = "Yellow"; }

                    DisplayCurrentTurn();
                });
            }
            private void DisplayCurrentTurn()
            {
                if (optTwoPlayer.Checked)
                {
                    SetCurrentTurn();
                }
            }

            private void CheckPair()
            {

            }

            private void TimerTick()
            {
                // lblTimer.Start();
                // lblTimer.Tick += delegate;
                // var ssTime = TimeSpan.FromSeconds(time);
                // lblTimer.Text = "00: " + time.ToString();
            }

            private void Timer_Tick(object? sender, EventArgs e)
            {
                DisplayMessage(DateTime.Now.Millisecond.ToString());
                timer.Start();
                timer.Tick += delegate
                {
                    time--;
                    if (time < 0)
                    {
                        timer.Stop();
                    }
                    //var ssTime = TimeSpan.FromSeconds(time);
                    lblTimer.Text = "00: " + time.ToString();
                };

            }
            private void PlayingOptions_Click(object? sender, EventArgs e)
            {
                gameactive = true;
                PlayGame();
                StartGame();
                timer.Stop();
            }

            private void StartGame()
            {
                AssignColorToButtons();
            }
        }
    }



  