Imports System.Xml
Imports System.IO

Module Database
    Public Function fileExist(ByVal filepath As String, Optional ByVal Raw As Boolean = False) As Boolean
        If Raw = True Then
            fileExist = System.IO.File.Exists(filepath)
        Else
            fileExist = System.IO.File.Exists(filepath)
        End If
    End Function

    Function AccountExists(ByVal LoginEmail As String) As Boolean
        Dim Filename As String
        filename = Application.StartupPath & "/Data_Files/Accounts/" & Trim(LoginEmail) & ".bin"

        If fileExist(filename) Then
            AccountExists = True
        Else
            AccountExists = False
        End If
    End Function

    Public Sub AddCharacterName(ByVal Index As Long, ByVal CharacterName As String)
        If Len(Trim$(Player(Index).CharacterName)) = 0 Then
            Player(Index).CharacterName = CharacterName
            Call Player(Index).SavePlayer(Index)
            Exit Sub
        End If
    End Sub

    Public Sub CreateNewAccount(ByVal Index As Long, ByVal LoginEmail As String, ByVal Password As String)
        Player(Index).ClearPlayerData(Index)
        Player(Index).LoginEmail = LoginEmail
        Player(Index).Password = Password
        Player(Index).CharacterName = ""
        Player(Index).Sprite = 1
        Player(Index).Dir = 1
        Player(Index).X = 1
        Player(Index).Y = 5
        Player(Index).SavePlayer(Index)
    End Sub

    Function VerifyPassword(ByVal LoginEmail As String, ByVal Password As String) As Boolean
        Dim filename As String
        Dim RightPassword As String
        Dim LoginEmailCheck As String
        Dim nFileNum As Integer
        RightPassword = ""
        LoginEmailCheck = ""
        VerifyPassword = False
        If AccountExists(LoginEmail) Then
            filename = Application.StartupPath & "/Data_Files/Accounts/" & Trim$(LoginEmail) & ".bin"
            nFileNum = FreeFile()
            FileOpen(nFileNum, filename, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared)
            FileGetObject(nFileNum, LoginEmailCheck)
            FileGetObject(nFileNum, RightPassword)
            FileClose()

            If Trim(LoginEmailCheck) <> Trim(LoginEmail) Then
                Exit Function
            End If

            If UCase$(Trim$(Password)) = UCase$(Trim$(RightPassword)) Then
                VerifyPassword = True
            Else
                VerifyPassword = False
            End If
        End If
    End Function

    Function MultipleAccountLogin(ByVal LoginEmail As String) As Boolean
        Dim i As Long
        MultipleAccountLogin = False

        For i = 1 To MAX_PLAYERS
            If LCase$(Trim$(Player(i).LoginEmail)) = LCase$(LoginEmail) Then
                MultipleAccountLogin = True
                Exit Function
            Else
                MultipleAccountLogin = False
                Exit Function
            End If
        Next
    End Function

    Public Sub HandlePlayeCreatedCharacter(ByVal Index As Long, ByVal CharacterName As String)
        Dim LoginEmail As String

        LoginEmail = Player(Index).LoginEmail

        If Not Networking.IsPlaying(Index) Then
            Console.WriteLine("Account: " & LoginEmail & " has began playing Aranoth!")
            Player(Index).LoadPlayer(Index, LoginEmail)
            Player(Index).isPlaying = True
            Packet_SendPlayersData()
            Packet_SendAcceptLogin(Index)
        End If
    End Sub
End Module