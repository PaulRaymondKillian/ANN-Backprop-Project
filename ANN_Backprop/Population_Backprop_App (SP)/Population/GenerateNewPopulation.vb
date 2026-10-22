''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'' Project: Artificial Neural Network Error Backwards-Propagation Project	    		''
'' Component: Generate New Population logic extension               					''
'' Description: This file is used to store methods pertaining to the specific genetic   ''
''              algorithm methods used to evolve a population into subsequent           ''
''              populations. The user of the Population object will be able to choose   ''
''              from 1 of the statically assigned new population generation methods.    ''
''																						''
'' Author: Paul Killian																	''
'' Contact: PaulKillian@DecodedCortex.com												''
'' Website: www.DecodedCortex.com														''
'' Disclaimer: Any non-commercial use of this code or a derivative therof must give     ''
''              credit to those who developed it (Decoded Cortex). Any commercial use of''
''              this code or the application themselves is now allowed unless authorized''
''              by Decoded Cortex.                                                      ''
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Module GenerateNewPopulation
    'It will be assumed the parameter populations will be sorted

    'Copy BestBackprop, Clone best 5/10, Sim Anneal 2 Cases for each best 1/5, Randomize Rest if any
    Public Function GenerateNewPopulation1(ByVal Population() As Backprop.Backprop, ByVal BackpropParms() As String, ByVal bestBackprop As Backprop.Backprop, ByVal exemplarInputs(,) As Double, ByVal exemplarOutputs(,) As Double, ByVal exemplarCount As Integer, ByVal countInputNeurons As Integer, ByVal countOutputNeurons As Integer) As Backprop.Backprop()
        Dim PopSize As Integer = Population.Length
        Dim intervalSize As Integer = Convert.ToInt32(PopSize / 10)
        Dim newPopulation(PopSize - 1) As Backprop.Backprop
        Dim currentIndex As Integer = 0
        Try
            Console.WriteLine("Generating New Population (#1)")
            'Check if Population Size is large enough for this type of new pop generation method
            If PopSize <= 10 Then
                Throw New Exception("Error Generating New Population #1 - Population Size Too Low: " & PopSize & ", must be >= 10")
            End If

            'Copy Best Backprop
            Dim copy As Backprop.Backprop = bestBackprop.Copy
            newPopulation(currentIndex) = copy
            Console.WriteLine("(" & currentIndex & ")Cloning Best Backprop:" & newPopulation(currentIndex).ID)
            currentIndex += 1

            'Clone the best 5/10 PopSize
            For i As Integer = 1 To 5 * intervalSize
                newPopulation(currentIndex) = Population(currentIndex)
                Console.WriteLine("(" & currentIndex & ")Cloning Best Half Population:" & newPopulation(currentIndex).ID)
                currentIndex += 1
            Next

            'Simulate 2 cases of annealing for the top 1/5 PopSize backprop objects
            For i As Integer = 1 To 2 * intervalSize
                For j As Integer = 1 To 2
                    copy = Population(i - 1).Copy
                    copy.InitiateSimulatedAnnealing()
                    copy.ID = copy.ID & "-SA" & j
                    newPopulation(currentIndex) = copy
                    Console.WriteLine("(" & currentIndex & ")Sim Anneal Best 1/5 Population:" & newPopulation(currentIndex).ID)
                    currentIndex += 1
                Next
            Next

            'Randomize Rest of the Population
            Do Until currentIndex > Population.Length - 1
                newPopulation(currentIndex) = New Backprop.Backprop(BackpropParms, exemplarInputs, exemplarOutputs, exemplarCount, countInputNeurons, countOutputNeurons)
                newPopulation(currentIndex).ID = "R" & currentIndex
                Console.WriteLine("(" & currentIndex & ")Random New Member:" & newPopulation(currentIndex).ID)
                currentIndex += 1
            Loop
            Console.WriteLine("New Population Generated!")
            Return newPopulation
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'Copy Best Backprop, Randomize Rest
    Public Function GenerateNewPopulation2(ByVal Population() As Backprop.Backprop, ByVal BackpropParms() As String, ByVal bestBackprop As Backprop.Backprop, ByVal exemplarInputs(,) As Double, ByVal exemplarOutputs(,) As Double, ByVal exemplarCount As Integer, ByVal countInputNeurons As Integer, ByVal countOutputNeurons As Integer) As Backprop.Backprop()
        Dim newPopulation(Population.Length - 1) As Backprop.Backprop
        Dim currentIndex As Integer = 0

        Try
            Console.WriteLine("Generating New Population (#2)")
            'Check if Population Size is large enough for this type of new pop generation method
            If Population.Length < 2 Then
                Throw New Exception("Error Generating New Population #2 - Population Size Too Low: " & Population.Length & ", must be > 1")
            End If

            'Copy Best Backprop
            Dim copy As Backprop.Backprop = bestBackprop.Copy
            newPopulation(currentIndex) = copy
            Console.WriteLine("(" & currentIndex & ")Cloning Best Backprop:" & newPopulation(currentIndex).ID)
            currentIndex += 1

            'Randomize Rest of the Population
            Do Until currentIndex > Population.Length - 1
                newPopulation(currentIndex) = Population(currentIndex)
                Console.WriteLine("(" & currentIndex & ")Cloning:" & newPopulation(currentIndex).ID)
                currentIndex += 1
            Loop
            Console.WriteLine("New Population Generated!")

            Return newPopulation

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'Copy Best Backprop, Sim Anneal 50 cases of Best Backprop, Randomize Rest if any
    Public Function GenerateNewPopulation3(ByVal Population() As Backprop.Backprop, ByVal BackpropParms() As String, ByVal bestBackprop As Backprop.Backprop, ByVal exemplarInputs(,) As Double, ByVal exemplarOutputs(,) As Double, ByVal exemplarCount As Integer, ByVal countInputNeurons As Integer, ByVal countOutputNeurons As Integer) As Backprop.Backprop()
        Dim newPopulation(Population.Length - 1) As Backprop.Backprop
        Dim currentIndex As Integer = 0

        Try
            'Check if Population Size is large enough for this type of new pop generation method
            If Population.Length < 51 Then
                Throw New Exception("Error Generating New Population #3 - Population Size Too Low: " & Population.Length & ", must be > 51")
            End If

            'Copy Best Backprop
            Dim copy As Backprop.Backprop = bestBackprop.Copy
            newPopulation(currentIndex) = copy
            currentIndex += 1

            'Simulate Anneal Best Backprop 50 times
            For j As Integer = 0 To 49
                newPopulation(currentIndex) = bestBackprop.Copy
                newPopulation(currentIndex).InitiateSimulatedAnnealing()
                newPopulation(currentIndex).ID = newPopulation(currentIndex).ID & "-SA" & j
                currentIndex += 1
            Next

            'Randomize Rest of the Population
            Do Until currentIndex > Population.Length - 1
                newPopulation(currentIndex) = New Backprop.Backprop(BackpropParms, exemplarInputs, exemplarOutputs, exemplarCount, countInputNeurons, countOutputNeurons)
                newPopulation(currentIndex).ID = "R" & currentIndex
                currentIndex += 1
            Loop

            Return newPopulation
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GenerateNewPopulation4(ByVal Population() As Backprop.Backprop, ByVal BackpropParms() As String, ByVal bestBackprop As Backprop.Backprop, ByVal exemplarInputs(,) As Double, ByVal exemplarOutputs(,) As Double, ByVal exemplarCount As Integer, ByVal countInputNeurons As Integer, ByVal countOutputNeurons As Integer) As Backprop.Backprop()
        Dim newPopulation(Population.Length - 1) As Backprop.Backprop
        Dim currentIndex As Integer = 0

        Try
            'Check if Population Size is large enough for this type of new pop generation method
            If Population.Length < 101 Then
                Throw New Exception("Error Generating New Population #4 - Population Size Too Low: " & Population.Length & ", must be > 100")
            End If

            'Copy Best Backprop
            Dim copy As Backprop.Backprop = bestBackprop.Copy
            newPopulation(currentIndex) = copy
            currentIndex += 1

            'Clone the best 30 Backprop Objects
            For i As Integer = 0 To 29
                newPopulation(currentIndex) = Population(currentIndex)
                currentIndex += 1
            Next

            'Simulate 2 cases of annealing for the top 30 backprop objects
            For i As Integer = 0 To 29
                For j As Integer = 0 To 1
                    newPopulation(currentIndex) = Population(i).Copy
                    newPopulation(currentIndex).InitiateSimulatedAnnealing()
                    newPopulation(currentIndex).ID = newPopulation(currentIndex).ID & "-SA" & j
                    currentIndex += 1
                Next
            Next

            'Randomize Rest of the Population
            Do Until currentIndex > Population.Length - 1
                newPopulation(currentIndex) = New Backprop.Backprop(BackpropParms, exemplarInputs, exemplarOutputs, exemplarCount, countInputNeurons, countOutputNeurons)
                newPopulation(currentIndex).ID = "R" & currentIndex
                currentIndex += 1
            Loop

            Return newPopulation
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GenerateNewPopulation5(ByVal Population() As Backprop.Backprop, ByVal BackpropParms() As String, ByVal bestBackprop As Backprop.Backprop, ByVal exemplarInputs(,) As Double, ByVal exemplarOutputs(,) As Double, ByVal exemplarCount As Integer, ByVal countInputNeurons As Integer, ByVal countOutputNeurons As Integer) As Backprop.Backprop()
        Dim newPopulation(Population.Length - 1) As Backprop.Backprop
        Dim currentIndex As Integer = 0

        Try
            'Check if Population Size is large enough for this type of new pop generation method
            If Population.Length < 1 Then
                Throw New Exception("Error Generating New Population #5 - Population Size Too Low: " & Population.Length & ", must be > 0")
            End If

            'Copy Best Backprop
            Dim copy As Backprop.Backprop = bestBackprop.Copy
            newPopulation(currentIndex) = copy
            currentIndex += 1

            'Clone entire population
            For i As Integer = currentIndex To Population.Length - 1
                newPopulation(currentIndex) = Population(currentIndex)
                currentIndex += 1
            Next

            Return newPopulation
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GenerateNewPopulation6(ByVal Population() As Backprop.Backprop, ByVal BackpropParms() As String, ByVal bestBackprop As Backprop.Backprop, ByVal exemplarInputs(,) As Double, ByVal exemplarOutputs(,) As Double, ByVal exemplarCount As Integer, ByVal countInputNeurons As Integer, ByVal countOutputNeurons As Integer) As Backprop.Backprop()
        Dim newPopulation(Population.Length - 1) As Backprop.Backprop
        Dim currentIndex As Integer = 0

        Try
            'Check if Population Size is large enough for this type of new pop generation method
            If Population.Length < 1 Then
                Throw New Exception("Error Generating New Population #6 - Population Size Too Low: " & Population.Length & ", must be > 0")
            End If

            'Copy Best Backprop
            Dim copy As Backprop.Backprop = bestBackprop.Copy
            newPopulation(currentIndex) = copy
            currentIndex += 1

            'Randomize Entire Population
            Do Until currentIndex > Population.Length - 1
                newPopulation(currentIndex) = New Backprop.Backprop(BackpropParms, exemplarInputs, exemplarOutputs, exemplarCount, countInputNeurons, countOutputNeurons)
                newPopulation(currentIndex).ID = "R" & currentIndex
                currentIndex += 1
            Loop

            Return newPopulation
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GenerateNewPopulation7(ByVal Population() As Backprop.Backprop, ByVal BackpropParms() As String, ByVal bestBackprop As Backprop.Backprop, ByVal exemplarInputs(,) As Double, ByVal exemplarOutputs(,) As Double, ByVal exemplarCount As Integer, ByVal countInputNeurons As Integer, ByVal countOutputNeurons As Integer) As Backprop.Backprop()
        Dim r As New Random(System.DateTime.Now.Millisecond)
        Try
            'Check if Population Size is large enough for this type of new pop generation method
            If Population.Length < 101 Then
                Throw New Exception("Error Generating New Population #7 - Population Size Too Low: " & Population.Length & ", must be > 100")
            End If

            Dim randNum As Integer = r.Next(1, 6)

            If randNum = 1 Then
                Return GenerateNewPopulation1(Population, BackpropParms, bestBackprop, exemplarInputs, exemplarOutputs, exemplarCount, countInputNeurons, countOutputNeurons)
            ElseIf randNum = 2 Then
                Return GenerateNewPopulation2(Population, BackpropParms, bestBackprop, exemplarInputs, exemplarOutputs, exemplarCount, countInputNeurons, countOutputNeurons)
            ElseIf randNum = 3 Then
                Return GenerateNewPopulation3(Population, BackpropParms, bestBackprop, exemplarInputs, exemplarOutputs, exemplarCount, countInputNeurons, countOutputNeurons)
            ElseIf randNum = 4 Then
                Return GenerateNewPopulation4(Population, BackpropParms, bestBackprop, exemplarInputs, exemplarOutputs, exemplarCount, countInputNeurons, countOutputNeurons)
            ElseIf randNum = 5 Then
                Return GenerateNewPopulation5(Population, BackpropParms, bestBackprop, exemplarInputs, exemplarOutputs, exemplarCount, countInputNeurons, countOutputNeurons)
            ElseIf randNum = 6 Then
                Return GenerateNewPopulation6(Population, BackpropParms, bestBackprop, exemplarInputs, exemplarOutputs, exemplarCount, countInputNeurons, countOutputNeurons)
            Else
                Throw New Exception("Error Generating New Population #7 - Random Number is not in range (1-6)")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Module
