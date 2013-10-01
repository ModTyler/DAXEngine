Module Global_Variables

    'Client configuration to the server
    Public Const IP As String = "127.0.0.1"
    Public Const PORT As String = "43594"
    Public Const MenuMusic As String = "MainMenu.ogg"

    Public Declare Function GetAsyncKeyState Lib "user32" (ByVal vkey As Integer) As Short

    ' Graphics extension
    Public Const gfxExt As String = ".png"

    ' File paths
    Public Const pathContent As String = "Data_Files/"
    Public Const pathFonts As String = pathContent & "fonts/"
    Public Const pathSprites As String = pathContent & "graphics/sprites/"
    Public Const pathTilesets As String = pathContent & "graphics/tilesets/"
    Public Const pathGui As String = pathContent & "graphics/gui/"
    Public Const pathButtons As String = pathGui & "buttons/"
    Public Const pathMusic As String = pathContent & "music/"
    Public Const pathSound As String = pathContent & "sounds/"

    ' Hardcoded sound effects
    Public Const buttonClick As String = "button.ogg"

    ' General constants [MUST MATCH WITH THE SERVER]
    Public Const MAX_PLAYERS As Byte = 70

    ' Tile engine
    Public Const picX As Byte = 32
    Public Const picY As Byte = 32

    ' Input
    Public mouseX As Integer
    Public mouseY As Integer
    Public mouseLeftDown As Integer
    Public mouseRightDown As Integer
    Public dirUp As Boolean
    Public dirDown As Boolean
    Public dirLeft As Boolean
    Public dirRight As Boolean

    ' Walking parameters
    Public Const WALKING_SPEED As Byte = 4
    Public Const RUNNING_SPEED As Byte = 6
    Public Const MOVING_WALKING As Byte = 1
    Public Const MOVING_RUNNING As Byte = 2

    ' Screen dimensions
    Public maxX As Integer
    Public maxY As Integer

    ' Used to check if in editor or not and variables for use in editor
    Public InMapEditor As Boolean
    Public EditorTileX As Long
    Public EditorTileY As Long
    Public EditorTileWidth As Long
    Public EditorTileHeight As Long
    Public EditorWarpMap As Long
    Public EditorWarpX As Long
    Public EditorWarpY As Long

    ' Loop control
    Public inMenu As Boolean
    Public inGame As Boolean
    Public elapsedTime As Integer
    Public gameFPS As Integer

    ' Main menu
    Public curMenu As Byte
    Public curTextbox As Byte
    Public LoginEmail As String
    Public LoginPassword As String
    Public RegisterEmail As String
    Public RegisterPassword As String
    Public NewCharacterName As String
    Public chatShowLine As String

    ' Fonts
    Public Verdana As TextRenderer

    ' Players
    Public MyIndex As Integer
    Public Player(MAX_PLAYERS) As PlayerStructure
    Public PlayerHighindex As Integer

    ' fader
    Public canFade As Boolean
    Public faderAlpha As Byte
    Public faderState As Byte
    Public faderSpeed As Byte

    ' chat
    Public Const maxChatLines As Byte = 15
    Public chatbuffer(maxChatLines) As String
    Public sChat As String
    Public inChat As Boolean

    ' Textures
    Public texTileset() As Integer
    Public texSprite() As Integer
    Public texButton() As Integer
    Public texGui() As Integer

    ' Texture counts
    Public countTileset As Integer
    Public countSprite As Integer
    Public countButton As Integer
    Public countGui As Integer

    ' Number of graphic files
    Public numTextures As Integer

    ' Global texture
    Public Texture() As TextureRec
    Public Structure TextureRec
        Dim Tex As SFML.Graphics.Sprite
        Dim Width As Integer
        Dim Height As Integer
        Dim FilePath As String
    End Structure

    Public ClientConfig As ConfigStruct
    Public Structure ConfigStruct
        Dim ScreenWidth As Integer
        Dim ScreenHeight As Integer
        Dim GameMusic As String
        Dim Music As Boolean
        Dim Sound As Boolean
    End Structure

    Public Enum DirEnum
        Up = 0
        Down
        Left
        Right
    End Enum

    Public Enum MenuEnum
        Main = 0
        Login
        Register
        CreateCharacter
        Credits
    End Enum

    Public Structure GeomRec
        Dim Left As Integer
        Dim Top As Integer
        Dim Width As Integer
        Dim Height As Integer
    End Structure
End Module