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
            this.tvwSceneList = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // tvwSceneList
            // 
            this.tvwSceneList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tvwSceneList;


    }
}