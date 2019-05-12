Module main

    Sub Main(args As String())
        Dim mytool As VBapitool
        mytool = New VBapitool(args(0), args(1), args(2))
        If mytool.NewSession() Then
            mytool.ExportFamtableinstances()
            mytool.EndSession()
        End If
    End Sub
End Module
