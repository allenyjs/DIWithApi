using System;
using System.Text;
using System.Net;
using System.Collections.Specialized;
using System.Configuration;
using Newtonsoft.Json;

namespace DIWithApi.Service
{
    public class ApiClient<T>
    {
        public WebClient client { get; set; }
        public NameValueCollection nvc { get; set; }

        private string QueryStr { get; set; }
        public byte[] buffer { get; set; }
        public string result { get; set; }
        /// <summary>
        /// 組織QueryStr
        /// </summary>
        public void generateQueryStr()
        {
            #region 加入QueryString
            this.QueryStr = string.Empty;
            if (nvc.Count > 0)
            {
                this.QueryStr = "?";
                int i = 0;
                foreach (string ParamKey in nvc.AllKeys)
                {
                    var x = nvc[ParamKey];
                    if (nvc[ParamKey] != null && !string.IsNullOrEmpty(ParamKey))
                    {
                        if (i > 0)
                        {
                            this.QueryStr += "&";
                        }
                        this.QueryStr += ParamKey + "=" + nvc[ParamKey];
                        i++;
                    }
                }
            }

            #endregion
        }
        public ApiClient()
        {
            client = new WebClient();
            client.Encoding = Encoding.UTF8;

            #region 埋apiKey在Header裡面
            string apiKey = string.Format("apiKey:{0}", GetAppKey.keyVal("apiKey"));
            client.Headers.Add(apiKey);
            #endregion

            nvc = new NameValueCollection();
            buffer = null;
            result = string.Empty;
        }

        /// <summary>
        /// Call WebAPI 並依照輸入的型別取回結果
        /// </summary>
        /// <param name="url">API位置</param>
        /// <param name="TModel">輸出型別的Model</param>
        /// <param name="MethodCode">HTTP Method</param>
        /// <returns></returns>
        public T ApiResult(string url, T TModel, HttpMethod MethodCode = HttpMethod.Get, SendFormat FormatCode = SendFormat.SeparateSeparately)
        {
            try
            {
                switch (MethodCode)
                {
                    case HttpMethod.Post:
                        this.result = Encoding.UTF8.GetString(client.UploadValues(url, "POST", nvc));
                        break;
                    case HttpMethod.Put:
                        client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                        if (FormatCode == SendFormat.SeparateSeparately)
                        {
                            generateQueryStr();
                            url += this.QueryStr;
                        }
                        this.result = Encoding.UTF8.GetString(client.UploadValues(url, "PUT", nvc));
                        break;
                    case HttpMethod.Delete:
                        if (FormatCode == SendFormat.SeparateSeparately)
                        {
                            generateQueryStr();
                            url += this.QueryStr;
                        }
                        this.result = Encoding.UTF8.GetString(client.UploadValues(url, "DELETE", nvc));
                        break;
                    default:
                        generateQueryStr();
                        url += this.QueryStr;
                        this.result = client.DownloadString(url);
                        break;
                }

                //當欄位值為null時忽略不轉成Json
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                };


                if (typeof(T) == typeof(string))
                {
                    TModel = (T)Convert.ChangeType(this.result, typeof(T));
                }
                else
                {
                    TModel = JsonConvert.DeserializeObject<T>(this.result, settings);
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            nvc.Clear();
            return TModel;
        }
        public string ApiPath(string ApiActionName = "")
        {
            string ApiDomain = GetAppKey.keyVal("apiDomain");

            return string.Format("http://{0}/api/{1}", ApiDomain, ApiActionName);
        }
    }
    public enum HttpMethod
    {
        Get = 0,
        Post = 1,
        Put = 2,
        Delete = 3
    }
    public enum SendFormat
    {
        SeparateSeparately = 0,
        FromBody = 1
    }
    public class GetAppKey
    {
        public static string keyVal(string keyName)
        {
            return ConfigurationManager.AppSettings[keyName];
        }
    }
}
