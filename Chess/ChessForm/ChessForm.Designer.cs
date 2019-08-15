namespace ChessForm
{
    using ChessGame;
    partial class ChessForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chessPanel = new CP.ChessPanel();
            this.turn = new System.Windows.Forms.Label();
            this.debug = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(646, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // chessPanel
            // 
            this.chessPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chessPanel.Location = new System.Drawing.Point(0, 24);
            this.chessPanel.Margin = new System.Windows.Forms.Padding(2);
            this.chessPanel.Name = "chessPanel";
            this.chessPanel.Size = new System.Drawing.Size(646, 825);
            this.chessPanel.TabIndex = 0;
            // 
            // turn
            // 
            this.turn.AutoSize = true;
            this.turn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.turn.Location = new System.Drawing.Point(12, 670);
            this.turn.Name = "turn";
            this.turn.Size = new System.Drawing.Size(0, 24);
            this.turn.TabIndex = 2;
            // 
            // debug
            // 
            this.debug.AutoSize = true;
            this.debug.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.debug.Location = new System.Drawing.Point(12, 703);
            this.debug.Name = "debug";
            this.debug.Size = new System.Drawing.Size(70, 24);
            this.debug.TabIndex = 3;
            this.debug.Text = "debug";
            // 
            // ChessForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(646, 849);
            this.Controls.Add(this.debug);
            this.Controls.Add(this.turn);
            this.Controls.Add(this.chessPanel);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ChessForm";
            this.Text = "Chess";
            this.Shown += new System.EventHandler(this.ChessForm_Shown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CP.ChessPanel chessPanel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.Label turn;
        private System.Windows.Forms.Label debug;
    }
}
