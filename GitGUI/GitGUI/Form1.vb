Public Class Form1
    Dim path As String
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.FolderBrowserDialog1.ShowDialog()
        path = Me.FolderBrowserDialog1.SelectedPath.ToString
        If Not My.Computer.FileSystem.DirectoryExists(path & "\.git") Then
            MsgBox("Error: the folder you selected is not a GIT repository.  Please run git init and setup your repo before using this tool - this tool is only a shortcut and should not be used to replace knowing how to use GIT from the command line.")
        Else
            Me.TextBox1.Text = path
        End If
    End Sub

    Private Sub TextBox1_TextChange_Leave(sender As Object, e As EventArgs) Handles TextBox1.Leave
        path = Me.TextBox1.Text
        If Not My.Computer.FileSystem.DirectoryExists(path & "\.git") Then
            MsgBox("Error: the folder you selected is not a GIT repository.  Please run git init and setup your repo before using this tool - this tool is only a shortcut and should not be used to replace knowing how to use GIT from the command line.")
            Me.TextBox1.Focus()
            Me.TextBox1.SelectAll()
        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If My.Computer.FileSystem.FileExists(My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData & "\installed.txt") Then
            loadlastdata()
        Else
            If Not My.Computer.FileSystem.DirectoryExists(My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData) Then
                My.Computer.FileSystem.CreateDirectory(My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData)
            End If
            If My.Computer.FileSystem.FileExists(My.Computer.FileSystem.SpecialDirectories.ProgramFiles & "\Git\bin\git.exe") Then
                Me.TextBox2.Text = (My.Computer.FileSystem.SpecialDirectories.ProgramFiles & "\Git\bin\git.exe")
            Else
                If My.Computer.FileSystem.FileExists(My.Computer.FileSystem.SpecialDirectories.ProgramFiles & " (x86)\Git\bin\git.exe") Then
                    Me.TextBox2.Text = (My.Computer.FileSystem.SpecialDirectories.ProgramFiles & " (x86)\Git\bin\git.exe")
                End If
            End If
            Me.CheckBox1.Checked = True
            Me.TopMost = True
        End If
    End Sub

    Private Sub loadlastdata()

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If Me.CheckBox1.Checked = True Then
            Me.TopMost = True
        Else
            Me.TopMost = False
        End If
    End Sub
End Class
