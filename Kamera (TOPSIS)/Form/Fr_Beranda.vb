Imports Telerik.WinControls.Theme
Imports Telerik.WinControls
Imports Kamera__TOPSIS_.C_Util
Imports Kamera__TOPSIS_.C_HandleDtBase
Imports Telerik.WinControls.UI
Imports Kamera__TOPSIS_.My.Resources
Imports System.Data.OleDb
Imports System.IO

Public Class Fr_Beranda
    Private control As Object,
        tbText As String,
        tbObj As Object,
        isAdmin As Boolean,
        logoG As Boolean,
        Full As Boolean,
        obj As New ArrayList,
        modeBaru, modeinput As Boolean,
        db As New C_HandleDtBase,
        k As New C_Katalog,
        tp As C_Topsis,
                    nama_kriteria() As String,
            nilai_Krit() As Double,
            id_p As String, bb_p As String,
            filterSubKriteria As Boolean,
            th As Threading.Thread,
            st1 As Integer = 0,
            strK() As String,
            nmk1 As New ArrayList,
    st0 As Integer = 0,
    totalbobot As Integer



#Region "Handle Tampilan"
    Private Sub loader(sender As Object, e As EventArgs) Handles MyBase.Shown, MyBase.Load
        Try
            If Not File.Exists("\gambar\noimage.bmp") Then My.Resources.NoImage.Save("\gambar\noimage.bmp")
        Catch
        End Try

        katalog(Nothing)
        refreshDGV()
        modeEdit(False)
        isiFormulir(dg_KinK)
        isiFormulir(dg_SKinSK)
        isiFormulir(dg_kam)
        remv()
        RadCollapsiblePanel1.IsExpanded = False
        CType(Me.pg_cat.GetChildAt(0).GetChildAt(0), Telerik.WinControls.UI.StripViewItemContainer).Visibility = Telerik.WinControls.ElementVisibility.Collapsed


    End Sub

    'Handle Tombol Header
    Private Sub tombolHeader(sender As Object, e As EventArgs) Handles tb_menu.Click, tb_login.Click, tb_full.Click, pb_logo.Click
        remv()
        Select Case sender.name

            Case tb_full.Name
                'tombol borderless

                If WindowState = FormWindowState.Maximized And Me.FormBorderStyle = FormBorderStyle.Sizable Then
                    Me.FormBorderStyle = FormBorderStyle.None
                ElseIf WindowState = FormWindowState.Normal Then
                    Me.WindowState = FormWindowState.Maximized
                    Me.FormBorderStyle = FormBorderStyle.None
                Else
                    Me.FormBorderStyle = FormBorderStyle.Sizable
                End If

            Case tb_menu.Name
                'tombol coll-exp menu
                If Tl_dasar.ColumnStyles.Item(0).Width = 183 Then
                    pb_logo.SizeMode = PictureBoxSizeMode.Zoom
                    Tl_dasar.ColumnStyles.Item(0).Width = 34
                    pb_logo.Width = 33
                    pb_logo.BackColor = Panel3.BackColor
                Else
                    Tl_dasar.ColumnStyles.Item(0).Width = 183
                    pb_logo.Width = 183
                    pb_logo.BackColor = Color.Transparent
                    pb_logo.SizeMode = PictureBoxSizeMode.CenterImage
                End If
            Case tb_login.Name
                'tombol login
                If isAdmin Then
                    If pesan(My.Resources.alertmsg,
                                                  My.Resources.alert,
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Warning) = DialogResult.Yes Then
                        tb_login.Text = My.Resources.isNotAdmin
                        seleksimenu(False)
                    End If
                Else
                    Fr_LoginAdmin.ShowDialog()
                    If Fr_LoginAdmin.DialogResult = DialogResult.OK Then
                        tb_login.Text = My.Resources.isAdmin
                        seleksimenu(True)
                    Else
                        tb_login.Text = My.Resources.isNotAdmin
                    End If
                    Fr_LoginAdmin.Dispose()
                End If
            Case pb_logo.Name
                'logo UNIVERSITAS
                Fr_tentang.ShowDialog()
                pb_logo.Image = My.Resources.logo_c
        End Select

    End Sub

    'Handle Tombol Menu Pilihan
    Private Sub tombolMenuPilihan(sender As Object, e As EventArgs) Handles nav_spek.Click, nav_skrit.Click, nav_pg.Click, nav_log.Click, nav_krit.Click, nav_cat.Click, nav_bobot.Click
        Dim l As Color = Color.DimGray
        sender.Enabled = False
        Try
            control.Enabled = True
            control.BackColor = l
        Catch
        Finally
            control = sender
        End Try

        l = Color.FromArgb(0, 122, 204)
        sender.BackColor = l

        Select Case sender.ToString
            Case nav_cat.ToString
                pg_cat.SelectedPage = pg_kat
            Case nav_pg.ToString
                pg_cat.SelectedPage = pg_tp
            Case nav_krit.ToString
                pg_cat.SelectedPage = pg_krit

            Case nav_skrit.ToString
                pg_cat.SelectedPage = pg_skrit
            Case nav_bobot.ToString
                pg_cat.SelectedPage = pg_bobot
                bb_ref.PerformClick()
            Case nav_spek.ToString
                pg_cat.SelectedPage = pg_spek

            Case nav_log.ToString
                pg_cat.SelectedPage = pg_log


        End Select
        refreshDGV()
        remv()

    End Sub


    'Handle MouseUP
    Private Sub mouseUpBtn(sender As Object, e As MouseEventArgs) Handles tb_ubK.MouseUp, tb_ub_sp.MouseUp, tb_tb_km.MouseUp, tb_spK.MouseUp, tb_sp_sp.MouseUp, tb_hpK.MouseUp, tb_hp_km.MouseUp, sk_ubh.MouseUp, sk_sp.MouseUp, sk_hps.MouseUp, sk_br.MouseUp, pg_lanjut.MouseUp
        remv()
    End Sub

    'Logo Hover
    Private Sub logo_hover(sender As Object, e As EventArgs) Handles pb_logo.MouseLeave, pb_logo.MouseHover

        If Not logoG Then
            pb_logo.Image = My.Resources.logo_g
            logoG = True
        Else
            pb_logo.Image = My.Resources.logo_c
            logoG = False

        End If


    End Sub


    Private Sub IsiRow(sender As Object, e As DataGridViewCellEventArgs) Handles dg_spek.CellClick, dg_SKinSK.CellClick, dg_KinK.CellClick, dg_kam.CellClick, DataGridView9.CellClick, DataGridView8.CellClick
        isiFormulir(sender)
    End Sub

    Private Sub selectCBox(sender As Object, e As UI.Data.PositionChangedEventArgs) Handles pg_k.SelectedIndexChanged, cb_KritSK.SelectedIndexChanged
        Select Case sender.name
            Case cb_KritSK.Name
                Try
                    cb_KritSK.Text = cb_KritSK.SelectedText.Remove(db.jumlahNol + db.awalan_PKSKrit.ToString.Length)
                Catch

                End Try
            Case pg_k.Name
                Dim b = False
                Dim r = db.QueryTable("select nama from sub_kriteria where id_kriteria='" + db.IDBerdasarNama(pg_k.Text) + "'")
                pg_pil.Items.Clear()

                For Each rr As DataRow In r.Rows
                    pg_pil.Items.Add(rr(0))
                    'If InStr(rr(0), ">") <> 0 Then
                    '    If rr(0).ToString.Split("-").Length = 1 Then
                    '        st1 = rr(0).ToString.Replace(">", "").Replace("=", "")
                    '    ElseIf rr(0).ToString.Split("-").Length = 2 Then
                    '        st0 = rr(0).ToString.Replace(">", "").Replace("=", "").Split("-")(0)
                    '        st1 = rr(0).ToString.Replace(">", "").Replace("=", "").Split("-")(1)
                    '    End If
                    'End If




                Next

        End Select

    End Sub

    'Validasi Combo Box
    Private Sub validasiCB(sender As Object, e As EventArgs) Handles sp_krit.TextChanged
        Select Case sender.name
            Case sp_krit.Name

                With sp_krit
                        If (.Text = "") Or (Not .Items.Contains(.Text)) Then
                        sp_lbK.Text = ""
                    Else
                        sp_lbK.Text = db.QueryStr("select id_kriteria from kriteria where nama='" & sp_krit.Text & "'")
                    End If

                    End With
        End Select
    End Sub
    'Validasi Combo Box
    Private Sub validasiT(sender As Object, e As EventArgs) Handles sp_krit.SelectedIndexChanged
        sp_lbK.Text = db.QueryStr("select id_kriteria from kriteria where nama='" & sp_krit.Text & "'")

        If sp_skrit.Items.Count > 0 Then sp_skrit.Items.Clear()

        For Each d As DataRow In db.QueryTable("Select nama from sub_kriteria where id_kriteria='" & sp_lbK.Text & "'").Rows
            sp_skrit.Items.Add(d.Item(0))

        Next
    End Sub


    'Handle Text Edit
    Private Sub validasiTX(sender As Object, e As EventArgs) Handles tx_nmSK.TextChanged, tx_nm_kritK.TextChanged, pg_nama.LostFocus, pg_lamat.LostFocus, Label3.TextChanged
        Select Case sender.name
            'validasi input nama kriteria
            Case tx_nm_kritK.Name
                With tx_nm_kritK
                    If db.QueryTable("select nama from Kriteria where id_kriteria <>'" + tx_id_kritK.Text + "' and nama='" + .Text + "'").Rows.Count > 0 Then
                        RadLabel1.Text = "<html>Kriteria dengan nama <b>'" + .Text + "'</b> telah ada dalam database</html>"
                        If modeinput Then tb_spK.Enabled = False
                    Else
                        RadLabel1.Text = ""
                        If modeinput Then tb_spK.Enabled = True
                    End If

                End With
            'validasi input nama sub-kriteria
            Case tx_nmSK.Name
                With tx_nmSK
                    If db.QueryTable("select nama from sub_kriteria where id_sub_kriteria <>'" + tx_idSK.Text + "' and nama='" + .Text + "' and id_kriteria='" + cb_KritSK.Text + "'").Rows.Count > 0 Then
                        RadLabel2.Text = "<html>Sub-kriteria dengan nama <b>'" + .Text + "'</b> telah ada dalam database</html>"
                        If modeinput Then sk_sp.Enabled = False
                    Else
                        RadLabel2.Text = ""
                        If modeinput Then sk_sp.Enabled = True
                    End If
                    'If tx_nmSK.Text.Last.ToString = "," Then tx_nmSK.Text = tx_nmSK.Text.Replace(",", ".")
                    'Dim tesOp() As String = {">=", "<=", "-", ">", "<"}
                    '    For Each str As String In tesOp
                    '    If tx_nmSK.Text.Contains(str) Then
                    '        AddHandler .KeyPress, AddressOf keyPressTX
                    '        Exit For
                    '    Else
                    '        RemoveHandler .KeyPress, AddressOf keyPressTX
                    '    End If
                    'Next

                End With

            Case pg_nama.Name
                If sender.Text.Length < 4 Then
                    ErrorProvider1.SetError(sender, "")
                Else
                    ErrorProvider1.Clear()
                End If

            Case pg_hp.Name
                If pg_hp.Text.Length < 11 Then
                    ErrorProvider1.SetError(sender, "")
                Else
                    ErrorProvider1.Clear()

                End If
            Case pg_lamat.Name
                If sender.Text.Length < 10 Then
                    ErrorProvider1.SetError(sender, "")
                Else
                    ErrorProvider1.Clear()
                End If
            Case Label3.Name
                If CInt(Label3.Text) >= 100 Then ErrorProvider1.SetError(sender, "Total nilai telah mencapai 100") : Label3.Text = 100 Else ErrorProvider1.Clear()

        End Select
    End Sub
    Private Sub MoveRow(ByVal moveUp As Boolean, r As RadGridView)
        Dim currentRow As GridViewRowInfo = r.CurrentRow
        If currentRow Is Nothing Then
            Return
        End If
        Dim index As Integer = If(moveUp, currentRow.Index - 1, currentRow.Index + 1)
        If index < 0 OrElse index >= r.RowCount Then
            Return
        End If
        r.Rows.Move(index, currentRow.Index)
        r.CurrentRow = r.Rows(index)
    End Sub
    'Handle Tombol Pengolah data
    Private Sub Act_Tb_EDIT(sender As Object, e As EventArgs) Handles tb_ubK.Click, tb_ub_sp.Click, tb_tb_sp.Click, tb_tb_km.Click, tb_spK.Click, tb_sp_sp.Click, tb_hpK.Click, tb_hp_sp.Click, tb_hp_km.Click, tb_brK.Click, t_mSK.Click, sk_ubh.Click, sk_sp.Click, sk_reset.Click, sk_hps.Click, sk_br.Click, pg_tambah.Click, pg_selesai.Click, pg_reset.Click, pg_mulai.Click, pg_min.Click, pg_lanjut.Click, pg_cek.Click, kat_ref.Click, bb_up.Click, bb_tb.Click, bb_sp.Click, bb_ref.Click, bb_hp.Click, bb_dw.Click
        Dim a As Boolean
        Select Case sender.name
#Region "Bobot"
            Case bb_up.Name
                MoveRow(True, dgk)
            Case bb_dw.Name
                MoveRow(False, dgk)
            Case bb_tb.Name

                dgn.Rows.AddNew()
            Case bb_hp.Name
                If dgn.CurrentRow Is Nothing Then Return
                dgn.CurrentRow.Delete()
            Case bb_sp.Name
                dgn.EndEdit()
                Dim s = ""
                Dim t() As Decimal
                'For Each r As GridViewRowInfo In dgn.Rows
                '    If IsNumeric(r.Cells(0).Value) Then s &= r.Cells(0).Value & ";"

                'Next

                For ins = 0 To dgn.Rows.Count - 1
                    ReDim Preserve t(ins)
                    t(ins) = (dgn.Rows(ins).Cells(0).Value)
                    'If IsNumeric(dgn.Rows(ins).Cells(0).Value) Then s &= dgn.Rows(ins).Cells(0).Value & ";"
                    'Debug.Print(dgn.Rows(ins).Cells(0).Value)

                Next

                Array.Sort(t)
                Dim inst = t.Count - 1
                Do
                    If IsNumeric(t(inst)) Then s &= t(inst) & ";"
                    inst -= 1
                Loop Until inst = -1
                My.Settings.DeretBobot = s

                ' Debug.Print(My.Settings.DeretBobot)
                s = ""
                For Each r As GridViewRowInfo In dgk.Rows
                    s &= r.Cells(0).Value & ";"
                Next
                My.Settings.DeretKriteria = s
                bb_ref.PerformClick()

            Case bb_ref.Name
                dgn.Rows.Clear()
                dgk.Rows.Clear()
                Dim sb() As String = {}, sk() As String = {}
                Dim t = My.Settings.DeretBobot, u = My.Settings.DeretKriteria
                If t.Length > 0 Then sb = t.Split(";")
                If u.Length > 0 Then sk = u.Split(";")




                Dim r As New ArrayList
                For Each dr As DataRow In db.QueryTable("select nama from kriteria order by id_kriteria").Rows
                    r.Add(dr(0))
                Next
                For index1 = 0 To sk.Count - 1
                    For index2 = 0 To r.Count - 1
                        If r(index2) = sk(index1) AndAlso index2 + 1 < r.Count - 1 Then
                            Try
                                r.Insert(index1, r(index2))
                                r.RemoveAt(index2 + 1)
                            Catch
                            End Try
                        End If
                    Next
                Next

                For Each ini As String In r
                    Dim rowInfo As GridViewRowInfo = Me.dgk.Rows.AddNew()
                    rowInfo.Cells(0).Value = ini
                Next

                For Each ini As String In sb
                    If IsNumeric(ini) Then
                        Dim rowinfo As GridViewRowInfo = Me.dgn.Rows.AddNew()
                        rowinfo.Cells(0).Value = CDec(ini)
                    End If
                Next
                dgn_CellBeginEdit(Nothing, Nothing)
                Label3.Text = totalbobot

#End Region
#Region "pembeli"

            Case kat_ref.Name
                katalog(Nothing)
            Case pg_lanjut.Name
                'tombol lanjut pada jendela pembeli
                If pg_nama.Text.Length > 4 And pg_hp.Text.Length > 11 And pg_lamat.Text.Length > 10 Then
                    id_p = db.QueryStr("select id_pembeli from " + db.tabel_pembeli + " where no_hp='" + pg_hp.Text + "'")
                    If id_p.Length = 0 Then
                        id_p = db.IDBaruBerdasarTabel(db.tabel_pembeli, Nothing)
                        db.QueryIUD("insert into " + db.tabel_pembeli + " (id_pembeli, nama, no_hp, alamat) " +
                            "values ('" + id_p + "','" + pg_nama.Text + "','" + pg_hp.Text + "','" + pg_lamat.Text + "')")

                    Else

                        db.QueryIUD("update " & db.tabel_pembeli &
                                    " set nama='" & pg_nama.Text & "', alamat='" & pg_lamat.Text & "', no_hp='" & pg_hp.Text & "' " &
                                    "where id_pembeli='" + id_p + "' ")
                    End If
                    pgHitung(True)
                    pg_reset.PerformClick()

                Else
                    pesan("Periksa isian kembali, pastikan :" + Chr(13) + "- Nama lebih dari 4 karakter, " + Chr(13) +
                          "- Nomor Handphone 11 atau 12 angka," + Chr(13) +
                          "- Alamat lebih dari 10 karakter.", "Pastikan telah melengkapi data anda", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Case pg_reset.Name
                'tombol reset pada jendela pembeli
                pg_k.Items.Clear()
                pgss.Rows.Clear()
                pgn.Rows.Clear()
                DataGridView1.DataSource = Nothing
                DataGridView2.DataSource = Nothing
                strK = My.Settings.DeretBobot.Split(";")

                For Each s As String In strK
                    If IsNumeric(s) Then pgn.Rows.AddNew.Cells(0).Value = s
                Next


                For Each dr As DataRow In db.QueryTable("select nama from kriteria order by id_kriteria asc").Rows                   'mengisi kriteria kedalam tabel data prioritas
                    'Dim rowInfo As GridViewRowInfo = Me.pg_prioritas.Rows.AddNew()
                    'rowInfo.Cells(0).Value = dr(0)
                    pg_k.Items.Add(dr(0))
                Next

                'For index = 0 To strK.Count - 1
                '    Try
                '        pg_prioritas.Rows.Item(index).Cells(1).Value = strK(index)
                '    Catch
                '        Exit For
                '    End Try
                'Next

                pg_TabHasil.SelectedPage = I_dk
                Try
                    Timer1.Enabled = False
                    th.Abort()
                Catch
                End Try
                RadProgressBar1.Value1 = 0
                RadProgressBar1.Text = ""

                III_nR.Enabled = False
                IV_nV.Enabled = False
                V_mS.Enabled = False
                RadPageViewPage6.Enabled = False
                RadPageViewPage7.Enabled = False
                RadPageViewPage8.Enabled = False
            Case pg_tambah.Name
                If pg_k.Text.Length > 0 AndAlso pg_pil.Text.Length > 0 Then
                    For Each rr As GridViewRowInfo In pgss.Rows
                        If rr.Cells(0).Value = pg_k.Text Then
                            rr.Cells(1).Value = pg_pil.Text
                            Return
                        End If
                    Next
                    Dim rowInfo As GridViewRowInfo = Me.pgss.Rows.AddNew()
                    rowInfo.Cells(0).Value = pg_k.Text
                    rowInfo.Cells(1).Value = pg_pil.Text
                Else
                    MessageBox.Show("Nilai spesifikasi belum diisi.")
                End If
            Case pg_min.Name
                If pgss.CurrentRow IsNot Nothing Then pgss.CurrentRow.Delete()
            Case pg_cek.Name
                If pgss.Rows.Count < 3 Then MessageBox.Show("Silakan masukan minimal 3 kriteria/spesifikasi.") : Return

                DataGridView1.DataSource = Nothing
                DataGridView2.DataSource = Nothing
                Timer1.Enabled = False
                pg_TabHasil.SelectedPage = I_dk
                RadProgressBar1.Value1 = 0
                RadProgressBar1.Text = ""

                Dim isibobot As New DataTable("bobot")
                Dim nilai As String,
                    baris As DataRow,
                    id_krit() As String = Nothing
                'nilai krriteria dan order kriteria
                ' Dim i As Integer = 0
                nmk1.Clear()
                Dim col As New SortedList
                'For index = 0 To pgss.Rows.Count - 1
                'Next
                For Each ss As GridViewRowInfo In pgss.Rows
                    nmk1.Add(ss.Cells(0).Value)
                    col.Add(ss.Cells(0).Value.ToString.ToLower, ss.Cells(1).Value)

                Next
                For Each k As String In My.Settings.DeretKriteria.Split(";")
                    If Not nmk1.Contains(k) AndAlso k.Length > 0 Then
                        nmk1.Add(k)
                    End If
                Next

                'filter spesifikasi yang dipilih pembeli, sync-ing dgn db.vertkehorz ====== sorry hardcoded ===== need optimized w/ hash method
                'Dim filter = " where "
                'Dim id = "", val = ""



                '  id = "sp.id_kriteria='" + db.IDBerdasarNama(ss.Cells(0).Value) + "'"
                ' Val = "sp.nilai_sub_kriteria " & IIf(ss.Cells(1).Value.ToString.Contains(">") OrElse ss.Cells(1).Value.ToString.Contains("<"), " ", " =") & ss.Cells(1).Value 'kondisi disini buruk
                'Filter &= "(" & id & " and " & Val() & ") OR "

                'filter = filter.Remove(filter.Length - 3, 3)

                For index = 0 To pg_k.Items.Count - 1                             'redim array u/ menyimpan data prioritas yang diprioritaskang



                    'If pg_prioritas.Rows(i).Cells(0).Value.ToString.Length > 0 Then

                    If (Not nmk1.Contains(pg_k.Items.Item(index).Value)) And pg_k.Items.Item(index).Value IsNot Nothing Then
                        'ReDim Preserve nama_kriteria(index)
                        'ReDim Preserve nilai_Krit(index)
                        'nama_kriteria(index) = pgss.Rows(index).Cells(0).Value
                        nmk1.Add(pg_k.Items.Item(index).Value)
                        'i += 1
                    End If
                Next

                nmk1.Remove("Merek")
                nama_kriteria = nmk1.ToArray(System.Type.GetType("System.String"))
                For index = 0 To nmk1.Count - 1
                    ReDim Preserve nilai_Krit(index)
                    Try
                        nilai_Krit(index) = IIf(IsNumeric(strK(index)), strK(index), 0) 'pgn.Rows(index).Cells(0).Value
                    Catch ex As Exception
                        nilai_Krit(index) = 0
                    End Try
                    Debug.Print(nilai_Krit(index))
                    'RadCheckedDropDownList1.Items.Add(nama_kriteria(index) & " (Bobot " & nilai_Krit(index) & ")")
                    'RadCheckedDropDownList1.Items.Item(index).Checked = True
                Next

                'Debug.Print(My.Settings.DeretBobot)

                Dim tb As New DataTable
                tb = db.QueryTable(db.VERTkeHORZ(nama_kriteria))
                Dim tbi = tb.Rows.Count
                If pgss.RowCount > 0 Then

                    For Each r As DataRow In tb.Rows
                        ' For indec = 0 To tb.Columns.Count - 1
                        Dim modefiltertrueisand = CheckBox1.Checked
                        Try
                            For Each ch As String In col.Keys
                                If CheckBox1.Checked Then
                                    modefiltertrueisand = modefiltertrueisand And logika(r.Item(ch), col.Item(ch))
                                Else
                                    modefiltertrueisand = modefiltertrueisand Or logika(r.Item(ch), col.Item(ch))
                                End If
                            Next
                            'st = tb.Columns.Item(indec).ColumnName.ToLower
                            'nil = col.Item(st)
                            'If modefiltertrueisand Then
                            '    bo = bo Or logika(r.Item(indec), nil)
                            'Else
                            'End If
                        Catch
                        End Try
                        ' Next
                        If Not modefiltertrueisand Then r.Delete() : tbi -= 1
                    Next
                End If


                If tbi < 1 Then DataGridView1.DataSource = Nothing : Return
                Dim ds1 As New DataSet
                ds1.Tables.Add(tb)


                DataGridView1.DataSource = ds1
                DataGridView1.DataMember = ds1.Tables(0).TableName

                With DataGridView1
                    ReDim id_krit(.ColumnCount - 1)
                    For c As Integer = 0 To .ColumnCount - 1
                        isibobot.Columns.Add(DataGridView1.Columns.Item(c).HeaderText, System.Type.GetType("System.String"))
                        If c = 0 Then id_krit(c) = "0" Else id_krit(c) = db.IDBerdasarNama(.Columns.Item(c).HeaderText.ToLower)
                    Next

                    For r As Integer = 0 To .RowCount - 1
                        baris = isibobot.NewRow
                        baris.Item(isibobot.Columns.Item(0).ColumnName) = .Rows(r).Cells(0).Value
                        For c As Integer = 1 To .ColumnCount - 1

                            If .Rows(r).Cells(c).Value.ToString.Length = 0 Then
                                baris.Item(.Columns.Item(c).Index) = "0"
                            Else
                                nilai = db.QueryStr("select sk.bobot from sub_kriteria as sk where sk.id_kriteria='" + id_krit(c) + "' and sk.id_sub_kriteria ='" + db.deteksiSUB(id_krit(c), .Rows(r).Cells(c).Value) + "'")
                                If nilai.Length = 0 Then
                                    baris.Item(.Columns.Item(c).Index) = "0"
                                Else
                                    baris.Item(.Columns.Item(c).Index) = nilai

                                End If
                            End If
                            .Rows.Item(r).Cells(c).Value = .Rows.Item(r).Cells(c).Value & " (" & baris.Item(c) & ")"
                        Next
                        isibobot.Rows.Add(baris)
                    Next
                End With
                Dim ds As New DataSet
                ds.Tables.Add(isibobot)
                DataGridView2.Columns.Clear()
                DataGridView2.DataSource = ds
                DataGridView2.DataMember = ds.Tables(0).ToString
                tp = New C_Topsis
                tp.isi_data(nama_kriteria, nilai_Krit, isibobot)

                III_nR.Enabled = False
                IV_nV.Enabled = False
                V_mS.Enabled = False
                RadPageViewPage6.Enabled = False
                RadPageViewPage7.Enabled = False
                RadPageViewPage8.Enabled = False
                pg_mulai.Enabled = True
            Case pg_mulai.Name
                'tombol mulai pada jendela pembeli

                Dim s As String = Nothing
                If nilai_Krit Is Nothing OrElse nilai_Krit.Length = 0 Then
                    MessageBox.Show("Proses dibatalkan sebab tidak ada bobot kriteria.")
                    Return
                End If

                For Each r As GridViewRowInfo In pgss.Rows
                    s &= r.Cells(0).Value & " (" & r.Cells(1).Value & ") =" & nilai_Krit(r.Index) & "; "
                Next
                For i As Integer = 0 To nama_kriteria.Length - 1
                    If InStr(s, nama_kriteria(i)) = 0 Then
                        s = s + nama_kriteria(i) & "=" & nilai_Krit(i).ToString & "; "
                    End If
                Next
                bb_p = db.IDBaruBerdasarTabel(db.tabel_Bobot, Nothing)
                db.QueryIUD("insert into " & db.tabel_Bobot & " (id_bobot, sintaks_nilaibobot, id_pembeli) " &
                    "values ('" + bb_p + "','" + s + "','" + id_p + "')")
                If DataGridView2.ColumnCount > 2 Then
                    th = New Threading.Thread(AddressOf tp.proses)
                    Timer1.Enabled = True
                    th.Start()
                    sender.enabled = False
                Else
                    pesan("Dibutuhkan minimal 2 kriteria untuk memulai proses perhitungan TOPSIS." + Chr(13) + "Silakan tambah kriteria pada tabel 'Data Prioritas' dan klik tombol 'Cek'.", Nothing, galat)
                End If

            Case pg_selesai.Name

                pg_nama.Clear()
                pg_lamat.Clear()
                pg_hp.Clear()

                'pg_prioritas.DataSource = Nothing

                pgHitung(False)
#End Region
#Region "Kriteria"
            Case t_mSK.Name
                'tombol menuju Sub-kriteria
                filterSubKriteria = True

                cb_KritSK.Text = tx_id_kritK.Text
                bersihkanFormulir()
                nav_skrit.PerformClick()

            Case tb_brK.Name
                'tombol baru pada halaman kriteria
                ubahORbaru(mode.ModeBaru, sender)

            Case tb_ubK.Name
                'tombol ubah pada halaman kriteria
                ubahORbaru(mode.ModeEdit, sender)
                isiFormulir(dg_KinK)
                If iKArr.Contains(tx_nm_kritK.Text.ToLower) Then
                    tx_nm_kritK.Enabled = False
                    RadLabel1.ForeColor = Color.Green
                    RadLabel1.Text = "<html>Kriteria dengan nama <b>'" + tx_nm_kritK.Text + "'</b> merupakan kriteria pokok dalam Spesifikasi Kamera, mode 'Ubah' nama dimatikan.</html>"
                Else
                    RadLabel1.ForeColor = Color.Red
                    RadLabel1.Text = Nothing
                End If
            Case tb_hpK.Name
                'tombol hapus pada halaman kriteria
                If iKArr.Contains(tx_nm_kritK.Text.ToLower) Then
                    pesan("Kriteria yang dikehendaki merupakan spesifikasi utama kamera, perintah dibatalkan.", Nothing, galat)
                Else

                    If pesan(alertmsg, alert, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = DialogResult.Yes Then
                        Try
                            'hapus kriteria
                            db.QueryIUD("DELETE FROM " & db.tabel_Kriteria _
                                & " WHERE id_kriteria = '" & tx_id_kritK.Text & "'")
                            'hapus sub kriteria
                            db.QueryIUD("DELETE FROM " & db.tabel_SubKriteria _
                                & " WHERE id_kriteria = '" & tx_id_kritK.Text & "'")
                            'hapus bobot
                            db.QueryIUD("DELETE FROM " & db.tabel_Spek _
                                & " WHERE id_kriteria = '" & tx_id_kritK.Text & "'")
                        Catch ex As Exception
                            pesan(gagalHps + ex.Message, galat, MessageBoxButtons.OK, MessageBoxIcon.Error)

                        End Try
                        refreshDGV()
                    End If
                End If
            Case tb_spK.Name
                'tombol simpan pada halaman kriteria
                a = False
                If RadLabel1.Text.Length = 0 Then

                    Try
                        If modeBaru Then
                            'simpan mode buat baru
                            a = db.QueryIUD("insert into " & db.tabel_Kriteria _
                                        & "(id_kriteria, nama) values ('" _
                                        & tx_id_kritK.Text & "','" & tx_nm_kritK.Text & "')")
                        Else
                            'simpan mode edit
                            a = db.QueryIUD("update " & db.tabel_Kriteria _
                                        & " set nama='" & tx_nm_kritK.Text & "' " +
                                        "where id_kriteria = '" & tx_id_kritK.Text & "'")
                        End If
                    Catch ex As Exception
                        pesan(gagalSimpan + Chr(13) + "kode gagal (EN):" + ex.Message, galat, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Finally
                        If a Then
                            modeEdit(False)
                            tbObj.text = tbText
                            refreshDGV()
                        End If
                    End Try
                Else
                    Beep()
                End If
#End Region
#Region "Sub Kriteria"
            Case sk_reset.Name
                filterSubKriteria = False
                refreshDGV()
            Case sk_br.Name
                'tombol baru pada sub-kriteria
                ubahORbaru(mode.ModeBaru, sender)
            Case sk_ubh.Name
                'tombol ubah pada sub-kriteria
                ubahORbaru(mode.ModeEdit, sender)
                isiFormulir(dg_SKinSK)
            Case sk_hps.Name
                'tombol hapus pada sub-kriteria
                If pesan(alertmsg, alert, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = DialogResult.Yes Then

                    If Not db.QueryIUD("DELETE FROM " & db.tabel_SubKriteria _
                                       & " WHERE id_sub_kriteria = '" & tx_idSK.Text & "'") And
                                       db.QueryIUD("DELETE FROM " & db.tabel_Spek _
                                       & " WHERE id_sub_kriteria = '" & tx_idSK.Text & "'") Then

                        pesan(gagalHps, galat, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                    refreshDGV()
                End If
            Case sk_sp.Name
                'tombol simpan pada sub-kriteria
                If RadLabel2.Text.Length = 0 Then
                    a = False
                    Try
                        If modeBaru Then
                            'simpan mode buat baru
                            a = db.QueryIUD("insert into " & db.tabel_SubKriteria _
                                        & "(id_sub_kriteria, id_kriteria, nama, bobot) " _
                                        & "values ('" & tx_idSK.Text & "','" & cb_KritSK.Text & "','" & tx_nmSK.Text & "','" & nud_bSK.Value.ToString & "')")
                        Else
                            'simpan mode edit
                            a = db.QueryIUD("update " & db.tabel_SubKriteria _
                                        & " set nama='" & tx_nmSK.Text & "', id_kriteria='" & cb_KritSK.Text & "',  bobot='" & nud_bSK.Value _
                                        & "' where id_sub_kriteria = '" & tx_idSK.Text & "'")
                        End If
                    Catch ex As Exception
                        pesan(gagalSimpan, ex, galat)
                    Finally
                        If a Then
                            modeEdit(False)
                            tbObj.text = tbText
                        End If
                        refreshDGV()
                    End Try
                End If
#End Region
#Region "Spesifikasi Kamera"

            Case tb_tb_km.Name
                'tombol tambah kamera pada jendela spek kamera
                ubahORbaru(mode.ModeBaru, sender)
            Case tb_hp_km.Name
                'tombol hapus kamera pada jendela spek kamera
                If pesan(alertmsg, alert, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = DialogResult.Yes Then
                    Try
                        db.QueryIUD("DELETE FROM " & db.tabel_Spek _
                                       & " WHERE id_kamera = '" & sp_idtx.Text & "'")
                        db.QueryIUD("DELETE FROM " & db.tabel_Kamera _
                                       & " WHERE id_kamera = '" & sp_idtx.Text & "'")
                    Catch ex As Exception
                        pesan(gagalHps + ex.Message, galat, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Finally

                        refreshDGV()

                    End Try

                End If
            Case tb_tb_sp.Name
                'tombol tambah spesifikasi pada jendela spek kamera
                If db.QueryTable("select * from spek_kamera where id_kriteria='" + sp_lbK.Text + "' and id_kamera='" + sp_idtx.Text + "'").Rows.Count > 0 Then
                    pesan("Maaf telah terdapat spesifikasi dengan nama kriteria '" + sp_krit.Text + "' didalam spesifikasi, Perintah dibatalkan.", galat, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    db.QueryIUD("insert into " + db.tabel_Spek + " (id_kriteria, id_kamera,nilai_sub_kriteria) " +
                                "values ('" + sp_lbK.Text + "','" + sp_idtx.Text + "','" + sp_skrit.Text + "')")
                    invalidateSpek()
                End If

            Case tb_ub_sp.Name
                'tombol ubah spesifikasi pada jendela spek kamera
                ubahORbaru(mode.ModeEdit, sender)
                sp_merek.Enabled = False
                If sp_krit.Items.Count > 0 Then sp_krit.Items.Clear()

                For Each ddd As DataRow In db.QueryTable("select nama from kriteria").Rows
                    sp_krit.Items.Add(ddd.Item(0))
                Next
                isiFormulir(dg_kam)
            Case tb_sp_sp.Name
                'tombol simpan pada jendela spek kamera

                a = False
                Try
                    If modeBaru Then
                        'simpan mode buat baru
                        Try
                            db.QueryIUD("insert into " + db.tabel_Kamera + " (id_kamera,nama_tipe) values ('" + sp_idtx.Text + "','" + sp_tipe.Text + "') ")
                            For index = 0 To iKArr.Length - 1
                                db.QueryIUD("Insert into " + db.tabel_Spek + " (id_kriteria, id_kamera,  nilai_sub_kriteria) " +
                                   "Values ('" + db.IDBerdasarNama(iKArr(index)) + "','" + sp_idtx.Text + "','" + sArr(index) + "')")
                            Next
                            a = True
                        Catch ex As Exception
                            pesan(gagalSimpan, ex, galat)
                        End Try
                    Else
                        'simpan mode edit
                        db.QueryIUD("update " + db.tabel_Kamera + " set nama_tipe='" + sp_tipe.Text + "'" +
                                        "where id_kamera='" + sp_idtx.Text + "'")
                        For index = 0 To iKArr.Length - 1
                            If db.QueryTable("select * from " + db.tabel_Spek + " where id_kamera='" + sp_idtx.Text + "' and id_kriteria='" + db.IDBerdasarNama(iKArr(index)) + "'").Rows.Count > 0 Then
                                db.QueryIUD("update " + db.tabel_Spek + " set nilai_sub_kriteria='" + sArr(index) + "'" +
                                        "where id_kamera='" + sp_idtx.Text + "' and id_kriteria='" + db.IDBerdasarNama(iKArr(index)) + "'")
                            Else
                                db.QueryIUD("Insert into " + db.tabel_Spek + " (id_kriteria, id_kamera,  nilai_sub_kriteria) " +
                                   "Values ('" + db.IDBerdasarNama(iKArr(index)) + "','" + sp_idtx.Text + "','" + sArr(index) + "')")
                            End If

                        Next
                        a = True
                    End If
                    Try
                        If sp_pb.Image IsNot Nothing Then db.simpanGambar(sp_pb.Image, sp_idtx.Text)
                    Catch ex As Exception
                        pesan("Gagal menyimpan gambar, keterangan galat: " + ex.Message, Nothing, galat)
                    End Try
                Catch ex As Exception
                    pesan(gagalSimpan, ex, galat)
                Finally
                    If a Then
                        modeEdit(False)
                        tbObj.text = tbText

                        If modeBaru Then refreshDGV() : invalidateSpek() Else invalidateSpek() : refreshDGV()

                    End If
                End Try


            Case tb_hp_sp.Name

                If iKArr.Contains(sp_krit.Text.ToLower) Then
                    pesan("Spesifikasi yang akan dihapus adalah spesifikasi utama, perintah dibatalkan." + Chr(13) + "Nama Kriteria : " + sp_krit.Text, galat, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    If pesan(alertmsg, alert, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = DialogResult.Yes Then
                        If Not db.QueryIUD("DELETE FROM " & db.tabel_Spek _
                                           & " WHERE id_kriteria = '" & sp_lbK.Text & "'") And Not db.hapusGambar(sp_idtx.Text) Then
                            pesan(gagalHps, galat, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End If
                        invalidateSpek()
                    End If

                End If
#End Region
        End Select

    End Sub

    Private Sub dgn_CellBeginEdit(sender As Object, e As GridViewCellCancelEventArgs) Handles dgn.CellBeginEdit
        totalbobot = 0
        For Each info As GridViewRowInfo In dgn.Rows
            totalbobot += info.Cells(0).Value
        Next
    End Sub

    Private Sub sp_iso_Leave(sender As Object, e As EventArgs) Handles sp_iso.Leave

        Dim iso() = sp_iso.Text.Split("-")
        Try
            tb_sp_sp.Enabled = iso.Length = 2 OrElse (Not IsNumeric(iso(0))) OrElse (Not IsNumeric(iso(1)))
        Catch

            tb_sp_sp.Enabled = False
        End Try
        If Not tb_sp_sp.Enabled Then
            MessageBox.Show("Silakan cek isian ISO ")
        End If
    End Sub

    Private Sub sp_merek_TextChanged(sender As Object, e As EventArgs) Handles sp_merek.TextChanged
        If Not modeBaru Then Return
        If sp_merek.Items.Contains(sp_merek.Text) Then
            sp_idtx.Text = db.IDBaruBerdasarTabel(db.tabel_Kamera, sp_merek.Text.First)
        Else
            sp_idtx.Text = db.IDBaruBerdasarTabel(db.tabel_Kamera, Nothing)
        End If

    End Sub

    Private Sub sp_iso_KeyPress(sender As Object, e As KeyPressEventArgs) Handles sp_iso.KeyPress
        e.Handled = Not (Char.IsNumber(e.KeyChar) Or e.KeyChar = CChar("-") Or e.KeyChar = Chr(8))
    End Sub

    Private Sub dgn_ValueChanging(sender As Object, e As ValueChangingEventArgs) Handles dgn.ValueChanging
        totalbobot += e.NewValue - e.OldValue
        e.Cancel = totalbobot > 100 And e.NewValue > e.OldValue
        Label3.Text = totalbobot
    End Sub

    Private Function logika(ByVal diuji As Object, ByVal penguj As Object) As Boolean
        Try
            If diuji = penguj Then Return True
            Dim penguji = db.hapusKarakterTidakDipakai(penguj)

            Dim bog As Boolean = penguji.ToString.Contains(">"),
            bol As Boolean = penguji.ToString.Contains("<"),
            bot As Boolean = penguji.ToString.Contains("-"),
            bos As Boolean = InStr(penguji, "/") = 0
            'If bog Or bol Or (bot And Not bos) Then
            '    If Not bot Then
            '        If bog Then
            '            Return CDbl(diuji) > CDbl(penguji.ToString.Replace(">", "").Replace(" ", ""))
            '        ElseIf bol Then
            '            Return CDbl(diuji) < CDbl(penguji.ToString.Replace("<", "").Replace(" ", ""))
            '        ElseIf bot Then
            '            Dim sp() = penguji.ToString.Replace("<", "").Replace(" ", "").Split("-"),
            '                sd() = diuji.ToString.Replace("<", "").Replace(" ", "").Split("-")
            '            Return True
            '            'If sp.Count = 2 And sd.Count = 2 Then
            '            '    Return db.nilaiAntara(sd(0), )
            '            'End If
            '        End If
            '    End If

            'End If
            If IsNumeric(diuji) Then

                If bog Then
                    Return CDbl(diuji) > CDbl(penguji.ToString.Replace(">", "").Replace("=", "").Replace(" ", ""))
                ElseIf bol Then
                    Return CDbl(diuji) < CDbl(penguji.ToString.Replace("<", "").Replace("=", "").Replace(" ", ""))
                Else
                    Dim sp() = penguji.ToString.Replace(" ", "").Split("-")
                    If sp.Length = 2 Then Return db.nilaiAntara(CDbl(diuji), CDbl(sp(0)), CDbl(sp(1)))
                End If
            ElseIf diuji.ToString.Contains("-") AndAlso Not diuji.ToString.Contains("/") Then
                Dim dp() = diuji.ToString.Replace(" ", "").Split("-")
                If bog Then
                    Dim sp() = penguji.ToString.Replace(">", "").Replace(" ", "").Replace("=", "").Split("-")

                    If sp.Length = 2 AndAlso dp.Length = 2 Then Return CDbl(dp(0)) >= CDbl(sp(0)) AndAlso CDbl(dp(1)) >= CDbl(sp(1))
                ElseIf bol Then
                    Dim sp() = penguji.ToString.Replace("<", "").Replace(" ", "").Replace("=", "").Split("-")

                    If sp.Length = 2 AndAlso dp.Length = 2 Then Return CDbl(dp(0)) <= CDbl(sp(0)) AndAlso CDbl(dp(1)) <= CDbl(sp(1))
                Else
                    Dim sp() = penguji.ToString.Replace(" ", "").Split("-")
                    If sp.Length = 4 AndAlso dp.Length = 2 Then Return db.nilaiAntara(CDbl(dp(0)), CDbl(sp(0)), CDbl(sp(2))) AndAlso db.nilaiAntara(CDbl(dp(1)), CDbl(sp(1)), CDbl(sp(3)))
                End If

            End If
        Catch
            MessageBox.Show("Terdapat nilai yang tidak dapat diuji (diambil nilai bobotnya).")
            Return False
        End Try
        Return False
    End Function

    Private Sub namaTx(sender As Object, e As KeyPressEventArgs) Handles pg_nama.KeyPress, pg_hp.KeyPress
        Select Case sender.name
            Case pg_hp.Name
                e.Handled = (Not ((Char.IsNumber(e.KeyChar) AndAlso pg_hp.Text.Length < 12 Or e.KeyChar = Chr(8))))
            Case pg_nama.Name
                e.Handled = (Not (Char.IsLetter(e.KeyChar) Or e.KeyChar = Chr(8) Or e.KeyChar = CChar(" ")))

        End Select
    End Sub

    Private Sub paintcell(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView4.CellClick, DataGridView3.CellClick, DataGridView2.CellClick, DataGridView1.CellClick
        Try
            DataGridView1.CurrentCell = DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex)
            DataGridView2.CurrentCell = DataGridView2.Rows(e.RowIndex).Cells(e.ColumnIndex)

            DataGridView3.CurrentCell = DataGridView3.Rows(e.RowIndex).Cells(e.ColumnIndex)
            DataGridView4.CurrentCell = DataGridView4.Rows(e.RowIndex).Cells(e.ColumnIndex)
        Catch
        End Try
    End Sub

    Private Sub pgHitung(v As Boolean)

        pg_reset.Enabled = v
        pg_cek.Enabled = v
        'pg_prioritas.Enabled = v
        pg_TabHasil.Enabled = v
        pg_tambah.Enabled = v
        pg_ly.Enabled = v
        pg_min.Enabled = v
        pgss.Enabled = v
        pgn.Enabled = v

        pg_selesai.Enabled = v
    End Sub


    Private Sub pic(sender As Object, e As EventArgs) Handles sp_pb.Click

        Try
            'Filter extensi file gambar
            If OFD.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim fsImage As New System.IO.FileStream(OFD.FileName, IO.FileMode.Open,
                IO.FileAccess.Read)

                Dim MyImage As Image = Image.FromStream(fsImage)
                sp_pb.Image = MyImage
                fsImage.Close()
                'fsImage.Dispose()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, imgfailload)
        End Try
    End Sub

    Private Sub dragForm(sender As Object, e As MouseEventArgs) Handles Panel3.MouseDown, FlowLayoutPanel1.MouseDown
        C_Util.move(Handle)
    End Sub
#End Region


#Region "Behavior"
    Private Sub refreshDGV()
        'method untuk merefresh data pada tabel data kriteria
        Me.KriteriaTableAdapter.Fill(Me.Data_Set.Kriteria)
        Me.Sub_KriteriaTableAdapter.Fill(Me.Data_Set.Sub_Kriteria)
        ' Me.KameraTableAdapter.Fill(Me.Data_Set.Kamera)
        dg_kam.DataSource = db.QueryDS("select distinct k.id_kamera as ID, k.nama_tipe as TIPE, s.nilai_sub_kriteria As MEREK from kamera as k inner join spek_kamera as s on k.id_kamera=s.id_kamera where s.id_kriteria='" + db.IDBerdasarNama("merek") + "'")
        If filterSubKriteria Then
            dg_SKinSK.DataSource = db.QueryDS("select sk.id_sub_kriteria as [Id Sub-kriteria], sk.id_kriteria as [Id Kriteria],k.nama as [Nama Kriteria], sk.nama as [Nama Sub-kriteria],sk.bobot as [Bobot Sub] from sub_kriteria as sk inner join kriteria as k on sk.id_kriteria=k.id_kriteria where k.id_kriteria='" + cb_KritSK.Text + "'")
        Else
            dg_SKinSK.DataSource = db.QueryDS("select sk.id_sub_kriteria as [Id Sub-kriteria], sk.id_kriteria as [Id Kriteria],k.nama as [Nama Kriteria], sk.nama as [Nama Sub-kriteria],sk.bobot as [Bobot Sub] from sub_kriteria as sk inner join kriteria as k on sk.id_kriteria=k.id_kriteria")
        End If
        invalidateSpek()
        DataGridView8.DataSource = db.QueryDS("select id_pembeli as [ID pembeli], nama AS NAMA, no_hp AS HANDPHONE, alamat as [ALAMAT] from " & db.tabel_pembeli)
    End Sub



    Private Sub tickMulaiProses(sender As Object, e As EventArgs) Handles Timer1.Tick
        If th.ThreadState = Threading.ThreadState.Stopped Then RadProgressBar1.Value1 = 100 : RadProgressBar1.Text = ""
        If RadProgressBar1.Value1 < 100 Then
            If tp.kelajuanProses > 100 Then
                RadProgressBar1.Value1 = 100
            Else
                RadProgressBar1.Value1 = tp.kelajuanProses
            End If
            RadProgressBar1.Text = "---" & tp.kelajuanProses & "%---"
        Else
            DataGridView3.DataSource = tp.getDataSource(C_Topsis.matriks.normalisai_R)
            DataGridView4.DataSource = tp.getDataSource(C_Topsis.matriks.normalisasi_V)
            DataGridView5.DataSource = tp.getDataSource(C_Topsis.matriks.solusi_ideal)
            DataGridView6.DataSource = tp.getDataSource(C_Topsis.matriks.Mseparasi)
            DataGridView7.DataSource = tp.getDataSource(C_Topsis.matriks.Kedekatan_Rltf)
            DataGridView11.DataSource = tp.getDataSource(C_Topsis.matriks.Hasil)
            Timer1.Enabled = False
            III_nR.Enabled = True
            IV_nV.Enabled = True
            V_mS.Enabled = True
            RadPageViewPage6.Enabled = True
            RadPageViewPage7.Enabled = True
            RadPageViewPage8.Enabled = True
            RadProgressBar1.Value1 = 100
            Dim str As New ArrayList
            For Each d As DataGridViewRow In DataGridView11.Rows
                str.Add(d.Cells(0).Value.ToString.Replace(" - ", "-").Split("- ")(1))
            Next
            katalog(str.ToArray)
            pg_TabHasil.SelectedPage = RadPageViewPage8
            Dim data = db.QueryTable(db.VERTkeHORZ(Nothing, True) & " where km.nama_tipe='" + str.Item(0) + "'")
            Dim ket As String
            Try
                ket = "Kamera paling sesuai dengan spesifikasi yang anda inginkan adalah kamera merek "
                'ket &= DataGridView11.Rows(0).Cells(1).Value
                'ket &= " Kamera DSLR dengan Merk "
                ket &= db.QueryStr("select nilai_sub_kriteria from spek_kamera where id_kriteria='" & db.IDBerdasarNama("merek") & "' and id_kamera='" + DataGridView11.Rows(0).Cells(0).Value.ToString.Replace(" - ", "-").Split("- ")(0) + "'")
                ket &= " " & DataGridView11.Rows(0).Cells(0).Value.ToString.Replace(" - ", "-").Split("- ")(1)
                ket &= ", sedangkan kamera paling mendekati dengan spesifikasi yang anda ingnkan adalah kamera merek "

                ket &= db.QueryStr("select nilai_sub_kriteria from spek_kamera where id_kriteria='" & db.IDBerdasarNama("merek") & "' and id_kamera='" + DataGridView11.Rows(1).Cells(0).Value.ToString.Replace(" - ", "-").Split("- ")(0) + "'")
                ket &= " " & DataGridView11.Rows(1).Cells(0).Value.ToString.Replace(" - ", "-").Split("- ")(1)
                'ket &= " dengan nilai "
                'ket &= DataGridView11.Rows(1).Cells(1).Value
                ket &= ". "
            Catch
                ket = "-"
            End Try
            Label29.Text = ket
            db.QueryIUD("insert into " & db.tabel_HR & "(id_hasil_rekomendasi, id_bobot, id_kamera, jumlah_nilai,ket) " +
                        "values ('" & db.IDBaruBerdasarTabel(db.tabel_HR, Nothing) & "','" &
                        bb_p & "','" &
                       data.Rows.Item(0).Item(0) & "','" &
                        DataGridView11.Rows(0).Cells(1).Value & "','" & ket & "')")

        End If

    End Sub

    Private Sub bersihkanFormulir()

        tx_id_kritK.Clear()
        tx_nm_kritK.Clear()

        tx_idSK.Clear()
        tx_nmSK.Clear()
        nud_bSK.Value = 0

        sp_tipe.Clear()
        sp_hrg.Value = 0

        sp_hrg.Value = 0
        sp_sensor.Value = 0
        sp_LCD.Value = 0

        sp_iso.Clear()
        sp_focus.Value = 0

    End Sub

    Private Sub modeEdit(status As Boolean)
        'status tombol navigasi
        nav_cat.Enabled = Not status
        nav_pg.Enabled = Not status
        nav_krit.Enabled = Not status
        nav_skrit.Enabled = Not status
        nav_spek.Enabled = Not status
        nav_log.Enabled = Not status

        'status tabel
        dg_KinK.Enabled = Not status
        dg_SKinK.Enabled = Not status
        dg_SKinSK.Enabled = Not status
        dg_kam.Enabled = Not status
        dg_spek.Enabled = status

        'mode input true= mode tambah baru, false=mode edit
        modeinput = status

        'tombol editor pada jendela kriteria
        tb_brK.Enabled = Not status
        tb_ubK.Enabled = Not status
        tb_hpK.Enabled = Not status
        tb_spK.Enabled = status

        'tombol editor pada jendela sub-kriteria
        sk_br.Enabled = Not status
        sk_ubh.Enabled = Not status
        sk_hps.Enabled = Not status
        sk_sp.Enabled = status

        'tombol editor pada jendela spek
        tb_tb_km.Enabled = Not status
        tb_hp_km.Enabled = Not status
        tb_sp_sp.Enabled = status

        tb_tb_sp.Enabled = status
        tb_ub_sp.Enabled = Not status
        tb_hp_sp.Enabled = status

        'formulir isian pada jendela kriteria
        tx_nm_kritK.Enabled = status
        tx_nm_kritK.ShowClearButton = status

        'formulir isian pada jendela sub-kriteria
        tx_nmSK.Enabled = status
        cb_KritSK.Enabled = IIf(filterSubKriteria, False, status)
        nud_bSK.Enabled = status

        'formulir isian pada jendela spek
        sp_pb.Enabled = status
        sp_tipe.Enabled = status
        sp_merek.Enabled = status
        sp_hrg.Enabled = status
        sp_sensor.Enabled = status
        sp_LCD.Enabled = status
        sp_koneksiwifi.Enabled = status
        sp_shutterS.Enabled = status
        sp_iso.Enabled = status
        sp_focus.Enabled = status
        sp_flash.Enabled = status
        'sp_thn.Enabled = status
        sp_krit.Enabled = status
        sp_skrit.Enabled = status

        isiPilihan()

    End Sub

    Private Sub isiPilihan()

        Dim dttb As DataTable
        Select Case pg_cat.SelectedPage.Name
            Case pg_kat.Name

            Case pg_tp.Name

            Case pg_krit.Name
                tx_id_kritK.Text = db.IDBaruBerdasarTabel(db.tabel_Kriteria, Nothing)
            Case pg_skrit.Name
                tx_idSK.Text = db.IDBaruBerdasarTabel(db.tabel_SubKriteria, Nothing)
                If Not filterSubKriteria Then
                    cb_KritSK.Items.Clear()
                    dttb = db.QueryTable("select * from " & db.tabel_Kriteria)
                    For Each d As DataRow In dttb.Rows
                        cb_KritSK.Items.Add(d(0) & " | " & d(1))
                    Next
                    dttb.Dispose()
                End If


            Case pg_spek.Name
                sp_idtx.Text = db.IDBaruBerdasarTabel(db.tabel_Kamera, Nothing)
                sp_merek.Items.Clear()
                sp_koneksiwifi.Items.Clear()
                sp_shutterS.Items.Clear()
                sp_flash.Items.Clear()
                'sp_thn.Items.Clear()


                Dim str As String = "select sk.nama from " & db.tabel_SubKriteria & " as sk where sk.id_kriteria= "
                dttb = db.QueryTable(str & "'" + db.QueryStr("select id_kriteria from Kriteria where nama like 'merek'") + "'")
                For Each d As DataRow In dttb.Rows
                    sp_merek.Items.Add(d(0))
                Next

                dttb = db.QueryTable(str & "'" + db.QueryStr("select id_kriteria from Kriteria where nama like '%wifi%'") + "'")
                For Each d As DataRow In dttb.Rows
                    sp_koneksiwifi.Items.Add(d(0))
                Next
                dttb = db.QueryTable(str & "'" + db.QueryStr("select id_kriteria from Kriteria where nama like  '%flash%' ") + "'")
                For Each d As DataRow In dttb.Rows
                    sp_flash.Items.Add(d(0))
                Next
                dttb = db.QueryTable(str & "'" + db.QueryStr("select id_kriteria from Kriteria where nama like  '%shutter speed%' ") + "'")
                For Each d As DataRow In dttb.Rows
                    sp_shutterS.Items.Add(d(0))
                Next
                dttb.Dispose()

                'For index = -2 To 50
                '    sp_thn.Items.Add(Date.Now.Year - index)

                'Next

            Case pg_log.Name

        End Select

    End Sub

    Private Sub seleksimenu(isvisible As Boolean)
        isAdmin = isvisible
        If Not isvisible Then
            nav_cat.PerformClick()
        End If

        nav_krit.Visible = isvisible
        nav_skrit.Visible = isvisible
        nav_bobot.Visible = isvisible
        nav_spek.Visible = isvisible
        nav_log.Visible = isvisible
    End Sub

    Private Sub remv()
        tb_dummy.Select()
    End Sub

    Private Sub isiFormulir(OBJ As Object)
        Select Case OBJ.Name
            Case dg_KinK.Name
                'tabel kriteria pada jendela kriteria
                Try
                    tx_id_kritK.Text = OBJ.CurrentRow.Cells(0).Value
                    tx_nm_kritK.Text = OBJ.CurrentRow.Cells(1).Value
                    dg_SKinK.DataSource = db.QueryDS("select nama as NAMA, bobot as BOBOT from sub_kriteria where id_kriteria='" + tx_id_kritK.Text + "' order by id_sub_kriteria")
                Catch
                End Try
            Case dg_SKinSK.Name
                'tabel sub-kriteria pada jendela sub-kriteria
                Try
                    cb_KritSK.Text = OBJ.CurrentRow.Cells(1).Value
                    tx_idSK.Text = OBJ.CurrentRow.Cells(0).Value
                    tx_nmSK.Text = OBJ.CurrentRow.Cells(3).Value
                    nud_bSK.Value = OBJ.CurrentRow.Cells(4).Value
                Catch
                End Try
            Case dg_kam.Name
                'tabel kamera pada jendela spek
                If OBJ.CurrentRow Is Nothing Then Return
                sp_tipe.Text = OBJ.CurrentRow.Cells(1).Value
                sp_idtx.Text = OBJ.CurrentRow.Cells(0).Value
                sp_pb.Image = db.bacaGambr(OBJ.CurrentRow.Cells(0).Value)

                invalidateSpek()
                Dim ss As New ArrayList
                For index = 0 To iKArr.Length - 1
                    ss.Add(db.QueryStr("select sp.nilai_sub_kriteria " +
                                                 "from spek_kamera as sp inner join kamera as km on sp.id_kamera=km.id_kamera Where km.id_kamera='" + OBJ.CurrentRow.Cells(0).Value + "' " +
                                                 "and sp.id_kriteria = (select id_kriteria from kriteria where nama like '" + iKArr(index) + "')  "))

                Next
                sp_merek.Text = ss(0)
                sp_hrg.Value = IIf(ss(1) <> "", ss(1), 0)
                sp_sensor.Value = IIf(ss(2) <> "", ss(2), 0)
                sp_LCD.Value = IIf(ss(3) <> "", ss(3), 0)

                sp_koneksiwifi.Text = ss(4)
                sp_shutterS.Text = ss(5)
                sp_iso.Text = ss(6)
                sp_focus.Value = IIf(ss(7) <> "", ss(7), 0)
                sp_flash.Text = ss(8)
                'sp_thn.Text = ss(9)
                ss.Clear()

            Case dg_spek.Name
                'tabel speksifikasi padajendela spek kamera
                sp_lbK.Text = OBJ.CurrentRow.Cells(0).Value
                sp_krit.SelectedIndex = sp_krit.FindString(OBJ.CurrentRow.Cells(1).Value)
                sp_skrit.Text = OBJ.CurrentRow.Cells(2).Value
            Case DataGridView8.Name
                DataGridView10.DataMember = ""
                Label32.ResetText()
                DataGridView9.DataSource = db.QueryDS("select id_bobot as [ID BOBOT], id_pembeli AS [ID pembeli], sintaks_nilaibobot AS [NILAI BOBOT] from bobot where id_pembeli='" + OBJ.CurrentRow.Cells(0).Value + "'")
            Case DataGridView9.Name
                Dim sintaks() As String = DataGridView9.CurrentRow.Cells(2).Value.ToString.Replace("; ", ";").Split(";")
                Dim nilai() As String, datat As New DataTable("K"), dr As DataRow, ds As New DataSet
                DataGridView12.DataSource = db.QueryDS("select * from hasil_rekomendasi where id_bobot='" & OBJ.CurrentRow.Cells(0).Value & "'")
                Label32.Text = db.QueryStr("select ket from hasil_rekomendasi where id_bobot='" & OBJ.CurrentRow.Cells(0).Value & "'")

                datat.Columns.Add("KRITERIA")
                datat.Columns.Add("NILAI")
                For Each s As String In sintaks
                    dr = datat.NewRow
                    nilai = s.Split("=")
                    dr.ItemArray = nilai
                    datat.Rows.Add(dr)
                Next
                ds.Tables.Add(datat)
                DataGridView10.DataSource = ds
                DataGridView10.DataMember = ds.Tables(0).TableName
        End Select
    End Sub

    Private Sub invalidateSpek()
        Try
            dg_spek.DataSource = db.QueryDS("SELECT Kriteria.id_kriteria as [ID Kriteria], Kriteria.nama as [Kriteria], Spek_kamera.nilai_sub_kriteria as [Nilai] " +
                                                              "FROM Kriteria INNER JOIN (Kamera INNER JOIN Spek_kamera ON Kamera.id_kamera = Spek_kamera.id_kamera) " +
                                                              "ON Kriteria.id_kriteria = Spek_kamera.id_kriteria " +
                                                              "where Kamera.id_kamera = '" & dg_kam.CurrentRow.Cells(0).Value & "'  order by spek_kamera.id_kriteria asc")
        Catch
        End Try

    End Sub

    Private Sub katalog(filter() As Object)

        k = New C_Katalog
        k.filter(filter)
        th = New Threading.Thread(AddressOf k.proses)
        AddHandler k.selesai, AddressOf isiPage
        th.Start()
    End Sub
    Private Sub isiPage()
        Select Case pg_cat.SelectedPage.Name
            Case pg_kat.Name

                WebBrowser1.DocumentText = k.html
            Case pg_tp.Name

                WebBrowser2.DocumentText = k.html
            Case Else

        End Select
    End Sub
    Private Function sArr() As String()
        'need optimizing
        'Public Shared iKArr As String() = {"harga", "sensor", "lcd", "koneksi wifi", "kecepatan shutter speed", "iso", "titik fokus", "built in flash", "tanggal produksi"} 'nama^ kriteria untuk formulir isian spesifikasi 
        Return {sp_merek.Text, sp_hrg.Value, sp_sensor.Value, sp_LCD.Value, sp_koneksiwifi.Text, sp_shutterS.Text, sp_iso.Text, sp_focus.Value, sp_flash.Text} 'nama^ control untuk formulir isian spesifikasi kamera sesuai ikarr pada C_HandleDB
    End Function


    Private Sub ubahORbaru(ByRef modeVal As mode, ByRef sender As Object)
        Select Case modeVal
            Case mode.ModeBaru
                If sender.Text = batal Then
                    tbObj.text = tbText
                    bersihkanFormulir()
                    modeEdit(False)

                Else
                    tbText = sender.text
                    tbObj = sender
                    sender.Text = batal
                    bersihkanFormulir()
                    modeBaru = True
                    modeEdit(True)
                End If

            Case mode.ModeEdit
                If sender.Text = batal Then
                    tbObj.text = tbText
                    modeEdit(False)
                Else
                    tbText = sender.text
                    tbObj = sender
                    sender.Text = batal
                    modeBaru = False
                    modeEdit(True)
                End If
        End Select
        sender.enabled = True
    End Sub

#End Region

End Class