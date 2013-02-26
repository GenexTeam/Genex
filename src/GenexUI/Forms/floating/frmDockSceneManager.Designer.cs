namespace GenexUI.forms.floating
{
    partial class frmDockSceneManager
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
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
            this.tvwSceneList = new System.Windows.Forms.TreeView();
            this.ctmSceneList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctmSceneList_Open = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.ctmSceneList_Layer = new System.Windows.Forms.ToolStripMenuItem();
            this.可视层ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.障碍层ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.遮挡层ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ctmSceneList_DesignMode = new System.Windows.Forms.ToolStripMenuItem();
            this.ctmSceneList_CodeMode = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.ctmSceneList_Cut = new System.Windows.Forms.ToolStripMenuItem();
            this.ctmSceneList_Copy = new System.Windows.Forms.ToolStripMenuItem();
            this.ctmSceneList_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.ctmSceneList_Rename = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.ctmSceneList_OpenDir = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.ctmSceneList_Property = new System.Windows.Forms.ToolStripMenuItem();
            this.ctmSceneList.SuspendLayout();
            this.SuspendLayout();
            // 
            // tvwSceneList
            // 
            this.tvwSceneList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvwSceneList.ContextMenuStrip = this.ctmSceneList;
            this.tvwSceneList.FullRowSelect = true;
            this.tvwSceneList.HideSelection = false;
            this.tvwSceneList.LabelEdit = true;
            this.tvwSceneList.Location = new System.Drawing.Point(5, 29);
            this.tvwSceneList.Name = "tvwSceneList";
            this.tvwSceneList.PathSeparator = "/";
            this.tvwSceneList.ShowNodeToolTips = true;
            this.tvwSceneList.Size = new System.Drawing.Size(233, 356);
            this.tvwSceneList.TabIndex = 0;
            this.tvwSceneList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvwSceneList_MouseDown);
            // 
            // ctmSceneList
            // 
            this.ctmSceneList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctmSceneList_Open,
            this.toolStripMenuItem1,
            this.ctmSceneList_Layer,
            this.ctmSceneList_DesignMode,
            this.ctmSceneList_CodeMode,
            this.toolStripMenuItem2,
            this.ctmSceneList_Cut,
            this.ctmSceneList_Copy,
            this.ctmSceneList_Delete,
            this.ctmSceneList_Rename,
            this.toolStripMenuItem4,
            this.ctmSceneList_OpenDir,
            this.toolStripMenuItem3,
            this.ctmSceneList_Property});
            this.ctmSceneList.Name = "ctmSceneList";
            this.ctmSceneList.Size = new System.Drawing.Size(221, 248);
            // 
            // ctmSceneList_Open
            // 
            this.ctmSceneList_Open.Name = "ctmSceneList_Open";
            this.ctmSceneList_Open.Size = new System.Drawing.Size(220, 22);
            this.ctmSceneList_Open.Text = "打开(&O)";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(217, 6);
            // 
            // ctmSceneList_Layer
            // 
            this.ctmSceneList_Layer.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.可视层ToolStripMenuItem,
            this.障碍层ToolStripMenuItem,
            this.遮挡层ToolStripMenuItem});
            this.ctmSceneList_Layer.Name = "ctmSceneList_Layer";
            this.ctmSceneList_Layer.Size = new System.Drawing.Size(220, 22);
            this.ctmSceneList_Layer.Text = "图层(&L)";
            // 
            // 可视层ToolStripMenuItem
            // 
            this.可视层ToolStripMenuItem.Name = "可视层ToolStripMenuItem";
            this.可视层ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.可视层ToolStripMenuItem.Text = "可视层(&V)";
            // 
            // 障碍层ToolStripMenuItem
            // 
            this.障碍层ToolStripMenuItem.Name = "障碍层ToolStripMenuItem";
            this.障碍层ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.障碍层ToolStripMenuItem.Text = "障碍层(&B)";
            // 
            // 遮挡层ToolStripMenuItem
            // 
            this.遮挡层ToolStripMenuItem.Name = "遮挡层ToolStripMenuItem";
            this.遮挡层ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.遮挡层ToolStripMenuItem.Text = "遮挡层(&D)";
            // 
            // ctmSceneList_DesignMode
            // 
            this.ctmSceneList_DesignMode.Name = "ctmSceneList_DesignMode";
            this.ctmSceneList_DesignMode.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.D)));
            this.ctmSceneList_DesignMode.Size = new System.Drawing.Size(220, 22);
            this.ctmSceneList_DesignMode.Text = "设计视图(&Y)";
            // 
            // ctmSceneList_CodeMode
            // 
            this.ctmSceneList_CodeMode.Name = "ctmSceneList_CodeMode";
            this.ctmSceneList_CodeMode.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.C)));
            this.ctmSceneList_CodeMode.Size = new System.Drawing.Size(220, 22);
            this.ctmSceneList_CodeMode.Text = "代码视图(&G)";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(217, 6);
            // 
            // ctmSceneList_Cut
            // 
            this.ctmSceneList_Cut.Name = "ctmSceneList_Cut";
            this.ctmSceneList_Cut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.ctmSceneList_Cut.Size = new System.Drawing.Size(220, 22);
            this.ctmSceneList_Cut.Text = "剪切(&T)";
            this.ctmSceneList_Cut.Click += new System.EventHandler(this.ctmSceneList_Cut_Click);
            // 
            // ctmSceneList_Copy
            // 
            this.ctmSceneList_Copy.Name = "ctmSceneList_Copy";
            this.ctmSceneList_Copy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.ctmSceneList_Copy.Size = new System.Drawing.Size(220, 22);
            this.ctmSceneList_Copy.Text = "复制(&C)";
            // 
            // ctmSceneList_Delete
            // 
            this.ctmSceneList_Delete.Name = "ctmSceneList_Delete";
            this.ctmSceneList_Delete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.ctmSceneList_Delete.Size = new System.Drawing.Size(220, 22);
            this.ctmSceneList_Delete.Text = "删除(&D)";
            // 
            // ctmSceneList_Rename
            // 
            this.ctmSceneList_Rename.Name = "ctmSceneList_Rename";
            this.ctmSceneList_Rename.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.ctmSceneList_Rename.Size = new System.Drawing.Size(220, 22);
            this.ctmSceneList_Rename.Text = "重命名(&M)";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(217, 6);
            // 
            // ctmSceneList_OpenDir
            // 
            this.ctmSceneList_OpenDir.Name = "ctmSceneList_OpenDir";
            this.ctmSceneList_OpenDir.Size = new System.Drawing.Size(220, 22);
            this.ctmSceneList_OpenDir.Text = "打开目标文件夹(&X)";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(217, 6);
            // 
            // ctmSceneList_Property
            // 
            this.ctmSceneList_Property.Name = "ctmSceneList_Property";
            this.ctmSceneList_Property.ShortcutKeys = System.Windows.Forms.Keys.F12;
            this.ctmSceneList_Property.Size = new System.Drawing.Size(220, 22);
            this.ctmSceneList_Property.Text = "属性(&R)";
            // 
            // frmDockSceneManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(244, 392);
            this.Controls.Add(this.tvwSceneList);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmDockSceneManager";
            this.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.DockLeft;
            this.Text = "场景管理器";
            this.Load += new System.EventHandler(this.frmDockSceneExplorer_Load);
            this.ctmSceneList.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tvwSceneList;
        private System.Windows.Forms.ContextMenuStrip ctmSceneList;
        private System.Windows.Forms.ToolStripMenuItem ctmSceneList_Open;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem ctmSceneList_DesignMode;
        private System.Windows.Forms.ToolStripMenuItem ctmSceneList_CodeMode;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem ctmSceneList_Cut;
        private System.Windows.Forms.ToolStripMenuItem ctmSceneList_Copy;
        private System.Windows.Forms.ToolStripMenuItem ctmSceneList_Delete;
        private System.Windows.Forms.ToolStripMenuItem ctmSceneList_Rename;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem ctmSceneList_Property;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem ctmSceneList_OpenDir;
        private System.Windows.Forms.ToolStripMenuItem ctmSceneList_Layer;
        private System.Windows.Forms.ToolStripMenuItem 可视层ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 障碍层ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 遮挡层ToolStripMenuItem;


    }
}