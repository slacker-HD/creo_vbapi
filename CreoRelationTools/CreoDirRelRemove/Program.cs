using pfcls;
using System;
using System.IO;

namespace CreoDirRelClear
{
    internal class Program
    {
        /// <summary>
        /// 批量将给定目录prt的关系清空
        /// </summary>
        /// <param name="args"> arg0: proe app path arg1 :dir of prt files</param>
        private static void Main(string[] args)
        {
            IpfcAsyncConnection asyncConnection = null;
            Istringseq Files;

            string proeapp, inputdir;
            if (args.Length != 2)
            {
                Console.Write("参数数目不正确.");
                System.Environment.Exit(0);
            }
            proeapp = args[0] + " -g:no_graphics -i:rpc_input";
            inputdir = args[1] + "\\";

            if (Directory.Exists(inputdir) == false)
            {
                Console.Write("输入文件夹不存在，程序退出.");
                System.Environment.Exit(0);
            }
            Console.WriteLine("开始清空...");
            try
            {
                asyncConnection = new CCpfcAsyncConnection().Start(proeapp, "");
            }
            catch
            {
                Console.WriteLine("无法建立与Creo的连接.");
                System.Environment.Exit(0);
            }
            Console.WriteLine("Creo会话创建完毕...");
            try
            {
                Console.WriteLine(inputdir + "读取中...");
                Files = ((IpfcBaseSession)(asyncConnection.Session)).ListFiles("*.prt", (int)EpfcFileListOpt.EpfcFILE_LIST_LATEST, inputdir);
                Console.WriteLine("prt文件列表读取完毕...");
                foreach (string file in Files)
                {
                    ClearRel(asyncConnection, file);
                }
            }
            catch
            {
                Console.WriteLine("无法读取" + inputdir + "...");
            }
            finally
            {
                try
                {
                    asyncConnection.End();
                }
                catch
                {
                }
            }
        }

        private static void ClearRel(IpfcAsyncConnection AsyncConnection, string FileFullName)
        {
            IpfcModelDescriptor descmodel;
            IpfcRetrieveModelOptions options;
            IpfcModel model;

            Console.WriteLine("打开" + FileFullName + "...");
            try
            {
                Console.WriteLine("开始清空" + FileFullName + "关系...");
                descmodel = (new CCpfcModelDescriptor()).Create((int)EpfcModelType.EpfcMDL_PART, "", null);
                descmodel.Path = FileFullName;
                options = (new CCpfcRetrieveModelOptions()).Create();
                options.AskUserAboutReps = false;
                model = ((IpfcBaseSession)(AsyncConnection.Session)).RetrieveModelWithOpts(descmodel, options);
                ((IpfcBaseSession)(AsyncConnection.Session)).CreateModelWindow(model);
            }
            catch
            {
                Console.WriteLine("无法打开" + FileFullName + "...");
                return;
            }

            try
            {
                ((IpfcRelationOwner)model).DeleteRelations();
                model.Save();
            }
            catch
            {
                Console.WriteLine("无法清空" + FileFullName + "关系...");
                return;
            }

            Console.WriteLine(FileFullName + "关系清空完毕...");

            try
            {
                ((IpfcBaseSession)(AsyncConnection.Session)).EraseUndisplayedModels();
                model.EraseWithDependencies();
            }
            catch
            {
            }
        }
    }
}
