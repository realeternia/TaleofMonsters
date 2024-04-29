using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace NarlonLib.Net
{
    class NLWebPage
    {      
        #region ˽�г�Ա
        private Uri m_uri;   //url  
        private List<Link> m_links;    //����ҳ�ϵ�����  
        private string m_title;        //����  
        private string m_html;         //HTML����  
        private string m_outstr;       //��ҳ������Ĵ��ı�  
        private bool m_good;           //��ҳ�Ƿ����  
        private int m_pagesize;       //��ҳ�Ĵ�С  
        private static Dictionary<string, CookieContainer> webcookies = new Dictionary<string, CookieContainer>();//���������ҳ��Cookie  

        #endregion

        #region ����

        /// <summary>  
        /// ͨ�������Կɻ�ñ���ҳ����ַ��ֻ��  
        /// </summary>  
        public string URL
        {
            get
            {
                return m_uri.AbsoluteUri;
            }
        }

        /// <summary>  
        /// ͨ�������Կɻ�ñ���ҳ�ı��⣬ֻ��  
        /// </summary>  
        public string Title
        {
            get
            {
                if (m_title == "")
                {
                    Regex reg = new Regex(@"(?m)<title[^>]*>(?<title>(?:\w|\W)*?)</title[^>]*>", RegexOptions.Multiline | RegexOptions.IgnoreCase);
                    Match mc = reg.Match(m_html);
                    if (mc.Success)
                        m_title = mc.Groups["title"].Value.Trim();
                }
                return m_title;
            }
        }
        public string M_html
        {
            get
            {
                if (m_html == null)
                {
                    m_html = "";
                }
                return m_html;
            }
        }
        /// <summary>  
        /// �����Ի�ñ���ҳ������������Ϣ��ֻ��  
        /// </summary>  
        public List<Link> Links
        {
            get
            {
                if (m_links.Count == 0) getLinks();
                return m_links;
            }
        }


        /// <summary>  
        /// �����Է��ر���ҳ��ȫ�����ı���Ϣ��ֻ��  
        /// </summary>  
        public string Context
        {
            get
            {
                if (m_outstr == "") getContext(Int16.MaxValue);
                return m_outstr;
            }
        }

        /// <summary>  
        /// �����Ի�ñ���ҳ�Ĵ�С  
        /// </summary>  
        public int PageSize
        {
            get
            {
                return m_pagesize;
            }
        }
        /// <summary>  
        /// �����Ի�ñ���ҳ������վ������  
        /// </summary>  
        public List<Link> InsiteLinks
        {
            get
            {
                return getSpecialLinksByUrl("^http://" + m_uri.Host, Int16.MaxValue);
            }
        }

        /// <summary>  
        /// �����Ա�ʾ����ҳ�Ƿ����  
        /// </summary>  
        public bool IsGood
        {
            get
            {
                return m_good;
            }
        }
        /// <summary>  
        /// �����Ա�ʾ��ҳ�����ڵ���վ  
        /// </summary>  
        public string Host
        {
            get
            {
                return m_uri.Host;
            }
        }
        #endregion


        /// <summary>  
        /// ��HTML�����з�����������Ϣ  
        /// </summary>  
        /// <returns>List<Link></returns>  
        private void getLinks()
        {
            if (m_links.Count == 0)
            {
                Regex[] regex = new Regex[2];
                regex[0] = new Regex(@"<a.*?href\s*=""(?<URL>[^""]*).*?>(?<title>[^<]*)</a>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                regex[1] = new Regex("<[i]*frame[^><]+src=(\"|')?(?<url>([^>\"'\\s)])+)(\"|')?[^>]*>", RegexOptions.IgnoreCase);

                for (int i = 0; i < 2; i++)
                {
                    Match match = regex[i].Match(m_html);
                    while (match.Success)
                    {
                        try
                        {
                            string url = HttpUtility.UrlDecode(new Uri(m_uri, match.Groups["URL"].Value).AbsoluteUri);

                            string text = "";
                            if (i == 0) text = new Regex("(<[^>]+>)|(\\s)|( )|&|\"", RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(match.Groups["title"].Value, "");

                            Link link = new Link();
                            link.Text = text;
                            link.NavigateUrl = url;

                            m_links.Add(link);
                        }
                        catch (Exception ex) { Console.WriteLine(ex.Message); };
                        match = match.NextMatch();
                    }
                }
            }
        }
        /// <summary>  
        /// ��˽�з�����һ��HTML�ı�����ȡ��һ�������Ĵ��ı�  
        /// </summary>  
        /// <param name="instr">HTML����</param>  
        /// <param name="firstN">��ȡ��ͷ�����ٸ���</param>  
        /// <param name="withLink">�Ƿ�Ҫ�����������</param>  
        /// <returns>���ı�</returns>  
        private string getFirstNchar(string instr, int firstN, bool withLink)
        {
            if (m_outstr == "")
            {
                m_outstr = instr.Clone() as string;
                m_outstr = new Regex(@"(?m)<script[^>]*>(\w|\W)*?</script[^>]*>", RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(m_outstr, "");
                m_outstr = new Regex(@"(?m)<style[^>]*>(\w|\W)*?</style[^>]*>", RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(m_outstr, "");
                m_outstr = new Regex(@"(?m)<select[^>]*>(\w|\W)*?</select[^>]*>", RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(m_outstr, "");
                if (!withLink) m_outstr = new Regex(@"(?m)<a[^>]*>(\w|\W)*?</a[^>]*>", RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(m_outstr, "");
                Regex objReg = new System.Text.RegularExpressions.Regex("(<[^>]+?>)| ", RegexOptions.Multiline | RegexOptions.IgnoreCase);
                m_outstr = objReg.Replace(m_outstr, "");
                Regex objReg2 = new System.Text.RegularExpressions.Regex("(\\s)+", RegexOptions.Multiline | RegexOptions.IgnoreCase);
                m_outstr = objReg2.Replace(m_outstr, " ");

            }
            return m_outstr.Length > firstN ? m_outstr.Substring(0, firstN) : m_outstr;
        }


        #region �����ķ�
        /// <summary>  
        /// �˹��з�����ȡ��ҳ��һ�������Ĵ��ı���������������  
        /// </summary>  
        /// <param name="firstN">����</param>  
        /// <returns></returns>  
        public string getContext(int firstN)
        {
            return getFirstNchar(m_html, firstN, true);
        }

        /// <summary>  
        /// �˹��з����ӱ���ҳ����������ȡһ�����������ӣ������ӵ�URL����ĳ����ʽ  
        /// </summary>  
        /// <param name="pattern">����ʽ</param>  
        /// <param name="count">���ص����ӵĸ���</param>  
        /// <returns>List<Link></returns>  
        public List<Link> getSpecialLinksByUrl(string pattern, int count)
        {
            if (m_links.Count == 0) getLinks();
            List<Link> SpecialLinks = new List<Link>();
            List<Link>.Enumerator i;
            i = m_links.GetEnumerator();
            int cnt = 0;
            while (i.MoveNext() && cnt < count)
            {
                if (new Regex(pattern, RegexOptions.Multiline | RegexOptions.IgnoreCase).Match(i.Current.NavigateUrl).Success)
                {
                    SpecialLinks.Add(i.Current);
                    cnt++;
                }
            }
            return SpecialLinks;
        }

        /// <summary>  
        /// �˹��з����ӱ���ҳ����������ȡһ�����������ӣ������ӵ���������ĳ����ʽ  
        /// </summary>  
        /// <param name="pattern">����ʽ</param>  
        /// <param name="count">���ص����ӵĸ���</param>  
        /// <returns>List<Link></returns>  
        public List<Link> getSpecialLinksByText(string pattern, int count)
        {
            if (m_links.Count == 0) getLinks();
            List<Link> SpecialLinks = new List<Link>();
            List<Link>.Enumerator i;
            i = m_links.GetEnumerator();
            int cnt = 0;
            while (i.MoveNext() && cnt < count)
            {
                if (new Regex(pattern, RegexOptions.Multiline | RegexOptions.IgnoreCase).Match(i.Current.Text).Success)
                {
                    SpecialLinks.Add(i.Current);
                    cnt++;
                }
            }
            return SpecialLinks;
        }

        /// <summary>  
        /// �⹫�з�����ȡ����ҳ�Ĵ��ı�������ĳ����ʽ������  
        /// </summary>  
        /// <param name="pattern">����ʽ</param>  
        /// <returns>��������</returns>  
        public string getSpecialWords(string pattern)
        {
            if (m_outstr == "") getContext(Int16.MaxValue);
            Regex regex = new Regex(pattern, RegexOptions.Multiline | RegexOptions.IgnoreCase);
            Match mc = regex.Match(m_outstr);
            if (mc.Success)
                return mc.Groups[1].Value;
            return string.Empty;
        }
        #endregion

        #region ���캯��

        private void Init(string _url)
        {
            try
            {
                m_uri = new Uri(_url);
                m_links = new List<Link>();
                m_html = "";
                m_outstr = "";
                m_title = "";
                m_good = true;
                if (_url.EndsWith(".rar") || _url.EndsWith(".dat") || _url.EndsWith(".msi"))
                {
                    m_good = false;
                    return;
                }
                HttpWebRequest rqst = (HttpWebRequest)WebRequest.Create(m_uri);
                rqst.AllowAutoRedirect = true;
                rqst.MaximumAutomaticRedirections = 3;
                rqst.UserAgent = "Mozilla/4.0 (compatible; MSIE 5.01; Windows NT 5.0)";
                rqst.KeepAlive = true;
                rqst.Timeout = 10000;
                lock (NLWebPage.webcookies)
                {
                    if (NLWebPage.webcookies.ContainsKey(m_uri.Host))
                        rqst.CookieContainer = NLWebPage.webcookies[m_uri.Host];
                    else
                    {
                        CookieContainer cc = new CookieContainer();
                        NLWebPage.webcookies[m_uri.Host] = cc;
                        rqst.CookieContainer = cc;
                    }
                }
                HttpWebResponse rsps = (HttpWebResponse)rqst.GetResponse();
                Stream sm = rsps.GetResponseStream();
                if (!rsps.ContentType.ToLower().StartsWith("text/") || rsps.ContentLength > 1 << 22)
                {
                    rsps.Close();
                    m_good = false;
                    return;
                }
                Encoding cding = System.Text.Encoding.Default;
                string contenttype = rsps.ContentType.ToLower();
                int ix = contenttype.IndexOf("charset=");
                if (ix != -1 || Host == "www.moj.gov.cn")
                {
                    try
                    {
                        cding = System.Text.Encoding.GetEncoding(rsps.ContentType.Substring(ix + "charset".Length + 1));
                    }
                    catch
                    {
                        cding = Encoding.Default;
                    }
                    if (Host == "www.moj.gov.cn")
                    {
                        cding = Encoding.UTF8;
                    }

                    //�ô���������� �е���Ҫ����  
                    //m_html = HttpUtility.HtmlDecode(new StreamReader(sm, cding).ReadToEnd());  
                    m_html = new StreamReader(sm, cding).ReadToEnd();

                }
                else
                {
                    //�ô���������� �е���Ҫ����  
                    //m_html = HttpUtility.HtmlDecode(new StreamReader(sm, cding).ReadToEnd());  

                    m_html = new StreamReader(sm, cding).ReadToEnd();
                    Regex regex = new Regex("charset=(?<cding>[^=]+)?\"", RegexOptions.IgnoreCase);
                    string strcding = regex.Match(m_html).Groups["cding"].Value;
                    try
                    {
                        cding = Encoding.GetEncoding(strcding);
                    }
                    catch
                    {
                        cding = Encoding.Default;
                    }
                    byte[] bytes = Encoding.Default.GetBytes(m_html.ToCharArray());
                    m_html = cding.GetString(bytes);
                    if (m_html.Split('?').Length > 100)
                    {
                        m_html = Encoding.Default.GetString(bytes);
                    }
                }
                m_pagesize = m_html.Length;
                m_uri = rsps.ResponseUri;
                rsps.Close();
            }
            catch (Exception)
            {

            }
        }
        public NLWebPage(string _url)
        {
            string uurl = "";
            try
            {
                uurl = Uri.UnescapeDataString(_url);
                _url = uurl;
            }
            catch { };
            Init(_url);
        }
        #endregion
    }

     internal class Link
    {
        public string Text;
        public string NavigateUrl;
    }
}
