﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WeChat.CorpLib.Model
{
    /// <summary>
    /// 事件类型，LOCATION
    /// </summary>
    public class CorpRecEventLocation : CorpRecEventBase
    {

        public CorpRecEventLocation(string sMsg)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(sMsg);
                XmlNode root = doc.FirstChild;
                this.ToUserName = root["ToUserName"].InnerText;
                this.FromUserName = root["FromUserName"].InnerText;
                this.CreateTime = root["CreateTime"].InnerText;
                this.MsgType = root["MsgType"].InnerText;
                this.Event = root["Event"].InnerText;
                this.Latitude = root["Latitude"].InnerText;
                this.Longitude = root["Longitude"].InnerText;
                this.Precision = root["Precision"].InnerText;
                this.AgentID = root["AgentID"].InnerText;
            }
            catch (Exception e)
            {
                log.Error("CorpRecEventLocation", e);
            }
        }

        public static event WechatEventHandler<CorpRecEventLocation> OnEventLocation;        //声明事件
        public override void DoProcess()
        {
            if (OnEventLocation != null)
            { //如果有对象注册 
                OnEventLocation(this);  //调用所有注册对象的方法
            }
        }

        /// <summary>
        /// 地理位置纬度
        /// </summary>
        public string Latitude { get; private set; }

        /// <summary>
        /// 地理位置经度
        /// </summary>
        public string Longitude { get; private set; }

        /// <summary>
        /// 地理位置精度
        /// </summary>
        public string Precision { get; private set; }


    }
}