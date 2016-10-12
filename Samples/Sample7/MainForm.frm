VERSION 5.00
Object = "{7E28D927-9F0F-4CC6-9BD9-E095B1574CCC}#1.0#0"; "ptt.dll"
Object = "{831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.0#0"; "MSCOMCTL.OCX"
Begin VB.Form MainForm 
   BorderStyle     =   3  'Fixed Dialog
   Caption         =   "Sample7 -- VB6"
   ClientHeight    =   7350
   ClientLeft      =   150
   ClientTop       =   780
   ClientWidth     =   7545
   BeginProperty Font 
      Name            =   "Tahoma"
      Size            =   8.25
      Charset         =   204
      Weight          =   400
      Underline       =   0   'False
      Italic          =   0   'False
      Strikethrough   =   0   'False
   EndProperty
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   7350
   ScaleWidth      =   7545
   StartUpPosition =   3  'Windows Default
   Begin PttLibCtl.Ptt meshControl 
      Height          =   4095
      Left            =   3840
      OleObjectBlob   =   "MainForm.frx":0000
      TabIndex        =   15
      Top             =   120
      Width           =   3615
   End
   Begin VB.CommandButton btnPlay 
      Caption         =   "Play"
      Height          =   375
      Left            =   6120
      TabIndex        =   6
      Top             =   6480
      Width           =   1335
   End
   Begin VB.CommandButton btnDeleteAll 
      Caption         =   "Delete all"
      Height          =   375
      Left            =   6120
      TabIndex        =   14
      Top             =   6000
      Width           =   1335
   End
   Begin VB.CommandButton btnDelete 
      Caption         =   "Delete"
      Height          =   375
      Left            =   6120
      TabIndex        =   13
      Top             =   5520
      Width           =   1335
   End
   Begin VB.CommandButton btnUnread 
      Caption         =   "Mark as unread"
      Height          =   375
      Left            =   6120
      TabIndex        =   12
      Top             =   5040
      Width           =   1335
   End
   Begin VB.CommandButton btnRead 
      Caption         =   "Mark as read"
      Height          =   375
      Left            =   6120
      TabIndex        =   11
      Top             =   4560
      Width           =   1335
   End
   Begin MSComctlLib.ImageList imageListHistory 
      Left            =   1320
      Top             =   1680
      _ExtentX        =   1005
      _ExtentY        =   1005
      BackColor       =   -2147483643
      ImageWidth      =   16
      ImageHeight     =   16
      MaskColor       =   16711935
      _Version        =   393216
      BeginProperty Images {2C247F25-8591-11D1-B16A-00C0F0283628} 
         NumListImages   =   2
         BeginProperty ListImage1 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "MainForm.frx":0024
            Key             =   ""
         EndProperty
         BeginProperty ListImage2 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "MainForm.frx":0136
            Key             =   ""
         EndProperty
      EndProperty
   End
   Begin MSComctlLib.ListView listHistory 
      Height          =   2415
      Left            =   120
      TabIndex        =   8
      Top             =   4560
      Width           =   5895
      _ExtentX        =   10398
      _ExtentY        =   4260
      View            =   3
      LabelEdit       =   1
      MultiSelect     =   -1  'True
      LabelWrap       =   -1  'True
      HideSelection   =   0   'False
      HideColumnHeaders=   -1  'True
      FullRowSelect   =   -1  'True
      _Version        =   393217
      ForeColor       =   -2147483640
      BackColor       =   -2147483643
      Appearance      =   1
      NumItems        =   0
   End
   Begin MSComctlLib.ImageList imageListContacts 
      Left            =   600
      Top             =   1680
      _ExtentX        =   1005
      _ExtentY        =   1005
      BackColor       =   -2147483643
      ImageWidth      =   16
      ImageHeight     =   16
      MaskColor       =   16711935
      _Version        =   393216
      BeginProperty Images {2C247F25-8591-11D1-B16A-00C0F0283628} 
         NumListImages   =   8
         BeginProperty ListImage1 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "MainForm.frx":0248
            Key             =   ""
         EndProperty
         BeginProperty ListImage2 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "MainForm.frx":079A
            Key             =   ""
         EndProperty
         BeginProperty ListImage3 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "MainForm.frx":0CEC
            Key             =   ""
         EndProperty
         BeginProperty ListImage4 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "MainForm.frx":123E
            Key             =   ""
         EndProperty
         BeginProperty ListImage5 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "MainForm.frx":1790
            Key             =   ""
         EndProperty
         BeginProperty ListImage6 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "MainForm.frx":1CE2
            Key             =   ""
         EndProperty
         BeginProperty ListImage7 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "MainForm.frx":2234
            Key             =   ""
         EndProperty
         BeginProperty ListImage8 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "MainForm.frx":2786
            Key             =   ""
         EndProperty
      EndProperty
   End
   Begin VB.ComboBox cbUsername 
      Height          =   315
      ItemData        =   "MainForm.frx":2CD8
      Left            =   120
      List            =   "MainForm.frx":2CDA
      TabIndex        =   5
      Text            =   "test7"
      Top             =   360
      Width           =   3615
   End
   Begin VB.Timer timerExit 
      Enabled         =   0   'False
      Interval        =   100
      Left            =   120
      Top             =   1680
   End
   Begin VB.CommandButton btnPTT 
      Caption         =   "PTT"
      BeginProperty Font 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   204
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Left            =   120
      TabIndex        =   4
      Top             =   3840
      Width           =   3615
   End
   Begin VB.CommandButton btnSignIn 
      Caption         =   "Sign In"
      Height          =   375
      Left            =   2400
      TabIndex        =   3
      Top             =   1680
      Width           =   1335
   End
   Begin VB.TextBox tbPassword 
      Height          =   315
      IMEMode         =   3  'DISABLE
      Left            =   120
      PasswordChar    =   "*"
      TabIndex        =   1
      Text            =   "test"
      Top             =   1200
      Width           =   3615
   End
   Begin MSComctlLib.ListView listContacts 
      Height          =   3615
      Left            =   120
      TabIndex        =   7
      Top             =   120
      Width           =   3615
      _ExtentX        =   6376
      _ExtentY        =   6376
      View            =   3
      LabelEdit       =   1
      MultiSelect     =   -1  'True
      LabelWrap       =   -1  'True
      HideSelection   =   0   'False
      HideColumnHeaders=   -1  'True
      FullRowSelect   =   -1  'True
      _Version        =   393217
      ForeColor       =   -2147483640
      BackColor       =   -2147483643
      Appearance      =   1
      NumItems        =   0
   End
   Begin MSComctlLib.StatusBar statusBar 
      Align           =   2  'Align Bottom
      Height          =   285
      Left            =   0
      TabIndex        =   10
      Top             =   7065
      Width           =   7545
      _ExtentX        =   13309
      _ExtentY        =   503
      Style           =   1
      _Version        =   393216
      BeginProperty Panels {8E3867A5-8586-11D1-B16A-00C0F0283628} 
         NumPanels       =   1
         BeginProperty Panel1 {8E3867AB-8586-11D1-B16A-00C0F0283628} 
         EndProperty
      EndProperty
   End
   Begin VB.Label labelUsername 
      Caption         =   "Username:"
      Height          =   255
      Left            =   120
      TabIndex        =   9
      Top             =   120
      Width           =   2535
   End
   Begin VB.Label labelPassword 
      Caption         =   "Password:"
      Height          =   255
      Left            =   120
      TabIndex        =   2
      Top             =   960
      Width           =   2535
   End
   Begin VB.Label labelHistory 
      Caption         =   "History:"
      Height          =   255
      Left            =   120
      TabIndex        =   0
      Top             =   4320
      Width           =   2535
   End
   Begin VB.Menu File 
      Caption         =   "File"
      Begin VB.Menu SignIn 
         Caption         =   "Sign in"
      End
      Begin VB.Menu SignOut 
         Caption         =   "Sign out"
      End
      Begin VB.Menu Exit 
         Caption         =   "Exit"
      End
   End
   Begin VB.Menu Tools 
      Caption         =   "Tools"
      Begin VB.Menu ChangePassword 
         Caption         =   "Change password"
      End
      Begin VB.Menu Options 
         Caption         =   "Options..."
      End
   End
   Begin VB.Menu Help 
      Caption         =   "Help"
      Begin VB.Menu About 
         Caption         =   "About"
      End
      Begin VB.Menu SendFeedback 
         Caption         =   "Send feedback..."
      End
   End
End
Attribute VB_Name = "MainForm"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Private bExiting As Boolean
Private contactIds() As String
Private historyIds() As String
Private history As IHistory
Private ignoreHistoryChanges As Boolean

Private Sub About_Click()
    meshControl.ShowAboutDialog hWnd
End Sub

Private Sub btnDelete_Click()
    Dim arrayOfIds() As String
    Dim message As IMessage
    Dim i As Integer
    
    If Not (history Is Nothing) Then
        arrayOfIds = collectSelectedItems(listHistory, historyIds)
        
        If Not (UBound(arrayOfIds) = LBound(arrayOfIds) And "" = arrayOfIds(0)) Then
            ignoreHistoryChanges = True
            For i = 0 To UBound(arrayOfIds)
                Set message = history.Find(arrayOfIds(i))
                If Not (message Is Nothing) Then
                    message.Delete
                End If
            Next i
            ignoreHistoryChanges = False
            updateHistory
        End If
    End If
End Sub

Private Sub btnDeleteAll_Click()
    If Not (history Is Nothing) Then
        history.DeleteAll
    End If
End Sub

Private Sub btnPlay_Click()
    Dim arrayOfIds() As String
    Dim message As IMessage
    Dim i As Integer
    Dim player As IAudioPlayer
    
    If Not (history Is Nothing) Then
        arrayOfIds = collectSelectedItems(listHistory, historyIds)
        
        If (UBound(arrayOfIds) = LBound(arrayOfIds) And Not ("" = arrayOfIds(0))) Then
            Set message = history.Find(arrayOfIds(i))
            Set player = meshControl.AudioPlayer
            player.message = message
            player.Play
        End If
    End If
End Sub

Private Sub btnRead_Click()
    Dim arrayOfIds() As String
    Dim message As IMessage
    Dim i As Integer
    
    If Not (history Is Nothing) Then
        arrayOfIds = collectSelectedItems(listHistory, historyIds)
        
        If Not (UBound(arrayOfIds) = LBound(arrayOfIds) And "" = arrayOfIds(0)) Then
            ignoreHistoryChanges = True
            For i = 0 To UBound(arrayOfIds)
                Set message = history.Find(arrayOfIds(i))
                If Not (message Is Nothing) Then
                    message.Read = True
                End If
            Next i
            ignoreHistoryChanges = False
            updateHistory
        End If
    End If
End Sub

Private Sub btnUnread_Click()
    Dim arrayOfIds() As String
    Dim message As IMessage
    Dim i As Integer
    
    If Not (history Is Nothing) Then
        arrayOfIds = collectSelectedItems(listHistory, historyIds)
        
        If Not (UBound(arrayOfIds) = LBound(arrayOfIds) And "" = arrayOfIds(0)) Then
            ignoreHistoryChanges = True
            For i = 0 To UBound(arrayOfIds)
                Set message = history.Find(arrayOfIds(i))
                If Not (message Is Nothing) Then
                    message.Read = False
                End If
            Next i
            ignoreHistoryChanges = False
            updateHistory
        End If
    End If
End Sub

Private Sub ChangePassword_Click()
    If meshControl.NetworkStatus <> NSONLINE Then
        MsgBox "To change the password you need to log in"
    Else
        meshControl.ShowPasswordWizard hWnd
    End If
End Sub

Private Sub Exit_Click()
    Unload Me
End Sub

Private Sub listContacts_ItemClick(ByVal item As MSComctlLib.ListItem)
    updateHistory
End Sub

Private Sub meshControl_HistoryChanged()
    updateHistory
End Sub

Private Sub Options_Click()
    meshControl.ShowSettingsDialog hWnd
End Sub

Private Sub SendFeedback_Click()
    meshControl.ShowFeedbackDialog hWnd
End Sub

Private Sub SignIn_Click()
    btnSignIn_Click
End Sub

Private Sub SignOut_Click()
    meshControl.SignOut
End Sub

Private Sub btnPTT_MouseDown(Button As Integer, Shift As Integer, x As Single, y As Single)
    Dim arrayOfIds() As String
    
    arrayOfIds = collectSelectedItems(listContacts, contactIds)
    
    If Not (UBound(arrayOfIds) = LBound(arrayOfIds) And "" = arrayOfIds(0)) Then
        meshControl.BeginMessage2 arrayOfIds
    End If
End Sub

Private Sub listContacts_DblClick()
    Dim i, iFirst, iSelected As Integer
    Dim contact As IContact
    Dim channel As IChannel
    Dim group As IGroup
    
    iFirst = 0
    iSelected = 0
    For i = 1 To listContacts.ListItems.Count
        If listContacts.ListItems(i).Selected = True Then
            iFirst = i
            iSelected = iSelected + 1
        End If
    Next i
    
    If iSelected = 1 Then
        Set contact = meshControl.contacts.Find(contactIds(iFirst - 1))
        If Not (contact Is Nothing) Then
            Select Case (contact.Type)
            Case CTGROUP:
                Set group = contact
                If Not (group Is Nothing) Then
                    'TODO: show group history or group users
                    Set group = Nothing
                End If
            Case CTCHANNEL:
                Set channel = contact
                If Not (channel Is Nothing) Then
                    'connect/disconnect channel
                    If channel.status = OSOFFLINE Then
                        channel.Connect
                    Else
                        channel.Disconnect
                    End If
                    'TODO: open channel message history
                    Set channel = Nothing
                End If
            Case Else:
                Set user = contact
                If Not (user Is Nothing) Then
                    'TODO: show user message history
                    Set user = Nothing
                End If
            End Select
        End If
        Set contact = Nothing
    End If
End Sub

Private Sub btnPTT_MouseUp(Button As Integer, Shift As Integer, x As Single, y As Single)
    meshControl.EndMessage
End Sub

Private Sub btnSignIn_Click()
    Dim username As String
    Dim pass As String
    Dim res As Boolean
    
    username = cbUsername.text
    pass = tbPassword.text
    
    If username <> "" And pass <> "" Then
        meshControl.SignIn username, pass, False
    Else
        MsgBox "Empty username or password"
        setStatus "Empty username or password"
    End If
End Sub

Private Sub Form_Load()
    Dim OemConfig() As Byte

    bExiting = False
    ignoreHistoryChanges = False
    Set history = Nothing
    manageControls
    listContacts.ColumnHeaders.Add , , "", listContacts.Width - 400
    listContacts.SmallIcons = imageListContacts
    listContacts.Icons = imageListContacts
    listHistory.ColumnHeaders.Add , , "", listHistory.Width - 400
    listHistory.SmallIcons = imageListHistory
    listHistory.Icons = imageListHistory
    'replace "default" below with your network name
    meshControl.Network.NetworkName = "default"
    meshControl.Network.LoginServer = "default.loudtalks.net"
    meshControl.Network.WebServer = "default.loudtalks.net"
    meshControl.Network.AddSupernode "default.loudtalks.net"
    meshControl.Settings.ShowTrayIcon = True
    meshControl.Settings.ShowIncomingNotification = False
    'Customize using embedded oem.config
    OemConfig = LoadResData(101, "OEM_CONFIG")
    meshControl.Customization.OemConfigData = OemConfig
    Set ctlDynamic = meshControl
End Sub

Private Sub Form_Unload(Cancel As Integer)
    'if we are online, go offline first
    If meshControl.NetworkStatus <> NSOFFLINE Then
        bExiting = True
        meshControl.SignOut
        Cancel = 1
    End If
End Sub

Private Sub meshControl_ContactListChanged()
    updateContacts
End Sub

Private Sub meshControl_GetCanSignIn(pbVal As Boolean)
    pbVal = Not (tbPassword.text = "") And Not (cbUsername.text = "")
End Sub

Private Sub meshControl_SignInRequested()
    btnSignIn_Click
End Sub

Private Sub meshControl_SignInStarted()
    setStatus "Signing in..."
    manageControls
End Sub

Private Sub meshControl_SignInSucceeded()
    setStatus "Signed in"
    manageControls
    updateContacts
    updateHistory
End Sub

Private Sub meshControl_SignInFailed()
    setStatus "Sign in failed"
    manageControls
End Sub

Private Sub meshControl_SignOutComplete()
    setStatus "Signed Out"
    manageControls
    updateContacts
    updateHistory
    If bExiting Then
        'we can't unload form here, do this later when timer hits
        timerExit.Enabled = True
    End If
End Sub

Private Sub meshControl_SignOutStarted()
    setStatus "Signing out..."
    manageControls
End Sub

Private Sub meshControl_MessageInBegin(ByVal pMessage As PttLibCtl.IMessage, pbActivate As Boolean)
    Dim contact As IContact
    Dim messageIn As IAudioInMessage

    Set messageIn = pMessage
    If Not (messageIn Is Nothing) Then
        Set contact = pMessage.Sender
        If Not (contact Is Nothing) Then
            Debug.Print "Incoming message " + messageIn.Id + " from " + contact.Name + " begins"
        End If
    End If
    'activate incoming message if possible
    pbActivate = True
End Sub

Private Sub meshControl_MessageInEnd(ByVal pMessage As PttLibCtl.IMessage)
    Dim contact As IContact
    Dim messageIn As IAudioInMessage

    Set messageIn = pMessage
    If Not (messageIn Is Nothing) Then
        Set contact = pMessage.Sender
        If Not (contact Is Nothing) Then
            Debug.Print "Incoming message " + messageIn.Id + " from " + contact.Name + " ends, duration " + Str(messageIn.duration)
        End If
    End If
End Sub

Private Sub meshControl_MessageOutBegin(ByVal pMessage As PttLibCtl.IMessage, ByVal pContact As PttLibCtl.IContact)
    Dim messageOut As IAudioOutMessage
    
    Set messageOut = pMessage
    If Not (messageOut Is Nothing Or pContact Is Nothing) Then
        Debug.Print "Outgoing message " + messageOut.Id + " to " + pContact.Name + " begins"
    End If
End Sub

Private Sub meshControl_MessageOutEnd(ByVal pMessage As PttLibCtl.IMessage, ByVal pContact As PttLibCtl.IContact)
    Dim messageOut As IAudioOutMessage
    
    Set messageOut = pMessage
    If Not (messageOut Is Nothing Or pContact Is Nothing) Then
        Debug.Print "Outgoing message " + messageOut.Id + " to " + pContact.Name + " ends, duration " + Str(messageOut.duration)
    End If
End Sub

Private Sub meshControl_MessageOutError(ByVal pMessage As PttLibCtl.IMessage, ByVal pContact As PttLibCtl.IContact)
    Dim messageOut As IAudioOutMessage
    
    Set messageOut = pMessage
    If Not (messageOut Is Nothing Or pContact Is Nothing) Then
        Debug.Print "Outgoing message " + messageOut.Id + " to " + pContact.Name + " error"
    End If
End Sub

Private Sub meshControl_AudioMessageInStart(ByVal pMessage As PttLibCtl.IAudioInMessage)
    Dim contact As IContact

    If Not (pMessage Is Nothing) Then
        Set contact = pMessage.Sender
        If Not (contact Is Nothing) Then
            Debug.Print "Incoming message " + pMessage.Id + " from " + contact.Name + " starts"
        End If
    End If
End Sub

Private Sub meshControl_AudioMessageInStop(ByVal pMessage As PttLibCtl.IAudioInMessage)
    Dim contact As IContact

    If Not (pMessage Is Nothing) Then
        Set contact = pMessage.Sender
        If Not (contact Is Nothing) Then
            Debug.Print "Incoming message " + pMessage.Id + " from " + contact.Name + " stops"
        End If
    End If
End Sub

Private Sub updateContacts()
    Dim i As Integer
    Dim contacts As IContacts
    Dim contact As IContact
    Dim statusString As String
    Dim nameString As String
    Dim iconIndex As Integer
    Dim item As ListItem

    listContacts.ListItems.Clear
    Set contacts = meshControl.contacts
    
    ReDim contactIds(contacts.Count) As String

    For i = 0 To contacts.Count - 1
        Set contact = contacts.item(i)
        If Not (contact Is Nothing) Then
            contacttype = contact.Type
            contactIds(i) = contact.Id
            nameString = contact.FullName
            If nameString = "" Then
                nameString = contact.Name
            End If
            statusString = contactStatus(contact)
            iconIndex = ContactIcon(contact)
            Set item = listContacts.ListItems.Add(, , nameString + " (" + statusString + ")", iconIndex, iconIndex)
            item.Bold = (contacttype = CTGROUP Or contacttype = CTCHANNEL)
            Set contact = Nothing
        End If
    Next i

    Set contacts = Nothing

End Sub

Private Sub updateHistory()
    Dim i, j As Integer
    Dim contact As IContact
    Dim message As IMessage
    Dim audioMessage As IAudioMessage
    Dim audioInMessage As IAudioInMessage
    Dim audioOutMessage As IAudioOutMessage
    Dim dateString, timeString, nameString As String
    Dim iconIndex As Integer
    Dim item As ListItem

    Set history = Nothing

    Set contact = Nothing
    If Not (listContacts.SelectedItem Is Nothing) Then
        Set contact = meshControl.contacts.Find(contactIds(listContacts.SelectedItem.Index - 1))
    End If

    If Not (contact Is Nothing) Then
        Set history = contact.history
    ElseIf meshControl.contacts.Count > 0 Then
        Set history = meshControl.history("")
    End If

    listHistory.ListItems.Clear
    ReDim historyIds(0) As String
    
    If Not (history Is Nothing) Then
        ReDim historyIds(history.Count) As String
        For i = 0 To history.Count - 1
            Set message = history.item(i)
            If Not (message Is Nothing) Then
                historyIds(i) = message.Id
                dateString = Format(message.CreationTime, "General Date")
                If message.Type = MTAUDIO Then
                    Set audioMessage = message
                    timeString = msToString(audioMessage.duration)
                    If message.Incoming Then
                        iconIndex = 1
                        Set audioInMessage = audioMessage
                        nameString = audioInMessage.Sender.Name
                        If Not (audioInMessage.Author Is Nothing) Then
                            nameString = nameString + "\" + audioInMessage.Author.Name
                        End If
                    Else
                        iconIndex = 2
                        Set audioOutMessage = audioMessage
                        nameString = ""
                        For j = 1 To audioOutMessage.Recipients.Count
                            If Not (nameString = "") Then
                                nameString = nameString + ", "
                            End If
                            nameString = nameString + audioOutMessage.Recipients.item(j - 1).Name
                        Next j
                    End If
                End If
                Set item = listHistory.ListItems.Add(, , dateString + " " + timeString + " " + nameString, iconIndex, iconIndex)
                item.Bold = Not message.Read
                Set message = Nothing
                Set audioMessage = Nothing
                Set audioInMessage = Nothing
                Set audioOutMessage = Nothing
            End If
        Next i
    End If
End Sub

Private Function contactStatus(contact As IContact)
    Dim status As ONLINE_STATUS
    Dim user As IUser
    Dim group As IGroup
    Dim channel As IChannel
    Dim s As String
    
    s = ""
    
    If Not (contact Is Nothing) Then
        status = contact.status
        Select Case (contact.Type)
        Case CTGROUP:
            Set group = contact
            If group Is Nothing Or group.OnlineCount < 1 Then
                s = statusToString(OSOFFLINE)
            Else
                s = Trim(Str(group.OnlineCount)) + "/" + Trim(Str(group.Count))
            End If
        Case CTCHANNEL:
            Set channel = contact
            If status = OSOFFLINE Or status = OSCONNECTING Or channel Is Nothing Then
                s = statusToString(status)
            Else
                s = "Users online: " + Trim(Str(channel.OnlineCount))
            End If
        Case Else:
            Set user = contact
            If Not (status = OSOFFLINE Or user Is Nothing) Then
                s = user.CustomStatusText
            End If
            If s = "" Then
                s = statusToString(status)
            End If
        End Select
    End If
    
    Set group = Nothing
    Set channel = Nothing
    contactStatus = s
    
End Function

Private Function ContactIcon(contact As IContact) As Integer
    Dim i As Integer

    i = 2
    
    If Not (contact Is Nothing) Then
        Select Case (contact.Type)
        Case CTGROUP:
            i = 6
        Case CTCHANNEL:
            If contact.status = OSOFFLINE Then
                i = 8
            Else
                i = 7
            End If
        Case Else:
            Select Case (contact.status)
            Case OSAVAILABLE:
                i = 1
            Case OSAWAY:
                i = 4
            Case OSBUSY:
                i = 3
            Case OSHEADPHONES:
                i = 5
            Case Else:
                i = 2
            End Select
        End Select
    End If
    
    ContactIcon = i
End Function

Private Function statusToString(contactStatus As ONLINE_STATUS) As String
    Select Case (contactStatus)
    Case OSAVAILABLE:
        statusToString = "Available"
    Case OSAWAY:
        statusToString = "Away"
    Case OSBUSY:
        statusToString = "Busy"
    Case OSHEADPHONES:
        statusToString = "Headphones"
    Case OSCONNECTING:
        statusToString = "Connecting..."
    Case Else:
        statusToString = "Offline"
    End Select
End Function

Private Function msToString(duration As Integer) As String
    Dim res As String
    Dim sec As Integer
    sec = duration \ 1000
    res = ""

    If sec < 60 Then
        res = Trim(Str(sec)) + "." + Trim(Str((duration - sec * 1000) / 100)) + " s"
    Else
        res = Trim(Str(sec \ 60)) + " m"
        If sec Mod 60 > 0 Then
            res = res + " " + Trim(Str(sec Mod 60)) + " s"
        End If
    End If
    msToString = res
End Function

Private Sub timerExit_Timer()
    If bExiting Then
        Unload Me
    End If
End Sub

Private Function collectSelectedItems(listCtrl As ListView, idIndex() As String) As String()
    Dim arrayOfIds() As String
    Dim i As Integer
    Dim iSelected As Integer
    Dim iCounter As Integer
    
    ReDim arrayOfIds(0) As String
    arrayOfIds(0) = ""
    iSelected = 0
    For i = 1 To listCtrl.ListItems.Count
        If listCtrl.ListItems(i).Selected = True Then
            iSelected = iSelected + 1
        End If
    Next i
    
    If iSelected > 0 Then
        iCounter = 0
        For i = 1 To listCtrl.ListItems.Count
            If listCtrl.ListItems(i).Selected = True Then
                If UBound(idIndex) >= i - 1 Then
                    ReDim Preserve arrayOfIds(iCounter) As String
                    arrayOfIds(iCounter) = idIndex(i - 1)
                    iCounter = iCounter + 1
                End If
            End If
        Next i
    End If

    collectSelectedItems = arrayOfIds
End Function

Private Sub manageControls()
    Dim status As NETWORK_STATUS
    Dim showContacts As Boolean
    
    status = NSOFFLINE
    If Not (meshControl Is Nothing) Then
        status = meshControl.NetworkStatus
    End If
    
    showContacts = (status = NSONLINE Or status = NSSIGNINGOUT)
    labelUsername.Visible = Not showContacts
    cbUsername.Visible = Not showContacts
    labelPassword.Visible = Not showContacts
    tbPassword.Visible = Not showContacts
    btnSignIn.Visible = Not showContacts
    listContacts.Visible = showContacts
    btnPTT.Visible = showContacts
    labelHistory.Visible = showContacts
    listHistory.Visible = showContacts
    btnRead.Visible = showContacts
    btnUnread.Visible = showContacts
    btnDelete.Visible = showContacts
    btnDeleteAll.Visible = showContacts
    btnPlay.Visible = showContacts
End Sub

Private Sub setStatus(text As String)
    statusBar.SimpleText = text
End Sub
