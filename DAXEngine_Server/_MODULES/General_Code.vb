Module General_Code
    Sub ServerLoop()
        Dim Tick As Integer
        Dim tmrPlayerSave As Integer
        Do While inServer
            Tick = System.Environment.TickCount()
            'Saves players every 5 minutes
            If tmrPlayerSave < Tick Then
                For I = 1 To PlayerHighIndex
                    If Networking.IsPlaying(I) Then Player(I).SavePlayer(I)
                Next
                tmrPlayerSave = System.Environment.TickCount + 300000
            End If
        Loop
    End Sub
End Module
