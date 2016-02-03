Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.FolderBrowserDialog1.ShowDialog()
        Me.TextBox1.Text = Me.FolderBrowserDialog1.SelectedPath.ToString
    End Sub


End Class
