Public NotInheritable Class Fr_tentang

    Private Sub Fr_tentang_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Set the title of the form.
        Dim ApplicationTitle As String
        If My.Application.Info.Title <> "" Then
            ApplicationTitle = My.Application.Info.Title
        Else
            ApplicationTitle = System.IO.Path.GetFileNameWithoutExtension(My.Application.Info.AssemblyName)
        End If
        Me.Text = String.Format("Tentang {0}", ApplicationTitle)
        Me.lb_nm.Text = My.Resources.nama
        Me.lb_nim.Text = My.Resources.nim
        Me.lb_jur.Text = My.Resources.jurusan

        Me.TextBoxDescription.Text = My.Resources.deskripsi

    End Sub

    Private Sub OKButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OKButton.Click
        Me.Close()
    End Sub

End Class
