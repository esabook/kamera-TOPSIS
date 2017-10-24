Public Class C_Topsis
    Dim nama_Kriteria() As String = Nothing,
        nilai_Kriteria() As Double = Nothing,
        tabel_Keputusan As DataTable = Nothing,
        skip_IndexColumn As Integer = 1,
        Jumlah_HAsil_Teratas As Integer = 10,
        pembulatanbelakangkoma = 3,
        proses_Num As Integer = 0,
        proses_MaxNum As Integer = Nothing,
        AP = "IDEAL POSITIF (A+)",
        AN = "IDEAL NEGATIF (A-)",
        SP = "SEPARASI POSITIF (S+)",
        SN = "SEPARASI NEGATIF (S-)",
        KR = "KEDEKATAN RELATIF",
        HS = "HASIL PERHITUNGAN TOPSIS: " & Jumlah_HAsil_Teratas & " TERUNGGUL",
        gunakanNilaiNaN = False

    Private normalisai_R As DataTable = Nothing
    Private normalisasi_V As DataTable = Nothing
    Private solusi_ideal As DataTable = Nothing 'berisi 2 baris <baris pertama = A+; kedua = A->
    Private M_separasi As New DataTable("S")    'berisi 2 kolom <kolom pertama = s+; kedua = S-> dihitung dari nilai 'skipIndexColumn'
    Private Kedekatan_Rltf As New DataTable("R")
    Private Hasil As New DataTable("H")

    Public kelajuanProses As Integer = 0


    Public Enum matriks
        normalisai_R
        normalisasi_V
        solusi_ideal
        Mseparasi
        Kedekatan_Rltf
        Hasil
    End Enum
    Public Function getDataTable(ByRef matriks_topsis As matriks) As DataTable
        Select Case matriks_topsis
            Case matriks.normalisai_R
                Return normalisai_R
            Case matriks.normalisasi_V
                Return normalisasi_V
            Case matriks.solusi_ideal
                Return solusi_ideal
            Case matriks.Mseparasi
                Return M_separasi
            Case matriks.Kedekatan_Rltf
                Return Kedekatan_Rltf
            Case matriks.Hasil
                Return Hasil
        End Select
        Return Nothing
    End Function
    Public Function getDataSource(ByRef matriks_topis As matriks) As BindingSource
        Dim ds As New DataSet
        Dim record As New BindingSource

        Select Case matriks_topis
            Case matriks.normalisai_R
                ds.Tables.Add(normalisai_R)

            Case matriks.normalisasi_V
                ds.Tables.Add(normalisasi_V)
            Case matriks.solusi_ideal
                ds.Tables.Add(solusi_ideal)
            Case matriks.Mseparasi
                ds.Tables.Add(M_separasi)
            Case matriks.Kedekatan_Rltf
                ds.Tables.Add(Kedekatan_Rltf)
            Case matriks.Hasil
                ds.Tables.Add(Hasil)
        End Select
        record.DataSource = ds
        record.DataMember = ds.Tables(0).TableName
        Return record
    End Function

    Public Sub isi_data(ByRef namakriteria() As String, ByRef nilaikriteria() As Double, ByRef tabelkeputusan As DataTable)
        Try
            M_separasi.Dispose()
            Kedekatan_Rltf.Dispose()
            Hasil.Dispose()
        Catch

        End Try

        nama_Kriteria = namakriteria
        nilai_Kriteria = nilaikriteria
        tabel_Keputusan = tabelkeputusan
    End Sub
    Public Sub proses()

        proses_MaxNum = ((tabel_Keputusan.Columns.Count - 1 - skip_IndexColumn) * (tabel_Keputusan.Rows.Count)) * 6 'norm_R=2 putaran; norm_v=1; solusiIdeal=1; separasi=1; kede_rltf=1
        norm_R()
        norm_V()
        solusiIdeal()
        separasi()
        kedekatanRelatif()
        hasilTopsis(Jumlah_HAsil_Teratas)
        'Debug.Print(kelajuanProses)
    End Sub

    Private Sub norm_R()
        normalisai_R = tabel_Keputusan.Copy
        Dim total_R_perKolom(normalisai_R.Columns.Count - 1 - skip_IndexColumn) As Double                                                      'tempat simpan akar total kolom
        For j = skip_IndexColumn To normalisai_R.Columns.Count - 1                                                                         'menghitung akar total kolom
            For Each i As DataRow In normalisai_R.Rows
                total_R_perKolom(j - skip_IndexColumn) += i.Item(j) ^ 2
                proses_Num += 1
                kelajuanProses = proses_Num * 100 / proses_MaxNum
            Next
            total_R_perKolom(j - skip_IndexColumn) = Math.Round(Math.Sqrt(total_R_perKolom(j - skip_IndexColumn)), pembulatanbelakangkoma)

        Next
        Dim int = 0
        For j = skip_IndexColumn To normalisai_R.Columns.Count - 1
            For Each i As DataRow In normalisai_R.Rows
                int = total_R_perKolom(j - skip_IndexColumn)
                i.Item(j) = Math.Round(IIf(int = 0 And gunakanNilaiNaN, 0, i.Item(j) / total_R_perKolom(j - skip_IndexColumn)), pembulatanbelakangkoma)
                proses_Num += 1
                kelajuanProses = proses_Num * 100 / proses_MaxNum

            Next
        Next
    End Sub
    Private Sub norm_V()
        Dim tkelajuan = 30
        normalisasi_V = normalisai_R.Copy

        For j = skip_IndexColumn To normalisasi_V.Columns.Count - 1                                                                         '
            For Each i As DataRow In normalisasi_V.Rows
                i.Item(j) = Math.Round(CDbl(i.Item(j) * nilai_Kriteria(j - skip_IndexColumn)), pembulatanbelakangkoma)




                proses_Num += 1
                kelajuanProses = proses_Num * 100 / proses_MaxNum

            Next

        Next
    End Sub
    Private Sub solusiIdeal()
        Dim tkelajuan = 30
        solusi_ideal = normalisasi_V.Clone

        Dim row_max As DataRow = solusi_ideal.NewRow
        Dim row_min As DataRow = solusi_ideal.NewRow

        For i = 1 To skip_IndexColumn
            row_max.Item(i - skip_IndexColumn) = AP
            row_min.Item(i - skip_IndexColumn) = AN
        Next


        For j = skip_IndexColumn To solusi_ideal.Columns.Count - 1

            row_max.Item(j) = normalisasi_V.Rows(0).Item(j)
            row_min.Item(j) = normalisasi_V.Rows(0).Item(j)

            For Each i As DataRow In normalisasi_V.Rows

                Select Case i.Item(j)
                    Case > row_max.Item(j)
                        row_max.Item(j) = i.Item(j)
                    Case < row_min.Item(j)
                        row_min.Item(j) = i.Item(j)
                End Select
                proses_Num += 1
                kelajuanProses = proses_Num * 100 / proses_MaxNum

            Next
        Next
        solusi_ideal.Rows.Add(row_max)
        solusi_ideal.Rows.Add(row_min)
    End Sub
    Private Sub separasi()
        Dim tkelajuan = 5
        For i = 1 To skip_IndexColumn
            M_separasi.Columns.Add(normalisasi_V.Columns(i - skip_IndexColumn).ColumnName)
        Next


        M_separasi.Columns.Add(SP, System.Type.GetType("System.Double"))
        M_separasi.Columns.Add(SN, System.Type.GetType("System.Double"))

        Dim separasi As DataRow

        Dim dp As Double
        Dim dn As Double
        For Each i As DataRow In normalisasi_V.Rows
            dp = 0
            dn = 0                                                                     'mereset nilai setiap putaran / baris
            'Dim sssp = "", sssn = ""
            separasi = M_separasi.NewRow
            For j = skip_IndexColumn To normalisasi_V.Columns.Count - 1
                dp += ((i.Item(j) - solusi_ideal.Rows(0).Item(j)) ^ 2)                     'menjumlahkan nilai sementr dg (V-A+)^2
                dn += ((i.Item(j) - solusi_ideal.Rows(1).Item(j)) ^ 2)                     'menjumlahkan nilai sementr dg (V-A-)^2

                'sssp &= ((i.Item(j) - solusi_ideal.Rows(0).Item(j)) ^ 2) & " =(" & i.Item(j) & " - " & solusi_ideal.Rows(0).Item(j) & ")^2; "
                ' sssn &= ((i.Item(j) - solusi_ideal.Rows(1).Item(j)) ^ 2) & " =(" & i.Item(j) & " - " & solusi_ideal.Rows(1).Item(j) & ")^2; "

                proses_Num += 1
                kelajuanProses = proses_Num * 100 / proses_MaxNum

            Next
            'Debug.Print("POSITIF " & dp & " =======" & sssp)
            'Debug.Print("POSITIF " & Math.Sqrt(dp))
            'Debug.Print("NEGATIF " & dn & " =======" & sssn)
            'Debug.Print("NEGATIF " & Math.Sqrt(dn))

            For j = 1 To skip_IndexColumn
                separasi.Item(j - skip_IndexColumn) = i.Item(j - skip_IndexColumn)
            Next
            separasi.Item(skip_IndexColumn) = Math.Round(IIf(dp < 0 And gunakanNilaiNaN, 0, Math.Sqrt(dp)), pembulatanbelakangkoma)
            separasi.Item(skip_IndexColumn + 1) = Math.Round(IIf(dn < 0 And gunakanNilaiNaN, 0, Math.Sqrt(dn)), pembulatanbelakangkoma)
            M_separasi.Rows.Add(separasi)
        Next
    End Sub
    Private Sub kedekatanRelatif()
        Dim tkelajuan = 5
        For i = 1 To skip_IndexColumn
            Kedekatan_Rltf.Columns.Add(normalisasi_V.Columns(i - skip_IndexColumn).ColumnName)
        Next
        Kedekatan_Rltf.Columns.Add(KR)
        Dim kd As DataRow
        For Each i As DataRow In M_separasi.Rows
            kd = Kedekatan_Rltf.NewRow
            For j = 1 To skip_IndexColumn
                kd.Item(j - skip_IndexColumn) = i.Item(j - skip_IndexColumn)
                proses_Num += 1
                kelajuanProses = proses_Num * 100 / proses_MaxNum

            Next
            kd.Item(skip_IndexColumn) = Math.Round(i.Item(skip_IndexColumn + 1) / (i.Item(skip_IndexColumn + 1) + i.Item(skip_IndexColumn)), pembulatanbelakangkoma)
            Kedekatan_Rltf.Rows.Add(kd)
        Next
    End Sub
    Public Sub hasilTopsis(ByVal alt As Integer)
        Hasil = Kedekatan_Rltf.Copy
        Dim datav As New DataView(Hasil)
        datav.Sort = Kedekatan_Rltf.Columns(1).ColumnName & " DESC"
        Hasil = datav.ToTable

    End Sub
End Class
