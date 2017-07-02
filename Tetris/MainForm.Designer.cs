namespace Tetris
{
    partial class MainForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.view1 = new Tetris.View();
            this.SuspendLayout();
            // 
            // view1
            // 
            this.view1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.view1.Location = new System.Drawing.Point(0, 0);
            this.view1.Name = "view1";
            this.view1.Size = new System.Drawing.Size(362, 523);
            this.view1.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 523);
            this.Controls.Add(this.view1);
            this.MaximumSize = new System.Drawing.Size(380, 570);
            this.MinimumSize = new System.Drawing.Size(380, 570);
            this.Name = "MainForm";
            this.Text = "Tetris";
            this.ResumeLayout(false);

        }

        #endregion

        private View view1;
    }
}

