﻿namespace GenexUI.Forms
{
    partial class frmNewFileGuider
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("GX空白游戏项目");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("GX游戏项目(RPG范例)");
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("文件文件");
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("常规");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("场景");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("脚本");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("文档");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("图像");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("默认模板", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5});
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("模板", new System.Windows.Forms.TreeNode[] {
            treeNode6});
            this.templateDescription = new System.Windows.Forms.RichTextBox();
            this.templateList = new System.Windows.Forms.ListView();
            this.templateFileList = new System.Windows.Forms.TreeView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.fileName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.openFilePath = new System.Windows.Forms.Button();
            this.filePath = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.canel = new System.Windows.Forms.Button();
            this.confirm = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // templateDescription
            // 
            this.templateDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.templateDescription.Location = new System.Drawing.Point(498, 1);
            this.templateDescription.Name = "templateDescription";
            this.templateDescription.Size = new System.Drawing.Size(175, 299);
            this.templateDescription.TabIndex = 36;
            this.templateDescription.Text = "xxx";
            // 
            // templateList
            // 
            this.templateList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.templateList.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3});
            this.templateList.Location = new System.Drawing.Point(158, 1);
            this.templateList.Name = "templateList";
            this.templateList.Size = new System.Drawing.Size(334, 299);
            this.templateList.TabIndex = 35;
            this.templateList.UseCompatibleStateImageBehavior = false;
            this.templateList.View = System.Windows.Forms.View.List;
            // 
            // templateFileList
            // 
            this.templateFileList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.templateFileList.Location = new System.Drawing.Point(7, 1);
            this.templateFileList.Name = "templateFileList";
            treeNode1.Name = "节点2";
            treeNode1.Text = "常规";
            treeNode2.Name = "节点3";
            treeNode2.Text = "场景";
            treeNode3.Name = "节点4";
            treeNode3.Text = "脚本";
            treeNode4.Name = "节点5";
            treeNode4.Text = "文档";
            treeNode5.Name = "节点6";
            treeNode5.Text = "图像";
            treeNode6.Name = "节点1";
            treeNode6.Text = "默认模板";
            treeNode7.Name = "节点0";
            treeNode7.Text = "模板";
            this.templateFileList.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode7});
            this.templateFileList.Size = new System.Drawing.Size(145, 299);
            this.templateFileList.TabIndex = 34;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.fileName);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.openFilePath);
            this.panel1.Controls.Add(this.filePath);
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Controls.Add(this.canel);
            this.panel1.Controls.Add(this.confirm);
            this.panel1.Location = new System.Drawing.Point(7, 309);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(666, 100);
            this.panel1.TabIndex = 37;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 35;
            this.label2.Text = "位置：";
            // 
            // fileName
            // 
            this.fileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fileName.Location = new System.Drawing.Point(57, 19);
            this.fileName.Name = "fileName";
            this.fileName.Size = new System.Drawing.Size(368, 21);
            this.fileName.TabIndex = 34;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 33;
            this.label1.Text = "名称：";
            // 
            // openFilePath
            // 
            this.openFilePath.Location = new System.Drawing.Point(396, 54);
            this.openFilePath.Name = "openFilePath";
            this.openFilePath.Size = new System.Drawing.Size(29, 23);
            this.openFilePath.TabIndex = 40;
            this.openFilePath.Text = "...";
            this.openFilePath.UseVisualStyleBackColor = true;
            this.openFilePath.Click += new System.EventHandler(this.openFilePath_Click);
            // 
            // filePath
            // 
            this.filePath.Location = new System.Drawing.Point(60, 54);
            this.filePath.Name = "filePath";
            this.filePath.Size = new System.Drawing.Size(365, 21);
            this.filePath.TabIndex = 39;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(480, 25);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(108, 16);
            this.checkBox1.TabIndex = 38;
            this.checkBox1.Text = "为项目创建目录";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // canel
            // 
            this.canel.Location = new System.Drawing.Point(579, 58);
            this.canel.Name = "canel";
            this.canel.Size = new System.Drawing.Size(75, 23);
            this.canel.TabIndex = 37;
            this.canel.Text = "取消";
            this.canel.UseVisualStyleBackColor = true;
            // 
            // confirm
            // 
            this.confirm.Location = new System.Drawing.Point(480, 58);
            this.confirm.Name = "confirm";
            this.confirm.Size = new System.Drawing.Size(75, 23);
            this.confirm.TabIndex = 36;
            this.confirm.Text = "确定";
            this.confirm.UseVisualStyleBackColor = true;
            // 
            // frmNewFileGuider
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(681, 411);
            this.Controls.Add(this.templateDescription);
            this.Controls.Add(this.templateList);
            this.Controls.Add(this.templateFileList);
            this.Controls.Add(this.panel1);
            this.Name = "frmNewFileGuider";
            this.Text = "新建文件";
            this.Load += new System.EventHandler(this.frmNewFileGuider_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox templateDescription;
        private System.Windows.Forms.ListView templateList;
        private System.Windows.Forms.TreeView templateFileList;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox fileName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button openFilePath;
        private System.Windows.Forms.TextBox filePath;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button canel;
        private System.Windows.Forms.Button confirm;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;



    }
}