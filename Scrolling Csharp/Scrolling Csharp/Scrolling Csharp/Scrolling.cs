using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using mshtml;

namespace Scrolling_Csharp
{
    public partial class Scrolling : Form
    {
        IntPtr iHwnd;
        Boolean WebOpen;
        Boolean WebNewCheck;
        Boolean SendBtnClick;
        Boolean FirstLoad;
        Boolean NextButton;
        Boolean cmb버튼선택하고첫로드이후;
        int BtnNoCheck = 1;
        int cmbButtonLastNo;
        String address;
        String txtHref;
        String txtHrefValue;
        String HtmlPage;
        String txtContent;
        String cmbTxt;
        public Scrolling()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            WebLoad();
            FirstLoad = true;
        }
        private void WebLoad()
        {
            address = $"https://news.naver.com/main/list.naver?mode=LS2D&mid=shm&sid1=101&sid2=258{HtmlPage}";
            WebBrowser1.Navigate(address);
            if (btnSendMsg.Text == "중지")
                WebOpen = true;
        }
        private void SetBtnNo()
        {
            Bar.Text = "증권 뉴스 버튼 개수 체크 중 입니다.";
            foreach (HtmlElement DivEle in WebBrowser1.Document.GetElementsByTagName("div"))
            {
                if (DivEle.GetAttribute("className") == "paging")
                {
                    Bar.Value = 10;
                    if (DivEle.Children.Count > 10)
                    {
                        if (DivEle.Children.Count == 11)
                        {
                            for (var i = BtnNoCheck; i <= DivEle.Children.Count - 1 + (BtnNoCheck - 1); i++)
                                cmbButton.Items.Add(i);
                            Bar.Value = 30;
                        }
                        else if (DivEle.Children.Count == 12)
                        {
                            for (var i = BtnNoCheck; i <= (DivEle.Children.Count - 2) + (BtnNoCheck - 1); i++)
                                cmbButton.Items.Add(i);
                            Bar.Value = 60;
                        }
                        if (DivEle.Children[DivEle.Children.Count - 1].InnerText == "다음")
                        {
                            DivEle.Children[DivEle.Children.Count - 1].InvokeMember("click");
                            NextButton = true;
                            if (DivEle.Children.Count == 12)
                                BtnNoCheck += DivEle.Children.Count - 2;
                            if (DivEle.Children.Count == 11)
                                BtnNoCheck += DivEle.Children.Count - 1;
                            break;
                        }
                        else
                        {
                            SetcmbItem(DivEle);
                            WebLoad();
                            Bar.Value = 100;
                            Bar.Text = "버튼개수 측정 끝";
                            break;
                        }
                    }
                    else
                    {
                        SetcmbItem(DivEle);
                        WebLoad();
                        Bar.Value = 100;
                        Bar.Text = "버튼개수 측정 끝";
                        NextButton = true;
                        break;
                    }
                }
            }
        }
        private void SetcmbItem(HtmlElement DivEle)
        {
            for (var i = BtnNoCheck; i <= (DivEle.Children.Count -1) + (BtnNoCheck -1); i++)
            {
                cmbButton.Items.Add(i);
                BtnNoCheck += DivEle.Children.Count - 2;
                cmbButtonLastNo = BtnNoCheck;
                BtnNoCheck = 1;
                HtmlPage = $"&page=1";
            }
        }
        private void WebBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (NextButton == false | BtnNoCheck > 10)
            {
                SetBtnNo();
                if (NextButton)
                    return;
            }
            if (SendBtnClick == false)
                return;
            if (WebOpen & FirstLoad)
            {
                SetUrl(); return;
            }
            if (FirstLoad == false & cmbButton.Text == "")
            {
                SetUrl(); BtnClick(SendBtnClick);
            }
        }
        private void BtnClick(bool @bool)
        {
            if (IsDisposed)
                return;
            if (@bool)
            {
                btnSendMsg.Text = "중지";
                WebLoad();
            }
            else if (@bool == false)
            {
                btnSendMsg.Text = "링크전송";
                return;
            }
        }
        private void SetUrl()
        {
            if (WebBrowser1.Document == null)
                return;
            if (cmbTokBang.Text == "")
                return;
            if (cmbTokBang.SelectedItem == null)
                return;
            Bar.Text = "스크롤링 중 입니다.";
            foreach (HtmlElement DivEle in WebBrowser1.Document.GetElementsByTagName("div"))
            {
                if (DivEle.GetAttribute("className") == "list_body newsflash_body")
                {
                    Bar.Value = 10;
                    if (FirstLoad == true)
                    {
                        if (DivEle.Children.Count < 1)
                            return;
                        SetNewsCount_For(DivEle);
                        Bar.Value += 5;
                        if (cmbButtonLastNo != 1 & txtMsg.Text != "")
                        {
                            cmbButtonLastNo -= 1;
                            HtmlPage = $"&page={cmbButtonLastNo}";
                            WebLoad();
                            return;
                        }
                        else if (cmbButtonLastNo == 1 & txtMsg.Text != "")
                        {
                            Bar.Value = 0;
                            Bar.Text = "로드가 완료되었습니다.";
                            FirstLoad = false;
                            cmbButtonLastNo = cmbButton.Items.Count;
                            return;
                        }
                    }
                    else
                    {
                        if (SendBtnClick == false)
                            return;
                        if (cmb버튼선택하고첫로드이후 == true)
                        {
                            cmbButton.Text = ""; cmb버튼선택하고첫로드이후 = false;
                        }
                        Bar.Text = "새로운 뉴스 스크롤링 중 입니다.";
                        if (txtMsg.Text == "" & cmbButton.Text == "")
                        {
                            DelayBool(500);
                            var HtmlA = DivEle.Children[0].Children[0].Children[0].Children[0].Children[0];
                            SetHtmlTagA_href(HtmlA);
                            Bar.Value = 70;
                            return;
                        }
                        else if (txtMsg.Text != "" & cmbButton.Text == "")
                        {
                            var HtmlDl = DivEle.Children[0].Children[0].Children[0];
                            CheckedCompanyName(HtmlDl);
                            Bar.Value = 70;
                            return;
                        }
                        else
                        {
                            if (DivEle.Children.Count < 1)
                                return;
                            SetNewsCount_For(DivEle);
                            Bar.Value = 70;
                            return;
                        }
                    }
                    return;
                }
            }
        }
        private void CheckedCompanyName(HtmlElement HtmDl)
        {
            var HtmlDd = HtmDl.Children[HtmDl.Children.Count - 1];
            var htmlDt = HtmDl.Children[HtmDl.Children.Count - 2];
            if (HtmlDd.InnerText.Contains($"({txtMsg.Text.ToUpper()}") | htmlDt.InnerText.Contains($"{txtMsg.Text.ToLower()}"))
            {
                var HtmlA = HtmDl.Children[0].Children[0];
                SetHtmlTagA_href(HtmlA);
            }
            else
                txtHrefValue = "";
        }
        private void SetNewsCount_For(HtmlElement divele)
        {
            for (var i = divele.Children.Count - 1; i >= 0; i += -1)
            {
                var HtmlUl = divele.Children[i].Children;
                for (var j = HtmlUl.Count - 1; j >= 0; j += -1)
                {
                    if (IsDisposed)
                        return;
                    if (SendBtnClick == false)
                        return;
                    if (txtHrefValue != "")
                        DelayBool(500);
                    if (cmbButton.Text == "")
                        SetCmbTxtCheckedValue(j, HtmlUl);
                    else
                        SetCmbTxtCheckedValue(j, HtmlUl);
                }
            }
            if (txtMsg.Text == "" & FirstLoad == true & cmbButton.Text == "")
            {
                Bar.Value = 60;
                FirstLoad = false;
            }
            else if (txtMsg.Text == "" & FirstLoad == true & cmbButton.Text != "")
            {
                Bar.Value = 60;
                HtmlPage = $"&page=1";
                FirstLoad = false;
                cmb버튼선택하고첫로드이후 = true;
                WebLoad();
            }
        }
        private void SetCmbTxtCheckedValue(int j, HtmlElementCollection HtmlUl)
        {
            if (txtMsg.Text == "")
            {
                if (txtHrefValue != "")
                    DelayBool(2000);
                var HtmlLi = HtmlUl[j].Children[0].Children[0].Children[0];
                SetHtmlTagA_href(HtmlLi);
            }
            else
            {
                var HtmlDl = HtmlUl[j].Children[0];
                CheckedCompanyName(HtmlDl);
            }
        }

        [DllImport("user32.dll")]
        private static extern IntPtr GetShellWindow();

        [DllImport("user32.dll")]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder IpString, int nMaxCount);

        [DllImport("user32.dll")]
        private static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern UInt32 GetWindowThreadProcessId(IntPtr hWnd, out UInt32 IpdwProcessId);


        [DllImport("user32.dll")]
        private static extern bool IsWindowVisible(IntPtr hWnd);
        
        private delegate  bool EnumWindowsProc(IntPtr hWnd, int IParam);
        [DllImport("user32.dll")]
        private static extern bool EnumWindows(EnumWindowsProc enumFunc, int Iparam);
        
        [DllImport("user32")]
        private static extern int PostMessage(IntPtr hwnd, int wMsg, int wParam, int IParam);
        const int VK_RETURN = 0xD;
        private const int WM_KEYDOWN = 0x100;
        private const int WM_SETTEXT = 0xC;
        private IntPtr hShellWindow = GetShellWindow();
        private Dictionary<IntPtr, string> dictWindows = new Dictionary<IntPtr, string>();
        private int currentProcessID;
        private void SetHtmlTagA_href(HtmlElement HtmlA)
        {
            if (HtmlA == null)
                return;
            var dElem =(HTMLAnchorElementClass)HtmlA.DomElement;
            //dynamic dElem =HtmlA.DomElement;
            txtHrefValue = dElem.IHTMLAnchorElement_href;
           // txtHrefValue = ((HTMLAnchorElementClass)dElem).IHTMLAnchorElement_href;
           WebNewCheck = true;
            if (txtHrefValue != "" & WebNewCheck)
                SetKakaoHref();
        }
        public IDictionary<IntPtr, string> GetOpenWindowsFromPID(int processID)
        {
            dictWindows.Clear();
            currentProcessID = processID;
            EnumWindows(enumWindowsInternal, 0);
            return dictWindows;
        }
        private bool enumWindowsInternal(IntPtr hWnd, int IParam)
        {
            if ((hWnd != hShellWindow))
            { 
                if (!IsWindowVisible(hWnd))
                    return true;
                int length = GetWindowTextLength(hWnd);
                if ((length == 0))
                    return true;
                GetWindowThreadProcessId(hWnd,out UInt32 windowPid);
                if ((windowPid != currentProcessID))
                    return true;
                StringBuilder stringBuilder = new StringBuilder(length);
                GetWindowText(hWnd, stringBuilder, (length + 1));
                dictWindows.Add(hWnd, stringBuilder.ToString());
            }
            return true;
        }
        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, string IParam);
        [DllImport("user32.dll", EntryPoint = "FindWindowExA")]
        private static extern IntPtr FindWindowEx(IntPtr hWnd1, IntPtr hWnd2, string Ipsz1, string Ipsz2);
        private Task<int> delay;
        private void SetKakaoHref()
        {
            if (IsDisposed)
                return;                                    
            IntPtr getHwmd = hwmdList[cmbTokBang.SelectedIndex].hwmd;
            iHwnd = getHwmd;
            if (txtHref == txtHrefValue)
                return;
            txtHref = txtHrefValue;
            IntPtr iHwndChild = FindWindowEx(iHwnd, IntPtr.Zero, "RichEdit50W", Constants.vbNullString);
            SendMessage(iHwndChild, WM_SETTEXT, 0, txtHref);
            PostMessage(iHwnd, WM_KEYDOWN, VK_RETURN, 0);
            WebOpen = false;
            WebNewCheck = false;
        }
        private List<model> hwmdList = new List<model>();
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (SendBtnClick)
            {
                Interaction.MsgBox("지금 작업을 중단해주세요~"); return;
            }
            cmbTokBang.Items.Clear();
            Process[] processes = Process.GetProcessesByName("KakaoTalk");
            foreach (Process poc in processes)
            {
                if (poc.MainWindowTitle.Length > 1)
                {
                    IDictionary<IntPtr, string> windows = GetOpenWindowsFromPID(poc.Id);
                    foreach (KeyValuePair<IntPtr, string> kvp in windows)
                    {
                        poc.EnableRaisingEvents = true;
                        try
                        {
                            string cname = kvp.ToString();                            
                            if (!cname.Contains("카카오톡"))
                            {
                                if (!cname.Contains("KakaoTalkEdgeWnd"))
                                {
                                    if (!cname.Contains("KakaoTalkShadowWn"))                                        
                                        cmbTokBang.Items.Add(kvp.ToString());
                                    hwmdList.Add(new model(kvp.Value,kvp.Key));
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Interaction.MsgBox(poc.ProcessName.ToString() + " " + ex.Message);
                        }
                    }
                    if (cmbTokBang.Items.Count != 0)
                        cmbTokBang.SelectedIndex = 0;
                    else if (cmbTokBang.Items.Count == 0)
                        Interaction.MsgBox("카카오대화방을 열어주세요!");
                }
                poc.Exited += OnProcessExit;
            }
        }
        private void OnProcessExit(object sender, EventArgs e)
        {
            Process poc = (Process)sender;
            btnRefresh.PerformClick();
            Interaction.MsgBox("카카오톡의 종료가 감지되었습니다.");
        }
        private void DelayBool(int second)
        {
            if (SetDelay(second) == false)
                return;
        }
        private bool SetDelay(int second)
        {
            var dtDelayStart = DateTime.Now;
            TimeSpan dtDuration = new TimeSpan(0, 0, 0, 0, second);
            DateTime dtThis = dtDelayStart.Add(dtDuration);
            while (dtThis >= dtDelayStart)
            {
                System.Windows.Forms.Application.DoEvents();
                dtDelayStart = DateTime.Now;
                if (IsDisposed)
                    return false;
            }
            return true;
        }
        private void WebBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            if (SendBtnClick == false)
                return;
            if (WebOpen & FirstLoad)
            {
                SetUrl(); return;
            }
            if (FirstLoad == false & cmb버튼선택하고첫로드이후 == true)
                SetUrl();
        }

        private void cmbTokBang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SendBtnClick)
            {
                cmbTokBang.Text = cmbTxt; Interaction.MsgBox("지금 작업을 중단하고 고르세요~"); return;
            }
        }

        private void btnSendMsg_Click(object sender, EventArgs e)
        {
            if (btnSendMsg.Text == "링크전송")
            {
                Bar.Text = "시작 되었습니다";
                txtContent = txtMsg.Text;
                cmbTxt = cmbTokBang.Text;
                SendBtnClick = true;
            }
            else if (btnSendMsg.Text == "중지")
            {
                Bar.Text = "중지 되었습니다";
                Bar.Value = 0;
                SendBtnClick = false;
                if (txtContent != txtMsg.Text)
                {
                    FirstLoad = true; NextButton = false;
                }
            }
            SetPageNo();
        }
        private void SetPageNo()
        {
            if (Information.IsNumeric(cmbButton.Text))
                HtmlPage = $"&page={cmbButton.Text}";
            else if (cmbButton.Text == "" & FirstLoad == true & txtMsg.Text != "")
                HtmlPage = $"&page={cmbButtonLastNo}";
            else
                HtmlPage = $"&page=1";
            BtnClick(SendBtnClick);
        }

        private void cmbButton_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SendBtnClick)
            {
                Interaction.MsgBox("지금 작업을 중단하고 고르세요~"); return;
            }
            SetPageNo();
        }

        private void txtMsg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSendMsg_Click(null, null);
        }
    }
    class model
    {
        public string key;
        public IntPtr hwmd;
        public model(string key, IntPtr hwmd)
        {
            this.key = key;
            this.hwmd = hwmd;
        }

    }
 
}
