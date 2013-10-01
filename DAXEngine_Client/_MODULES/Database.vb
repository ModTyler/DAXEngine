Imports System.Xml
Imports System.IO

Module Database
    Public Function fileExist(ByVal filepath As String, Optional ByVal Raw As Boolean = False) As Boolean
        If Raw = True Then
            fileExist = System.IO.File.Exists(filepath)
        Else
            fileExist = System.IO.File.Exists(Application.StartupPath & "/" & filepath)
        End If
    End Function

    Public Sub LoadOptions()
        Dim m_xmld As XmlDocument
        Dim m_nodelist As XmlNodeList
        Dim m_node As XmlNode
        m_xmld = New XmlDocument
        m_xmld.Load(pathContent & "config.xml")

        m_nodelist = m_xmld.SelectNodes("configuration/resolution")
        For Each m_node In m_nodelist
            ClientConfig.ScreenWidth = m_node.Item("width").InnerText
            ClientConfig.ScreenHeight = m_node.Item("height").InnerText
        Next

        m_nodelist = m_xmld.SelectNodes("configuration/audio")
        For Each m_node In m_nodelist
            If m_node.Item("music").InnerText = "on" Then
                ClientConfig.Music = True
            End If
            If m_node.Item("sound").InnerText = "on" Then
                ClientConfig.Sound = True
            End If
            ClientConfig.GameMusic = m_node.Item("game").InnerText
        Next
    End Sub
End Module