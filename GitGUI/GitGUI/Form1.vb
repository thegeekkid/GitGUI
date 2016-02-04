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

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.FolderBrowserDialog1.ShowDialog()
        path = Me.FolderBrowserDialog1.SelectedPath.ToString
        If Not My.Computer.FileSystem.FileExists(path & "\git.exe") Then
            MsgBox("Error: git.exe not found.  Please be sure you select the bin folder from the git installation.")
        End If
        Me.TextBox2.Text = path
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If Me.TextBox5.Text = "" Then
            MsgBox("Error: a commit message is required.")
        Else
            path = Environment.GetEnvironmentVariable("PATH")
            If Not path.Contains(Me.TextBox2.Text) Then
                If MsgBox("Notice: Your PATH variable does not have your GIT installation directory in it.  This will cause this program to not operate correctly.  To install the GIT installation directory into your PATH variable, click Yes.  (Select no if the GIT installtion directory that you selected will change or you wish to add it manually.", vbYesNo) = vbYes Then
                    Environment.SetEnvironmentVariable("PATH", (path & ";" & Me.TextBox2.Text))
                End If
            End If
            git("add --all")
            git("commit -m """ & Me.TextBox5.Text & "")
        End If

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        path = Environment.GetEnvironmentVariable("PATH")
        If Not path.Contains(Me.TextBox2.Text) Then
            If MsgBox("Notice: Your PATH variable does not have your GIT installation directory in it.  This will cause this program to not operate correctly.  To install the GIT installation directory into your PATH variable, click Yes.  (Select no if the GIT installtion directory that you selected will change or you wish to add it manually.", vbYesNo) = vbYes Then
                Environment.SetEnvironmentVariable("PATH", (path & ";" & Me.TextBox2.Text))
            End If
        End If
        git("push " & Me.TextBox4.Text & " " & Me.TextBox5.Text)
    End Sub
    Private Sub git(arguments As String)
        My.Computer.FileSystem.WriteAllText((My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData & "\temp.bat"), ("@echo off" & vbCrLf & "cd /d " & Me.TextBox1.Text & vbCrLf & "git " & arguments & vbCrLf & "exit"), False)
        Dim proc As Process = New Process
        proc.StartInfo.FileName = (My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData & "\temp.bat")
        proc.Start()
        proc.WaitForExit()
        My.Computer.FileSystem.DeleteFile(My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData & "\temp.bat")
    End Sub
End Class
