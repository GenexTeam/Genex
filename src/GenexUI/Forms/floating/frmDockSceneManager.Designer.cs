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
            this.打开OToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.可视层ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.障碍层ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.遮挡层ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.查看属性ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.代码视图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.剪切ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.复制ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.重命名ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.属性ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            // 
            // ctmSceneList
            // 
            this.ctmSceneList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开OToolStripMenuItem,
            this.toolStripMenuItem1,
            this.toolStripMenuItem6,
            this.查看属性ToolStripMenuItem,
            this.代码视图ToolStripMenuItem,
            this.toolStripMenuItem2,
            this.剪切ToolStripMenuItem,
            this.复制ToolStripMenuItem,
            this.删除ToolStripMenuItem,
            this.重命名ToolStripMenuItem,
            this.toolStripMenuItem4,
            this.toolStripMenuItem5,
            this.toolStripMenuItem3,
            this.属性ToolStripMenuItem});
            this.ctmSceneList.Name = "ctmSceneList";
            this.ctmSceneList.Size = new System.Drawing.Size(221, 270);
            // 
            // 打开OToolStripMenuItem
            // 
            this.打开OToolStripMenuItem.Name = "打开OToolStripMenuItem";
            this.打开OToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.打开OToolStripMenuItem.Text = "打开(&O)";
            this.打开OToolStripMenuItem.Click += new System.EventHandler(this.打开OToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(173, 6);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.可视层ToolStripMenuItem,
            this.障碍层ToolStripMenuItem,
            this.遮挡层ToolStripMenuItem});
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(176, 22);
            this.toolStripMenuItem6.Text = "图层(&L)";
            // 
            // 可视层ToolStripMenuItem
            // 
            this.可视层ToolStripMenuItem.Name = "可视层ToolStripMenuItem";
            this.可视层ToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.可视层ToolStripMenuItem.Text = "可视层(&V)";
            // 
            // 障碍层ToolStripMenuItem
            // 
            this.障碍层ToolStripMenuItem.Name = "障碍层ToolStripMenuItem";
            this.障碍层ToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.障碍层ToolStripMenuItem.Text = "障碍层(&B)";
            // 
            // 遮挡层ToolStripMenuItem
            // 
            this.遮挡层ToolStripMenuItem.Name = "遮挡层ToolStripMenuItem";
            this.遮挡层ToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.遮挡层ToolStripMenuItem.Text = "遮挡层(&D)";
            // 
            // 查看属性ToolStripMenuItem
            // 
            this.查看属性ToolStripMenuItem.Name = "查看属性ToolStripMenuItem";
            this.查看属性ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.D)));
            this.查看属性ToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.查看属性ToolStripMenuItem.Text = "设计视图(&Y)";
            // 
            // 代码视图ToolStripMenuItem
            // 
            this.代码视图ToolStripMenuItem.Name = "代码视图ToolStripMenuItem";
            this.代码视图ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.C)));
            this.代码视图ToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.代码视图ToolStripMenuItem.Text = "代码视图(&G)";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(173, 6);
            // 
            // 剪切ToolStripMenuItem
            // 
            this.剪切ToolStripMenuItem.Name = "剪切ToolStripMenuItem";
            this.剪切ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.剪切ToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.剪切ToolStripMenuItem.Text = "剪切(&T)";
            // 
            // 复制ToolStripMenuItem
            // 
            this.复制ToolStripMenuItem.Name = "复制ToolStripMenuItem";
            this.复制ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.复制ToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.复制ToolStripMenuItem.Text = "复制(&C)";
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.删除ToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.删除ToolStripMenuItem.Text = "删除(&D)";
            // 
            // 重命名ToolStripMenuItem
            // 
            this.重命名ToolStripMenuItem.Name = "重命名ToolStripMenuItem";
            this.重命名ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.重命名ToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.重命名ToolStripMenuItem.Text = "重命名(&M)";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(173, 6);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(176, 22);
            this.toolStripMenuItem5.Text = "打开目标文件夹(&X)";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(173, 6);
            // 
            // 属性ToolStripMenuItem
            // 
            this.属性ToolStripMenuItem.Name = "属性ToolStripMenuItem";
            this.属性ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F12;
            this.属性ToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.属性ToolStripMenuItem.Text = "属性(&R)";
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
        private System.Windows.Forms.ToolStripMenuItem 打开OToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 查看属性ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 代码视图ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem 剪切ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 复制ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 重命名ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem 属性ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem 可视层ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 障碍层ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 遮挡层ToolStripMenuItem;


    }
}