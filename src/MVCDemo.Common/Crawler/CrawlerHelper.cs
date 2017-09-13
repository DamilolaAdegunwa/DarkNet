using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MVCDemo.Common.Crawler
{
    /// <summary>
    /// 爬虫类
    /// </summary>
    public class CrawlerHelper
    {
        /// <summary>
        /// 
        /// </summary>
        private static Common.Logger.Logger logger = new Common.Logger.Logger(typeof(CrawlerHelper));
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="URL"></param>
        /// <param name="xpath"></param>
        /// <returns></returns>
        public static T GetModelByURL<T>(string URL, List<CrawlerRule> ruleList, T enity) where T : class
        {
            logger.Info("down file by ");
            string strHTML = Http.HttpHelper.DownloadHtml(URL, Encoding.GetEncoding("gbk"));
            try
            {
                //初始化document
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(strHTML);
                ruleList.ForEach(u =>
                {
                    //拿到规则,找到节点数据
                    string rulePath = u.Rule;
                    HtmlNode node = doc.DocumentNode.SelectSingleNode(rulePath);
                    if (node == null)
                    {
                        logger.Warn($"该规则:{u.Rule} 未找到数据");

                    }
                    else
                    {
                        var nText = node.InnerHtml.Replace(@"&nbsp;", " ").Replace(@"<br>", "\n");
                        string propertyValue = nText;
                        if (u.RuleType == RuleType.Href)
                        {
                            propertyValue = node.Attributes["href"].Value;
                        }
                        PropertyInfo propertyInfo = typeof(T).GetProperty(u.PropertyName);
                        if (propertyInfo == null)
                        {
                            logger.Warn($"类型:{typeof(T).Name },没有该属性:{u.PropertyName} ");
                        }
                        propertyInfo.SetValue(enity, propertyValue);
                    }

                });
                return enity;
            }
            catch (Exception ex)
            {
                logger.Error($"数据抓取异常:{ex.Message.ToString()}");
                throw;
            }
        }

        /// <summary>
        /// 获取单页面的数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetSinglePage<T>(List<CrawlerRule> ruleList, string strURL, Encoding encoding, T obj) where T : class
        {
            string strHTML = Http.HttpHelper.DownloadHtml(strURL, encoding);
            try
            {
                //初始化document
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(strHTML);
                ruleList.ForEach(u =>
                {
                    //拿到规则,找到节点数据
                    string rulePath = u.Rule;
                    HtmlNode node = doc.DocumentNode.SelectSingleNode(rulePath);
                    if (node == null)
                    {
                        logger.Warn($"该规则:{u.Rule} 未找到数据");
                    }
                    string content = string.Empty;

                    switch (u.RuleType)
                    {
                        case RuleType.Text:
                            content = node.InnerText;
                            break;
                        case RuleType.Html:
                            content = node.InnerHtml;
                            break;
                        case RuleType.Href:
                            content = node.Attributes["href"].Value;
                            break;
                    }
                    PropertyInfo propertyInfo = typeof(T).GetProperty(u.PropertyName);
                    if (propertyInfo == null)
                    {
                        logger.Warn($"类型:{typeof(T).Name },没有该属性:{u.PropertyName} ");
                    }
                    propertyInfo.SetValue(obj, content);
                });
                return obj;
            }

            catch (Exception ex)
            {
                logger.Error($"数据抓取异常:{ex.Message.ToString()}");
                throw;
            }
        }

        /// <summary>
        /// 通过一起起始页面来慢慢查找下一页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> GetMultiPageByStart<T>() where T : class
        {
            return default(List<T>);
        }

    }
}
