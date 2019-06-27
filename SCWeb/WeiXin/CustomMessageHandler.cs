﻿using Senparc.NeuChar.Entities;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Entities.Request;
using Senparc.Weixin.MP.MessageHandlers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SCWeb.WeiXin
{
    public class CustomMessageHandler : MessageHandler<CustomMessageContext>
    {
        public CustomMessageHandler(Stream inputStream, PostModel postModel)
           : base(inputStream, postModel)
        {

        }

        public override IResponseMessageBase DefaultResponseMessage(IRequestMessageBase requestMessage)
        {
            var responseMessage = base.CreateResponseMessage<ResponseMessageText>(); //ResponseMessageText也可以是News等其他类型
            responseMessage.Content = "这条消息来自DefaultResponseMessage。";
            return responseMessage;
        }
        public override IResponseMessageBase OnTextRequest(RequestMessageText requestMessage)
        {
            var responseMessage = base.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "您的OpenID是：" + requestMessage.FromUserName      //这里的requestMessage.FromUserName也可以直接写成base.WeixinOpenId
                                    + "。\r\n您发送了文字信息：" + requestMessage.Content;  //\r\n用于换行，requestMessage.Content即用户发过来的文字内容
            return responseMessage;
        }
        public override void OnExecuting()
        {
            if (RequestMessage.FromUserName == "wx1ddae1470624f4ac")
            {
                CancelExcute = true; //终止此用户的对话

                //如果没有下面的代码，用户不会收到任何回复，因为此时ResponseMessage为null

                //添加一条固定回复
                var responseMessage = CreateResponseMessage<ResponseMessageText>();
                responseMessage.Content = "Hey！你已经被拉黑啦！";

                ResponseMessage = responseMessage;//设置返回对象
            }
        }
    }
}