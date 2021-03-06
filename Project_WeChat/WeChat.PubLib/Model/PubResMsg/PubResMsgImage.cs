﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeChat.PubLib.Model
{
    /// <summary>
    /// 回复图片消息
    /// </summary>
    public class PubResMsgImage : PubResMsgBase
    {
        public PubResMsgImage()
        {
            this.MsgType = "image";
        }

        public PubResMsgImage(PubRecMsgBase instanse)
        {
            this.MsgType = "image";
            this.CreateTime = instanse.CreateTime;
            this.FromUserName = instanse.ToUserName;
            this.ToUserName = instanse.FromUserName; 
        }
        /// <summary>
        /// 通过素材管理中的接口上传多媒体文件，得到的id。
        /// </summary>
        public string MediaId { get; set; }
    }
}
