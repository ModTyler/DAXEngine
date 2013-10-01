Public Class PlayerStructure
    Public LoginEmail As String
    Public Password As String
    Public CharacterName As String
    Public Sprite As Integer
    Public Dir As Byte
    Public X As Integer
    Public Y As Integer
    ' non-saved values
    Public XOffset As Integer, YOffset As Integer
    Public Moving As Boolean
    Public PlayerStep As Byte
    Public isPlaying As Boolean
    Public Buffer As DAXNetworkingComponent.ByteBuffer

    Sub SavePlayer(ByVal Index As Long)
        Dim filename As String
        Dim F As Long
        filename = Application.StartupPath & "/Data_Files/Accounts/" & Trim$(Player(Index).LoginEmail) & ".bin"

        F = FreeFile()
        FileOpen(F, filename, OpenMode.Binary, OpenAccess.Write, OpenShare.Default)
        FilePutObject(F, Player(Index).LoginEmail)
        FilePutObject(F, Player(Index).Password)
        FilePutObject(F, Player(Index).CharacterName)
        FilePutObject(F, Player(Index).Sprite)
        FilePutObject(F, Player(Index).Dir)
        FilePutObject(F, Player(Index).x)
        FilePutObject(F, Player(Index).y)
        FileClose(F)
    End Sub

    Sub LoadPlayer(ByVal Index As Long, ByVal LoginEmail As String)
        Dim filename As String
        Dim F As Long
        Call ClearPlayerData(Index)
        filename = Application.StartupPath & "/Data_Files/Accounts/" & Trim(LoginEmail) & ".bin"
        F = FreeFile()
        FileOpen(F, filename, OpenMode.Binary, OpenAccess.Read, OpenShare.Default)
        FileGetObject(F, Player(Index).LoginEmail)
        FileGetObject(F, Player(Index).Password)
        FileGetObject(F, Player(Index).CharacterName)
        FileGetObject(F, Player(Index).Sprite)
        FileGetObject(F, Player(Index).Dir)
        FileGetObject(F, Player(Index).X)
        FileGetObject(F, Player(Index).Y)
        FileClose(F)
    End Sub

    Sub ClearPlayerData(ByVal Index As Long)
        Player(Index).Buffer = New DAXNetworkingComponent.ByteBuffer
        Player(Index).LoginEmail = ""
        Player(Index).Password = ""
        Player(Index).CharacterName = ""
        Player(Index).Sprite = 0
        Player(Index).Dir = 0
        Player(Index).X = 0
        Player(Index).Y = 0
    End Sub
End Class