﻿Public Class Form1
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
        If Not My.Computer.FileSystem.DirectoryExists(My.Computer.FileSystem.SpecialDirectories.ProgramFiles & "\Semrau Software Consulting\GitGUI") Then
            install()
        Else
            loadlastdata()
        End If
    End Sub
    Private Sub install()

    End Sub
    Private Sub loadlastdata()

    End Sub
End Class
