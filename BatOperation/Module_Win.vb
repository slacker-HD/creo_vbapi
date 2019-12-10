
Option Explicit On
Imports System.Configuration
Imports System.IO
Imports System.Text

Module Module_Win
    ''' <summary>
    ''' 判断文件是否存在
    ''' </summary>
    ''' <param name="File">文件路径</param>
    ''' <returns>是否存在</returns>
    Public Function IsFileExist(ByVal File As String) As Boolean
        IsFileExist = IO.File.Exists(File)
    End Function
    ''' <summary>
    ''' 判断目录是否存在
    ''' </summary>
    ''' <param name="Path">目录名</param>
    ''' <returns>是否存在</returns>
    Public Function IsPathExist(ByVal Path As String) As Boolean
        IsPathExist = IO.Directory.Exists(Path)
    End Function


    ''' <summary>
    ''' 删除文件到回收站
    ''' </summary>
    ''' <param name="FilePath">文件路径</param>
    ''' <returns>是否删除</returns>
    Public Function Recycle(ByVal FilePath As String) As Boolean
        If File.Exists(FilePath) Then
            Try
                My.Computer.FileSystem.DeleteFile(FilePath, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.SendToRecycleBin, FileIO.UICancelOption.DoNothing)
                Recycle = True
            Catch ex As Exception
                Recycle = False
            End Try
        End If
    End Function
    ''' <summary>
    ''' 保存配置文件
    ''' </summary>
    ''' <param name="Exefile">Creo程序路径</param>
    ''' <param name="ConfigFile">Creo配置文件路径</param>
    ''' <param name="ShowWindow">显示creo窗体</param>
    ''' <returns>是否保存</returns>
    Public Function SaveConfig(ByVal Exefile As String, ByVal ConfigFile As String, ByVal ShowWindow As Boolean) As Boolean
        Try
            Dim config As Configuration
            config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
            config.AppSettings.Settings.Item("CmdLine").Value = Exefile
            config.AppSettings.Settings.Item("Configfile").Value = ConfigFile
            If ShowWindow = True Then
                config.AppSettings.Settings.Item("ShowWindow").Value = "True"
            Else
                config.AppSettings.Settings.Item("ShowWindow").Value = "False"
            End If
            config.Save()
            ConfigurationManager.RefreshSection("appSettings")
            SaveConfig = True
        Catch ex As Exception
            SaveConfig = False
        End Try
    End Function
    ''' <summary>
    ''' 按行返回读取的文本文件
    ''' </summary>
    ''' <param name="filename">文本文件</param>
    ''' <returns>按行读取的字符串数组</returns>
    Public Function GetTexts(ByVal Filename As String) As String()
        Return File.ReadAllLines(Filename, Encoding.UTF8)
    End Function

    ''' <summary>
    ''' csv转datatable
    ''' </summary>
    ''' <param name="FilePath">csv文件</param>
    ''' <returns>DataTable</returns>
    Public Function CSVToDataTable(ByVal FilePath As String) As DataTable
        Dim streamreader As StreamReader
        Dim line As String
        Dim strArray As String()

        CSVToDataTable = New DataTable()

        Try
            streamreader = New StreamReader(FilePath)
            line = streamreader.ReadLine()
            strArray = line.Split(","c)
            Dim row As DataRow
            For Each s As String In strArray
                Dim column As New DataColumn
                column.ColumnName = s
                CSVToDataTable.Columns.Add(column)
            Next

            Do
                line = streamreader.ReadLine
                If Not line = String.Empty Then
                    row = CSVToDataTable.NewRow()
                    row.ItemArray = line.Split(","c)
                    CSVToDataTable.Rows.Add(row)
                Else
                    Exit Do
                End If
            Loop
        Catch ex As Exception
            MessageBox.Show("读取参数文件出错，请检查！")
            Return New DataTable()
        End Try
    End Function
    ''' <summary>
    ''' 用于传递修改图框的数据
    ''' </summary>
    Public Structure FrmPassData
        Public FileName As String
        Public SheetNumber As Integer
        Public FrmName As String
        Public DelTab As Boolean
    End Structure

End Module
