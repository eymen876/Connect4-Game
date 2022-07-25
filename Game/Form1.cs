using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game
{
    public partial class Form1 : Form
    {
        static int nRows = 9;
        public static int[,]board = new int [nRows,nRows];
        int currentPlayer = 1;
        int chosenColumn;
        bool[] columnFull = new bool[nRows];
        Panel[,] grid = new Panel[nRows, nRows];
        Button[] buttons = new Button[nRows];
        Color p1color = Color.Red;
        Color p2color = Color.Yellow;
        Color background = Color.Black;
        Color winColor = Color.White;
        int Height = 30;
        int Width = 30;
        int timer = 5;
        int timervalue = 5;


        public Form1()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for(int i = 0; i < nRows; i++)
            {
                for (int j = 0; j < nRows; j++)
                {
                    grid[i, j] = new Panel();
                    grid[i, j].Height = Height;
                    grid[i, j].Width = Width;
                    grid[i, j].BorderStyle = BorderStyle.FixedSingle;
                    grid[i, j].BackColor = background;
                    grid[i, j].Visible = true;
                    grid[i, j].Location = new Point(i * Width, j * Height);
                    panel1.Controls.Add(grid[i, j]);
                }
                buttons[i] = new Button();
                buttons[i].Height =Height;
                buttons[i].Width = Width;
                buttons[i].Text = (i).ToString();
                buttons[i].Location = new Point(i * Width, nRows * Height);
                buttons[i].Click += new EventHandler(PlaceTheTile);
                panel1.Controls.Add(buttons[i]);

            }
        }
        public void PlaceTheTile(object sender, EventArgs e)
        {
            chosenColumn = Int32.Parse(((Button)sender).Text);
            PlaceInChosen();

        }
        public void PlaceInChosen()
        {
            int index = nRows - 1;
            int slot = Form1.board[index, chosenColumn];
            while ((slot == 1 || slot == 2) && index > 0)
            {
                index--;
                slot = Form1.board[index, chosenColumn]; ;
            }
            if ((slot == 1 || slot == 2) && index == 0)
            {
                columnFull[chosenColumn] = true;
            }
            if (columnFull[chosenColumn] != true)
            {
                Form1.board[index, chosenColumn] = currentPlayer;
                if (currentPlayer == 1)
                {
                    grid[chosenColumn, index].BackColor = p1color;
                    timer = timervalue;
                    timer1.Start();
                }
                else
                {
                    grid[chosenColumn, index].BackColor = p2color;
                    timer = timervalue;
                    timer1.Start();
                }

            }
            //check win here
            if (isWin())
            {
                label3.Text = "Player " + currentPlayer + " has won!";
                MessageBox.Show("Player " + currentPlayer + " has won!");
                if(currentPlayer == 1)
                {
                    BackColor = p1color;
                }
                else
                {
                    BackColor = p2color;
                }

            }

            if(!isWin() && columnFull[chosenColumn]==true)
            {
                label3.Text = "Column "+ chosenColumn +" is full";
            }
            else if (!isWin() && currentPlayer == 1)
            {
                currentPlayer = 2;
                label3.Text = "Player " + currentPlayer + "'s Turn";
                panel2.BackColor = p2color;
            }
            else if(!isWin() && currentPlayer == 2)
            {
                currentPlayer = 1;
                label3.Text = "Player " + currentPlayer + "'s Turn";
                panel2.BackColor = p1color;
            }


        }
        public bool isWin()
        {
            for (int r=0;r <nRows;r++)
            {
                for (int c = 0; c < nRows; c++)
                {
                    if (c<nRows-3 && board[r, c] == currentPlayer && board[r, c + 1] == currentPlayer && board[r, c + 2] == currentPlayer && board[r, c + 3] == currentPlayer)
                    {
                        grid[c,r].BackColor = winColor;
                        grid[c+1,r].BackColor = winColor;
                        grid[c+2,r].BackColor = winColor;
                        grid[c+3,r].BackColor = winColor; 

                        return true;
                    }
                    else if (r < nRows - 3 && board[r, c]==currentPlayer && board[r+1,c]== currentPlayer && board[r+2, c] == currentPlayer&&board[r+3,c]==currentPlayer)
                    {
                        grid[c,r].BackColor = winColor;
                        grid[c,r+1].BackColor = winColor;
                        grid[c,r+2].BackColor = winColor;
                        grid[c, r + 3].BackColor = winColor;
                        return true;
                    }
                    else if(c < nRows - 3 && r < nRows - 3 && board[r, c]==currentPlayer && board[r+1,c+1]== currentPlayer &&board[r+2,c+2]==currentPlayer && board[r+3,c+3]==currentPlayer)
                    {
                        grid[c, r].BackColor = winColor;
                        grid[c+1, r+1].BackColor = winColor;
                        grid[c+2, r+2].BackColor = winColor;
                        grid[c+3, r+3].BackColor = winColor;
                        return true;
                    }
                    else if( c<nRows-3 && r>=3 && board[r,c]==currentPlayer && board[r-1,c+1]== currentPlayer && board[r-2,c+2] == currentPlayer && board[r - 3, c + 3] == currentPlayer)
                    {
                        grid[c, r].BackColor=winColor;
                        grid[c+1,r-1].BackColor = winColor; 
                        grid[c+2,r-2].BackColor = winColor;
                        grid[c+3, r-3].BackColor = winColor;
                        return true;
                    }


                }
            }
            return false;
        }

        private void restart_Click(object sender, EventArgs e)
        {
            currentPlayer = 1;
            label3.Text = "Game has been restarted\nPlayer "+currentPlayer+"'s turn";
            colorDialog1.Reset();
            colorDialog2.Reset();
            BackColor = Color.White;
            timer1.Stop();
            for (int i = 0; i < nRows; i++)
            {
                for (int j = 0; j < nRows; j++)
                {
                    grid[i, j].BackColor = background;
                    board[i, j] = 0;
                }
                columnFull[i] = false;
            }

        }

        private void player1_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            player1.BackColor = colorDialog1.Color;
            p1color =colorDialog1.Color;

        }

        private void player2_Click(object sender, EventArgs e)
        {
            colorDialog2.ShowDialog();
            player2.BackColor= colorDialog2.Color;
            p2color = colorDialog2.Color;
            
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (isWin())
            {
                timer1.Stop();
            }


            if (timer > 0)
            {
                timer -= 1;
                TimerTextbox.Text = timer.ToString();

            }
            else
            {
                timer = timervalue;

                if (currentPlayer == 1)
                {
                    currentPlayer = 2;
                    label3.Text = "Player " + currentPlayer + "'s Turn";
                    panel2.BackColor = p2color;
                }
                else if(currentPlayer == 2)
                {
                    currentPlayer = 1;
                    label3.Text = "Player " + currentPlayer + "'s Turn";
                    panel2.BackColor = p1color;
                }
            }
        }

        private void CustomizeButton_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            form.Show();

        }
    }
}
