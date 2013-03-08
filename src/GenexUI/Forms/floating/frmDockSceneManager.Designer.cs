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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDockSceneManager));
            this.tvwSceneList = new System.Windows.Forms.TreeView();
            this.ctmSceneNode = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctmSceneNode_Open = new System.Windows.Forms.ToolStripMenuItem();
            this.ctmTs1 = new System.Windows.Forms.ToolStripSeparator();
            this.ctmSceneNode_Layer = new System.Windows.Forms.ToolStripMenuItem();
            this.可视层ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.障碍层ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.遮挡层ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ctmSceneNode_DesignMode = new System.Windows.Forms.ToolStripMenuItem();
            this.ctmSceneNode_CodeMode = new System.Windows.Forms.ToolStripMenuItem();
            this.ctmTs2 = new System.Windows.Forms.ToolStripSeparator();
            this.ctmSceneNode_Cut = new System.Windows.Forms.ToolStripMenuItem();
            this.ctmSceneNode_Copy = new System.Windows.Forms.ToolStripMenuItem();
            this.ctmSceneNode_Paste = new System.Windows.Forms.ToolStripMenuItem();
            this.ctmSceneNode_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.ctmSceneNode_Rename = new System.Windows.Forms.ToolStripMenuItem();
            this.ctmTs3 = new System.Windows.Forms.ToolStripSeparator();
            this.ctmSceneNode_OpenDir = new System.Windows.Forms.ToolStripMenuItem();
            this.ctmTs4 = new System.Windows.Forms.ToolStripSeparator();
            this.ctmSceneNode_Reload = new System.Windows.Forms.ToolStripMenuItem();
            this.ctmSceneNode_Property = new System.Windows.Forms.ToolStripMenuItem();
            this.imgTreeIcons = new System.Windows.Forms.ImageList(this.components);
            this.ctmSceneNode_Add = new System.Windows.Forms.ToolStripMenuItem();
            this.ctmSceneNode_Add_Scene = new System.Windows.Forms.ToolStripMenuItem();
            this.ctmSceneNode_Add_NewDir = new System.Windows.Forms.ToolStripMenuItem();
            this.ctmSceneNode.SuspendLayout();
            this.SuspendLayout();
            // 
            // tvwSceneList
            // 
            this.tvwSceneList.AllowDrop = true;
            this.tvwSceneList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvwSceneList.ContextMenuStrip = this.ctmSceneNode;
            this.tvwSceneList.FullRowSelect = true;
            this.tvwSceneList.ImageIndex = 0;
            this.tvwSceneList.ImageList = this.imgTreeIcons;
            this.tvwSceneList.LabelEdit = true;
            this.tvwSceneList.Location = new System.Drawing.Point(5, 29);
            this.tvwSceneList.Name = "tvwSceneList";
            this.tvwSceneList.PathSeparator = "/";
            this.tvwSceneList.SelectedImageIndex = 0;
            this.tvwSceneList.ShowNodeToolTips = true;
            this.tvwSceneList.Size = new System.Drawing.Size(233, 356);
            this.tvwSceneList.TabIndex = 0;
            this.tvwSceneList.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.tvwSceneList_ItemDrag);
            this.tvwSceneList.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvwSceneList_AfterSelect);
            this.tvwSceneList.DragDrop += new System.Windows.Forms.DragEventHandler(this.tvwSceneList_DragDrop);
            this.tvwSceneList.DragEnter += new System.Windows.Forms.DragEventHandler(this.tvwSceneList_DragEnter);
            this.tvwSceneList.DragOver += new System.Windows.Forms.DragEventHandler(this.tvwSceneList_DragOver);
            this.tvwSceneList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvwSceneList_MouseDown);
            this.tvwSceneList.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tvwSceneList_MouseUp);
            // 
            // ctmSceneNode
            // 
            this.ctmSceneNode.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctmSceneNode_Open,
            this.ctmSceneNode_Add,
            this.ctmTs1,
            this.ctmSceneNode_Layer,
            this.ctmSceneNode_DesignMode,
            this.ctmSceneNode_CodeMode,
            this.ctmTs2,
            this.ctmSceneNode_Cut,
            this.ctmSceneNode_Copy,
            this.ctmSceneNode_Paste,
            this.ctmSceneNode_Delete,
            this.ctmSceneNode_Rename,
            this.ctmTs3,
            this.ctmSceneNode_OpenDir,
            this.ctmTs4,
            this.ctmSceneNode_Reload,
            this.ctmSceneNode_Property});
            this.ctmSceneNode.Name = "ctmSceneNode";
            this.ctmSceneNode.Size = new System.Drawing.Size(213, 336);
            // 
            // ctmSceneNode_Open
            // 
            this.ctmSceneNode_Open.Name = "ctmSceneNode_Open";
            this.ctmSceneNode_Open.Size = new System.Drawing.Size(212, 22);
            this.ctmSceneNode_Open.Text = "打开(&O)";
            // 
            // ctmTs1
            // 
            this.ctmTs1.Name = "ctmTs1";
            this.ctmTs1.Size = new System.Drawing.Size(209, 6);
            // 
            // ctmSceneNode_Layer
            // 
            this.ctmSceneNode_Layer.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.可视层ToolStripMenuItem,
            this.障碍层ToolStripMenuItem,
            this.遮挡层ToolStripMenuItem});
            this.ctmSceneNode_Layer.Name = "ctmSceneNode_Layer";
            this.ctmSceneNode_Layer.Size = new System.Drawing.Size(212, 22);
            this.ctmSceneNode_Layer.Text = "图层(&L)";
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
            // ctmSceneNode_DesignMode
            // 
            this.ctmSceneNode_DesignMode.Name = "ctmSceneNode_DesignMode";
            this.ctmSceneNode_DesignMode.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.D)));
            this.ctmSceneNode_DesignMode.Size = new System.Drawing.Size(212, 22);
            this.ctmSceneNode_DesignMode.Text = "设计视图(&Y)";
            // 
            // ctmSceneNode_CodeMode
            // 
            this.ctmSceneNode_CodeMode.Name = "ctmSceneNode_CodeMode";
            this.ctmSceneNode_CodeMode.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.C)));
            this.ctmSceneNode_CodeMode.Size = new System.Drawing.Size(212, 22);
            this.ctmSceneNode_CodeMode.Text = "代码视图(&G)";
            // 
            // ctmTs2
            // 
            this.ctmTs2.Name = "ctmTs2";
            this.ctmTs2.Size = new System.Drawing.Size(209, 6);
            // 
            // ctmSceneNode_Cut
            // 
            this.ctmSceneNode_Cut.Name = "ctmSceneNode_Cut";
            this.ctmSceneNode_Cut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.ctmSceneNode_Cut.Size = new System.Drawing.Size(212, 22);
            this.ctmSceneNode_Cut.Text = "剪切(&T)";
            this.ctmSceneNode_Cut.Click += new System.EventHandler(this.ctmSceneNode_Cut_Click);
            // 
            // ctmSceneNode_Copy
            // 
            this.ctmSceneNode_Copy.Name = "ctmSceneNode_Copy";
            this.ctmSceneNode_Copy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.ctmSceneNode_Copy.Size = new System.Drawing.Size(212, 22);
            this.ctmSceneNode_Copy.Text = "复制(&C)";
            this.ctmSceneNode_Copy.Click += new System.EventHandler(this.ctmSceneNode_Copy_Click);
            // 
            // ctmSceneNode_Paste
            // 
            this.ctmSceneNode_Paste.Name = "ctmSceneNode_Paste";
            this.ctmSceneNode_Paste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.ctmSceneNode_Paste.Size = new System.Drawing.Size(212, 22);
            this.ctmSceneNode_Paste.Text = "粘贴(&V)";
            this.ctmSceneNode_Paste.Click += new System.EventHandler(this.ctmSceneNode_Paste_Click);
            // 
            // ctmSceneNode_Delete
            // 
            this.ctmSceneNode_Delete.Name = "ctmSceneNode_Delete";
            this.ctmSceneNode_Delete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.ctmSceneNode_Delete.Size = new System.Drawing.Size(212, 22);
            this.ctmSceneNode_Delete.Text = "删除(&D)";
            // 
            // ctmSceneNode_Rename
            // 
            this.ctmSceneNode_Rename.Name = "ctmSceneNode_Rename";
            this.ctmSceneNode_Rename.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.ctmSceneNode_Rename.Size = new System.Drawing.Size(212, 22);
            this.ctmSceneNode_Rename.Text = "重命名(&M)";
            // 
            // ctmTs3
            // 
            this.ctmTs3.Name = "ctmTs3";
            this.ctmTs3.Size = new System.Drawing.Size(209, 6);
            // 
            // ctmSceneNode_OpenDir
            // 
            this.ctmSceneNode_OpenDir.Name = "ctmSceneNode_OpenDir";
            this.ctmSceneNode_OpenDir.Size = new System.Drawing.Size(212, 22);
            this.ctmSceneNode_OpenDir.Text = "打开目标文件夹(&X)";
            // 
            // ctmTs4
            // 
            this.ctmTs4.Name = "ctmTs4";
            this.ctmTs4.Size = new System.Drawing.Size(209, 6);
            // 
            // ctmSceneNode_Reload
            // 
            this.ctmSceneNode_Reload.Name = "ctmSceneNode_Reload";
            this.ctmSceneNode_Reload.Size = new System.Drawing.Size(212, 22);
            this.ctmSceneNode_Reload.Text = "重新加载列表(&U)";
            this.ctmSceneNode_Reload.Click += new System.EventHandler(this.ctmSceneNode_Reload_Click);
            // 
            // ctmSceneNode_Property
            // 
            this.ctmSceneNode_Property.Name = "ctmSceneNode_Property";
            this.ctmSceneNode_Property.ShortcutKeys = System.Windows.Forms.Keys.F12;
            this.ctmSceneNode_Property.Size = new System.Drawing.Size(212, 22);
            this.ctmSceneNode_Property.Text = "属性(&R)";
            // 
            // imgTreeIcons
            // 
            this.imgTreeIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgTreeIcons.ImageStream")));
            this.imgTreeIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.imgTreeIcons.Images.SetKeyName(0, "project.png");
            this.imgTreeIcons.Images.SetKeyName(1, "dir.png");
            this.imgTreeIcons.Images.SetKeyName(2, "scene.png");
            // 
            // ctmSceneNode_Add
            // 
            this.ctmSceneNode_Add.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctmSceneNode_Add_Scene,
            this.ctmSceneNode_Add_NewDir});
            this.ctmSceneNode_Add.Name = "ctmSceneNode_Add";
            this.ctmSceneNode_Add.Size = new System.Drawing.Size(212, 22);
            this.ctmSceneNode_Add.Text = "添加(&N)";
            // 
            // ctmSceneNode_Add_Scene
            // 
            this.ctmSceneNode_Add_Scene.Name = "ctmSceneNode_Add_Scene";
            this.ctmSceneNode_Add_Scene.Size = new System.Drawing.Size(152, 22);
            this.ctmSceneNode_Add_Scene.Text = "游戏场景(&S)...";
            // 
            // ctmSceneNode_Add_NewDir
            // 
            this.ctmSceneNode_Add_NewDir.Name = "ctmSceneNode_Add_NewDir";
            this.ctmSceneNode_Add_NewDir.Size = new System.Drawing.Size(152, 22);
            this.ctmSceneNode_Add_NewDir.Text = "新建文件夹(&D)";
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
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frmDockSceneManager_MouseDown);
            this.ctmSceneNode.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tvwSceneList;
        private System.Windows.Forms.ContextMenuStrip ctmSceneNode;
        private System.Windows.Forms.ToolStripMenuItem ctmSceneNode_Open;
        private System.Windows.Forms.ToolStripSeparator ctmTs1;
        private System.Windows.Forms.ToolStripMenuItem ctmSceneNode_DesignMode;
        private System.Windows.Forms.ToolStripMenuItem ctmSceneNode_CodeMode;
        private System.Windows.Forms.ToolStripSeparator ctmTs2;
        private System.Windows.Forms.ToolStripMenuItem ctmSceneNode_Cut;
        private System.Windows.Forms.ToolStripMenuItem ctmSceneNode_Copy;
        private System.Windows.Forms.ToolStripMenuItem ctmSceneNode_Delete;
        private System.Windows.Forms.ToolStripMenuItem ctmSceneNode_Rename;
        private System.Windows.Forms.ToolStripSeparator ctmTs4;
        private System.Windows.Forms.ToolStripMenuItem ctmSceneNode_Property;
        private System.Windows.Forms.ToolStripSeparator ctmTs3;
        private System.Windows.Forms.ToolStripMenuItem ctmSceneNode_OpenDir;
        private System.Windows.Forms.ToolStripMenuItem ctmSceneNode_Layer;
        private System.Windows.Forms.ToolStripMenuItem 可视层ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 障碍层ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 遮挡层ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ctmSceneNode_Paste;
        private System.Windows.Forms.ImageList imgTreeIcons;
        private System.Windows.Forms.ToolStripMenuItem ctmSceneNode_Reload;
        private System.Windows.Forms.ToolStripMenuItem ctmSceneNode_Add;
        private System.Windows.Forms.ToolStripMenuItem ctmSceneNode_Add_Scene;
        private System.Windows.Forms.ToolStripMenuItem ctmSceneNode_Add_NewDir;


    }
}