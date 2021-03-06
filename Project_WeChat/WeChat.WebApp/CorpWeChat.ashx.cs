﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using WeChat.CorpLib.Core;

namespace WeChat.WebApp
{
    /// <summary>
    /// CorpWeChat 的摘要说明
    /// </summary>
    public class CorpWeChat : IHttpHandler
    {

        log4net.ILog log = log4net.LogManager.GetLogger("Log.Logging");//获取一个日志记录器 
        CorpCore corpCore;

        public CorpWeChat()
        {
            corpCore = new CorpCore(); 

            //PubRecEventClick.OnEventClick += DoClick;
            //PubRecEventSubscribe.OnEventSubscribe += DoSubscribe;
            //PubRecMsgText.OnMsgText += DoMsgText;
        }

       

        public bool IsReusable
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            string pMsgSignature = HttpContext.Current.Request.QueryString["msg_signature"];
            string pTimeStamp = HttpContext.Current.Request.QueryString["timestamp"];
            string pNonce = HttpContext.Current.Request.QueryString["nonce"];
            string sResult = "success";

            #region for debug
            //pTimeStamp = "1483334816";
            //pNonce = "709329334";
            //pMsgSignature = "7ebda6ce61cbd4f8fbbca69e195c2768e3c9e71e";
            #endregion

            if (HttpContext.Current.Request.HttpMethod.ToUpper() == "POST")
            {
                string postStr = string.Empty;
                using (Stream stream = HttpContext.Current.Request.InputStream)
                {
                    Byte[] postBytes = new Byte[stream.Length];
                    stream.Read(postBytes, 0, (Int32)stream.Length);
                    postStr = Encoding.UTF8.GetString(postBytes);
                }

                #region for debug 
                //postStr = "<xml><ToUserName><![CDATA[gh_dc9f5aee123b]]></ToUserName><FromUserName><![CDATA[o6w0juLpCRYHJTqUTcG1L9hz-uh0]]></FromUserName><CreateTime>1484057461</CreateTime><MsgType><![CDATA[text]]></MsgType><Content><![CDATA[6]]></Content><MsgId>6373978260806390997</MsgId><Encrypt><![CDATA[dK8jouN8itPoAPz+kN1ayvWSKlKQsyEOA2A9ja0Vnq/kdsHnQgWlKYt5HAqkUwWIs2B09BVUkZuvlYgsYar6VcjjuH2kPtFbfhHYmYDKASVDEq2y4/ZLiMhdYm9aWIoTlsYTPWEuD8MDYbWmDSfFKbC5N39XbO38yqifgkHWsA9f7/NuiWTczjw4CjsjpwjlciZwPWo52YiprcafwwtQcYia+jCAyhkaaezAqFcSIchYhBQQw0RfHFb0Ig6ved8Dxw/jWqZEjAonfOiPpZiRBz0y2OvjxSIWroHAqLk9qPESA2zAW09F+HM/gX/PAmmXAvnoGn95Cgku2gv+IcF0xE1d8o8UWneRrOTMspriI9Wg01fanLK+kUQK25cfMPLjF0EZlzEoSSzZIzarKc4KefBLWRpPbX07NQYfZxT5T6Q=]]></Encrypt></xml>";
                #endregion

                log.Debug("ProcessRequest Get:" + postStr);
                if (!string.IsNullOrEmpty(postStr))
                {
                    //sResult = corpCore.ProcessMsg(postStr, pMsgSignature, pTimeStamp, pNonce);
                    string sMsg=string.Empty;

                    //Tencent.WXBizMsgCrypt wxcpt = new Tencent.WXBizMsgCrypt("A8g63ewDwHqpWPv3XWwkOHCkqGyI", "FmdgzIbwLJ56TzbABxGUmXUFIzQ472ccTSPO4D1zKLc", "wx92133d8721792124");
                    //int ret = 0;
                    //ret = wxcpt.DecryptMsg(pMsgSignature, pTimeStamp, pNonce, postStr, ref sMsg);
                    //log.Debug("ProcessRequest Get2:" + sMsg);
                     

                    log.Debug("ProcessRequest sResult:" + sResult);
                }
                HttpContext.Current.Response.Write(sResult);
                HttpContext.Current.ApplicationInstance.CompleteRequest();

            }

            else
            {
                string pEchoStr = HttpContext.Current.Request.QueryString["echostr"];
                try
                {
                    #region for debug
                    //pTimeStamp = "1483334816";
                    //pNonce = "709329334";
                    //pEchoStr = "6890308849856673530";
                    //pMsgSignature = "7ebda6ce61cbd4f8fbbca69e195c2768e3c9e71e";
                    #endregion

                    pEchoStr = corpCore.CorpAuth(pTimeStamp, pNonce, pEchoStr, pMsgSignature);
                    log.Debug("ProcessRequest pEchoStr:" + pEchoStr);
                    HttpContext.Current.Response.Write(pEchoStr);
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                    //HttpContext.Current.Response.End(); 

                }
                catch (Exception e)
                {
                    log.Error("ProcessRequest Get:", e);//写入一条新log
                }
            }
        }

       

    }
}