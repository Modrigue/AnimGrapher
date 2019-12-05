namespace AnimGrapher
{
    partial class MainForm
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelGraph = new System.Windows.Forms.Panel();
            this.pictureboxPencil = new System.Windows.Forms.PictureBox();
            this.labelCoords = new System.Windows.Forms.Label();
            this.pictureboxGraph = new System.Windows.Forms.PictureBox();
            this.panelControl = new System.Windows.Forms.Panel();
            this.labelDrawType = new System.Windows.Forms.Label();
            this.comboboxDrawType = new System.Windows.Forms.ComboBox();
            this.comboboxCurveType = new System.Windows.Forms.ComboBox();
            this.buttonCopy2Eq = new System.Windows.Forms.Button();
            this.buttonCopy1Eq = new System.Windows.Forms.Button();
            this.buttonNewEquation = new System.Windows.Forms.Button();
            this.labelEquation = new System.Windows.Forms.Label();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonAbout = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.comboboxCurves = new System.Windows.Forms.ComboBox();
            this.buttonQuit = new System.Windows.Forms.Button();
            this.buttonExport = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.buttonNextStep = new System.Windows.Forms.Button();
            this.buttonPausePlay = new System.Windows.Forms.Button();
            this.labelHints = new System.Windows.Forms.Label();
            this.comboboxHints = new System.Windows.Forms.ComboBox();
            this.buttonClear = new System.Windows.Forms.Button();
            this.labelBackColor = new System.Windows.Forms.Label();
            this.buttonBackColor = new System.Windows.Forms.Button();
            this.labelDrawColor = new System.Windows.Forms.Label();
            this.label2Eq = new System.Windows.Forms.Label();
            this.label1Eq = new System.Windows.Forms.Label();
            this.textbox2Eq = new System.Windows.Forms.TextBox();
            this.textbox1Eq = new System.Windows.Forms.TextBox();
            this.buttonDrawColor = new System.Windows.Forms.Button();
            this.labelUnit = new System.Windows.Forms.Label();
            this.numericupdownUnit = new System.Windows.Forms.NumericUpDown();
            this.labelYMinMax = new System.Windows.Forms.Label();
            this.numericupdownYMax = new System.Windows.Forms.NumericUpDown();
            this.labelXMinMax = new System.Windows.Forms.Label();
            this.numericupdownYMin = new System.Windows.Forms.NumericUpDown();
            this.numericupdownXMax = new System.Windows.Forms.NumericUpDown();
            this.numericupdownXMin = new System.Windows.Forms.NumericUpDown();
            this.labelPMinMax = new System.Windows.Forms.Label();
            this.numericupdownPMax = new System.Windows.Forms.NumericUpDown();
            this.numericupdownPMin = new System.Windows.Forms.NumericUpDown();
            this.labelThickness = new System.Windows.Forms.Label();
            this.numericupdownThickness = new System.Windows.Forms.NumericUpDown();
            this.labelPStep = new System.Windows.Forms.Label();
            this.numericupdownPStep = new System.Windows.Forms.NumericUpDown();
            this.buttonDraw = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.tooltip_ = new System.Windows.Forms.ToolTip(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.panelGraph.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureboxPencil)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureboxGraph)).BeginInit();
            this.panelControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericupdownUnit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericupdownYMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericupdownYMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericupdownXMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericupdownXMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericupdownPMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericupdownPMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericupdownThickness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericupdownPStep)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panelGraph, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panelControl, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(984, 650);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // panelGraph
            // 
            this.panelGraph.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.panelGraph.Controls.Add(this.pictureboxPencil);
            this.panelGraph.Controls.Add(this.labelCoords);
            this.panelGraph.Controls.Add(this.pictureboxGraph);
            this.panelGraph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGraph.Location = new System.Drawing.Point(0, 150);
            this.panelGraph.Margin = new System.Windows.Forms.Padding(0);
            this.panelGraph.Name = "panelGraph";
            this.panelGraph.Size = new System.Drawing.Size(984, 500);
            this.panelGraph.TabIndex = 17;
            this.panelGraph.Resize += new System.EventHandler(this.panelGraph_Resize);
            // 
            // pictureboxPencil
            // 
            this.pictureboxPencil.BackColor = System.Drawing.Color.Transparent;
            this.pictureboxPencil.Image = global::AnimGrapher.Properties.Resources.pencil_vertical_short_96;
            this.pictureboxPencil.Location = new System.Drawing.Point(0, 404);
            this.pictureboxPencil.Name = "pictureboxPencil";
            this.pictureboxPencil.Size = new System.Drawing.Size(20, 96);
            this.pictureboxPencil.TabIndex = 4;
            this.pictureboxPencil.TabStop = false;
            this.pictureboxPencil.Visible = false;
            // 
            // labelCoords
            // 
            this.labelCoords.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelCoords.BackColor = System.Drawing.Color.Transparent;
            this.labelCoords.ForeColor = System.Drawing.Color.DarkGray;
            this.labelCoords.Location = new System.Drawing.Point(888, 447);
            this.labelCoords.Name = "labelCoords";
            this.labelCoords.Size = new System.Drawing.Size(84, 41);
            this.labelCoords.TabIndex = 3;
            this.labelCoords.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // pictureboxGraph
            // 
            this.pictureboxGraph.BackColor = System.Drawing.Color.Transparent;
            this.pictureboxGraph.Cursor = System.Windows.Forms.Cursors.Cross;
            this.pictureboxGraph.Location = new System.Drawing.Point(0, 0);
            this.pictureboxGraph.Margin = new System.Windows.Forms.Padding(0);
            this.pictureboxGraph.Name = "pictureboxGraph";
            this.pictureboxGraph.Size = new System.Drawing.Size(984, 500);
            this.pictureboxGraph.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureboxGraph.TabIndex = 2;
            this.pictureboxGraph.TabStop = false;
            this.pictureboxGraph.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureboxGraph_MouseClick);
            this.pictureboxGraph.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pictureboxGraph_MouseDoubleClick);
            this.pictureboxGraph.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureboxGraph_MouseDown);
            this.pictureboxGraph.MouseLeave += new System.EventHandler(this.pictureboxGraph_MouseLeave);
            this.pictureboxGraph.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureboxGraph_MouseMove);
            this.pictureboxGraph.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureboxGraph_MouseUp);
            // 
            // panelControl
            // 
            this.panelControl.Controls.Add(this.labelDrawType);
            this.panelControl.Controls.Add(this.comboboxDrawType);
            this.panelControl.Controls.Add(this.comboboxCurveType);
            this.panelControl.Controls.Add(this.buttonCopy2Eq);
            this.panelControl.Controls.Add(this.buttonCopy1Eq);
            this.panelControl.Controls.Add(this.buttonNewEquation);
            this.panelControl.Controls.Add(this.labelEquation);
            this.panelControl.Controls.Add(this.buttonDelete);
            this.panelControl.Controls.Add(this.buttonAbout);
            this.panelControl.Controls.Add(this.buttonSave);
            this.panelControl.Controls.Add(this.comboboxCurves);
            this.panelControl.Controls.Add(this.buttonQuit);
            this.panelControl.Controls.Add(this.buttonExport);
            this.panelControl.Controls.Add(this.buttonStop);
            this.panelControl.Controls.Add(this.buttonNextStep);
            this.panelControl.Controls.Add(this.buttonPausePlay);
            this.panelControl.Controls.Add(this.labelHints);
            this.panelControl.Controls.Add(this.comboboxHints);
            this.panelControl.Controls.Add(this.buttonClear);
            this.panelControl.Controls.Add(this.labelBackColor);
            this.panelControl.Controls.Add(this.buttonBackColor);
            this.panelControl.Controls.Add(this.labelDrawColor);
            this.panelControl.Controls.Add(this.label2Eq);
            this.panelControl.Controls.Add(this.label1Eq);
            this.panelControl.Controls.Add(this.textbox2Eq);
            this.panelControl.Controls.Add(this.textbox1Eq);
            this.panelControl.Controls.Add(this.buttonDrawColor);
            this.panelControl.Controls.Add(this.labelUnit);
            this.panelControl.Controls.Add(this.numericupdownUnit);
            this.panelControl.Controls.Add(this.labelYMinMax);
            this.panelControl.Controls.Add(this.numericupdownYMax);
            this.panelControl.Controls.Add(this.labelXMinMax);
            this.panelControl.Controls.Add(this.numericupdownYMin);
            this.panelControl.Controls.Add(this.numericupdownXMax);
            this.panelControl.Controls.Add(this.numericupdownXMin);
            this.panelControl.Controls.Add(this.labelPMinMax);
            this.panelControl.Controls.Add(this.numericupdownPMax);
            this.panelControl.Controls.Add(this.numericupdownPMin);
            this.panelControl.Controls.Add(this.labelThickness);
            this.panelControl.Controls.Add(this.numericupdownThickness);
            this.panelControl.Controls.Add(this.labelPStep);
            this.panelControl.Controls.Add(this.numericupdownPStep);
            this.panelControl.Controls.Add(this.buttonDraw);
            this.panelControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl.Location = new System.Drawing.Point(0, 0);
            this.panelControl.Margin = new System.Windows.Forms.Padding(0);
            this.panelControl.Name = "panelControl";
            this.panelControl.Size = new System.Drawing.Size(984, 150);
            this.panelControl.TabIndex = 3;
            // 
            // labelDrawType
            // 
            this.labelDrawType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelDrawType.AutoSize = true;
            this.labelDrawType.Location = new System.Drawing.Point(280, 122);
            this.labelDrawType.Name = "labelDrawType";
            this.labelDrawType.Size = new System.Drawing.Size(32, 13);
            this.labelDrawType.TabIndex = 45;
            this.labelDrawType.Text = "Draw";
            // 
            // comboboxDrawType
            // 
            this.comboboxDrawType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.comboboxDrawType.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboboxDrawType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboboxDrawType.FormattingEnabled = true;
            this.comboboxDrawType.Items.AddRange(new object[] {
            "Line",
            "Dots",
            "Square"});
            this.comboboxDrawType.Location = new System.Drawing.Point(317, 118);
            this.comboboxDrawType.Name = "comboboxDrawType";
            this.comboboxDrawType.Size = new System.Drawing.Size(60, 21);
            this.comboboxDrawType.TabIndex = 44;
            this.tooltip_.SetToolTip(this.comboboxDrawType, "Drawing style (line or plot)");
            this.comboboxDrawType.SelectedIndexChanged += new System.EventHandler(this.comboboxDrawType_SelectedIndexChanged);
            // 
            // comboboxCurveType
            // 
            this.comboboxCurveType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.comboboxCurveType.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboboxCurveType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboboxCurveType.FormattingEnabled = true;
            this.comboboxCurveType.Items.AddRange(new object[] {
            "Parametric",
            "Polar",
            "Cartesian",
            "Inequality"});
            this.comboboxCurveType.Location = new System.Drawing.Point(149, 6);
            this.comboboxCurveType.Name = "comboboxCurveType";
            this.comboboxCurveType.Size = new System.Drawing.Size(80, 21);
            this.comboboxCurveType.TabIndex = 43;
            this.tooltip_.SetToolTip(this.comboboxCurveType, "Curve type");
            this.comboboxCurveType.SelectedIndexChanged += new System.EventHandler(this.comboboxCurveType_SelectedIndexChanged);
            // 
            // buttonCopy2Eq
            // 
            this.buttonCopy2Eq.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonCopy2Eq.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonCopy2Eq.Enabled = false;
            this.buttonCopy2Eq.Image = global::AnimGrapher.Properties.Resources.clipboard_16;
            this.buttonCopy2Eq.Location = new System.Drawing.Point(765, 56);
            this.buttonCopy2Eq.Name = "buttonCopy2Eq";
            this.buttonCopy2Eq.Size = new System.Drawing.Size(25, 25);
            this.buttonCopy2Eq.TabIndex = 42;
            this.tooltip_.SetToolTip(this.buttonCopy2Eq, "Copy equation to clipboard");
            this.buttonCopy2Eq.UseVisualStyleBackColor = true;
            this.buttonCopy2Eq.Click += new System.EventHandler(this.buttonCopy2Eq_Click);
            // 
            // buttonCopy1Eq
            // 
            this.buttonCopy1Eq.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonCopy1Eq.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonCopy1Eq.Enabled = false;
            this.buttonCopy1Eq.Image = global::AnimGrapher.Properties.Resources.clipboard_16;
            this.buttonCopy1Eq.Location = new System.Drawing.Point(765, 30);
            this.buttonCopy1Eq.Name = "buttonCopy1Eq";
            this.buttonCopy1Eq.Size = new System.Drawing.Size(25, 25);
            this.buttonCopy1Eq.TabIndex = 41;
            this.tooltip_.SetToolTip(this.buttonCopy1Eq, "Copy equation to clipboard");
            this.buttonCopy1Eq.UseVisualStyleBackColor = true;
            this.buttonCopy1Eq.Click += new System.EventHandler(this.buttonCopy1Eq_Click);
            // 
            // buttonNewEquation
            // 
            this.buttonNewEquation.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.buttonNewEquation.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonNewEquation.Location = new System.Drawing.Point(170, 59);
            this.buttonNewEquation.Name = "buttonNewEquation";
            this.buttonNewEquation.Size = new System.Drawing.Size(60, 23);
            this.buttonNewEquation.TabIndex = 40;
            this.buttonNewEquation.TabStop = false;
            this.buttonNewEquation.Text = "New";
            this.tooltip_.SetToolTip(this.buttonNewEquation, "Create new equation");
            this.buttonNewEquation.UseVisualStyleBackColor = true;
            this.buttonNewEquation.Click += new System.EventHandler(this.buttonNewEquation_Click);
            // 
            // labelEquation
            // 
            this.labelEquation.AutoSize = true;
            this.labelEquation.Location = new System.Drawing.Point(10, 10);
            this.labelEquation.Name = "labelEquation";
            this.labelEquation.Size = new System.Drawing.Size(99, 13);
            this.labelEquation.TabIndex = 39;
            this.labelEquation.Text = "Select an equation:";
            // 
            // buttonDelete
            // 
            this.buttonDelete.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.buttonDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonDelete.Image = global::AnimGrapher.Properties.Resources.trash_16;
            this.buttonDelete.Location = new System.Drawing.Point(141, 59);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(23, 23);
            this.buttonDelete.TabIndex = 38;
            this.buttonDelete.TabStop = false;
            this.tooltip_.SetToolTip(this.buttonDelete, "Delete equation");
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonAbout
            // 
            this.buttonAbout.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonAbout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonAbout.Location = new System.Drawing.Point(902, 86);
            this.buttonAbout.Name = "buttonAbout";
            this.buttonAbout.Size = new System.Drawing.Size(70, 23);
            this.buttonAbout.TabIndex = 37;
            this.buttonAbout.TabStop = false;
            this.buttonAbout.Text = "About...";
            this.buttonAbout.UseVisualStyleBackColor = true;
            this.buttonAbout.Click += new System.EventHandler(this.buttonAbout_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.buttonSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonSave.Enabled = false;
            this.buttonSave.Location = new System.Drawing.Point(11, 59);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(125, 23);
            this.buttonSave.TabIndex = 36;
            this.buttonSave.TabStop = false;
            this.buttonSave.Text = "Save equation";
            this.tooltip_.SetToolTip(this.buttonSave, "Save equation");
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // comboboxCurves
            // 
            this.comboboxCurves.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.comboboxCurves.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboboxCurves.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboboxCurves.FormattingEnabled = true;
            this.comboboxCurves.Location = new System.Drawing.Point(12, 32);
            this.comboboxCurves.Name = "comboboxCurves";
            this.comboboxCurves.Size = new System.Drawing.Size(217, 21);
            this.comboboxCurves.TabIndex = 0;
            this.tooltip_.SetToolTip(this.comboboxCurves, "Graphical hints type (axis, grid or none)");
            this.comboboxCurves.SelectedIndexChanged += new System.EventHandler(this.comboboxCurves_SelectedIndexChanged);
            // 
            // buttonQuit
            // 
            this.buttonQuit.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonQuit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonQuit.Location = new System.Drawing.Point(902, 117);
            this.buttonQuit.Name = "buttonQuit";
            this.buttonQuit.Size = new System.Drawing.Size(70, 23);
            this.buttonQuit.TabIndex = 34;
            this.buttonQuit.TabStop = false;
            this.buttonQuit.Text = "Quit";
            this.buttonQuit.UseVisualStyleBackColor = true;
            this.buttonQuit.Click += new System.EventHandler(this.buttonQuit_Click);
            // 
            // buttonExport
            // 
            this.buttonExport.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonExport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonExport.Enabled = false;
            this.buttonExport.Image = global::AnimGrapher.Properties.Resources.save_64;
            this.buttonExport.Location = new System.Drawing.Point(902, 12);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(70, 70);
            this.buttonExport.TabIndex = 33;
            this.buttonExport.TabStop = false;
            this.tooltip_.SetToolTip(this.buttonExport, "Export image");
            this.buttonExport.UseVisualStyleBackColor = true;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonStop.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonStop.Enabled = false;
            this.buttonStop.Image = global::AnimGrapher.Properties.Resources.stop_32;
            this.buttonStop.Location = new System.Drawing.Point(502, 100);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(40, 40);
            this.buttonStop.TabIndex = 32;
            this.buttonStop.TabStop = false;
            this.tooltip_.SetToolTip(this.buttonStop, "Stop draw");
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // buttonNextStep
            // 
            this.buttonNextStep.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonNextStep.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonNextStep.Image = global::AnimGrapher.Properties.Resources.fast_forward_32;
            this.buttonNextStep.Location = new System.Drawing.Point(455, 100);
            this.buttonNextStep.Name = "buttonNextStep";
            this.buttonNextStep.Size = new System.Drawing.Size(40, 40);
            this.buttonNextStep.TabIndex = 31;
            this.buttonNextStep.TabStop = false;
            this.tooltip_.SetToolTip(this.buttonNextStep, "Next draw step");
            this.buttonNextStep.UseVisualStyleBackColor = true;
            this.buttonNextStep.Click += new System.EventHandler(this.buttonNextStep_Click);
            // 
            // buttonPausePlay
            // 
            this.buttonPausePlay.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonPausePlay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonPausePlay.Enabled = false;
            this.buttonPausePlay.Image = global::AnimGrapher.Properties.Resources.play_32;
            this.buttonPausePlay.Location = new System.Drawing.Point(409, 100);
            this.buttonPausePlay.Name = "buttonPausePlay";
            this.buttonPausePlay.Size = new System.Drawing.Size(40, 40);
            this.buttonPausePlay.TabIndex = 30;
            this.buttonPausePlay.TabStop = false;
            this.buttonPausePlay.UseVisualStyleBackColor = true;
            this.buttonPausePlay.Click += new System.EventHandler(this.buttonPausePlay_Click);
            // 
            // labelHints
            // 
            this.labelHints.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelHints.AutoSize = true;
            this.labelHints.Location = new System.Drawing.Point(189, 122);
            this.labelHints.Name = "labelHints";
            this.labelHints.Size = new System.Drawing.Size(31, 13);
            this.labelHints.TabIndex = 29;
            this.labelHints.Text = "Hints";
            // 
            // comboboxHints
            // 
            this.comboboxHints.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.comboboxHints.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboboxHints.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboboxHints.FormattingEnabled = true;
            this.comboboxHints.Items.AddRange(new object[] {
            "None",
            "Axis",
            "Grid"});
            this.comboboxHints.Location = new System.Drawing.Point(223, 118);
            this.comboboxHints.Name = "comboboxHints";
            this.comboboxHints.Size = new System.Drawing.Size(54, 21);
            this.comboboxHints.TabIndex = 6;
            this.tooltip_.SetToolTip(this.comboboxHints, "Graphical hints type (axis, grid or none)");
            this.comboboxHints.SelectedIndexChanged += new System.EventHandler(this.comboboxHints_SelectedIndexChanged);
            // 
            // buttonClear
            // 
            this.buttonClear.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonClear.Enabled = false;
            this.buttonClear.Image = global::AnimGrapher.Properties.Resources.eraser_pixel_buddha_32;
            this.buttonClear.Location = new System.Drawing.Point(548, 100);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(40, 40);
            this.buttonClear.TabIndex = 27;
            this.buttonClear.TabStop = false;
            this.tooltip_.SetToolTip(this.buttonClear, "Erase all");
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // labelBackColor
            // 
            this.labelBackColor.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelBackColor.AutoSize = true;
            this.labelBackColor.Location = new System.Drawing.Point(614, 122);
            this.labelBackColor.Name = "labelBackColor";
            this.labelBackColor.Size = new System.Drawing.Size(32, 13);
            this.labelBackColor.TabIndex = 26;
            this.labelBackColor.Text = "Back";
            // 
            // buttonBackColor
            // 
            this.buttonBackColor.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonBackColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.buttonBackColor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonBackColor.FlatAppearance.BorderSize = 0;
            this.buttonBackColor.Location = new System.Drawing.Point(647, 113);
            this.buttonBackColor.Name = "buttonBackColor";
            this.buttonBackColor.Size = new System.Drawing.Size(30, 30);
            this.buttonBackColor.TabIndex = 25;
            this.buttonBackColor.TabStop = false;
            this.tooltip_.SetToolTip(this.buttonBackColor, "Background color");
            this.buttonBackColor.UseVisualStyleBackColor = false;
            this.buttonBackColor.Click += new System.EventHandler(this.buttonBackColor_Click);
            // 
            // labelDrawColor
            // 
            this.labelDrawColor.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelDrawColor.AutoSize = true;
            this.labelDrawColor.Location = new System.Drawing.Point(620, 90);
            this.labelDrawColor.Name = "labelDrawColor";
            this.labelDrawColor.Size = new System.Drawing.Size(26, 13);
            this.labelDrawColor.TabIndex = 24;
            this.labelDrawColor.Text = "Pen";
            // 
            // label2Eq
            // 
            this.label2Eq.AutoSize = true;
            this.label2Eq.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2Eq.Location = new System.Drawing.Point(250, 59);
            this.label2Eq.Name = "label2Eq";
            this.label2Eq.Size = new System.Drawing.Size(41, 17);
            this.label2Eq.TabIndex = 23;
            this.label2Eq.Text = "y(t) =";
            // 
            // label1Eq
            // 
            this.label1Eq.AutoSize = true;
            this.label1Eq.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1Eq.Location = new System.Drawing.Point(250, 33);
            this.label1Eq.Name = "label1Eq";
            this.label1Eq.Size = new System.Drawing.Size(41, 17);
            this.label1Eq.TabIndex = 22;
            this.label1Eq.Text = "x(t) =";
            // 
            // textbox2Eq
            // 
            this.textbox2Eq.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textbox2Eq.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.textbox2Eq.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textbox2Eq.Location = new System.Drawing.Point(296, 56);
            this.textbox2Eq.Name = "textbox2Eq";
            this.textbox2Eq.Size = new System.Drawing.Size(465, 25);
            this.textbox2Eq.TabIndex = 8;
            this.textbox2Eq.TextChanged += new System.EventHandler(this.textbox2Eq_TextChanged);
            // 
            // textbox1Eq
            // 
            this.textbox1Eq.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textbox1Eq.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.textbox1Eq.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textbox1Eq.Location = new System.Drawing.Point(296, 30);
            this.textbox1Eq.Name = "textbox1Eq";
            this.textbox1Eq.Size = new System.Drawing.Size(465, 25);
            this.textbox1Eq.TabIndex = 7;
            this.textbox1Eq.TextChanged += new System.EventHandler(this.textbox1Eq_TextChanged);
            // 
            // buttonDrawColor
            // 
            this.buttonDrawColor.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonDrawColor.BackColor = System.Drawing.Color.Black;
            this.buttonDrawColor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonDrawColor.FlatAppearance.BorderSize = 0;
            this.buttonDrawColor.Location = new System.Drawing.Point(647, 81);
            this.buttonDrawColor.Name = "buttonDrawColor";
            this.buttonDrawColor.Size = new System.Drawing.Size(30, 30);
            this.buttonDrawColor.TabIndex = 19;
            this.buttonDrawColor.TabStop = false;
            this.tooltip_.SetToolTip(this.buttonDrawColor, "Pen color");
            this.buttonDrawColor.UseVisualStyleBackColor = false;
            this.buttonDrawColor.Click += new System.EventHandler(this.buttonDrawColor_Click);
            // 
            // labelUnit
            // 
            this.labelUnit.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelUnit.AutoSize = true;
            this.labelUnit.Location = new System.Drawing.Point(189, 90);
            this.labelUnit.Name = "labelUnit";
            this.labelUnit.Size = new System.Drawing.Size(26, 13);
            this.labelUnit.TabIndex = 18;
            this.labelUnit.Text = "Unit";
            // 
            // numericupdownUnit
            // 
            this.numericupdownUnit.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.numericupdownUnit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numericupdownUnit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.numericupdownUnit.DecimalPlaces = 2;
            this.numericupdownUnit.Enabled = false;
            this.numericupdownUnit.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericupdownUnit.Location = new System.Drawing.Point(223, 88);
            this.numericupdownUnit.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericupdownUnit.Name = "numericupdownUnit";
            this.numericupdownUnit.Size = new System.Drawing.Size(54, 20);
            this.numericupdownUnit.TabIndex = 5;
            this.tooltip_.SetToolTip(this.numericupdownUnit, "x,y graphical hints interval");
            this.numericupdownUnit.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numericupdownUnit.ValueChanged += new System.EventHandler(this.numericupdownUnit_ValueChanged);
            // 
            // labelYMinMax
            // 
            this.labelYMinMax.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelYMinMax.AutoSize = true;
            this.labelYMinMax.Location = new System.Drawing.Point(68, 122);
            this.labelYMinMax.Name = "labelYMinMax";
            this.labelYMinMax.Size = new System.Drawing.Size(42, 13);
            this.labelYMinMax.TabIndex = 16;
            this.labelYMinMax.Text = "<  yv  <";
            // 
            // numericupdownYMax
            // 
            this.numericupdownYMax.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.numericupdownYMax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numericupdownYMax.Cursor = System.Windows.Forms.Cursors.Hand;
            this.numericupdownYMax.DecimalPlaces = 2;
            this.numericupdownYMax.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericupdownYMax.Location = new System.Drawing.Point(110, 120);
            this.numericupdownYMax.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericupdownYMax.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.numericupdownYMax.Name = "numericupdownYMax";
            this.numericupdownYMax.Size = new System.Drawing.Size(54, 20);
            this.numericupdownYMax.TabIndex = 4;
            this.tooltip_.SetToolTip(this.numericupdownYMax, "y view maximum value");
            this.numericupdownYMax.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericupdownYMax.ValueChanged += new System.EventHandler(this.numericupdownYMax_ValueChanged);
            // 
            // labelXMinMax
            // 
            this.labelXMinMax.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelXMinMax.AutoSize = true;
            this.labelXMinMax.Location = new System.Drawing.Point(68, 90);
            this.labelXMinMax.Name = "labelXMinMax";
            this.labelXMinMax.Size = new System.Drawing.Size(42, 13);
            this.labelXMinMax.TabIndex = 14;
            this.labelXMinMax.Text = "<  xv  <";
            // 
            // numericupdownYMin
            // 
            this.numericupdownYMin.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.numericupdownYMin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numericupdownYMin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.numericupdownYMin.DecimalPlaces = 2;
            this.numericupdownYMin.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericupdownYMin.Location = new System.Drawing.Point(12, 119);
            this.numericupdownYMin.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericupdownYMin.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.numericupdownYMin.Name = "numericupdownYMin";
            this.numericupdownYMin.Size = new System.Drawing.Size(54, 20);
            this.numericupdownYMin.TabIndex = 3;
            this.tooltip_.SetToolTip(this.numericupdownYMin, "y view minimum value");
            this.numericupdownYMin.Value = new decimal(new int[] {
            2,
            0,
            0,
            -2147483648});
            this.numericupdownYMin.ValueChanged += new System.EventHandler(this.numericupdownYMin_ValueChanged);
            // 
            // numericupdownXMax
            // 
            this.numericupdownXMax.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.numericupdownXMax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numericupdownXMax.Cursor = System.Windows.Forms.Cursors.Hand;
            this.numericupdownXMax.DecimalPlaces = 2;
            this.numericupdownXMax.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericupdownXMax.Location = new System.Drawing.Point(110, 88);
            this.numericupdownXMax.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericupdownXMax.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.numericupdownXMax.Name = "numericupdownXMax";
            this.numericupdownXMax.Size = new System.Drawing.Size(54, 20);
            this.numericupdownXMax.TabIndex = 2;
            this.tooltip_.SetToolTip(this.numericupdownXMax, "x view maximum value");
            this.numericupdownXMax.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericupdownXMax.ValueChanged += new System.EventHandler(this.numericupdownXMax_ValueChanged);
            // 
            // numericupdownXMin
            // 
            this.numericupdownXMin.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.numericupdownXMin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numericupdownXMin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.numericupdownXMin.DecimalPlaces = 2;
            this.numericupdownXMin.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericupdownXMin.Location = new System.Drawing.Point(12, 88);
            this.numericupdownXMin.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericupdownXMin.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.numericupdownXMin.Name = "numericupdownXMin";
            this.numericupdownXMin.Size = new System.Drawing.Size(54, 20);
            this.numericupdownXMin.TabIndex = 1;
            this.tooltip_.SetToolTip(this.numericupdownXMin, "x view minimum value");
            this.numericupdownXMin.Value = new decimal(new int[] {
            2,
            0,
            0,
            -2147483648});
            this.numericupdownXMin.ValueChanged += new System.EventHandler(this.numericupdownXMin_ValueChanged);
            // 
            // labelPMinMax
            // 
            this.labelPMinMax.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelPMinMax.AutoSize = true;
            this.labelPMinMax.Location = new System.Drawing.Point(789, 90);
            this.labelPMinMax.Name = "labelPMinMax";
            this.labelPMinMax.Size = new System.Drawing.Size(37, 13);
            this.labelPMinMax.TabIndex = 8;
            this.labelPMinMax.Text = "<   t  <";
            // 
            // numericupdownPMax
            // 
            this.numericupdownPMax.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.numericupdownPMax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numericupdownPMax.Cursor = System.Windows.Forms.Cursors.Hand;
            this.numericupdownPMax.DecimalPlaces = 3;
            this.numericupdownPMax.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numericupdownPMax.Location = new System.Drawing.Point(832, 88);
            this.numericupdownPMax.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericupdownPMax.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.numericupdownPMax.Name = "numericupdownPMax";
            this.numericupdownPMax.Size = new System.Drawing.Size(54, 20);
            this.numericupdownPMax.TabIndex = 10;
            this.tooltip_.SetToolTip(this.numericupdownPMax, "t maximum value");
            this.numericupdownPMax.Value = new decimal(new int[] {
            6283,
            0,
            0,
            196608});
            this.numericupdownPMax.ValueChanged += new System.EventHandler(this.numericupdownPMax_ValueChanged);
            // 
            // numericupdownPMin
            // 
            this.numericupdownPMin.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.numericupdownPMin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numericupdownPMin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.numericupdownPMin.DecimalPlaces = 3;
            this.numericupdownPMin.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numericupdownPMin.Location = new System.Drawing.Point(726, 88);
            this.numericupdownPMin.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericupdownPMin.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.numericupdownPMin.Name = "numericupdownPMin";
            this.numericupdownPMin.Size = new System.Drawing.Size(54, 20);
            this.numericupdownPMin.TabIndex = 9;
            this.tooltip_.SetToolTip(this.numericupdownPMin, "t minimum value");
            this.numericupdownPMin.ValueChanged += new System.EventHandler(this.numericupdownPMin_ValueChanged);
            // 
            // labelThickness
            // 
            this.labelThickness.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelThickness.AutoSize = true;
            this.labelThickness.Location = new System.Drawing.Point(789, 122);
            this.labelThickness.Name = "labelThickness";
            this.labelThickness.Size = new System.Drawing.Size(56, 13);
            this.labelThickness.TabIndex = 4;
            this.labelThickness.Text = "Thickness";
            // 
            // numericupdownThickness
            // 
            this.numericupdownThickness.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.numericupdownThickness.Cursor = System.Windows.Forms.Cursors.Hand;
            this.numericupdownThickness.Location = new System.Drawing.Point(847, 120);
            this.numericupdownThickness.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericupdownThickness.Name = "numericupdownThickness";
            this.numericupdownThickness.Size = new System.Drawing.Size(39, 20);
            this.numericupdownThickness.TabIndex = 12;
            this.tooltip_.SetToolTip(this.numericupdownThickness, "Draw thickness");
            this.numericupdownThickness.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericupdownThickness.ValueChanged += new System.EventHandler(this.numericupdownThickness_ValueChanged);
            // 
            // labelPStep
            // 
            this.labelPStep.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelPStep.AutoSize = true;
            this.labelPStep.Location = new System.Drawing.Point(682, 122);
            this.labelPStep.Name = "labelPStep";
            this.labelPStep.Size = new System.Drawing.Size(33, 13);
            this.labelPStep.TabIndex = 2;
            this.labelPStep.Text = "t step";
            // 
            // numericupdownPStep
            // 
            this.numericupdownPStep.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.numericupdownPStep.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numericupdownPStep.Cursor = System.Windows.Forms.Cursors.Hand;
            this.numericupdownPStep.DecimalPlaces = 4;
            this.numericupdownPStep.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numericupdownPStep.Location = new System.Drawing.Point(726, 120);
            this.numericupdownPStep.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numericupdownPStep.Name = "numericupdownPStep";
            this.numericupdownPStep.Size = new System.Drawing.Size(54, 20);
            this.numericupdownPStep.TabIndex = 11;
            this.tooltip_.SetToolTip(this.numericupdownPStep, "t draw step");
            this.numericupdownPStep.Value = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericupdownPStep.ValueChanged += new System.EventHandler(this.numericupdownPStep_ValueChanged);
            // 
            // buttonDraw
            // 
            this.buttonDraw.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonDraw.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonDraw.Enabled = false;
            this.buttonDraw.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDraw.Image = global::AnimGrapher.Properties.Resources.pencil_64;
            this.buttonDraw.Location = new System.Drawing.Point(818, 12);
            this.buttonDraw.Name = "buttonDraw";
            this.buttonDraw.Size = new System.Drawing.Size(70, 70);
            this.buttonDraw.TabIndex = 0;
            this.buttonDraw.TabStop = false;
            this.tooltip_.SetToolTip(this.buttonDraw, "Draw curve");
            this.buttonDraw.UseVisualStyleBackColor = true;
            this.buttonDraw.Click += new System.EventHandler(this.buttonDraw_Click);
            // 
            // MainForm
            // 
            this.AcceptButton = this.buttonDraw;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.ClientSize = new System.Drawing.Size(984, 650);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1000, 689);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Anim Grapher";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panelGraph.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureboxPencil)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureboxGraph)).EndInit();
            this.panelControl.ResumeLayout(false);
            this.panelControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericupdownUnit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericupdownYMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericupdownYMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericupdownXMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericupdownXMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericupdownPMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericupdownPMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericupdownThickness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericupdownPStep)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureboxGraph;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panelControl;
        private System.Windows.Forms.Button buttonDraw;
        private System.Windows.Forms.NumericUpDown numericupdownPStep;
        private System.Windows.Forms.Label labelPStep;
        private System.Windows.Forms.NumericUpDown numericupdownThickness;
        private System.Windows.Forms.Label labelThickness;
        private System.Windows.Forms.Label labelPMinMax;
        private System.Windows.Forms.NumericUpDown numericupdownPMax;
        private System.Windows.Forms.NumericUpDown numericupdownPMin;
        private System.Windows.Forms.NumericUpDown numericupdownXMin;
        private System.Windows.Forms.NumericUpDown numericupdownXMax;
        private System.Windows.Forms.Label labelYMinMax;
        private System.Windows.Forms.NumericUpDown numericupdownYMax;
        private System.Windows.Forms.Label labelXMinMax;
        private System.Windows.Forms.NumericUpDown numericupdownYMin;
        private System.Windows.Forms.Panel panelGraph;
        private System.Windows.Forms.Label labelCoords;
        private System.Windows.Forms.PictureBox pictureboxPencil;
        private System.Windows.Forms.Label labelUnit;
        private System.Windows.Forms.NumericUpDown numericupdownUnit;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button buttonDrawColor;
        private System.Windows.Forms.TextBox textbox2Eq;
        private System.Windows.Forms.TextBox textbox1Eq;
        private System.Windows.Forms.Label label2Eq;
        private System.Windows.Forms.Label label1Eq;
        private System.Windows.Forms.Label labelDrawColor;
        private System.Windows.Forms.Label labelBackColor;
        private System.Windows.Forms.Button buttonBackColor;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Label labelHints;
        private System.Windows.Forms.ComboBox comboboxHints;
        private System.Windows.Forms.Button buttonPausePlay;
        private System.Windows.Forms.Button buttonNextStep;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.ToolTip tooltip_;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.Button buttonQuit;
        private System.Windows.Forms.ComboBox comboboxCurves;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonAbout;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Label labelEquation;
        private System.Windows.Forms.Button buttonNewEquation;
        private System.Windows.Forms.Button buttonCopy1Eq;
        private System.Windows.Forms.Button buttonCopy2Eq;
        private System.Windows.Forms.ComboBox comboboxCurveType;
        private System.Windows.Forms.Label labelDrawType;
        private System.Windows.Forms.ComboBox comboboxDrawType;
    }
}

