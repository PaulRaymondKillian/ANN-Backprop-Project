Imports System.IO
Imports System.Data.SqlClient

Public Class Form1

    'ANN - We are using the ANN portion of the Backprop object, adds continuity across programs on how ANN works
    Dim ANN As ANN.ANN

    Dim DesiredOutputs() As String
    Dim StockNames() As String
    Dim StockSymbols() As String
    Dim countStocks As Integer

    Private Sub btnANNFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnANNFile.Click
        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            txtANNFile.Text = OpenFileDialog1.FileName
            LoadANNFile(txtANNFile.Text)
        End If
    End Sub

    Public Sub LoadANNFile(ByVal filepath As String)
        ANN = New ANN.ANN(filepath)
        txtInputNeurons.Text = ANN.countInputNeurons
        txtHiddenNeurons.Text = ANN.countHiddenNeurons
        txtHiddenLayers.Text = ANN.countHiddenLayers
        txtOutputNeurons.Text = ANN.countOutputNeurons
    End Sub

    Private Sub GroupBox2_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox2.Enter

    End Sub

    Private Sub btnRawData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRawData.Click
        If OpenFileDialog3.ShowDialog = Windows.Forms.DialogResult.OK Then
            txtRawData.Text = OpenFileDialog3.FileName
            LoadRawDataFile(txtRawData.Text)
        End If
    End Sub

    Public Sub LoadRawDataFile(ByVal filepath As String)
        lbSelectedDateData.Items.Clear()
        Dim sr As StreamReader
        Dim Inputs(numRowsInTextFile(filepath) - 1) As String
        Dim tempOutputs(numRowsInTextFile(filepath) - 1)
        ReDim DesiredOutputs(numRowsInTextFile(filepath) - 1)
        Dim index As Integer = 0
        Dim line As String

        Try
            sr = File.OpenText(filepath)

            txtParmsPerStock.Text = sr.ReadLine() 'First line of Raw Data File has the number of parms per stock

            Do Until sr.EndOfStream
                line = sr.ReadLine
                Inputs(index) = line
                index += 1
            Loop

            For i As Integer = index - 1 To 0 Step -1
                lbSelectedDateData.Items.Add(Inputs(i))
            Next

            txtDataRows.Text = lbSelectedDateData.Items.Count

            sr.Close()

            txtDataParms.Text = lbSelectedDateData.Items(0).ToString.Split(","c).Length

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function numRowsInTextFile(ByVal filePath As String) As Integer
        Dim sr As StreamReader = File.OpenText(filePath)
        Dim rows As Integer = 0
        Dim line As String

        Do
            line = sr.ReadLine
            rows += 1
        Loop Until sr.EndOfStream

        Return rows

    End Function

    Private Sub btnStockFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStockFile.Click
        If OpenFileDialog2.ShowDialog = Windows.Forms.DialogResult.OK Then
            txtHeaderFile.Text = OpenFileDialog2.FileName
            LoadHeaderFile(txtHeaderFile.Text)
            LoadStockSymbols()
        End If
    End Sub

    Public Sub LoadHeaderFile(ByVal FilePath As String)
        lbSelectDate.Items.Clear()
        Dim sr As StreamReader
        Dim line As String
        Dim countDayData As Integer
        Dim Days() As String

        Try
            'The first row of the header file contains all the stocks in a comma delimited string.
            sr = File.OpenText(FilePath)

            'Read in and separate out stock names
            line = sr.ReadLine
            StockNames = line.Split(","c)
            countStocks = StockNames.Length
            txtCountStocks.Text = countStocks

            'The rest of the lines in the file contain the dates where data exists in the corresponding raw data file
            countDayData = numRowsInTextFile(FilePath) - 1  'Subtract 1 to not count the Stock name rows
            txtCountDays.Text = countDayData
            ReDim Days(countDayData - 1)
            For i As Integer = 0 To countDayData - 1
                line = sr.ReadLine
                Days(i) = line
            Next

            For i = countDayData - 1 To 0 Step -1
                lbSelectDate.Items.Add(Days(i))
            Next


            sr.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub LoadStockSymbols()
        lbStockList1.Items.Clear()
        lbStockList2.Items.Clear()
        lbStockList3.Items.Clear()
        Dim myConnection As SqlConnection
        Dim myCommand As SqlCommand
        Dim connectionString As String
        Dim dr As SqlDataReader

        Try
            Dim sr As StreamReader
            sr = File.OpenText("ConnectionString.txt")
            connectionString = sr.ReadLine
            sr.Close()

            myConnection = New SqlConnection(connectionString)
            myConnection.Open()
            ReDim StockSymbols(StockNames.Length - 1)
            For i As Integer = 0 To StockNames.Length - 1
                myCommand = New SqlCommand("Select Symbol from Stock where ID=" & StockNames(i), myConnection)
                dr = myCommand.ExecuteReader()
                dr.Read()
                StockSymbols(i) = dr(0).ToString
                dr.Close()
            Next
            myConnection.Close()

            For i As Integer = 0 To StockSymbols.Length - 1
                If i < 33 Then
                    lbStockList1.Items.Add(StockSymbols(i) & ": ")
                ElseIf i < 66 Then
                    lbStockList2.Items.Add(StockSymbols(i) & ": ")
                Else
                    lbStockList3.Items.Add(StockSymbols(i) & ": ")
                End If
            Next

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub lbSelectDate_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbSelectDate.SelectedIndexChanged
        If lbSelectedDateData.Items.Count > 0 Then
            lbSelectedDateData.SelectedIndex = lbSelectDate.SelectedIndex

            lbStockList1.Items.Clear()
            lbStockList2.Items.Clear()
            lbStockList3.Items.Clear()

            Dim outputs() As Double
            Dim ActualOutputs(countStocks - 1) As String
            Dim CalculatedOutputs(countStocks - 1) As String

            If lbSelectDate.SelectedIndex > -1 And ValidateANN() Then
                outputs = CalculateOutput(lbSelectDate.SelectedIndex)

                For j As Integer = 0 To countStocks - 1
                    ActualOutputs(j) = (StockSymbols(j) & ": ").PadRight(10, " "c) & Math.Round(outputs(j), 8)
                    If j < 33 Then
                        lbStockList1.Items.Add(ActualOutputs(j))
                    ElseIf j >= 33 And j < 66 Then
                        lbStockList2.Items.Add(ActualOutputs(j))
                    Else
                        lbStockList3.Items.Add(ActualOutputs(j))
                    End If
                Next
            Else
                MsgBox("Cannot Calculate Network: ANN, Header and DataSet combination is invalid.")
            End If

        End If
    End Sub

    Private Sub lbSelectedDateData_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbSelectedDateData.SelectedIndexChanged
        If lbSelectDate.Items.Count > 0 Then
            lbSelectDate.SelectedIndex = lbSelectedDateData.SelectedIndex
        End If
    End Sub

    Public Function CalculateOutput(ByVal index As Integer) As Double()
        Dim s As String = lbSelectedDateData.Items(index).ToString
        Dim split() As String = s.Split(","c)
        Dim inputs(split.Length - 1) As Double

        For i As Integer = 0 To split.Length - 1
            inputs(i) = Convert.ToDouble(split(i))
        Next

        Return ANN.run(inputs)
    End Function

    Public Function ValidateANN() As Boolean
        Dim Validated As Boolean = True

        If ANN Is Nothing Or lbSelectDate.Items.Count = 0 Or lbSelectedDateData.Items.Count = 0 Then
            Validated = False
        Else
            If ANN.countInputNeurons <> lbSelectedDateData.Items(0).ToString.Split(","c).Length Then
                Validated = False
            End If
            If ANN.countOutputNeurons <> countStocks Then
                Validated = False
            End If

        End If

        Return Validated
    End Function
End Class
