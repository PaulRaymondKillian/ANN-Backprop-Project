Option Strict On

Imports System.IO

Public Class Population
    Private Const _CLASSNAME As String = "Population"
    Private BackpropParms(9) As String
    Private Population() As Backprop.Backprop
    Private PopulationSize As Integer
    Private Generations As Integer
    Private EpochsPerGeneration As Integer
    Private NewGenerationMethod As Integer
    Private SaveDirectory As String
    Private GenSaveInterval As Integer
    Private FilePrefix As String
    Private BestBackprop As Backprop.Backprop
    Private BestBackpropFitness() As Double
    Private AvgGenerationFitness() As Double
    Private Started As DateTime
    Private VerboseLogging As Boolean

    Dim ConsoleLibrary As New Console_Library.Console_Library   'Used to change text color in console

    Public Sub New(ByVal filepath As String)
        Dim _METHODNAME As String = "New(filePath)"
        Console.WriteLine("Creating Population...")

        importPopFile(filepath)
        Started = Now
        ReDim Population(PopulationSize - 1)
        Try
            'Initialize Backprop Objects of Population
            Console.Write(PopulationSize & " Total: ")
            For i As Integer = 0 To PopulationSize - 1
                Console.Write(i & " ")
                Population(i) = New Backprop.Backprop(BackpropParms)
                Population(i).ID = i.ToString
            Next

            'Set BestBackprop object to its initial value which will be over-written later
            BestBackprop = Population(0).Copy
            BestBackprop.Fitness = Double.MaxValue  'Set to max value so it will always be overwritten
            Console.WriteLine()
            Console.WriteLine("Population Created!")

            RunGenerations()

        Catch ex As Exception
            Throw New Exception("Error - Class: " & _CLASSNAME & " Method: " & _METHODNAME & " Descr: Error creating population; input file may be invalid or corrupt.")
        End Try
    End Sub

    'Public Sub New(ByVal BackpropParms() As String, ByVal PopSize As Integer)
    '    Dim _METHODNAME As String = "New(InputFile,PopSize)"
    '    _BackpropParms = BackpropParms
    '    If PopSize <= 0 Then
    '        Throw New Exception("Error - Class: Population, Method: New(), Desc: Invalid PopulationSize parameter, must be greater than zero")
    '        Exit Sub
    '    End If
    '    Started = Now
    '    PopulationSize = PopSize
    '    ReDim Population(PopulationSize - 1)
    '    Try
    '        For i As Integer = 0 To PopulationSize - 1
    '            Population(i) = New Backprop.Backprop(_BackpropParms)
    '            Population(i).ID = i
    '        Next
    '        'Set BestBackprop object to its initial value which will be over-written later
    '        BestBackprop = Population(0).Copy
    '        BestBackprop.Fitness = Double.MaxValue  'Set to max value so it will always be overwritten
    '    Catch ex As Exception
    '        Throw New Exception("Error - Class: " & _CLASSNAME & " Method: " & _METHODNAME & " Descr: Error creating population; input file may be invalid or corrupt.")
    '    End Try
    'End Sub

    Public Sub RunPopulation(ByVal Epochs As Integer)
        Dim previousFitness As Double
        Dim currentFitness As Double
        For i As Integer = 0 To PopulationSize - 1
            Population(i).calcFitness()
            previousFitness = Population(i).Fitness
            ConsoleLibrary.SetColor(Console_Library.Console_Library.ConsoleColor.Gray)
            Population(i).Train(Epochs, VerboseLogging)
            Population(i).calcFitness()
            currentFitness = Population(i).Fitness
            If currentFitness - previousFitness < 0 Then
                ConsoleLibrary.SetColor(Console_Library.Console_Library.ConsoleColor.Green)
            ElseIf currentFitness - previousFitness > 0 Then
                ConsoleLibrary.SetColor(Console_Library.Console_Library.ConsoleColor.Red)
            Else
                ConsoleLibrary.SetColor(Console_Library.Console_Library.ConsoleColor.White)
            End If
            Console.WriteLine(Population(i).ID & ": " & currentFitness & "   PrvFit: " & previousFitness & "    Diff: " & currentFitness - previousFitness & " Avg: " & currentFitness / Population(i).exemplarCount)

        Next
    End Sub

    'Public Sub CalculatePopulationFitness()
    '    ConsoleLibrary.SetColor(Console_Library.Console_Library.ConsoleColor.White)
    '    Console.WriteLine("Calculating Population Fitnesses")
    '    For i As Integer = 0 To PopulationSize - 1
    '        Population(i).calcFitness()
    '        Console.WriteLine(Population(i).ID & ": " & Population(i).Fitness)
    '    Next
    'End Sub

    Public Sub SortPopulation()
        Dim sortedPopulation(PopulationSize - 1) As Backprop.Backprop
        Dim sortedArray(PopulationSize - 1) As Boolean  'Set to false by default
        Dim bestFitnessValue As Double
        Dim bestFitnessIndex As Integer = -1

        'Selection Sort Algorithm
        For i As Integer = 0 To PopulationSize - 1
            bestFitnessValue = Double.MaxValue
            For j As Integer = 0 To PopulationSize - 1
                If Population(j).Fitness < bestFitnessValue And Not sortedArray(j) Then
                    bestFitnessValue = Population(j).Fitness
                    bestFitnessIndex = j
                End If
            Next
            sortedPopulation(i) = Population(bestFitnessIndex)
            sortedArray(bestFitnessIndex) = True
        Next

        Population = sortedPopulation
    End Sub

    Public Sub NewPopulation(ByVal GenerationMethod As Integer)
        Try
            If GenerationMethod = 1 Then
                Population = GenerateNewPopulation1(Population, BackpropParms, BestBackprop)
            ElseIf GenerationMethod = 2 Then
                Population = GenerateNewPopulation2(Population, BackpropParms, BestBackprop)
            ElseIf GenerationMethod = 3 Then
                Population = GenerateNewPopulation3(Population, BackpropParms, BestBackprop)
            ElseIf GenerationMethod = 4 Then
                Population = GenerateNewPopulation4(Population, BackpropParms, BestBackprop)
            ElseIf GenerationMethod = 5 Then
                Population = GenerateNewPopulation5(Population, BackpropParms, BestBackprop)
            ElseIf GenerationMethod = 6 Then
                Population = GenerateNewPopulation6(Population, BackpropParms, BestBackprop)
            ElseIf GenerationMethod = 7 Then
                Population = GenerateNewPopulation7(Population, BackpropParms, BestBackprop)
            Else
                Throw New Exception("Error - Class: " & _CLASSNAME & ", Method: NewPopulation(int), Desc: Invalid Value for New Generation Method: " & GenerationMethod)
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Sub RunGeneration(ByVal EpochsToRun As Integer, ByVal NewGenerationMethod As Integer, ByVal GenerateNewPopulation As Boolean)
        RunPopulation(EpochsToRun)
        SortPopulation()
        RetainBestBackprop()
        If GenerateNewPopulation Then
            NewPopulation(NewGenerationMethod)
        End If
    End Sub

    Public Sub RunGenerations()

        ReDim BestBackpropFitness(Generations - 1)
        ReDim AvgGenerationFitness(Generations)

        Console.WriteLine("Calculating Initial Average Population Fitness...")
        AvgGenerationFitness(0) = AveragePopulationFitness()
        Console.WriteLine("Initial Average Population Fitness Calculated!")

        For i As Integer = 0 To Generations - 1
            ConsoleLibrary.SetColor(Console_Library.Console_Library.ConsoleColor.Yellow)
            Console.WriteLine("Generation: " & i & "    Best-Rho: " & Population(0).rho & "  BestBackprop: " & BestBackprop.Fitness)
            If i < Generations - 1 Then
                RunGeneration(EpochsPerGeneration, NewGenerationMethod, True)
            Else
                RunGeneration(EpochsPerGeneration, NewGenerationMethod, False)
            End If
            BestBackpropFitness(i) = BestBackprop.Fitness

            Console.WriteLine("Calculating Average Population Fitness...")
            AvgGenerationFitness(i + 1) = AveragePopulationFitness()
            Console.WriteLine("Average Population Fitness Calculated!")

            'Save the Population Data if Save Interval 
            If i = Generations - 1 Then
                SavePopData(True)
            ElseIf i Mod GenSaveInterval = 0 Then
                SavePopData(False)
            End If
        Next
    End Sub

    Public Sub importPopFile(ByVal filepath As String)
        Dim sr As StreamReader
        Dim line As String
        Dim readLine() As String
        Try
            sr = File.OpenText(filepath)

            sr.ReadLine()   'Buffer

            line = sr.ReadLine
            readLine = line.Split("="c)
            If readLine.Count <> 2 Then
                Throw New Exception("Error - ANN_Fast_Backprop_PopulationApp    Descr: Invalid contents of input file. Inputs and DesiredOutput should be delimited by a single '=' character")
                Exit Sub
            End If
            BackpropParms(0) = readLine(1)  'Dats Set File Location


            sr.ReadLine()   'Buffer
            sr.ReadLine()   'Buffer

            line = sr.ReadLine
            readLine = line.Split("="c)
            If readLine.Count <> 2 Then
                Throw New Exception("Error - ANN_Fast_Backprop_PopulationApp    Descr: Invalid contents of input file. Inputs and DesiredOutput should be delimited by a single '=' character")
                Exit Sub
            End If
            PopulationSize = (Convert.ToInt32(readLine(1)))

            line = sr.ReadLine
            readLine = line.Split("="c)
            If readLine.Count <> 2 Then
                Throw New Exception("Error - ANN_Fast_Backprop_PopulationApp    Descr: Invalid contents of input file. Inputs and DesiredOutput should be delimited by a single '=' character")
                Exit Sub
            End If
            Generations = Convert.ToInt32((readLine(1)))

            line = sr.ReadLine
            readLine = line.Split("="c)
            If readLine.Count <> 2 Then
                Throw New Exception("Error - ANN_Fast_Backprop_PopulationApp    Descr: Invalid contents of input file. Inputs and DesiredOutput should be delimited by a single '=' character")
                Exit Sub
            End If
            EpochsPerGeneration = Convert.ToInt32((readLine(1)))

            line = sr.ReadLine
            readLine = line.Split("="c)
            If readLine.Count <> 2 Then
                Throw New Exception("Error - ANN_Fast_Backprop_PopulationApp    Descr: Invalid contents of input file. Inputs and DesiredOutput should be delimited by a single '=' character")
                Exit Sub
            End If
            NewGenerationMethod = Convert.ToInt32((readLine(1)))

            sr.ReadLine()   'Buffer
            sr.ReadLine()   'Buffer

            line = sr.ReadLine
            readLine = line.Split("="c)
            If readLine.Count <> 2 Then
                Throw New Exception("Error - ANN_Fast_Backprop_PopulationApp    Descr: Invalid contents of input file. Inputs and DesiredOutput should be delimited by a single '=' character")
                Exit Sub
            End If
            BackpropParms(1) = readLine(1)  'Hidden Neurons

            line = sr.ReadLine
            readLine = line.Split("="c)
            If readLine.Count <> 2 Then
                Throw New Exception("Error - ANN_Fast_Backprop_PopulationApp    Descr: Invalid contents of input file. Inputs and DesiredOutput should be delimited by a single '=' character")
                Exit Sub
            End If
            BackpropParms(2) = readLine(1)  'Hidden Layers

            sr.ReadLine()   'Buffer
            sr.ReadLine()   'Buffer

            line = sr.ReadLine
            readLine = line.Split("="c)
            If readLine.Count <> 2 Then
                Throw New Exception("Error - ANN_Fast_Backprop_PopulationApp    Descr: Invalid contents of input file. Inputs and DesiredOutput should be delimited by a single '=' character")
                Exit Sub
            End If
            BackpropParms(3) = readLine(1)  'Max Weight Value

            line = sr.ReadLine
            readLine = line.Split("="c)
            If readLine.Count <> 2 Then
                Throw New Exception("Error - ANN_Fast_Backprop_PopulationApp    Descr: Invalid contents of input file. Inputs and DesiredOutput should be delimited by a single '=' character")
                Exit Sub
            End If
            BackpropParms(4) = readLine(1)  'Max Bias Value

            line = sr.ReadLine
            readLine = line.Split("="c)
            If readLine.Count <> 2 Then
                Throw New Exception("Error - ANN_Fast_Backprop_PopulationApp    Descr: Invalid contents of input file. Inputs and DesiredOutput should be delimited by a single '=' character")
                Exit Sub
            End If
            BackpropParms(5) = readLine(1)  'Rho

            line = sr.ReadLine
            readLine = line.Split("="c)
            If readLine.Count <> 2 Then
                Throw New Exception("Error - ANN_Fast_Backprop_PopulationApp    Descr: Invalid contents of input file. Inputs and DesiredOutput should be delimited by a single '=' character")
                Exit Sub
            End If
            BackpropParms(6) = readLine(1)  'Rho Multiplier

            line = sr.ReadLine
            readLine = line.Split("="c)
            If readLine.Count <> 2 Then
                Throw New Exception("Error - ANN_Fast_Backprop_PopulationApp    Descr: Invalid contents of input file. Inputs and DesiredOutput should be delimited by a single '=' character")
                Exit Sub
            End If
            BackpropParms(7) = readLine(1)  'Dynamic Rho Interval

            line = sr.ReadLine
            readLine = line.Split("="c)
            If readLine.Count <> 2 Then
                Throw New Exception("Error - ANN_Fast_Backprop_PopulationApp    Descr: Invalid contents of input file. Inputs and DesiredOutput should be delimited by a single '=' character")
                Exit Sub
            End If
            BackpropParms(8) = readLine(1)  'Momentum Scaler

            line = sr.ReadLine
            readLine = line.Split("="c)
            If readLine.Count <> 2 Then
                Throw New Exception("Error - ANN_Fast_Backprop_PopulationApp    Descr: Invalid contents of input file. Inputs and DesiredOutput should be delimited by a single '=' character")
                Exit Sub
            End If
            BackpropParms(9) = readLine(1)  'Sim Annealing Max Percent

            sr.ReadLine()   'Buffer
            sr.ReadLine()   'Buffer

            line = sr.ReadLine
            readLine = line.Split("="c)
            If readLine.Count <> 2 Then
                Throw New Exception("Error - ANN_Fast_Backprop_PopulationApp    Descr: Invalid contents of input file. Inputs and DesiredOutput should be delimited by a single '=' character")
                Exit Sub
            End If
            SaveDirectory = (readLine(1))

            line = sr.ReadLine
            readLine = line.Split("="c)
            If readLine.Count <> 2 Then
                Throw New Exception("Error - ANN_Fast_Backprop_PopulationApp    Descr: Invalid contents of input file. Inputs and DesiredOutput should be delimited by a single '=' character")
                Exit Sub
            End If
            GenSaveInterval = Convert.ToInt32(readLine(1))

            line = sr.ReadLine
            readLine = line.Split("="c)
            If readLine.Count <> 2 Then
                Throw New Exception("Error - ANN_Fast_Backprop_PopulationApp    Descr: Invalid contents of input file. Inputs and DesiredOutput should be delimited by a single '=' character")
                Exit Sub
            End If
            FilePrefix = (readLine(1))

            sr.ReadLine()   'Buffer
            sr.ReadLine()   'Buffer

            line = sr.ReadLine
            readLine = line.Split("="c)
            If readLine.Count <> 2 Then
                Throw New Exception("Error - ANN_Fast_Backprop_PopulationApp    Descr: Invalid contents of input file. Inputs and DesiredOutput should be delimited by a single '=' character")
                Exit Sub
            End If
            If readLine(1).ToUpper = "TRUE" Or readLine(1).ToUpper = "T" Then
                VerboseLogging = True
            Else
                VerboseLogging = False
            End If

        Catch ex As Exception
            Throw New Exception("Error - Population File is Invalid or Corrupt")
        End Try
    End Sub

    Public Sub SavePopData(ByVal OutputToConsole As Boolean)
        ConsoleLibrary.SetColor(Console_Library.Console_Library.ConsoleColor.White)

        BestBackprop.calcFitness()
        Console.WriteLine("Saving data to: " & SaveDirectory & "\" & FilePrefix & "\")
        Try
            Directory.CreateDirectory(SaveDirectory & "\" & FilePrefix)
            If OutputToConsole Then
                OutputPopulationData(SaveDirectory & "\" & FilePrefix & "\ProcessOutput.txt")
            End If
            BestBackprop.outputNetwork(SaveDirectory & "\" & FilePrefix & "\" & FilePrefix & "_Network.ANN")
            BestBackprop.outputNetworkOutput(SaveDirectory & "\" & FilePrefix & "\" & FilePrefix & "_Output.txt")
            WritePOPFile(SaveDirectory & "\" & FilePrefix & "\" & FilePrefix & ".POP")
        Catch ex As Exception

        End Try
        Console.WriteLine("Data Saved Successfully!")
    End Sub

    Public Sub RetainBestBackprop()
        Population(0).calcFitness()
        BestBackprop.calcFitness()
        If Population(0).Fitness < BestBackprop.Fitness Or Double.IsNaN(BestBackprop.Fitness) Then
            BestBackprop = Population(0).Copy
            BestBackprop.ID = BestBackprop.ID & "-B"
            BestBackprop.calcFitness()
        End If
    End Sub

    Public Function AveragePopulationFitness() As Double
        Dim totalFitness As Double = 0
        For i As Integer = 0 To PopulationSize - 1
            Population(i).calcFitness()
            totalFitness += Population(i).Fitness
        Next
        Return (totalFitness / PopulationSize)
    End Function

    Public Sub WritePOPFile(ByVal filePath As String)
        Dim sw As New StreamWriter(filePath)
        sw.WriteLine("### Data Set ###")
        sw.WriteLine("Dataset file location=" & BackpropParms(0))
        sw.WriteLine("")
        sw.WriteLine("### Population Configuration ###")
        sw.WriteLine("Population Size=" & PopulationSize)
        sw.WriteLine("Generations=" & Generations)
        sw.WriteLine("Epochs Per Generation=" & EpochsPerGeneration)
        sw.WriteLine("New Generation Method=" & NewGenerationMethod)
        sw.WriteLine("")
        sw.WriteLine("### Network Topology ###")
        sw.WriteLine("Hidden Neurons=" & BackpropParms(1))
        sw.WriteLine("Hidden Layers=" & BackpropParms(2))
        sw.WriteLine("")
        sw.WriteLine("### Training Configuation ###")
        sw.WriteLine("Max weight value=" & BackpropParms(3))
        sw.WriteLine("Max bias value =" & BackpropParms(4))
        sw.WriteLine("Initial rho value=" & BackpropParms(5))
        sw.WriteLine("Rho multiplier=" & BackpropParms(6))
        sw.WriteLine("Dynamic rho interval=" & BackpropParms(7))
        sw.WriteLine("Momentum Scaler=" & BackpropParms(8))
        sw.WriteLine("Simulated annealing max percent(%)=" & BackpropParms(9))
        sw.WriteLine("")
        sw.WriteLine("### Save Data Settings ###")
        sw.WriteLine("Save Directory=" & SaveDirectory)
        sw.WriteLine("Generation Save Interval=" & GenSaveInterval)
        sw.WriteLine("File Prefix=" & FilePrefix)
        sw.Close()
    End Sub

    Public Sub OutputPopulationData(ByVal FilePath As String)
        Dim Finished As DateTime
        Finished = Now
        Dim TimeElapsed As TimeSpan
        TimeElapsed = Finished - Started

        Dim sw As New StreamWriter(FilePath)
        sw.WriteLine("ANN_Fast_Backprop_Dynamic_Population")
        sw.WriteLine()
        Console.WriteLine("BestBackprop ID: " & BestBackprop.ID & "   BestBackprop Fitness:" & BestBackprop.Fitness)
        sw.WriteLine("BestBackprop ID: " & BestBackprop.ID & "   BestBackprop Fitness:" & BestBackprop.Fitness)
        sw.WriteLine()
        Console.WriteLine("Initial Population Avg Fitness: " & AvgGenerationFitness(0))
        sw.WriteLine("Initial Population Avg Fitness: " & AvgGenerationFitness(0))
        For i As Integer = 0 To BestBackpropFitness.Length - 1
            Console.WriteLine("Best Backprop Fitness Generation " & i & ": " & BestBackpropFitness(i) & "   AvgFitness: " & AvgGenerationFitness(i + 1))
            sw.WriteLine("Best Backprop Fitness Generation " & i & ": " & BestBackpropFitness(i) & "   AvgFitness: " & AvgGenerationFitness(i + 1))
        Next
        sw.WriteLine()
        Console.WriteLine("Processed In: " & Convert.ToInt32(TimeElapsed.Hours) & " Hours, " & Convert.ToInt32(TimeElapsed.Minutes) & " Minutes, " & Convert.ToInt32(TimeElapsed.Seconds) & " Seconds.")
        sw.WriteLine("Processed In: " & Convert.ToInt32(TimeElapsed.Hours) & " Hours, " & Convert.ToInt32(TimeElapsed.Minutes) & " Minutes, " & Convert.ToInt32(TimeElapsed.Seconds) & " Seconds.")

        sw.Close()
    End Sub

End Class
