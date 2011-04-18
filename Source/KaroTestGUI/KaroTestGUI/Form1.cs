﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KaroEngine;
using System.Runtime.InteropServices;

namespace KaroTestGUI
{

    public partial class Form1 : Form
    {
        KaroEngine.KaroEngine   engine;
        Pen                     penBlack;
        Pen                     penGray;
        Brush                   brushBlack;
        Brush                   brushWhite;
        Brush                   brushRed;
        Brush                   brushBlue;
        int                     boxSize = 25;
        bool                    gameOver = false; 

        Point                   clickedFirst;
        Point                   clickedSecond;

        KaroEngine.Managed_Tile[] board; 

        public Form1()
        {
            engine = new KaroEngine.KaroEngine();
            penBlack        = Pens.Black;
            penGray         = new Pen(Color.Gray, 2);    
            brushBlack      = Brushes.Black;            
            brushRed        = Brushes.Red;
            brushWhite      = Brushes.White;
            brushBlue       = Brushes.Blue;

            clickedFirst    = new Point(-1, -1);
            clickedSecond   = new Point(-1, -1);

            board = new Managed_Tile[225];

            unsafe
            {
                IntPtr intp = (IntPtr)engine.GetBoard();
                int[] data = new int[225];
                Marshal.Copy(intp, data, 0, 225);

                for (int i = 0; i < data.Length; i++)
                {
                    board[i] = (KaroEngine.Managed_Tile)Enum.Parse(typeof(KaroEngine.Managed_Tile), data[i].ToString());
                }
            }

            InitializeComponent();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;  
          
            for (int y = 0; y < 15; y++)
            {
                for (int x = 0; x < 15; x++) 
                {
                    //Draw the board
                    if (board[(y * 15) + x] != KaroEngine.Managed_Tile.EMPTY) {
                        g.FillRectangle(brushBlack, x * boxSize, y * boxSize, boxSize, boxSize);
                    }

                    //draw the 'selected' tiles
                    if (x == clickedFirst.X && y == clickedFirst.Y)
                    {
                        g.FillRectangle(brushBlue, x * boxSize, y * boxSize, boxSize, boxSize);
                    }
                    if (x == clickedSecond.X && y == clickedSecond.Y)
                    {
                        g.FillRectangle(brushBlue, x * boxSize, y * boxSize, boxSize, boxSize);
                    }

                    //check what kind of tiles, pawns etc are on the board.
                    switch (board[(y * 15) + x]) { 
                        case KaroEngine.Managed_Tile.EMPTY:
                            break;
                        case KaroEngine.Managed_Tile.SOLIDTILE:
                            break;
                        case KaroEngine.Managed_Tile.MOVEABLETILE:
                            break;
                        case KaroEngine.Managed_Tile.REDUNMARKED:
                            g.FillEllipse(brushRed, x * boxSize+1, y * boxSize+1, boxSize-2, boxSize-2);                            
                            break;
                        case KaroEngine.Managed_Tile.REDMARKED:
                            g.FillEllipse(brushRed, x * boxSize + 1, y * boxSize + 1, boxSize - 2, boxSize -2 );
                            g.DrawEllipse(penGray, x * boxSize + 5, y * boxSize + 5, boxSize - 10, boxSize - 10);
                            break;
                        case KaroEngine.Managed_Tile.WHITEMARKED:
                            g.FillEllipse(brushWhite, x * boxSize + 1, y * boxSize + 1, boxSize - 2, boxSize - 2);                            
                            break;
                        case KaroEngine.Managed_Tile.WHITEUNMARKED:
                            g.FillEllipse(brushWhite, x * boxSize + 1, y * boxSize + 1, boxSize - 2, boxSize - 2);
                            g.DrawEllipse(penGray, x * boxSize + 5, y * boxSize + 5, boxSize - 10, boxSize - 10);
                            break;                    
                    }

                    //Draw a grid
                    g.DrawRectangle(Pens.Gray, x * boxSize, y * boxSize, boxSize, boxSize);
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (!this.gameOver)
            {
                if (clickedFirst.X == -1)
                {
                    clickedFirst.X = (e.X - 1) / this.boxSize;
                    clickedFirst.Y = (e.Y - 1) / this.boxSize;
                    board[(clickedFirst.Y * 15) + clickedFirst.X] = KaroEngine.Managed_Tile.REDMARKED;
                }
                else if (clickedSecond.X == -1)
                {
                    clickedSecond.X = (e.X - 1) / this.boxSize;
                    clickedSecond.Y = (e.Y - 1) / this.boxSize;
                    board[(clickedSecond.Y * 15) + clickedSecond.X] = KaroEngine.Managed_Tile.WHITEMARKED;
                }
                else 
                {
                    clickedFirst = new Point(-1, -1);
                    clickedSecond = new Point(-1, -1);
                }
            }

            pictureBox1.Invalidate();
        }
    }
}
