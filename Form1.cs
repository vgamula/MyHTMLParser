using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using HtmlAgilityPack;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace MyHtmlParser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static HtmlDocument GetWebPageHtmlFromUrl(string url)
        {
            HtmlDocument doc =  new HtmlDocument();
            StreamReader reader = new StreamReader(WebRequest.Create(url).GetResponse().GetResponseStream(), Encoding.UTF8); //put your encoding            
            doc.Load(reader);
            return doc;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            var a = GetWebPageHtmlFromUrl(@"http://anekdotu.org.ua/random");
            var data =
                a.GetElementbyId("content").ChildNodes.Where(t => t.Name == "div" && t.Attributes["class"].Value == "entry").ToList();
            foreach (var htmlNode in data)
            {
                listBox1.Items.Add(
                    new Anecdote()
                        {
                            Title = HtmlEntity.DeEntitize(htmlNode.ChildNodes[1].InnerText.Trim()),
                            Text = HtmlEntity.DeEntitize(htmlNode.InnerText.Substring(htmlNode.ChildNodes[1].InnerText.Trim().Length+2))
                        }
                    );
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            richTextBox1.Text = (listBox1.SelectedItem as Anecdote).Text;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Load("http://anekdotu.org.ua/wp-content/themes/pop-blue/images/logo.gif");
        }
    }
}