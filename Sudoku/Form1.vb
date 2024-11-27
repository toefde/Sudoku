Imports System.Net
Imports System.Web.Script.Serialization

Public Class Form1
    Public kastenarr(8, 8) As kasten
    Public arr As New List(Of kasten)
    Public vals As New List(Of Integer)
    Private level = 1
    Private ssize = 9

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim c = 0
        For y = 0 To kastenarr.GetLength(1) - 1
            For x = 0 To kastenarr.GetLength(0) - 1
                kastenarr(x, y) = New kasten(c)
                kastenarr(x, y).Location = New Point(IIf(x >= 3, IIf(x >= 6, x * kastenarr(x, y).Width + 4, x * kastenarr(x, y).Width + 2), x * kastenarr(x, y).Width), IIf(y >= 3, IIf(y >= 6, y * kastenarr(x, y).Height + 4, y * kastenarr(x, y).Height + 2), y * kastenarr(x, y).Height)) 'y * kastenarr(x, y).Height)
                Controls.Add(kastenarr(x, y))
                c += 1
            Next
        Next
        For i = 1 To 9
            vals.Add(9)
            arr.Add(New kasten(i))
            arr.Item(i - 1).setValue(i.ToString, Color.Black)
            arr.Item(i - 1).auswahlKasten = True
            arr.Item(i - 1).Location = New Point(650, i + (i - 1) * arr.Item(i - 1).Height)
            Controls.Add(arr.Item(i - 1))
        Next
    End Sub

    Public Function activeKasten()
        For Each item In kastenarr
            If item.active Then
                Return item
            End If
        Next
    End Function

    Private Async Sub req()
        Dim client As New WebClient
        Dim result As String = Await client.DownloadStringTaskAsync("http://www.cs.utep.edu/cheon/ws/sudoku/new/?size=" & ssize & "&level=" & level)
        Dim j As JavaScriptSerializer = New JavaScriptSerializer()
        Dim json As Object = j.Deserialize(result, GetType(Object))
        For Each k As KeyValuePair(Of String, Object) In json
            If k.Key = "squares" Then
                For Each item In k.Value
                    Dim x = 0, y = 0, v = 0
                    For Each kvp As KeyValuePair(Of String, Object) In item
                        Select Case kvp.Key
                            Case "x"
                                x = kvp.Value
                            Case "y"
                                y = kvp.Value
                            Case "value"
                                v = kvp.Value
                        End Select
                    Next
                    kastenarr(x, y).setValue(v, Color.Blue)
                    vals.Item(v - 1) -= 1
                Next
            End If
        Next
        For i = 0 To arr.Count - 1
            arr.Item(i).setL9(vals.Item(i))
        Next
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        For Each item In kastenarr
            item.value = 0
        Next
        If RadioButton1.Checked Then
            level = 1
        ElseIf RadioButton2.Checked Then
            level = 2
        Else
            level = 3
        End If
        For i = 1 To 9
            vals.Item(i - 1) = 9
            arr.Item(i - 1).setL9(vals.Item(i - 1))
        Next
        req()
        For Each item In kastenarr
            item.clear()
        Next
    End Sub
    Public Sub increase(k As Integer)
        If vals.Item(k) < 9 Then
            vals.Item(k) += 1
            arr.Item(k).setL9(vals.Item(k))
        End If

    End Sub
    Public Sub decrease(k As Integer)
        If vals.Item(k) > 0 Then
            vals.Item(k) -= 1
            arr.Item(k).setL9(vals.Item(k))
        End If
    End Sub
End Class
