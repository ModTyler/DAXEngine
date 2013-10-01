Module SendData

    Public Sub SendAlertMessage(ByVal Index As Long, ByVal Message As String)
        Dim Buffer As DAXNetworkingComponent.ByteBuffer
        Buffer = New DAXNetworkingComponent.ByteBuffer
        Buffer.WriteLong(ServerPackets.SAlertMessage)
        Buffer.WriteString(Message)
        Networking.SendDataTo(Index, Buffer.ToArray)
        Buffer = Nothing
    End Sub

    Public Sub Packet_SendAcceptLogin(ByVal index As Long)
        Dim Buffer As DAXNetworkingComponent.ByteBuffer
        Buffer = New DAXNetworkingComponent.ByteBuffer
        Buffer.WriteLong(ServerPackets.SAcceptedLogin)
        Buffer.WriteLong(index)
        Networking.SendDataTo(index, Buffer.ToArray())
        Buffer = Nothing
    End Sub

    Public Sub Packet_SendAcceptNewAccount(ByVal index As Long)
        Dim Buffer As DAXNetworkingComponent.ByteBuffer
        Buffer = New DAXNetworkingComponent.ByteBuffer
        Buffer.WriteLong(ServerPackets.SAcceptNewAccount)
        Networking.SendDataTo(index, Buffer.ToArray())
        Buffer = Nothing
    End Sub

    Public Sub Packet_SendPlayersData()
        Dim i As Integer
        For i = 1 To PlayerHighIndex
            Packet_SendPlayerData(i)
        Next
    End Sub

    Public Sub Packet_SendPlayerData(ByVal index As Long)
        Dim Buffer As DAXNetworkingComponent.ByteBuffer
        Buffer = New DAXNetworkingComponent.ByteBuffer
        Buffer.WriteLong(ServerPackets.SPlayerData)
        Buffer.WriteLong(index)
        Buffer.WriteLong(PlayerHighIndex)
        Buffer.WriteString(Player(index).CharacterName)
        Buffer.WriteLong(Player(index).Sprite)
        Buffer.WriteLong(Player(index).X)
        Buffer.WriteLong(Player(index).Y)
        Buffer.WriteLong(Player(index).Dir)
        Networking.SendDataToAll(Buffer.ToArray())
        Buffer = Nothing
    End Sub

    Public Sub Packet_SendClearPlayerData(ByVal index As Long)
        Dim Buffer As DAXNetworkingComponent.ByteBuffer
        Buffer = New DAXNetworkingComponent.ByteBuffer
        Buffer.WriteLong(ServerPackets.SClearPlayerData)
        Buffer.WriteLong(index)
        Buffer.WriteLong(PlayerHighIndex)
        Networking.SendDataToAllBut(index, Buffer.ToArray())
        Buffer = Nothing
    End Sub

    Public Sub Packet_SendPositionData(ByVal index As Long)
        Dim Buffer As DAXNetworkingComponent.ByteBuffer
        Buffer = New DAXNetworkingComponent.ByteBuffer
        Buffer.WriteLong(ServerPackets.SPositionData)
        Buffer.WriteLong(index)
        Buffer.WriteLong(Player(index).Moving)
        Buffer.WriteLong(Player(index).X)
        Buffer.WriteLong(Player(index).Y)
        Buffer.WriteLong(Player(index).Dir)
        Networking.SendDataToAllBut(index, Buffer.ToArray())
        Buffer = Nothing
    End Sub

    Public Sub Packet_SendAlertMessage(ByVal Message As String)
        Dim Buffer As DAXNetworkingComponent.ByteBuffer
        Buffer = New DAXNetworkingComponent.ByteBuffer
        Buffer.WriteLong(ServerPackets.SMessageData)
        Buffer.WriteString(Message)
        Networking.SendDataToAll(Buffer.ToArray())
        Buffer = Nothing
    End Sub

    Public Sub Packet_SendEditMapRequest(ByVal Index As Long)
        Dim Buffer As DAXNetworkingComponent.ByteBuffer
        Buffer = New DAXNetworkingComponent.ByteBuffer
        Buffer.WriteLong(ServerPackets.SEditMap)
        Networking.SendDataTo(Index, Buffer.ToArray())
        Buffer = Nothing
    End Sub
End Module