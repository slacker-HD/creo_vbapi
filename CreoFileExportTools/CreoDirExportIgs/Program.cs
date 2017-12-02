using pfcls;
using System;
using System.IO;

namespace CreoDirExportIges
{
    internal class Program
    {
        /// <summary>
        /// 批量将给定目录prt导出到指定目录iges文件
        /// </summary>
        /// <param name="args"> arg0: proe app path arg1 :dir of prt files arg2 :dir for output </param>
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
                Files = ((IpfcBaseSession)(asyncConnection.Session)).ListFiles("*.prt", (int)EpfcFileListOpt.EpfcFILE_LIST_LATEST, inputdir);
                Console.WriteLine("prt文件列表读取完毕...");
                foreach (string file in Files)
                {
                    ConvertToIges(asyncConnection, file, outputdir);
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

        private static void ConvertToIges(IpfcAsyncConnection AsyncConnection, string FileFullName, string Outputdir)
        {
            IpfcModelDescriptor descmodel;
            IpfcRetrieveModelOptions options;
            IpfcModel model;
            IpfcIGES3DExportInstructions igesinstructions;
            IpfcGeomExportFlags flags;

            Console.WriteLine("打开" + FileFullName + "...");
            try
            {
                Console.WriteLine("开始转换" + FileFullName + "...");
                descmodel = (new CCpfcModelDescriptor()).Create((int)EpfcModelType.EpfcMDL_PART, "", null);
                descmodel.Path = FileFullName;
                options = (new CCpfcRetrieveModelOptions()).Create();
                options.AskUserAboutReps = false;
                model = ((IpfcBaseSession)(AsyncConnection.Session)).RetrieveModelWithOpts(descmodel, options);
            }
            catch
            {
                Console.WriteLine("无法打开" + FileFullName + "...");
                return;
            }

            try
            {
                flags = (new CCpfcGeomExportFlags()).Create();
                igesinstructions = (new CCpfcIGES3DExportInstructions()).Create( flags);
                model.Export(Outputdir + model.InstanceName.ToLower() + ".igs", (IpfcExportInstructions)igesinstructions);
            }
            catch
            {
                Console.WriteLine("无法转换" + FileFullName + "...");
                return;
            }

            Console.WriteLine(FileFullName + "转换完毕...");

            try
            {
                model.Erase();
            }
            catch
            {
            }
        }
    }
}