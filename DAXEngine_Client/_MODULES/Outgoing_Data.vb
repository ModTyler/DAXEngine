Module SendData
    Public Sub Packet_SendLoginRequest(ByVal LoginEmail As String, ByVal Password As String)
        Dim Buffer As DAXNetworkingComponent.ByteBuffer
        Buffer = New DAXNetworkingComponent.ByteBuffer
        Buffer.WriteLong(ClientPackets.CLoginRequest)
        Buffer.WriteString(LoginEmail)
        Buffer.WriteString(Password)
        Networking.SendData(Buffer.ToArray())
        Buffer = Nothing
    End Sub

    Public Sub Packet_SendRegisterNewAccountRequest(ByVal RegisterEmail As String, ByVal RegisterPassword As String)
        Dim Buffer As DAXNetworkingComponent.ByteBuffer
        Buffer = New DAXNetworkingComponent.ByteBuffer
        Buffer.WriteLong(ClientPackets.CNewAccountRequest)
        Buffer.WriteString(RegisterEmail)
        Buffer.WriteString(RegisterPassword)
        Networking.SendData(Buffer.ToArray())
        Buffer = Nothing
    End Sub

    Public Sub Packet_SendNewCharacterData(ByVal CharacterName As String)
        Dim Buffer As DAXNetworkingComponent.ByteBuffer
        Buffer = New DAXNetworkingComponent.ByteBuffer
        Buffer.WriteLong(ClientPackets.CNewCharacterData)
        Buffer.WriteString(CharacterName)
        Networking.SendData(Buffer.ToArray())
        Buffer = Nothing
    End Sub

    Public Sub Packet_SendPositionData()
        Dim Buffer As DAXNetworkingComponent.ByteBuffer
        Buffer = New DAXNetworkingComponent.ByteBuffer
        Buffer.WriteLong(ClientPackets.CPositionData)
        Buffer.WriteLong(Player(MyIndex).Moving)
        Buffer.WriteLong(Player(MyIndex).X)
        Buffer.WriteLong(Player(MyIndex).Y)
        Buffer.WriteLong(Player(MyIndex).Dir)
        Networking.SendData(Buffer.ToArray())
        Buffer = Nothing
    End Sub

    Public Sub Packet_SendMessageData(ByVal Message As String)
        Dim Buffer As DAXNetworkingComponent.ByteBuffer
        Buffer = New DAXNetworkingComponent.ByteBuffer
        Buffer.WriteLong(ClientPackets.CMessageData)
        Buffer.WriteString(Message)
        Networking.SendData(Buffer.ToArray())
        Buffer = Nothing
    End Sub

    Public Sub Packet_SendRequestEditMap()
        Dim Buffer As DAXNetworkingComponent.ByteBuffer
        Buffer = New DAXNetworkingComponent.ByteBuffer
        Buffer.WriteLong(ClientPackets.CRequestEditMap)
        Networking.SendData(Buffer.ToArray())
        Buffer = Nothing
    End Sub
End Module