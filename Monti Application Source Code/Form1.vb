Imports System.Net.Mail
Public Class Form1
    Public Class MyClass1
    End Class

    Dim count As Byte

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        SerialPort1.PortName = TextBox4.Text
        SerialPort1.BaudRate = 9600
        SerialPort1.Open()

        SerialPort2.PortName = TextBox9.Text
        SerialPort2.BaudRate = 4800
        SerialPort2.Open()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'SerialPort1.Write(TextBox1.Text & Chr(10))
        If ListBox1.SelectedIndex <> -1 Then
            ListBox1.Items.RemoveAt(ListBox1.SelectedIndex)
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'SerialPort1.Write(TextBox2.Text & Chr(10))
        'ListBox1.Items.Add(TextBox1.Text + TextBox7.Text + TextBox3.Text)
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'SerialPort1.Write(TextBox3.Text & Chr(10))
    End Sub

    Private Sub SerialPort1_DataReceived(ByVal sender As System.Object, ByVal e As System.IO.Ports.SerialDataReceivedEventArgs) Handles SerialPort1.DataReceived

    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Static Dim temp As String
        Static Dim temp2 As String
        Static Dim index As Integer = 1
        Dim latest As Integer


        If SerialPort1.IsOpen Then
            Do While SerialPort1.BytesToRead > 0
                latest = SerialPort1.ReadChar
                temp = temp & Chr(latest)
                If (latest = 13) Then
                    'ListBox1.Items.Clear()
                    ListBox1.Items(1) = (temp)
                    ' parse bpm
                    Dim words As String() = ListBox1.Items(1).Split(New Char() {" "c})
                    ListBox1.Items(1) = "BPM=""" + words(2) + """"
                    temp = ""
                End If
            Loop
        End If

        If SerialPort2.IsOpen Then
            Do While SerialPort2.BytesToRead > 0
                latest = SerialPort2.ReadChar
                temp2 = temp2 & Chr(latest)
                If (latest = 13) Then
                    'ListBox1.Items.Clear()
                    
                    If Mid(temp2, 3, 5) = "GPRMC" Then
                        ListBox1.Items(0) = (temp2)

                        ' parse GPS

                        'Dim words2 As String() = ListBox1.Items(0).Split(New Char() {","c})

                        'words2(3) = Mid(words2(3), 1, 2) + "." + Mid(words2(3), 3, 2) + Mid(words2(3), 6, 4)
                        'words2(5) = Mid(words2(5), 1, 3) + "." + Mid(words2(5), 4, 2) + Mid(words2(5), 7, 4)

                      
                        'old way    ListBox1.Items(0) = "GPS=""" + words2(3) + "," + words2(5) + """"

                        Dim words3 As String() = ListBox1.Items(0).Split(New Char() {","c})
                        Dim int_temp1 As Double
                        Dim int_temp2 As Double
                        Dim int_temp3 As Double

                        int_temp1 = Val(Mid(words3(3), 1, 2))
                        int_temp2 = Val(Mid(words3(3), 3, 2))
                        int_temp3 = Val(Mid(words3(3), 6, 4)) / 10000
                        If words3(4) = "S" Then
                            words3(3) = ((int_temp1) + ((int_temp2 + int_temp3) / 60))
                            words3(3) = "-" + words3(3)
                        Else
                            words3(3) = ((int_temp1) + ((int_temp2 + int_temp3) / 60))
                        End If
                        'old way    words3(3) = Mid(words3(3), 1, 2) + "." + Mid(words3(3), 3, 2) + Mid(words3(3), 6, 4)

                        int_temp1 = Val(Mid(words3(5), 1, 3))
                        int_temp2 = Val(Mid(words3(5), 4, 2))
                        int_temp3 = Val(Mid(words3(5), 7, 4)) / 10000
                        If words3(6) = "W" Then
                            words3(5) = ((int_temp1) + ((int_temp2 + int_temp3) / 60))
                            words3(5) = "-" + words3(5)
                        Else
                            words3(5) = ((int_temp1) + ((int_temp2 + int_temp3) / 60))
                        End If

                        'old way words3(5) = Mid(words3(5), 1, 3) + "." + Mid(words3(5), 4, 2) + Mid(words3(5), 7, 4)

                        ListBox1.Items(0) = "GPS=""" + words3(3) + "," + words3(5) + """"



                    End If
                    temp2 = ""
                End If
            Loop
        End If



        Do While (ListBox1.Items.Count > 2)
            ListBox1.Items.RemoveAt(0)
        Loop

        If SerialPort1.IsOpen Then SerialPort1.Write("G1" & Chr(13))

        ''''''''''''
        ''''test area
        '''''''''''''''
        ''''''''''''''
        'ListBox1.Items(0) = "$GPRMC,215122.000,A,4220.4042,N,07105.3020,W,0.09,36.88,250411,,*2D"


        'Dim words3 As String() = ListBox1.Items(0).Split(New Char() {","c})
        'Dim int_temp1 As Double
        'Dim int_temp2 As Double
        'Dim int_temp3 As Double

        'int_temp1 = Val(Mid(words3(3), 1, 2))
        'int_temp2 = Val(Mid(words3(3), 3, 2))
        'int_temp3 = Val(Mid(words3(3), 6, 4)) / 10000
        'If words3(4) = "S" Then
        '    words3(3) = ((int_temp1) + ((int_temp2 + int_temp3) / 60))
        '    words3(3) = "-" + words3(3)
        'Else
        '    words3(3) = ((int_temp1) + ((int_temp2 + int_temp3) / 60))
        'End If
        ''old way    words3(3) = Mid(words3(3), 1, 2) + "." + Mid(words3(3), 3, 2) + Mid(words3(3), 6, 4)

        'int_temp1 = Val(Mid(words3(5), 1, 3))
        'int_temp2 = Val(Mid(words3(5), 4, 2))
        'int_temp3 = Val(Mid(words3(5), 7, 4)) / 10000
        'If words3(6) = "W" Then
        '    words3(5) = ((int_temp1) + ((int_temp2 + int_temp3) / 60))
        '    words3(5) = "-" + words3(5)
        'Else
        '    words3(5) = ((int_temp1) + ((int_temp2 + int_temp3) / 60))
        'End If

        ''old way words3(5) = Mid(words3(5), 1, 3) + "." + Mid(words3(5), 4, 2) + Mid(words3(5), 7, 4)

        'ListBox1.Items(0) = "GPS=""" + words3(3) + "," + words3(5) + """"

    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Timer1.Interval = TextBox5.Text
        'Timer1.Enabled = True
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Timer1.Enabled = False
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        SerialPort1.Close()
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        ListBox1.Items.Clear()
    End Sub

    Private Sub TextBox6_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox6.TextChanged

    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        Dim i As Integer

        FileOpen(1, TextBox6.Text, OpenMode.Output)

        i = 0
        Do While i < ListBox1.Items.Count
            PrintLine(1, ListBox1.Items.Item(i))
            i = i + 1
        Loop

        FileClose(1)

    End Sub

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ListBox1.Items.Add("GPS=""42.161709,-71.508412""")
        ListBox1.Items.Add("BPM=""100""")

        Timer2.Interval = 10000
        Timer2.Enabled = False
        
    End Sub

    Private Sub Label3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Label4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub



    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Dim fromAddress As New MailAddress(TextBox1.Text, "senduser")

        Dim toAddress As New MailAddress(TextBox2.Text, "Receiver user")

        Dim msg As New MailMessage(fromAddress, toAddress)



        msg.Body = ListBox1.Items.Item(0) & ControlChars.CrLf
        msg.Body = msg.Body & ListBox1.Items.Item(1) & ControlChars.CrLf
        



        msg.Subject = "Info From Team NEUARTmaxx"





        Dim mailSender As New System.Net.Mail.SmtpClient()

        mailSender.Host = TextBox3.Text
        mailSender.Port = TextBox8.Text
        mailSender.Credentials = New Net.NetworkCredential(TextBox5.Text, TextBox7.Text)

        Try

            mailSender.Send(msg)

            'MsgBox("Message sent")

        Catch ex As Exception

            MsgBox(ex.Message)

        End Try

    End Sub


    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub TextBox7_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox7.TextChanged

    End Sub

    Private Sub TextBox4_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox4.TextChanged

    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        Button1.PerformClick()
    End Sub

    Private Sub Button2_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Timer2.Enabled = True
    End Sub
End Class

