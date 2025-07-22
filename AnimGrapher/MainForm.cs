using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Text;
//using System.Timers;
using System.Windows.Forms;
//using System.Windows.Threading;


namespace AnimGrapher
{
    public partial class MainForm : Form
    {
        private readonly float dpiX_;
        private readonly float dpiY_;

        // for debug purposes only
        #if DEBUG
            private int xExport_ = 0;
            private int yExport_ = 0;
            private int wExport_ = 0;
            private int hExport_ = 0;
        #endif

        Graphics gGraph_;
        Bitmap bitmapGraph_;
        int wBaseBox_;
        int hBaseBox_;
        bool hasResized_;
        bool hasPaused_;
        bool hasCleared_;
        bool isDrawing_;

        // draw parameters

        private readonly View view_;

        // used for parametric, polar, cartesian equations
        private double p_;
        private double pStep_ = 0.05;
        private double pMin_ = 0;
        private double pMax_ = 2 * Math.PI;

        // used for equality, inequality equations
        private double px_;
        private double py_;

        private DrawType drawType_ = DrawType.LINE;
        private float penWidth_ = 3;

        private Color drawColor_ = Color.Black;
        private Color backColor_ = Color.FromArgb(252, 252, 254);

        readonly SolidBrush drawBrush_;
        readonly Pen drawPen_;

        private Point prevPoint_;
        private bool hasInitPoint_;
        private bool lastPoint_;

        private readonly bool showCoordsAtDraw_ = true;

        private bool isUpdatingOtherNumericUpDown_ = false;

        private bool mouseCanMoveGraph_ = true;
        private bool mouseButtonOnGraphDown_ = false;
        private double mouseButtonOnGraphX_;
        private double mouseButtonOnGraphY_;
        private int mouseButtonOnGraphXScreen_;
        private int mouseButtonOnGraphYScreen_;

        // timer objects

        // archaic, slow, but stable
        readonly System.Windows.Forms.Timer timer_;
        private readonly int timerInterval_ = 1; // ms

        // recursion depth error
        //private System.Timers.Timer timer_;
        //private int timerInterval_ = 1; // ms

        // not 100% reliable
        //DispatcherTimer timer_;
        //private double timerInterval_ = 0.1; // ticks

        // not 100% stable
        //HighResolutionTimer timer_;
        //private float timerInterval_ = 0.1f;

        // random issue with CallbackOnCollectedDelegate with GC
        //private MultimediaTimer timer_;
        //private int timerInterval_ = 100;

        // expression objects
        private string[] variables_;
        private string expr1_ = "";
        private string expr2_ = "";       

        // saved equations
        private readonly string pathEquations_ = "AnimGrapherEquations.txt";
        private readonly List<CurveData> savedEquations_;

        // curve type
        private CurveType curveType_ = CurveType.PARAMETRIC;

        public MainForm()
        {
            view_ = new View();

            Graphics graphics = this.CreateGraphics();
            dpiX_ = graphics.DpiX;
            dpiY_ = graphics.DpiY;

            //#if DEBUG
            //    MessageBox.Show("DPI: X = " + dpiX_ + ", Y = " + dpiY_);
            //#endif

            ExpressionTools.Init();

            // for test purposes only
            //string[] variables = new string[] { "x", "y" };
            //double[] values = new double[] { 2, 3 };
            //string expr = "x*y";
            //bool exprValid = ExpressionTools.IsExpressionValid(expr, variables);
            //double res = ExpressionTools.Evaluate(expr, variables, values);

            drawBrush_ = new SolidBrush(drawColor_);
            drawPen_ = new Pen(drawColor_);

            // setup timer
            timer_ = new System.Windows.Forms.Timer
            {
                Interval = timerInterval_
            };
            timer_.Tick += timer__Tick;

            //timer_ = new System.Timers.Timer();
            //timer_.Interval = timerview_.Interval;
            //timer_.Elapsed += Timer__Elapsed;

            //timer_ = new DispatcherTimer();
            //timer_.Interval = TimeSpan.FromTicks((long)timerview_.Interval);
            //timer_.Interval = TimeSpan.FromMilliseconds(timerview_.Interval);
            //timer_.Tick += timer__Tick;

            //timer_ = new HighResolutionTimer(timerview_.Interval);
            //timer_.UseHighPriorityThread = true;
            //timer_.Elapsed += timer__Tick;//Timer__Elapsed;

            //timer_ = new MultimediaTimer() { Interval = timerview_.Interval };
            //timer_.Elapsed += timer__Tick;

            savedEquations_ = new List<CurveData>();

            InitializeComponent();

            wBaseBox_ = pictureboxGraph.Width;
            hBaseBox_ = pictureboxGraph.Height;

            pictureboxPencil.Parent = pictureboxGraph;
            panelGraph.BackColor = backColor_;
            hasResized_ = false;
            hasPaused_ = false;
            hasCleared_ = true;
            isDrawing_ = false;

            // not functional: prevent flicker
            //this.DoubleBuffered = true;
            //typeof(panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty
            //| BindingFlags.Instance | BindingFlags.NonPublic, null,
            //panel, new object[] { true });
            //SetStyle(ControlStyles.OptimizedDoubleBuffer |
            //        ControlStyles.AllPaintingInWmPaint |
            //        ControlStyles.UserPaint, true);

            variables_ = new string[] { };

            comboboxCurveType.SelectedIndex = 0;
            comboboxDrawType.SelectedIndex = 0;

            // update view

            //view_.Isometric = false;
            if (view_.Isometric)
                view_.UpdateGivenY(wBaseBox_, hBaseBox_);

            updateViewDisplay();

            this.Refresh();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.SuspendLayout();

            // load saved equations if file exists
            if (File.Exists(pathEquations_))
            {
                readEquationsFile(pathEquations_);

                if (savedEquations_.Count > 0)
                {
                    updateComboboxCurves();
                    comboboxCurves.SelectedIndex = 0;
                }
            }

            comboboxHints.SelectedIndex = 0; // default: none

            updateViewDisplay();
            clearGraph();
            updateGUI(true /* curveTypeChanged */);

            this.ResumeLayout();

            // focus on 1st equation
            this.ActiveControl = textbox1Eq;
            textbox1Eq.Focus();
        }

        private void updateComboboxCurves()
        {
            comboboxCurves.Items.Clear();

            comboboxCurves.DisplayMember = "Name";
            comboboxCurves.ValueMember = "Equation";

            // first item empty
            CurveData cdEmpty = new CurveData("<New>", curveType_, view_.Isometric);
            comboboxCurves.Items.Add(cdEmpty);

            // fill combobox
            foreach (CurveData cd in savedEquations_)
            {
                if (cd.Type == curveType_)
                    comboboxCurves.Items.Add(cd);
            }
        }

        #region Draw functions

        private void drawAxis()
        {
            Tuple<double, double> coords = convertXYToScreenCoords(0, 0);
            int xCenter = (int)(coords.Item1 + 0.5);
            int yCenter = (int)(coords.Item2 + 0.5);
            
            SolidBrush myBrush = new SolidBrush(Color.LightGray);
            Pen myPen = new Pen(myBrush);

            // draw axis
            gGraph_.DrawLine(myPen, new Point(xCenter, 0), new Point(xCenter, hBaseBox_));
            gGraph_.DrawLine(myPen, new Point(0, yCenter), new Point(wBaseBox_, yCenter));

            // draw interval marks

            int markLength = 5;

            // x > 0
            for (double xMark = view_.Xunit; xMark <= view_.Xmax; xMark += view_.Xunit)
            {
                coords = convertXYToScreenCoords(xMark, 0);
                int xMarkScreen = (int)(coords.Item1 + 0.5);
                int yMarkScreen = (int)(coords.Item2 + 0.5);

                gGraph_.DrawLine(myPen, new Point(xMarkScreen, yMarkScreen - markLength), new Point(xMarkScreen, yMarkScreen + markLength));
            }

            // x < 0
            for (double xMark = -view_.Xunit; xMark >= view_.Xmin; xMark -= view_.Xunit)
            {
                coords = convertXYToScreenCoords(xMark, 0);
                int xMarkScreen = (int)(coords.Item1 + 0.5);
                int yMarkScreen = (int)(coords.Item2 + 0.5);

                gGraph_.DrawLine(myPen, new Point(xMarkScreen, yMarkScreen - markLength), new Point(xMarkScreen, yMarkScreen + markLength));
            }

            // y > 0
            for (double yMark = view_.Xunit; yMark <= view_.Ymax; yMark += view_.Xunit)
            {
                coords = convertXYToScreenCoords(0, yMark);
                int xMarkScreen = (int)(coords.Item1 + 0.5);
                int yMarkScreen = (int)(coords.Item2 + 0.5);

                gGraph_.DrawLine(myPen, new Point(xMarkScreen - markLength, yMarkScreen), new Point(xMarkScreen + markLength, yMarkScreen));
            }

            // y < 0
            for (double yMark = -view_.Xunit; yMark >= view_.Ymin; yMark -= view_.Xunit)
            {
                coords = convertXYToScreenCoords(0, yMark);
                int xMarkScreen = (int)(coords.Item1 + 0.5);
                int yMarkScreen = (int)(coords.Item2 + 0.5);

                gGraph_.DrawLine(myPen, new Point(xMarkScreen - markLength, yMarkScreen), new Point(xMarkScreen + markLength, yMarkScreen));
            }

            // update image
            pictureboxGraph.Image = bitmapGraph_;
        }

        private void drawGrid()
        {
            Tuple<double, double> coords = convertXYToScreenCoords(0, 0);
            int xCenter = (int)(coords.Item1 + 0.5);
            int yCenter = (int)(coords.Item2 + 0.5);

            // draw grid
            SolidBrush gridBrush = new SolidBrush(Color.Gainsboro);
            Pen gridPen = new Pen(gridBrush);

            // x > 0
            for (double xMark = view_.Xunit; xMark <= view_.Xmax; xMark += view_.Xunit)
            {
                coords = convertXYToScreenCoords(xMark, 0);
                int xMarkScreen = (int)(coords.Item1 + 0.5);
                //int yMarkScreen = (int)(coords.Item2 + 0.5);

                gGraph_.DrawLine(gridPen, new Point(xMarkScreen, 0), new Point(xMarkScreen, hBaseBox_));
            }

            // x < 0
            for (double xMark = -view_.Xunit; xMark >= view_.Xmin; xMark -= view_.Xunit)
            {
                coords = convertXYToScreenCoords(xMark, 0);
                int xMarkScreen = (int)(coords.Item1 + 0.5);
                //int yMarkScreen = (int)(coords.Item2 + 0.5);

                gGraph_.DrawLine(gridPen, new Point(xMarkScreen, 0), new Point(xMarkScreen, hBaseBox_));
            }

            // y > 0
            for (double yMark = view_.Yunit; yMark <= view_.Ymax; yMark += view_.Yunit)
            {
                coords = convertXYToScreenCoords(0, yMark);
                //int xMarkScreen = (int)(coords.Item1 + 0.5);
                int yMarkScreen = (int)(coords.Item2 + 0.5);

                gGraph_.DrawLine(gridPen, new Point(0, yMarkScreen), new Point(wBaseBox_, yMarkScreen));
            }

            // y < 0
            for (double yMark = -view_.Yunit; yMark >= view_.Ymin; yMark -= view_.Yunit)
            {
                coords = convertXYToScreenCoords(0, yMark);
                //int xMarkScreen = (int)(coords.Item1 + 0.5);
                int yMarkScreen = (int)(coords.Item2 + 0.5);

                gGraph_.DrawLine(gridPen, new Point(0, yMarkScreen), new Point(wBaseBox_, yMarkScreen));
            }

            // draw axis
            SolidBrush axisBrush = new SolidBrush(Color.Silver);
            Pen axisPen = new Pen(axisBrush);
            gGraph_.DrawLine(axisPen, new Point(xCenter, 0), new Point(xCenter, hBaseBox_));
            gGraph_.DrawLine(axisPen, new Point(0, yCenter), new Point(wBaseBox_, yCenter));

            // update image
            pictureboxGraph.Image = bitmapGraph_;
        }

        private void drawInitPoint(double x, double y)
        {
            Tuple<double, double> coords = convertXYToScreenCoords(x, y);
            int xBox = (int)(coords.Item1 + 0.5);
            int yBox = (int)(coords.Item2 + 0.5);

            // nop if point is inside picture box
            if (xBox < 0 - penWidth_ || xBox > wBaseBox_ + penWidth_
             || yBox < 0 - penWidth_ || yBox > hBaseBox_ + penWidth_)
                return;

            Point currPoint = new Point(xBox, yBox);

            // draw current point as disc
            gGraph_.FillEllipse(drawBrush_,
                currPoint.X - penWidth_ / 2, currPoint.Y - penWidth_ / 2,
                penWidth_, penWidth_
            );

            prevPoint_ = currPoint;
            hasInitPoint_ = true;

            // update pencil position
            int xPencil = currPoint.X - pictureboxPencil.Width / 2;
            int yPencil = currPoint.Y - pictureboxPencil.Height;
            pictureboxPencil.Invoke(new MethodInvoker(delegate
            {
                pictureboxPencil.Location = new Point(xPencil, yPencil);
                pictureboxPencil.Visible = true;
            }));

            // update image
            pictureboxGraph.Image = bitmapGraph_;
        }

        private void drawNextPoint(double x, double y, bool showPencil = false)
        {
            Tuple<double, double> coords = convertXYToScreenCoords(x, y);
            int xBox = (int)(coords.Item1 + 0.5);
            int yBox = (int)(coords.Item2 + 0.5);

            // nop if point is inside picture box
            if (xBox < -1000 || xBox > wBaseBox_ + 1000
             || yBox < -1000 || yBox > hBaseBox_ + 1000)
            {
                // hide pencil?
                return;
            }

            Point currPoint = new Point(xBox, yBox);

            switch (drawType_)
            {
                case DrawType.LINE:
                    if (hasInitPoint_)
                    {
                        if (penWidth_ > 2)
                        {
                            // draw current point as disc
                            gGraph_.FillEllipse(drawBrush_,
                                currPoint.X - penWidth_ / 2, currPoint.Y - penWidth_ / 2,
                                penWidth_, penWidth_
                            );
                        }
                        gGraph_.DrawLine(drawPen_, prevPoint_, currPoint);
                    }
                    break;

                case DrawType.DOT:
                    // draw current point as disc
                    gGraph_.FillEllipse(drawBrush_,
                        currPoint.X - penWidth_ / 2, currPoint.Y - penWidth_ / 2,
                        penWidth_, penWidth_
                    );
                    break;

                case DrawType.SQUARE:
                    // draw current point as square
                    gGraph_.FillRectangle(drawBrush_,
                        currPoint.X - penWidth_ / 2, currPoint.Y - penWidth_ / 2,
                        penWidth_, penWidth_
                    );
                    break;
            }

            // update pencil position if option enabled
            if (showPencil)
            {
                int xPencil = currPoint.X - pictureboxPencil.Width / 2;
                int yPencil = currPoint.Y - pictureboxPencil.Height;
                pictureboxPencil.Invoke(new MethodInvoker(delegate
                {
                    pictureboxPencil.Location = new Point(xPencil, yPencil);
                }));
            }

            prevPoint_ = currPoint;
            hasInitPoint_ = true;

            // update image
            pictureboxGraph.Image = bitmapGraph_;
        }

        private void timer__Tick(object sender, EventArgs e)
        {
            int speedFactor = 1;
            for (int i = 0; i < speedFactor; i++)
            {
                //DateTime startDate = DateTime.Now;

                double x = 0;
                double y = 0;
                double r = 0;
                bool canDraw = true;

                // equality / inequality specific
                if (curveType_ == CurveType.EQUALITY
                 || curveType_ == CurveType.INEQUALITY)
                {

                    try
                    {
                        double[] values = new double[] { px_, py_};
                        r = ExpressionTools.Evaluate(expr1_, variables_, values);

                        if (curveType_ == CurveType.EQUALITY)
                            canDraw = (Math.Abs(r) < 0.001); // not precise enough
                        else if (curveType_ == CurveType.INEQUALITY)
                            canDraw = (r > 0);
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message == "undefined")
                        {
                            if (showCoordsAtDraw_)
                            {
                                labelCoords.Text = Environment.NewLine;
                                labelCoords.Text += "θ=" + p_.ToString("0.0000") + Environment.NewLine;
                                labelCoords.Text += "r=<undef>";
                            }

                            p_ += pStep_;
                            return;
                        }
                        else
                        {
                            stopDraw();
                            string msg = "Error in expression r for t = " + p_.ToString(CultureInfo.GetCultureInfo("en-GB")) + ":" + Environment.NewLine;
                            msg += Environment.NewLine;
                            msg += ex.Message;
                            MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    if (Double.IsNaN(r) || Double.IsNegativeInfinity(r) || Double.IsPositiveInfinity(r))
                    {
                        px_ += pStep_;
                        return;
                    }
                }

                    // polar curve specific: calculate r
                if (curveType_ == CurveType.POLAR)
                {
                    try
                    {
                        r = ExpressionTools.Evaluate(expr1_, "t", p_);
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message == "undefined")
                        {
                            if (showCoordsAtDraw_)
                            {
                                labelCoords.Text = Environment.NewLine;
                                labelCoords.Text += "<undef>";
                            }

                            p_ += pStep_;
                            return;
                        }
                        else
                        {
                            stopDraw();
                            string msg = "Error in expression for x,y = " + px_.ToString(CultureInfo.GetCultureInfo("en-GB")) + "," + py_.ToString(CultureInfo.GetCultureInfo("en-GB")) + ":" + Environment.NewLine;
                            msg += Environment.NewLine;
                            msg += ex.Message;
                            MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    if (Double.IsNaN(r) || Double.IsNegativeInfinity(r) || Double.IsPositiveInfinity(r))
                    {
                        p_ += pStep_;
                        return;
                    }
                }

                // x
                try
                {
                    switch (curveType_)
                    {
                        case CurveType.PARAMETRIC:
                            x = ExpressionTools.Evaluate(expr1_, "t", p_);
                            break;

                        case CurveType.POLAR:
                            x = r * Math.Cos(p_);
                            break;

                        case CurveType.CARTESIAN:
                            x = p_;
                            break;

                        case CurveType.EQUALITY:
                        case CurveType.INEQUALITY:
                            x = px_;
                            break;
                    }
                }
                catch (Exception ex)
                {
                    if (ex.Message == "undefined")
                    {
                        if (showCoordsAtDraw_)
                        {
                            labelCoords.Text = "t=" + p_.ToString("0.0000") + Environment.NewLine;
                            labelCoords.Text += "x=<undef>";
                        }

                        p_ += pStep_;
                        return;
                    }
                    else
                    {
                        stopDraw();
                        string msg = "Error in expression x for t = " + p_.ToString(CultureInfo.GetCultureInfo("en-GB")) + ":" + Environment.NewLine;
                        msg += Environment.NewLine;
                        msg += ex.Message;
                        MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                if (Double.IsNaN(x) || Double.IsNegativeInfinity(x) || Double.IsPositiveInfinity(x))
                {
                    p_ += pStep_;
                    return;
                }

                // y
                try
                {
                    switch (curveType_)
                    {
                        case CurveType.PARAMETRIC:
                            y = ExpressionTools.Evaluate(expr2_, "t", p_);
                            break;

                        case CurveType.POLAR:
                            y = r * Math.Sin(p_);
                            break;

                        case CurveType.CARTESIAN:
                            y = ExpressionTools.Evaluate(expr1_, "x", p_);
                            break;

                        case CurveType.EQUALITY:
                        case CurveType.INEQUALITY:
                            y = py_;
                            break;
                    }
                }
                catch (Exception ex)
                {
                    if (ex.Message == "undefined")
                    {
                        if (showCoordsAtDraw_)
                        {
                            if (curveType_ == CurveType.PARAMETRIC)
                                labelCoords.Text = "t=" + p_.ToString("0.0000") + Environment.NewLine;
                            else
                                labelCoords.Text = Environment.NewLine;

                            labelCoords.Text += "x=" + x.ToString("0.0000") + Environment.NewLine;
                            labelCoords.Text += "y=<undef>";
                        }

                        p_ += pStep_;
                        return;
                    }
                    else
                    {
                        stopDraw();

                        string parameter = "t";
                        if (curveType_ == CurveType.CARTESIAN)
                            parameter = "x";

                        string msg = "Error in expression y for " + parameter + " = " + p_.ToString(CultureInfo.GetCultureInfo("en-GB")) + ":" + Environment.NewLine;
                        msg += Environment.NewLine;
                        msg += ex.Message;
                        MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                if (Double.IsNaN(y) || Double.IsNegativeInfinity(y) || Double.IsPositiveInfinity(y))
                {
                    p_ += pStep_;
                    return;
                }

                // draw point
                if (canDraw)
                {
                    // show pencil?
                    bool showPencil = (i == speedFactor - 1);

                    // draw point
                    if (!hasInitPoint_)
                        drawInitPoint(x, y);
                    else
                        drawNextPoint(x, y, showPencil);
                }

                // display coordinates
                if (showCoordsAtDraw_)
                {
                    switch (curveType_)
                    {
                        case CurveType.PARAMETRIC:
                            labelCoords.Text = "t=" + p_.ToString("0.0000") + Environment.NewLine;
                            labelCoords.Text += "x=" + x.ToString("0.0000") + Environment.NewLine;
                            labelCoords.Text += "y=" + y.ToString("0.0000");
                            break;

                        case CurveType.POLAR:
                            labelCoords.Text = Environment.NewLine;
                            labelCoords.Text += "θ=" + p_.ToString("0.0000") + Environment.NewLine;
                            labelCoords.Text += "r=" + r.ToString("0.0000");
                            break;

                        case CurveType.CARTESIAN:
                        case CurveType.EQUALITY:
                        case CurveType.INEQUALITY:
                            labelCoords.Text = Environment.NewLine;
                            labelCoords.Text += "x=" + x.ToString("0.0000") + Environment.NewLine;
                            labelCoords.Text += "y=" + y.ToString("0.0000");
                            break;
                    }
                }

                // stop if last point
                if (lastPoint_)
                {
                    stopDraw();
                    return;
                }

                // next step
                switch (curveType_)
                {
                    case CurveType.PARAMETRIC:
                    case CurveType.POLAR:
                    case CurveType.CARTESIAN:

                        // last point reached?
                        if (p_ + pStep_ > pMax_)
                        {
                            p_ = pMax_;
                            lastPoint_ = true;
                        }
                        else
                            p_ += pStep_;

                        break;

                    case CurveType.EQUALITY:
                    case CurveType.INEQUALITY:
                        // last point reached?
                        if (px_ + pStep_ > view_.Xmax
                         && py_ + pStep_ > view_.Ymax)
                        {
                            px_ = view_.Xmax;
                            py_ = view_.Ymax;
                            lastPoint_ = true;
                        }
                        else if (px_ + pStep_ > view_.Xmax)
                        {
                            // next line
                            px_ = view_.Xmin;
                            py_ += pStep_;
                        }
                        else
                            px_ += pStep_;
                        break;
                }

                //string drawTime = (DateTime.Now - startDate).TotalMilliseconds.ToString(); //.ToString(@"hh\:mm\:ss");
                //Console.WriteLine("Draw time:" + drawTime);
            }
        }

        private void initDraw()
        {
            // clear graph if resize has been made
            if (hasResized_)
            {
                hasResized_ = false;
                clearGraph();
            }

            // init draw parameters
            timer_.Stop();
            labelCoords.Invoke(new MethodInvoker(delegate { labelCoords.Visible = false; }));
            pictureboxPencil.Visible = false;
            hasInitPoint_ = false;
            lastPoint_ = false;
            updateDrawParams();

            p_ = pMin_;
            px_ = view_.Xmin;
            py_ = view_.Ymin;

            switch (curveType_)
            {
                case CurveType.PARAMETRIC:
                    expr1_ = textbox1Eq.Text;
                    expr2_ = textbox2Eq.Text;
                    break;

                case CurveType.POLAR:
                case CurveType.CARTESIAN:
                case CurveType.EQUALITY:
                case CurveType.INEQUALITY:
                    expr1_ = textbox1Eq.Text;
                    expr2_ = "";
                    break;
            }

            labelCoords.Text = "";
            labelCoords.Visible = true;
            //pictureboxPencil.Visible = true;

            // disable axis parameters and resizing
            enableChangeView(false);
            //this.FormBorderStyle = FormBorderStyle.FixedSingle;

            buttonClear.Enabled = true;
            buttonStop.Invoke(new MethodInvoker(delegate { buttonStop.Enabled = true; }));
            buttonExport.Enabled = false;
            hasCleared_ = false;
            isDrawing_ = true;
        }

        private void stopDraw()
        {
            timer_.Stop();
            hasPaused_ = false;
            isDrawing_ = false;
            //labelCoords.Visible = false;
            labelCoords.Invoke(new MethodInvoker(delegate
            {
                labelCoords.Visible = false;
            }));
            //pictureboxPencil.Visible = false;
            pictureboxPencil.Invoke(new MethodInvoker(delegate { pictureboxPencil.Visible = false; }));
            //buttonStop.Enabled = false;
            buttonStop.Invoke(new MethodInvoker(delegate { buttonStop.Enabled = false; }));
            updatePausePlayGUI(true);

            //enableAxisParameters(true);
            //this.FormBorderStyle = FormBorderStyle.Sizable;
            //this.Invoke(new MethodInvoker(delegate { this.MaximizeBox = true; }));
            updateGUI();
        }

        #endregion

        #region Buttons callbacks

        private void buttonSave_Click(object sender, EventArgs e)
        {
            // open save name dialog
            CurveData curCD = comboboxCurves.SelectedItem as CurveData;
            string name = (curCD.Name == "<New>") ? "Curve 1" : curCD.Name;

            NameEquationDialog dialog = new NameEquationDialog(name);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                name = dialog.GetName();
            
                // build parametric curve data

                CurveData curveData = new CurveData(name, curveType_, view_.Isometric);

                switch (curveType_)
                {
                    case CurveType.PARAMETRIC:
                        curveData.Equation1 = textbox1Eq.Text;
                        curveData.Equation2 = textbox2Eq.Text;
                        break;

                    case CurveType.POLAR:
                    case CurveType.CARTESIAN:
                    case CurveType.EQUALITY:
                    case CurveType.INEQUALITY:
                        curveData.Equation1 = textbox1Eq.Text;
                        curveData.Equation2 = "";
                        break;
                }

                curveData.XVmin = (double)numericupdownXMin.Value;
                curveData.XVmax = (double)numericupdownXMax.Value;
                curveData.YVmin = (double)numericupdownYMin.Value;
                curveData.YVmax = (double)numericupdownYMax.Value;
                curveData.Isometric = view_.Isometric;

                curveData.Pmin = (double)numericupdownPMin.Value;
                curveData.Pmax = (double)numericupdownPMax.Value;
                curveData.Pstep = (double)numericupdownPStep.Value;

                curveData.Thickness = (double)numericupdownThickness.Value;

                // search if name already exists
                bool exists = false;
                int index = -1;
                foreach (CurveData cd in savedEquations_)
                {
                    index++;
                    if (cd.Name == name) // found
                    {
                        exists = true;
                        break;
                    }
                }

                int indexCombobox;
                if (exists && index >= 0 && index < savedEquations_.Count)
                {
                    string msg = "Equation \"" + name + "\" already exists." + Environment.NewLine;
                    msg += Environment.NewLine;
                    msg += "Do you want to replace it?";
                    DialogResult result = MessageBox.Show(msg, "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (result == DialogResult.Yes)
                    {
                        savedEquations_[index] = curveData;
                        indexCombobox = comboboxCurves.SelectedIndex; // index + 1;
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    savedEquations_.Add(curveData);
                    indexCombobox = comboboxCurves.Items.Count; // new last index
                }

                // update equations file and combo box
                saveEquationsToFile(pathEquations_);
                updateComboboxCurves();
                try
                {
                    comboboxCurves.SelectedIndex = indexCombobox;
                }
                catch (Exception)
                {
                    //
                }

                MessageBox.Show("Equation \"" + name + "\" has been sucessfully saved to file " + Path.GetFullPath(pathEquations_) + ".", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);     
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            CurveData curCD = comboboxCurves.SelectedItem as CurveData;
            string name = curCD.Name;

            string msg = "Are you sure to delete equation \"" + name + "\"?";
            DialogResult result = MessageBox.Show(msg, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result == DialogResult.Yes)
            {
                savedEquations_.Remove(curCD);

                // update equations file and combo box
                saveEquationsToFile(pathEquations_);
                updateComboboxCurves();
                comboboxCurves.SelectedIndex = 0;

                MessageBox.Show("Equation \"" + name + "\" has been sucessfully deleted.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);     
            }
        }

        private void buttonNewEquation_Click(object sender, EventArgs e)
        {
            CurveData curveData = comboboxCurves.SelectedItem as CurveData;
            bool wasNewEquation = (curveData.Name == "<New>");

            comboboxCurves.SelectedIndex = 0;

            // force event
            if (wasNewEquation)
                comboboxCurves_SelectedIndexChanged(null/*dummy*/, null/*dummy*/);
        }

        private void buttonDraw_Click(object sender, EventArgs e)
        {
            initDraw();
            hasPaused_ = false;
            updatePausePlayGUI(false);

            timer_.Start();

            //DateTime startDate = DateTime.Now;
            //int nbTicks = (int)((tMax_ - tMin_) / tStep_) + 1;
            //for (int i = 0; i < nbTicks; i++)
            //    timer__Tick(null, null);
            //string drawTime = (DateTime.Now - startDate).TotalMilliseconds.ToString(); //.ToString(@"hh\:mm\:ss");
            //Console.WriteLine("Draw time:" + drawTime);
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            stopDraw();
            clearGraph();

            enableChangeView(true);
            buttonClear.Enabled = false;
            buttonExport.Enabled = false;

            hasCleared_ = true;
        }

        private void buttonPausePlay_Click(object sender, EventArgs e)
        {
            if (!isDrawing_) // start drawing
            {
                initDraw();
                updatePausePlayGUI(false);
                timer_.Start();
                return;
            }

            // switch pause/play
            if (!hasPaused_)
            {
                updatePausePlayGUI(true);
                timer_.Stop();
            }
            else
            {
                updatePausePlayGUI(false);
                timer_.Start();
            }
        }

        private void updatePausePlayGUI(bool status)       // true for play, false for pause
        {
            if (status)
            {
                // pause activated, display play button
                buttonPausePlay.Invoke(new MethodInvoker(delegate {
                    tooltip_.SetToolTip(buttonPausePlay, "Continue draw");
                    buttonPausePlay.Image = Properties.Resources.play_32;
                }));
                buttonExport.Invoke(new MethodInvoker(delegate { buttonExport.Enabled = true; }));
                hasPaused_ = true;
            }
            else
            {
                // play activated, display pause button
                buttonPausePlay.Invoke(new MethodInvoker(delegate {
                    tooltip_.SetToolTip(buttonPausePlay, "Pause draw");
                    buttonPausePlay.Image = Properties.Resources.pause_32;
                }));
                buttonExport.Invoke(new MethodInvoker(delegate { buttonExport.Enabled = false; }));
                hasPaused_ = false;
            }
        }

        private void buttonNextStep_Click(object sender, EventArgs e)
        {
            if (!isDrawing_) // init draw if not drawing
            {
                initDraw();
                hasPaused_ = true;
            }

            // force pause if not already paused
            if (!hasPaused_)
            {
                updatePausePlayGUI(true);
                timer_.Stop();
                hasPaused_ = true;
                return;
            }

            // draw next tick
            timer__Tick(null/*dummy*/, null/*dummy*/);
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            stopDraw();
        }

        private void buttonCopy1Eq_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textbox1Eq.Text);
            textbox1Eq.Focus();
            textbox1Eq.SelectAll();
        }

        private void buttonCopy2Eq_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textbox2Eq.Text);
            textbox2Eq.Focus();
            textbox2Eq.SelectAll();
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            // capture graph into bitmap
            int offsetX = 8;
            int offsetY = 31;
            Bitmap bmp;
            Rectangle boundsWindow = this.Bounds;
            Rectangle boundsPanel = panelGraph.Bounds;
            boundsPanel.Offset(boundsWindow.Left + offsetX, boundsWindow.Top + offsetY);

            Rectangle boundsGraph = pictureboxGraph.Bounds;
            boundsGraph.Offset(boundsPanel.Left, boundsPanel.Top);

            #if DEBUG
                string msg = "Offset = " + offsetX + ", " + offsetY + Environment.NewLine + Environment.NewLine;
                msg += "Bounds window = " + boundsWindow.Left + ", " + boundsWindow.Top + Environment.NewLine + Environment.NewLine;
                msg += "Bounds panel = " + boundsPanel.Left + ", " + boundsPanel.Top + Environment.NewLine + Environment.NewLine;
                msg += "Bounds graph = " + boundsGraph.Left + ", " + boundsGraph.Top + ", " + boundsGraph.Width + ", " + boundsGraph.Height;
                MessageBox.Show(msg, "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                string pathExportConfig = "AnimGrapherExportConfig.txt";
                if (File.Exists(pathExportConfig))
                {
                    readExportConfigFile(pathExportConfig);

                    boundsGraph.Location = new Point(xExport_, yExport_);
                    boundsGraph.Size = new Size(wExport_, hExport_);

                    msg = "Export config file found. Using coordinates:" + Environment.NewLine;
                    msg += "Left = " + xExport_ + Environment.NewLine;
                    msg += "Top = " + yExport_ + Environment.NewLine;
                    msg += "Width = " + wExport_ + Environment.NewLine;
                    msg += "Height = " + hExport_ + Environment.NewLine;
                    MessageBox.Show(msg, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            #endif

            using (Bitmap bitmap = new Bitmap(boundsGraph.Width, boundsGraph.Height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(new Point(boundsGraph.Left, boundsGraph.Top), Point.Empty, boundsGraph.Size);
                }

                bmp = (Bitmap)bitmap.Clone();
            }

            CurveData curCD = comboboxCurves.SelectedItem as CurveData;
            string defaultName = (curCD.Name == "<New>") ? "Curve1" : curCD.Name;

            // save bitmap
            SaveFileDialog dialog = new SaveFileDialog
            {
                DefaultExt = ".jpg",
                AddExtension = true,
                Filter = "JPEG - Image files|*.jpg|BMP - Bitmap files|*.bmp",
                FileName = defaultName
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                switch (dialog.FilterIndex)
                {
                    case 1:
                        bmp.Save(dialog.FileName, ImageFormat.Jpeg);
                        break;

                    case 2:
                        bmp.Save(dialog.FileName, ImageFormat.Bmp);
                        break;
                }

                MessageBox.Show("Image has been sucessfully exported to " + dialog.FileName, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void buttonAbout_Click(object sender, EventArgs e)
        {
            AboutDialog dialog = new AboutDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                //
            }
        }

        private void buttonQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        private void clearGraph()
        {
            timer_.Stop();
            labelCoords.Visible = false;
            pictureboxPencil.Visible = false;

            // clear graph
            //if (bitmapGraph_ != null)
            //    bitmapGraph_.Dispose();
            gGraph_?.Clear(Color.Transparent);

            // initialize graphics

            bitmapGraph_ = new Bitmap(wBaseBox_, hBaseBox_, PixelFormat.Format32bppArgb);
            gGraph_ = Graphics.FromImage(bitmapGraph_);
            gGraph_.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            gGraph_.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            gGraph_.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            gGraph_.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            hasInitPoint_ = false;

            // set draw parameters
            updateDrawParams();
            updateHints();

            p_ = pMin_;
            px_ = view_.Xmin;
            py_ = view_.Ymin;
        }

        #region Draw parameters callbacks

        private void numericupdownPMin_ValueChanged(object sender, EventArgs e)
        {
            updateDrawParams();
        }

        private void numericupdownPMax_ValueChanged(object sender, EventArgs e)
        {
            updateDrawParams();
        }

        private void numericupdownPStep_ValueChanged(object sender, EventArgs e)
        {
            updateDrawParams();
        }

        private void numericupdownThickness_ValueChanged(object sender, EventArgs e)
        {
            updateDrawParams();
        }

        private void updateDrawParams()
        {
            // ensure coherency
            numericupdownPMin.Maximum = numericupdownPMax.Value - numericupdownPMin.Increment;
            numericupdownPMax.Minimum = numericupdownPMin.Value + numericupdownPMax.Increment;

            // update values
            pMin_ = (double)numericupdownPMin.Value;
            pMax_ = (double)numericupdownPMax.Value;
            pStep_ = (double)numericupdownPStep.Value;
            penWidth_ = (float)numericupdownThickness.Value;
            drawPen_.Width = penWidth_;
        }

        #endregion

        #region Axis parameters callbacks

        private void numericupdownXMin_ValueChanged(object sender, EventArgs e)
        {
            if (isUpdatingOtherNumericUpDown_)
                return;

            view_.Xmin = (double)numericupdownXMin.Value;

            if (view_.Isometric)
                view_.UpdateGivenX(wBaseBox_, hBaseBox_);

            isUpdatingOtherNumericUpDown_ = true;
            updateViewDisplay();
            clearGraph();
            isUpdatingOtherNumericUpDown_ = false;
        }

        private void numericupdownXMax_ValueChanged(object sender, EventArgs e)
        {
            if (isUpdatingOtherNumericUpDown_)
                return;

            if (isUpdatingOtherNumericUpDown_)
                return;

            view_.Xmax = (double)numericupdownXMax.Value;

            if (view_.Isometric)
                view_.UpdateGivenX(wBaseBox_, hBaseBox_);

            isUpdatingOtherNumericUpDown_ = true;
            updateViewDisplay();
            clearGraph();
            isUpdatingOtherNumericUpDown_ = false;
        }

        private void numericupdownYMin_ValueChanged(object sender, EventArgs e)
        {
            if (isUpdatingOtherNumericUpDown_)
                return;

            view_.Ymin = (double)numericupdownYMin.Value;

            if (view_.Isometric)
                view_.UpdateGivenY(wBaseBox_, hBaseBox_);

            isUpdatingOtherNumericUpDown_ = true;
            updateViewDisplay();
            clearGraph();
            isUpdatingOtherNumericUpDown_ = false;
        }

        private void numericupdownYMax_ValueChanged(object sender, EventArgs e)
        {
            if (isUpdatingOtherNumericUpDown_)
                return;

            view_.Ymax = (double)numericupdownYMax.Value;

            if (view_.Isometric)
                view_.UpdateGivenY(wBaseBox_, hBaseBox_);

            isUpdatingOtherNumericUpDown_ = true;
            updateViewDisplay();
            clearGraph();
            isUpdatingOtherNumericUpDown_ = false;
        }

        private void numericupdownUnit_ValueChanged(object sender, EventArgs e)
        {
            updateViewDisplay();
            clearGraph();
        }

        private void updateViewDisplay()
        {
            if (true)
            {
                // update interface
                numericupdownXMin.Value = (decimal)view_.Xmin;
                numericupdownXMax.Value = (decimal)view_.Xmax;
                numericupdownYMin.Value = (decimal)view_.Ymin;
                numericupdownYMax.Value = (decimal)view_.Ymax;
                
                // ensure coherency
                numericupdownXMin.Maximum = numericupdownXMax.Value - numericupdownXMin.Increment;
                numericupdownXMax.Minimum = numericupdownXMin.Value + numericupdownXMax.Increment;
                numericupdownYMin.Maximum = numericupdownYMax.Value - numericupdownYMin.Increment;
                numericupdownYMax.Minimum = numericupdownYMin.Value + numericupdownYMax.Increment;

                // update units
                view_.Xunit = (double)numericupdownUnit.Value;
                view_.Yunit = (double)numericupdownUnit.Value;

                return;
            }

            //// ensure coherency
            //numericupdownXMin.Maximum = numericupdownXMax.Value - numericupdownXMin.Increment;
            //numericupdownXMax.Minimum = numericupdownXMin.Value + numericupdownXMax.Increment;
            //numericupdownYMin.Maximum = numericupdownYMax.Value - numericupdownYMin.Increment;
            //numericupdownYMax.Minimum = numericupdownYMin.Value + numericupdownYMax.Increment;

            //// update values
            //view_.Xmin = (double)numericupdownXMin.Value;
            //view_.Xmax = (double)numericupdownXMax.Value;
            //view_.Ymin = (double)numericupdownYMin.Value;
            //view_.Ymax = (double)numericupdownYMax.Value;

            //view_.Xunit = (double)numericupdownUnit.Value;
            ////view_.Yunit = (double)numericupdownInterval.Value;

            //// resize picture box

            //// Ratio width/height
            //float ratioXY = (float)(view_.Xmax - view_.Xmin) / (float)(view_.Ymax - view_.Ymin);

            //// Margin
            //int wMargin = 0;
            //int hMargin = 0;

            //// Compute picture box dimensions
            //hBaseBox_ = Math.Abs(panelGraph.Height - 2 * hMargin);
            //wBaseBox_ = Math.Abs((int)(hBaseBox_ * ratioXY));

            //if (wBaseBox_ + 2 * wMargin > panelGraph.Width)
            //{
            //    wBaseBox_ = panelGraph.Width - 2 * wMargin;
            //    hBaseBox_ = (int)(wBaseBox_ / ratioXY);
            //}

            //// Compute picture box location
            //int xBox = panelGraph.Width / 2 - wBaseBox_ / 2;
            //int yBox = panelGraph.Height / 2 - hBaseBox_ / 2;

            //// Update picture box
            //pictureboxGraph.Location = new Point(xBox, yBox);
            //pictureboxGraph.Width = wBaseBox_;
            //pictureboxGraph.Height = hBaseBox_;            
        }

        private void enableChangeView(bool status)
        {
            labelXMinMax.Enabled = status;
            labelYMinMax.Enabled = status;
            numericupdownXMin.Enabled = status;
            numericupdownXMax.Enabled = status;
            numericupdownYMin.Enabled = status;
            numericupdownYMax.Enabled = status;

            mouseCanMoveGraph_ = status;

            labelUnit.Enabled = status;
            numericupdownUnit.Enabled = status && (comboboxHints.SelectedIndex > 0);
            labelHints.Enabled = status;
            comboboxHints.Enabled = status;

            this.MaximizeBox = status;
            this.FormBorderStyle = status ? FormBorderStyle.Sizable : FormBorderStyle.FixedSingle;
        }

        // TODO: layers
        private void updateHints()
        {
            switch (comboboxHints.SelectedIndex)
            {
                case 0: // none, nop
                    break;
                case 1: // axis
                    drawAxis();
                    break;
                case 2: // grid
                    drawGrid();
                    break;
            }
        }

        #endregion

        private void MainForm_Resize(object sender, EventArgs e)
        {
            // minimize causes draw error when window is maximized
            this.MinimizeBox = (this.WindowState == FormWindowState.Normal);
        }

        private void panelGraph_Resize(object sender, EventArgs e)
        {
            // nop if minimized
            if (this.WindowState == FormWindowState.Minimized)
                return;

            // Compute picture box dimensions
            wBaseBox_ = panelGraph.Width;
            hBaseBox_ = panelGraph.Height;

            // Compute picture box location
            int xBox = panelGraph.Width / 2 - wBaseBox_ / 2;
            int yBox = panelGraph.Height / 2 - hBaseBox_ / 2;

            // Update picture box
            pictureboxGraph.Location = new Point(xBox, yBox);
            pictureboxGraph.Width = wBaseBox_;
            pictureboxGraph.Height = hBaseBox_;

            if (view_.Isometric)
                view_.UpdateGivenY(wBaseBox_, hBaseBox_);

            clearGraph();
            updateViewDisplay();
            pictureboxGraph.Image = bitmapGraph_;

            hasResized_ = true;
        }

        private void buttonDrawColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                drawColor_ = colorDialog1.Color;
                buttonDrawColor.BackColor = drawColor_;
                if (drawBrush_ != null)
                    drawBrush_.Color = drawColor_;
                if (drawPen_ != null)
                    drawPen_.Color = drawColor_;
            }  
        }

        private void buttonBackColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                backColor_ = colorDialog1.Color;
                panelGraph.BackColor = backColor_;
                buttonBackColor.BackColor = backColor_;
            }  
        }

        private void textbox1Eq_TextChanged(object sender, EventArgs e)
        {
            updateGUI();
        }

        private void textbox2Eq_TextChanged(object sender, EventArgs e)
        {
            updateGUI();
        }

        private void updateGUI(bool curveTypeChanged = false)
        {
            bool exprsValid = false; // false

            if (curveTypeChanged)
            {
                expr1_ = "";
                expr2_ = "";
            }

            switch (curveType_)
            {
                case CurveType.PARAMETRIC:
                    {
                        if (curveTypeChanged)
                        {
                            label1Eq.Text = "x(t) =";
                            label2Eq.Visible = true;
                            textbox2Eq.Visible = true;
                            buttonCopy2Eq.Visible = true;

                            comboboxDrawType.SelectedIndex = 0; // line
                            comboboxDrawType.Enabled = true;

                            numericupdownPMin.Enabled = true;
                            numericupdownPMax.Enabled = true;
                            labelPMinMax.Enabled = true;
                            labelPMinMax.Text = "<   t  <";
                            labelPStep.Text = "t step";
                            tooltip_.SetToolTip(numericupdownPMin, "t minimum value");
                            tooltip_.SetToolTip(numericupdownPMax, "t maximum value");
                            tooltip_.SetToolTip(numericupdownPStep, "t draw step");
                        }

                        variables_ = new string[] { "t" };
                        bool xtExprValid = ExpressionTools.IsExpressionValid(textbox1Eq.Text, variables_);
                        bool ytExprValid = ExpressionTools.IsExpressionValid(textbox2Eq.Text, variables_);
                        exprsValid = xtExprValid && ytExprValid;

                        textbox1Eq.BackColor = xtExprValid ? SystemColors.Window : Color.MistyRose;
                        textbox2Eq.BackColor = ytExprValid ? SystemColors.Window : Color.MistyRose;
                        buttonCopy1Eq.Enabled = xtExprValid;
                        buttonCopy2Eq.Enabled = ytExprValid;
                    }
                    break;

                case CurveType.POLAR:
                    {
                        if (curveTypeChanged)
                        {
                            label1Eq.Text = "r(t) =";
                            label2Eq.Visible = false;
                            textbox2Eq.Text = "";
                            textbox2Eq.Visible = false;
                            buttonCopy2Eq.Visible = false;

                            comboboxDrawType.SelectedIndex = 0; // line

                            numericupdownPMin.Enabled = true;
                            numericupdownPMax.Enabled = true;
                            labelPMinMax.Enabled = true;
                            labelPMinMax.Text = "<   t  <";
                            labelPStep.Text = "t step";
                            tooltip_.SetToolTip(numericupdownPMin, "t minimum value");
                            tooltip_.SetToolTip(numericupdownPMax, "t maximum value");
                            tooltip_.SetToolTip(numericupdownPStep, "t draw step");
                        }

                        variables_ = new string[] { "t" };
                        bool rExprValid = ExpressionTools.IsExpressionValid(textbox1Eq.Text, variables_);
                        exprsValid = rExprValid;

                        textbox1Eq.BackColor = rExprValid ? SystemColors.Window : Color.MistyRose;
                        buttonCopy1Eq.Enabled = rExprValid;
                    }
                    break;

                case CurveType.CARTESIAN:
                    {
                        if (curveTypeChanged)
                        {
                            label1Eq.Text = "y(x) =";
                            label2Eq.Visible = false;
                            textbox2Eq.Text = "";
                            textbox2Eq.Visible = false;
                            buttonCopy2Eq.Visible = false;

                            comboboxDrawType.SelectedIndex = 0; // line

                            numericupdownPMin.Enabled = true;
                            numericupdownPMax.Enabled = true;
                            labelPMinMax.Enabled = true;
                            labelPMinMax.Text = "<   x  <";
                            labelPStep.Text = "x step";
                            tooltip_.SetToolTip(numericupdownPMin, "x minimum value");
                            tooltip_.SetToolTip(numericupdownPMax, "x maximum value");
                            tooltip_.SetToolTip(numericupdownPStep, "x draw step");
                        }

                        variables_ = new string[] { "x" };
                        bool yxExprValid = ExpressionTools.IsExpressionValid(textbox1Eq.Text, variables_);
                        exprsValid = yxExprValid;

                        textbox1Eq.BackColor = yxExprValid ? SystemColors.Window : Color.MistyRose;
                        buttonCopy1Eq.Enabled = yxExprValid;
                    }
                    break;

                case CurveType.EQUALITY:
                    {
                        if (curveTypeChanged)
                        {
                            label1Eq.Text = " 0  =";
                            label2Eq.Visible = false;
                            textbox2Eq.Text = "";
                            textbox2Eq.Visible = false;
                            buttonCopy2Eq.Visible = false;

                            comboboxDrawType.SelectedIndex = 1; // dots

                            numericupdownPMin.Enabled = false;
                            numericupdownPMax.Enabled = false;
                            labelPMinMax.Enabled = false;
                            labelPMinMax.Text = "";
                            labelPStep.Text = "x,y step";
                            tooltip_.SetToolTip(numericupdownPMin, "x,y minimum value");
                            tooltip_.SetToolTip(numericupdownPMax, "x,y maximum value");
                            tooltip_.SetToolTip(numericupdownPStep, "x,y draw step");
                        }

                        variables_ = new string[] { "x", "y" };
                        bool xyExprValid = ExpressionTools.IsExpressionValid(textbox1Eq.Text, variables_);
                        exprsValid = xyExprValid;

                        textbox1Eq.BackColor = xyExprValid ? SystemColors.Window : Color.MistyRose;
                        buttonCopy1Eq.Enabled = xyExprValid;
                    }
                    break;

                case CurveType.INEQUALITY:
                    {
                        if (curveTypeChanged)
                        {
                            label1Eq.Text = " 0  <";
                            label2Eq.Visible = false;
                            textbox2Eq.Text = "";
                            textbox2Eq.Visible = false;
                            buttonCopy2Eq.Visible = false;

                            comboboxDrawType.SelectedIndex = 1; // dots

                            numericupdownPMin.Enabled = false;
                            numericupdownPMax.Enabled = false;
                            labelPMinMax.Enabled = false;
                            labelPMinMax.Text = "";
                            labelPStep.Text = "x,y step";
                            tooltip_.SetToolTip(numericupdownPMin, "x,y minimum value");
                            tooltip_.SetToolTip(numericupdownPMax, "x,y maximum value");
                            tooltip_.SetToolTip(numericupdownPStep, "x,y draw step");
                        }

                        variables_ = new string[] { "x" , "y" };
                        bool xyExprValid = ExpressionTools.IsExpressionValid(textbox1Eq.Text, variables_);
                        exprsValid = xyExprValid;

                        textbox1Eq.BackColor = xyExprValid ? SystemColors.Window : Color.MistyRose;
                        buttonCopy1Eq.Enabled = xyExprValid;
                    }
                    break;
            }

            buttonDraw.Enabled = exprsValid;
            buttonNextStep.Enabled = exprsValid;
            buttonSave.Enabled = exprsValid;
            buttonPausePlay.Enabled = isDrawing_ || exprsValid;
        }

        private void pictureboxGraph_MouseClick(object sender, MouseEventArgs e)
        {
            //
        }

        private void pictureboxGraph_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // center view on origin
            if (mouseCanMoveGraph_)
            {
                double xRange = view_.Xmax - view_.Xmin;
                double yRange = view_.Ymax - view_.Ymin;

                view_.Xmin = -0.5 * xRange;
                view_.Xmax =  0.5 * xRange;
                view_.Ymin = -0.5 * yRange;
                view_.Ymax =  0.5 * yRange;

                isUpdatingOtherNumericUpDown_ = true;
                updateViewDisplay();
                clearGraph();
                isUpdatingOtherNumericUpDown_ = false;
            }
        }

        private void pictureboxGraph_MouseDown(object sender, MouseEventArgs e)
        {
            if (isDrawing_)
                return;

            if (mouseCanMoveGraph_ && e.Button == MouseButtons.Left)
            {
                mouseButtonOnGraphXScreen_ = e.X;
                mouseButtonOnGraphYScreen_ = e.Y;
                Tuple<double, double> xy = convertScreenCoordsToXY(mouseButtonOnGraphXScreen_, mouseButtonOnGraphYScreen_);
                mouseButtonOnGraphX_ = xy.Item1;
                mouseButtonOnGraphY_ = xy.Item2;

                this.pictureboxGraph.Cursor = Cursors.Hand;
                mouseButtonOnGraphDown_ = true;
            }
        }

        private void pictureboxGraph_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing_)
                return;

            int xScreen = e.X;
            int yScreen = e.Y;
            Tuple<double, double> xy = convertScreenCoordsToXY(xScreen, yScreen);
            double x = xy.Item1;
            double y = xy.Item2;

            if (mouseCanMoveGraph_ && mouseButtonOnGraphDown_)
            {
                bool moveDiscrete = (Control.ModifierKeys == Keys.Shift);
                if (moveDiscrete)
                {
                    int dDelta = 20; // pixels
                    xScreen = mouseButtonOnGraphXScreen_ + dDelta * ((xScreen - mouseButtonOnGraphXScreen_) / dDelta);
                    yScreen = mouseButtonOnGraphYScreen_ + dDelta * ((yScreen - mouseButtonOnGraphYScreen_) / dDelta);
                    System.Diagnostics.Trace.WriteLine("delta x = " + (xScreen - mouseButtonOnGraphXScreen_));
                    xy = convertScreenCoordsToXY(xScreen, yScreen);
                    x = xy.Item1;
                    y = xy.Item2;
                }
                
                double xDelta = x - mouseButtonOnGraphX_;
                double yDelta = y - mouseButtonOnGraphY_;

                bool updateX = moveDiscrete || (xScreen != mouseButtonOnGraphXScreen_);
                bool updateY = moveDiscrete || (yScreen != mouseButtonOnGraphYScreen_);

                if (updateX)
                {
                    view_.Xmin -= xDelta;
                    view_.Xmax -= xDelta;
                }

                if (updateY)
                {
                    view_.Ymin -= yDelta;
                    view_.Ymax -= yDelta;
                }

                if (updateX || updateY)
                {
                    isUpdatingOtherNumericUpDown_ = true;
                    updateViewDisplay();
                    clearGraph();
                    isUpdatingOtherNumericUpDown_ = false;
                }
            }
            else
            {
                labelCoords.Visible = true;

                // display coordinates
                labelCoords.Text = Environment.NewLine;
                labelCoords.Text += "x=" + x.ToString("0.0000") + Environment.NewLine;
                labelCoords.Text += "y=" + y.ToString("0.0000");
            }
        }

        private void pictureboxGraph_MouseUp(object sender, MouseEventArgs e)
        {

            if (isDrawing_)
                return;

            this.pictureboxGraph.Cursor = Cursors.Cross;
            mouseButtonOnGraphDown_ = false;
        }

        private void pictureboxGraph_MouseLeave(object sender, EventArgs e)
        {
            mouseButtonOnGraphDown_ = false;

            if (isDrawing_)
                return;

            labelCoords.Visible = false;
        }

        private void comboboxHints_SelectedIndexChanged(object sender, EventArgs e)
        {
            clearGraph();
            pictureboxGraph.Image = bitmapGraph_;

            numericupdownUnit.Enabled = (comboboxHints.SelectedIndex > 0);
        }

        private void comboboxCurveType_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurveType curveTypeFormer = curveType_;
            CurveType curveTypeNew = CurveType.PARAMETRIC; // default

            switch (comboboxCurveType.SelectedItem.ToString().ToUpper())
            {
                case "PARAMETRIC":
                    curveTypeNew = CurveType.PARAMETRIC;
                    break;

                case "POLAR":
                    curveTypeNew = CurveType.POLAR;
                    break;

                case "CARTESIAN":
                    curveTypeNew = CurveType.CARTESIAN;
                    break;

                case "EQUALITY":
                    curveTypeNew = CurveType.EQUALITY;
                    break;

                case "INEQUALITY":
                    curveTypeNew = CurveType.INEQUALITY;
                    break;
            }

            // nop if type not changed
            if (curveTypeNew == curveTypeFormer)
                return;

            // update GUI, with type changed
            curveType_ = curveTypeNew;
            updateGUI(true /* curveTypeChanged */);

            // update curves combo box
            updateComboboxCurves();
            comboboxCurves.SelectedIndex = 0;
        }

        private void comboboxDrawType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboboxDrawType.SelectedItem.ToString().ToUpper())
            {
                case "LINE":
                    if (curveType_ == CurveType.EQUALITY
                     || curveType_ == CurveType.INEQUALITY)
                    {
                        MessageBox.Show("Line draw mode not allowed for this type of equation, changing to dots.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        comboboxDrawType.SelectedIndex = 1;
                    }
                    else
                        drawType_ = DrawType.LINE;

                    break;

                case "DOTS":
                    drawType_ = DrawType.DOT;
                    break;

                case "SQUARE":
                    drawType_ = DrawType.SQUARE;
                    break;
            }
        }

        #region Coordinates conversion

        private Tuple<double, double> convertXYToScreenCoords(double x, double y)
        {
            int w = wBaseBox_;
            int h = hBaseBox_;

            double xScaleFactor = w / (view_.Xmax - view_.Xmin);
            double xCenter = 0 - xScaleFactor * view_.Xmin;

            double yScaleFactor = h / (view_.Ymin - view_.Ymax);
            double yCenter = 0 - yScaleFactor * view_.Ymax;

            double xScreen = xCenter + xScaleFactor * x;
            double yScreen = yCenter + yScaleFactor * y;

            return Tuple.Create(xScreen, yScreen);
        }

        private Tuple<double, double> convertScreenCoordsToXY(double xScreen, double yScreen)
        {
            int w = wBaseBox_;
            int h = hBaseBox_;

            double xScaleFactor = (view_.Xmax - view_.Xmin) / w;
            double x0 = view_.Xmin;

            double yScaleFactor = (view_.Ymax - view_.Ymin) / h;
            double y0 = view_.Ymax;

            double x = x0 + xScaleFactor * xScreen;
            double y = y0 - yScaleFactor * yScreen;

            return Tuple.Create(x, y);
        }

        #endregion

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            stopDraw();
        }

        private void comboboxCurves_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isDrawing_ && !hasPaused_)
            {
                updatePausePlayGUI(true);
                timer_.Stop();
            }

            CurveData cd = comboboxCurves.SelectedItem as CurveData;
            bool isNewEquation = (cd.Name == "<New>");

            switch (curveType_)
            {
                case CurveType.PARAMETRIC:
                    textbox1Eq.Text = cd.Equation1;
                    textbox2Eq.Text = cd.Equation2;
                    expr1_ = textbox1Eq.Text;
                    expr2_ = textbox2Eq.Text;
                    break;

                case CurveType.POLAR:
                case CurveType.CARTESIAN:
                case CurveType.EQUALITY:
                case CurveType.INEQUALITY:
                    textbox1Eq.Text = cd.Equation1;
                    textbox2Eq.Text = "";
                    expr1_ = textbox1Eq.Text;
                    expr2_ = "";
                    break;
            }

            if (cd.Isometric)
                cd.UpdateGivenY(wBaseBox_, hBaseBox_);

            decimal xMin = (decimal)cd.XVmin;
            decimal xMax = (decimal)cd.XVmax;
            decimal yMin = (decimal)cd.YVmin;
            decimal yMax = (decimal)cd.YVmax;

            // if loading an equation with different border values, show choice dialog
            if (!isNewEquation)
            {                
                if (xMin != numericupdownXMin.Value || xMax != numericupdownXMax.Value
                 || yMin != numericupdownYMin.Value || yMax != numericupdownYMax.Value)
                {
                    string msg = "Equation \"" + cd.Name + "\" has different predefined x,y min and max values." + Environment.NewLine;
                    msg += Environment.NewLine;
                    msg += "Do you want apply them? (The current image will be erased)";
                    DialogResult result = MessageBox.Show(msg, "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (result == DialogResult.Yes)
                    {
                        stopDraw();

                        enableChangeView(true);
                        buttonClear.Enabled = false;

                        numericupdownXMin.Value = xMin;
                        numericupdownXMax.Value = xMax;
                        numericupdownYMin.Value = yMin;
                        numericupdownYMax.Value = yMax;

                        updateViewDisplay();
                        clearGraph();
                    }
                    else
                    {
                        // nop
                    }
                }
            }
            else
            {
                switch (curveType_)
                {
                    case CurveType.PARAMETRIC:
                    case CurveType.POLAR:
                        cd.Pmin = 0;
                        cd.Pmax = 6.283; // 2*Math.PI;
                        break;

                    case CurveType.CARTESIAN:
                    case CurveType.EQUALITY:
                    case CurveType.INEQUALITY:
                        cd.Pmin = view_.Xmin;
                        cd.Pmax = view_.Xmax;
                        break;
                }

                // focus on x equation
                this.ActiveControl = textbox1Eq;
                textbox1Eq.Focus();
            }

            numericupdownPMin.Value = (decimal)cd.Pmin;
            numericupdownPMax.Value = (decimal)cd.Pmax;
            numericupdownPStep.Value = (decimal)cd.Pstep;

            numericupdownThickness.Value = (int)cd.Thickness;

            buttonSave.Text = isNewEquation ? "Save equation as..." : "Save equation";
            buttonDelete.Enabled = !isNewEquation;
        }

        #region Equations load/save

        private void readEquationsFile(string filename)
        {
            string line;
            CurveData cd = new CurveData();

            // Read the file and process it line by line
            bool isNewEquation = false;
            StreamReader file = new StreamReader(filename, Encoding.UTF8);
            while ((line = file.ReadLine()) != null)
            {
                if (String.IsNullOrEmpty(line))
                    continue;

                // new equation name
                if (!isNewEquation)
                if (line.StartsWith("[") && line.EndsWith("]"))
                {
                    // push previous equation in list if existing
                    if (!String.IsNullOrEmpty(cd.Name))
                        savedEquations_.Add(cd.Copy());

                    // create new equation
                    cd.Clear();
                    cd.Name = line.Substring(1, line.Length - 2);
                    isNewEquation = true;
                    continue;
                }

                isNewEquation = false;

                string[] paramValue = line.Split('=', '<');
                if (paramValue.Length != 2) // invalid
                    continue;

                string param = paramValue[0];
                string value = paramValue[1];

                CultureInfo culture = new CultureInfo("en-US");
                switch (param.ToLower())
                {
                    case "type":
                        switch (value.ToUpper())
                        {
                            case "PARAMETRIC":
                                cd.Type = CurveType.PARAMETRIC;
                                break;

                            case "POLAR":
                                cd.Type = CurveType.POLAR;
                                break;

                            case "CARTESIAN":
                                cd.Type = CurveType.CARTESIAN;
                                break;

                            case "EQUALITY":
                                cd.Type = CurveType.EQUALITY;
                                break;

                            case "INEQUALITY":
                                cd.Type = CurveType.INEQUALITY;
                                break;

                            default:
                                cd.Type = CurveType.PARAMETRIC;
                                break;
                        }
                        break;
                    
                    // equations
                    case "x(t)":
                        cd.Equation1 = value;
                        break;
                    case "y(t)":
                        cd.Equation2 = value;
                        break;
                    case "r(t)":
                        cd.Equation1 = value;
                        break;
                    case "y(x)":
                        cd.Equation1 = value;
                        break;
                    case "0":
                        cd.Equation1 = value;
                        break;

                    // view
                    case "xv_min":
                        cd.XVmin = Convert.ToDouble(value, culture);
                        break;
                    case "xv_max":
                        cd.XVmax = Convert.ToDouble(value, culture);
                        break;
                    case "yv_min":
                        cd.YVmin = Convert.ToDouble(value, culture);
                        break;
                    case "yv_max":
                        cd.YVmax = Convert.ToDouble(value, culture);
                        break;
                    case "isometric":
                        cd.Isometric = Convert.ToBoolean(value, culture);
                        break;

                    // parametric and polar curves
                    case "t_min":
                        cd.Pmin = Convert.ToDouble(value, culture);
                        break;
                    case "t_max":
                        cd.Pmax = Convert.ToDouble(value, culture);
                        break;
                    case "t_step":
                        cd.Pstep = Convert.ToDouble(value, culture);
                        break;

                    // cartesian curves
                    case "view_.Xmin":
                        cd.Pmin = Convert.ToDouble(value, culture);
                        break;
                    case "view_.Xmax":
                        cd.Pmax = Convert.ToDouble(value, culture);
                        break;
                    case "x_step":
                        cd.Pstep = Convert.ToDouble(value, culture);
                        break;

                    case "thickness":
                        cd.Thickness = Convert.ToInt32(value);
                        break;
                }
            }

            // push last equation in list if existing
            if (!String.IsNullOrEmpty(cd.Name))
                savedEquations_.Add(cd.Copy());

            file.Close();
        }

        private void saveEquationsToFile(string filename)
        {
            string equationsDesc = "";

            foreach (CurveData cd in savedEquations_)
                equationsDesc += cd.GetDescription();

            File.WriteAllText(filename, equationsDesc);
        }

#if DEBUG
            private void readExportConfigFile(string filename)
            {
                string line;

                // Read the file and process it line by line
                StreamReader file = new StreamReader(filename, Encoding.UTF8);
                while ((line = file.ReadLine()) != null)
                {
                    if (String.IsNullOrEmpty(line))
                        continue;

                    if (line.StartsWith("[") && line.EndsWith("]"))
                        continue;

                    string[] paramValue = line.Split('=');
                    if (paramValue.Length != 2) // invalid
                        continue;

                    string param = paramValue[0];
                    string value = paramValue[1];

                    switch (param.ToLower())
                    {
                        case "left":
                            xExport_ = Convert.ToInt32(value);
                            break;
                        case "top":
                            yExport_ = Convert.ToInt32(value);
                            break;
                        case "width":
                            wExport_ = Convert.ToInt32(value);
                            break;
                        case "height":
                            hExport_ = Convert.ToInt32(value);
                            break;
                    }
                }

                file.Close();
            }
#endif

        #endregion
    }
}
