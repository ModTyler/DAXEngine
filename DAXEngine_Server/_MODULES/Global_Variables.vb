Module Global_Variables
    'Server configuration to the client
    Public Const PORT As String = "43594"

    'Main server Loop control
    Public inServer As Boolean

    'Player data
    Public Player(MAX_PLAYERS) As PlayerStructure
    Public PlayerHighIndex As Integer

    ' General constants [MUST MATCH WITH THE CLIENT]
    Public Const MAX_PLAYERS As Byte = 70
End Module

