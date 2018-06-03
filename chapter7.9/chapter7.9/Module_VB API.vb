Imports System.Configuration
Imports pfcls

Module Module_vbapi
    Public asyncConnection As IpfcAsyncConnection = Nothing '全局变量，用于存储连接会话的句柄

    ''' <summary>
    ''' 连接现有会话
    ''' </summary>
    ''' <returns>是否连接成功</returns>
    Public Function Creo_Connect() As Boolean
        Try
            If asyncConnection Is Nothing OrElse Not asyncConnection.IsRunning Then
                asyncConnection = (New CCpfcAsyncConnection).Connect(Nothing, Nothing, Nothing, Nothing)
                Creo_Connect = True
            Else
                Return False
            End If
        Catch ex As Exception
            Creo_Connect = False
            MsgBox(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
        End Try
    End Function

    ''' <summary>
    ''' 打开新会话
    ''' </summary>
    ''' <returns>新建会话是否成功</returns>
    Public Function Creo_New() As Boolean
        Try
            Dim CmdLine As String = ConfigurationManager.AppSettings("CmdLine").ToString()
            Dim TextPath As String = ConfigurationManager.AppSettings("TextPath").ToString()
            asyncConnection = (New CCpfcAsyncConnection).Start(CmdLine, TextPath)
            '''''''''''''''''''''''补充之前的问题，使用config文件'''''''''''''''''''''''''''''''''
            asyncConnection.Session.LoadConfigFile(ConfigurationManager.AppSettings("Configfile").ToString())
            '''''''''''''''''''''''选择工作目录'''''''''''''''''''''''''''''''''
            asyncConnection.Session.ChangeDirectory(ConfigurationManager.AppSettings("WorkDirectory").ToString())
            Creo_New = True
        Catch ex As Exception
            Creo_New = False
            MsgBox(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
        End Try
    End Function

    ''' <summary>
    ''' 重生Drawing
    ''' </summary>
    Private Sub Reg_Csheet()
        Dim drawing As IpfcDrawing
        drawing = asyncConnection.Session.CurrentModel
        If Isdrawding() = True Then
            drawing = CType(drawing, IpfcDrawing)
            drawing.RegenerateSheet(drawing.CurrentSheetNumber)
        End If
    End Sub

    ''' <summary>
    ''' 判断是否为打开的是否为工程图
    ''' </summary>
    ''' <returns>是否为工程图</returns>
    Private Function Isdrawding() As Boolean
        Try
            If asyncConnection.Session.CurrentModel Is Nothing Then
                Isdrawding = False
            ElseIf (asyncConnection.Session.CurrentModel.Type = EpfcModelType.EpfcMDL_DRAWING) Then
                Isdrawding = True
            Else
                Isdrawding = False
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
            Isdrawding = False
        End Try
    End Function

    Public Sub ChangeSheet(ByVal DrawingFormatFile As String)
        Dim tableOwner As IpfcTableOwner
        Dim model As IpfcModel
        Dim tables As IpfcTables
        Dim table As IpfcTable
        Dim se As IpfcSelection
        Dim tabcel As IpfcTableCell

        Dim selectionOptions As IpfcSelectionOptions
        Dim selections As CpfcSelections
        Dim selectFeats As IpfcSelection
        Dim selectedfeat As IpfcModelItem
        '初始化selection选项
        selectionOptions = (New CCpfcSelectionOptions).Create("table_cell") '设置可选特征的类型，这里为特征对象
        selectionOptions.MaxNumSels = 1 '设置一次可选择特征的数量
        selections = asyncConnection.Session.Select(selectionOptions, Nothing)
        ''确定选择了一个对象
        If selections.Count > 0 Then
            selectFeats = selections.Item(0)
        End If

        'tableOwner = CType(asyncConnection.Session.CurrentModel, IpfcTableOwner)
        'model = asyncConnection.Session.CurrentModel

        'asyncConnection.Session.select

        'tableOwner = CType(model, IpfcTableOwner)
        'tables = tableOwner.ListTables()

        'table = tables.Item(0)

        'tabcel = (New CCpfcTableCell).Create(2, 1)

        'se = (New CMpfcSelect).CreateModelItemSelection(table, Nothing)
        'se.SelTableCell = tabcel

        'CType(asyncConnection.Session, IpfcBaseSession).CurrentSelectionBuffer.Clear()
        'CType(asyncConnection.Session, IpfcBaseSession).CurrentSelectionBuffer.AddSelection(se)

        'asyncConnection.Session.RunMacro("IMI ~ Open `main_dlg_cur` `Sst_bar.filter_list`;~ Close `main_dlg_cur` `Sst_bar.filter_list`;~ Select `main_dlg_cur` `Sst_bar.filter_list` 1 `23_Table Cell_SEL FILTER`;~ Timer `UI Desktop` `UI Desktop` `popupMenuRMBTimerCB`;~ Close `rmb_popup` `PopupMenu`;~ Command `ProCmdDwgEditRptSymbol` ;~ Select `popuplist` `list` 1 `asm...`;~ Select `popuplist` `list` 1 `mbr...`;~ Select `popuplist` `list` 1 `User Defined`;XH;~ Command `ProCmdDwgTblRegUpd`;")

        'se = CType(asyncConnection.Session, IpfcBaseSession).CurrentSelectionBuffer.Contents.Item(0)
        'Try
        '    If Isdrawding() = True Then

        '    sheetOwner = CType(asyncConnection.Session.CurrentModel, IpfcSheetOwner)
        '        '打开一个图框文件
        '        modelDesc = (New CCpfcModelDescriptor).Create(EpfcModelType.EpfcMDL_DWG_FORMAT, Nothing, Nothing)
        '        modelDesc.Path = DrawingFormatFile
        '        retrieveModelOptions = (New CCpfcRetrieveModelOptions).Create
        '        retrieveModelOptions.AskUserAboutReps = False
        '        '加载零件
        '        model = asyncConnection.Session.RetrievemodelWithOpts(modelDesc, retrieveModelOptions)
        '        DrawingFormat = CType（model, IpfcDrawingFormat）
        '        sheetOwner.SetSheetFormat(sheetOwner.CurrentSheetNumber, DrawingFormat, Nothing, Nothing)
        '        Reg_Csheet()
        '    End If
        'Catch ex As Exception
        '    MsgBox(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
        'End Try
    End Sub

    'Create draft line with pre defined colour
    '======================================================================
    'Function   :   createLine
    'Purpose    :   This function creates a draft line in one of the
    '               colors predefined in Creo Parametric. The start and end
    '               points are interactively selected by the user.
    '======================================================================
    Public Sub CreateLine()
        Dim model As IpfcModel
        Dim rgbColour As IpfcColorRGB
        Dim drawing As IpfcDrawing
        Dim currSheet As Integer
        Dim view As IpfcView2D
        Dim mouse1 As IpfcMouseStatus
        Dim mouse2 As IpfcMouseStatus
        Dim start As IpfcPoint3D
        Dim finish As IpfcPoint3D
        Dim geom As IpfcLineDescriptor
        Dim lineInstructions As IpfcDetailEntityInstructions
        Dim session As IpfcSession = asyncConnection.Session

        Try
            session.SetLineStyle(EpfcStdLineStyle.EpfcLINE_DASH)

            '======================================================================
            'Get the current drawing and its background view
            '======================================================================
            model = session.CurrentModel
            If model Is Nothing Then
                Throw New Exception("Model not present")
            End If
            If Not model.Type = EpfcModelType.EpfcMDL_DRAWING Then
                Throw New Exception("Model is not drawing")
            End If
            drawing = CType(model, IpfcDrawing)

            currSheet = drawing.CurrentSheetNumber
            view = drawing.GetSheetBackgroundView(currSheet)

            '======================================================================
            'Set end points of the line
            '======================================================================
            mouse1 = session.UIGetNextMousePick(EpfcMouseButton.EpfcMOUSE_BTN_LEFT)
            start = mouse1.Position

            mouse2 = session.UIGetNextMousePick(EpfcMouseButton.EpfcMOUSE_BTN_LEFT)
            finish = mouse2.Position

            '======================================================================
            'Allocate and initialize curve descriptor
            '======================================================================
            geom = (New CCpfcLineDescriptor).Create(start, finish)

            rgbColour = session.GetRGBFromStdColor(EpfcStdColor.EpfcCOLOR_QUILT)

            '======================================================================
            'Allocate data for draft entity
            '======================================================================
            lineInstructions = (New CCpfcDetailEntityInstructions).Create(geom, view)
            lineInstructions.Color = rgbColour

            '======================================================================
            'Create and display the line
            '======================================================================
            drawing.CreateDetailItem(lineInstructions)

            session.CurrentWindow.Repaint()
        Catch ex As Exception
            MsgBox(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)

        End Try

    End Sub

End Module