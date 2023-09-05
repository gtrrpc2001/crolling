
namespace Scrolling_Csharp
{
    partial class Scrolling
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Scrolling));
            this.btnRefresh = new System.Windows.Forms.Button();
            this.cmbTokBang = new System.Windows.Forms.ComboBox();
            this.btnSendMsg = new System.Windows.Forms.Button();
            this.cmbButton = new System.Windows.Forms.ComboBox();
            this.txtMsg = new System.Windows.Forms.TextBox();
            this.WebBrowser1 = new System.Windows.Forms.WebBrowser();
            this.Bar = new DevComponents.DotNetBar.Controls.ProgressBarX();
            this.SuspendLayout();
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Location = new System.Drawing.Point(0, -1);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(136, 21);
            this.btnRefresh.TabIndex = 6;
            this.btnRefresh.Text = "대화방선택";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // cmbTokBang
            // 
            this.cmbTokBang.FormattingEnabled = true;
            this.cmbTokBang.Location = new System.Drawing.Point(135, 0);
            this.cmbTokBang.Name = "cmbTokBang";
            this.cmbTokBang.Size = new System.Drawing.Size(242, 20);
            this.cmbTokBang.TabIndex = 8;
            this.cmbTokBang.SelectedIndexChanged += new System.EventHandler(this.cmbTokBang_SelectedIndexChanged);
            // 
            // btnSendMsg
            // 
            this.btnSendMsg.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnSendMsg.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSendMsg.Location = new System.Drawing.Point(0, 20);
            this.btnSendMsg.Name = "btnSendMsg";
            this.btnSendMsg.Size = new System.Drawing.Size(136, 39);
            this.btnSendMsg.TabIndex = 9;
            this.btnSendMsg.Text = "링크전송";
            this.btnSendMsg.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSendMsg.UseVisualStyleBackColor = false;
            this.btnSendMsg.Click += new System.EventHandler(this.btnSendMsg_Click);
            // 
            // cmbButton
            // 
            this.cmbButton.FormattingEnabled = true;
            this.cmbButton.Location = new System.Drawing.Point(0, 39);
            this.cmbButton.Name = "cmbButton";
            this.cmbButton.Size = new System.Drawing.Size(136, 20);
            this.cmbButton.TabIndex = 10;
            this.cmbButton.SelectedIndexChanged += new System.EventHandler(this.cmbButton_SelectedIndexChanged);
            // 
            // txtMsg
            // 
            this.txtMsg.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.txtMsg.Location = new System.Drawing.Point(135, 20);
            this.txtMsg.Multiline = true;
            this.txtMsg.Name = "txtMsg";
            this.txtMsg.Size = new System.Drawing.Size(242, 39);
            this.txtMsg.TabIndex = 11;
            this.txtMsg.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMsg_KeyDown);
            // 
            // WebBrowser1
            // 
            this.WebBrowser1.Location = new System.Drawing.Point(1, 3);
            this.WebBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.WebBrowser1.Name = "WebBrowser1";
            this.WebBrowser1.ScriptErrorsSuppressed = true;
            this.WebBrowser1.Size = new System.Drawing.Size(107, 21);
            this.WebBrowser1.TabIndex = 13;
            this.WebBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.WebBrowser1_DocumentCompleted);
            this.WebBrowser1.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.WebBrowser1_Navigated);
            // 
            // Bar
            // 
            // 
            // 
            // 
            this.Bar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.Bar.Location = new System.Drawing.Point(0, 58);
            this.Bar.Name = "Bar";
            this.Bar.Size = new System.Drawing.Size(377, 23);
            this.Bar.TabIndex = 14;
            this.Bar.TextVisible = true;
            // 
            // Scrolling
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(377, 79);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.Bar);
            this.Controls.Add(this.txtMsg);
            this.Controls.Add(this.cmbButton);
            this.Controls.Add(this.btnSendMsg);
            this.Controls.Add(this.cmbTokBang);
            this.Controls.Add(this.WebBrowser1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Scrolling";
            this.Text = "NewsScolling";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button btnRefresh;
        internal System.Windows.Forms.ComboBox cmbTokBang;
        internal System.Windows.Forms.Button btnSendMsg;
        internal System.Windows.Forms.ComboBox cmbButton;
        internal System.Windows.Forms.TextBox txtMsg;
        private System.Windows.Forms.WebBrowser WebBrowser1;
        private DevComponents.DotNetBar.Controls.ProgressBarX Bar;
    }
}

