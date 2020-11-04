using pfcls;
using System;
using System.Timers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CSharpMenuTest
{
    class CreoFunction
    {
        private IpfcAsyncConnection asyncConnection = null;
        private Timer eventTimer; //定时器，用于定时处理asyncConnection.EventProcess，防止Creo无法处理事件导致程序锁死
        /// <summary>
        /// 连接现有会话并添加菜单
        /// </summary>
        /// <returns>是否连接成功</returns>
        public bool ConnectCreo()
        {
            try
            {
                if (asyncConnection == null || asyncConnection.IsRunning() == true)
                {
                    asyncConnection = new CCpfcAsyncConnection().Connect(null, null, null, null);
                    AddEventProcess();       
                    AddPushButtonMenu();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 启动定时器
        /// </summary>
        private void AddEventProcess()
        {
            eventTimer = new Timer(10);
            eventTimer.Enabled = true;
            eventTimer.Elapsed += new ElapsedEventHandler(TimeElapsed);
         }

        /// <summary>
        /// 定时处理asyncConnection事件的loop，Full Asynchronous Mode必须
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimeElapsed(object sender, ElapsedEventArgs e)
        {
            asyncConnection.EventProcess();
        }



        /// <summary>
        /// 按钮的响应函数类，必须这么继承,每个菜单项都要有一个
        /// </summary>
        private class TestButtonUICommandActionListener : IpfcUICommandActionListener, IpfcActionListener, ICIPClientObject
        {

            private IpfcAsyncConnection asyncConnection;

            public TestButtonUICommandActionListener(ref IpfcAsyncConnection aC)
            {
                asyncConnection = aC;
            }
            public string GetClientInterfaceName()
            {
                return "IpfcUICommandActionListener";
            }

            // '' <summary>
            // '' 点击按钮的响应函数
            // '' </summary>
            public void OnCommand()
            {
                asyncConnection.Session.UIShowMessageDialog("触发了自定义命令。", null);
            }
        }

        /// <summary>
        /// 判断按钮是否可用的UICommandAccessListener,每个菜单项都要有一个
        /// </summary>
        private class TestButtonUICommandAccessListener : ICIPClientObject, IpfcUICommandAccessListener, IpfcActionListener
        {

            private IpfcAsyncConnection asyncConnection;
            public TestButtonUICommandAccessListener(ref IpfcAsyncConnection aC)
            {
                asyncConnection = aC;
            }
            /// <summary>
            /// 必须继承
            /// </summary>
            /// <returns></returns>
            public string GetClientInterfaceName()
            {
                return "IpfcUICommandAccessListener";
            }

            /// <summary>
            /// 判断按钮在当前会话是否可用，这里只是简单判断必须是打开DRWING才可用
            /// </summary>
            /// <param name="_AllowErrorMessages"></param>
            /// <returns></returns>
            public int OnCommandAccess(bool _AllowErrorMessages)
            {
                return (int)EpfcCommandAccess.EpfcACCESS_AVAILABLE;
            }
        }

        /// <summary>
        /// 添加一个菜单项
        /// </summary>
        private void AddPushButtonMenu()
        {
            IpfcUICommand UICommand;
            IpfcUICommandActionListener UICommandActionListener;
            IpfcUICommandAccessListener UICommandAccessListener;
            try
            {
                //整个过程与Toolkit添加菜单按钮的过程类似
                //创建IpfcUICommandActionListener对象
                UICommandActionListener = new TestButtonUICommandActionListener(ref asyncConnection);
                //添加Command,字符串唯一
                UICommand = asyncConnection.Session.UICreateCommand("TEST", UICommandActionListener);
                //创建UICommandAccessListener
                UICommandAccessListener = new TestButtonUICommandAccessListener(ref asyncConnection);
                //判断按钮是否可用函数
                ((IpfcActionSource)UICommand).AddActionListener((IpfcActionListener)UICommandAccessListener);
                /****************************************************/
                //添加自定义菜单按钮,最后一个是消息文件，确定位置，同时系统限制长度不能超过40
                /****************************************************/
                asyncConnection.Session.UIAddButton(UICommand, "Windows", null, "MyPushButton", "MyPushButtonHelp", "D:\\ProeRes\\msg.txt");
            }
            catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }
    }
}
