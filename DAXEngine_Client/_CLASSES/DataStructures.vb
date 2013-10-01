Public Class PlayerStructure
    Public CharacterName As String
    Public Password As String
    Public Sprite As Integer
    Public X As Integer, Y As Integer
    Public Dir As Byte
    ' non-saved values
    Public XOffset As Long, YOffset As Long
    Public Moving As Boolean
    Public PlayerStep As Byte

    Public Sub Load(NewName As String, NewSprite As Integer, NewX As Integer, NewY As Integer, NewDir As Byte)
        CharacterName = NewName
        Sprite = NewSprite
        X = NewX
        Y = NewY
        Dir = NewDir
    End Sub

    Sub ProcessMovement(ByVal Index As Long)
        Dim MovementSpeed As Long

        Select Case Player(Index).Moving
            Case MOVING_WALKING : MovementSpeed = WALKING_SPEED
            Case MOVING_RUNNING : MovementSpeed = RUNNING_SPEED
            Case Else : Exit Sub
        End Select

        Select Case Dir
            Case DirEnum.Up
                Player(Index).YOffset = Player(Index).YOffset - MovementSpeed
                If Player(Index).YOffset < 0 Then Player(Index).YOffset = 0
            Case DirEnum.Down
                Player(Index).YOffset = Player(Index).YOffset + MovementSpeed
                If Player(Index).YOffset > 0 Then Player(Index).YOffset = 0
            Case DirEnum.Left
                Player(Index).XOffset = Player(Index).XOffset - MovementSpeed
                If Player(Index).XOffset < 0 Then Player(Index).XOffset = 0
            Case DirEnum.Right
                Player(Index).XOffset = Player(Index).XOffset + MovementSpeed
                If Player(Index).XOffset > 0 Then Player(Index).XOffset = 0
        End Select

        ' Check if completed walking over to the next tile
        If Moving = True Then
            If Dir = DirEnum.Right Or Dir = DirEnum.Down Then
                If (XOffset >= 0) And (YOffset >= 0) Then
                    Moving = False
                    If PlayerStep = 1 Then
                        PlayerStep = 3
                    Else
                        PlayerStep = 1
                    End If
                End If
            Else
                If (XOffset <= 0) And (YOffset <= 0) Then
                    Moving = False
                    If PlayerStep = 1 Then
                        PlayerStep = 3
                    Else
                        PlayerStep = 1
                    End If
                End If
            End If
        End If
    End Sub
End Class