Public Class ClientSocket
    Public index As Long
    Public IP As String
    Public WithEvents ServerListener As DAXNetworkingComponent.NetworkComponent

    Private Sub ServerListener_DataArrival(sender As Object, e As DAXNetworkingComponent.WinsockDataArrivalEventArgs) Handles ServerListener.DataArrival
        If Networking.IsConnected(index) Then
            Call Networking.IncomingData(index, ServerListener.Get)
        End If
    End Sub

    Private Sub ServerListener_Disconnected(sender As Object, e As EventArgs) Handles ServerListener.Disconnected
        Networking.CloseSocket(index)
    End Sub
End Class