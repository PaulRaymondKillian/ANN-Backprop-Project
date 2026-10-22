Option Strict On

Imports System.IO

Public Class Backprop

    Public ID As String 'Optional String To Use to Identifier the specific object

    'Const Values
    Const VALUE As Integer = 0
    Const BIAS As Integer = 1
    Const DELTA As Integer = 2

    Public DataFileName As String
    Public _Parms() As String

    'Network Structure Variables
    Public countInputNeurons As Integer
    Public countHiddenNeurons As Integer
    Public countHiddenLayers As Integer
    Public countOutputNeurons As Integer
    Public epochs As Integer

    Public outputInterval As Integer
    Public outputDeltaInterval As Integer

    'Create Exemplar Set
    Public exemplarCount As Integer
    Dim exemplarInputs(,) As Double
    Dim exemplarOutputs(,) As Double

    Dim randomizedIndex() As Integer

    'Dynamic Rho Variables
    Public rho As Double
    Public rhoInterval As Integer
    Public rhoMultiplier As Double

    'Neuron arrays
    Public inputValues() As Double
    Public hiddenValues(,,) As Double
    Public outputValues(,) As Double

    'Weight arrays
    Public ihWeights(,,) As Double 'Increased dimension of weight array to include CURRENT/PREVIOUS weights (Momentum)
    Public hhWeights(,,,) As Double 'Increased dimension of weight array to include CURRENT/PREVIOUS weights (Momentum)
    Public hoWeights(,,) As Double 'Increased dimension of weight array to include CURRENT/PREVIOUS weights (Momentum)
    Public maxWeightValue As Double
    Public maxBiasValue As Double

    'Momentum Related Terms
    Public UseMomentum As Boolean = False   'Set the false by default
    Public momentumScaler As Double = 0     'Set to 0 by default
    Const CURRENT As Integer = 0
    Const PREVIOUS As Integer = 1

    'Simulated Annealing Terms
    Public annealingMaxPercent As Integer

    'Interval Value When Program Should Ask If You Want to Save (Used for learning tracking)
    Public SaveInterval As Integer
    Public CurrentDirectory As String
    Public FileTitle As String

    'Fitness Variable to determine accuracy/quality of backprop network
    Public Fitness As Double = 0

    'Store ANN File location used for BPROP output file
    Private ANNFileName As String

    Public Sub New(ByVal Parms() As String, ByRef pExemplarInputs(,) As Double, ByRef pExemplarOutputs(,) As Double, ByVal pExemplarCount As Integer, ByVal pCountInputNeurons As Integer, ByVal pCountOutputNeurons As Integer)
        _Parms = Parms
        importBackpropParms(_Parms)
        exemplarInputs = pExemplarInputs
        exemplarOutputs = pExemplarOutputs
        exemplarCount = pExemplarCount
        countInputNeurons = pCountInputNeurons
        countOutputNeurons = pCountOutputNeurons

        InitializeArrays()
        RandomizeWeightAndBiasArrays()

    End Sub

    Public Sub New(ByVal filepath As String)
        DeconstructCurrentDirectory(filepath)
        'Import Backprop Information
        importBackpropFile(filepath)
        'Import and Setup Exemplar datasets, input/output neuron counts
        importDataSet(DataFileName)
        'Import hidden and layer counts, initialize arrays, and set weight and bias values of network
        importANNFile(ANNFileName)
    End Sub
    

    Public Sub importBackpropParms(ByVal Parms() As String)

        DataFileName = Parms(0)
        countHiddenNeurons = Convert.ToInt32(Parms(1))
        countHiddenLayers = Convert.ToInt32(Parms(2))
        maxWeightValue = Convert.ToDouble(Parms(3))
        maxBiasValue = Convert.ToDouble(Parms(4))
        rho = Convert.ToDouble(Parms(5))
        rhoMultiplier = Convert.ToDouble(Parms(6))
        rhoInterval = Convert.ToInt32(Parms(7))
        UseMomentum = Convert.ToBoolean(Parms(8))
        momentumScaler = Convert.ToDouble(Parms(9))
        annealingMaxPercent = Convert.ToInt32(Parms(10))

    End Sub

    Public Sub importBackpropFile(ByVal filepath As String)
        Dim line As String
        Dim readLine() As String
        Dim sr As StreamReader

        Try
            sr = File.OpenText(filepath)

            sr.ReadLine()   'Buffer

            line = sr.ReadLine
            readLine = line.Split("="c)
            If readLine.Count <> 2 Then
                Throw New Exception("Error - ANN_Fast_Backprop_PopulationApp    Descr: Invalid contents of input file. Inputs and DesiredOutput should be delimited by a single '=' character")
                Exit Sub
            End If
            ID = readLine(1)  'Dats Set File Location
            'ID = sr.ReadLine

            line = sr.ReadLine
            readLine = line.Split("="c)
            If readLine.Count <> 2 Then
                Throw New Exception("Error - ANN_Fast_Backprop_PopulationApp    Descr: Invalid contents of input file. Inputs and DesiredOutput should be delimited by a single '=' character")
                Exit Sub
            End If
            FileTitle = readLine(1)  'Dats Set File Location
            'FileTitle = sr.ReadLine


            sr.ReadLine()   'Buffer
            sr.ReadLine()   'Buffer


            line = sr.ReadLine
            readLine = line.Split("="c)
            If readLine.Count <> 2 Then
                Throw New Exception("Error - ANN_Fast_Backprop_PopulationApp    Descr: Invalid contents of input file. Inputs and DesiredOutput should be delimited by a single '=' character")
                Exit Sub
            End If
            DataFileName = readLine(1)  'Dats Set File Location
            'DataFileName = sr.ReadLine

            line = sr.ReadLine
            readLine = line.Split("="c)
            If readLine.Count <> 2 Then
                Throw New Exception("Error - ANN_Fast_Backprop_PopulationApp    Descr: Invalid contents of input file. Inputs and DesiredOutput should be delimited by a single '=' character")
                Exit Sub
            End If
            ANNFileName = readLine(1)  'Dats Set File Location
            'ANNFileName = sr.ReadLine


            sr.ReadLine()   'Buffer
            sr.ReadLine()   'Buffer


            line = sr.ReadLine
            readLine = line.Split("="c)
            If readLine.Count <> 2 Then
                Throw New Exception("Error - ANN_Fast_Backprop_PopulationApp    Descr: Invalid contents of input file. Inputs and DesiredOutput should be delimited by a single '=' character")
                Exit Sub
            End If
            rho = Convert.ToDouble(readLine(1))  'Dats Set File Location
            'rho = Convert.ToDouble(sr.ReadLine)

            line = sr.ReadLine
            readLine = line.Split("="c)
            If readLine.Count <> 2 Then
                Throw New Exception("Error - ANN_Fast_Backprop_PopulationApp    Descr: Invalid contents of input file. Inputs and DesiredOutput should be delimited by a single '=' character")
                Exit Sub
            End If
            rhoMultiplier = Convert.ToDouble(readLine(1))  'Dats Set File Location
            'rhoMultiplier = Convert.ToDouble(sr.ReadLine)

            line = sr.ReadLine
            readLine = line.Split("="c)
            If readLine.Count <> 2 Then
                Throw New Exception("Error - ANN_Fast_Backprop_PopulationApp    Descr: Invalid contents of input file. Inputs and DesiredOutput should be delimited by a single '=' character")
                Exit Sub
            End If
            rhoInterval = Convert.ToInt32(readLine(1))  'Dats Set File Location
            'rhoInterval = Convert.ToInt32(sr.ReadLine)

            line = sr.ReadLine
            readLine = line.Split("="c)
            If readLine.Count <> 2 Then
                Throw New Exception("Error - ANN_Fast_Backprop_PopulationApp    Descr: Invalid contents of input file. Inputs and DesiredOutput should be delimited by a single '=' character")
                Exit Sub
            End If
            UseMomentum = Convert.ToBoolean(readLine(1))  'Dats Set File Location
            'UseMomentum = Convert.ToBoolean(sr.ReadLine)

            line = sr.ReadLine
            readLine = line.Split("="c)
            If readLine.Count <> 2 Then
                Throw New Exception("Error - ANN_Fast_Backprop_PopulationApp    Descr: Invalid contents of input file. Inputs and DesiredOutput should be delimited by a single '=' character")
                Exit Sub
            End If
            momentumScaler = Convert.ToDouble(readLine(1))  'Dats Set File Location
            'momentumScaler = Convert.ToDouble(sr.ReadLine)

            line = sr.ReadLine
            readLine = line.Split("="c)
            If readLine.Count <> 2 Then
                Throw New Exception("Error - ANN_Fast_Backprop_PopulationApp    Descr: Invalid contents of input file. Inputs and DesiredOutput should be delimited by a single '=' character")
                Exit Sub
            End If
            annealingMaxPercent = Convert.ToInt32(readLine(1))  'Dats Set File Location
            'annealingMaxPercent = Convert.ToInt32(sr.ReadLine)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub InitializeArrays()
        'Initialize Arrays
        ReDim inputValues(countInputNeurons - 1)
        ReDim hiddenValues(countHiddenNeurons - 1, countHiddenLayers - 1, 2)
        ReDim outputValues(countOutputNeurons - 1, 2)
        ReDim ihWeights(countInputNeurons - 1, countHiddenNeurons - 1, 1)
        If countHiddenLayers > 1 Then
            ReDim hhWeights(countHiddenNeurons - 1, countHiddenNeurons - 1, countHiddenLayers - 2, 1)
        End If
        ReDim hoWeights(countHiddenNeurons - 1, countOutputNeurons - 1, 1)
    End Sub

    Public Sub RandomizeWeightAndBiasArrays()
        'Randomize Weight Arrays
        For i As Integer = 0 To countInputNeurons - 1
            For j As Integer = 0 To countHiddenNeurons - 1
                ihWeights(i, j, CURRENT) = RandomWeight(maxWeightValue)
                ihWeights(i, j, PREVIOUS) = ihWeights(i, j, CURRENT)    'Setting Previous Weight so first calculation is 0
            Next
        Next

        For layer As Integer = 0 To countHiddenLayers - 2
            For i As Integer = 0 To countHiddenNeurons - 1
                For j As Integer = 0 To countHiddenNeurons - 1
                    hhWeights(i, j, layer, CURRENT) = RandomWeight(maxWeightValue)
                    hhWeights(i, j, layer, PREVIOUS) = hhWeights(i, j, layer, CURRENT)  'Setting Previous Weight so first calculation is 0
                Next
            Next
        Next

        For i As Integer = 0 To countHiddenNeurons - 1
            For j As Integer = 0 To countOutputNeurons - 1
                hoWeights(i, j, CURRENT) = RandomWeight(maxWeightValue)
                hoWeights(i, j, PREVIOUS) = hoWeights(i, j, CURRENT)    'Setting Previous Weight so first calculation is 0
            Next
        Next

        'Randomize Bias Values
        For layer As Integer = 0 To countHiddenLayers - 1
            For i As Integer = 0 To countHiddenNeurons - 1
                hiddenValues(i, layer, BIAS) = RandomWeight(maxBiasValue)
            Next
        Next

        For i As Integer = 0 To countOutputNeurons - 1
            outputValues(i, BIAS) = RandomWeight(maxBiasValue)
        Next
    End Sub



    Public Sub importANNFile(ByVal FilePath As String)
        Dim sr As StreamReader
        Dim line As String

        Try
            sr = File.OpenText(FilePath)

            'Read in countInputNeurons and verify
            line = sr.ReadLine
            If Convert.ToInt32(line) <> countInputNeurons Then
                Throw New Exception("Error - Importing ANN from File, Input Neuron Counts do not match between ANN file and DataSet file!")
            End If

            'Read in countHiddenNeurons
            countHiddenNeurons = Convert.ToInt32(sr.ReadLine)

            'Read in countHiddenLayers
            countHiddenLayers = Convert.ToInt32(sr.ReadLine)

            'Read in countOutputNeurons and verify
            line = sr.ReadLine
            If Convert.ToInt32(line) <> countOutputNeurons Then
                Throw New Exception("Error - Importing ANN from File, Output Neuron Counts do not match between ANN file and DataSet file!")
            End If

            'Read in count Total Weights (not used in this program, but used in others)
            line = sr.ReadLine

            'Now Initialize Weight and Neuron arrays since we now have all needed data (counts)
            InitializeArrays()

            'Read in the Input-Hidden weights and Biases
            For hiddenNeuron As Integer = 0 To countHiddenNeurons - 1
                For inputNeuron As Integer = 0 To countInputNeurons - 1
                    ihWeights(inputNeuron, hiddenNeuron, CURRENT) = Convert.ToDouble(sr.ReadLine)
                Next
                hiddenValues(hiddenNeuron, 0, BIAS) = Convert.ToDouble(sr.ReadLine)
            Next
            'Below is the code used to write out the I-H Weights
            'For hiddenNeuron As Integer = 0 To countHiddenNeurons - 1
            '    For inputNeuron As Integer = 0 To countInputNeurons - 1
            '        sw.WriteLine(ihWeights(inputNeuron, hiddenNeuron, CURRENT))
            '    Next
            '    sw.WriteLine(hiddenValues(hiddenNeuron, 0, BIAS))
            'Next



            If countHiddenLayers > 1 Then
                For layer As Integer = 0 To countHiddenLayers - 2
                    For HiddenAbove As Integer = 0 To countHiddenNeurons - 1
                        For HiddenBelow As Integer = 0 To countHiddenNeurons - 1
                            hhWeights(HiddenBelow, HiddenAbove, layer, CURRENT) = Convert.ToDouble(sr.ReadLine)
                        Next
                        hiddenValues(HiddenAbove, layer + 1, BIAS) = Convert.ToDouble(sr.ReadLine)
                    Next
                Next
            End If
            'Below is the code used to write the H-H weights and biases
            'If countHiddenLayers > 1 Then
            '    For layer As Integer = 0 To countHiddenLayers - 2
            '        For HiddenAbove As Integer = 0 To countHiddenNeurons - 1
            '            For HiddenBelow As Integer = 0 To countHiddenNeurons - 1
            '                sw.WriteLine(hhWeights(HiddenBelow, HiddenAbove, layer, CURRENT))
            '            Next
            '            sw.WriteLine(hiddenValues(HiddenAbove, layer + 1, BIAS))
            '        Next
            '    Next
            'End If




            For outputNeuron As Integer = 0 To countOutputNeurons - 1
                For hiddenNeuron As Integer = 0 To countHiddenNeurons - 1
                    hoWeights(hiddenNeuron, outputNeuron, CURRENT) = Convert.ToDouble(sr.ReadLine)
                Next
                outputValues(outputNeuron, BIAS) = Convert.ToDouble(sr.ReadLine)
            Next
            'Below is the code used to write out the H-O weights and biases
            'For outputNeuron As Integer = 0 To countOutputNeurons - 1
            '    For hiddenNeuron As Integer = 0 To countHiddenNeurons - 1
            '        sw.WriteLine(hoWeights(hiddenNeuron, outputNeuron, CURRENT))
            '    Next
            '    sw.WriteLine(outputValues(outputNeuron, BIAS))
            'Next


        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Sub outputNetwork(ByVal fileName As String)
        ANNFileName = fileName  'Save ANN File Path for use in BPROP File output

        Dim sw As New StreamWriter(fileName)
        sw.WriteLine(countInputNeurons)
        sw.WriteLine(countHiddenNeurons)
        sw.WriteLine(countHiddenLayers)
        sw.WriteLine(countOutputNeurons)
        sw.WriteLine(((countInputNeurons + 1) * countHiddenNeurons) + ((countHiddenLayers - 1) * (countHiddenNeurons + 1) * countHiddenNeurons) + ((countHiddenNeurons + 1) * countOutputNeurons))

        For hiddenNeuron As Integer = 0 To countHiddenNeurons - 1
            For inputNeuron As Integer = 0 To countInputNeurons - 1
                sw.WriteLine(ihWeights(inputNeuron, hiddenNeuron, CURRENT))
            Next
            sw.WriteLine(hiddenValues(hiddenNeuron, 0, BIAS))
        Next

        If countHiddenLayers > 1 Then
            For layer As Integer = 0 To countHiddenLayers - 2
                For HiddenAbove As Integer = 0 To countHiddenNeurons - 1
                    For HiddenBelow As Integer = 0 To countHiddenNeurons - 1
                        sw.WriteLine(hhWeights(HiddenBelow, HiddenAbove, layer, CURRENT))
                    Next
                    sw.WriteLine(hiddenValues(HiddenAbove, layer + 1, BIAS))
                Next
            Next
        End If

        For outputNeuron As Integer = 0 To countOutputNeurons - 1
            For hiddenNeuron As Integer = 0 To countHiddenNeurons - 1
                sw.WriteLine(hoWeights(hiddenNeuron, outputNeuron, CURRENT))
            Next
            sw.WriteLine(outputValues(outputNeuron, BIAS))
        Next

        sw.Close()

    End Sub

    Public Sub outputBPROPFile(ByVal filename As String, ByVal FileTitle As String)
        If DataFileName = "" Then
            Throw New Exception("Error creting BPROP file: DataFileName cannot be nothing")
        End If
        If ANNFileName = "" Then
            Throw New Exception("Error creating BPROP file: ANNFileName cannot be nothing, output ANN File first, then create BPROP file.")
        End If

        Dim sw As New StreamWriter(filename)
        Try
            sw.WriteLine("### Backprop Identification ###")
            sw.WriteLine("ID=" & ID)
            sw.WriteLine("File Title=" & FileTitle)
            sw.WriteLine()
            sw.WriteLine("### Input Files - !!! NEVER MODIFY THIS PART !!! ###")
            sw.WriteLine("DO NOT MODIFY: Dataset file location=" & DataFileName)
            sw.WriteLine("DO NOT MODIFY: ANN file location=" & ANNFileName)
            sw.WriteLine()
            sw.WriteLine("### Training Configuration ###")
            sw.WriteLine("Initial rho value=" & Convert.ToDecimal(rho))
            sw.WriteLine("Rho multiplier=" & rhoMultiplier)
            sw.WriteLine("Rho interval=" & rhoInterval)
            sw.WriteLine("Use Momentum=" & UseMomentum)
            sw.WriteLine("Momentum scaler=" & momentumScaler)
            sw.WriteLine("Simulated annealing max percent(%)=" & annealingMaxPercent)
        Catch ex As Exception
            Throw ex
        Finally
            sw.Close()
        End Try
    End Sub

    Public Sub outputNetworkOutput(ByVal fileName As String)
        Dim sw As New StreamWriter(fileName)

        sw.WriteLine("Fitness: " & Fitness & " Avg: " & (Fitness / exemplarCount) & "  Rho: " & rho)

        For i As Integer = 0 To exemplarCount - 1
            For j As Integer = 0 To countInputNeurons - 1
                sw.Write(exemplarInputs(i, j))
                If j <> countInputNeurons - 1 Then
                    sw.Write(",")
                End If
            Next

            run(i)
            sw.Write("=")
            For j As Integer = 0 To countOutputNeurons - 1
                sw.Write(outputValues(j, VALUE))
                If j <> countOutputNeurons - 1 Then
                    sw.Write(",")
                End If
            Next
            sw.WriteLine()
        Next

        sw.Close()

    End Sub

    Public Sub run(ByVal exemplarIndex As Integer)
        'Set Input Neuron Values
        For i As Integer = 0 To countInputNeurons - 1
            inputValues(i) = exemplarInputs(exemplarIndex, i)
        Next

        'Calculate First Layer of Hidden Neuron Values
        For hiddenNeuron As Integer = 0 To countHiddenNeurons - 1
            hiddenValues(hiddenNeuron, 0, VALUE) = 0   'Reset to 0
            For inputNeuron As Integer = 0 To countInputNeurons - 1
                hiddenValues(hiddenNeuron, 0, VALUE) += inputValues(inputNeuron) * ihWeights(inputNeuron, hiddenNeuron, CURRENT)
            Next
            hiddenValues(hiddenNeuron, 0, VALUE) = Activation(hiddenValues(hiddenNeuron, 0, VALUE) + hiddenValues(hiddenNeuron, 0, BIAS))
        Next

        'Calculate Hidden Layer Neuron Values
        If countHiddenLayers > 1 Then
            For layer As Integer = 1 To countHiddenLayers - 1
                For aboveHidden As Integer = 0 To countHiddenNeurons - 1
                    hiddenValues(aboveHidden, layer, VALUE) = 0   'Reset to 0
                    For belowHidden As Integer = 0 To countHiddenNeurons - 1
                        hiddenValues(aboveHidden, layer, VALUE) += hiddenValues(belowHidden, layer - 1, VALUE) * hhWeights(belowHidden, aboveHidden, layer - 1, CURRENT)
                    Next
                    hiddenValues(aboveHidden, layer, VALUE) = Activation(hiddenValues(aboveHidden, layer, VALUE) + hiddenValues(aboveHidden, layer, BIAS))
                Next
            Next
        End If

        'Calculate Output Neuron Values
        For outputNeuron As Integer = 0 To countOutputNeurons - 1
            outputValues(outputNeuron, VALUE) = 0    'Reset to 0
            For hiddenNeuron As Integer = 0 To countHiddenNeurons - 1
                outputValues(outputNeuron, VALUE) += hiddenValues(hiddenNeuron, countHiddenLayers - 1, VALUE) * hoWeights(hiddenNeuron, outputNeuron, CURRENT)
            Next
            outputValues(outputNeuron, VALUE) += outputValues(outputNeuron, BIAS)
        Next
    End Sub

    Public Sub Backprop(ByVal iteration As Integer)
        'Calculate Output Neuron Deltas
        For i As Integer = 0 To countOutputNeurons - 1
            outputValues(i, DELTA) = -2 * (exemplarOutputs(iteration, i) - outputValues(i, VALUE))
        Next

        'Calculate Top Layer Hidden Neuron Deltas
        Dim totalDeltas As Double = 0
        For hiddenNeuron As Integer = 0 To countHiddenNeurons - 1
            hiddenValues(hiddenNeuron, countHiddenLayers - 1, DELTA) = 0   'Reset Value
            For outputNeuron As Integer = 0 To countOutputNeurons - 1
                hiddenValues(hiddenNeuron, countHiddenLayers - 1, DELTA) += fPrime(hiddenValues(hiddenNeuron, countHiddenLayers - 1, VALUE)) * outputValues(outputNeuron, DELTA) * hoWeights(hiddenNeuron, outputNeuron, CURRENT)
                totalDeltas += hiddenValues(hiddenNeuron, countHiddenLayers - 1, DELTA) 'Keep running total of delta values for analysis
            Next
        Next

        'Calculate Hidden Layer Neuron Deltas
        If countHiddenLayers > 1 Then
            For layer As Integer = countHiddenLayers - 2 To 0 Step -1
                For belowHidden As Integer = 0 To countHiddenNeurons - 1
                    hiddenValues(belowHidden, layer, DELTA) = 0 'Reset Value
                    For aboveHidden As Integer = 0 To countHiddenNeurons - 1
                        hiddenValues(belowHidden, layer, DELTA) += fPrime(hiddenValues(belowHidden, layer, VALUE)) * hiddenValues(aboveHidden, layer + 1, DELTA) * hhWeights(belowHidden, aboveHidden, layer, CURRENT)
                        totalDeltas += hiddenValues(belowHidden, layer, DELTA)  'Keep running total of delta values for analysis
                    Next
                Next
            Next
        End If

        'Calculate Hidden-Output Weights
        ''Utilizing Momemtum
        '
        ''XXXXXXXXX MOMENTUM CURRENTLY DISABLED TO TEST FASTER PROCESSING ENHANCEMENTS XXXXXXXX
        '
        Dim previousWeightChange As Double
        For hiddenNeuron As Integer = 0 To countHiddenNeurons - 1
            For outputNeuron As Integer = 0 To countOutputNeurons - 1
                If UseMomentum Then
                    previousWeightChange = hoWeights(hiddenNeuron, outputNeuron, CURRENT) - hoWeights(hiddenNeuron, outputNeuron, PREVIOUS)    'Store previous weight change
                    hoWeights(hiddenNeuron, outputNeuron, PREVIOUS) = hoWeights(hiddenNeuron, outputNeuron, CURRENT)    'Store current weight to be used as the next iteration's previous weight
                    hoWeights(hiddenNeuron, outputNeuron, CURRENT) = hoWeights(hiddenNeuron, outputNeuron, CURRENT) - rho * outputValues(outputNeuron, DELTA) * hiddenValues(hiddenNeuron, countHiddenLayers - 1, VALUE) + momentumScaler * previousWeightChange

                Else
                    'XXXX Momentum Stuff Commented Out, otherwise exactly the same as above XXXX
                    'previousWeightChange = hoWeights(hiddenNeuron, outputNeuron, CURRENT) - hoWeights(hiddenNeuron, outputNeuron, PREVIOUS)    'Store previous weight change
                    'hoWeights(hiddenNeuron, outputNeuron, PREVIOUS) = hoWeights(hiddenNeuron, outputNeuron, CURRENT)    'Store current weight to be used as the next iteration's previous weight
                    hoWeights(hiddenNeuron, outputNeuron, CURRENT) = hoWeights(hiddenNeuron, outputNeuron, CURRENT) - rho * outputValues(outputNeuron, DELTA) * hiddenValues(hiddenNeuron, countHiddenLayers - 1, VALUE) ' + momentumScaler * previousWeightChange
                End If
            Next
        Next

        'Calculate Hidden-Hidden Weights
        'Utilizing Momentum
        If countHiddenLayers > 1 Then
            For layer As Integer = countHiddenLayers - 2 To 0 Step -1
                For belowHidden As Integer = 0 To countHiddenNeurons - 1
                    For aboveHidden As Integer = 0 To countHiddenNeurons - 1
                        If UseMomentum Then
                            previousWeightChange = hhWeights(belowHidden, aboveHidden, layer, CURRENT) - hhWeights(belowHidden, aboveHidden, layer, PREVIOUS) 'Store previous weight change
                            hhWeights(belowHidden, aboveHidden, layer, PREVIOUS) = hhWeights(belowHidden, aboveHidden, layer, CURRENT)  'Store current weight to be used as next iteration's previous weight
                            hhWeights(belowHidden, aboveHidden, layer, CURRENT) = hhWeights(belowHidden, aboveHidden, layer, CURRENT) - rho * hiddenValues(aboveHidden, layer + 1, DELTA) * hiddenValues(belowHidden, layer, VALUE) + momentumScaler * previousWeightChange
                        Else
                            'XXXX Momentum Stuff Commented Out, otherwise exactly the same as above XXXX
                            'previousWeightChange = hhWeights(belowHidden, aboveHidden, layer, CURRENT) - hhWeights(belowHidden, aboveHidden, layer, PREVIOUS) 'Store previous weight change
                            'hhWeights(belowHidden, aboveHidden, layer, PREVIOUS) = hhWeights(belowHidden, aboveHidden, layer, CURRENT)  'Store current weight to be used as next iteration's previous weight
                            hhWeights(belowHidden, aboveHidden, layer, CURRENT) = hhWeights(belowHidden, aboveHidden, layer, CURRENT) - rho * hiddenValues(aboveHidden, layer + 1, DELTA) * hiddenValues(belowHidden, layer, VALUE) ' + momentumScaler * previousWeightChange
                        End If
                    Next
                Next
            Next
        End If

        'Calculate Input-Hidden Weights
        ''Utilizing Momentum
        For inputNeuron As Integer = 0 To countInputNeurons - 1
            For hiddenNeuron As Integer = 0 To countHiddenNeurons - 1
                If UseMomentum Then
                    previousWeightChange = ihWeights(inputNeuron, hiddenNeuron, CURRENT) - ihWeights(inputNeuron, hiddenNeuron, PREVIOUS)
                    ihWeights(inputNeuron, hiddenNeuron, PREVIOUS) = ihWeights(inputNeuron, hiddenNeuron, CURRENT)
                    ihWeights(inputNeuron, hiddenNeuron, CURRENT) = ihWeights(inputNeuron, hiddenNeuron, CURRENT) - rho * hiddenValues(hiddenNeuron, 0, DELTA) * inputValues(inputNeuron) + momentumScaler * previousWeightChange
                Else
                    'XXXX Momentum Stuff Commented Out, otherwise exactly the same as above XXXX
                    'previousWeightChange = ihWeights(inputNeuron, hiddenNeuron, CURRENT) - ihWeights(inputNeuron, hiddenNeuron, PREVIOUS)
                    'ihWeights(inputNeuron, hiddenNeuron, PREVIOUS) = ihWeights(inputNeuron, hiddenNeuron, CURRENT)
                    ihWeights(inputNeuron, hiddenNeuron, CURRENT) = ihWeights(inputNeuron, hiddenNeuron, CURRENT) - rho * hiddenValues(hiddenNeuron, 0, DELTA) * inputValues(inputNeuron) ' + momentumScaler * previousWeightChange
                End If
            Next
        Next
    End Sub

    Public Sub TrainEpoch(ByVal UseRhoMultiplier As Boolean, ByVal SimulatedAnnealing As Boolean)
        Fitness = 0 'Reset Fitness Value
        randomizedIndex = randomizeTheIndex(exemplarCount)

        For iteration As Integer = 0 To exemplarCount - 1
            run(randomizedIndex(iteration))
            Backprop(randomizedIndex(iteration))
        Next

        'Simulated Annealing
        ''Adjust each weight in the weights layers to be in a location 'near' its current location (simulating shaking the solution plane)
        If SimulatedAnnealing Then
            InitiateSimulatedAnnealing()
        End If

        'Dynamic Rho Value
        If UseRhoMultiplier Then
            rho *= rhoMultiplier
        End If

    End Sub

    Public Sub Train(ByVal Epochs As Integer, ByVal VerboseLogging As Boolean)
        Dim Time1Start As DateTime = Now
        Dim Time1End As DateTime
        Dim TimeSpan1 As TimeSpan
        For i As Integer = 1 To Epochs
            If (i Mod rhoInterval) = 0 Then
                TrainEpoch(True, False)
            Else
                TrainEpoch(False, False)
            End If

            If VerboseLogging And i Mod 100 = 0 Then
                Console.Write(".")
            End If
        Next
        Time1End = Now
        TimeSpan1 = Time1End - Time1Start
        If VerboseLogging Then
            Console.WriteLine(" - " & Epochs & " Epochs Trained in: " & Convert.ToInt32(TimeSpan1.Hours) & "h, " & Convert.ToInt32(TimeSpan1.Minutes) & "m, " & Convert.ToInt32(TimeSpan1.Seconds) & "s, " & Convert.ToInt32(TimeSpan1.Milliseconds) & "ms - " & exemplarCount & " Exemplar Sets, Momentum: " & UseMomentum & ", Network: " & countInputNeurons & "-" & countHiddenNeurons & "-" & countHiddenLayers & "-" & countOutputNeurons)
        End If
    End Sub

    Public Sub InitiateSimulatedAnnealing()

        For hiddenNeuron As Integer = 0 To countHiddenNeurons - 1
            For outputNeuron As Integer = 0 To countOutputNeurons - 1
                hoWeights(hiddenNeuron, outputNeuron, CURRENT) = hoWeights(hiddenNeuron, outputNeuron, CURRENT) + ((RandomNumber(annealingMaxPercent) / 100) * hoWeights(hiddenNeuron, outputNeuron, CURRENT))
            Next
        Next

        If countHiddenLayers > 1 Then
            For layer As Integer = countHiddenLayers - 2 To 0 Step -1
                For belowHidden As Integer = 0 To countHiddenNeurons - 1
                    For aboveHidden As Integer = 0 To countHiddenNeurons - 1
                        hhWeights(belowHidden, aboveHidden, layer, CURRENT) = hhWeights(belowHidden, aboveHidden, layer, CURRENT) + ((RandomNumber(annealingMaxPercent) / 100) * hhWeights(belowHidden, aboveHidden, layer, CURRENT))
                    Next
                Next
            Next
        End If

        For inputNeuron As Integer = 0 To countInputNeurons - 1
            For hiddenNeuron As Integer = 0 To countHiddenNeurons - 1
                ihWeights(inputNeuron, hiddenNeuron, CURRENT) = ihWeights(inputNeuron, hiddenNeuron, CURRENT) + ((RandomNumber(annealingMaxPercent) / 100) * ihWeights(inputNeuron, hiddenNeuron, CURRENT))
            Next
        Next

    End Sub

    Public Sub calcFitness()
        Fitness = 0
        For iteration As Integer = 0 To exemplarCount - 1
            run(iteration)
            For i As Integer = 0 To countOutputNeurons - 1
                Fitness += AbsVal(exemplarOutputs(iteration, i) - outputValues(i, VALUE))  'Error
            Next
        Next
    End Sub

    Public Function Copy() As Backprop
        Dim ACopy As New Backprop(_Parms, exemplarInputs, exemplarOutputs, exemplarCount, countInputNeurons, countOutputNeurons)

        ACopy.ID = ID
        ACopy.rho = rho
        ACopy.rhoInterval = rhoInterval
        ACopy.rhoMultiplier = rhoMultiplier

        For i As Integer = 0 To countInputNeurons - 1
            For j As Integer = 0 To countHiddenNeurons - 1
                ACopy.ihWeights(i, j, CURRENT) = ihWeights(i, j, CURRENT)
                ACopy.ihWeights(i, j, PREVIOUS) = ihWeights(i, j, PREVIOUS)
            Next
        Next

        If countHiddenLayers > 1 Then
            For layer As Integer = 0 To countHiddenLayers - 2
                For i As Integer = 0 To countHiddenNeurons - 1
                    For j As Integer = 0 To countHiddenNeurons - 1
                        ACopy.hhWeights(i, j, layer, CURRENT) = hhWeights(i, j, layer, CURRENT)
                        ACopy.hhWeights(i, j, layer, PREVIOUS) = hhWeights(i, j, layer, PREVIOUS)
                    Next
                Next
            Next
        End If

        For i As Integer = 0 To countHiddenNeurons - 1
            For j As Integer = 0 To countOutputNeurons - 1
                ACopy.hoWeights(i, j, CURRENT) = hoWeights(i, j, CURRENT)
                ACopy.hoWeights(i, j, PREVIOUS) = hoWeights(i, j, PREVIOUS)
            Next
        Next

        For layer As Integer = 0 To countHiddenLayers - 1
            For i As Integer = 0 To countHiddenNeurons - 1
                ACopy.hiddenValues(i, layer, BIAS) = hiddenValues(i, layer, BIAS)
            Next
        Next

        For i As Integer = 0 To countOutputNeurons - 1
            ACopy.outputValues(i, BIAS) = outputValues(i, BIAS)
        Next

        Return ACopy
    End Function

    Public Sub importDataSet(ByVal filePath As String)
        Const _MethodName As String = "importDataSet(filePath)"
        Dim line As String
        Dim inputs() As String
        Dim sInputValues() As String
        Dim sOutputValues() As String
        exemplarCount = numRowsInTextFile(filePath)

        Dim sr As StreamReader = File.OpenText(filePath)
        line = sr.ReadLine

        inputs = line.Split("="c)

        If inputs.Count <> 2 Then
            Throw New Exception("Error - Method: " & _MethodName & _
                                        "Descr: Invalid contents of input file. Inputs and DesiredOutput should be delimited by a single '=' character")
            Exit Sub
        End If

        sInputValues = inputs(0).Split(","c)
        sOutputValues = inputs(1).Split(","c)

        countInputNeurons = Convert.ToInt32(sInputValues.Count)
        ReDim exemplarInputs(exemplarCount - 1, countInputNeurons - 1)
        countOutputNeurons = Convert.ToInt32(sOutputValues.Count)
        ReDim exemplarOutputs(exemplarCount - 1, countOutputNeurons - 1)

        For i As Integer = 0 To exemplarCount - 1

            For k As Integer = 0 To countInputNeurons - 1
                exemplarInputs(i, k) = Convert.ToDouble(sInputValues(k))
            Next

            For k As Integer = 0 To countOutputNeurons - 1
                exemplarOutputs(i, k) = Convert.ToDouble(sOutputValues(k))
            Next

            line = sr.ReadLine

            If Not line = Nothing Then
                inputs = line.Split("="c)

                If inputs.Count <> 2 Then
                    Throw New Exception("Error - Method: " & _MethodName & _
                                        "Descr: Data File invalid or corrupt")

                    Exit Sub
                End If

                sInputValues = inputs(0).Split(","c)
                sOutputValues = inputs(1).Split(","c)
            End If

        Next

        sr.Close()

    End Sub

    Public Sub DeconstructCurrentDirectory(ByVal filepath As String)
        Dim split() As String

        'Extract the Directory Path
        split = filepath.Split("\"c)
        For i As Integer = 0 To split.Length - 2
            If i = 0 Then
                CurrentDirectory = split(i) & "\"
            Else
                CurrentDirectory += split(i) & "\"
            End If
        Next
    End Sub

End Class
