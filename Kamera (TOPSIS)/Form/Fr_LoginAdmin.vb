Public Class Fr_LoginAdmin



    Public valid As Boolean
    Dim nama = "admin", password = "admin"

    Private Sub tb_btl_Click(sender As Object, e As EventArgs) Handles tb_btl.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub tb_log_Click(sender As Object, e As EventArgs) Handles tb_log.Click

        If valid Then
            DialogResult = DialogResult.OK
            Close()

        Else
            If nama = tx_nama.Text And password = tx_pass.Text Then
                lb_ver.Visible = True
                valid = True
                lb_ver.ForeColor = Color.GreenYellow
                lb_ver.Text = My.Resources.kombinampassF
                tb_log.Text = "&OK"
            Else
                lb_ver.Visible = True
                valid = False
                lb_ver.ForeColor = Color.Red
                lb_ver.Text = My.Resources.kombinampassS

            End If
        End If

    End Sub

    Private Sub Fr_LoginAdmin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lb_ver.Visible = False

    End Sub
End Class