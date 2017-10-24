Public Class C_Katalog
    Dim db As New C_HandleDtBase, pl = 5, pr = 95
    Dim produk As Integer
    Dim awal As String = "<!doc type html><html><head>
<style type=""text/css"">
a:hover {
	color: rgba(255,255,255,1.00);
}
/* Offer text banner*/

/* Main konten of the site */
#konten {
	clear: both;
	overflow: auto;
	padding-top: 29px;
}
/* main konten of the site */
#konten .mainkonten {
	float: left;
	text-align: center;
}
/* Whole page konten */
#mainWrapper {
	width: " & pr & "%;
	padding-left: " & pl & "%;
}
/* produk rows for catalog */
#konten .mainkonten .produkRow {
	overflow: auto;
	color: rgba(146,146,146,1.00);
	text-align: center;
	border-bottom-color: #D9D9D9;
	margin-bottom: 12px;
}
/* Each produk Information in the catalog */
.mainkonten .produkRow .produkInfo {
	padding-left: 1%;
	padding-right: 1%;
	width: 30%;
	position: static;
	float: left;
	margin-right: 3px;
	border-radius: 3px;
	margin-left: 3px;
	padding-bottom: 0px;
	border-right-width: thin;
	border-right-style: solid;
	border-left-width: thin;
	border-left-style: solid;
	background-color: #DCDCDC;
	padding-top: 7px;
	border-bottom-style: solid;
}
/* harga of a produks in catalog */
.produkRow .produkInfo .merek {
	font-family: 'Montserrat', sans-serif;
	color: rgba(146,146,146,1.00);
	font-size: 22px;
	position: relative;
	top: -20px;
}
.produkRow .produkInfo .harga {
	font-family: 'Montserrat', sans-serif;
	color: rgba(255,255,255,1.00);
	font-size: 22px;
	position: relative;
	background-color: #B0171A;
	top: -48px;
	opacity: 0.9;
	margin-bottom: -44px;
	border-bottom-right-radius: 4px;
	border-bottom-left-radius: 4px;
	border-top-left-radius: 0px;
}
/* konten holder for produks in catalog*/
.produkRow .produkInfo .produkkonten {
	position: relative;
	top: -37px;
	font-size: 14px;
	font-family: source-sans-pro, sans-serif;
	font-style: normal;
	font-weight: 200;
	color: rgba(146,146,146,1.00);
	white-space: pre-wrap;
}
/* produk's images in catalog */
.produkInfo div img {
	border-radius: 4px;
	background-color: #B40C0F;
	height: 75%;
	width: 100%;
}

/* Media query for tablets */
@media screen and (max-width:700px) {
/*The sidebar and mainkonten of page */
#konten {
	position: relative;
	top: -22px;
	width: 100%;
	overflow: hidden;
}
/* offer banners konten */
#mainWrapper #offer p {
	font-size: small;
}
/* main konten region of page */
#mainWrapper #konten .mainkonten {
	overflow: hidden;
	width: 95%;
	margin-top: 40px;
}
/* harga of produks in catalog view */
.produkRow .produkInfo .merek {
	font-size: 19px;
}
/* konten holders in catalog view */
.produkRow .produkInfo .produkkonten {
	font-size: 16px;
}
/* Buy buttons in catalog view */
.produkRow .produkInfo .buyButton {
	font-size: 15px;
}
/* Offer- Text banner */
#mainWrapper #offer {
	padding-left: 22%;
}
}

/*media query for small screen devices */
@media screen and (max-width:480px) {
/*Offer - Text Banner */
#mainWrapper #offer {
	padding-left: 0px;
	text-align: center;
}
/* Each produk in catalog view */
.mainkonten .produkRow .produkInfo {
	width: 100%;
	display: block;
	padding-left: 0px;
	padding-right: 0px;
	position: relative;
	left: -2%;
}
/* Main konten which excludes the sidebar */
#mainwrapper #konten .mainkonten {
	margin-top: -81px;
	text-align: center;
	width: 100%;
	padding-left: 0px;
}
}

</style>
<metacharset=""utf-8""><metaname=""viewport""konten=""width=device-width,initial-scale=1""><meta http-equiv=""X-UA-Compatible"" content=""IE=9""/></head><body><div id=""mainWrapper""><div id=""konten""><section class=""mainkonten"">"
    Dim baris = " <div class=""produkRow"">"
    Dim satuan As String
    Dim akrir As String = "</section></div></div></body></html>"
    Dim hasil As String
    Dim tb As DataTable
    Public Event selesai()

    Dim li As New ArrayList,
        lu As New ArrayList,
        fltr() As Object
    Public Function html() As String
        Return hasil
    End Function
    Public Sub filter(textfilter() As Object)
        fltr = textfilter
    End Sub
    Public Sub proses()
        Dim filter As String = db.VERTkeHORZ(Nothing, True)
        If IsNothing(fltr) Then
            tb = db.QueryTable(filter)
            pl = 15
            pr = 85
            produk = 3

        Else
            produk = 3
            tb = db.QueryTable(filter).Clone
            For Each s As String In fltr
                Dim r As DataRow
                r = tb.NewRow
                r.ItemArray = db.QueryTable(filter & " where km.nama_tipe='" + s + "'").Rows.Item(0).ItemArray
                tb.Rows.Add(r)
            Next


            'filter &= " where instr('"
            'For Each s As String In fltr
            '    filter &= s & "-"
            'Next
            'filter &= "', km.nama_tipe)<>0"
        End If

        For index = 0 To tb.Columns.Count - 1
            li.Add(tb.Columns(index).ColumnName)
        Next
f:      For i = 0 To li.Count - 1
            If InStr("ID-HARGA-MEREK-ALTERNATIF", li(i)) > 0 Then
                li.RemoveAt(i)
                GoTo f
            End If
        Next
        Dim jumlahList = 0
        For Each dr As DataRow In tb.Rows

            If jumlahList Mod produk = 0 Then satuan &= baris


            lu.Clear()
            For Each s As String In li
                lu.Add(dr(tb.Columns.IndexOf(s)))
            Next
            satuan &= satuanKamera(db.pathGambar(dr(tb.Columns.IndexOf("ID")), db.read.read), dr(tb.Columns.IndexOf("HARGA")), dr(tb.Columns.IndexOf("MEREK")), dr(tb.Columns.IndexOf("ALTERNATIF")), lu.ToArray)


            jumlahList += 1
            If jumlahList Mod produk = 0 Then satuan &= "</div>" : jumlahList = 0

        Next
        hasil = (awal & satuan & akrir)
        RaiseEvent selesai()
    End Sub

    Private Function satuanKamera(pathGambar As String, harga As String, merek As String, tipe As String, spek() As Object) As String
        satuanKamera = Nothing
        satuanKamera &= "<article class=""produkInfo""><div ><img alt=""Tidak ada Gambar"" src=""" + pathGambar + """ draggable=""false""></div><p class=""harga"">Rp. " + harga + ",-</p><p class=""merek"">" + merek & " " & tipe & "</p><p class=""produkKonten"">"
        For k = 0 To li.Count - 1
            satuanKamera &= "<b >   " + li(k) + ": </b>" + IIf(spek(k).ToString.Length = 0, "-", spek(k) & " " & db.QueryStr("select satuan from kriteria where nama like '%" + li(k) + "%'"))
        Next
        satuanKamera &= "</p></article>"
        Return satuanKamera
    End Function




End Class
