using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;

namespace WebMonthMvc
{
    public class HttpClientHelper
    {
        /*
         * Method请求方式 Get put delete post
         * path请求路径 api/student 删除时 api/student/3
         * content(添加和修改时)需要保存的数据 放进HttpContent中的 Json字符串 {'Name':'张三','Age':19} 
         */

        public static string Sender(string method, string path, string content)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:56092/");
            HttpResponseMessage responseMessage = null;
            switch (method)
            {
                case "get"://查询
                    responseMessage = client.GetAsync(path).Result;
                    break;
                case "post"://添加
                    HttpContent hcontent = new StringContent(content);
                    hcontent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    responseMessage = client.PostAsync(path, hcontent).Result;
                    break;
                case "delete"://删除
                    responseMessage = client.DeleteAsync(path).Result;
                    break;
                case "put"://修改
                    HttpContent hcontent1 = new StringContent(content);
                    hcontent1.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    responseMessage = client.PutAsync(path, hcontent1).Result;
                    break;
                default:
                    break;
            }
            //分析请求的结果(返回的消息对象)
            if (responseMessage.IsSuccessStatusCode == true)
            {
                //result 添加修改删除 受影响行数1
                //result 查询时 返回json数组字符串
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                return result;
            }
            else
            {
                return "失败";
            }
        }
    }
}