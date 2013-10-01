Imports SFML.Audio

Public Class AudioHandler
    Public Shared SoundPlayer As Sound
    Public Shared MusicPlayer As Music
    Public Shared SoundPlayerBuffer As SoundBuffer

    Public Shared Sub PlayMusic(ByVal filename As String)
        If ClientConfig.Music = False Then Exit Sub

        If Not fileExist(pathMusic & filename) Then Exit Sub

        If MusicPlayer Is Nothing Then
            MusicPlayer = New Music(pathMusic & filename)
            MusicPlayer.Play()
        Else
            MusicPlayer.Stop()
            MusicPlayer.Dispose()
            MusicPlayer = Nothing
            MusicPlayer = New Music(pathMusic & filename)
            MusicPlayer.Play()
        End If
    End Sub

    Public Shared Sub StopMusic()
        If ClientConfig.Music = False Then Exit Sub
        If MusicPlayer Is Nothing Then Exit Sub
        MusicPlayer.Stop()
        MusicPlayer.Dispose()
        MusicPlayer = Nothing
    End Sub

    Public Shared Sub PlaySound(ByVal filename As String)
        If ClientConfig.Sound = False Then Exit Sub
        If Not fileExist(pathSound & filename) Then Exit Sub

        If SoundPlayer Is Nothing Then
            SoundPlayerBuffer = New SoundBuffer(pathSound & filename)
            SoundPlayer = New Sound(SoundPlayerBuffer)
            SoundPlayer.Play()
        Else
            SoundPlayer.Stop()
            SoundPlayer.Dispose()
            SoundPlayerBuffer.Dispose()
            SoundPlayerBuffer = Nothing
            SoundPlayer = Nothing
            SoundPlayerBuffer = New SoundBuffer(pathSound & filename)
            SoundPlayer = New Sound(SoundPlayerBuffer)
            SoundPlayer.Play()
        End If
    End Sub

    Public Shared Sub StopSound()
        If ClientConfig.Sound = False Then Exit Sub
        If SoundPlayer Is Nothing Then Exit Sub
        SoundPlayer.Stop()
        SoundPlayer.Dispose()
        SoundPlayerBuffer.Dispose()
        SoundPlayerBuffer = Nothing
        SoundPlayer = Nothing
    End Sub
End Class