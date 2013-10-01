Module Game_Logic

    Function IsTryingToMove() As Boolean
        If dirUp Or dirDown Or dirLeft Or dirRight Then
            IsTryingToMove = True
        Else
            IsTryingToMove = False
        End If
    End Function

    Function CanMove() As Boolean
        CanMove = True

        If Player(MyIndex).Moving = True Then
            Return False
        End If

        If inChat Then
            Return False
        End If

        If dirUp Then
            Player(MyIndex).Dir = DirEnum.Up
            If Player(MyIndex).Y = 0 Then Return False
        ElseIf dirDown Then
            Player(MyIndex).Dir = DirEnum.Down
            If Player(MyIndex).Y = maxY Then Return False
        ElseIf dirLeft Then
            Player(MyIndex).Dir = DirEnum.Left
            If Player(MyIndex).X = 0 Then Return False
        ElseIf dirRight Then
            Player(MyIndex).Dir = DirEnum.Right
            If Player(MyIndex).X = maxX Then Return False
        End If

    End Function

    Sub CheckMovement()
        If IsTryingToMove() Then
            If CanMove() Then
                Player(MyIndex).Moving = True

                Select Case Player(MyIndex).Dir
                    Case DirEnum.Up
                        Player(MyIndex).YOffset = picY
                        Player(MyIndex).Y = Player(MyIndex).Y - 1
                    Case DirEnum.Down
                        Player(MyIndex).YOffset = picY * -1
                        Player(MyIndex).Y = Player(MyIndex).Y + 1
                    Case DirEnum.Left
                        Player(MyIndex).XOffset = picX
                        Player(MyIndex).X = Player(MyIndex).X - 1
                    Case DirEnum.Right
                        Player(MyIndex).XOffset = picX * -1
                        Player(MyIndex).X = Player(MyIndex).X + 1
                End Select
                Packet_SendPositionData()
            End If
        End If
    End Sub

    Public Sub CheckInputKeys()

        ' move up
        If GetKeyState(Keys.W) < 0 Then
            dirUp = True
            dirDown = False
            dirLeft = False
            dirRight = False
            Exit Sub
        Else
            dirUp = False
        End If

        'Move Right
        If GetKeyState(Keys.D) < 0 Then
            dirUp = False
            dirDown = False
            dirLeft = False
            dirRight = True
            Exit Sub
        Else
            dirRight = False
        End If

        'Move down
        If GetKeyState(Keys.S) < 0 Then
            dirUp = False
            dirDown = True
            dirLeft = False
            dirRight = False
            Exit Sub
        Else
            dirDown = False
        End If

        'Move left
        If GetKeyState(Keys.A) < 0 Then
            dirUp = False
            dirDown = False
            dirLeft = True
            dirRight = False
            Exit Sub
        Else
            dirLeft = False
        End If
    End Sub

    Public Sub showMenu()
        inGame = False
        AudioHandler.StopMusic()
        AudioHandler.PlayMusic(MenuMusic)
        ' fader
        faderAlpha = 255
        faderState = 0
        faderSpeed = 4
        canFade = True
        inMenu = True
        menuLoop()
    End Sub

    Public Sub showGame()
        inMenu = False
        AudioHandler.StopMusic()
        AudioHandler.PlayMusic(ClientConfig.GameMusic)
        inGame = True
        gameLoop()
    End Sub

    Public Function GetKeyState(ByVal key As Integer) As Boolean
        Dim s As Short
        s = GetAsyncKeyState(key)
        If s = 0 Then Return False
        Return True
    End Function
End Module