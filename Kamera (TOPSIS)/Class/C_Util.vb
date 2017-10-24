Imports System.Runtime.InteropServices

Public Class C_Util

    Dim poin As Point,
        c As Boolean,
        nc As Integer = &H84,
        cl As Integer = &H1,
        cap As Integer = &H2


    <DllImportAttribute("user32.dll")>
    Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal Msg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
    End Function
    Declare Sub Sleep Lib "kernel32" (ByVal dwMilliseconds As Long)
    <DllImportAttribute("user32.dll")>
    Shared Function ReleaseCapture() As Boolean
    End Function
    Shared Sub move(b As IntPtr)
        ReleaseCapture()
        SendMessage(b, &HA1, &H2, 0)
    End Sub
    Shared Function pesan(isi As String, titel As String, btn As MessageBoxButtons, ikon As MessageBoxIcon) As DialogResult

        Return MessageBox.Show(isi & "                                                  " & Chr(13), titel.ToUpper, btn, ikon, MessageBoxDefaultButton.Button1)
    End Function
    Shared Function pesan(isi As String, ex As Exception, titel As String) As DialogResult
        Dim tx As String
        If IsNothing(ex) Then : tx = "" : Else : tx = "Pesan Galat: " + ex.Message : End If
        Return pesan(isi + Chr(13) + tx, titel, MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Function

End Class
