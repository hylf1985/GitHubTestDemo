using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Config;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEE.EpsonRobot
{
    //实现你的AppServer和AppSession
    //http://docs.supersocket.net/v1-6/zh-CN/Implement-your-AppServer-and-AppSession

    /*什么是AppServer?
      AppServer 代表了监听客户端连接，承载TCP连接的服务器实例。理想情况下，我们可以通过AppServer实例获取任何你想要的客户端连接，服务器级别的操作和逻辑应该定义在此类之中。
     *阿笨的理解就是AppServer是Socket服务器。
     */

    public class MyAppServer : AppServer<MyAppSession>
    {
        protected override bool Setup(IRootConfig rootConfig, IServerConfig config)
        {
            return base.Setup(rootConfig, config);
        }

        protected override void OnStarted()
        {
            base.OnStarted();
        }

        protected override void OnStopped()
        {
            base.OnStopped();
        }
    }

    /*
    * 什么是AppSession?
      AppSession 代表一个和客户端的逻辑连接，基于连接的操作应该定于在该类之中。你可以用该类的实例发送数据到客户端，接收客户端发送的数据或者关闭连接。
     * 阿笨的理解就是：AppSession是Socket中的一个连接会话。
    */

    public class MyAppSession : AppSession<MyAppSession>
    {
        protected override void OnSessionStarted()
        {
            this.Send("Connected\r\n");
        }

        protected override void HandleUnknownRequest(StringRequestInfo requestInfo)
        {
            this.Send("push Unknow request\r\n");
        }

        protected override void HandleException(Exception e)
        {
            this.Send("push Application error: {0}\r\n", e.Message);
        }

        protected override void OnSessionClosed(CloseReason reason)
        {
            base.OnSessionClosed(reason);
        }
    }
}
