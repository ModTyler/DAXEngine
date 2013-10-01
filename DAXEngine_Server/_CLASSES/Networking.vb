Imports DAXNetworkingComponent.NetworkComponent

Public Class Networking
    Public Shared Clients(MAX_PLAYERS) As ClientSocket

    Public Shared Sub UpdateHighIndex()
        Dim i As Long
        PlayerHighIndex = 0
        For i = MAX_PLAYERS To 1 Step -1
            If IsConnected(i) Then
                PlayerHighIndex = i
                Exit Sub
            End If
        Next i
    End Sub

    Public Shared Function IsConnected(ByVal index As Long) As Boolean
        If Clients(index).ServerListener.State = DAXNetworkingComponent.WinsockStates.Connected Then
            Return True
        End If
        Return False
    End Function

    Public Shared Function IsPlaying(ByVal index As Long) As Boolean
        If IsConnected(index) Then
            If Not IsNothing(Player(index)) Then
                If Player(index).isPlaying Then
                    Return True
                End If
            End If
        End If
        Return False
    End Function

    Public Shared Function GetPlayerIP(ByVal index As Long) As String
        Return Clients(index).IP
    End Function

    Public Shared Function FindOpenPlayerSlot() As Long
        Dim i As Long
        For i = 1 To PlayerHighIndex
            If Not IsConnected(i) Then
                Return i
                Exit Function
            End If
        Next i
        Return 0
    End Function

    Public Shared Sub SendDataTo(ByVal index As Long, ByRef Data() As Byte)
        Dim Buffer As DAXNetworkingComponent.ByteBuffer
        Dim TempData() As Byte
        If IsConnected(index) Then
            Buffer = New DAXNetworkingComponent.ByteBuffer
            TempData = Data

            Buffer.WriteLong(UBound(TempData) - LBound(TempData) + 1)
            Buffer.WriteBytes(TempData)

            Clients(index).ServerListener.Send(Buffer.ToArray)
            Buffer = Nothing
        End If
    End Sub

    Public Shared Sub SendDataToAll(ByRef Data() As Byte)
        Dim i As Long

        For i = 1 To PlayerHighIndex
            If IsPlaying(i) Then
                Call SendDataTo(i, Data)
            End If
        Next
    End Sub

    Public Shared Sub SendDataToAllBut(ByVal index As Long, ByRef Data() As Byte)
        Dim i As Long
        For i = 1 To PlayerHighIndex
            If IsPlaying(i) Then
                If i <> index Then
                    Call SendDataTo(i, Data)
                End If
            End If
        Next
    End Sub

    Public Shared Sub HandleData(ByVal index As Long, ByRef Data() As Byte)
        Dim Buffer As DAXNetworkingComponent.ByteBuffer
        Buffer = New DAXNetworkingComponent.ByteBuffer
        Buffer.WriteBytes(Data)
        HandleDataPackets(Buffer.ReadLong, index, Buffer.ReadBytes(Buffer.Length))
        Buffer = Nothing
    End Sub

    Public Shared Sub SocketConnected(ByVal index As Long)
        If index = 0 Then Exit Sub
        Console.WriteLine("Received connection from " & GetPlayerIP(index) & ".")
    End Sub

    Public Shared Sub IncomingData(ByVal index As Long, ByVal Data() As Byte)
        Dim pLength As Long
        Dim Buffer As DAXNetworkingComponent.ByteBuffer
        Buffer = New DAXNetworkingComponent.ByteBuffer
        Buffer.WriteBytes(Data)

        If Buffer.Length >= 8 Then pLength = Buffer.ReadLong(False)

        Do While pLength > 0 And pLength <= Buffer.Length - 8
            If pLength <= Buffer.Length - 8 Then
                Buffer.ReadLong()
                HandleData(index, Buffer.ReadBytes(pLength))
            End If

            pLength = 0
            If Buffer.Length >= 8 Then pLength = Buffer.ReadLong(False)
        Loop
        Buffer = Nothing
    End Sub

    Public Shared Sub CloseSocket(ByVal index As Long)
        If index > 0 Then
            Console.WriteLine("Connection from " & GetPlayerIP(index) & " has been terminated.")
            Clients(index).ServerListener.Close()
            UpdateHighIndex()
            Packet_SendClearPlayerData(index)
        End If
    End Sub

    Public Shared Function GetPublicIP() As String
        Dim direction As String = ""
        Dim request As System.Net.WebRequest
        On Error GoTo errorhandler
        request = System.Net.WebRequest.Create("http://checkip.dyndns.org/")
        Using response As System.Net.WebResponse = request.GetResponse()
            Using stream As New System.IO.StreamReader(response.GetResponseStream())
                direction = stream.ReadToEnd()
            End Using
        End Using

        'Search for the ip in the html
        Dim first As Integer = direction.IndexOf("Address: ") + 9
        Dim last As Integer = direction.LastIndexOf("</body>")
        direction = direction.Substring(first, last - first)

        Return direction
errorhandler:
        Return "localhost"
    End Function
End Class