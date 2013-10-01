'Packets sent by server to client
Public Enum ServerPackets
    SAlertMessage = 1
    SAcceptedLogin
    SAcceptNewAccount
    SPlayerData
    SClearPlayerData
    SPositionData
    SMessageData
    SEditMap
    ' Make sure SMSG_COUNT is below everything else
    SMSG_COUNT
End Enum

'Packets sent by client to server
Public Enum ClientPackets
    CLoginRequest = 1
    CNewAccountRequest
    CNewCharacterData
    CPositionData
    CMessageData
    CRequestEditMap
    ' Make sure CMSG_COUNT is below everything else
    CMSG_COUNT
End Enum