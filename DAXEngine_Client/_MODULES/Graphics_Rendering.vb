Imports SFML.Graphics
Imports SFML.Window

Module Graphics_Rendering
    Public Sub InitTextures()
        ' Buttons
        countButton = 1
        Do While fileExist(pathButtons & countButton & gfxExt)
            ReDim Preserve texButton(0 To countButton)
            texButton(countButton) = WindowRenderer.cacheTexture(pathButtons & countButton & gfxExt)
            countButton = countButton + 1
        Loop
        countButton = countButton - 1

        ' guis
        countGui = 1
        Do While fileExist(pathGui & countGui & gfxExt)
            ReDim Preserve texGui(0 To countGui)
            texGui(countGui) = WindowRenderer.cacheTexture(pathGui & countGui & gfxExt)
            countGui = countGui + 1
        Loop
        countGui = countGui - 1

        ' sprites
        countSprite = 1
        Do While fileExist(pathSprites & countSprite & gfxExt)
            ReDim Preserve texSprite(0 To countSprite)
            texSprite(countSprite) = WindowRenderer.cacheTexture(pathSprites & countSprite & gfxExt)
            countSprite = countSprite + 1
        Loop
        countSprite = countSprite - 1

        ' tilesets
        countTileset = 1
        Do While fileExist(pathTilesets & countTileset & gfxExt)
            ReDim Preserve texTileset(0 To countTileset)
            texTileset(countTileset) = WindowRenderer.cacheTexture(pathTilesets & countTileset & gfxExt)
            countTileset = countTileset + 1
        Loop
        countTileset = countTileset - 1
    End Sub

    Public Sub renderMenu()
        On Error GoTo errorhandler
        If frmClient.WindowState = FormWindowState.Minimized Then Exit Sub

        WindowRenderer.Window.Clear(New Color(255, 255, 255))

        If faderState < 2 Then
            If Not faderAlpha = 255 Then WindowRenderer.DrawTexture(texGui(2), (ClientConfig.ScreenWidth * 0.5) - (Texture(texGui(2)).Width * 0.5), (ClientConfig.ScreenHeight * 0.5) - (Texture(texGui(2)).Height * 0.5), 0, 0, Texture(texGui(2)).Width, Texture(texGui(2)).Height, Texture(texGui(2)).Width, Texture(texGui(2)).Height)
            DrawFader()
            Call Verdana.Draw("Press 'SPACE' to skip intro", 2, 2, New Color(100, 100, 100, 255))
        Else
            ' Render background
            Call WindowRenderer.DrawTexture(texGui(4), (ClientConfig.ScreenWidth * 0.5) - (Texture(texGui(4)).Width * 0.5), 0, 0, 0, Texture(texGui(4)).Width, Texture(texGui(4)).Height, Texture(texGui(4)).Width, Texture(texGui(4)).Height)
            Call WindowRenderer.DrawTexture(texGui(1), 0, ClientConfig.ScreenHeight - 20, 0, 0, ClientConfig.ScreenWidth, 20, 32, 32, 200, 0, 0, 0)
            Call WindowRenderer.DrawTexture(texGui(3), (ClientConfig.ScreenWidth * 0.5) - (Texture(texGui(3)).Width * 0.5 + 5), +150, 0, 0, Texture(texGui(3)).Width, Texture(texGui(3)).Height, Texture(texGui(3)).Width, Texture(texGui(3)).Height)
            Call WindowRenderer.DrawTexture(texGui(2), (ClientConfig.ScreenWidth * 0.5) - (Texture(texGui(2)).Width * 0.5), 0, 0, 0, Texture(texGui(2)).Width, Texture(texGui(2)).Height, Texture(texGui(2)).Width, Texture(texGui(2)).Height)
            Call Verdana.Draw(Application.ProductName & " v" & Application.ProductVersion, 5, ClientConfig.ScreenHeight - 18, Color.White)
            Call Verdana.Draw("daxgames.net", ClientConfig.ScreenWidth - 5 - Verdana.GetWidth("daxgames.com"), ClientConfig.ScreenHeight - 18, Color.White)
            Call Verdana.Draw("FPS: " & gameFPS, 5, 5, Color.White)

            Select Case curMenu
                Case MenuEnum.Main : DrawMenu()
                Case MenuEnum.Login : DrawLoginWindow()
                Case MenuEnum.Register : DrawRegisterNewAccountWindow()
                Case MenuEnum.CreateCharacter : DrawCreateNewCharacterWindow()
                Case MenuEnum.Credits : DrawCreditsWindow()
            End Select

            DrawFader()
        End If

        WindowRenderer.Window.Display()
        Exit Sub
errorhandler:
        Err.Clear()
        Exit Sub
    End Sub

    Private Sub DrawFader()
        Call WindowRenderer.DrawTexture(texGui(1), 0, 0, 0, 0, ClientConfig.ScreenWidth, ClientConfig.ScreenHeight, 32, 32, faderAlpha, 0, 0, 0)
    End Sub

    Private Sub DrawMenu()
        Call Verdana.Draw("News:", (ClientConfig.ScreenWidth * 0.5) - (Verdana.GetWidth("News:") * 0.5), (ClientConfig.ScreenHeight * 0.5) - 42, Color.Yellow)
        Call Verdana.Draw("Welcome to the DAX Engine", (ClientConfig.ScreenWidth * 0.5) - (Verdana.GetWidth("Welcome to the DAX Engine") * 0.5), (ClientConfig.ScreenHeight * 0.5) - 28, Color.White)
        Call Verdana.Draw("Login with any emal address and password", (ClientConfig.ScreenWidth * 0.5) - (Verdana.GetWidth("Login with any emal address and password") * 0.5), (ClientConfig.ScreenHeight * 0.5) - 14, Color.White)
        Call Verdana.Draw("To enter your very own world that you", (ClientConfig.ScreenWidth * 0.5) - (Verdana.GetWidth("To enter your very own world that you") * 0.5), (ClientConfig.ScreenHeight * 0.5), Color.White)
        Call Verdana.Draw("can create!", (ClientConfig.ScreenWidth * 0.5) - (Verdana.GetWidth("can create!") * 0.5), (ClientConfig.ScreenHeight * 0.5) + 14, Color.White)
        Call renderButton((ClientConfig.ScreenWidth * 0.5 - 80) - (84 * 0.5), (ClientConfig.ScreenHeight * 0.5) - (40 * 0.5) + 85, 84, 40, 1, 2, 1)
        Call renderButton((ClientConfig.ScreenWidth * 0.5) - (84 * 0.5), (ClientConfig.ScreenHeight * 0.5) - (40 * 0.5) + 85, 84, 40, 5, 6, 2)
        Call renderButton((ClientConfig.ScreenWidth * 0.5 + 80) - (84 * 0.5), (ClientConfig.ScreenHeight * 0.5) - (40 * 0.5) + 85, 84, 40, 3, 4, 4)
    End Sub

    Private Sub DrawLoginWindow()
        Call Verdana.Draw("Email:", (ClientConfig.ScreenWidth * 0.5) - 55 - Verdana.GetWidth("Email:"), (ClientConfig.ScreenHeight * 0.5) - 27, Color.White)
        Call WindowRenderer.DrawTexture(texGui(1), (ClientConfig.ScreenWidth * 0.5) - 50, (ClientConfig.ScreenHeight * 0.5) - 30, 0, 0, 175, 20, 32, 32, 200, 0, 0, 0)
        If curTextbox = 0 Then
            Call Verdana.Draw(LoginEmail & chatShowLine, (ClientConfig.ScreenWidth * 0.5) - 47, (ClientConfig.ScreenHeight * 0.5) - 27, Color.White)
        Else
            Call Verdana.Draw(LoginEmail, (ClientConfig.ScreenWidth * 0.5) - 47, (ClientConfig.ScreenHeight * 0.5) - 27, Color.White)
        End If
        Call Verdana.Draw("Password:", (ClientConfig.ScreenWidth * 0.5) - 55 - Verdana.GetWidth("Password:"), (ClientConfig.ScreenHeight * 0.5) + 3, Color.White)
        Call WindowRenderer.DrawTexture(texGui(1), (ClientConfig.ScreenWidth * 0.5) - 50, (ClientConfig.ScreenHeight * 0.5), 0, 0, 175, 20, 32, 32, 200, 0, 0, 0)
        If curTextbox = 1 Then
            Call Verdana.Draw(LoginPassword & chatShowLine, (ClientConfig.ScreenWidth * 0.5) - 47, (ClientConfig.ScreenHeight * 0.5) + 3, Color.White)
        Else
            Call Verdana.Draw(LoginPassword, (ClientConfig.ScreenWidth * 0.5) - 47, (ClientConfig.ScreenHeight * 0.5) + 3, Color.White)
        End If
        Call renderButton((ClientConfig.ScreenWidth * 0.5) - (84 * 0.5), (ClientConfig.ScreenHeight * 0.5) - (40 * 0.5) + 46, 84, 40, 7, 8, 5)
    End Sub

    Private Sub DrawRegisterNewAccountWindow()
        Call Verdana.Draw("Email:", (ClientConfig.ScreenWidth * 0.5) - 55 - Verdana.GetWidth("Email:"), (ClientConfig.ScreenHeight * 0.5) - 27, Color.White)
        Call WindowRenderer.DrawTexture(texGui(1), (ClientConfig.ScreenWidth * 0.5) - 50, (ClientConfig.ScreenHeight * 0.5) - 30, 0, 0, 175, 20, 32, 32, 200, 0, 0, 0)
        If curTextbox = 0 Then
            Call Verdana.Draw(RegisterEmail & chatShowLine, (ClientConfig.ScreenWidth * 0.5) - 47, (ClientConfig.ScreenHeight * 0.5) - 27, Color.White)
        Else
            Call Verdana.Draw(RegisterEmail, (ClientConfig.ScreenWidth * 0.5) - 47, (ClientConfig.ScreenHeight * 0.5) - 27, Color.White)
        End If
        Call Verdana.Draw("Desired Password:", (ClientConfig.ScreenWidth * 0.5) - 55 - Verdana.GetWidth("Desired Password:"), (ClientConfig.ScreenHeight * 0.5) + 3, Color.White)
        Call WindowRenderer.DrawTexture(texGui(1), (ClientConfig.ScreenWidth * 0.5) - 50, (ClientConfig.ScreenHeight * 0.5), 0, 0, 175, 20, 32, 32, 200, 0, 0, 0)
        If curTextbox = 1 Then
            Call Verdana.Draw(RegisterPassword & chatShowLine, (ClientConfig.ScreenWidth * 0.5) - 47, (ClientConfig.ScreenHeight * 0.5) + 3, Color.White)
        Else
            Call Verdana.Draw(RegisterPassword, (ClientConfig.ScreenWidth * 0.5) - 47, (ClientConfig.ScreenHeight * 0.5) + 3, Color.White)
        End If
        Call renderButton((ClientConfig.ScreenWidth * 0.5) - (84 * 0.5), (ClientConfig.ScreenHeight * 0.5) - (40 * 0.5) + 46, 84, 40, 7, 8, 6)
    End Sub

    Private Sub DrawCreateNewCharacterWindow()
        Call Verdana.Draw("Character Name:", (ClientConfig.ScreenWidth * 0.5) - 55 - Verdana.GetWidth("Character Name:"), (ClientConfig.ScreenHeight * 0.5) - 27, Color.White)
        Call WindowRenderer.DrawTexture(texGui(1), (ClientConfig.ScreenWidth * 0.5) - 50, (ClientConfig.ScreenHeight * 0.5) - 30, 0, 0, 175, 20, 32, 32, 200, 0, 0, 0)
        If curTextbox = 0 Then
            Call Verdana.Draw(NewCharacterName & chatShowLine, (ClientConfig.ScreenWidth * 0.5) - 47, (ClientConfig.ScreenHeight * 0.5) - 27, Color.White)
        Else
            Call Verdana.Draw(NewCharacterName, (ClientConfig.ScreenWidth * 0.5) - 47, (ClientConfig.ScreenHeight * 0.5) - 27, Color.White)
        End If
        Call renderButton((ClientConfig.ScreenWidth * 0.5) - (84 * 0.5), (ClientConfig.ScreenHeight * 0.5) - (40 * 0.5) + 60, 84, 40, 7, 8, 7)
    End Sub

    Private Sub DrawCreditsWindow()
        Call Verdana.Draw("DAX Engine Credits:", (ClientConfig.ScreenWidth * 0.5) - (Verdana.GetWidth("DAX Engine Credits:") * 0.5), (ClientConfig.ScreenHeight * 0.5) - 42, Color.Yellow)
        Call Verdana.Draw("Lead Programmer: Tyler Pyskaty", (ClientConfig.ScreenWidth * 0.5) - (Verdana.GetWidth("Lead Programmer: Tyler Pyskaty") * 0.5), (ClientConfig.ScreenHeight * 0.5) - 28, Color.White)
        Call Verdana.Draw("Special Thanks: Thomas 'Deathbeam' Slusny", (ClientConfig.ScreenWidth * 0.5) - (Verdana.GetWidth("Special Thanks: Thomas 'Deathbeam' Slusny") * 0.5), (ClientConfig.ScreenHeight * 0.5) - 14, Color.White)
        Call Verdana.Draw("Music: http://www.nosoapradio.us/", (ClientConfig.ScreenWidth * 0.5) - (Verdana.GetWidth("Music: http://www.nosoapradio.us/") * 0.5), (ClientConfig.ScreenHeight * 0.5), Color.White)
        Call Verdana.Draw("SFML Library: www.sfml-dev.org", (ClientConfig.ScreenWidth * 0.5) - (Verdana.GetWidth("SFML Library: www.sfml-dev.org") * 0.5), (ClientConfig.ScreenHeight * 0.5) + 14, Color.White)
    End Sub

    Private Sub renderButton(ByVal X As Integer, ByVal Y As Integer, ByVal W As Integer, ByVal H As Integer, ByVal Norm As Integer, ByVal Hov As Integer, ByVal ButtonIndex As Integer)

        ' Change the button state
        If mouseX > X And mouseX < X + W And mouseY > Y And mouseY < Y + H Then
            ' Hover state
            Call WindowRenderer.DrawTexture(texButton(Hov), X, Y, 0, 0, W, H, W, H)

            ' When the button is clicked
            If mouseLeftDown > 0 Then
                ' Button sound
                AudioHandler.PlaySound("button.ogg")

                ' Handle what the button does
                Select Case ButtonIndex
                    Case 0 ' Nothing
                    Case 1 : curMenu = MenuEnum.Login
                    Case 2 : curMenu = MenuEnum.Register
                    Case 3 : curMenu = MenuEnum.CreateCharacter
                    Case 4 : curMenu = MenuEnum.Credits
                    Case 5 : SendLoginRequest() 'Accept login button
                    Case 6 : SendRegisterNewAccountRequest() 'Accept register new account button
                    Case 7 : SendCreateNewCharacter() 'Accept create new character button
                    Case Else : MsgBox("Button not assigned. Report this immediately!")
                End Select
                mouseLeftDown = False
            End If
        Else
            ' Normal state
            Call WindowRenderer.DrawTexture(texButton(Norm), X, Y, 0, 0, W, H, W, H)
        End If
    End Sub

    Public Sub renderGame()
        Dim i As Integer
        On Error GoTo errorhandler
        If frmClient.WindowState = FormWindowState.Minimized Then Exit Sub

        WindowRenderer.Window.Clear(New Color(255, 255, 255))

        Call DrawMapTiles()
        Call Verdana.Draw("FPS: " & gameFPS, 5, 5, Color.White)

        For i = 1 To PlayerHighindex
            If Not IsNothing(Player(i)) Then
                DrawPlayer(i)
                DrawPlayerName(i)
            End If
        Next

        DrawChat()

        WindowRenderer.Window.Display()
errorhandler:
        Err.Clear()

        Exit Sub
    End Sub

    Private Sub DrawChat()
        Dim i As Long
        Call WindowRenderer.DrawTexture(texGui(5), (ClientConfig.ScreenWidth * 0.5) - (Texture(texGui(5)).Width * 0.5) - 165, 225, 0, 0, Texture(texGui(5)).Width, Texture(texGui(5)).Height, Texture(texGui(5)).Width, Texture(texGui(5)).Height)

        If inChat Then
            Verdana.Draw(sChat & chatShowLine, 8, ClientConfig.ScreenHeight - 22, Color.White)
        Else
            Verdana.Draw("Press 'ENTER' to start chatting", 8, ClientConfig.ScreenHeight - 22, Color.White)
        End If
        For i = 1 To maxChatLines
            Verdana.Draw(chatbuffer(i), 8, ClientConfig.ScreenHeight - 252 + (15 * (i - 1)), Color.White)
        Next
    End Sub

    Private Sub DrawPlayerName(ByVal Index As Integer)
        Dim textX As Integer, textY As Integer, Text As String, textSize As Integer

        Text = Trim$(Player(Index).CharacterName)
        textSize = Verdana.GetWidth(Text)

        textX = Player(Index).X * picX + Player(Index).XOffset + (picX \ 2) - (textSize / 1.5)
        textY = Player(Index).Y * picY + Player(Index).YOffset - picY

        If Player(Index).Sprite >= 1 Then
            textY = Player(Index).Y * picY + Player(Index).YOffset - (Texture(texSprite(Player(Index).Sprite)).Height / 4) + 16
        End If

        Verdana.Draw(Text, textX, textY, Color.White)
    End Sub

    Private Sub DrawPlayer(ByVal Index As Integer)
        Dim Anim As Byte
        Dim X As Integer
        Dim Y As Integer
        Dim Sprite As Integer, spritetop As Integer
        Dim rec As GeomRec

        Sprite = Player(Index).Sprite

        If Sprite < 1 Then Exit Sub

        ' Reset frame
        If Player(Index).PlayerStep = 3 Then
            Anim = 0
        ElseIf Player(Index).PlayerStep = 1 Then
            Anim = 2
        End If

        Select Case Player(Index).Dir
            Case DirEnum.Up
                If (Player(Index).YOffset > 8) Then Anim = Player(Index).PlayerStep
                spritetop = 3
            Case DirEnum.Down
                If (Player(Index).YOffset < -8) Then Anim = Player(Index).PlayerStep
                spritetop = 0
            Case DirEnum.Left
                If (Player(Index).XOffset > 8) Then Anim = Player(Index).PlayerStep
                spritetop = 1
            Case DirEnum.Right
                If (Player(Index).XOffset < -8) Then Anim = Player(Index).PlayerStep
                spritetop = 2
        End Select

        rec.Top = spritetop * (Texture(texSprite(Sprite)).Height / 4)
        rec.Height = Texture(texSprite(Sprite)).Height / 4
        rec.Left = Anim * (Texture(texSprite(Sprite)).Width / 4)
        rec.Width = Texture(texSprite(Sprite)).Width / 4
        X = Player(Index).X * picX + Player(Index).XOffset - ((Texture(texSprite(Sprite)).Width / 3 - 32) / 2)
        Y = Player(Index).Y * picY + Player(Index).YOffset - ((Texture(texSprite(Sprite)).Height / 4) - 32) - 4

        WindowRenderer.DrawTexture(texSprite(Sprite), X, Y, rec.Left, rec.Top, rec.Width, rec.Height, rec.Width, rec.Height)
    End Sub

    Private Sub DrawMapTiles()
        Dim X As Long, Y As Long
        For X = 0 To maxX
            For Y = 0 To maxY
                Call WindowRenderer.DrawTexture(texTileset(1), X * picX, Y * picY, 0, 1 * picY, picX, picY, picX, picY)
            Next Y
        Next X
    End Sub
End Module