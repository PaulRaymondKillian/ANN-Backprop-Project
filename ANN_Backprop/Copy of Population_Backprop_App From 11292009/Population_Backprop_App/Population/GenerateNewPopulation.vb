Module GenerateNewPopulation
    'It will be assumed the parameter populations will be sorted

    Public Function GenerateNewPopulation1(ByVal Population() As Backprop.Backprop, ByVal BackpropParms() As String, ByVal bestBackprop As Backprop.Backprop) As Backprop.Backprop()
        Dim PopSize As Integer = Population.Length
        Dim intervalSize As Integer = PopSize / 20
        Dim newPopulation(PopSize - 1) As Backprop.Backprop
        Dim currentIndex As Integer = 0
        Try
            'Check if Population Size is large enough for this type of new pop generation method
            If PopSize < 21 Then
                Throw New Exception("Error Generating New Population #1 - Population Size Too Low: " & PopSize & ", must be > 21")
            End If

            'Copy Best Backprop
            Dim copy As Backprop.Backprop = bestBackprop.Copy
            newPopulation(currentIndex) = copy
            currentIndex += 1

            'Simulate Anneal 1/20 PopSize of Best Backprop
            For i As Integer = 1 To intervalSize
                copy = bestBackprop.Copy
                copy.InitiateSimulatedAnnealing()
                copy.ID = copy.ID & "-BSA" & i
                newPopulation(currentIndex) = copy
                currentIndex += 1
            Next

            'Clone the best 2/20 PopSize
            For i As Integer = 1 To 2 * intervalSize
                newPopulation(currentIndex) = Population(currentIndex)
                currentIndex += 1
            Next

            'Simulate 1/20 PopSize cases of annealing for the top 2/20 PopSize backprop objects
            For i As Integer = 1 To 2 * intervalSize
                For j As Integer = 1 To intervalSize
                    copy = Population(i - 1).Copy
                    copy.InitiateSimulatedAnnealing()
                    copy.ID = copy.ID & "-SA" & j
                    newPopulation(currentIndex) = copy
                    currentIndex += 1
                Next
            Next

            'Randomize Rest of the Population
            Do Until currentIndex > Population.Length - 1
                newPopulation(currentIndex) = New Backprop.Backprop(BackpropParms)
                newPopulation(currentIndex).ID = "R" & currentIndex
                currentIndex += 1
            Loop

            Return newPopulation
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GenerateNewPopulation2(ByVal Population() As Backprop.Backprop, ByVal BackpropParms() As String, ByVal bestBackprop As Backprop.Backprop) As Backprop.Backprop()
        Dim newPopulation(Population.Length - 1) As Backprop.Backprop
        Dim currentIndex As Integer = 0

        Try
            'Check if Population Size is large enough for this type of new pop generation method
            If Population.Length < 91 Then
                Throw New Exception("Error Generating New Population #2 - Population Size Too Low: " & Population.Length & ", must be > 91")
            End If

            'Copy Best Backprop
            Dim copy As Backprop.Backprop = bestBackprop.Copy
            newPopulation(currentIndex) = copy
            currentIndex += 1

            'Clone the best 20 Backprop Objects
            For i As Integer = 0 To 19
                newPopulation(currentIndex) = Population(currentIndex)
                currentIndex += 1
            Next

            'Simulate 3 cases of annealing for the top 20 backprop objects
            For i As Integer = 0 To 19
                For j As Integer = 0 To 2
                    newPopulation(currentIndex) = Population(i).Copy
                    newPopulation(currentIndex).InitiateSimulatedAnnealing()
                    newPopulation(currentIndex).ID = newPopulation(currentIndex).ID & "-SA" & j
                    currentIndex += 1
                Next
            Next

            'Randomize Rest of the Population
            Do Until currentIndex > Population.Length - 1
                newPopulation(currentIndex) = New Backprop.Backprop(BackpropParms)
                newPopulation(currentIndex).ID = "R" & currentIndex
                currentIndex += 1
            Loop

            Return newPopulation

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GenerateNewPopulation3(ByVal Population() As Backprop.Backprop, ByVal BackpropParms() As String, ByVal bestBackprop As Backprop.Backprop) As Backprop.Backprop()
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

            ''Removed Below Code, appears to be doing essentially the same thing as the copy best backprop code above
            ''Clone the best 1 Backprop Objects
            'For i As Integer = 0 To 0
            '    newPopulation(currentIndex) = Population(currentIndex)
            '    currentIndex += 1
            'Next

            'Simulate Anneal Best Backprop 50 times
            For j As Integer = 0 To 49
                newPopulation(currentIndex) = bestBackprop.Copy
                newPopulation(currentIndex).InitiateSimulatedAnnealing()
                newPopulation(currentIndex).ID = newPopulation(currentIndex).ID & "-SA" & j
                currentIndex += 1
            Next

            'Randomize Rest of the Population
            Do Until currentIndex > Population.Length - 1
                newPopulation(currentIndex) = New Backprop.Backprop(BackpropParms)
                newPopulation(currentIndex).ID = "R" & currentIndex
                currentIndex += 1
            Loop

            Return newPopulation
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GenerateNewPopulation4(ByVal Population() As Backprop.Backprop, ByVal BackpropParms() As String, ByVal bestBackprop As Backprop.Backprop) As Backprop.Backprop()
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

            'Simulate 3 cases of annealing for the top 30 backprop objects
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
                newPopulation(currentIndex) = New Backprop.Backprop(BackpropParms)
                newPopulation(currentIndex).ID = "R" & currentIndex
                currentIndex += 1
            Loop

            Return newPopulation
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GenerateNewPopulation5(ByVal Population() As Backprop.Backprop, ByVal BackpropParms() As String, ByVal bestBackprop As Backprop.Backprop) As Backprop.Backprop()
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

            Do Until currentIndex > Population.Length - 1
                newPopulation(currentIndex) = bestBackprop.Copy
                newPopulation(currentIndex).InitiateSimulatedAnnealing()
                newPopulation(currentIndex).ID = newPopulation(currentIndex).ID & "-SA" & currentIndex
                currentIndex += 1
            Loop

            Return newPopulation
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GenerateNewPopulation6(ByVal Population() As Backprop.Backprop, ByVal BackpropParms() As String, ByVal bestBackprop As Backprop.Backprop) As Backprop.Backprop()
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
                newPopulation(currentIndex) = New Backprop.Backprop(BackpropParms)
                newPopulation(currentIndex).ID = "R" & currentIndex
                currentIndex += 1
            Loop

            Return newPopulation
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GenerateNewPopulation7(ByVal Population() As Backprop.Backprop, ByVal BackpropParms() As String, ByVal bestBackprop As Backprop.Backprop) As Backprop.Backprop()
        Dim r As New Random(System.DateTime.Now.Millisecond)
        Try
            'Check if Population Size is large enough for this type of new pop generation method
            If Population.Length < 101 Then
                Throw New Exception("Error Generating New Population #7 - Population Size Too Low: " & Population.Length & ", must be > 100")
            End If

            Dim randNum As Integer = r.Next(1, 6)

            If randNum = 1 Then
                Return GenerateNewPopulation1(Population, BackpropParms, bestBackprop)
            ElseIf randNum = 2 Then
                Return GenerateNewPopulation2(Population, BackpropParms, bestBackprop)
            ElseIf randNum = 3 Then
                Return GenerateNewPopulation3(Population, BackpropParms, bestBackprop)
            ElseIf randNum = 4 Then
                Return GenerateNewPopulation4(Population, BackpropParms, bestBackprop)
            ElseIf randNum = 5 Then
                Return GenerateNewPopulation5(Population, BackpropParms, bestBackprop)
            ElseIf randNum = 6 Then
                Return GenerateNewPopulation6(Population, BackpropParms, bestBackprop)
            Else
                Throw New Exception("Error Generating New Population #7 - Random Number is not in range (1-6)")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Module
