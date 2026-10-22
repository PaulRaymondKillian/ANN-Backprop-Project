''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'' Project: Artificial Neural Network Error Backwards-Propagation Project	    		''
'' Component: Backprop Process App														''
'' Description: Application that performs the core artificial neural network backwards	''
''				error-propagation learning algorithm. This is the VB.Net version of the ''
''              Backprop Process App. There exists a C++ version of this project as     ''
''              well, however, the Population Backprop App is VB.Net. This is a         ''
''              stand-alone project, whereas the Population Backprop App uses the       ''
''              Backprop object, not directly associated with this specific application.''
''																						''
'' Author: Paul Killian																	''
'' Contact: PaulKillian@DecodedCortex.com												''
'' Website: www.DecodedCortex.com														''
'' Disclaimer: Any non-commercial use of this code or a derivative therof must give     ''
''              credit to those who developed it (Decoded Cortex). Any commercial use of''
''              this code or the application themselves is now allowed unless authorized''
''              by Decoded Cortex.                                                      ''
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Option Strict On

Imports System.IO

Module Module1


    Private BPROPFileLocation As String
    Private AppPreviousEntries(9) As String

    Dim ConsoleLibrary As New Console_Library.Console_Library   'Used to change text color in console

    Sub Main()
        'Change the Console appearance and redisplay.
        Console.Title = "Backprop Process App - SP"
        Console.SetWindowSize(150, 50)
        Console.BufferHeight = 1000
        Console.BufferWidth = 150

        OutputProgramInfo()

        Try
            LoadBackpropAppDataFile()
            Dim Backprop As New Backprop.Backprop(BPROPFileLocation)
            RunBackprop(Backprop)

        Catch ex As Exception
            ConsoleLibrary.SetColor(Console_Library.Console_Library.ConsoleColor.RedForte)
            Console.WriteLine(ex.Message)
        Finally
            ConsoleLibrary.SetColor(Console_Library.Console_Library.ConsoleColor.White)
            Console.Write("Please Press Enter to Exit...")
            Dim z As String = Console.ReadLine()
        End Try
    End Sub

    Private Sub LoadBackpropAppDataFile()
        Try
            'Create the AppPreviousEntries file if it does not already exist
            If Not File.Exists("AppPreviousEntriesBPROP.txt") Then
                File.Create("AppPreviousEntriesBPROP.txt")
            End If

            Console.WriteLine("Please Select Your BPROP File Choice- ")
            Console.WriteLine()

            Dim numPreviousEntries As Integer = 0
            Dim sr As StreamReader
            sr = File.OpenText("AppPreviousEntriesBPROP.txt")
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
                a = File.CreateText("AppPreviousEntriesBPROP.txt")
                a.Close()
                numPreviousEntries = 0
                Console.Clear()
                OutputProgramInfo()
                sr = Nothing
                LoadBackpropAppDataFile() 'Re-load the function then
            ElseIf InStr(input.ToUpper, ".POP") > 0 Then
                BPROPFileLocation = input
                GoTo SetupNewFile
            ElseIf input = "1" Then
                If numPreviousEntries >= 1 Then
                    BPROPFileLocation = AppPreviousEntries(0)
                Else
                    GoTo EnterNewFile
                End If
            ElseIf input = "2" Then
                If numPreviousEntries >= 2 Then
                    BPROPFileLocation = AppPreviousEntries(1)
                Else
                    GoTo EnterNewFile
                End If
            ElseIf input = "3" Then
                If numPreviousEntries >= 3 Then
                    BPROPFileLocation = AppPreviousEntries(2)
                Else
                    GoTo EnterNewFile
                End If
            ElseIf input = "4" Then
                If numPreviousEntries >= 4 Then
                    BPROPFileLocation = AppPreviousEntries(3)
                Else
                    GoTo EnterNewFile
                End If
            ElseIf input = "5" Then
                If numPreviousEntries >= 5 Then
                    BPROPFileLocation = AppPreviousEntries(4)
                Else
                    GoTo EnterNewFile
                End If
            ElseIf input = "6" Then
                If numPreviousEntries >= 6 Then
                    BPROPFileLocation = AppPreviousEntries(5)
                Else
                    GoTo EnterNewFile
                End If
            ElseIf input = "7" Then
                If numPreviousEntries >= 7 Then
                    BPROPFileLocation = AppPreviousEntries(6)
                Else
                    GoTo EnterNewFile
                End If
            ElseIf input = "8" Then
                If numPreviousEntries >= 8 Then
                    BPROPFileLocation = AppPreviousEntries(7)
                Else
                    GoTo EnterNewFile
                End If
            ElseIf input = "9" Then
                If numPreviousEntries >= 9 Then
                    BPROPFileLocation = AppPreviousEntries(8)
                Else
                    GoTo EnterNewFile
                End If
            ElseIf input = "10" Then
                If numPreviousEntries >= 10 Then
                    BPROPFileLocation = AppPreviousEntries(9)
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
        BPROPFileLocation = Console.ReadLine
SetupNewFile:
        If InStr(BPROPFileLocation, ".BPROP") = 0 Then
            Throw New Exception("Invalid POP File Inputted!")
        End If

        'Move everything down 1
        For i As Integer = 9 To 1 Step -1
            AppPreviousEntries(i) = AppPreviousEntries(i - 1)
        Next
        AppPreviousEntries(0) = BPROPFileLocation

        Dim sw As StreamWriter
        sw = File.CreateText("AppPreviousEntriesBPROP.txt")
        For i As Integer = 0 To 9
            If AppPreviousEntries(i) <> "" Then
                sw.WriteLine(AppPreviousEntries(i))
            End If
        Next
        sw.Close()
    End Sub

    Public Sub RunBackprop(ByVal Backprop As Backprop.Backprop)
        Dim EpochsPerInterval, Intervals, SaveInterval As Integer
        Console.WriteLine()
        Console.Write("Epochs Per Interval: ")
        EpochsPerInterval = Convert.ToInt32(Console.ReadLine)

        Console.Write("Total Intervals to Run: ")
        Intervals = Convert.ToInt32(Console.ReadLine)

        Console.Write("Save Interval: ")
        SaveInterval = Convert.ToInt32(Console.ReadLine)

        Dim currentFitness(Intervals) As Double
        Dim Diff As Double

        Backprop.calcFitness()
        currentFitness(0) = Backprop.Fitness
        Console.WriteLine()
        ConsoleLibrary.SetColor(Console_Library.Console_Library.ConsoleColor.WhiteForte)
        Console.WriteLine("Intial State - " & Backprop.ID & ": " & Math.Round(currentFitness(0), 5) & " Avg: " & Math.Round(currentFitness(0) / Backprop.exemplarCount, 5) & " Rho: " & Backprop.rho)
        For i As Integer = 1 To Intervals
            ConsoleLibrary.SetColor(Console_Library.Console_Library.ConsoleColor.Gray)
            Backprop.Train(EpochsPerInterval, True)
            Backprop.calcFitness()
            currentFitness(i) = Backprop.Fitness
            Diff = currentFitness(i) - currentFitness(i - 1)
            SetTextColor(Diff, currentFitness(1) - currentFitness(0))
            Console.WriteLine("Interval " & i & ", Epochs Run: " & EpochsPerInterval & ", Total Epochs: " & (EpochsPerInterval * (i + 1)) & "  - " & Backprop.ID & ": " & Math.Round(currentFitness(i), 5) & " Avg: " & Math.Round(currentFitness(i) / Backprop.exemplarCount, 5) & " Diff: " & Math.Round(Diff, 5) & " Rho: " & Backprop.rho)
            If i Mod SaveInterval = 0 Then
                Directory.CreateDirectory(Backprop.CurrentDirectory & Backprop.FileTitle)
                Backprop.outputNetwork(Backprop.CurrentDirectory & Backprop.FileTitle & "\" & Backprop.FileTitle & ".ANN")
                Backprop.outputNetworkOutput(Backprop.CurrentDirectory & Backprop.FileTitle & "\" & Backprop.FileTitle & ".txt")
                Backprop.outputBPROPFile(Backprop.CurrentDirectory & Backprop.FileTitle & "\" & Backprop.FileTitle & ".BPROP", Backprop.FileTitle)
            End If
        Next

    End Sub


    Public Sub SetTextColor(ByVal Diff As Double, ByVal InitialDiff As Double)
        If InitialDiff = 0 Then
            Throw New Exception("Error - Initial Diff is 0, that is bad probably...")
        End If
        Dim PercentLessThanInitialDiff As Double = Diff / InitialDiff

        If PercentLessThanInitialDiff >= 1 Then      'If No Diff or is first Diff or is unusual diff
            ConsoleLibrary.SetColor(Console_Library.Console_Library.ConsoleColor.WhiteForte)
        ElseIf PercentLessThanInitialDiff < 0 Then  'If Diff is Negative then BAD!!!!
            ConsoleLibrary.SetColor(Console_Library.Console_Library.ConsoleColor.Yellow)
        ElseIf PercentLessThanInitialDiff >= 0.75 Then
            ConsoleLibrary.SetColor(Console_Library.Console_Library.ConsoleColor.SkyBlueForte)
        ElseIf PercentLessThanInitialDiff >= 0.5 Then
            ConsoleLibrary.SetColor(Console_Library.Console_Library.ConsoleColor.SkyBlue)
        ElseIf PercentLessThanInitialDiff >= 0.1 Then
            ConsoleLibrary.SetColor(Console_Library.Console_Library.ConsoleColor.BlueForte)
        ElseIf PercentLessThanInitialDiff >= 0.05 Then
            ConsoleLibrary.SetColor(Console_Library.Console_Library.ConsoleColor.Blue)
        ElseIf PercentLessThanInitialDiff >= 0.03 Then
            ConsoleLibrary.SetColor(Console_Library.Console_Library.ConsoleColor.Purple)
        ElseIf PercentLessThanInitialDiff >= 0.01 Then
            ConsoleLibrary.SetColor(Console_Library.Console_Library.ConsoleColor.PurpleForte)
        ElseIf PercentLessThanInitialDiff >= 0.001 Then
            ConsoleLibrary.SetColor(Console_Library.Console_Library.ConsoleColor.Red)
        ElseIf PercentLessThanInitialDiff >= 0 Then
            ConsoleLibrary.SetColor(Console_Library.Console_Library.ConsoleColor.RedForte)
        Else
            ConsoleLibrary.SetColor(Console_Library.Console_Library.ConsoleColor.GreenForte)
        End If

    End Sub

    Public Sub OutputProgramInfo()
        ConsoleLibrary.SetColor(Console_Library.Console_Library.ConsoleColor.RedForte)
        Console.WriteLine("Backprop Process App - SP (2009)")
        Console.WriteLine("Created by: Paul Killian")
        Console.WriteLine()
        ConsoleLibrary.SetColor(Console_Library.Console_Library.ConsoleColor.WhiteForte)
    End Sub

End Module
