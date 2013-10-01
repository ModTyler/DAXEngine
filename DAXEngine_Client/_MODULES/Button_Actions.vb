Module Button_Actions
    Public Sub SendLoginRequest()
        If curMenu = MenuEnum.Login Then
            If Networking.ConnectToServer() Then
                If Len(Trim(LoginEmail)) > 0 And Len(Trim(LoginPassword)) > 0 Then
                    Packet_SendLoginRequest(LoginEmail, LoginPassword)
                Else
                    MsgBox("Username and password fields can not be empty")
                End If
            Else
                curMenu = MenuEnum.Main
                MsgBox("Server is offline")
            End If
        End If
    End Sub

    Public Sub SendRegisterNewAccountRequest()
        If curMenu = MenuEnum.Register Then
            If Networking.ConnectToServer() Then
                If Len(Trim(RegisterEmail)) > 0 And Len(Trim(RegisterPassword)) > 0 Then
                    Packet_SendRegisterNewAccountRequest(RegisterEmail, RegisterPassword)
                Else
                    MsgBox("Username and password fields can not be empty")
                End If
            Else
                curMenu = MenuEnum.Main
                MsgBox("Server is offline")
            End If
        End If
    End Sub

    Public Sub SendCreateNewCharacter()
        If curMenu = MenuEnum.CreateCharacter Then
            If Networking.ConnectToServer() Then
                If Len(Trim(NewCharacterName)) > 0 And Len(Trim(NewCharacterName)) > 0 Then
                    Packet_SendNewCharacterData(NewCharacterName)
                Else
                    MsgBox("Character name cannot be left blank!")
                End If
            Else
                curMenu = MenuEnum.Main
                MsgBox("Server is offline")
            End If
        End If
    End Sub
End Module
