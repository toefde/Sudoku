
Public Class kasten
    Public index As Int16
    Public value = 0
    Public active = False
    Private Shared leftClick = True
    Private labelList As New List(Of Label)
    Public auswahlKasten As Boolean = False
    Sub New(ind As Int16)
        InitializeComponent()
        index = ind
        For Each item As Label In Controls
            item.Visible = False
            labelList.Add(item)
        Next
        labelList.Remove(numberLabel)
        labelList = labelList.OrderBy(Function(x As Label) x.Name).ToList
        Dim s = ""
        For Each item In labelList
            s &= item.Name
        Next
        numberLabel.Text = ""
    End Sub

    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles numberLabel.KeyDown, MyBase.KeyDown, Label1.KeyDown, Label2.KeyDown, Label3.KeyDown, Label4.KeyDown, Label5.KeyDown, Label6.KeyDown, Label7.KeyDown, Label8.KeyDown, Label9.KeyDown
        If Not auswahlKasten Then
            If e.KeyData.ToString.Last Like "[1-9]" Then
                If leftClick Then
                    For Each item In labelList
                        item.Visible = False
                    Next
                    Try
                        If Form1.activeKasten.value <> 0 Then
                            Form1.increase(e.KeyData.ToString.Last.ToString - 1)
                        End If
                        Form1.activeKasten.setValue(e.KeyData.ToString.Last.ToString, Color.Black)
                    Catch ex As Exception
                    End Try
                    Form1.decrease(e.KeyData.ToString.Last.ToString - 1)
                    'Form1.arr(Form1.vals.Item(CInt(e.KeyData.ToString.Last.ToString) - 1))
                Else
                    Form1.activeKasten.setGuess(labelList(CInt(e.KeyData.ToString.Last.ToString) - 1))
                End If
            ElseIf e.KeyData = Keys.Delete Or e.KeyData.ToString.Last = "0" Then
                If numberLabel.Visible Then
                    Form1.increase(value - 1)
                    value = 0
                    numberLabel.Text = ""
                    numberLabel.Visible = False
                Else
                    For Each item In labelList
                        item.Visible = False
                    Next
                End If
            ElseIf e.KeyData = Keys.Enter Then
                For Each item In labelList
                    item.Visible = False
                Next
                Try
                    If Form1.activeKasten.value <> 0 Then
                        Form1.increase(value - 1)
                    End If
                    For Each item In Form1.arr
                        If item.BackColor = Color.PaleVioletRed Then
                            Form1.activeKasten.setValue(item.numberLabel.Text, Color.Black)
                        End If
                    Next
                Catch ex As Exception
                End Try
                Form1.decrease(value - 1)
            End If
        End If
    End Sub

    Public Sub setGuess(lbl As Label)
        If lbl.Visible = True Then
            lbl.Visible = False
        Else
            lbl.Visible = True
        End If

    End Sub

    Public Sub setValue(val As String, c As Color)
        value = val
        numberLabel.ForeColor = c
        numberLabel.Text = value
        For Each item In labelList
            item.SendToBack()
            item.Visible = False
        Next

        numberLabel.Visible = True
        coloringKasten()

    End Sub
    Private Sub Button1_Click(sender As Object, e As MouseEventArgs) Handles numberLabel.Click, MyBase.Click, Label1.Click, Label2.Click, Label3.Click, Label4.Click, Label5.Click, Label6.Click, Label7.Click, Label8.Click, Label9.Click

        For Each item In Form1.kastenarr
            If item.value = 0 Then
                item.BackColor = Color.White
            End If
            item.active = False
        Next
        active = True


        coloringKasten()

        If e.Button = MouseButtons.Right Then
            leftClick = False
        Else
            leftClick = True
        End If
    End Sub
    Private Sub coloringKasten()

        If auswahlKasten Then
            For Each item In Form1.arr
                item.BackColor = Color.LightGray
            Next
            For Each item In Form1.arr
                item.BackColor = Color.LightGray
            Next
            BackColor = Color.PaleVioletRed
            For Each item In Form1.kastenarr
                If Not item.auswahlKasten And item.value = value Then
                    item.BackColor = Color.LightGray
                Else
                    item.BackColor = Color.White
                End If
            Next
        Else

            If value = 0 Then
                BackColor = Color.PaleVioletRed
            Else
                For Each item In Form1.arr
                    item.BackColor = Color.LightGray
                Next
                Form1.arr(value - 1).BackColor = Color.PaleVioletRed
                BackColor = Color.LightGray
                For Each item In Form1.kastenarr
                    If Not item.auswahlKasten And item.value = value Then
                        item.BackColor = Color.LightGray
                    Else
                        item.BackColor = Color.White
                    End If
                Next
            End If
        End If



    End Sub
    Public Sub setL9(v As Integer)
        Label9.Text = v.ToString
        Label9.Visible = True
    End Sub

    Public Sub clear()
        numberLabel.Text = ""
        numberLabel.Visible = False
        For Each item In labelList
            item.Visible = False
            item.SendToBack()
        Next
    End Sub
End Class
