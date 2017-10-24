Imports System.Runtime.InteropServices
Imports Telerik.WinControls
Imports Kamera__TOPSIS_.C_Util
Imports System.IO

Public Class Fr_Splash
    Dim poin As Point, nc As Integer = &H84, cl As Integer = &H1, cap As Integer = &H2

    Private Sub up(sender As Object, e As MouseEventArgs) Handles Label3.MouseUp
        Label3.ForeColor = Color.Black
    End Sub


    Private Sub down(sender As Object, e As MouseEventArgs) Handles Label3.MouseDown
        Label3.ForeColor = Color.White
    End Sub

    Private Sub clos(sender As Object, e As EventArgs) Handles Label3.Click
        End
    End Sub

    Public Sub m(sender As Object, e As MouseEventArgs) Handles Label2.MouseDown, ProgressBar2.MouseDown, PictureBox1.MouseDown, MyBase.MouseDown, Label1.MouseDown
        C_Util.move(Handle)
    End Sub

End Class