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
        Dim sheetOwner As IpfcSheetOwner
        Dim DrawingFormat As IpfcDrawingFormat
        Dim modelDesc As IpfcModelDescriptor
        Dim retrieveModelOptions As IpfcRetrieveModelOptions
        Dim model As IpfcModel
        Try
            If Isdrawding() = True Then
                sheetOwner = CType(asyncConnection.Session.CurrentModel, IpfcSheetOwner)
                '打开一个图框文件
                modelDesc = (New CCpfcModelDescriptor).Create(EpfcModelType.EpfcMDL_DWG_FORMAT, Nothing, Nothing)
                modelDesc.Path = DrawingFormatFile
                retrieveModelOptions = (New CCpfcRetrieveModelOptions).Create
                retrieveModelOptions.AskUserAboutReps = False
                '加载零件
                model = asyncConnection.Session.RetrievemodelWithOpts(modelDesc, retrieveModelOptions)
                DrawingFormat = CType（model, IpfcDrawingFormat）
                sheetOwner.SetSheetFormat(sheetOwner.CurrentSheetNumber, DrawingFormat, Nothing, Nothing)
                Reg_Csheet()
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
        End Try
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