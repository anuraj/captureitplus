using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Json;
using System.Windows.Forms;

namespace SendToPlugins
{
    public partial class SendToSkydriveUI : Form
    {
        private string _fileName;
        private string _authToken;
        private BackgroundWorker _backgroundWorker;

        public SendToSkydriveUI()
        {
            InitializeComponent();
        }

        public SendToSkydriveUI(string fileName)
            : this()
        {
            _fileName = fileName;
            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.WorkerReportsProgress = true;
            _backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorkerRunWorkerCompleted);
            _backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(backgroundWorkerProgressChanged);
            _backgroundWorker.DoWork += new DoWorkEventHandler(backgroundWorkerDoWork);
        }

        private void backgroundWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            string authCode = e.Argument.ToString();
            makeAccessTokenRequest(accessTokenUrl + authCode);
        }

        private void backgroundWorkerProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            uploadProgress.Value = e.ProgressPercentage;
        }

        private void backgroundWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error == null)
            {

            }
            else
            {
                throw new ApplicationException(e.Error.Message);
            }
            Close();
        }

        private static string client_id = "000000004C0DD8BF";
        private static string client_secret = "k89ThaQUE27TAfjQw-Od64ifbCZzLGfw";
        private static string accessTokenUrl = String.Format(@"https://login.live.com/oauth20_token.srf?client_id={0}&client_secret={1}&redirect_uri=https://login.live.com/oauth20_desktop.srf&grant_type=authorization_code&code=", client_id, client_secret);
        private static string apiUrl = @"https://apis.live.net/v5.0/me/skydrive/files";
        private Dictionary<string, string> tokenData = new Dictionary<string, string>();

        private void SendToSkydriveUI_Load(object sender, EventArgs e)
        {

        }

        private void cmdSignIn_Click(object sender, EventArgs e)
        {
            SkydriveAuth skyDriveAuth = new SkydriveAuth();
            if (skyDriveAuth.ShowDialog() == DialogResult.OK)
            {
                string authCode = skyDriveAuth.AuthCode;
                _backgroundWorker.RunWorkerAsync(authCode);
            }
        }

        private void makeAccessTokenRequest(string requestUrl)
        {
            ReportProgress(5);
            WebClient wc = new WebClient();
            string result = wc.DownloadString(new Uri(requestUrl));
            ReportProgress(20);
            JsonTextParser parser = new JsonTextParser();
            JsonObject jsonObj = parser.Parse(result);
            ReportProgress(25);
            foreach (JsonObject field in jsonObj as JsonObjectCollection)
            {
                if (field.Name == "access_token")
                {
                    makeApiRequest(apiUrl, _fileName, field.GetValue().ToString());
                    break;
                }
            }
        }
        
        private void makeApiRequest(string requestUrl, string fileName, string accessToken)
        {
            ReportProgress(30);
            _authToken = accessToken;
            string url = string.Format("{0}/{1}?access_token={2}", requestUrl, Path.GetFileName(fileName), accessToken);
            using (var client = new System.Net.WebClient())
            {
                ReportProgress(40);
                client.UploadData(url, "PUT", ImageToByteArray(_fileName));
                ReportProgress(100);
            }
        }

        public byte[] ImageToByteArray(string fileName)
        {
            byte[] imageData;
            ReportProgress(60);
            FileStream fileStream = File.OpenRead(fileName);
            imageData = new byte[fileStream.Length];
            fileStream.Read(imageData, 0, imageData.Length);
            fileStream.Close();
            ReportProgress(80);
            return imageData;
        }

        private void ReportProgress(int progress)
        {
            _backgroundWorker.ReportProgress(progress);
        }
    }
}
