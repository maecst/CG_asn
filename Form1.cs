﻿using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Text;
using System.Drawing.Drawing2D;

namespace asgn5v1
{
    /// <summary>
    /// Summary description for Transformer.
    /// </summary>
    public class Transformer : System.Windows.Forms.Form
    {
        private System.ComponentModel.IContainer components;
        //private bool GetNewData();

        // basic data for Transformer
        int numpts = 0;
        int numlines = 0;
        bool gooddata = false;
        double[,] vertices;                     // initial set of points
        double[,] scrnpts;                      // current set of points
        double[,] ctrans = new double[4, 4];    // your main transformation matrix
        double centreToBaseline;                // distance from centre of shape to baseline (y = 0)
        Timer timer = new Timer();
        

        int toolBar1Width = 24;

        private System.Windows.Forms.ImageList tbimages;
        private System.Windows.Forms.ToolBar toolBar1;
        private System.Windows.Forms.ToolBarButton transleftbtn;
        private System.Windows.Forms.ToolBarButton transrightbtn;
        private System.Windows.Forms.ToolBarButton transupbtn;
        private System.Windows.Forms.ToolBarButton transdownbtn;
        private System.Windows.Forms.ToolBarButton toolBarButton1;
        private System.Windows.Forms.ToolBarButton scaleupbtn;
        private System.Windows.Forms.ToolBarButton scaledownbtn;
        private System.Windows.Forms.ToolBarButton toolBarButton2;
        private System.Windows.Forms.ToolBarButton rotxby1btn;
        private System.Windows.Forms.ToolBarButton rotyby1btn;
        private System.Windows.Forms.ToolBarButton rotzby1btn;
        private System.Windows.Forms.ToolBarButton toolBarButton3;
        private System.Windows.Forms.ToolBarButton rotxbtn;
        private System.Windows.Forms.ToolBarButton rotybtn;
        private System.Windows.Forms.ToolBarButton rotzbtn;
        private System.Windows.Forms.ToolBarButton toolBarButton4;
        private System.Windows.Forms.ToolBarButton shearrightbtn;
        private System.Windows.Forms.ToolBarButton shearleftbtn;
        private System.Windows.Forms.ToolBarButton toolBarButton5;
        private System.Windows.Forms.ToolBarButton resetbtn;
        private System.Windows.Forms.ToolBarButton exitbtn;
        int[,] lines;

        public Transformer()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //

            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            Text = "COMP 4560:  Assignment 5 (200830) (Mae Yee)";
            ResizeRedraw = true;
            BackColor = Color.Black;
            MenuItem miNewDat = new MenuItem("New &Data...",
                new EventHandler(MenuNewDataOnClick));
            MenuItem miExit = new MenuItem("E&xit",
                new EventHandler(MenuFileExitOnClick));
            MenuItem miDash = new MenuItem("-");
            MenuItem miFile = new MenuItem("&File",
                new MenuItem[] { miNewDat, miDash, miExit });
            MenuItem miAbout = new MenuItem("&About",
                new EventHandler(MenuAboutOnClick));
            Menu = new MainMenu(new MenuItem[] { miFile, miAbout });


        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Transformer));
            this.tbimages = new System.Windows.Forms.ImageList(this.components);
            this.toolBar1 = new System.Windows.Forms.ToolBar();
            this.transleftbtn = new System.Windows.Forms.ToolBarButton();
            this.transrightbtn = new System.Windows.Forms.ToolBarButton();
            this.transupbtn = new System.Windows.Forms.ToolBarButton();
            this.transdownbtn = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
            this.scaleupbtn = new System.Windows.Forms.ToolBarButton();
            this.scaledownbtn = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton2 = new System.Windows.Forms.ToolBarButton();
            this.rotxby1btn = new System.Windows.Forms.ToolBarButton();
            this.rotyby1btn = new System.Windows.Forms.ToolBarButton();
            this.rotzby1btn = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton3 = new System.Windows.Forms.ToolBarButton();
            this.rotxbtn = new System.Windows.Forms.ToolBarButton();
            this.rotybtn = new System.Windows.Forms.ToolBarButton();
            this.rotzbtn = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton4 = new System.Windows.Forms.ToolBarButton();
            this.shearrightbtn = new System.Windows.Forms.ToolBarButton();
            this.shearleftbtn = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton5 = new System.Windows.Forms.ToolBarButton();
            this.resetbtn = new System.Windows.Forms.ToolBarButton();
            this.exitbtn = new System.Windows.Forms.ToolBarButton();
            this.SuspendLayout();
            // 
            // tbimages
            // 
            this.tbimages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("tbimages.ImageStream")));
            this.tbimages.TransparentColor = System.Drawing.Color.Transparent;
            this.tbimages.Images.SetKeyName(0, "");
            this.tbimages.Images.SetKeyName(1, "");
            this.tbimages.Images.SetKeyName(2, "");
            this.tbimages.Images.SetKeyName(3, "");
            this.tbimages.Images.SetKeyName(4, "");
            this.tbimages.Images.SetKeyName(5, "");
            this.tbimages.Images.SetKeyName(6, "");
            this.tbimages.Images.SetKeyName(7, "");
            this.tbimages.Images.SetKeyName(8, "");
            this.tbimages.Images.SetKeyName(9, "");
            this.tbimages.Images.SetKeyName(10, "");
            this.tbimages.Images.SetKeyName(11, "");
            this.tbimages.Images.SetKeyName(12, "");
            this.tbimages.Images.SetKeyName(13, "");
            this.tbimages.Images.SetKeyName(14, "");
            this.tbimages.Images.SetKeyName(15, "");
            // 
            // toolBar1
            // 
            this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.transleftbtn,
            this.transrightbtn,
            this.transupbtn,
            this.transdownbtn,
            this.toolBarButton1,
            this.scaleupbtn,
            this.scaledownbtn,
            this.toolBarButton2,
            this.rotxby1btn,
            this.rotyby1btn,
            this.rotzby1btn,
            this.toolBarButton3,
            this.rotxbtn,
            this.rotybtn,
            this.rotzbtn,
            this.toolBarButton4,
            this.shearrightbtn,
            this.shearleftbtn,
            this.toolBarButton5,
            this.resetbtn,
            this.exitbtn});
            this.toolBar1.Dock = System.Windows.Forms.DockStyle.Right;
            this.toolBar1.DropDownArrows = true;
            this.toolBar1.ImageList = this.tbimages;
            this.toolBar1.Location = new System.Drawing.Point(484, 0);
            this.toolBar1.Name = "toolBar1";
            this.toolBar1.ShowToolTips = true;
            this.toolBar1.Size = new System.Drawing.Size(toolBar1Width, 306);
            this.toolBar1.TabIndex = 0;
            this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
            // 
            // transleftbtn
            // 
            this.transleftbtn.ImageIndex = 1;
            this.transleftbtn.Name = "transleftbtn";
            this.transleftbtn.ToolTipText = "translate left";
            // 
            // transrightbtn
            // 
            this.transrightbtn.ImageIndex = 0;
            this.transrightbtn.Name = "transrightbtn";
            this.transrightbtn.ToolTipText = "translate right";
            // 
            // transupbtn
            // 
            this.transupbtn.ImageIndex = 2;
            this.transupbtn.Name = "transupbtn";
            this.transupbtn.ToolTipText = "translate up";
            // 
            // transdownbtn
            // 
            this.transdownbtn.ImageIndex = 3;
            this.transdownbtn.Name = "transdownbtn";
            this.transdownbtn.ToolTipText = "translate down";
            // 
            // toolBarButton1
            // 
            this.toolBarButton1.Name = "toolBarButton1";
            this.toolBarButton1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // scaleupbtn
            // 
            this.scaleupbtn.ImageIndex = 4;
            this.scaleupbtn.Name = "scaleupbtn";
            this.scaleupbtn.ToolTipText = "scale up";
            // 
            // scaledownbtn
            // 
            this.scaledownbtn.ImageIndex = 5;
            this.scaledownbtn.Name = "scaledownbtn";
            this.scaledownbtn.ToolTipText = "scale down";
            // 
            // toolBarButton2
            // 
            this.toolBarButton2.Name = "toolBarButton2";
            this.toolBarButton2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // rotxby1btn
            // 
            this.rotxby1btn.ImageIndex = 6;
            this.rotxby1btn.Name = "rotxby1btn";
            this.rotxby1btn.ToolTipText = "rotate about x by 1";
            // 
            // rotyby1btn
            // 
            this.rotyby1btn.ImageIndex = 7;
            this.rotyby1btn.Name = "rotyby1btn";
            this.rotyby1btn.ToolTipText = "rotate about y by 1";
            // 
            // rotzby1btn
            // 
            this.rotzby1btn.ImageIndex = 8;
            this.rotzby1btn.Name = "rotzby1btn";
            this.rotzby1btn.ToolTipText = "rotate about z by 1";
            // 
            // toolBarButton3
            // 
            this.toolBarButton3.Name = "toolBarButton3";
            this.toolBarButton3.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // rotxbtn
            // 
            this.rotxbtn.ImageIndex = 9;
            this.rotxbtn.Name = "rotxbtn";
            this.rotxbtn.ToolTipText = "rotate about x continuously";
            // 
            // rotybtn
            // 
            this.rotybtn.ImageIndex = 10;
            this.rotybtn.Name = "rotybtn";
            this.rotybtn.ToolTipText = "rotate about y continuously";
            // 
            // rotzbtn
            // 
            this.rotzbtn.ImageIndex = 11;
            this.rotzbtn.Name = "rotzbtn";
            this.rotzbtn.ToolTipText = "rotate about z continuously";
            // 
            // toolBarButton4
            // 
            this.toolBarButton4.Name = "toolBarButton4";
            this.toolBarButton4.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // shearrightbtn
            // 
            this.shearrightbtn.ImageIndex = 12;
            this.shearrightbtn.Name = "shearrightbtn";
            this.shearrightbtn.ToolTipText = "shear right";
            // 
            // shearleftbtn
            // 
            this.shearleftbtn.ImageIndex = 13;
            this.shearleftbtn.Name = "shearleftbtn";
            this.shearleftbtn.ToolTipText = "shear left";
            // 
            // toolBarButton5
            // 
            this.toolBarButton5.Name = "toolBarButton5";
            this.toolBarButton5.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // resetbtn
            // 
            this.resetbtn.ImageIndex = 14;
            this.resetbtn.Name = "resetbtn";
            this.resetbtn.ToolTipText = "restore the initial image";
            // 
            // exitbtn
            // 
            this.exitbtn.ImageIndex = 15;
            this.exitbtn.Name = "exitbtn";
            this.exitbtn.ToolTipText = "exit the program";
            // 
            // Transformer
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(508, 306);
            this.Controls.Add(this.toolBar1);
            this.Name = "Transformer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Transformer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.Run(new Transformer());
        }

        protected override void OnPaint(PaintEventArgs pea)
        {
            Graphics grfx = pea.Graphics;
            Pen pen = new Pen(Color.White, 3);
            double temp;
            int k;

            // draw a red dot at the centre
            grfx.FillRectangle(
                Brushes.Yellow, 
                this.ClientSize.Width / 2 - 1 - toolBar1Width / 2, 
                this.ClientSize.Height / 2 - 1, 
                2, 
                2);

            if (gooddata)
            {
                // create the screen coordinates:
                // scrnpts = vertices * ctrans
                for (int i = 0; i < numpts; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        temp = 0.0d;
                        for (k = 0; k < 4; k++)
                            temp += vertices[i, k] * ctrans[k, j];
                        scrnpts[i, j] = temp;
                    }
                }

                // now draw the lines
                for (int i = 0; i < numlines; i++)
                {
                    grfx.DrawLine(pen, (int)scrnpts[lines[i, 0], 0], (int)scrnpts[lines[i, 0], 1],
                        (int)scrnpts[lines[i, 1], 0], (int)scrnpts[lines[i, 1], 1]);
                }


            } // end of gooddata block	
        } // end of OnPaint

        void MenuNewDataOnClick(object obj, EventArgs ea)
        {
            //MessageBox.Show("New Data item clicked.");
            gooddata = GetNewData();
            RestoreInitialImage();
        }

        void MenuFileExitOnClick(object obj, EventArgs ea)
        {
            Close();
        }

        void MenuAboutOnClick(object obj, EventArgs ea)
        {
            AboutDialogBox dlg = new AboutDialogBox();
            dlg.ShowDialog();
        }

        void RestoreInitialImage()
        {
            Invalidate();
            setIdentity(ctrans, 4, 4);
        } // end of RestoreInitialImage

        bool GetNewData()
        {
            string strinputfile, text;
            ArrayList coorddata = new ArrayList();
            ArrayList linesdata = new ArrayList();
            OpenFileDialog opendlg = new OpenFileDialog();
            opendlg.Title = "Choose File with Coordinates of Vertices";
            if (opendlg.ShowDialog() == DialogResult.OK)
            {
                strinputfile = opendlg.FileName;
                FileInfo coordfile = new FileInfo(strinputfile);
                StreamReader reader = coordfile.OpenText();
                do
                {
                    text = reader.ReadLine();
                    if (text != null) coorddata.Add(text);
                } while (text != null);
                reader.Close();
                DecodeCoords(coorddata);
            }
            else
            {
                MessageBox.Show("***Failed to Open Coordinates File***");
                return false;
            }

            opendlg.Title = "Choose File with Data Specifying Lines";
            if (opendlg.ShowDialog() == DialogResult.OK)
            {
                strinputfile = opendlg.FileName;
                FileInfo linesfile = new FileInfo(strinputfile);
                StreamReader reader = linesfile.OpenText();
                do
                {
                    text = reader.ReadLine();
                    if (text != null) linesdata.Add(text);
                } while (text != null);
                reader.Close();
                DecodeLines(linesdata);
            }
            else
            {
                MessageBox.Show("***Failed to Open Line Data File***");
                return false;
            }
            scrnpts = new double[numpts, 4];
            setIdentity(ctrans, 4, 4);  //initialize transformation matrix to identity
            return true;
        } // end of GetNewData

        void DecodeCoords(ArrayList coorddata)
        {
            //this may allocate slightly more rows that necessary
            vertices = new double[coorddata.Count, 4];
            numpts = 0;
            string[] text = null;
            for (int i = 0; i < coorddata.Count; i++)
            {
                text = coorddata[i].ToString().Split(' ', ',');
                vertices[numpts, 0] = double.Parse(text[0]);
                if (vertices[numpts, 0] < 0.0d) break;
                vertices[numpts, 1] = double.Parse(text[1]);
                vertices[numpts, 2] = double.Parse(text[2]);
                vertices[numpts, 3] = 1.0d;
                numpts++;
            }

        }// end of DecodeCoords

        void DecodeLines(ArrayList linesdata)
        {
            //this may allocate slightly more rows that necessary
            lines = new int[linesdata.Count, 2];
            numlines = 0;
            string[] text = null;
            for (int i = 0; i < linesdata.Count; i++)
            {
                text = linesdata[i].ToString().Split(' ', ',');
                lines[numlines, 0] = int.Parse(text[0]);
                if (lines[numlines, 0] < 0) break;
                lines[numlines, 1] = int.Parse(text[1]);
                numlines++;
            }
        } // end of DecodeLines

        void setIdentity(double[,] A, int nrow, int ncol)
        {
            for (int i = 0; i < nrow; i++)
            {
                for (int j = 0; j < ncol; j++) A[i, j] = 0.0d;
                A[i, i] = 1.0d;
            }

            if (gooddata)
            {
                BuildInitialShape();
            }
        }// end of setIdentity

        private void Transformer_Load(object sender, System.EventArgs e)
        {

        }

        /**
 *  Take the intial set of vertices and present the shape in the middle of
 *  the screen, right side up, and scaled so that its height is half the
 *  screen height
 */
        void BuildInitialShape()
        {
            // width and height of initial shape
            const int initialSize = 20;

            // get midpoints for frame and shape
            double midpointX = this.ClientSize.Width / 2.0 - toolBar1Width / 2.0;
            double midpointY = this.ClientSize.Height / 2.0;
            double shapeX = vertices[0, 0];
            double shapeY = vertices[0, 1];
            double shapeZ = vertices[0, 2];

            // translate midpoint of shape to (0,0,0)
            ctrans = translate(ctrans, -shapeX, -shapeY, -shapeZ);

            // get distance from the centre of the shape to the baseline (y = 0)
            centreToBaseline = vertices[0, 1];

            // reflect shape about the x-axis
            ctrans = reflect(ctrans, 'x');

            // scale shape to be half height of frame
            double scalor = midpointY / initialSize;
            ctrans = scale(ctrans, scalor);

            // translate shape to middle of frame
            ctrans = translate(ctrans, midpointX, midpointY, shapeZ);
        }

        private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
        {
            if (e.Button == transleftbtn)
            {
                timer.Dispose();
                ctrans = translate(ctrans, -50, 0, 0);
                Refresh();
            }
            if (e.Button == transrightbtn)
            {
                timer.Dispose();
                ctrans = translate(ctrans, 50, 0, 0);
                Refresh();
            }
            if (e.Button == transupbtn)
            {
                timer.Dispose();
                ctrans = translate(ctrans, 0, -25, 0);
                Refresh();
            }

            if (e.Button == transdownbtn)
            {
                timer.Dispose();
                ctrans = translate(ctrans, 0, 25, 0);
                Refresh();
            }
            if (e.Button == scaleupbtn)
            {
                timer.Dispose();
                ctrans = translate(ctrans, -scrnpts[0, 0], -scrnpts[0, 1], -scrnpts[0,2]);
                ctrans = scale(ctrans, 1.1);
                ctrans = translate(ctrans, scrnpts[0, 0], scrnpts[0, 1], scrnpts[0,2]);
                Refresh();
            }
            if (e.Button == scaledownbtn)
            {
                timer.Dispose();
                ctrans = translate(ctrans, -scrnpts[0, 0], -scrnpts[0, 1], -scrnpts[0,2]);
                ctrans = scale(ctrans, 0.9);
                ctrans = translate(ctrans, scrnpts[0, 0], scrnpts[0, 1], scrnpts[0,2]);
                Refresh();
            }
            if (e.Button == rotxby1btn)
            {
                timer.Dispose();
                rotateInX(sender, e);
            }
            if (e.Button == rotyby1btn)
            {
                timer.Dispose();
                rotateInY(sender, e);
            }
            if (e.Button == rotzby1btn)
            {
                timer.Dispose();
                rotateInZ(sender, e);
            }
            if (e.Button == rotxbtn)
            {
                timer.Dispose();
                timer.Interval = 30;       // time in milliseconds
                timer.Tick += rotateInX;
                timer.Start();
            }
            if (e.Button == rotybtn)
            {
                timer.Dispose();
                timer.Interval = 30;       // time in milliseconds
                timer.Tick += rotateInY;
                timer.Start();
            }

            if (e.Button == rotzbtn)
            {
                timer.Dispose();
                timer.Interval = 30;       // time in milliseconds
                timer.Tick += rotateInZ;
                timer.Start();
            }
            if (e.Button == shearleftbtn)
            {
                timer.Dispose();
                double Ydistance = centreToBaseline;

                ctrans = translate(ctrans, 0, -Ydistance, 0);
                ctrans = shear(ctrans, 'l');
                ctrans = translate(ctrans, 0, Ydistance, 0);
                Refresh();
            }
            if (e.Button == shearrightbtn)
            {
                timer.Dispose();
                double Ydistance = centreToBaseline;
                
                ctrans = translate(ctrans, 0, -Ydistance, 0);
                ctrans = shear(ctrans, 'r');
                ctrans = translate(ctrans, 0, Ydistance, 0);
                Refresh();
            }
            if (e.Button == resetbtn)
            {
                timer.Dispose();
                RestoreInitialImage();
            }
            if (e.Button == exitbtn)
            {
                timer.Dispose();
                Close();
            }
        }

        #region rotationHelpers
        /**
         *  Helper methods used in single and continuous rotations
         */
        void rotateInX(object sender, EventArgs e)
        {
            ctrans = translate(ctrans, -scrnpts[0, 0], -scrnpts[0, 1], -scrnpts[0, 2]);
            ctrans = rotate(ctrans, 'x');
            ctrans = translate(ctrans, scrnpts[0, 0], scrnpts[0, 1], scrnpts[0, 2]);
            Refresh();
        }
        void rotateInY(object sender, EventArgs e)
        {
            ctrans = translate(ctrans, -scrnpts[0, 0], -scrnpts[0, 1], -scrnpts[0, 2]);
            ctrans = rotate(ctrans, 'y');
            ctrans = translate(ctrans, scrnpts[0, 0], scrnpts[0, 1], scrnpts[0, 2]);
            Refresh();
        }
        void rotateInZ(object sender, EventArgs e)
        {
            ctrans = translate(ctrans, -scrnpts[0, 0], -scrnpts[0, 1], -scrnpts[0, 2]);
            ctrans = rotate(ctrans, 'z');
            ctrans = translate(ctrans, scrnpts[0, 0], scrnpts[0, 1], scrnpts[0, 2]);
            Refresh();
        }
        #endregion

        #region transformations
        /**
         *  Helper method for multiplying two 4x4 matrices.
         */
        double[,] multiply4x4(double[,] matrix1, double[,] matrix2)
        {
            double[,] tempMatrix = new double[4,4];

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    double temp = 0.0d;
                    for (int k = 0; k < 4; k++)
                        temp += matrix1[i, k] * matrix2[k, j];
                    tempMatrix[i, j] = temp;
                }
            }
            return tempMatrix;
        }

        /**
         *  Translation matrix
         */
        double[,] translate(double[,] ctrans, double x, double y, double z)
        {
            double[,] translationMatrix = new double[,] 
            {   { 1, 0, 0, 0 }, 
                { 0, 1, 0, 0 }, 
                { 0, 0, 1, 0 }, 
                { x, y, z, 1 }
            };

            centreToBaseline += y;
            return multiply4x4(ctrans, translationMatrix);
        }

        /**
         *  Reflection matrix
         */
        double[,] reflect(double[,] ctrans, char axis = 'x')
        {

            double x =  1;
            double y = -1;
            
            double[,] reflectionMatrix = new double[,]
            {   { x, 0, 0, 0 }, 
                { 0, y, 0, 0 }, 
                { 0, 0, 1, 0 }, 
                { 0, 0, 0, 1 }
            };

            if (axis == 'y') {
                x = -1;
                y =  1;
            }

            return multiply4x4(ctrans, reflectionMatrix);
        }

        /**
         *  Scaling matrix
         */
        double[,] scale(double[,] ctrans, double s)
        {
            double[,] scalorMatrix = new double[,]
            {   { s, 0, 0, 0 }, 
                { 0, s, 0, 0 }, 
                { 0, 0, s, 0 }, 
                { 0, 0, 0, 1 }
            };

            centreToBaseline = centreToBaseline * s;
            return multiply4x4(ctrans, scalorMatrix);
        }

        /**
         *  Rotation matrix
         */
        double[,] rotate(double[,] ctrans, char axis = 'x')
        {
            // rotation angle in radians
            double θ = 0.05;
            // trig values
            double cosθ = Math.Cos(θ);
            double sinθ = Math.Sin(θ);

            double[,] rotationMatrix = new double[,]
            {   { 1,    0,     0,  0 }, 
                { 0, cosθ, -sinθ,  0 }, 
                { 0, sinθ,  cosθ,  0 },
                { 0,    0,     0,  1 }
            };

            if (axis == 'y')
            {
                rotationMatrix = new double[,]
                {   {cosθ, 0, sinθ,  0 }, 
                    {    0, 1,    0,  0 }, 
                    {-sinθ, 0, cosθ,  0 }, 
                    {    0, 0,    0,  1 }
                };
            }

            if (axis == 'z')
            {
                rotationMatrix = new double[,]
                {   { cosθ, sinθ,   0,  0 }, 
                    {-sinθ, cosθ,   0,  0 }, 
                    {    0,    0,   1,  0 }, 
                    {    0,    0,   0,  1 }
                };
            }

            return multiply4x4(ctrans, rotationMatrix);
        }

        /**
         *  Shearing matrix
         */
        double[,] shear(double[,] ctrans, char dir = 'r')
        {
            double r = -0.1;

            if (dir == 'l')
                r = 0.1;

            double[,] shearingMatrix = new double[,]
            {   { 1, 0, 0, 0 }, 
                { r, 1, 0, 0 }, 
                { 0, 0, 1, 0 }, 
                { 0, 0, 0, 1 }
            };

            return multiply4x4(ctrans, shearingMatrix);
        }
        #endregion

    }
}
