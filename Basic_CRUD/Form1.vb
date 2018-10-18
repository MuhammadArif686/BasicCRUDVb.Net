Public Class Form1

    Sub KosongkanForm()
        txtKode.Text = ""
        txtNama.Text = ""
        txtNoTelepon.Text = ""
        txtEmail.Text = ""
        txtAlamat.Text = ""
        txtKode.Focus()
    End Sub

    Sub MatikanForm()
        txtKode.Enabled = False
        txtNama.Enabled = False
        cmbKelamin.Enabled = False
        txtNoTelepon.Enabled = False
        txtEmail.Enabled = False
        txtAlamat.Enabled = False
    End Sub

    Sub HidupkanForm()
        txtKode.Enabled = True
        txtNama.Enabled = True
        cmbKelamin.Enabled = True
        txtNoTelepon.Enabled = True
        txtEmail.Enabled = True
        txtAlamat.Enabled = True
    End Sub

    Sub TampilkanData()
        Call KoneksiDB()
        DA = New OleDb.OleDbDataAdapter("select * from kontak", CONN)
        DS = New DataSet
        DA.Fill(DS)
        DGV.DataSource = DS.Tables(0)
        DGV.ReadOnly = True
    End Sub

    Private Sub DGV_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DGV.CellMouseClick
        On Error Resume Next
        txtKode.Text = DGV.Rows(e.RowIndex).Cells(0).Value
        txtNama.Text = DGV.Rows(e.RowIndex).Cells(1).Value
        cmbKelamin.Text = DGV.Rows(e.RowIndex).Cells(2).Value
        txtNoTelepon.Text = DGV.Rows(e.RowIndex).Cells(3).Value
        txtEmail.Text = DGV.Rows(e.RowIndex).Cells(4).Value
        txtAlamat.Text = DGV.Rows(e.RowIndex).Cells(5).Value

        Call HidupkanForm()
        txtKode.Enabled = False
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call MatikanForm()
        Call TampilkanData()
    End Sub

    Private Sub btnKeluar_Click(sender As Object, e As EventArgs) Handles btnKeluar.Click
        Me.Close()
    End Sub

    Private Sub btnTambah_Click(sender As Object, e As EventArgs) Handles btnTambah.Click
        Call HidupkanForm()
        Call KosongkanForm()
    End Sub

    Private Sub btnBatal_Click(sender As Object, e As EventArgs) Handles btnBatal.Click
        Call MatikanForm()
        Call KosongkanForm()
    End Sub

    Private Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        If txtKode.Text = "" Or txtNama.Text = "" Or txtEmail.Text = "" Then
            MsgBox("Data Tidak Lengkap, Silahkan Dilengkapi Terlebih Dahulu")
            Exit Sub
        Else
            Call KoneksiDB()
            CMD = New OleDb.OleDbCommand("select * from kontak where kode = '" & txtKode.Text & "'", CONN)
            DR = CMD.ExecuteReader()
            DR.Read()
            If Not DR.HasRows Then
                Call KoneksiDB()
                Dim simpan As String
                simpan = "insert into kontak values ('" & txtKode.Text & "', '" & txtNama.Text & "', '" & cmbKelamin.Text & "', '" & txtNoTelepon.Text & "', '" & txtEmail.Text & "', '" & txtAlamat.Text & "')"
                CMD = New OleDb.OleDbCommand(simpan, CONN)
                CMD.ExecuteNonQuery()
                MsgBox("Data Berhasil Diinputkan")
            Else
                MsgBox("Kode yang anda masukkan sudah tersedia, silahkan masukkan dengan kode lainnya")
            End If

            Call MatikanForm()
            Call KosongkanForm()
            Call TampilkanData()

        End If
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        If txtKode.Text = "" Or txtNama.Text = "" Or txtNoTelepon.Text = "" Then
            MsgBox("Data belum lengkap, silahkan dilengkapi terlebih dahulu")
            Exit Sub
        Else
            Call KoneksiDB()
            CMD = New OleDb.OleDbCommand("update kontak set nama_teman = '" & txtNama.Text & "', jenis_kelamin = '" & cmbKelamin.Text & "', no_telp = '" & txtNoTelepon.Text & "', email = '" & txtEmail.Text & "', alamat_rumah = '" & txtAlamat.Text & "' where kode = '" & txtKode.Text & "'", CONN)
            CMD.ExecuteNonQuery()
            MsgBox("Data Berhasil Diperbaharui")

        End If

        Call MatikanForm()
        Call KosongkanForm()
        Call TampilkanData()

    End Sub

    Private Sub btnHapus_Click(sender As Object, e As EventArgs) Handles btnHapus.Click
        If txtKode.Text = "" Then
            MsgBox("Tidak ada data yang dihapus")
            Exit Sub
        Else
            Call KoneksiDB()
            CMD = New OleDb.OleDbCommand("delete from kontak where kode = '" & txtKode.Text & "'", CONN)
            CMD.ExecuteNonQuery()
            MsgBox("Data Berhasil Dihapus")
            Call MatikanForm()
            Call KosongkanForm()
            Call TampilkanData()

        End If
    End Sub
End Class
