Option Explicit On
Imports pfcls
Module Module_Frm
    Public Function GetDrwList(ByVal InputPath As String) As Hashtable
        Dim Files As Cstringseq
        If asyncConnection Is Nothing Then
            If (CreateConnection() = False) Then
                MessageBox.Show("无法创建Creo会话,请确认Creo环境正确配置！")
                Return Nothing
            End If
        End If

        If IsPathExist(InputPath) = False Then
            MessageBox.Show("输入目录不存在！")
            Return Nothing
        End If

        GetDrwList = New Hashtable
        asyncConnection.Session.ChangeDirectory(InputPath)
        Files = CType(asyncConnection.Session, IpfcBaseSession).ListFiles("*.drw", EpfcFileListOpt.EpfcFILE_LIST_LATEST, asyncConnection.Session.GetCurrentDirectory)

        For Each filename In Files
            GetDrwList.Add(filename, SheetInfo(filename))
        Next
    End Function

    Private Function SheetInfo(ByVal Filename As String) As List(Of String)
        Dim descmodel As IpfcModelDescriptor
        Dim options As IpfcRetrieveModelOptions
        Dim model As IpfcModel
        Dim sheetnum As Integer
        Dim sheetformat As IpfcModelDescriptor
        Try
            descmodel = (New CCpfcModelDescriptor).Create(EpfcModelType.EpfcMDL_DRAWING, "", Nothing)
            descmodel.Path = Filename
            options = (New CCpfcRetrieveModelOptions).Create()
            options.AskUserAboutReps = False
            model = asyncConnection.Session.RetrieveModelWithOpts(descmodel, options)
            asyncConnection.Session.CreateModelWindow(model)

        Catch ex As Exception
            Return Nothing
        End Try
        Try
            SheetInfo = New List(Of String)
            sheetnum = CType(model, IpfcSheetOwner).NumberOfSheets
            For i = 1 To sheetnum
                sheetformat = CType(model, IpfcSheetOwner).GetSheetFormatDescr(i)
                If sheetformat Is Nothing Then
                    SheetInfo.Add("无图框或图框文件遗失")
                Else
                    SheetInfo.Add(sheetformat.InstanceName)
                End If
            Next
        Catch ex As Exception
            SheetInfo = Nothing
        End Try
    End Function

    Public Function GetFrmList(ByVal InputPath As String) As List(Of String)
        Dim Files As Cstringseq
        If asyncConnection Is Nothing Then
            If (CreateConnection() = False) Then
                MessageBox.Show("无法创建Creo会话,请确认Creo环境正确配置！")
                Return Nothing
            End If
        End If

        If IsPathExist(InputPath) = False Then
            MessageBox.Show("输入目录不存在！")
            Return Nothing
        End If

        GetFrmList = New List(Of String)
        asyncConnection.Session.ChangeDirectory(InputPath)
        Files = CType(asyncConnection.Session, IpfcBaseSession).ListFiles("*.frm", EpfcFileListOpt.EpfcFILE_LIST_LATEST, asyncConnection.Session.GetCurrentDirectory)

        For Each filename In Files
            GetFrmList.Add(filename)
        Next
    End Function


    Public Sub ChangeFrms(ByVal Items As List(Of FrmPassData))
        If asyncConnection Is Nothing Then
            If (CreateConnection() = False) Then
                MessageBox.Show("无法创建Creo会话,请确认Creo环境正确配置！")
                Return
            End If
        End If
        For Each item In Items
            ChangeFrm(item)
        Next
    End Sub

    Private Function ChangeFrm(ByVal Item As FrmPassData) As Boolean
        Dim modelDesc As IpfcModelDescriptor
        Dim retrieveModelOptions As IpfcRetrieveModelOptions
        Dim modeldrawing As IpfcModel
        Dim descmodel As IpfcModelDescriptor
        Dim options As IpfcRetrieveModelOptions
        Dim modelfrm As IpfcModel
        Try
            descmodel = (New CCpfcModelDescriptor).Create(EpfcModelType.EpfcMDL_DRAWING, "", Nothing)
            descmodel.Path = Item.FileName
            options = (New CCpfcRetrieveModelOptions).Create()
            options.AskUserAboutReps = False
            modeldrawing = asyncConnection.Session.RetrieveModelWithOpts(descmodel, options)
            modeldrawing.Display()
            asyncConnection.Session.CurrentWindow.Activate()


            modelDesc = (New CCpfcModelDescriptor).Create(EpfcModelType.EpfcMDL_DWG_FORMAT, Nothing, Nothing)
            modelDesc.Path = Item.FrmName
            retrieveModelOptions = (New CCpfcRetrieveModelOptions).Create
            retrieveModelOptions.AskUserAboutReps = False
            modelfrm = asyncConnection.Session.RetrievemodelWithOpts(modelDesc, retrieveModelOptions)

            asyncConnection.Session.RunMacro("IMICHANGEFRM ~ Activate `main_dlg_cur` `switcher_lay_buttons_lay_ph.page_" + (Item.SheetNumber - 1).ToString() + "` 1;~ Trail `UI Desktop` `UI Desktop` `SmartTabs` `selectButton main_dlg_cur@switcher_lay_buttons_lay page_" + (Item.SheetNumber - 1).ToString() + " 0`;")
            If Item.DelTab = True Then
                DelTabofFrm(modeldrawing, Item.SheetNumber)
            End If
            CType(modeldrawing, IpfcSheetOwner).SetSheetFormat(Item.SheetNumber, CType（modelfrm, IpfcDrawingFormat）, Nothing, Nothing)
            modeldrawing.Save()
            ChangeFrm = True
        Catch ex As Exception
            ChangeFrm = False
        End Try
    End Function

    Private Function DelTabofFrm(ByVal Drawing As IpfcModel, ByVal SheetNumber As Integer) As Boolean
        Dim tables As IpfcTables
        Try
            tables = CType(Drawing, IpfcTableOwner).ListTables()
            For i = 0 To tables.Count - 1
                If tables.Item(i).CheckIfIsFromFormat(SheetNumber) = True Then
                    CType(Drawing, IpfcTableOwner).DeleteTable(tables.Item(i), False)
                End If
            Next
            DelTabofFrm = True
        Catch ex As Exception
            DelTabofFrm = False
        End Try

    End Function
End Module
