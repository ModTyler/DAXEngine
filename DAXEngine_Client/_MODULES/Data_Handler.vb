Module Data_Handler
    Public Sub HandleDataPackets(ByVal PacketNum As Long, ByRef Data() As Byte)
        If PacketNum = 0 Then Exit Sub
        If PacketNum = ServerPackets.SAlertMessage Then Handle_AlertMessage(Data)
        If PacketNum = ServerPackets.SAcceptedLogin Then Handle_AcceptedLogin(Data)
        If PacketNum = ServerPackets.SAcceptNewAccount Then Handle_AcceptedNewAccount(Data)
        If PacketNum = ServerPackets.SPlayerData Then Handle_PlayerData(Data)
        If PacketNum = ServerPackets.SClearPlayerData Then Handle_ClearPlayerData(Data)
        If PacketNum = ServerPackets.SPositionData Then Handle_PositionData(Data)
        If PacketNum = ServerPackets.SMessageData Then Handle_MessageData(Data)
        If PacketNum = ServerPackets.SEditMap Then Handle_EditMap()
    End Sub

    Private Sub Handle_AlertMessage(ByVal data() As Byte)
        Dim Message As String
        Dim Buffer As DAXNetworkingComponent.ByteBuffer
        Buffer = New DAXNetworkingComponent.ByteBuffer
        Buffer.WriteBytes(data)
        curMenu = MenuEnum.Main
        Message = Buffer.ReadString
        Buffer = Nothing
        curMenu = MenuEnum.Main
        Call MsgBox(Message)
    End Sub

    Private Sub Handle_AcceptedLogin(ByRef Data() As Byte)
        Dim Buffer As DAXNetworkingComponent.ByteBuffer
        Buffer = New DAXNetworkingComponent.ByteBuffer
        Buffer.WriteBytes(Data)
        MyIndex = Buffer.ReadLong
        Buffer = Nothing
        faderState = 3
        faderAlpha = 0
    End Sub

    Private Sub Handle_AcceptedNewAccount(ByRef Data() As Byte)
        Dim Buffer As DAXNetworkingComponent.ByteBuffer
        Buffer = New DAXNetworkingComponent.ByteBuffer
        Buffer.WriteBytes(Data)
        Buffer = Nothing
        curMenu = MenuEnum.CreateCharacter
    End Sub

    Private Sub Handle_PlayerData(ByRef Data() As Byte)
        Dim tempIndex As Integer
        Dim Buffer As DAXNetworkingComponent.ByteBuffer
        Buffer = New DAXNetworkingComponent.ByteBuffer
        Buffer.WriteBytes(Data)
        tempIndex = Buffer.ReadLong
        PlayerHighindex = Buffer.ReadLong
        If IsNothing(Player(tempIndex)) Then Player(tempIndex) = New PlayerStructure
        Player(tempIndex).Load(Buffer.ReadString, Buffer.ReadLong, Buffer.ReadLong, Buffer.ReadLong, Buffer.ReadLong)
        Buffer = Nothing
    End Sub

    Private Sub Handle_ClearPlayerData(ByRef Data() As Byte)
        Dim tempIndex As Integer
        Dim Buffer As DAXNetworkingComponent.ByteBuffer
        Buffer = New DAXNetworkingComponent.ByteBuffer
        Buffer.WriteBytes(Data)
        tempIndex = Buffer.ReadLong
        PlayerHighindex = Buffer.ReadLong
        Player(tempIndex) = Nothing
        Buffer = Nothing
    End Sub

    Private Sub Handle_PositionData(ByRef Data() As Byte)
        Dim tempIndex As Integer
        Dim Buffer As DAXNetworkingComponent.ByteBuffer
        Buffer = New DAXNetworkingComponent.ByteBuffer
        Buffer.WriteBytes(Data)
        tempIndex = Buffer.ReadLong
        Player(tempIndex).Moving = Buffer.ReadLong
        Player(tempIndex).X = Buffer.ReadLong
        Player(tempIndex).Y = Buffer.ReadLong
        Player(tempIndex).Dir = Buffer.ReadLong
        Select Case Player(tempIndex).Dir
            Case DirEnum.Up
                Player(tempIndex).YOffset = picY
            Case DirEnum.Down
                Player(tempIndex).YOffset = picY * -1
            Case DirEnum.Left
                Player(tempIndex).XOffset = picX
            Case DirEnum.Right
                Player(tempIndex).XOffset = picX * -1
        End Select
        Buffer = Nothing
    End Sub

    Private Sub Handle_MessageData(ByRef Data() As Byte)
        Dim Buffer As DAXNetworkingComponent.ByteBuffer, Message As String, I As Integer
        Buffer = New DAXNetworkingComponent.ByteBuffer
        Buffer.WriteBytes(Data)
        Message = Buffer.ReadString
        Buffer = Nothing
        For I = 1 To maxChatLines
            If Len(Trim(chatbuffer(I))) = 0 Then
                chatbuffer(I) = Message
                Exit Sub
            End If
        Next
        For I = 1 To maxChatLines
            If I < maxChatLines Then
                chatbuffer(I) = chatbuffer(I + 1)
            Else
                chatbuffer(I) = Message
            End If
        Next
    End Sub

    Private Sub Handle_EditMap()
        InitializeMapEditor()
    End Sub
End Module