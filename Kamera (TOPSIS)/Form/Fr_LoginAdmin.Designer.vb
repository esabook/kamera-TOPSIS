<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Fr_LoginAdmin

    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.tb_btl = New System.Windows.Forms.Button()
        Me.tb_log = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lb_ver = New System.Windows.Forms.Label()
        Me.tx_nama = New Telerik.WinControls.UI.RadTextBox()
        Me.object_5b166ba3_81d4_4167_aae1_81d43e61d54c = New Telerik.WinControls.RootRadElement()
        Me.tx_pass = New Telerik.WinControls.UI.RadTextBox()
        Me.object_2561c9bd_611c_44bd_b9e8_c4199fc7b730 = New Telerik.WinControls.RootRadElement()
        CType(Me.tx_nama, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tx_pass, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tb_btl
        '
        Me.tb_btl.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.tb_btl.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.tb_btl.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.tb_btl.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.tb_btl.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.tb_btl.ForeColor = System.Drawing.Color.White
        Me.tb_btl.Location = New System.Drawing.Point(91, 217)
        Me.tb_btl.Name = "tb_btl"
        Me.tb_btl.Size = New System.Drawing.Size(69, 46)
        Me.tb_btl.TabIndex = 0
        Me.tb_btl.Text = "&Batal"
        Me.tb_btl.UseVisualStyleBackColor = False
        '
        'tb_log
        '
        Me.tb_log.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.tb_log.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.tb_log.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.tb_log.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.tb_log.ForeColor = System.Drawing.Color.White
        Me.tb_log.Location = New System.Drawing.Point(166, 217)
        Me.tb_log.Name = "tb_log"
        Me.tb_log.Size = New System.Drawing.Size(174, 46)
        Me.tb_log.TabIndex = 1
        Me.tb_log.Text = "&Masuk"
        Me.tb_log.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(77, 13)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(290, 38)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Untuk masuk kedalam mode admin, silakan lengkapi isian berikut."
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lb_ver
        '
        Me.lb_ver.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lb_ver.Location = New System.Drawing.Point(91, 170)
        Me.lb_ver.Name = "lb_ver"
        Me.lb_ver.Size = New System.Drawing.Size(250, 42)
        Me.lb_ver.TabIndex = 8
        '
        'tx_nama
        '
        Me.tx_nama.AutoSize = False
        Me.tx_nama.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tx_nama.ForeColor = System.Drawing.Color.White
        Me.tx_nama.Location = New System.Drawing.Point(91, 88)
        Me.tx_nama.MaxLength = 50
        Me.tx_nama.Name = "tx_nama"
        Me.tx_nama.NullText = "Nama Akun Admin"
        Me.tx_nama.ShowClearButton = True
        Me.tx_nama.Size = New System.Drawing.Size(250, 27)
        Me.tx_nama.TabIndex = 9
        Me.tx_nama.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        CType(Me.tx_nama.GetChildAt(0), Telerik.WinControls.UI.RadTextBoxElement).Text = ""
        CType(Me.tx_nama.GetChildAt(0), Telerik.WinControls.UI.RadTextBoxElement).BackColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(55, Byte), Integer))
        CType(Me.tx_nama.GetChildAt(0).GetChildAt(1), Telerik.WinControls.Primitives.FillPrimitive).BackColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(55, Byte), Integer))
        CType(Me.tx_nama.GetChildAt(0).GetChildAt(2), Telerik.WinControls.Primitives.BorderPrimitive).BoxStyle = Telerik.WinControls.BorderBoxStyle.FourBorders
        CType(Me.tx_nama.GetChildAt(0).GetChildAt(2), Telerik.WinControls.Primitives.BorderPrimitive).Width = 2.0!
        CType(Me.tx_nama.GetChildAt(0).GetChildAt(2), Telerik.WinControls.Primitives.BorderPrimitive).LeftWidth = 0!
        CType(Me.tx_nama.GetChildAt(0).GetChildAt(2), Telerik.WinControls.Primitives.BorderPrimitive).TopWidth = 0!
        CType(Me.tx_nama.GetChildAt(0).GetChildAt(2), Telerik.WinControls.Primitives.BorderPrimitive).RightWidth = 0!
        CType(Me.tx_nama.GetChildAt(0).GetChildAt(2), Telerik.WinControls.Primitives.BorderPrimitive).BottomWidth = 2.0!
        CType(Me.tx_nama.GetChildAt(0).GetChildAt(2), Telerik.WinControls.Primitives.BorderPrimitive).SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.[Default]
        CType(Me.tx_nama.GetChildAt(0).GetChildAt(2), Telerik.WinControls.Primitives.BorderPrimitive).ForeColor = System.Drawing.SystemColors.ButtonFace
        '
        'object_5b166ba3_81d4_4167_aae1_81d43e61d54c
        '
        Me.object_5b166ba3_81d4_4167_aae1_81d43e61d54c.Name = "object_5b166ba3_81d4_4167_aae1_81d43e61d54c"
        Me.object_5b166ba3_81d4_4167_aae1_81d43e61d54c.StretchHorizontally = True
        Me.object_5b166ba3_81d4_4167_aae1_81d43e61d54c.StretchVertically = True
        '
        'tx_pass
        '
        Me.tx_pass.AutoSize = False
        Me.tx_pass.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tx_pass.ForeColor = System.Drawing.Color.White
        Me.tx_pass.Location = New System.Drawing.Point(90, 135)
        Me.tx_pass.MaxLength = 50
        Me.tx_pass.Name = "tx_pass"
        Me.tx_pass.NullText = "Password"
        Me.tx_pass.PasswordChar = Global.Microsoft.VisualBasic.ChrW(9679)
        Me.tx_pass.ShowClearButton = True
        Me.tx_pass.Size = New System.Drawing.Size(250, 27)
        Me.tx_pass.TabIndex = 10
        Me.tx_pass.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        CType(Me.tx_pass.GetChildAt(0), Telerik.WinControls.UI.RadTextBoxElement).Text = ""
        CType(Me.tx_pass.GetChildAt(0), Telerik.WinControls.UI.RadTextBoxElement).BackColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(55, Byte), Integer))
        CType(Me.tx_pass.GetChildAt(0).GetChildAt(1), Telerik.WinControls.Primitives.FillPrimitive).BackColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(55, Byte), Integer))
        CType(Me.tx_pass.GetChildAt(0).GetChildAt(2), Telerik.WinControls.Primitives.BorderPrimitive).BoxStyle = Telerik.WinControls.BorderBoxStyle.FourBorders
        CType(Me.tx_pass.GetChildAt(0).GetChildAt(2), Telerik.WinControls.Primitives.BorderPrimitive).Width = 2.0!
        CType(Me.tx_pass.GetChildAt(0).GetChildAt(2), Telerik.WinControls.Primitives.BorderPrimitive).LeftWidth = 0!
        CType(Me.tx_pass.GetChildAt(0).GetChildAt(2), Telerik.WinControls.Primitives.BorderPrimitive).TopWidth = 0!
        CType(Me.tx_pass.GetChildAt(0).GetChildAt(2), Telerik.WinControls.Primitives.BorderPrimitive).RightWidth = 0!
        CType(Me.tx_pass.GetChildAt(0).GetChildAt(2), Telerik.WinControls.Primitives.BorderPrimitive).BottomWidth = 2.0!
        CType(Me.tx_pass.GetChildAt(0).GetChildAt(2), Telerik.WinControls.Primitives.BorderPrimitive).SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.[Default]
        CType(Me.tx_pass.GetChildAt(0).GetChildAt(2), Telerik.WinControls.Primitives.BorderPrimitive).ForeColor = System.Drawing.SystemColors.ButtonFace
        CType(Me.tx_pass.GetChildAt(0).GetChildAt(3).GetChildAt(0), Telerik.WinControls.UI.LightVisualButtonElement).EnableImageTransparency = True
        CType(Me.tx_pass.GetChildAt(0).GetChildAt(3).GetChildAt(0), Telerik.WinControls.UI.LightVisualButtonElement).Visibility = Telerik.WinControls.ElementVisibility.Hidden
        '
        'object_2561c9bd_611c_44bd_b9e8_c4199fc7b730
        '
        Me.object_2561c9bd_611c_44bd_b9e8_c4199fc7b730.Name = "object_2561c9bd_611c_44bd_b9e8_c4199fc7b730"
        Me.object_2561c9bd_611c_44bd_b9e8_c4199fc7b730.StretchHorizontally = True
        Me.object_2561c9bd_611c_44bd_b9e8_c4199fc7b730.StretchVertically = True
        '
        'Fr_LoginAdmin
        '
        Me.AcceptButton = Me.tb_log
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(28, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.CancelButton = Me.tb_btl
        Me.ClientSize = New System.Drawing.Size(426, 286)
        Me.ControlBox = False
        Me.Controls.Add(Me.tx_pass)
        Me.Controls.Add(Me.tx_nama)
        Me.Controls.Add(Me.lb_ver)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.tb_log)
        Me.Controls.Add(Me.tb_btl)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximumSize = New System.Drawing.Size(432, 311)
        Me.MinimumSize = New System.Drawing.Size(432, 311)
        Me.Name = "Fr_LoginAdmin"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Login Admin"
        CType(Me.tx_nama, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tx_pass, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tb_btl As System.Windows.Forms.Button
    Friend WithEvents tb_log As System.Windows.Forms.Button
    Friend WithEvents Label3 As Label
    Friend WithEvents lb_ver As Label
    Friend WithEvents object_5b166ba3_81d4_4167_aae1_81d43e61d54c As Telerik.WinControls.RootRadElement
    Friend WithEvents tx_nama As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents tx_pass As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents object_2561c9bd_611c_44bd_b9e8_c4199fc7b730 As Telerik.WinControls.RootRadElement
End Class
