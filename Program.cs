using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;
using System.Net;
using Wesley.Crawler.SimpleCrawler.Models;
//这是最基础的测试版本

namespace Wesley.Crawler.SimpleCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            //测试代理IP是否生效：http://1212.ip138.com/ic.asp

            //测试当前爬虫的User-Agent：http://www.whatismyuseragent.net

            //1.并发抓取示例
            //ConcurrentCrawler();

            SaveCrawler();

            Console.ReadKey();
        }





          /// <summary>
        /// 并发抓取示例
        /// </summary>
        public static void ConcurrentCrawler() {
            var hotelList = new List<Hotel>() {
                new Hotel { HotelName="遵义浙商酒店", Uri=new Uri("http://hotels.ctrip.com/hotel/4983680.html?isFull=F") },
                new Hotel { HotelName="遵义森林大酒店", Uri=new Uri("http://hotels.ctrip.com/hotel/1665124.html?isFull=F") },
            };
            var hotelCrawler = new SimpleCrawler();
            hotelCrawler.OnStart += (s, e) =>
            {
                Console.WriteLine("爬虫开始抓取地址：" + e.Uri.ToString());
            };
            hotelCrawler.OnError += (s, e) =>
            {
                Console.WriteLine("爬虫抓取出现错误：" + e.Uri.ToString() + "，异常消息：" + e.Exception.Message);
            };
            hotelCrawler.OnCompleted += (s, e) =>
            {
                Console.WriteLine();
                Console.WriteLine("===============================================");
                Console.WriteLine("爬虫抓取任务完成！");
                Console.WriteLine("耗时：" + e.Milliseconds + "毫秒");
                Console.WriteLine("线程：" + e.ThreadId);
                Console.WriteLine("地址：" + e.Uri.ToString());
            };
            Parallel.For(0, 2, (i) =>
            {
                var hotel = hotelList[i];
                hotelCrawler.Start(hotel.Uri);
            });
        }

        public static void SaveCrawler()
        {

            var Url = "http://finance.sina.com.cn/china/2017-07-15/doc-ifyiakur8982765.shtml";//定义爬虫入口URL
            var saveCrawler = new SimpleCrawler();//调用刚才写的爬虫程序
            var weblist = new List<Web>();
            saveCrawler.OnStart += (s, e) =>
            {
                Console.WriteLine("爬虫开始抓取地址：" + e.Uri.ToString());
            };
            saveCrawler.OnError += (s, e) =>
            {
                Console.WriteLine("爬虫抓取出现错误：" + e.Uri.ToString() + "，异常消息：" + e.Exception.Message);
            };
            saveCrawler.OnCompleted += (s, e) =>
            {

                var txt = e.PageSource;
                SaveContents(txt);

                
                Console.WriteLine("===============================================");
                Console.WriteLine("耗时：" + e.Milliseconds + "毫秒");
                Console.WriteLine("线程：" + e.ThreadId);
                Console.WriteLine("地址：" + e.Uri.ToString());
            };
            saveCrawler.Start(new Uri(Url)).Wait();//没被封锁就别使用代理：60.221.50.118:8090
         }
        public static void SaveContents(string html)
        {
            StreamWriter fs = new StreamWriter(@"C:\Users\Administrator\Desktop\PageExtractor\6566\01.txt");
            fs.Write(html);


        }

    }








   



}


