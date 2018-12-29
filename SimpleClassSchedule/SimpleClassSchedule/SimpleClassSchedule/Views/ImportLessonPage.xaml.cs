using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SimpleClassSchedule.Models;
using System.Xml.Serialization;

namespace SimpleClassSchedule.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ImportLessonPage : ContentPage
    {
        CookieContainer container;
        //生成验证码图片
        public static string nameid;

        public void ReflshPicImage()
        {
            //获取验证码
            String url = "http://210.42.121.241/servlet/GenImg";
            HttpWebRequest request1 = (HttpWebRequest)WebRequest.Create(url);
            request1.Method = "GET";
            request1.Accept = "image/png, image/svg+xml, image/*; q=0.8, */*; q=0.5";
            request1.CookieContainer = new CookieContainer();
            //保存验证码cookie，以便后续登陆，获取课程表
            container = request1.CookieContainer;
            request1.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/64.0.3282.140 Safari/537.36 Edge/17.17134";
            HttpWebResponse response1 = (HttpWebResponse)request1.GetResponse();
            response1.Cookies = container.GetCookies(request1.RequestUri);
            Stream stream = response1.GetResponseStream();
            var imageSource = ImageSource.FromStream(() => stream);
            Device.BeginInvokeOnMainThread(() => { yzmImage.Source = imageSource; });
        }

        private void OnTranslate(object sender, EventArgs e)
        {
            //登陆操作
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://210.42.121.241/servlet/Login");
            request.Method = "POST";
            //根据正常登陆的过程，抓包看数据怎么传输，然后模拟
            //传入验证码cookie
            request.CookieContainer = container;
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64)" +
                " AppleWebKit/537.36 (KHTML, like Gecko) Chrome/64.0.3282.140 Safari/537.36 Edge/17.17134";
            request.Accept = "text/html, application/xhtml+xml, application/xml; q=0.9, */*; q=0.8";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Referer = "http://210.42.121.241/servlet/Login";

            //保存账号密码
            App.UserPreferences.SetString("pwd", pwdIn.Text);
            App.UserPreferences.SetString("id", idIn.Text);
            nameid = idIn.Text;
            //发现密码上传时，用md5加密，所以也要加密上传
            byte[] result = Encoding.Default.GetBytes(pwdIn.Text);//textBox2 密码
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            String pwd = BitConverter.ToString(output).Replace("-", "").ToLower();


            //根据抓包发现的表单结构传入数据
            String postData = String.Format("id={0}&pwd={1}&xdvfb={2}", idIn.Text, pwd, yzmIn.Text);
            //账号 textBox1
            //验证码 textBox3
            byte[] postdatabyte = Encoding.ASCII.GetBytes(postData);
            request.ContentLength = postdatabyte.Length;
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(postdatabyte, 0, postdatabyte.Length);
            }


            //接收输出数据
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string strWebData = string.Empty;
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                strWebData = reader.ReadToEnd();
            }
            //发现只有网页框架，于是正常登陆看课程表数据是如何传到客户端的
            //发现有一个get请求，于是模拟，但发现请求url里有token值传入。
            //找了半天发现token值在网页框架中有


            //正则表达式获取token值
            string tokenPatten = "csrftoken=[a-zA-Z0-9-]+";
            Regex r = new Regex(tokenPatten);
            Match match = r.Match(strWebData);
            string t = match.Value;
            List<char> cList = new List<char>();
            bool flag = false;
            for (int i = 0; i < t.Length; i++)
            {
                if (flag) cList.Add(t[i]);
                if (t[i] == '=')
                {
                    flag = true;
                }
            }
            string token = new string(cList.ToArray());

            //模拟get操作
            HttpWebRequest requestTable =
                (HttpWebRequest)WebRequest.Create
                ("http://210.42.121.241/servlet/Svlt_QueryStuLsn?action=queryStuLsn&csrftoken="
                + token);
            requestTable.Method = "GET";
            requestTable.CookieContainer = container;
            requestTable.Referer = "http://210.42.121.241/stu/stu_index.jsp";
            requestTable.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/64.0.3282.140 Safari/537.36 Edge/17.17134";
            requestTable.Accept = "text/html, application/xhtml+xml, application/xml; q=0.9, */*; q=0.8";
            requestTable.ContentType = "application/x-www-form-urlencoded";


            HttpWebResponse responseTable = (HttpWebResponse)requestTable.GetResponse();
            Encoding cd = System.Text.Encoding.GetEncoding(responseTable.CharacterSet);
            Stream resStream = responseTable.GetResponseStream();
            string studenTable = string.Empty;
            using (StreamReader reader = new StreamReader(resStream, cd))
            {
                studenTable = reader.ReadToEnd();
            }
            string pattern = "checkBrowserType.*</script>";
            string s1 = new Regex(pattern, RegexOptions.Singleline).Match(studenTable).Value;

            //App.UserPreferences.SetString("LessonInfo", s1);//保存字符串
            GridLesson gridLesson = new GridLesson();
            gridLesson.read(s1);
            writeXml(gridLesson.lessons);

            if (s1 != "")
            {
                DisplayAlert("登录成功", "请重新启动查看课程表", "确定");
            }
            else
            {
                DisplayAlert("登录失败", "请检查输入是否正确", "确定");
                ReflshPicImage();
            }
        }


        public ImportLessonPage ()
		{
            InitializeComponent();
            pwdIn.Text = App.UserPreferences.GetString("pwd");
            idIn.Text = App.UserPreferences.GetString("id");
            ReflshPicImage();
        }

        private void writeXml(List<Lesson>temp)
        {
            //#region 保存字符串到文件
            //string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "LessonInfo.txt");
            //File.WriteAllText(fileName,s1);
            //#endregion
            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Lessons.xml");
            using (var writer = new StreamWriter(fileName))
            {
                var serializer = new XmlSerializer(typeof(List<Lesson>));
                serializer.Serialize(writer,temp);
            }
        }
    }

}