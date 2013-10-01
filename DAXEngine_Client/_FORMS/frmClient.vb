Public Class frmClient

    Private Sub frmClient_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.Opaque, True)
        Me.Refresh()
    End Sub

    Private Sub frmClient_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
        inMenu = False
        inGame = False
        Verdana = Nothing
        Networking.Dispose()
        WindowRenderer.Dispose()
        End
    End Sub

    Private Sub frmClient_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If inGame Then
            Select Case e.KeyCode
                Case Keys.Return
                    If inChat Then
                        If Len(Trim(sChat)) > 0 Then Packet_SendMessageData(sChat)
                        sChat = vbNullString
                        inChat = False
                    Else
                        inChat = True
                    End If
                Case Keys.Back
                    If inChat And Len(sChat) > 0 Then sChat = Mid(sChat, 1, Len(sChat) - 1)
                Case Keys.Insert
                    Packet_SendRequestEditMap()
            End Select
        End If
        If inMenu Then
            Select Case e.KeyCode
                Case Keys.Escape
                    If curMenu <> MenuEnum.Main Then
                        ' Button sound
                        AudioHandler.PlaySound("button.ogg")
                        curMenu = MenuEnum.Main
                    Else
                        Me.Close()
                    End If
                Case Keys.Tab
                    If curTextbox = 0 Then
                        curTextbox = 1
                    Else
                        curTextbox = 0
                    End If
                Case Keys.Back
                    If curMenu = MenuEnum.Login Then
                        If curTextbox = 0 Then
                            If Len(LoginEmail) > 0 Then LoginEmail = Mid(LoginEmail, 1, Len(LoginEmail) - 1)
                        Else
                            If Len(LoginPassword) > 0 Then LoginPassword = Mid(LoginPassword, 1, Len(LoginPassword) - 1)
                        End If
                    End If
                    If curMenu = MenuEnum.Register Then
                        If curTextbox = 0 Then
                            If Len(RegisterEmail) > 0 Then RegisterEmail = Mid(RegisterEmail, 1, Len(RegisterEmail) - 1)
                        Else
                            If Len(RegisterPassword) > 0 Then RegisterPassword = Mid(RegisterPassword, 1, Len(RegisterPassword) - 1)
                        End If
                    End If
                Case Keys.Space
                    If faderState < 2 Then
                        faderState = 2
                        faderAlpha = 0
                    End If
                Case Keys.Return
                    If curMenu = MenuEnum.Login Then
                        If Networking.ConnectToServer() Then
                            If Len(Trim(LoginEmail)) > 0 And Len(Trim(LoginPassword)) > 0 Then
                                Packet_SendLoginRequest(LoginEmail, LoginPassword)
                            Else
                                MsgBox("Username and password fields can not be empty")
                            End If
                        Else
                            MsgBox("Server is offline")
                        End If
                    End If
                    If curMenu = MenuEnum.Register Then
                        If Networking.ConnectToServer() Then
                            If Len(Trim(RegisterEmail)) > 0 And Len(Trim(RegisterPassword)) > 0 Then
                                Packet_SendRegisterNewAccountRequest(RegisterEmail, RegisterPassword)
                            Else
                                MsgBox("Regestration fields can not be empty")
                            End If
                        Else
                            MsgBox("Server is offline")
                        End If
                    End If
            End Select
        End If
    End Sub

    Private Sub frmClient_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
        If inMenu And curMenu = MenuEnum.Login Then
            If Not GetKeyState(Keys.Back) And Not GetKeyState(Keys.Return) And Not GetKeyState(Keys.Tab) And Not GetKeyState(Keys.Escape) Then
                If curTextbox = 0 Then
                    LoginEmail = LoginEmail & e.KeyChar
                Else
                    LoginPassword = LoginPassword & e.KeyChar
                End If
            End If
        End If
        If inMenu And curMenu = MenuEnum.Register Then
            If Not GetKeyState(Keys.Back) And Not GetKeyState(Keys.Return) And Not GetKeyState(Keys.Tab) And Not GetKeyState(Keys.Escape) Then
                If curTextbox = 0 Then
                    RegisterEmail = RegisterEmail & e.KeyChar
                Else
                    RegisterPassword = RegisterPassword & e.KeyChar
                End If
            End If
        End If
        If inMenu And curMenu = MenuEnum.CreateCharacter Then
            If Not GetKeyState(Keys.Back) And Not GetKeyState(Keys.Return) And Not GetKeyState(Keys.Tab) And Not GetKeyState(Keys.Escape) Then
                If curTextbox = 0 Then
                    NewCharacterName = NewCharacterName & e.KeyChar
                End If
            End If
        End If
        If inGame And inChat Then
            If Not GetKeyState(Keys.Back) And Not GetKeyState(Keys.Return) And Not GetKeyState(Keys.Tab) And Not GetKeyState(Keys.Escape) Then
                sChat = sChat & e.KeyChar
            End If
        End If
    End Sub

    Private Sub frmClient_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        mouseLeftDown = Windows.Forms.MouseButtons.Left
        mouseRightDown = Windows.Forms.MouseButtons.Right
    End Sub

    Private Sub frmClient_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
        mouseX = e.X
        mouseY = e.Y
    End Sub

    Private Sub frmClient_MouseUp(sender As Object, e As MouseEventArgs) Handles Me.MouseUp
        mouseLeftDown = 0
        mouseRightDown = 0
    End Sub
End Class
