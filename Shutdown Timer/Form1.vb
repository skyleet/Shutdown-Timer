Imports System.Runtime.InteropServices
Public Class Form1

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Close()
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        Process.Start("http://skyleet.github.io")
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Start()
        Me.Icon = My.Resources.Icon1
        'Me.Text = "Shutdown Timer by SKY"
        'NotifyIcon1.Visible = False
        If ComboBox2.SelectedIndex = -1 Then
            ComboBox2.SelectedIndex = 9
        End If
        If ComboBox1.SelectedIndex = -1 Then
            ComboBox1.SelectedIndex = 0
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label6.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") ' TimeOfDay.ToString("dd MM yyyy hh:mm:ss tt")
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click
        If CheckBox1.Checked Then
            CheckBox1.Checked = False
        Else
            CheckBox1.Checked = True
        End If
    End Sub

    'test
    'Private Sub timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick

    '    Label1.Text = "time is " + Timer2.Interval

    'End Sub
    'test

    ' dragging code
#Region " Move Form "

    ' [ Move Form ]
    '
    ' // By Elektro 

    Public MoveForm As Boolean
    Public MoveForm_MousePosition As Point

    Public Sub MoveForm_MouseDown(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseDown ' Add more handles here (Example: PictureBox1.MouseDown)

        If e.Button = MouseButtons.Left Then
            MoveForm = True
            Me.Cursor = Cursors.SizeAll
            MoveForm_MousePosition = e.Location
        End If

    End Sub

    Public Sub MoveForm_MouseMove(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseMove ' Add more handles here (Example: PictureBox1.MouseMove)

        If MoveForm Then
            Me.Location = Me.Location + (e.Location - MoveForm_MousePosition)
        End If

    End Sub

    Public Sub MoveForm_MouseUp(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseUp ' Add more handles here (Example: PictureBox1.MouseUp)

        If e.Button = MouseButtons.Left Then
            MoveForm = False
            Me.Cursor = Cursors.Default
        End If

    End Sub
    Dim Time As Integer
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click


        Time = (ComboBox2.SelectedIndex + 1) * 60
        If Timer2.Enabled = 0 Then
            Timer2.Start()

            If CheckBox1.Checked Then
                TurnOffLCD()
            End If
            Me.WindowState = FormWindowState.Minimized
        End If

    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Time = Time - 1
        Label1.Text = "System will " + ComboBox1.Text + " after " + Time.ToString() + " Seconds."
        If Time = 0 Then
            If ComboBox1.SelectedIndex = 0 Then
                Process.Start("shutdown", "/s /hybrid /t 00")
            ElseIf ComboBox1.SelectedIndex = 1 Then
                Process.Start("shutdown", "/s /t 00")
            ElseIf ComboBox1.SelectedIndex = 2 Then
                Process.Start("shutdown", "/s /f /t 00")
            ElseIf ComboBox1.SelectedIndex = 3 Then
                Process.Start("shutdown", "/r /t 00")
            ElseIf ComboBox1.SelectedIndex = 4 Then
                Process.Start("shutdown", "/r /f /t 00")
            ElseIf ComboBox1.SelectedIndex = 5 Then
                Process.Start("shutdown")
            End If
            Me.Close()
        End If

    End Sub

#End Region
    'dragging code


    'turn off lcd
    'These are codes which windows can recongnise which tell it to perform certain actions. 
    Private Const MONITOR_OFF As Integer = 2
    Private SC_MONITORPOWER As Integer = &HF170
    Private WM_SYSCOMMAND As Integer = &H112

    'There is no direct function in VB to turn off the LCD screen, so we need to make a few calls to some a system file to do our work for us.

    'The FindWindow function gets the handle to the topmost window whose class name and window name match the specified strings. In this case we will not be using it for this exact purpose.
    <DllImport("user32.dll", SetLastError:=True)>
    Private Shared Function FindWindow(ByVal lpClassName As String, ByVal lpWindowName As String) As IntPtr
    End Function

    'The sendmessage command unlocks (almost) endless possibilities. Think of it as a function which tells the operating system to do something. Each thing has a seperate signature.
    <DllImport("user32.dll")>
    Private Shared Function SendMessage(ByVal hWnd As Integer, ByVal hMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
    End Function


    Public Sub TurnOffLCD()
        'We are using the FindWindow function to call a function which will turn off the LCD screen. It's not exactly the 'best' code to do it, but it does the trick, so why not use it?
        Dim num As Integer = 0
        num = SendMessage(FindWindow(Nothing, Nothing).ToInt32, Me.WM_SYSCOMMAND, Me.SC_MONITORPOWER, 2)
    End Sub


    'turn off lcd


    'minimize2tray
    'This is the event that is fired as the application is closing, whether it
    'be from a close button in the application or from the user
    'clicking the X in the upper right hand corner
    ''Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
    'What we will do here is trap the closing of the application and send the application
    'to the system tray (or so it will appear, we will just make it invisible, re-showing
    'it will be up to you and your notify icon)

    'First minimize the form
    ''Me.WindowState = FormWindowState.Minimized
    'Cancel the closing of the application
    ''e.Cancel = True
    ''NotifyIcon1.Visible = True


    'Now make it invisible (make it look like it went into the system tray)
    ''Me.Visible = False

    ''End Sub

    Private Sub Form1_ReSize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Resize
        If Me.WindowState = FormWindowState.Minimized Then
            'e.Cancel = True
            NotifyIcon1.Visible = True
            'NotifyIcon1.ShowBalloonTip(1, "Shutdown Timer", "Running minimzed", ToolTipIcon.Info)
            Me.Visible = False
        End If
    End Sub

    'minimize2tray

    'restore win
    Private Sub notifyIcon1_DoubleClick(ByVal Sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles NotifyIcon1.DoubleClick
        ' Show the form when the user double clicks on the notify icon.

        ' Set the WindowState to normal if the form is minimized.
        Me.Activate()
        'Me.WindowState = FormWindowState.Minimized
        'If (Me.WindowState = FormWindowState.Minimized) Then
        '    Me.WindowState = FormWindowState.Normal
        'End If
        'Me.WindowState = FormWindowState.Maximized

        Me.Visible = True
        Me.WindowState = FormWindowState.Normal
        'NotifyIcon1.Visible = False
        NotifyIcon1.Visible = False
        ' Activate the form.


    End Sub

    Private Sub OpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem.Click
        Me.Activate()
        Me.Visible = True
        Me.WindowState = FormWindowState.Normal

        NotifyIcon1.Visible = False
    End Sub

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        MessageBox.Show("Feel free to contact me at yadavsunil4796@gmail.com")
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub
    'restore win
End Class

