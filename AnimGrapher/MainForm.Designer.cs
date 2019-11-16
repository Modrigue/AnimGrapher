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
            this.buttonCopyYEq = new System.Windows.Forms.Button();
            this.buttonCopyXEq = new System.Windows.Forms.Button();
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
            this.labelYEq = new System.Windows.Forms.Label();
            this.labelXEq = new System.Windows.Forms.Label();
            this.textboxYEq = new System.Windows.Forms.TextBox();
            this.textboxXEq = new System.Windows.Forms.TextBox();
            this.buttonDrawColor = new System.Windows.Forms.Button();
            this.labelInterval = new System.Windows.Forms.Label();
            this.numericupdownInterval = new System.Windows.Forms.NumericUpDown();
            this.labelYMinMax = new System.Windows.Forms.Label();
            this.numericupdownYMax = new System.Windows.Forms.NumericUpDown();
            this.labelXMinMax = new System.Windows.Forms.Label();
            this.numericupdownYMin = new System.Windows.Forms.NumericUpDown();
            this.numericupdownXMax = new System.Windows.Forms.NumericUpDown();
            this.numericupdownXMin = new System.Windows.Forms.NumericUpDown();
            this.labelTMinMax = new System.Windows.Forms.Label();
            this.numericupdownTMax = new System.Windows.Forms.NumericUpDown();
            this.numericupdownTMin = new System.Windows.Forms.NumericUpDown();
            this.labelThickness = new System.Windows.Forms.Label();
            this.numericupdownThickness = new System.Windows.Forms.NumericUpDown();
            this.labelTStep = new System.Windows.Forms.Label();
            this.numericupdownTStep = new System.Windows.Forms.NumericUpDown();
            this.buttonDraw = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.tooltip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.panelGraph.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureboxPencil)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureboxGraph)).BeginInit();
            this.panelControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericupdownInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericupdownYMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericupdownYMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericupdownXMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericupdownXMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericupdownTMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericupdownTMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericupdownThickness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericupdownTStep)).BeginInit();
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
            this.labelCoords.Text = "coords";
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
            this.pictureboxGraph.MouseLeave += new System.EventHandler(this.pictureboxGraph_MouseLeave);
            this.pictureboxGraph.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureboxGraph_MouseMove);
            // 
            // panelControl
            // 
            this.panelControl.Controls.Add(this.buttonCopyYEq);
            this.panelControl.Controls.Add(this.buttonCopyXEq);
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
            this.panelControl.Controls.Add(this.labelYEq);
            this.panelControl.Controls.Add(this.labelXEq);
            this.panelControl.Controls.Add(this.textboxYEq);
            this.panelControl.Controls.Add(this.textboxXEq);
            this.panelControl.Controls.Add(this.buttonDrawColor);
            this.panelControl.Controls.Add(this.labelInterval);
            this.panelControl.Controls.Add(this.numericupdownInterval);
            this.panelControl.Controls.Add(this.labelYMinMax);
            this.panelControl.Controls.Add(this.numericupdownYMax);
            this.panelControl.Controls.Add(this.labelXMinMax);
            this.panelControl.Controls.Add(this.numericupdownYMin);
            this.panelControl.Controls.Add(this.numericupdownXMax);
            this.panelControl.Controls.Add(this.numericupdownXMin);
            this.panelControl.Controls.Add(this.labelTMinMax);
            this.panelControl.Controls.Add(this.numericupdownTMax);
            this.panelControl.Controls.Add(this.numericupdownTMin);
            this.panelControl.Controls.Add(this.labelThickness);
            this.panelControl.Controls.Add(this.numericupdownThickness);
            this.panelControl.Controls.Add(this.labelTStep);
            this.panelControl.Controls.Add(this.numericupdownTStep);
            this.panelControl.Controls.Add(this.buttonDraw);
            this.panelControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl.Location = new System.Drawing.Point(0, 0);
            this.panelControl.Margin = new System.Windows.Forms.Padding(0);
            this.panelControl.Name = "panelControl";
            this.panelControl.Size = new System.Drawing.Size(984, 150);
            this.panelControl.TabIndex = 3;
            // 
            // buttonCopyYEq
            // 
            this.buttonCopyYEq.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonCopyYEq.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonCopyYEq.Enabled = false;
            this.buttonCopyYEq.Image = global::AnimGrapher.Properties.Resources.clipboard_16;
            this.buttonCopyYEq.Location = new System.Drawing.Point(765, 47);
            this.buttonCopyYEq.Name = "buttonCopyYEq";
            this.buttonCopyYEq.Size = new System.Drawing.Size(25, 25);
            this.buttonCopyYEq.TabIndex = 42;
            this.tooltip1.SetToolTip(this.buttonCopyYEq, "Copy y(t) equation to clipboard");
            this.buttonCopyYEq.UseVisualStyleBackColor = true;
            this.buttonCopyYEq.Click += new System.EventHandler(this.buttonCopyYEq_Click);
            // 
            // buttonCopyXEq
            // 
            this.buttonCopyXEq.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonCopyXEq.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonCopyXEq.Enabled = false;
            this.buttonCopyXEq.Image = global::AnimGrapher.Properties.Resources.clipboard_16;
            this.buttonCopyXEq.Location = new System.Drawing.Point(765, 21);
            this.buttonCopyXEq.Name = "buttonCopyXEq";
            this.buttonCopyXEq.Size = new System.Drawing.Size(25, 25);
            this.buttonCopyXEq.TabIndex = 41;
            this.tooltip1.SetToolTip(this.buttonCopyXEq, "Copy x(t) equation to clipboard");
            this.buttonCopyXEq.UseVisualStyleBackColor = true;
            this.buttonCopyXEq.Click += new System.EventHandler(this.buttonCopyXEq_Click);
            // 
            // buttonNewEquation
            // 
            this.buttonNewEquation.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.buttonNewEquation.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonNewEquation.Location = new System.Drawing.Point(170, 50);
            this.buttonNewEquation.Name = "buttonNewEquation";
            this.buttonNewEquation.Size = new System.Drawing.Size(59, 23);
            this.buttonNewEquation.TabIndex = 40;
            this.buttonNewEquation.TabStop = false;
            this.buttonNewEquation.Text = "New";
            this.tooltip1.SetToolTip(this.buttonNewEquation, "Create new equation");
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
            this.buttonDelete.Location = new System.Drawing.Point(141, 50);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(23, 23);
            this.buttonDelete.TabIndex = 38;
            this.buttonDelete.TabStop = false;
            this.tooltip1.SetToolTip(this.buttonDelete, "Delete equation");
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
            this.buttonSave.Location = new System.Drawing.Point(11, 50);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(125, 23);
            this.buttonSave.TabIndex = 36;
            this.buttonSave.TabStop = false;
            this.buttonSave.Text = "Save equation";
            this.tooltip1.SetToolTip(this.buttonSave, "Save equation");
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // comboboxCurves
            // 
            this.comboboxCurves.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.comboboxCurves.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboboxCurves.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboboxCurves.FormattingEnabled = true;
            this.comboboxCurves.Location = new System.Drawing.Point(12, 23);
            this.comboboxCurves.Name = "comboboxCurves";
            this.comboboxCurves.Size = new System.Drawing.Size(217, 21);
            this.comboboxCurves.TabIndex = 0;
            this.tooltip1.SetToolTip(this.comboboxCurves, "Graphical hints type (axis, grid or none)");
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
            this.tooltip1.SetToolTip(this.buttonExport, "Export image");
            this.buttonExport.UseVisualStyleBackColor = true;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonStop.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonStop.Enabled = false;
            this.buttonStop.Image = global::AnimGrapher.Properties.Resources.stop_32;
            this.buttonStop.Location = new System.Drawing.Point(459, 95);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(40, 40);
            this.buttonStop.TabIndex = 32;
            this.buttonStop.TabStop = false;
            this.tooltip1.SetToolTip(this.buttonStop, "Stop draw");
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // buttonNextStep
            // 
            this.buttonNextStep.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonNextStep.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonNextStep.Image = global::AnimGrapher.Properties.Resources.fast_forward_32;
            this.buttonNextStep.Location = new System.Drawing.Point(412, 95);
            this.buttonNextStep.Name = "buttonNextStep";
            this.buttonNextStep.Size = new System.Drawing.Size(40, 40);
            this.buttonNextStep.TabIndex = 31;
            this.buttonNextStep.TabStop = false;
            this.tooltip1.SetToolTip(this.buttonNextStep, "Next draw step");
            this.buttonNextStep.UseVisualStyleBackColor = true;
            this.buttonNextStep.Click += new System.EventHandler(this.buttonNextStep_Click);
            // 
            // buttonPausePlay
            // 
            this.buttonPausePlay.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonPausePlay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonPausePlay.Enabled = false;
            this.buttonPausePlay.Image = global::AnimGrapher.Properties.Resources.play_32;
            this.buttonPausePlay.Location = new System.Drawing.Point(366, 95);
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
            this.labelHints.Location = new System.Drawing.Point(187, 122);
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
            this.comboboxHints.Location = new System.Drawing.Point(231, 118);
            this.comboboxHints.Name = "comboboxHints";
            this.comboboxHints.Size = new System.Drawing.Size(54, 21);
            this.comboboxHints.TabIndex = 6;
            this.tooltip1.SetToolTip(this.comboboxHints, "Graphical hints type (axis, grid or none)");
            this.comboboxHints.SelectedIndexChanged += new System.EventHandler(this.comboboxHints_SelectedIndexChanged);
            // 
            // buttonClear
            // 
            this.buttonClear.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonClear.Enabled = false;
            this.buttonClear.Image = global::AnimGrapher.Properties.Resources.eraser_pixel_buddha_32;
            this.buttonClear.Location = new System.Drawing.Point(505, 95);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(40, 40);
            this.buttonClear.TabIndex = 27;
            this.buttonClear.TabStop = false;
            this.tooltip1.SetToolTip(this.buttonClear, "Erase all");
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // labelBackColor
            // 
            this.labelBackColor.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelBackColor.AutoSize = true;
            this.labelBackColor.Location = new System.Drawing.Point(599, 122);
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
            this.buttonBackColor.Location = new System.Drawing.Point(632, 113);
            this.buttonBackColor.Name = "buttonBackColor";
            this.buttonBackColor.Size = new System.Drawing.Size(30, 30);
            this.buttonBackColor.TabIndex = 25;
            this.buttonBackColor.TabStop = false;
            this.tooltip1.SetToolTip(this.buttonBackColor, "Background color");
            this.buttonBackColor.UseVisualStyleBackColor = false;
            this.buttonBackColor.Click += new System.EventHandler(this.buttonBackColor_Click);
            // 
            // labelDrawColor
            // 
            this.labelDrawColor.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelDrawColor.AutoSize = true;
            this.labelDrawColor.Location = new System.Drawing.Point(605, 90);
            this.labelDrawColor.Name = "labelDrawColor";
            this.labelDrawColor.Size = new System.Drawing.Size(26, 13);
            this.labelDrawColor.TabIndex = 24;
            this.labelDrawColor.Text = "Pen";
            // 
            // labelYEq
            // 
            this.labelYEq.AutoSize = true;
            this.labelYEq.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelYEq.Location = new System.Drawing.Point(255, 50);
            this.labelYEq.Name = "labelYEq";
            this.labelYEq.Size = new System.Drawing.Size(41, 17);
            this.labelYEq.TabIndex = 23;
            this.labelYEq.Text = "y(t) =";
            // 
            // labelXEq
            // 
            this.labelXEq.AutoSize = true;
            this.labelXEq.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelXEq.Location = new System.Drawing.Point(255, 24);
            this.labelXEq.Name = "labelXEq";
            this.labelXEq.Size = new System.Drawing.Size(41, 17);
            this.labelXEq.TabIndex = 22;
            this.labelXEq.Text = "x(t) =";
            // 
            // textboxYEq
            // 
            this.textboxYEq.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textboxYEq.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.textboxYEq.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textboxYEq.Location = new System.Drawing.Point(296, 47);
            this.textboxYEq.Name = "textboxYEq";
            this.textboxYEq.Size = new System.Drawing.Size(465, 25);
            this.textboxYEq.TabIndex = 8;
            this.textboxYEq.TextChanged += new System.EventHandler(this.textboxYEq_TextChanged);
            // 
            // textboxXEq
            // 
            this.textboxXEq.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textboxXEq.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.textboxXEq.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textboxXEq.Location = new System.Drawing.Point(296, 21);
            this.textboxXEq.Name = "textboxXEq";
            this.textboxXEq.Size = new System.Drawing.Size(465, 25);
            this.textboxXEq.TabIndex = 7;
            this.textboxXEq.TextChanged += new System.EventHandler(this.textboxXEq_TextChanged);
            // 
            // buttonDrawColor
            // 
            this.buttonDrawColor.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonDrawColor.BackColor = System.Drawing.Color.Black;
            this.buttonDrawColor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonDrawColor.FlatAppearance.BorderSize = 0;
            this.buttonDrawColor.Location = new System.Drawing.Point(632, 81);
            this.buttonDrawColor.Name = "buttonDrawColor";
            this.buttonDrawColor.Size = new System.Drawing.Size(30, 30);
            this.buttonDrawColor.TabIndex = 19;
            this.buttonDrawColor.TabStop = false;
            this.tooltip1.SetToolTip(this.buttonDrawColor, "Pen color");
            this.buttonDrawColor.UseVisualStyleBackColor = false;
            this.buttonDrawColor.Click += new System.EventHandler(this.buttonDrawColor_Click);
            // 
            // labelInterval
            // 
            this.labelInterval.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelInterval.AutoSize = true;
            this.labelInterval.Location = new System.Drawing.Point(187, 90);
            this.labelInterval.Name = "labelInterval";
            this.labelInterval.Size = new System.Drawing.Size(42, 13);
            this.labelInterval.TabIndex = 18;
            this.labelInterval.Text = "Interval";
            // 
            // numericupdownInterval
            // 
            this.numericupdownInterval.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.numericupdownInterval.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numericupdownInterval.Cursor = System.Windows.Forms.Cursors.Hand;
            this.numericupdownInterval.DecimalPlaces = 2;
            this.numericupdownInterval.Enabled = false;
            this.numericupdownInterval.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericupdownInterval.Location = new System.Drawing.Point(231, 88);
            this.numericupdownInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericupdownInterval.Name = "numericupdownInterval";
            this.numericupdownInterval.Size = new System.Drawing.Size(54, 20);
            this.numericupdownInterval.TabIndex = 5;
            this.tooltip1.SetToolTip(this.numericupdownInterval, "x,y graphical hints interval");
            this.numericupdownInterval.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numericupdownInterval.ValueChanged += new System.EventHandler(this.numericupdownInterval_ValueChanged);
            // 
            // labelYMinMax
            // 
            this.labelYMinMax.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelYMinMax.AutoSize = true;
            this.labelYMinMax.Location = new System.Drawing.Point(68, 122);
            this.labelYMinMax.Name = "labelYMinMax";
            this.labelYMinMax.Size = new System.Drawing.Size(39, 13);
            this.labelYMinMax.TabIndex = 16;
            this.labelYMinMax.Text = "<   y  <";
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
            this.tooltip1.SetToolTip(this.numericupdownYMax, "y maximum value");
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
            this.labelXMinMax.Size = new System.Drawing.Size(39, 13);
            this.labelXMinMax.TabIndex = 14;
            this.labelXMinMax.Text = "<   x  <";
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
            this.tooltip1.SetToolTip(this.numericupdownYMin, "y minimum value");
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
            this.tooltip1.SetToolTip(this.numericupdownXMax, "x maximum value");
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
            this.tooltip1.SetToolTip(this.numericupdownXMin, "x minimum value");
            this.numericupdownXMin.Value = new decimal(new int[] {
            2,
            0,
            0,
            -2147483648});
            this.numericupdownXMin.ValueChanged += new System.EventHandler(this.numericupdownXMin_ValueChanged);
            // 
            // labelTMinMax
            // 
            this.labelTMinMax.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelTMinMax.AutoSize = true;
            this.labelTMinMax.Location = new System.Drawing.Point(775, 90);
            this.labelTMinMax.Name = "labelTMinMax";
            this.labelTMinMax.Size = new System.Drawing.Size(37, 13);
            this.labelTMinMax.TabIndex = 8;
            this.labelTMinMax.Text = "<   t  <";
            // 
            // numericupdownTMax
            // 
            this.numericupdownTMax.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.numericupdownTMax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numericupdownTMax.Cursor = System.Windows.Forms.Cursors.Hand;
            this.numericupdownTMax.DecimalPlaces = 3;
            this.numericupdownTMax.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numericupdownTMax.Location = new System.Drawing.Point(819, 88);
            this.numericupdownTMax.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericupdownTMax.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.numericupdownTMax.Name = "numericupdownTMax";
            this.numericupdownTMax.Size = new System.Drawing.Size(54, 20);
            this.numericupdownTMax.TabIndex = 10;
            this.tooltip1.SetToolTip(this.numericupdownTMax, "t maximum value");
            this.numericupdownTMax.Value = new decimal(new int[] {
            6283,
            0,
            0,
            196608});
            this.numericupdownTMax.ValueChanged += new System.EventHandler(this.numericupdownTMax_ValueChanged);
            // 
            // numericupdownTMin
            // 
            this.numericupdownTMin.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.numericupdownTMin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numericupdownTMin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.numericupdownTMin.DecimalPlaces = 3;
            this.numericupdownTMin.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numericupdownTMin.Location = new System.Drawing.Point(713, 88);
            this.numericupdownTMin.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericupdownTMin.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.numericupdownTMin.Name = "numericupdownTMin";
            this.numericupdownTMin.Size = new System.Drawing.Size(54, 20);
            this.numericupdownTMin.TabIndex = 9;
            this.tooltip1.SetToolTip(this.numericupdownTMin, "t minimum value");
            this.numericupdownTMin.ValueChanged += new System.EventHandler(this.numericupdownTMin_ValueChanged);
            // 
            // labelThickness
            // 
            this.labelThickness.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelThickness.AutoSize = true;
            this.labelThickness.Location = new System.Drawing.Point(776, 122);
            this.labelThickness.Name = "labelThickness";
            this.labelThickness.Size = new System.Drawing.Size(56, 13);
            this.labelThickness.TabIndex = 4;
            this.labelThickness.Text = "Thickness";
            // 
            // numericupdownThickness
            // 
            this.numericupdownThickness.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.numericupdownThickness.Cursor = System.Windows.Forms.Cursors.Hand;
            this.numericupdownThickness.Location = new System.Drawing.Point(834, 120);
            this.numericupdownThickness.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericupdownThickness.Name = "numericupdownThickness";
            this.numericupdownThickness.Size = new System.Drawing.Size(39, 20);
            this.numericupdownThickness.TabIndex = 12;
            this.tooltip1.SetToolTip(this.numericupdownThickness, "Draw thickness");
            this.numericupdownThickness.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericupdownThickness.ValueChanged += new System.EventHandler(this.numericupdownThickness_ValueChanged);
            // 
            // labelTStep
            // 
            this.labelTStep.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelTStep.AutoSize = true;
            this.labelTStep.Location = new System.Drawing.Point(678, 122);
            this.labelTStep.Name = "labelTStep";
            this.labelTStep.Size = new System.Drawing.Size(33, 13);
            this.labelTStep.TabIndex = 2;
            this.labelTStep.Text = "t step";
            // 
            // numericupdownTStep
            // 
            this.numericupdownTStep.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.numericupdownTStep.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numericupdownTStep.Cursor = System.Windows.Forms.Cursors.Hand;
            this.numericupdownTStep.DecimalPlaces = 4;
            this.numericupdownTStep.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numericupdownTStep.Location = new System.Drawing.Point(713, 120);
            this.numericupdownTStep.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numericupdownTStep.Name = "numericupdownTStep";
            this.numericupdownTStep.Size = new System.Drawing.Size(54, 20);
            this.numericupdownTStep.TabIndex = 11;
            this.tooltip1.SetToolTip(this.numericupdownTStep, "t draw step");
            this.numericupdownTStep.Value = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericupdownTStep.ValueChanged += new System.EventHandler(this.numericupdownTStep_ValueChanged);
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
            this.tooltip1.SetToolTip(this.buttonDraw, "Draw curve");
            this.buttonDraw.UseVisualStyleBackColor = true;
            this.buttonDraw.Click += new System.EventHandler(this.buttonDraw_Click);
            // 
            // MainForm
            // 
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
            ((System.ComponentModel.ISupportInitialize)(this.numericupdownInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericupdownYMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericupdownYMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericupdownXMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericupdownXMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericupdownTMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericupdownTMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericupdownThickness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericupdownTStep)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureboxGraph;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panelControl;
        private System.Windows.Forms.Button buttonDraw;
        private System.Windows.Forms.NumericUpDown numericupdownTStep;
        private System.Windows.Forms.Label labelTStep;
        private System.Windows.Forms.NumericUpDown numericupdownThickness;
        private System.Windows.Forms.Label labelThickness;
        private System.Windows.Forms.Label labelTMinMax;
        private System.Windows.Forms.NumericUpDown numericupdownTMax;
        private System.Windows.Forms.NumericUpDown numericupdownTMin;
        private System.Windows.Forms.NumericUpDown numericupdownXMin;
        private System.Windows.Forms.NumericUpDown numericupdownXMax;
        private System.Windows.Forms.Label labelYMinMax;
        private System.Windows.Forms.NumericUpDown numericupdownYMax;
        private System.Windows.Forms.Label labelXMinMax;
        private System.Windows.Forms.NumericUpDown numericupdownYMin;
        private System.Windows.Forms.Panel panelGraph;
        private System.Windows.Forms.Label labelCoords;
        private System.Windows.Forms.PictureBox pictureboxPencil;
        private System.Windows.Forms.Label labelInterval;
        private System.Windows.Forms.NumericUpDown numericupdownInterval;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button buttonDrawColor;
        private System.Windows.Forms.TextBox textboxYEq;
        private System.Windows.Forms.TextBox textboxXEq;
        private System.Windows.Forms.Label labelYEq;
        private System.Windows.Forms.Label labelXEq;
        private System.Windows.Forms.Label labelDrawColor;
        private System.Windows.Forms.Label labelBackColor;
        private System.Windows.Forms.Button buttonBackColor;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Label labelHints;
        private System.Windows.Forms.ComboBox comboboxHints;
        private System.Windows.Forms.Button buttonPausePlay;
        private System.Windows.Forms.Button buttonNextStep;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.ToolTip tooltip1;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.Button buttonQuit;
        private System.Windows.Forms.ComboBox comboboxCurves;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonAbout;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Label labelEquation;
        private System.Windows.Forms.Button buttonNewEquation;
        private System.Windows.Forms.Button buttonCopyXEq;
        private System.Windows.Forms.Button buttonCopyYEq;
    }
}

