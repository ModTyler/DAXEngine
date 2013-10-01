Module Data_Handler

    Public Sub HandleDataPackets(ByVal PacketNum As Long, ByVal index As Long, ByRef Data() As Byte)
        If PacketNum = 0 Then Exit Sub
        If PacketNum = ClientPackets.CLoginRequest Then Handle_Login(index, Data)
        If PacketNum = ClientPackets.CNewAccountRequest Then Handle_NewAccount(index, Data)
        If PacketNum = ClientPackets.CNewCharacterData Then Handle_NewCharacterData(index, Data)
        If PacketNum = ClientPackets.CPositionData Then Handle_PositionData(index, Data)
        If PacketNum = ClientPackets.CMessageData Then Handle_MessageData(index, Data)
        If PacketNum = ClientPackets.CRequestEditMap Then Handle_EditMapRequest(index, Data)
    End Sub

    Private Sub Handle_Login(ByVal index As Long, ByVal data() As Byte)
        Dim Buffer As DAXNetworkingComponent.ByteBuffer
        Dim LoginEmail As String, Password As String
        Buffer = New DAXNetworkingComponent.ByteBuffer
        Buffer.WriteBytes(data)
        LoginEmail = Buffer.ReadString
        Password = Buffer.ReadString
        Networking.UpdateHighIndex()
        Player(index) = New PlayerStructure

        If Len(Trim$(LoginEmail)) < 3 Or Len(Trim$(Password)) < 3 Then
            Call SendAlertMessage(index, "Your name and password must be at least three characters in length")
            Exit Sub
        End If

        If Not AccountExists(LoginEmail) Then
            Call SendAlertMessage(index, "That account name does not exist.")
            Exit Sub
        End If

        If Not VerifyPassword(LoginEmail, Password) Then
            Call SendAlertMessage(index, "Incorrect password.")
            Exit Sub
        End If

        If MultipleAccountLogin(LoginEmail) Then
            Call SendAlertMessage(index, "Multiple account logins is not authorized.")
            Exit Sub
        End If

        Player(index).LoadPlayer(index, LoginEmail)

        If Len(Trim$(Player(index).CharacterName)) > 0 Then
            Player(index).isPlaying = True
            Packet_SendPlayersData()
            Packet_SendAcceptLogin(index)
            Console.WriteLine("Account: " & LoginEmail & " has began playing Aranoth!")
        Else
            If Not Networking.IsPlaying(index) Then
                Packet_SendAcceptNewAccount(index)
            End If
        End If
        Buffer = Nothing
    End Sub

    Private Sub Handle_NewAccount(ByVal index As Long, ByVal data() As Byte)
        Dim Buffer As DAXNetworkingComponent.ByteBuffer
        Dim RegisterEmail As String, Password As String
        Buffer = New DAXNetworkingComponent.ByteBuffer
        Buffer.WriteBytes(data)
        RegisterEmail = Buffer.ReadString
        Password = Buffer.ReadString
        Buffer = Nothing
        Networking.UpdateHighIndex()
        Player(index) = New PlayerStructure

        If Not AccountExists(RegisterEmail) Then
            Call CreateNewAccount(index, RegisterEmail, Password)
            Packet_SendAcceptNewAccount(index)
            Console.WriteLine("Account: " & RegisterEmail & " has been created.")
        Else
            SendAlertMessage(index, "An account with that email is already registered!")
            Console.WriteLine("{AN ATTEPT TO CREATE AN EXISTING ACCOUNT HAS BEEN MADE FROM: " & Networking.GetPublicIP & "}")
        End If
    End Sub

    Private Sub Handle_NewCharacterData(ByVal index As Long, ByVal data() As Byte)
        Dim Buffer As DAXNetworkingComponent.ByteBuffer
        Dim CharacterName As String
        Buffer = New DAXNetworkingComponent.ByteBuffer
        Buffer.WriteBytes(data)
        CharacterName = Buffer.ReadString
        Player(index).CharacterName = CharacterName
        Player(index).SavePlayer(index)
        HandlePlayeCreatedCharacter(index, CharacterName)
        Buffer = Nothing
    End Sub

    Private Sub Handle_PositionData(ByVal index As Long, ByVal data() As Byte)
        Dim Buffer As DAXNetworkingComponent.ByteBuffer
        Buffer = New DAXNetworkingComponent.ByteBuffer
        Buffer.WriteBytes(data)
        Player(index).Moving = Buffer.ReadLong
        Player(index).X = Buffer.ReadLong
        Player(index).Y = Buffer.ReadLong
        Player(index).Dir = Buffer.ReadLong
        Buffer = Nothing
        Packet_SendPositionData(index)
    End Sub

    Private Sub Handle_MessageData(ByVal index As Long, ByVal data() As Byte)
        Dim Buffer As DAXNetworkingComponent.ByteBuffer, Message As String
        Buffer = New DAXNetworkingComponent.ByteBuffer
        Buffer.WriteBytes(data)
        Message = Buffer.ReadString
        Buffer = Nothing
        Packet_SendAlertMessage(Trim(Player(index).LoginEmail) & ": " & Message)
    End Sub

    Private Sub Handle_EditMapRequest(ByVal index As Long, ByRef Data() As Byte)
        Packet_SendEditMapRequest(index)
    End Sub
End Module