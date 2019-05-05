using pfcls;

namespace CreoCSharp
{
    internal class VBAPITool
    {
        private IpfcAsyncConnection asyncConnection = null;
        private string _cmdLine, _textPath;

        public VBAPITool(string CmdLine, string TextPath)
        {
            _cmdLine = CmdLine;
            _textPath = TextPath;
        }

        public VBAPITool()
        {
            _cmdLine = System.Configuration.ConfigurationManager.AppSettings.Get("CmdLine");
            _textPath = System.Configuration.ConfigurationManager.AppSettings.Get("TextPath");
        }

        public bool ConnectCreo()
        {
            try
            {
                if (asyncConnection == null || !asyncConnection.IsRunning())
                {
                    asyncConnection = new CCpfcAsyncConnection().Connect(null, null, null, null);
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

        public bool StartCreo()
        {
            try
            {
                asyncConnection = new CCpfcAsyncConnection().Start(_cmdLine, _textPath);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Openfile()
        {
            IpfcModelDescriptor modelDesc;
            IpfcFileOpenOptions fileOpenopts;
            string filename;
            IpfcRetrieveModelOptions retrieveModelOptions;
            IpfcModel model;
            try
            {
                fileOpenopts = new CCpfcFileOpenOptions().Create("*.prt");
                filename = asyncConnection.Session.UIOpenFile(fileOpenopts);
                modelDesc = new CCpfcModelDescriptor().Create((int)EpfcModelType.EpfcMDL_PART, null, null);
                modelDesc.Path = filename;
                retrieveModelOptions = new CCpfcRetrieveModelOptions().Create();
                retrieveModelOptions.AskUserAboutReps = false;
                model = ((IpfcBaseSession)(asyncConnection.Session)).RetrieveModelWithOpts(modelDesc, retrieveModelOptions);
                model.Display();
                ((IpfcBaseSession)(asyncConnection.Session)).get_CurrentWindow().Activate();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}