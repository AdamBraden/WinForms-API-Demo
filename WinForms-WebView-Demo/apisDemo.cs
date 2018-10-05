using System;
using System.Linq;
using System.Windows.Forms;
//
//Needed for extention methods on windows types, ie: .ToArray()
using System.Runtime.InteropServices.WindowsRuntime;
//
//Notifications & Windows XML APIs
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;


namespace WinForms_WebView_Demo
{
    public partial class apisDemo : Form
    {
        public apisDemo()
        {
            InitializeComponent();
        }

        private void GetSystemId(object sender, EventArgs e)
        {
            Windows.Storage.Streams.IBuffer buffId = Windows.System.Profile.SystemIdentification.GetSystemIdForPublisher().Id;

            textBox1.Text = ByteArrayToHexString(buffId.ToArray());
        }

        private void ShowPictureOfTheDay(object sender, EventArgs e)
        {
            string title = "featured picture of the day";
            string content = "beautiful scenery";
            string image = "https://picsum.photos/360/180?image=104";
            string logo = "https://picsum.photos/64?image=883";

            string xmlString =
            $@"<toast><visual>
                <binding template='ToastGeneric'>
                <text>{title}</text>
                <text>{content}</text>
                <image src='{image}'/>
                <image src='{logo}' placement='appLogoOverride' hint-crop='circle'/>
                </binding>
                </visual></toast>";

            XmlDocument toastXml = new XmlDocument();
            toastXml.LoadXml(xmlString);

            //Create & Show toast
            ToastNotification toast = new ToastNotification(toastXml);
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }

        /// <summary>
        /// Helper method to format byte array into a hex string
        /// </summary>
        /// <param name="ba"></param>
        /// <returns>Hex String</returns>
        public static string ByteArrayToHexString(byte[] ba)
        {
            return BitConverter.ToString(ba).Replace("-", "");
        }
    }
}
