# ZelloWork Windows app command line interface

### Description
ZelloWork command line interface allows you to send text, voice or image messages to the specified user or channel. ZelloWork application must be installed and the correct user account configured.

If Zello app is already running it will immediately start sending the message. If Zello app is not running it will start, sign in and then send the message. The result could be saved to a text file and you can customize operation timeout.

### Supported switches

Option | Description
-------|-------
/audio=`path` | The full path to the audio file contaning the message to send. File must be in `WAV` format. Options `/audio`, `/alert`, and `/image` are mutually exclusive.
/image=`path` | The full path the the image file to send.
/alert=`text` | The text of the call alert to send. The text must be enclosed in quotation marks, and any quotation marks inside must be escaped. Example: `"The boat is called \"Zello Star\""`
/contact=`name` | The `name` of the contact or channel to send the message to. 
/channel=false | _Optional._ Include this switch if name specified by the `/contact` switch is a name of a user. By default the message will be sent to a channel.
/report=`file name` |  _Optional._ The name of the file, where the app will write the operation result. If a file already exists, it will be appended. You can use this file to audit which messages were sent, when they were sent, and to troubleshoot issues.
/timeout=`timeout` |  _Optional._ Operation timeout in seconds. If the app is unable to complete the operation within specified time, it will cancel it and write error to the report file.

### Examples

#### Audio message
Send audio message from file `Audioclip.wav` to a channel named `This game has no name` with the timeout of `12` seconds and write operation result to `E:\Voice\log.txt`.

```shell
"C:\Program Files\ZelloWork\ptt.exe" /contact="This game has no name"  /audio="E:\Voice\Audioclip.wav" /report="E:\Voice\log.txt" /timeout=12
```

#### Call alert
Send call alert `Please call dispatch` to user `1182 Mike` with the timeout of `40` seconds and write operation result to `result.txt`


```shell
"C:\Program Files\ZelloWork\ptt.exe" /contact="1182 Mike"  /alert="Please call dispatch" /report="result.txt" /timeout=40
```



