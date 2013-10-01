Imports DAXNetworkingComponent

Public Class Networking
    Public Shared WithEvents PlayerSocket As DAXNetworkingComponent.NetworkComponent

    Private Shared Sub PlayerSocket_DataArrival(sender As Object, e As WinsockDataArrivalEventArgs) Handles PlayerSocket.DataArrival
        If IsConnected() Then Call IncomingData(PlayerSocket.Get)
    End Sub

    Private Shared Sub PlayerSocket_Disconnected(sender As Object, e As EventArgs) Handles PlayerSocket.Disconnected
        If inGame Then frmClient.Close()
    End Sub

    Public Shared Sub HandleData(ByRef Data() As Byte)
        Dim Buffer As ByteBuffer
        Buffer = New ByteBuffer
        buffer.WriteBytes(Data)
        HandleDataPackets(Buffer.ReadLong, Buffer.ReadBytes(Buffer.Length))
        Buffer = Nothing
    End Sub

    Public Shared Sub IncomingData(ByVal Data() As Byte)
        Dim Buffer As ByteBuffer
        Dim pLength As Long
        Buffer = New ByteBuffer

        Buffer.WriteBytes(Data)

        If Buffer.Length >= 8 Then pLength = Buffer.ReadLong(False)

        Do While pLength > 0 And pLength <= Buffer.Length - 8
            If pLength <= Buffer.Length - 8 Then
                Buffer.ReadLong()
                HandleData(Buffer.ReadBytes(pLength))
            End If

            pLength = 0
            If Buffer.Length >= 8 Then pLength = Buffer.ReadLong(False)
        Loop
        Buffer = Nothing
    End Sub

    Public Shared Sub Initialize()
        PlayerSocket = New DAXNetworkingComponent.NetworkComponent
        PlayerSocket.RemoteHost = IP
        PlayerSocket.RemotePort = PORT
    End Sub

    Public Shared Sub Dispose()
        PlayerSocket.Close()
        PlayerSocket = Nothing
    End Sub

    Public Shared Function ConnectToServer() As Boolean
        Dim Wait As Long

        If IsConnected() Then
            ConnectToServer = True
            Exit Function
        End If

        Wait = System.Environment.TickCount
        PlayerSocket.Close()
        PlayerSocket.Connect()

        Do While (Not IsConnected()) And (System.Environment.TickCount <= Wait + 1000)
            Application.DoEvents()
        Loop

        ConnectToServer = IsConnected()
    End Function

    Public Shared Function IsConnected() As Boolean
        If PlayerSocket.State = WinsockStates.Connected Then
            IsConnected = True
        Else
            IsConnected = False
        End If
    End Function

    Public Shared Sub SendData(ByRef Data() As Byte)
        Dim Buffer As ByteBuffer

        If IsConnected() Then
            Buffer = New ByteBuffer
            Buffer.WriteLong((UBound(Data) - LBound(Data)) + 1)
            Buffer.WriteBytes(Data)
            PlayerSocket.Send(Buffer.ToArray())
            Buffer = Nothing
        End If
    End Sub
End Class