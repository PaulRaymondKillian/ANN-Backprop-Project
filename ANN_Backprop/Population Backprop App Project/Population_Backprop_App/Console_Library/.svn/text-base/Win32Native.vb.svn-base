' Please do not remove :)
' Written by Kourosh Derakshan
'
Option Strict On

Imports System.Runtime.InteropServices
Imports System.Text


Friend Class Win32Native
    ' Input and output handle 
    Friend Const STD_OUTPUT_HANDLE As Integer = -11&
    Friend Const STD_INPUT_HANDLE As Integer = -10&
    ' Echo Input would let the program to receive each key stroke.
    Friend Const ENABLE_LINE_INPUT As Short = &H2S
    Friend Const ENABLE_ECHO_INPUT As Short = &H4S

    ' Color codes
    Friend Const FOREGROUND_BLUE As Integer = &H1&
    Friend Const FOREGROUND_GREEN As Integer = &H2&
    Friend Const FOREGROUND_RED As Integer = &H4&
    Friend Const FOREGROUND_INTENSIFY As Integer = &H8&
    Friend Const BACKGROUND_BLUE As Integer = &H10&
    Friend Const BACKGROUND_GREEN As Integer = &H20&
    Friend Const BACKGROUND_INTENSIFY As Integer = &H80&



    Public Enum BACKGROUNDCOLOR
        bBLUE = &H10&   'BACKGROUND_BLUE
        bGREEN = &H20&  'BACKGROUND_GREEN
        bRED = &H40&    'BACKGROUND_RED
        bBRIGHT = &H80& 'BACKGROUND_INTENSITY
    End Enum

    <StructLayout(LayoutKind.Sequential)> _
    Friend Structure COORD
        Friend x, y As Short
    End Structure

    <StructLayout(LayoutKind.Sequential)> _
    Friend Structure RECT
        Dim Left, Top, Right, Bottom As Short
    End Structure

    <StructLayout(LayoutKind.Sequential)> _
    Friend Structure CONSOLE_INFO
        Friend Size As COORD
        Friend CursorPosition As COORD
        Friend Attribute As Short
        Friend Window As RECT
        Friend MaxSize As COORD
    End Structure

    <StructLayout(LayoutKind.Sequential)> _
    Friend Structure CURSOR_INFO
        Friend Size As Integer
        Friend Visible As Boolean
    End Structure





    <DllImport("kernel32")> _
           Public Shared Sub SetConsoleTitle(ByVal lpTitleStr As String)
    End Sub

    <DllImport("kernel32")> _
    Public Shared Sub GetConsoleTitle(ByVal lpBuff As StringBuilder, ByVal buffSize As Integer)
    End Sub

    <DllImport("kernel32")> _
           Public Shared Function SetConsoleTextAttribute(ByVal hConsoleOutput As IntPtr, ByVal wAttributes As Integer) As Integer
    End Function

    <DllImport("kernel32")> _
    Public Shared Function FillConsoleOutputCharacter(ByVal Handle As IntPtr, ByVal uChar As Char, _
                                                      ByVal Len As Integer, ByVal start As COORD, ByRef written As Integer) As Integer
    End Function

    <DllImport("kernel32")> _
    Public Shared Function FillConsoleOutputAttribute(ByVal Handle As IntPtr, ByVal att As Short, _
                                                      ByVal Len As Integer, ByVal start As COORD, _
                                                      ByRef writted As Integer) As Boolean
    End Function


    <DllImport("kernel32")> _
    Public Shared Sub GetConsoleScreenBufferInfo(ByVal Handle As IntPtr, _
                                                 ByRef info As CONSOLE_INFO)
    End Sub

    <DllImport("kernel32")> _
    Public Shared Function SetConsoleCursorInfo(ByVal Handle As IntPtr, ByRef info As CURSOR_INFO) As Boolean
    End Function

    <DllImport("kernel32")> _
    Public Shared Function SetConsoleCursorPosition(ByVal handle As IntPtr, ByVal coord As Integer) As Boolean
    End Function

    <DllImport("kernel32")> _
    Public Shared Function GetStdHandle(ByVal nStdHandle As Integer) As IntPtr
    End Function

    <DllImport("kernel32")> _
    Public Shared Sub GetConsoleMode(ByVal hConsoleHandle As IntPtr, ByRef lpMode As Integer)
    End Sub

    <DllImport("kernel32")> _
    Public Shared Sub SetConsoleMode(ByVal hConsoleHandle As IntPtr, ByVal dwMode As Integer)
    End Sub

    <DllImport("kernel32")> _
    Public Shared Function CloseHandle(ByVal hObject As IntPtr) As Integer
    End Function

    <DllImport("kernel32")> _
    Public Shared Function AllocConsole() As Integer
    End Function

    <DllImport("kernel32")> _
    Public Shared Function FreeConsole() As Integer
    End Function

End Class