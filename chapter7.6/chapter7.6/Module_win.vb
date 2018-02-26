Imports System.IO

Module Module_win
    Public Tol_path As String = "" 'Creo公差表所在目录
    Public Symbolpath As String = "" '符号文件所在目录
    Public Msg_file As String = "" '消息文件路径
    Public Sub Get_tol_table(ByVal text As String, ByRef name As String, ByRef column As String)
        Dim i As Long
        For i = 1 To Len(text)
            If IsNumeric(Mid(text, i, 1)) = True Then
                column = column & Mid(text, i, 1)
            Else
                name = name & Mid(text, i, 1)
            End If
        Next i
    End Sub

    ''' <summary>
    ''' 给出配合公差标注标字符串
    ''' </summary>
    ''' <param name="Dvalue">公差值</param>
    ''' <param name="Table_Name">公差表</param>
    ''' <returns>尺寸后缀字符串</returns>
    Public Function Fit_tolerance_name(ByVal Dvalue As String, ByVal Table_Name As String) As String
        Dim S_holeshaft() As String
        Dim Prefix_1 As String = ""
        Dim Prefix_2 As String = ""
        Dim Name1 As String = ""
        Dim Column1 As String = ""
        Dim Name2 As String = ""
        Dim Column2 As String = ""
        S_holeshaft = Split(Table_Name, "/")
        Get_tol_table(S_holeshaft(0), Name1, Column1)
        Get_tol_table(S_holeshaft(1), Name2, Column2)
        Select Case Name1
            Case "A" To "Z"
                Prefix_1 = "hole_"
                Prefix_2 = "shaft_"
            Case "a" To "z"
                Prefix_2 = "hole_"
                Prefix_1 = "shaft_"
        End Select
        Prefix_1 = Get_TolTableValue(Tol_path & Prefix_1 & Name1 & ".ttl", Dvalue, Column1)
        Prefix_2 = Get_TolTableValue(Tol_path & Prefix_2 & Name2 & ".ttl", Dvalue, Column2)
        Fit_tolerance_name = "@D" + S_holeshaft(0) + "(" + Prefix_1 + ")/" + S_holeshaft(1) + "(" + Prefix_2 + ")"
    End Function

    ''' <summary>
    ''' 通过给定公差值和公差类型，获取公差值
    ''' </summary>
    ''' <param name="Tolfilename">公差表</param>
    ''' <param name="Dvalue">公差值</param>
    ''' <param name="Column">公差表值</param>
    ''' <returns>尺寸后缀字符串</returns>
    Private Function Get_TolTableValue(ByVal Tolfilename As String, ByVal Dvalue As Double, ByVal Column As String) As String
        Dim Tablefile As New StreamReader(Tolfilename)
        Dim S_read As String
        Dim S_column() As String
        Dim S_data() As String
        Dim i, j As Integer
        Dim Dim_bound() As String
        Dim Tol_bound() As String
        Get_TolTableValue = ""
        Do While Tablefile.Peek <> -1
            S_read = Tablefile.ReadLine
            '此步获得table column字符串数组到S_column，序号从1开始
            If InStr(Trim(S_read), Chr(34) & "Basic Size" & Chr(34)) = 1 Then
                If InStr(Trim(S_read), Chr(34) & " " & Chr(34) & "\" & Chr(34)) > 0 Then
                    S_column = Split(S_read, Chr(34) & " " & Chr(34) & "\" & Chr(34))
                    For i = 1 To S_column.Count - 1
                        S_column(i) = Replace(S_column(i), Chr(34), "")
                    Next
                Else
                    S_read = Replace(S_read, "Basic Size", "a")
                    S_read = Replace(S_read, Chr(34), "")
                    S_column = Split(S_read, " ")
                End If
                For i = 1 To S_column.Count - 1
                    If S_column(i) = Column Then
                        Exit For
                    End If
                Next
            Else
                If InStr(Trim(S_read), Chr(34) & " " & Chr(34) & "\" & Chr(34)) > 0 Then
                    '此步获得公差字符串数组到S_column，序号从1开始，0号字符串为长度Dvalue的范围
                    S_data = Split(S_read, Chr(34) & " " & Chr(34) & "\" & Chr(34))
                    S_data(0) = Mid(S_data(0), 2) '去除最前的引号
                    Dim_bound = Split(S_data(0), "-")
                    If Dim_bound.Count = 3 Then '第一个区间0-3在ttl文件中写法问题，修改一下
                        Dim_bound(0) = "0"
                        Dim_bound(1) = "3"
                    End If

                    '''''''''''''  "\"-"    处理这样的数据。。
                    For j = 1 To S_data.Length - 1
                        S_read = Replace(S_read, Chr(34), "")
                        If S_data(j) = "-" Then
                            S_data(j) = "0/0"
                        End If
                    Next
                    If Dvalue >= CDbl(Dim_bound(0)) And Dvalue < CDbl(Dim_bound(1)) Then

                        If Not InStr(S_data(i), "/") Then
                            S_data(i) = S_data(i) + "/-" + S_data(i)
                        End If
                        Tol_bound = Split(S_data(i), "/")
                        '上偏差字符串
                        If CDbl(Tol_bound(0)) > 0 Then
                            Tol_bound(0) = "@++" + (CDbl(Tol_bound(0)) / 1000).ToString
                        Else
                            Tol_bound(0) = "@+" + (CDbl(Tol_bound(0)) / 1000).ToString
                        End If
                        Tol_bound(0) = Tol_bound(0) + "@#"
                        '下偏差字符串
                        If CDbl(Tol_bound(1)) > 0 Then
                            Tol_bound(1) = "@-+" + (CDbl(Tol_bound(1)) / 1000).ToString
                        Else
                            Tol_bound(1) = "@-" + (CDbl(Tol_bound(1)) / 1000).ToString
                        End If
                        Tol_bound(1) = Tol_bound(1) + "@#"

                        Get_TolTableValue = Tol_bound(0) + Tol_bound(1)
                        Tablefile.Close()
                        Exit Function
                    End If
                End If
            End If

        Loop
        Tablefile.Close()
    End Function
End Module
