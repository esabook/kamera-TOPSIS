﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.42000
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On

Imports System

Namespace My.Resources
    
    'This class was auto-generated by the StronglyTypedResourceBuilder
    'class via a tool like ResGen or Visual Studio.
    'To add or remove a member, edit your .ResX file then rerun ResGen
    'with the /str option, or rebuild your VS project.
    '''<summary>
    '''  A strongly-typed resource class, for looking up localized strings, etc.
    '''</summary>
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(),  _
     Global.Microsoft.VisualBasic.HideModuleNameAttribute()>  _
    Friend Module Resources
        
        Private resourceMan As Global.System.Resources.ResourceManager
        
        Private resourceCulture As Global.System.Globalization.CultureInfo
        
        '''<summary>
        '''  Returns the cached ResourceManager instance used by this class.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend ReadOnly Property ResourceManager() As Global.System.Resources.ResourceManager
            Get
                If Object.ReferenceEquals(resourceMan, Nothing) Then
                    Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("Kamera__TOPSIS_.Resources", GetType(Resources).Assembly)
                    resourceMan = temp
                End If
                Return resourceMan
            End Get
        End Property
        
        '''<summary>
        '''  Overrides the current thread's CurrentUICulture property for all
        '''  resource lookups using this strongly typed resource class.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend Property Culture() As Global.System.Globalization.CultureInfo
            Get
                Return resourceCulture
            End Get
            Set
                resourceCulture = value
            End Set
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Perhatian Konsentrasi.
        '''</summary>
        Friend ReadOnly Property alert() As String
            Get
                Return ResourceManager.GetString("alert", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Anda Yakin?.
        '''</summary>
        Friend ReadOnly Property alertmsg() As String
            Get
                Return ResourceManager.GetString("alertmsg", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to &amp;Batal.
        '''</summary>
        Friend ReadOnly Property batal() As String
            Get
                Return ResourceManager.GetString("batal", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized resource of type System.Drawing.Bitmap.
        '''</summary>
        Friend ReadOnly Property camera() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("camera", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized resource of type System.Drawing.Bitmap.
        '''</summary>
        Friend ReadOnly Property catalog() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("catalog", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized resource of type System.Drawing.Bitmap.
        '''</summary>
        Friend ReadOnly Property criteria() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("criteria", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Deskripsi :
        '''Aplikasi ini adalah hasil skripsi TI S1..
        '''</summary>
        Friend ReadOnly Property deskripsi() As String
            Get
                Return ResourceManager.GetString("deskripsi", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized resource of type System.Drawing.Bitmap.
        '''</summary>
        Friend ReadOnly Property full() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("full", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Gagal menghapus data .
        '''</summary>
        Friend ReadOnly Property gagalHps() As String
            Get
                Return ResourceManager.GetString("gagalHps", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Gagal ketika menyimpan .
        '''</summary>
        Friend ReadOnly Property gagalSimpan() As String
            Get
                Return ResourceManager.GetString("gagalSimpan", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Kesalahan !!!.
        '''</summary>
        Friend ReadOnly Property galat() As String
            Get
                Return ResourceManager.GetString("galat", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized resource of type System.Drawing.Bitmap.
        '''</summary>
        Friend ReadOnly Property hasil() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("hasil", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized resource of type System.Drawing.Bitmap.
        '''</summary>
        Friend ReadOnly Property i() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("i", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Tidak dapat mengakses gambar.
        '''</summary>
        Friend ReadOnly Property imgfailload() As String
            Get
                Return ResourceManager.GetString("imgfailload", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Mode Admin.
        '''</summary>
        Friend ReadOnly Property isAdmin() As String
            Get
                Return ResourceManager.GetString("isAdmin", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Masuk.
        '''</summary>
        Friend ReadOnly Property isNotAdmin() As String
            Get
                Return ResourceManager.GetString("isNotAdmin", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Teknik Informatika - S1.
        '''</summary>
        Friend ReadOnly Property jurusan() As String
            Get
                Return ResourceManager.GetString("jurusan", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Telah Berhasil Masuk kedalam mode admin. Tekan  OK untuk kembali atau Batal untuk membatalkan dan kembali ke jendela sebelumnya..
        '''</summary>
        Friend ReadOnly Property kombinampassF() As String
            Get
                Return ResourceManager.GetString("kombinampassF", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Kombinasi Nama Admin atau Password tidak sesuai. Coba kembali.
        '''</summary>
        Friend ReadOnly Property kombinampassS() As String
            Get
                Return ResourceManager.GetString("kombinampassS", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized resource of type System.Drawing.Bitmap.
        '''</summary>
        Friend ReadOnly Property l() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("l", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized resource of type System.Drawing.Bitmap.
        '''</summary>
        Friend ReadOnly Property logo_c() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("logo_c", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized resource of type System.Drawing.Bitmap.
        '''</summary>
        Friend ReadOnly Property logo_g() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("logo_g", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Muhammad Agus Salim.
        '''</summary>
        Friend ReadOnly Property nama() As String
            Get
                Return ResourceManager.GetString("nama", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to 11 411  000.
        '''</summary>
        Friend ReadOnly Property nim() As String
            Get
                Return ResourceManager.GetString("nim", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized resource of type System.Drawing.Bitmap.
        '''</summary>
        Friend ReadOnly Property NoImage() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("NoImage", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized resource of type System.Drawing.Bitmap.
        '''</summary>
        Friend ReadOnly Property p() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("p", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized resource of type System.Drawing.Bitmap.
        '''</summary>
        Friend ReadOnly Property r() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("r", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized resource of type System.Drawing.Bitmap.
        '''</summary>
        Friend ReadOnly Property subkriteria() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("subkriteria", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to VisualStudio2012Dark.
        '''</summary>
        Friend ReadOnly Property tmdrk() As String
            Get
                Return ResourceManager.GetString("tmdrk", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized resource of type System.Drawing.Bitmap.
        '''</summary>
        Friend ReadOnly Property user_i() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("user_i", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
    End Module
End Namespace
