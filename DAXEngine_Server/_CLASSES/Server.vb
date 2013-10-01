Imports DAXNetworkingComponent.NetworkComponent

Public Class Server
    Shared WithEvents sckListen As DAXNetworkingComponent.NetworkComponent

    Shared Sub Main()
        Dim i As Integer, time1 As Integer, time2 As Integer
        time1 = System.Environment.TickCount
        PlayerHighIndex = 1
        Console.Title = "Loading..."
        Console.WriteLine("Loading networking...")
        sckListen = New DAXNetworkingComponent.NetworkComponent
        sckListen.BufferSize = 8192
        sckListen.LegacySupport = False
        sckListen.LocalPort = PORT
        sckListen.MaxPendingConnections = 1
        sckListen.Protocol = DAXNetworkingComponent.WinsockProtocol.Tcp
        sckListen.RemoteHost = "localhost"
        sckListen.RemotePort = PORT
        Console.WriteLine("Initializing player array...")
        For i = 1 To MAX_PLAYERS
            Networking.Clients(i) = New ClientSocket
            Networking.Clients(i).ServerListener = New DAXNetworkingComponent.NetworkComponent
        Next
        Console.WriteLine("Starting listener...")
        sckListen.Listen()
        Console.Title = "DAX Engine Server <IP " & Networking.GetPublicIP() & " Port " & sckListen.LocalPort & ">"
        time2 = System.Environment.TickCount
        Console.WriteLine("Initialization complete. Server loaded in " & time2 - time1 & "ms.")
        inServer = True
        ServerLoop()
    End Sub

    Private Shared Sub sckListen_ConnectionRequest1(sender As Object, e As DAXNetworkingComponent.WinsockConnectionRequestEventArgs) Handles sckListen.ConnectionRequest
        Dim i As Long
        PlayerHighIndex = PlayerHighIndex + 1
        i = Networking.FindOpenPlayerSlot()

        If i <> 0 Then
            ' we can connect them
            Networking.Clients(i).ServerListener.Accept(e.Client)
            Networking.Clients(i).index = i
            Networking.Clients(i).IP = e.ClientIP
            Call Networking.SocketConnected(i)
        End If
    End Sub
End Class