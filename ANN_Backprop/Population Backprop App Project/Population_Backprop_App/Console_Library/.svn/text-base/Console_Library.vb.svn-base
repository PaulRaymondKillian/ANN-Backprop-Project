' Please do not remove :)
' Written by Kourosh Derakshan
'
Option Strict On

Imports System.Text

Public NotInheritable Class Console_Library
#Region " Declarations "
    Private hConsoleIn, hConsoleOut As IntPtr
    Private conInfo As Win32Native.CONSOLE_INFO
    Private cursorInfo As Win32Native.CURSOR_INFO
    Private backColor As Integer
    Private backgroundAttrib As Short


    Enum InputMode
        LineInput
        EchoInput
    End Enum

    Public Enum ConsoleColor
        Black = 0
        Blue = Win32Native.FOREGROUND_BLUE
        Green = Win32Native.FOREGROUND_GREEN

        SkyBlue = Win32Native.FOREGROUND_BLUE + Win32Native.FOREGROUND_GREEN

        Red = Win32Native.FOREGROUND_RED
        Purple = Win32Native.FOREGROUND_BLUE + Win32Native.FOREGROUND_RED
        Brown = Win32Native.FOREGROUND_GREEN + Win32Native.FOREGROUND_RED
        White = Win32Native.FOREGROUND_BLUE + Win32Native.FOREGROUND_GREEN + _
                Win32Native.FOREGROUND_RED
        Gray = Win32Native.FOREGROUND_INTENSIFY
        BlueForte = Win32Native.FOREGROUND_BLUE + Win32Native.FOREGROUND_INTENSIFY
        GreenForte = Win32Native.FOREGROUND_GREEN + Win32Native.FOREGROUND_INTENSIFY
        SkyBlueForte = Win32Native.FOREGROUND_BLUE + Win32Native.FOREGROUND_GREEN + _
                       Win32Native.FOREGROUND_INTENSIFY
        RedForte = Win32Native.FOREGROUND_RED + Win32Native.FOREGROUND_INTENSIFY
        PurpleForte = Win32Native.FOREGROUND_BLUE + Win32Native.FOREGROUND_RED + _
        Win32Native.FOREGROUND_INTENSIFY
        Yellow = Win32Native.FOREGROUND_GREEN + Win32Native.FOREGROUND_RED + _
        Win32Native.FOREGROUND_INTENSIFY
        WhiteForte = Win32Native.FOREGROUND_BLUE + Win32Native.FOREGROUND_GREEN + _
                     Win32Native.FOREGROUND_RED + Win32Native.FOREGROUND_INTENSIFY
    End Enum

    Public Enum CursorType
        Off
        SingleLine
        Block
    End Enum
#End Region

    Public Sub New()
        ' Creates a console if there isn't already one loaded.
        Win32Native.AllocConsole()

        ' Set the input handle
        hConsoleIn = Win32Native.GetStdHandle(Win32Native.STD_INPUT_HANDLE)
        ' Set the output handle
        hConsoleOut = Win32Native.GetStdHandle(Win32Native.STD_OUTPUT_HANDLE)


        ' Fill in the console information to conInfo
        conInfo = New Win32Native.CONSOLE_INFO
        updateConsoleInfo()

        ' Fill in the cursor information for this console
        cursorInfo = New Win32Native.CURSOR_INFO
        SetCursorType(CursorType.SingleLine)

        ' Set the standard background attribute
        backgroundAttrib = CShort((CShort(ConsoleColor.Black) * &H10 + CShort(ConsoleColor.White)))
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
        FreeConsole()
    End Sub

    Public Property Title() As String
        Get
            Dim buffer As New StringBuilder(128)
            Win32Native.GetConsoleTitle(buffer, 128)
            Return buffer.ToString
        End Get

        Set(ByVal Value As String)
            Win32Native.SetConsoleTitle(Value)
        End Set
    End Property

    Public ReadOnly Property Columns() As Integer
        Get
            Return conInfo.MaxSize.x
        End Get
    End Property

    Public ReadOnly Property Rows() As Integer
        Get
            Return conInfo.MaxSize.y
        End Get
    End Property

    ' X position of the cursor (or the carret) on the screen
    Public ReadOnly Property CursorX() As Integer
        Get
            updateConsoleInfo()
            Return conInfo.CursorPosition.x
        End Get
    End Property

    ' Y position of the cursor (or the carret) on the screen
    Public ReadOnly Property CursorY() As Integer
        Get
            updateConsoleInfo()
            Return conInfo.CursorPosition.y
        End Get
    End Property

    ' LineInput will let the console read a line at a time. EchoInput lets the console
    ' read each key stroke, as the user types something (You have to use Console.Read ()
    ' instead of Console.ReadLine() when in EchoInput.)
    Public Sub SetMode(ByVal mode As InputMode)
        Dim conMode As Integer
        Win32Native.GetConsoleMode(hConsoleIn, conMode)

        If mode = InputMode.EchoInput Then
            conMode = conMode And Not (Win32Native.ENABLE_LINE_INPUT Or Win32Native.ENABLE_ECHO_INPUT)
        Else
            conMode = conMode Or (Win32Native.ENABLE_LINE_INPUT Or Win32Native.ENABLE_ECHO_INPUT)
        End If
        Win32Native.SetConsoleMode(hConsoleIn, conMode)
    End Sub

    Public Sub Clear()
        Dim written As Integer = 0
        Dim startCoord As New Win32Native.COORD
        startCoord.x = 0 : startCoord.y = 0

        Win32Native.FillConsoleOutputCharacter(hConsoleOut, " "c, conInfo.MaxSize.x * conInfo.MaxSize.y, startCoord, written)
        Win32Native.FillConsoleOutputAttribute(hConsoleOut, backgroundAttrib, conInfo.MaxSize.x * conInfo.MaxSize.y, startCoord, written)

        MoveCursor(1, 1)
    End Sub

    ' Use only if the console is in LineInput mode. If value is true, then the things
    ' that the user types will automatically show up in the console. (In EchoInput mode
    ' nothing shows up automatically)
    Public Sub EchoInput(ByVal value As Boolean)
        Dim Ret As Integer
        Win32Native.GetConsoleMode(hConsoleIn, Ret)
        If value Then
            Ret = Ret Or (Win32Native.ENABLE_ECHO_INPUT)
        Else
            Ret = Ret And Not (Win32Native.ENABLE_ECHO_INPUT)
        End If
        Win32Native.SetConsoleMode(hConsoleIn, Ret)
    End Sub

    Public Sub SetColor(ByVal foreColor As ConsoleColor, ByVal backColor As ConsoleColor)
        Me.backColor = CInt(backColor)
        SetColor(foreColor)
    End Sub

    Public Sub SetColor(ByVal foreColor As ConsoleColor)
        Win32Native.SetConsoleTextAttribute(hConsoleOut, CInt(foreColor) + 16 * backColor)
    End Sub

    Public Sub SetClsColor(ByVal backColor As ConsoleColor)
        backgroundAttrib = CShort(backColor) * &H10S
    End Sub

    Public Sub MoveCursor(ByVal x As Integer, ByVal y As Integer)
        conInfo.CursorPosition.x = CShort(x - 1)
        conInfo.CursorPosition.y = CShort(y - 1)
        If cursorInfo.Visible Then
            Dim coord As Integer = conInfo.CursorPosition.x + conInfo.CursorPosition.y * &H10000
            Win32Native.SetConsoleCursorPosition(hConsoleOut, coord)
        End If
    End Sub


    ' Change the cursor's shape and visibility state.
    Public Sub SetCursorType(ByVal newType As CursorType)
        '  Change the cursor type.
        Select Case newType
            Case CursorType.Block
                cursorInfo.Size = 100
                cursorInfo.Visible = True

            Case CursorType.SingleLine
                cursorInfo.Size = 10
                cursorInfo.Visible = True

            Case CursorType.Off
                cursorInfo.Size = 100
                cursorInfo.Visible = False
        End Select

        Win32Native.SetConsoleCursorInfo(hConsoleOut, cursorInfo)

        ' Move the cursor to its correct position.
        MoveCursor(conInfo.CursorPosition.x, conInfo.CursorPosition.y)
    End Sub

    ' Forces to close the console. Don't call if not necessary.
    Public Sub FreeConsole()
        ' I just added these myself. I don't if I really have to do this, but I thought
        ' they are necessary. That's why it's in a Try-Catch block, because I don't
        ' know if it may cause an error or not. But it shouldn't.
        Try
            Win32Native.FreeConsole()
            Win32Native.CloseHandle(hConsoleIn)
            Win32Native.CloseHandle(hConsoleOut)
            Win32Native.FreeConsole()
        Catch
        End Try
    End Sub


    Private Sub updateConsoleInfo()
        ' Fill in the console information to conInfo
        Win32Native.GetConsoleScreenBufferInfo(hConsoleOut, conInfo)
    End Sub
End Class
