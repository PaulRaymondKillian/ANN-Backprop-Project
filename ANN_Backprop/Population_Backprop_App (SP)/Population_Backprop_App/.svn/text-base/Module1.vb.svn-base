Option Strict On

Imports System.IO

Module Module1


    Private PopFileLocation As String
    Private AppPreviousEntries(9) As String

    Dim ConsoleLibrary As New Console_Library.Console_Library   'Used to change text color in console

    Sub Main()
        'Change the Console appearance and redisplay.
        Console.Title = "Population Backprop App - SP"
        Console.SetWindowSize(150, 50)
        Console.BufferHeight = 1000
        Console.BufferWidth = 150

        OutputProgramInfo()

        Try
            LoadPopulationAppDataFile()
            Dim Population As New Population.Population(PopFileLocation)

        Catch ex As Exception
            ConsoleLibrary.SetColor(Console_Library.Console_Library.ConsoleColor.RedForte)
            Console.WriteLine(ex.Message)
        Finally
            ConsoleLibrary.SetColor(Console_Library.Console_Library.ConsoleColor.White)
            Console.Write("Please Press Enter to Exit...")
            Dim z As String = Console.ReadLine()
        End Try
    End Sub

    Private Sub LoadPopulationAppDataFile()
        Try
            'Create the AppPreviousEntries file if it does not already exist
            If Not File.Exists("AppPreviousEntriesPOP.txt") Then
                File.Create("AppPreviousEntriesPOP.txt")
            End If

            Console.WriteLine("Please Select Your POP File Choice - ")
            Console.WriteLine()

            Dim numPreviousEntries As Integer = 0
            Dim sr As StreamReader
            sr = File.OpenText("AppPreviousEntriesPOP.txt")
            For i As Integer = 0 To 9
                If Not sr.EndOfStream Then
                    AppPreviousEntries(i) = sr.ReadLine
                    numPreviousEntries += 1
                    Console.WriteLine("Previous Entry " & i + 1 & ": " & AppPreviousEntries(i))
                End If
            Next
            sr.Close()
            Console.WriteLine()
            If numPreviousEntries > 0 Then
                Console.WriteLine("Enter 'Clear' to clear out the previous entries")
                Console.Write("Enter Value of Previous Entry to Re-Execute (1 to " & numPreviousEntries & "), or Enter New File: ")
            Else
                GoTo EnterNewFile
            End If

            Dim input As String = Console.ReadLine

            'Allow use to clear the AppPreviousEntries file
            If input.ToUpper = "CLEAR" Then
                Dim a As StreamWriter   'Empty out file
                a = File.CreateText("AppPreviousEntriesPOP.txt")
                a.Close()
                numPreviousEntries = 0
                Console.Clear()
                OutputProgramInfo()
                sr = Nothing
                LoadPopulationAppDataFile() 'Re-load the function then
            ElseIf InStr(input.ToUpper, ".POP") > 0 Then
                PopFileLocation = input
                GoTo SetupNewFile
            ElseIf input = "1" Then
                If numPreviousEntries >= 1 Then
                    PopFileLocation = AppPreviousEntries(0)
                Else
                    GoTo EnterNewFile
                End If
            ElseIf input = "2" Then
                If numPreviousEntries >= 2 Then
                    PopFileLocation = AppPreviousEntries(1)
                Else
                    GoTo EnterNewFile
                End If
            ElseIf input = "3" Then
                If numPreviousEntries >= 3 Then
                    PopFileLocation = AppPreviousEntries(2)
                Else
                    GoTo EnterNewFile
                End If
            ElseIf input = "4" Then
                If numPreviousEntries >= 4 Then
                    PopFileLocation = AppPreviousEntries(3)
                Else
                    GoTo EnterNewFile
                End If
            ElseIf input = "5" Then
                If numPreviousEntries >= 5 Then
                    PopFileLocation = AppPreviousEntries(4)
                Else
                    GoTo EnterNewFile
                End If
            ElseIf input = "6" Then
                If numPreviousEntries >= 6 Then
                    PopFileLocation = AppPreviousEntries(5)
                Else
                    GoTo EnterNewFile
                End If
            ElseIf input = "7" Then
                If numPreviousEntries >= 7 Then
                    PopFileLocation = AppPreviousEntries(6)
                Else
                    GoTo EnterNewFile
                End If
            ElseIf input = "8" Then
                If numPreviousEntries >= 8 Then
                    PopFileLocation = AppPreviousEntries(7)
                Else
                    GoTo EnterNewFile
                End If
            ElseIf input = "9" Then
                If numPreviousEntries >= 9 Then
                    PopFileLocation = AppPreviousEntries(8)
                Else
                    GoTo EnterNewFile
                End If
            ElseIf input = "10" Then
                If numPreviousEntries >= 10 Then
                    PopFileLocation = AppPreviousEntries(9)
                Else
                    GoTo EnterNewFile
                End If
            Else
                GoTo EnterNewFile
            End If
        Catch ex As Exception
            Throw ex
        End Try

        Exit Sub
EnterNewFile:
        Console.Write("Please Input Population File Location: ")
        PopFileLocation = Console.ReadLine
SetupNewFile:
        If InStr(PopFileLocation, ".POP") = 0 Then
            Throw New Exception("Invalid POP File Inputted!")
        End If

        'Move everything down 1
        For i As Integer = 9 To 1 Step -1
            AppPreviousEntries(i) = AppPreviousEntries(i - 1)
        Next
        AppPreviousEntries(0) = PopFileLocation

        Dim sw As StreamWriter
        sw = File.CreateText("AppPreviousEntriesPOP.txt")
        For i As Integer = 0 To 9
            If AppPreviousEntries(i) <> "" Then
                sw.WriteLine(AppPreviousEntries(i))
            End If
        Next
        sw.Close()
    End Sub

    Public Sub OutputProgramInfo()
        ConsoleLibrary.SetColor(Console_Library.Console_Library.ConsoleColor.RedForte)
        Console.WriteLine("Population Backprop App - SP (2009)")
        Console.WriteLine("Created by: Paul Killian")
        Console.WriteLine()
        ConsoleLibrary.SetColor(Console_Library.Console_Library.ConsoleColor.WhiteForte)
    End Sub

End Module
