using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MemoryGame
{
    public partial class frmMemoryGame : Form
    {
        int score = 0;      
        int scoreplayer2 = 0;
        int numMatches = 0;
        string currentturn = "";
        string player1 = "Current turn: Player 1";
        string player2 = "Current turn: Player 2";
        Random rnd = new Random();
        List<string> lstColorName;
        List<string> lstColorNameCopy;
        List<Button> lstall;
        System.Windows.Forms.Timer tmcountdown = new System.Windows.Forms.Timer();
        int time = 60;
        Button Choice1;
        Button Choice2;
        private bool btnDisabled = false;
        private bool ClickOnce = false;
        private bool ClickTwice = false;

        public frmMemoryGame()
        {
            InitializeComponent();
            lstall = new List<Button>() { btnA1, btnA2, btnA3, btnA4, btnB1, btnB2, btnB3, btnB4, btnC1, btnC2, btnC3, btnC4, btnD1, btnD2, btnD3, btnD4 };
            lstColorName = new List<string>() { "Red", "Red", "Orange", "Orange", "Yellow", "Yellow", "Green", "Green", "Blue", "Blue", "Purple", "Purple", "Pink", "Pink", "Aqua", "Aqua" };
            tmTimerWait.Interval = 850;
            btnPlay.Click += BtnPlay_Click;
            lstall.ForEach(b => b.Click += MemoryButton_Click);
            optSolo.Click += PlayingOptions_Click;
            optTwoPlayer.Click += PlayingOptions_Click;
            tmTimerWait.Tick += TmTimerWait_Tick;
            StartGame();
        }
        private void StartGame()
        {
            lblMemoryMatch.Text = "Press play to start";
            lstall.ForEach(b => b.Enabled = false);
            tmcountdown.Start();
            tmcountdown.Interval = 1000;
            tmcountdown.Tick += Timer_Tick;
            tmcountdown.Stop();
        }
        private void PlayGame()
        {
            RestartTimer();
            lblTimer.Text = "00:60";
            lblMemoryMatch.Text = "Memory Match";
            foreach (Button btn in lstall)
            {
                btn.Enabled = true;
                btn.BackColor = DefaultBackColor;
                btn.ForeColor = btn.BackColor;
            }
            AssignColorToButtons();
            if (optTwoPlayer.Checked)
            {
                tmcountdown.Stop();
                lblTimer.Text = "";
            }
            lblCurrentTurn.Text = "";
            lblScore.Text = "score: ";
            if (optTwoPlayer.Checked)
            {
                lblScore.Text = "Player 1 score: ";
                lblScorePlayer2.Text = "Player 2 score: ";
            }
            score = 1;
            scoreplayer2 = 1;
            numMatches = 0;
        }
        private void AssignColorToButtons()
        {
            int RandomNum;
            lstColorNameCopy = new List<string>(lstColorName);
            foreach (Button btn in lstall)
            {
                RandomNum = rnd.Next(0, lstColorNameCopy.Count);
                btn.Text = lstColorNameCopy[RandomNum];
                lstColorNameCopy.RemoveAt(RandomNum);
            }
        }
        private void ClickedButton(Button btn)
        {
            if (Choice1 != null && Choice2 != null)
            { return; }
            btn.BackColor = Color.Black;
            btn.ForeColor = GetForeColor();
            if (Choice1 == null)
            {
                Choice1 = btn;
                ClickOnce = true;
                DisplayCurrentTurn();
            }
            else
            {
                Choice2 = btn;
                if (ClickOnce == true && Choice1 == Choice2)
                {
                    ClickTwice = false;
                    Choice2 = null;
                }
                else
                {
                    ClickTwice = true;
                    CheckMatch();
                    DisableButton();
                }
            }
        }
        private void DisableButton()
        {
            if (btnDisabled == true)
            { return; }
        }
        private void CheckMatch()
        {
            if (Choice1.Text == Choice2.Text)
            {
                numMatches += 1;
                DisableMatch(Choice1);
                DisableMatch(Choice2);
                Choice1 = null;
                Choice2 = null;
                if (lblCurrentTurn.Text == player2)
                {
                    ScorePlayer2();
                    scoreplayer2 += 1;
                }
                else
                {
                    Score();
                    score += 1;
                }
            }
            else
            {
                tmTimerWait.Start();
            }
            CheckWinner();
        }
        private void DisableMatch(Button btn)
        {
            btn.Enabled = false;
            btn.BackColor = Color.Lime;
        }
        private void Score()
        {
            if (optSolo.Checked)
            { lblScore.Text = "score: " + score.ToString(); }
            else
            {
                if (lblCurrentTurn.Text == player1)
                { lblScore.Text = " Player1 score: " + score.ToString(); }
            }
        }
        private void ScorePlayer2()
        {
            if (lblCurrentTurn.Text == player2)
            {
                lblScorePlayer2.Text = "Player2 score: " + scoreplayer2.ToString();
            }
        }
        private void CheckWinner()
        {
            if (optSolo.Checked && numMatches == 8)
            {
                tmcountdown.Stop();
                SoundEffect();
                MessageBox.Show("You won in " + (60 - time) + " seconds!");
            }
            else if (optTwoPlayer.Checked && numMatches == 8)
            {
                SoundEffect();
                if (score > scoreplayer2)
                {
                    MessageBox.Show("Player 1 won!");
                }
                else if (scoreplayer2 > score)
                {
                    { MessageBox.Show("Player 2 won!"); }
                }
                else if (score == scoreplayer2)
                {
                    MessageBox.Show("Tie!");
                }
            }
        }
        private void DisplayCurrentTurn()
        {
            if (optTwoPlayer.Checked)
            {
                lblCurrentTurn.Text = currentturn;
                if (currentturn == player2 && ClickOnce == true && ClickTwice == true)
                {
                    currentturn = player1;
                }
                else
                {
                    currentturn = player2;
                }
            }
        }
        private void RestartTimer()
        {
            tmcountdown.Stop();
            time = 60;
            lblTimer.Text = "00: " + time;
            tmcountdown.Start();
        }
        private void ResetButton(Button btn)
        {
            btn.BackColor = DefaultBackColor;
            btn.ForeColor = btn.BackColor;
        }
        private void OptSoloTwoPlayer()
        {
            if (optSolo.Checked)
            {
                lblScorePlayer2.Hide();
                lblCurrentTurn.Text = "";
            }
            else if (optTwoPlayer.Checked)
            {
                lblCurrentTurn.Text = "";
                tmcountdown.Stop();
                lblScorePlayer2.Show();
                lblTimer.Text = "";
                currentturn = player1;
                Choice1 = null;
                Choice2 = null;
            }
        }
        private Color GetForeColor()
        {
            Random rnd = new Random();
            Color c = Color.FromArgb(rnd.Next(40, 256), rnd.Next(40, 256), rnd.Next(40, 256));
            return c;
        }
        private void SoundEffect()
        {
            SystemSounds.Exclamation.Play();
        }
        private void MemoryButton_Click(object? sender, EventArgs e)
        {
            Button btn = (Button)sender;
            ClickedButton(btn);
        }
        private void TmTimerWait_Tick(object? sender, EventArgs e)
        {
            tmTimerWait.Enabled = false;
            ResetButton(Choice1);
            ResetButton(Choice2);
            lblCurrentTurn.Text = "";
            Choice1 = null;
            Choice2 = null;
        }
        private void Timer_Tick(object? sender, EventArgs e)
        {
            if (time > 0)
            {
                time = time - 1;
                lblTimer.Text = "00: " + time;
            }
            else
            {
                tmcountdown.Stop();
                foreach (Button btn in lstall)
                {
                    btn.Enabled = false;
                }
            }
        }
        private void PlayingOptions_Click(object? sender, EventArgs e)
        {
            OptSoloTwoPlayer();
        }
        private void BtnPlay_Click(object? sender, EventArgs e)
        {
            PlayGame();
        }
    }
}


