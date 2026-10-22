''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'' Project: Artificial Neural Network Error Backwards-Propagation Project	    		''
'' Component: ANN Object													            ''
'' Description: This class encapsulates the Artificial Neural Network logic. This object''
''              is used by the Population Backprop App, but not in the Backprop Process ''
''              App.                                                                    ''
''																						''
'' Author: Paul Killian																	''
'' Contact: PaulKillian@DecodedCortex.com												''
'' Website: www.DecodedCortex.com														''
'' Disclaimer: Any non-commercial use of this code or a derivative therof must give     ''
''              credit to those who developed it (Decoded Cortex). Any commercial use of''
''              this code or the application themselves is now allowed unless authorized''
''              by Decoded Cortex.                                                      ''
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Imports System.IO

Public Class ANN

    Const e As Double = Math.E

    Const VALUE As Integer = 0
    Const BIAS As Integer = 1

    Public countInputNeurons As Integer
    Public countHiddenNeurons As Integer
    Public countHiddenLayers As Integer
    Public countOutputNeurons As Integer

    Public totalWeights As Integer

    'Neuron arrays
    Dim inputValues() As Double
    Public hiddenValues(,,) As Double
    Public outputValues(,) As Double

    'Weight arrays
    Public ihWeights(,) As Double 'Increased dimension of weight array to include CURRENT/PREVIOUS weights (Momentum)
    Public hhWeights(,,) As Double 'Increased dimension of weight array to include CURRENT/PREVIOUS weights (Momentum)
    Public hoWeights(,) As Double 'Increased dimension of weight array to include CURRENT/PREVIOUS weights (Momentum)
    Public maxWeightValue As Double
    Public maxBiasValue As Double

    Public Sub New(ByVal filepath As String)
        importANNFile(filepath)
    End Sub

    Public Sub importANNFile(ByVal FilePath As String)
        Dim sr As StreamReader

        Try
            sr = File.OpenText(FilePath)

            'Read in countInputNeurons and verify
            countInputNeurons = Convert.ToInt32(sr.ReadLine)

            'Read in countHiddenNeurons
            countHiddenNeurons = Convert.ToInt32(sr.ReadLine)

            'Read in countHiddenLayers
            countHiddenLayers = Convert.ToInt32(sr.ReadLine)

            'Read in countOutputNeurons and verify
            countOutputNeurons = Convert.ToInt32(sr.ReadLine)

            'Read in count Total Weights
            totalWeights = Convert.ToInt32(sr.ReadLine)

            'Now Initialize Weight and Neuron arrays since we now have all needed data (counts)
            InitializeArrays()

            'Read in the Input-Hidden weights and Biases
            For hiddenNeuron As Integer = 0 To countHiddenNeurons - 1
                For inputNeuron As Integer = 0 To countInputNeurons - 1
                    ihWeights(inputNeuron, hiddenNeuron) = Convert.ToDouble(sr.ReadLine)
                Next
                hiddenValues(hiddenNeuron, 0, BIAS) = Convert.ToDouble(sr.ReadLine)
            Next

            If countHiddenLayers > 1 Then
                For layer As Integer = 0 To countHiddenLayers - 2
                    For HiddenAbove As Integer = 0 To countHiddenNeurons - 1
                        For HiddenBelow As Integer = 0 To countHiddenNeurons - 1
                            hhWeights(HiddenBelow, HiddenAbove, layer) = Convert.ToDouble(sr.ReadLine)
                        Next
                        hiddenValues(HiddenAbove, layer + 1, BIAS) = Convert.ToDouble(sr.ReadLine)
                    Next
                Next
            End If

            For outputNeuron As Integer = 0 To countOutputNeurons - 1
                For hiddenNeuron As Integer = 0 To countHiddenNeurons - 1
                    hoWeights(hiddenNeuron, outputNeuron) = Convert.ToDouble(sr.ReadLine)
                Next
                outputValues(outputNeuron, BIAS) = Convert.ToDouble(sr.ReadLine)
            Next

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Sub InitializeArrays()
        'Initialize Arrays
        ReDim inputValues(countInputNeurons - 1)
        ReDim hiddenValues(countHiddenNeurons - 1, countHiddenLayers - 1, 1)
        ReDim outputValues(countOutputNeurons - 1, 1)
        ReDim ihWeights(countInputNeurons - 1, countHiddenNeurons - 1)
        If countHiddenLayers > 1 Then
            ReDim hhWeights(countHiddenNeurons - 1, countHiddenNeurons - 1, countHiddenLayers - 2)
        End If
        ReDim hoWeights(countHiddenNeurons - 1, countOutputNeurons - 1)
    End Sub

    Public Function run(ByVal inputArray() As Double) As Double()
        If inputArray.Length <> countInputNeurons Then
            Throw New Exception("Error - run(inputArray), Desc: Input Array not equal to number of input neurons")
        End If

        For i As Integer = 0 To countInputNeurons - 1
            inputValues(i) = inputArray(i)
        Next

        'Calculate First Layer of Hidden Neuron Values
        For hiddenNeuron As Integer = 0 To countHiddenNeurons - 1
            hiddenValues(hiddenNeuron, 0, VALUE) = 0   'Reset to 0
            For inputNeuron As Integer = 0 To countInputNeurons - 1
                hiddenValues(hiddenNeuron, 0, VALUE) += inputValues(inputNeuron) * ihWeights(inputNeuron, hiddenNeuron)
            Next
            hiddenValues(hiddenNeuron, 0, VALUE) = Activation(hiddenValues(hiddenNeuron, 0, VALUE) + hiddenValues(hiddenNeuron, 0, BIAS))
        Next

        'Calculate Hidden Layer Neuron Values
        If countHiddenLayers > 1 Then
            For layer As Integer = 1 To countHiddenLayers - 1
                For aboveHidden As Integer = 0 To countHiddenNeurons - 1
                    hiddenValues(aboveHidden, layer, VALUE) = 0   'Reset to 0
                    For belowHidden As Integer = 0 To countHiddenNeurons - 1
                        hiddenValues(aboveHidden, layer, VALUE) += hiddenValues(belowHidden, layer - 1, VALUE) * hhWeights(belowHidden, aboveHidden, layer - 1)
                    Next
                    hiddenValues(aboveHidden, layer, VALUE) = Activation(hiddenValues(aboveHidden, layer, VALUE) + hiddenValues(aboveHidden, layer, BIAS))
                Next
            Next
        End If

        'Calculate Output Neuron Values
        For outputNeuron As Integer = 0 To countOutputNeurons - 1
            outputValues(outputNeuron, VALUE) = 0    'Reset to 0
            For hiddenNeuron As Integer = 0 To countHiddenNeurons - 1
                outputValues(outputNeuron, VALUE) += hiddenValues(hiddenNeuron, countHiddenLayers - 1, VALUE) * hoWeights(hiddenNeuron, outputNeuron)
            Next
            outputValues(outputNeuron, VALUE) += outputValues(outputNeuron, BIAS)
        Next

        Dim Output(countOutputNeurons - 1) As Double
        For i As Integer = 0 To countOutputNeurons - 1
            Output(i) = outputValues(i, VALUE)
        Next

        Return Output

    End Function

    Public Function Activation(ByVal value As Double) As Double
        Return 1 / (1 + Math.Pow(e, -value))
    End Function

    Public Function fPrime(ByVal u As Double) As Double
        Return u * (1 - u)
    End Function

End Class
