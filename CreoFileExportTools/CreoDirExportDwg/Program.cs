using System;
using System.IO;
using pfcls;

namespace CreoDirExportDwg
{
    internal class Program
    {
        /// <summary>
        /// 批量将给定目录drw导出到指定目录dwg文件 
        /// </summary>
        /// <param name="args"> arg0: proe app path arg1 :dir of drw files arg2 :dir for output </param>
        private static void Main(string[] args)
        {
            IpfcAsyncConnection asyncConnection = null;
            Istringseq Files;
            string proeapp, inputdir, outputdir;
            if (args.Length != 3)
            {
                Console.Write("参数数目不正确.");
                System.Environment.Exit(0);
            }
            proeapp = args[0] + " -g:no_graphics -i:rpc_input";
            inputdir = args[1] + "\\";
            outputdir = args[2] + "\\";

            if (Directory.Exists(inputdir) == false)//如果不存在就创建file文件夹
            {
                Console.Write("输入文件夹不存在，程序退出.");
                System.Environment.Exit(0);
            }

            if (Directory.Exists(outputdir) == false)//如果不存在就创建file文件夹
            {
                Directory.CreateDirectory(outputdir);
            }

            Console.WriteLine("开始转换...");
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
                Files = ((IpfcBaseSession)(asyncConnection.Session)).ListFiles("*.drw", (int)EpfcFileListOpt.EpfcFILE_LIST_LATEST, inputdir);
                Console.WriteLine("drw文件列表读取完毕...");
                foreach (string file in Files)
                {
                    ConvertToDwg(asyncConnection, file, outputdir);
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

        private static void ConvertToDwg(IpfcAsyncConnection AsyncConnection, string FileFullName, string Outputdir)
        {
            IpfcModelDescriptor descmodel;
            IpfcRetrieveModelOptions options;
            IpfcModel model;
            IpfcDWG3DExportInstructions dwginstructions;

            Console.WriteLine("打开" + FileFullName + "...");
            try
            {
                Console.WriteLine("开始转换" + FileFullName + "...");
                descmodel = (new CCpfcModelDescriptor()).Create((int)EpfcModelType.EpfcMDL_DRAWING, "", null);
                descmodel.Path = FileFullName;
                options = (new CCpfcRetrieveModelOptions()).Create();
                options.AskUserAboutReps = false;
                model = ((IpfcBaseSession)(AsyncConnection.Session)).RetrieveModelWithOpts(descmodel, options);
                ((IpfcBaseSession)(AsyncConnection.Session)).CreateModelWindow(model);
                model.Display();
            }
            catch
            {
                Console.WriteLine("无法打开" + FileFullName + "...");
                return;
            }

            try
            {
                dwginstructions = (new CCpfcDWG3DExportInstructions()).Create();
                model.Export(Outputdir + model.InstanceName.ToLower() + ".dwg", (IpfcExportInstructions)dwginstructions);
            }
            catch
            {
                Console.WriteLine("无法转换" + FileFullName + "...");
                return;
            }

            Console.WriteLine(FileFullName + "转换完毕...");

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
