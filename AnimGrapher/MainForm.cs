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
        private float dpiX_;
        private float dpiY_;

        // for debug purposes only
        #if DEBUG
            private int xExport_ = 0;
            private int yExport_ = 0;
            private int wExport_ = 0;
            private int hExport_ = 0;
        #endif

        // display axis variables
        private double X_MIN = -2;
        private double X_MAX = +2;
        private double Y_MIN = -2;
        private double Y_MAX = +2;
        private double interval_ = 0.5;

        Graphics gGraph_;
        Bitmap bitmapGraph_;
        int wBaseBox_;
        int hBaseBox_;
        bool hasResized_;
        bool hasPaused_;
        bool hasCleared_;
        bool isDrawing_;

        // draw parameters

        private double p_;
        private double pStep_ = 0.05;
        private double pMin_ = 0;
        private double pMax_ = 2 * Math.PI;

        private DrawType drawType_ = DrawType.PLOT;
        private float penWidth_ = 3;

        private Color drawColor_ = Color.Black;
        private Color backColor_ = Color.FromArgb(252, 252, 254);

        SolidBrush drawBrush_;
        Pen drawPen_;

        private Point prevPoint_;
        private bool hasInitPoint_;
        private bool lastPoint_;

        private bool showCoordsAtDraw_ = true;

        // timer objects

        // archaic, slow, but stable
        System.Windows.Forms.Timer timer_;
        private int timerInterval_ = 1; // ms

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
        private string xtExpr_ = "";
        private string ytExpr_ = "";
        private string rtExpr_ = "";
        private string yxExpr_ = "";

        // saved equations
        private string pathEquations_ = "AnimGrapherEquations.txt";
        private List<CurveData> savedEquations_;

        // curve type
        private CurveType curveType_ = CurveType.PARAMETRIC;

        public MainForm()
        {
            Graphics graphics = this.CreateGraphics();
            dpiX_ = graphics.DpiX;
            dpiY_ = graphics.DpiY;

            //#if DEBUG
            //    MessageBox.Show("DPI: X = " + dpiX_ + ", Y = " + dpiY_);
            //#endif

            ExpressionTools.Init();

            drawBrush_ = new SolidBrush(drawColor_);
            drawPen_ = new Pen(drawColor_);

            // setup timer
            timer_ = new System.Windows.Forms.Timer();
            timer_.Interval = timerInterval_;
            timer_.Tick += timer__Tick;

            //timer_ = new System.Timers.Timer();
            //timer_.Interval = timerInterval_;
            //timer_.Elapsed += Timer__Elapsed;

            //timer_ = new DispatcherTimer();
            //timer_.Interval = TimeSpan.FromTicks((long)timerInterval_);
            //timer_.Interval = TimeSpan.FromMilliseconds(timerInterval_);
            //timer_.Tick += timer__Tick;

            //timer_ = new HighResolutionTimer(timerInterval_);
            //timer_.UseHighPriorityThread = true;
            //timer_.Elapsed += timer__Tick;//Timer__Elapsed;

            //timer_ = new MultimediaTimer() { Interval = timerInterval_ };
            //timer_.Elapsed += timer__Tick;

            savedEquations_ = new List<CurveData>();

            InitializeComponent();

            wBaseBox_ = pictureboxGraph.Width;
            hBaseBox_ = pictureboxGraph.Height;

            // deactivated (causes issues during edition): parameters additional callbacks

            //numericupdownXMin.TextChanged += new EventHandler(numericupdownXMin_TextChanged);
            //numericupdownXMax.TextChanged += new EventHandler(numericupdownXMax_TextChanged);
            //numericupdownYMin.TextChanged += new EventHandler(numericupdownYMin_TextChanged);
            //numericupdownYMax.TextChanged += new EventHandler(numericupdownYMax_TextChanged);
            //numericupdownInterval.TextChanged += new EventHandler(numericupdownInterval_TextChanged);

            //numericupdownTMin.TextChanged += new EventHandler(numericupdownTMin_TextChanged);
            //numericupdownTMax.TextChanged += new EventHandler(numericupdownTMax_TextChanged);
            //numericupdownTStep.TextChanged += new EventHandler(numericupdownTStep_TextChanged);
            //numericupdownThickness.TextChanged += new EventHandler(numericupdownThickness_TextChanged);

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

            comboboxCurveType.SelectedIndex = 0;
            comboboxDrawType.SelectedIndex = 0;

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
            //comboboxHints.SelectedIndex = 1; // default: axis

            updateAxisParams();
            clearGraph();
            updateGUI();

            this.ResumeLayout();

            // focus on x equation
            this.ActiveControl = textbox1Eq;
            textbox1Eq.Focus();
        }

        private void updateComboboxCurves()
        {
            comboboxCurves.Items.Clear();

            comboboxCurves.DisplayMember = "Name";
            comboboxCurves.ValueMember = "Equation";

            // first item empty
            CurveData cdEmpty = new CurveData("<New>", curveType_);
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
            for (double xMark = interval_; xMark <= X_MAX; xMark += interval_)
            {
                coords = convertXYToScreenCoords(xMark, 0);
                int xMarkScreen = (int)(coords.Item1 + 0.5);
                int yMarkScreen = (int)(coords.Item2 + 0.5);

                gGraph_.DrawLine(myPen, new Point(xMarkScreen, yMarkScreen - markLength), new Point(xMarkScreen, yMarkScreen + markLength));
            }

            // x < 0
            for (double xMark = -interval_; xMark >= X_MIN; xMark -= interval_)
            {
                coords = convertXYToScreenCoords(xMark, 0);
                int xMarkScreen = (int)(coords.Item1 + 0.5);
                int yMarkScreen = (int)(coords.Item2 + 0.5);

                gGraph_.DrawLine(myPen, new Point(xMarkScreen, yMarkScreen - markLength), new Point(xMarkScreen, yMarkScreen + markLength));
            }

            // y > 0
            for (double yMark = interval_; yMark <= Y_MAX; yMark += interval_)
            {
                coords = convertXYToScreenCoords(0, yMark);
                int xMarkScreen = (int)(coords.Item1 + 0.5);
                int yMarkScreen = (int)(coords.Item2 + 0.5);

                gGraph_.DrawLine(myPen, new Point(xMarkScreen - markLength, yMarkScreen), new Point(xMarkScreen + markLength, yMarkScreen));
            }

            // y < 0
            for (double yMark = -interval_; yMark >= Y_MIN; yMark -= interval_)
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
            for (double xMark = interval_; xMark <= X_MAX; xMark += interval_)
            {
                coords = convertXYToScreenCoords(xMark, 0);
                int xMarkScreen = (int)(coords.Item1 + 0.5);
                int yMarkScreen = (int)(coords.Item2 + 0.5);

                gGraph_.DrawLine(gridPen, new Point(xMarkScreen, 0), new Point(xMarkScreen, hBaseBox_));
            }

            // x < 0
            for (double xMark = -interval_; xMark >= X_MIN; xMark -= interval_)
            {
                coords = convertXYToScreenCoords(xMark, 0);
                int xMarkScreen = (int)(coords.Item1 + 0.5);
                int yMarkScreen = (int)(coords.Item2 + 0.5);

                gGraph_.DrawLine(gridPen, new Point(xMarkScreen, 0), new Point(xMarkScreen, hBaseBox_));
            }

            // y > 0
            for (double yMark = interval_; yMark <= Y_MAX; yMark += interval_)
            {
                coords = convertXYToScreenCoords(0, yMark);
                int xMarkScreen = (int)(coords.Item1 + 0.5);
                int yMarkScreen = (int)(coords.Item2 + 0.5);

                gGraph_.DrawLine(gridPen, new Point(0, yMarkScreen), new Point(wBaseBox_, yMarkScreen));
            }

            // y < 0
            for (double yMark = -interval_; yMark >= Y_MIN; yMark -= interval_)
            {
                coords = convertXYToScreenCoords(0, yMark);
                int xMarkScreen = (int)(coords.Item1 + 0.5);
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
                            // draw rectangle joining current and previous point
                            //int dx = currPoint.X - prevPoint_.X;
                            //int dy = currPoint.Y - prevPoint_.Y;
                            //float dist = (float)Math.Sqrt(dx * dx + dy * dy);
                            //float ratio = penWidth_ / dist / 2;
                            //float rdx = ratio * dx;
                            //float rdy = ratio * dy;
                            //Point[] points = new Point[4];
                            //points[0].X = (int)(currPoint.X + rdy + 0.5);
                            //points[0].Y = (int)(currPoint.Y - rdx + 0.5);
                            //points[1].X = (int)(currPoint.X - rdy + 0.5);
                            //points[1].Y = (int)(currPoint.Y + rdx + 0.5);
                            //points[2].X = (int)(prevPoint_.X - rdy + 0.5);
                            //points[2].Y = (int)(prevPoint_.Y + rdx + 0.5);
                            //points[3].X = (int)(prevPoint_.X + rdy + 0.5);
                            //points[3].Y = (int)(prevPoint_.Y - rdx + 0.5);
                            //gGraph_.FillPolygon(drawBrush_, points);

                            // draw current point as disc
                            gGraph_.FillEllipse(drawBrush_,
                                currPoint.X - penWidth_ / 2, currPoint.Y - penWidth_ / 2,
                                penWidth_, penWidth_
                            );
                        }
                        //else
                        gGraph_.DrawLine(drawPen_, prevPoint_, currPoint);
                    }
                    break;

                case DrawType.PLOT:
                    // draw current point as disc
                    gGraph_.FillEllipse(drawBrush_,
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

                // polar curve specific: calculate r
                if (curveType_ == CurveType.POLAR)
                {
                    try
                    {
                        r = ExpressionTools.Evaluate(rtExpr_, "t", p_);
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
                            x = ExpressionTools.Evaluate(xtExpr_, "t", p_);
                            break;

                        case CurveType.POLAR:
                            x = r * Math.Cos(p_);
                            break;

                        case CurveType.CARTESIAN:
                            x = p_;
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
                            y = ExpressionTools.Evaluate(ytExpr_, "t", p_);
                            break;

                        case CurveType.POLAR:
                            y = r * Math.Sin(p_);
                            break;

                        case CurveType.CARTESIAN:
                            y = ExpressionTools.Evaluate(yxExpr_, "x", p_);
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

                // show pencil?
                bool showPencil = (i == speedFactor - 1);

                // draw point
                if (!hasInitPoint_)
                    drawInitPoint(x, y);
                else
                    drawNextPoint(x, y, showPencil);

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

                // last point reached?
                if (p_ + pStep_ > pMax_)
                {
                    p_ = pMax_;
                    lastPoint_ = true;
                }
                else
                    p_ += pStep_;
                
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

            switch (curveType_)
            {
                case CurveType.PARAMETRIC:
                    xtExpr_ = textbox1Eq.Text;
                    ytExpr_ = textbox2Eq.Text;
                    break;

                case CurveType.POLAR:
                    rtExpr_ = textbox1Eq.Text;
                    break;

                case CurveType.CARTESIAN:
                    yxExpr_ = textbox1Eq.Text;
                    break;
            }

            labelCoords.Text = "";
            labelCoords.Visible = true;
            //pictureboxPencil.Visible = true;

            // disable axis parameters and resizing
            enableAxisParameters(false);
            //this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

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
            //this.MaximizeBox = true;
            this.Invoke(new MethodInvoker(delegate { this.MaximizeBox = true; }));
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

                CurveData curveData = new CurveData(name, curveType_);

                switch (curveType_)
                {
                    case CurveType.PARAMETRIC:
                        curveData.XtEquation = textbox1Eq.Text;
                        curveData.YtEquation = textbox2Eq.Text;
                        break;

                    case CurveType.POLAR:
                        curveData.RtEquation = textbox1Eq.Text;
                        break;

                    case CurveType.CARTESIAN:
                        curveData.YxEquation = textbox1Eq.Text;
                        break;
                }

                curveData.XVMin = (double)numericupdownXMin.Value;
                curveData.XVMax = (double)numericupdownXMax.Value;
                curveData.YVMin = (double)numericupdownYMin.Value;
                curveData.YVMax = (double)numericupdownYMax.Value;

                curveData.PMin = (double)numericupdownPMin.Value;
                curveData.PMax = (double)numericupdownPMax.Value;
                curveData.PStep = (double)numericupdownPStep.Value;

                curveData.Thickness = (double)numericupdownThickness.Value;

                // search if name already exists
                bool exists = false;
                int index = -1;
                int indexCombobox = 0;
                foreach (CurveData cd in savedEquations_)
                {
                    index++;
                    if (cd.Name == name) // found
                    {
                        exists = true;
                        break;
                    }
                }

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

            /*
            DateTime startDate = DateTime.Now;

            int nbTicks = (int)((tMax_ - tMin_) / tStep_) + 1;
            for (int i = 0; i < nbTicks; i++)
                timer__Tick(null, null);

            string drawTime = (DateTime.Now - startDate).TotalMilliseconds.ToString(); //.ToString(@"hh\:mm\:ss");
            Console.WriteLine("Draw time:" + drawTime);
            */
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            stopDraw();
            clearGraph();

            enableAxisParameters(true);
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
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.DefaultExt = ".jpg";
            dialog.AddExtension = true;
            dialog.Filter = "JPEG - Image files|*.jpg|BMP - Bitmap files|*.bmp";
            dialog.FileName = defaultName;

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
            if (gGraph_ != null)
                gGraph_.Clear(Color.Transparent);

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
        }

        #region Draw parameters callbacks

        private void numericupdownPMin_ValueChanged(object sender, EventArgs e)
        {
            updateDrawParams();
        }

        private void numericupdownPMin_TextChanged(object sender, EventArgs e)
        {
            updateDrawParams();
        }

        private void numericupdownPMax_ValueChanged(object sender, EventArgs e)
        {
            updateDrawParams();
        }

        private void numericupdownPMax_TextChanged(object sender, EventArgs e)
        {
            updateDrawParams();
        }

        private void numericupdownPStep_ValueChanged(object sender, EventArgs e)
        {
            updateDrawParams();
        }

        private void numericupdownPStep_TextChanged(object sender, EventArgs e)
        {
            updateDrawParams();
        }

        private void numericupdownThickness_ValueChanged(object sender, EventArgs e)
        {
            updateDrawParams();
        }

        private void numericupdownThickness_TextChanged(object sender, EventArgs e)
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
            updateAxisParams();
            clearGraph();
        }

        private void numericupdownXMin_TextChanged(object sender, EventArgs e)
        {
            updateAxisParams();
            clearGraph();
        }

        private void numericupdownXMax_ValueChanged(object sender, EventArgs e)
        {
            updateAxisParams();
            clearGraph();
        }

        private void numericupdownXMax_TextChanged(object sender, EventArgs e)
        {
            updateAxisParams();
            clearGraph();
        }

        private void numericupdownYMin_ValueChanged(object sender, EventArgs e)
        {
            updateAxisParams();
            clearGraph();
        }

        private void numericupdownYMin_TextChanged(object sender, EventArgs e)
        {
            updateAxisParams();
            clearGraph();
        }

        private void numericupdownYMax_ValueChanged(object sender, EventArgs e)
        {
            updateAxisParams();
            clearGraph();
        }

        private void numericupdownYMax_TextChanged(object sender, EventArgs e)
        {
            updateAxisParams();
            clearGraph();
        }

        private void numericupdownInterval_ValueChanged(object sender, EventArgs e)
        {
            updateAxisParams();
            clearGraph();
        }

        private void numericupdownInterval_TextChanged(object sender, EventArgs e)
        {
            updateAxisParams();
            clearGraph();
        }

        private void updateAxisParams()
        {
            // ensure coherency
            numericupdownXMin.Maximum = numericupdownXMax.Value - numericupdownXMin.Increment;
            numericupdownXMax.Minimum = numericupdownXMin.Value + numericupdownXMax.Increment;
            numericupdownYMin.Maximum = numericupdownYMax.Value - numericupdownYMin.Increment;
            numericupdownYMax.Minimum = numericupdownYMin.Value + numericupdownYMax.Increment;

            // update values
            X_MIN = (double)numericupdownXMin.Value;
            X_MAX = (double)numericupdownXMax.Value;
            Y_MIN = (double)numericupdownYMin.Value;
            Y_MAX = (double)numericupdownYMax.Value;

            interval_ = (double)numericupdownInterval.Value;

            // resize picture box

            // Ratio width/height
            float ratioXY = (float)(X_MAX - X_MIN) / (float)(Y_MAX - Y_MIN);

            // Margin
            int wMargin = 0;
            int hMargin = 0;

            // Compute picture box dimensions
            hBaseBox_ = Math.Abs(panelGraph.Height - 2 * hMargin);
            wBaseBox_ = Math.Abs((int)(hBaseBox_ * ratioXY));

            if (wBaseBox_ + 2 * wMargin > panelGraph.Width)
            {
                wBaseBox_ = panelGraph.Width - 2 * wMargin;
                hBaseBox_ = (int)(wBaseBox_ / ratioXY);
            }

            // Compute picture box location
            int xBox = panelGraph.Width / 2 - wBaseBox_ / 2;
            int yBox = panelGraph.Height / 2 - hBaseBox_ / 2;

            // Update picture box
            pictureboxGraph.Location = new Point(xBox, yBox);
            pictureboxGraph.Width = wBaseBox_;
            pictureboxGraph.Height = hBaseBox_;            
        }

        private void enableAxisParameters(bool status)
        {
            labelXMinMax.Enabled = status;
            labelYMinMax.Enabled = status;
            numericupdownXMin.Enabled = status;
            numericupdownXMax.Enabled = status;
            numericupdownYMin.Enabled = status;
            numericupdownYMax.Enabled = status;

            labelInterval.Enabled = status;
            numericupdownInterval.Enabled = status && (comboboxHints.SelectedIndex > 0);
            labelHints.Enabled = status;
            comboboxHints.Enabled = status;
        }

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

            updateAxisParams();
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

                            labelPMinMax.Text = "<   t  <";
                            labelPStep.Text = "t step";
                            tooltip_.SetToolTip(numericupdownPMin, "t minimum value");
                            tooltip_.SetToolTip(numericupdownPMax, "t maximum value");
                            tooltip_.SetToolTip(numericupdownPStep, "t draw step");

                            rtExpr_ = "";
                            yxExpr_ = "";
                        }

                        bool xtExprValid = ExpressionTools.IsExpressionValid(textbox1Eq.Text, "t");
                        bool ytExprValid = ExpressionTools.IsExpressionValid(textbox2Eq.Text, "t");
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

                            labelPMinMax.Text = "<   t  <";
                            labelPStep.Text = "t step";
                            tooltip_.SetToolTip(numericupdownPMin, "t minimum value");
                            tooltip_.SetToolTip(numericupdownPMax, "t maximum value");
                            tooltip_.SetToolTip(numericupdownPStep, "t draw step");

                            xtExpr_ = "";
                            ytExpr_ = "";
                            yxExpr_ = "";
                        }

                        bool rExprValid = ExpressionTools.IsExpressionValid(textbox1Eq.Text, "t");
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

                            labelPMinMax.Text = "<   x  <";
                            labelPStep.Text = "x step";
                            tooltip_.SetToolTip(numericupdownPMin, "x minimum value");
                            tooltip_.SetToolTip(numericupdownPMax, "x maximum value");
                            tooltip_.SetToolTip(numericupdownPStep, "x draw step");

                            xtExpr_ = "";
                            ytExpr_ = "";
                            rtExpr_ = "";
                        }

                        bool yxExprValid = ExpressionTools.IsExpressionValid(textbox1Eq.Text, "x");
                        exprsValid = yxExprValid;

                        textbox1Eq.BackColor = yxExprValid ? SystemColors.Window : Color.MistyRose;
                        buttonCopy1Eq.Enabled = yxExprValid;
                    }
                    break;
            }

            buttonDraw.Enabled = exprsValid;
            buttonNextStep.Enabled = exprsValid;
            buttonSave.Enabled = exprsValid;
            buttonPausePlay.Enabled = isDrawing_ || exprsValid;
        }

        private void pictureboxGraph_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing_)
                return;

            labelCoords.Visible = true;

            // display coordinates
            int xPic = e.X;
            int yPic = e.Y;
            Tuple<double, double> xy = convertScreenCoordsToXY(xPic, yPic);
            labelCoords.Text = Environment.NewLine;
            labelCoords.Text += "x=" + xy.Item1.ToString("0.0000") + Environment.NewLine;
            labelCoords.Text += "y=" + xy.Item2.ToString("0.0000");
        }

        private void pictureboxGraph_MouseLeave(object sender, EventArgs e)
        {
            if (isDrawing_)
                return;

            labelCoords.Visible = false;
        }

        private void comboboxHints_SelectedIndexChanged(object sender, EventArgs e)
        {
            clearGraph();
            pictureboxGraph.Image = bitmapGraph_;

            numericupdownInterval.Enabled = (comboboxHints.SelectedIndex > 0);
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
            }

            // nop if type not changed
            if (curveTypeNew == curveTypeFormer)
                return;

            // update GUI, with type changed
            curveType_ = curveTypeNew;
            updateGUI(true);

            // update curves combo box
            updateComboboxCurves();
            comboboxCurves.SelectedIndex = 0;
        }

        private void comboboxDrawType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboboxDrawType.SelectedItem.ToString().ToUpper())
            {
                case "LINE":
                    drawType_ = DrawType.LINE;
                    break;

                case "PLOT":
                    drawType_ = DrawType.PLOT;
                    break;
            }
        }

        #region Coordinates conversion

        private Tuple<double, double> convertXYToScreenCoords(double x, double y)
        {
            int w = wBaseBox_;
            int h = hBaseBox_;

            double xScaleFactor = w / (X_MAX - X_MIN);
            double xCenter = 0 - xScaleFactor * X_MIN;

            double yScaleFactor = h / (Y_MIN - Y_MAX);
            double yCenter = 0 - yScaleFactor * Y_MAX;

            double xScreen = xCenter + xScaleFactor * x;
            double yScreen = yCenter + yScaleFactor * y;

            return Tuple.Create(xScreen, yScreen);
        }

        private Tuple<double, double> convertScreenCoordsToXY(double xScreen, double yScreen)
        {
            int w = wBaseBox_;
            int h = hBaseBox_;

            double xScaleFactor = (X_MAX - X_MIN) / w;
            double x0 = X_MIN;

            double yScaleFactor = (Y_MAX - Y_MIN) / h;
            double y0 = Y_MIN;

            double x =  x0 + xScaleFactor * xScreen;
            double y = -y0 - yScaleFactor * yScreen;

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

            CurveData curveData = comboboxCurves.SelectedItem as CurveData;
            bool isNewEquation = (curveData.Name == "<New>");

            switch (curveType_)
            {
                case CurveType.PARAMETRIC:
                    textbox1Eq.Text = curveData.XtEquation;
                    textbox2Eq.Text = curveData.YtEquation;
                    xtExpr_ = textbox1Eq.Text;
                    ytExpr_ = textbox2Eq.Text;
                    break;

                case CurveType.POLAR:
                    textbox1Eq.Text = curveData.RtEquation;
                    rtExpr_ = textbox1Eq.Text;
                    break;

                case CurveType.CARTESIAN:
                    textbox1Eq.Text = curveData.YxEquation;
                    yxExpr_ = textbox1Eq.Text;
                    break;
            }

            decimal xMin = (decimal)curveData.XVMin;
            decimal xMax = (decimal)curveData.XVMax;
            decimal yMin = (decimal)curveData.YVMin;
            decimal yMax = (decimal)curveData.YVMax;

            // if loading an equation with different border values, show choice dialog
            if (!isNewEquation)
            {
                if (xMin != numericupdownXMin.Value || xMax != numericupdownXMax.Value
                 || yMin != numericupdownYMin.Value || yMax != numericupdownYMax.Value)
                {
                    string msg = "Equation \"" + curveData.Name + "\" has different predefined x,y min and max values." + Environment.NewLine;
                    msg += Environment.NewLine;
                    msg += "Do you want apply them? (The current image will be erased)";
                    DialogResult result = MessageBox.Show(msg, "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (result == DialogResult.Yes)
                    {
                        stopDraw();

                        enableAxisParameters(true);
                        buttonClear.Enabled = false;

                        numericupdownXMin.Value = (decimal)curveData.XVMin;
                        numericupdownXMax.Value = (decimal)curveData.XVMax;
                        numericupdownYMin.Value = (decimal)curveData.YVMin;
                        numericupdownYMax.Value = (decimal)curveData.YVMax;

                        updateAxisParams();
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
                // focus on x equation
                this.ActiveControl = textbox1Eq;
                textbox1Eq.Focus();
            }

            numericupdownPMin.Value = (decimal)curveData.PMin;
            numericupdownPMax.Value = (decimal)curveData.PMax;
            numericupdownPStep.Value = (decimal)curveData.PStep;

            numericupdownThickness.Value = (int)curveData.Thickness;

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

                string[] paramValue = line.Split('=');
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

                            default:
                                cd.Type = CurveType.PARAMETRIC;
                                break;
                        }
                        break;
                    
                    // equations
                    case "x(t)":
                        cd.XtEquation = value;
                        break;
                    case "y(t)":
                        cd.YtEquation = value;
                        break;
                    case "r(t)":
                        cd.RtEquation = value;
                        break;
                    case "y(x)":
                        cd.YxEquation = value;
                        break;
                    
                    // view
                    case "xv_min":
                        cd.XVMin = Convert.ToDouble(value, culture);
                        break;
                    case "xv_max":
                        cd.XVMax = Convert.ToDouble(value, culture);
                        break;
                    case "yv_min":
                        cd.YVMin = Convert.ToDouble(value, culture);
                        break;
                    case "yv_max":
                        cd.YVMax = Convert.ToDouble(value, culture);
                        break;
                    
                    // parametric and polar curves
                    case "t_min":
                        cd.PMin = Convert.ToDouble(value, culture);
                        break;
                    case "t_max":
                        cd.PMax = Convert.ToDouble(value, culture);
                        break;
                    case "t_step":
                        cd.PStep = Convert.ToDouble(value, culture);
                        break;

                    // cartesian curves
                    case "x_min":
                        cd.PMin = Convert.ToDouble(value, culture);
                        break;
                    case "x_max":
                        cd.PMax = Convert.ToDouble(value, culture);
                        break;
                    case "x_step":
                        cd.PStep = Convert.ToDouble(value, culture);
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
