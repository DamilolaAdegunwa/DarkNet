using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using MVCDemo.IBLL;
using Dark.Web.Controllers;

namespace MVCDemo.Controllers
{
    public class HomeController : BaseController
    {
        //private IUserService _userService;

        public HomeController()
        {
            
        }



        public ActionResult Index()
        {
            
            //GetBookList();
            //将数据写入到文本中
            try
            {
                //_userService.Add();

                //WordText();

                //var list = GetBookList();
                //FileStream fs = new FileStream(@"d:\\11.txt", FileMode.Append);
                //using (StreamWriter writer = new StreamWriter(fs))
                //{
                //    list.ForEach(u =>
                //    {
                //        writer.WriteLine(u.Title);
                //        writer.WriteLine(u.Content);
                //        writer.WriteLine("");

                //    });
                //}
            }
            catch (Exception )
            {

                throw;
            }
           
            return View();
        }

      

  


        public ActionResult TestIndex()
        {
            return View();
        }

        //private List<Book> GetBookList()
        //{
        //    string startPage = "/0_695/315880.html";
        //    List<Book> bookList = new List<Book>();
        //    string URL = "http://www.biqugexsw.com";
        //    List<CrawlerRule> rules = new List<CrawlerRule>() {
        //        new CrawlerRule() {Rule="//*[@id=\"wrapper\"]/div[4]/div/div[2]/h1",PropertyName="Title" },
        //        new CrawlerRule() {Rule="//*[@id=\"content\"]",PropertyName="Content" },
        //        new CrawlerRule() {Rule="//*[@id=\"wrapper\"]/div[4]/div/div[5]/a[4]",RuleType=RuleType.Href ,PropertyName="NextPath"}  };
        //    getBook(startPage, URL, bookList, rules);
        //    return bookList;
        //    //*[@id="footlink"]/a[5]
        //}

        //private void getBook(string page, string url, List<Book> bookList, List<CrawlerRule> rules)
        //{
        //    var book = new Book();
        //    CrawlerHelper.GetModelByURL<Book>(url + page, rules, book);
        //    if (book != null&&!string.IsNullOrEmpty(book.Content))
        //    {
        //        bookList.Add(book);
        //        if (book.NextPath != "" && book.NextPath.Contains("html"))
        //        {
        //            getBook(book.NextPath, url, bookList, rules);
        //        }
        //    }
        //}

    }



    public class Book
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public string NextPath { get; set; }
    }

    public class TestAttribute : Attribute
    {
        public string Name { get; set; }
    }
}