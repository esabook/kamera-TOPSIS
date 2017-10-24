Imports System.Data.OleDb
Imports System.IO

Public Class C_HandleDtBase

    Dim koneksiDB As New OleDbConnection(My.Settings.DataConnectionString)
    Dim dataAdapter As OleDb.OleDbDataAdapter
    Dim DBCommand As OleDbCommand
    Dim DBCBuilder As OleDbCommandBuilder
    Dim dataset As New DataSet
    Dim bindingSource As BindingSource

    Public jumlahNol As Integer = 2

    Public awalan_PKSKrit = "SK"
    Public awalan_PKKrit = "K"
    Public awalan_PKBbt = "B"
    Public awalan_PKHsil = "H"
    Public awalan_PKKmr = "C"
    Public awalan_PKPgj = "P"
    Public awalan_PKSpek = "SP"

    Dim seleksiT = "select * from "
    Public Shared iKArr As String() = {"merek", "harga", "mega pixel", "lcd", "koneksi wifi", "kecepatan shutter speed", "iso", "titik fokus", "built in flash"} 'nama^ kriteria untuk formulir isian spesifikasi kamera
    Public allowedstringonsub() = {">", "<", "-", "/", "=", "1", "2", "3", "4", "5", "6", "7", "8", "9", "0"}

    Public tabel_Kriteria = "Kriteria"
    Public tabel_SubKriteria = "Sub_Kriteria"
    Public tabel_Bobot = "Bobot"
    Public tabel_HR = "Hasil_Rekomendasi"
    Public tabel_Kamera = "Kamera"
    Public tabel_pembeli = "Pembeli"
    Public tabel_Spek = "Spek_Kamera"



    Public Function bacaGambr(ByRef id As String) As Image

        Try

            Return Image.FromFile(pathGambar(id, read.read), True)
        Catch
        End Try

    End Function
    Public Function simpanGambar(ByVal img As Image, id As String) As Boolean

        Dim bt As New Bitmap(img, New Size(300, 300 * img.Height / img.Width))

        bt.Save(pathGambar(id, read.write), Imaging.ImageFormat.Bmp)
        '
        '
        '
        Return simpanGambar
    End Function
    Public Enum read
        read
        write
    End Enum
    Public Function pathGambar(ByVal id As String, read As read) As String
        pathGambar = Nothing

        Directory.CreateDirectory(Application.StartupPath + "\gambar")
        pathGambar = Application.StartupPath + "\gambar\" + id + QueryStr("select nama_tipe from Kamera where id_kamera='" + id + "'") + ".bmp"
        If Not File.Exists(pathGambar) AndAlso read = read.read Then
            Return Application.StartupPath + "\gambar\NoImage.bmp"
        End If
        Return pathGambar
    End Function

    Public Function hapusGambar(ByRef id As String) As Boolean

        If (System.IO.File.Exists(pathGambar(id, read.write))) Then
            Try
                File.Delete(pathGambar(id, read.write))
                hapusGambar = True
            Catch
                hapusGambar = False
            End Try
        End If
        Return hapusGambar
    End Function

    Public Function IDBerdasarNama(ByRef nama As String) As String
        Return QueryStr("select id_kriteria from Kriteria where nama ='" + nama + "' ")
    End Function

    Public Function IDBaruBerdasarTabel(ByRef tabel As String, oneNllableFirstStringOnIdOptionalforCam As String) As String
        Dim cek As Boolean = True, nulll As String = ""
        If oneNllableFirstStringOnIdOptionalforCam IsNot Nothing Then
            nulll = " where instr(id_" & tabel & ", " & oneNllableFirstStringOnIdOptionalforCam & ")<>0"
        End If
        Dim i As Integer = QueryTable(seleksiT & tabel & nulll).Rows.Count + 1
        Dim r As String = Nothing

        While cek
            r = i.ToString
            For index = i.ToString.Length To jumlahNol - 1
                r = "0" & r
            Next
            Select Case tabel
                Case tabel_Bobot
                    r = IIf(oneNllableFirstStringOnIdOptionalforCam Is Nothing, awalan_PKBbt, oneNllableFirstStringOnIdOptionalforCam) & r
                Case tabel_HR
                    r = IIf(oneNllableFirstStringOnIdOptionalforCam Is Nothing, awalan_PKHsil, oneNllableFirstStringOnIdOptionalforCam) & r
                Case tabel_Kamera
                    r = IIf(oneNllableFirstStringOnIdOptionalforCam Is Nothing, awalan_PKKmr, oneNllableFirstStringOnIdOptionalforCam) & r
                Case tabel_Kriteria
                    r = IIf(oneNllableFirstStringOnIdOptionalforCam Is Nothing, awalan_PKKrit, oneNllableFirstStringOnIdOptionalforCam) & r
                Case tabel_pembeli
                    r = IIf(oneNllableFirstStringOnIdOptionalforCam Is Nothing, awalan_PKPgj, oneNllableFirstStringOnIdOptionalforCam) & r
                Case tabel_Spek
                    r = IIf(oneNllableFirstStringOnIdOptionalforCam Is Nothing, awalan_PKSpek, oneNllableFirstStringOnIdOptionalforCam) & r
                Case tabel_SubKriteria
                    r = IIf(oneNllableFirstStringOnIdOptionalforCam Is Nothing, awalan_PKSKrit, oneNllableFirstStringOnIdOptionalforCam) & r
                Case Else

            End Select

            If QueryTable(seleksiT + tabel + " where id_" + tabel.ToLower + "='" + r + "'").Rows.Count > 0 Then
                i += 1
            Else
                cek = False
            End If
        End While


        Return r
    End Function

    Public Function VERTkeHORZ(ByRef Array1DOfFieldName() As String, id As String) As String 'fungsi untuk query mengambil isi tabel vertikal [row] menjadi horizontal [column]
        Dim nilai
        nilai = "select Distinct " & IIf(id IsNot Nothing, "km.id_kamera AS [ID], ", " km.id_kamera&"" - ""&") & "km.nama_tipe as [ALTERNATIF]"
        If IsNothing(Array1DOfFieldName) Then
            Array1DOfFieldName = iKArr
        End If
        For index = 0 To Array1DOfFieldName.Length - 1

            nilai += ", (select sp.nilai_sub_kriteria from spek_kamera as sp where (sp.id_kamera=km.id_kamera) and (sp.id_kriteria='" +
                    IDBerdasarNama(Array1DOfFieldName(index)) + "')) as [" + Array1DOfFieldName(index).ToUpperInvariant + "]"

        Next
        nilai += " from kamera as km inner join spek_kamera as sp on km.id_kamera=sp.id_kamera"
        Return nilai

    End Function
    Public Function VERTkeHORZ(ByRef Array1DOfFieldName() As String) As String
        Return VERTkeHORZ(Array1DOfFieldName, Nothing)
    End Function
    Public Function deteksiSUB(ByRef id_krit As String, ByRef NilaiSUB As String) As String
        Dim numbPdSubKriteria() As String,
            numbNilaiMasukan() As String,
            tempString As String
        deteksiSUB = "0"


        If IsNumeric(NilaiSUB) Or NilaiSUB.Contains("-") And (Not NilaiSUB.Contains("/")) Then
            Dim dt As DataTable = QueryTable("select id_sub_kriteria, nama from sub_kriteria where id_kriteria = '" + id_krit + "'")
            For Each dr As DataRow In dt.Rows
                tempString = dr(1)
                tempString = hapusKarakterTidakDipakai(tempString)
                If dr(1) = NilaiSUB Then Return dr(0)
                numbPdSubKriteria = tempString.ToString.Split("-")
                numbNilaiMasukan = NilaiSUB.Split("-")

                If InStr(tempString, ">=") > 0 Then
                    If IsNumeric(NilaiSUB) AndAlso CDbl(NilaiSUB) >= CDbl(tempString.Substring(2)) Then
                        Return dr(0).ToString
                    Else
                        tempString = tempString.Substring(2)
                        numbPdSubKriteria = tempString.Split("-")
                        If numbPdSubKriteria.Length = 2 AndAlso numbNilaiMasukan.Length = 2 AndAlso CDbl(numbNilaiMasukan(0)) >= CDbl(numbPdSubKriteria(0)) AndAlso CDbl(numbNilaiMasukan(1)) >= CDbl(numbPdSubKriteria(1)) Then
                            Return dr(0).ToString
                        End If
                    End If

                ElseIf InStr(tempString, "<=") > 0 Then
                    If IsNumeric(NilaiSUB) AndAlso CDbl(NilaiSUB) <= CDbl(tempString.Substring(2)) Then
                        Return dr(0).ToString
                    Else
                        tempString = tempString.Substring(2)
                        numbPdSubKriteria = tempString.Split("-")
                        If numbPdSubKriteria.Length = 2 AndAlso numbNilaiMasukan.Length = 2 AndAlso CDbl(numbNilaiMasukan(0)) <= CDbl(numbPdSubKriteria(0)) AndAlso CDbl(numbNilaiMasukan(1)) <= CDbl(numbPdSubKriteria(1)) Then
                            Return dr(0).ToString
                        End If
                    End If
                ElseIf InStr(tempString, ">") > 0 Then
                    If IsNumeric(NilaiSUB) AndAlso CDbl(NilaiSUB) > CDbl(tempString.Substring(1)) Then
                        Return dr(0).ToString
                    Else
                        tempString = tempString.Substring(1)
                        numbPdSubKriteria = tempString.Split("-")
                        If numbPdSubKriteria.Length = 2 AndAlso numbNilaiMasukan.Length = 2 AndAlso CDbl(numbNilaiMasukan(0)) >= CDbl(numbPdSubKriteria(0)) AndAlso CDbl(numbNilaiMasukan(1)) > CDbl(numbPdSubKriteria(1)) Then
                            Return dr(0).ToString
                        End If
                    End If
                ElseIf InStr(tempString, "<") > 0 Then
                    If IsNumeric(NilaiSUB) AndAlso CDbl(NilaiSUB) < CDbl(tempString.Substring(1)) Then
                        Return dr(0).ToString
                    Else
                        tempString = tempString.Substring(1)
                        numbPdSubKriteria = tempString.Split("-")
                        If numbPdSubKriteria.Length = 2 AndAlso numbNilaiMasukan.Length = 2 AndAlso CDbl(numbNilaiMasukan(0)) < CDbl(numbPdSubKriteria(0)) AndAlso CDbl(numbNilaiMasukan(1)) < CDbl(numbPdSubKriteria(1)) Then
                            Return dr(0).ToString
                        End If
                    End If
                ElseIf InStr(tempString, "-") > 1 Then
                    If IsNumeric(NilaiSUB) AndAlso nilaiAntara(CDbl(NilaiSUB), CDbl(numbPdSubKriteria(0)), CDbl(numbPdSubKriteria(1))) Then
                        Return dr(0).ToString
                    ElseIf numbPdSubKriteria.Length = 4 AndAlso
                       nilaiAntara(CDbl(numbNilaiMasukan(0)), CDbl(numbPdSubKriteria(0)), CDbl(numbPdSubKriteria(2))) AndAlso
                        nilaiAntara(CDbl(numbNilaiMasukan(1)), CDbl(numbPdSubKriteria(1)), CDbl(numbPdSubKriteria(3))) Then
                        Return dr(0).ToString
                    End If

                End If



            Next

        Else
            deteksiSUB = QueryStr("select id_sub_kriteria from sub_kriteria where nama='" + NilaiSUB + "' and id_kriteria='" + id_krit + "'")
        End If
        Return deteksiSUB
    End Function

    Public Function hapusKarakterTidakDipakai(tempString As String) As String
        'importan update, must optimized using regex

        For Each c As Char In tempString
            If Not allowedstringonsub.Contains(CStr(c)) And c <> CChar(",") Then
                tempString = tempString.Replace(c, "")
            End If
        Next
        Return tempString.Replace(",", ".")
    End Function

    Public Function nilaiAntara(nilaiVariabel As Double, nilaiAwal As Double, nilaiAkhir As Double) As Boolean
        Dim nilai_awal As Double = nilaiAwal
        Dim nilai_akhir As Double = nilaiAkhir
        If nilaiAwal > nilaiAkhir Then
            nilai_awal = nilaiAkhir
            nilai_akhir = nilaiAwal
        End If
        Select Case nilaiVariabel
            Case nilai_awal To nilai_akhir
                Return True
            Case Else
                Return False
        End Select
    End Function
    Public Function QueryIUD(ByVal QueryString As String) As Boolean
        Debug.Print("IUD " & QueryString)
        Dim r As Integer
        open()
        DBCommand = New OleDbCommand(QueryString, koneksiDB)
        r = DBCommand.ExecuteNonQuery()
        close()
        Return r > 0
    End Function


    Public Function QueryTable(ByRef QueryString As String) As DataTable
        Debug.Print("TB " & QueryString)

        dataAdapter = New OleDbDataAdapter(QueryString, koneksiDB)
        DBCBuilder = New OleDbCommandBuilder(dataAdapter)

        Dim dt As New DataTable
        Try
            dataAdapter.Fill(dt)
        Catch
        End Try
        Return dt
    End Function

    Public Function QueryDS(ByRef QueryString As String) As BindingSource
        Debug.Print("DS " & QueryString)
        Dim record As New BindingSource
        Try
            open()
            dataAdapter = New OleDbDataAdapter(QueryString, koneksiDB)
            dataset = New DataSet
            dataAdapter.Fill(dataset)
            record.DataSource = dataset
            record.DataMember = dataset.Tables(0).ToString
            close()
        Catch
        End Try
        Return record


    End Function

    Public Function QueryStr(ByRef QueryString As String) As String
        Dim s As String = ""
        Try
            DBCommand = New OleDbCommand(QueryString, koneksiDB)
            open()
            Dim dr As OleDbDataReader = DBCommand.ExecuteReader(CommandBehavior.CloseConnection)
            If dr.HasRows Then
                dr.Read()
                s = dr(0)
            End If
        Catch
        End Try
        Return s
    End Function
    Private Function open() As OleDbConnection
        Try
            koneksiDB.Open()
        Catch
        End Try

        Return koneksiDB
    End Function

    Private Function close() As OleDbConnection
        Try
            koneksiDB.Close()
        Catch
        End Try
        Return koneksiDB
    End Function


    Enum mode
        ModeBaru
        ModeEdit
    End Enum
End Class
