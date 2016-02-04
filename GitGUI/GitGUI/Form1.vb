Public Class Form1
    Dim path As String
    Dim askedpath As Boolean = False
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
        If Not My.Computer.FileSystem.FileExists(My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData & "\installed.txt") Then
            If Not My.Computer.FileSystem.DirectoryExists(My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData) Then
                My.Computer.FileSystem.CreateDirectory(My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData)
            End If
            If My.Computer.FileSystem.FileExists(My.Computer.FileSystem.SpecialDirectories.ProgramFiles & "\Git\cmd\git.exe") Then
                Me.TextBox2.Text = (My.Computer.FileSystem.SpecialDirectories.ProgramFiles & "\Git\cmd\git.exe")
            Else
                If My.Computer.FileSystem.FileExists(My.Computer.FileSystem.SpecialDirectories.ProgramFiles & " (x86)\Git\cmd\git.exe") Then
                    Me.TextBox2.Text = (My.Computer.FileSystem.SpecialDirectories.ProgramFiles & " (x86)\Git\cmd\git.exe")
                End If
            End If
            Me.CheckBox1.Checked = True
            Me.TopMost = True
        End If
        loadlastdata()
    End Sub
    Private Sub Form1_Closing(sender As Object, e As EventArgs) Handles MyBase.Closing
        path = (My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData & "\")
        If Not Me.TextBox1.Text = "" Then
            My.Computer.FileSystem.WriteAllText((path & "lastrepo"), Me.TextBox1.Text, False)
        End If
        If Not Me.TextBox2.Text = "" Then
            My.Computer.FileSystem.WriteAllText((path & "gitlocation"), Me.TextBox2.Text, False)
        End If
        If Not Me.TextBox3.Text = "" Then
            My.Computer.FileSystem.WriteAllText((path & "lastbranch"), Me.TextBox3.Text, False)
        End If
        If Not Me.TextBox4.Text = "" Then
            My.Computer.FileSystem.WriteAllText((path & "lastremote"), Me.TextBox4.Text, False)
        End If
        My.Computer.FileSystem.WriteAllText((path & "topmost"), Me.CheckBox1.Checked, False)
        If Not My.Computer.FileSystem.FileExists(path & "installed.txt") Then
            My.Computer.FileSystem.WriteAllText((path & "installed.txt"), "", False)
        End If

        My.Computer.FileSystem.WriteAllText((path & "width"), Me.Width, False)
        My.Computer.FileSystem.WriteAllText((path & "height"), Me.Height, False)
    End Sub

    Private Sub loadlastdata()
        path = (My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData & "\")
        If My.Computer.FileSystem.FileExists(path & "lastrepo") Then
            Me.TextBox1.Text = My.Computer.FileSystem.ReadAllText(path & "lastrepo")
        End If
        If My.Computer.FileSystem.FileExists(path & "gitlocation") Then
            Me.TextBox2.Text = My.Computer.FileSystem.ReadAllText(path & "gitlocation")
        End If
        If My.Computer.FileSystem.FileExists(path & "lastbranch") Then
            Me.TextBox3.Text = My.Computer.FileSystem.ReadAllText(path & "lastbranch")
        End If
        If My.Computer.FileSystem.FileExists(path & "lastremote") Then
            Me.TextBox4.Text = My.Computer.FileSystem.ReadAllText(path & "lastremote")
        End If
        If My.Computer.FileSystem.FileExists(path & "topmost") Then
            Me.CheckBox1.Checked = My.Computer.FileSystem.ReadAllText(path & "topmost")
            Me.TopMost = Me.CheckBox1.Checked
        End If
        If My.Computer.FileSystem.FileExists(path & "width") Then
            Me.Width = My.Computer.FileSystem.ReadAllText(path & "width")
        End If
        If My.Computer.FileSystem.FileExists(path & "height") Then
            Me.Height = My.Computer.FileSystem.ReadAllText(path & "height")
        End If
        Me.CheckBox1.Focus()

        Me.TextBox5.Focus()
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        Me.TopMost = Me.CheckBox1.Checked
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.FolderBrowserDialog1.ShowDialog()
        path = Me.FolderBrowserDialog1.SelectedPath.ToString
        If Not My.Computer.FileSystem.FileExists(path & "\git.exe") Then
            MsgBox("Error: git.exe not found.  Please be sure you select the cmd folder from the git installation.")
        End If
        Me.TextBox2.Text = path
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If Me.TextBox5.Text = "" Then
            MsgBox("Error: a commit message is required.")
        Else
            path = Environment.GetEnvironmentVariable("PATH")

            If ((path.Contains(Me.TextBox2.Text) = False) And (askedpath = False)) Then

                If MsgBox("Notice: Your PATH variable does not have your GIT installation directory in it.  This will cause this program to not operate correctly.  To install the GIT installation directory into your PATH variable, click Yes.  (Select no if the GIT installtion directory that you selected will change or you wish to add it manually.", vbYesNo) = vbYes Then
                    Environment.SetEnvironmentVariable("PATH", (path & ";" & Me.TextBox2.Text))
                End If
                askedpath = True
            End If
            git("add --all")
            git("commit -m """ & Me.TextBox5.Text & "")
            Me.TextBox5.Text = ""
        End If
        Me.TextBox5.Focus()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        path = Environment.GetEnvironmentVariable("PATH")
        If ((path.Contains(Me.TextBox2.Text) = False) And (askedpath = False)) Then
            If MsgBox("Notice: Your PATH variable does not have your GIT installation directory in it.  This will cause this program to not operate correctly.  To install the GIT installation directory into your PATH variable, click Yes.  (Select no if the GIT installtion directory that you selected will change or you wish to add it manually.", vbYesNo) = vbYes Then
                Environment.SetEnvironmentVariable("PATH", (path & ";" & Me.TextBox2.Text))
            End If
            askedpath = True
        End If
        git("push " & Me.TextBox4.Text & " " & Me.TextBox3.Text)
        Me.TextBox5.Focus()
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