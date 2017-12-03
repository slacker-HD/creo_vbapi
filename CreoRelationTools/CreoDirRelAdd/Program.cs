using pfcls;
using System;
using System.IO;
using System.Text;

namespace CreoDirRelAdd
{
    internal class Program
    {
        /// <summary>
        /// 批量将给定目录prt的添加指定txt文件下的关系
        /// </summary>
        /// <param name="args"> arg0: proe app path arg1: dir of prt files arg2: txt file contents relations </param>
        private static void Main(string[] args)
        {
            IpfcAsyncConnection asyncConnection = null;
            Istringseq Files;
            string[] relations;
            string proeapp, inputdir, relfile;
            if (args.Length != 3)
            {
                Console.Write("参数数目不正确.");
                System.Environment.Exit(0);
            }
            proeapp = args[0] + " -g:no_graphics -i:rpc_input";
            inputdir = args[1] + "\\";
            relfile = args[2];
            if (Directory.Exists(inputdir) == false)
            {
                Console.Write("输入文件夹不存在，程序退出.");
                System.Environment.Exit(0);
            }
            if (File.Exists(relfile) == false)
            {
                Console.Write("输入关系文件不存在，程序退出.");
                System.Environment.Exit(0);
            }
            relations = Getrels(relfile);
            Console.WriteLine("开始添加...");
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
                    AddRelations(asyncConnection, file,relations );
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

        private static string[] Getrels(string filename)
        {
            return File.ReadAllLines(filename, Encoding.UTF8);
        }

        private static void AddRelations(IpfcAsyncConnection AsyncConnection, string FileFullName, string[] rels)
        {
            IpfcModelDescriptor descmodel;
            IpfcRetrieveModelOptions options;
            IpfcModel model;
            Cstringseq relations = new Cstringseq();
            IpfcRelationOwner relationOwner;
            Cstringseq originrels; 
            Console.WriteLine("打开" + FileFullName + "...");
            try
            {
                Console.WriteLine("开始添加" + FileFullName + "关系...");
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


            relationOwner = (IpfcRelationOwner)model;
            originrels = relationOwner.get_Relations();
            try
            {
                for (int i = 0; i <= originrels.Count - 1; i++)
                {
                    relations.Append(originrels[i]);
                }

                foreach (string line in rels)
                {
                    relations.Append(line);
                }
                relationOwner.set_Relations(relations);
                model.Save();
            }
            catch
            {
                Console.WriteLine("无法添加" + FileFullName + "关系...");
                return;
            }

            Console.WriteLine(FileFullName + "关系添加完毕...");

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