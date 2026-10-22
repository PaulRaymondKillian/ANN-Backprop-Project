<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.btnStockFile = New System.Windows.Forms.Button
        Me.txtStockFile = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.btnDataFileLocation = New System.Windows.Forms.Button
        Me.txtDataFileLocation = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.btnANNFileLocation = New System.Windows.Forms.Button
        Me.txtANNFileLocation = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.lbStockOutput1 = New System.Windows.Forms.ListBox
        Me.lbStockOutput2 = New System.Windows.Forms.ListBox
        Me.lbStockOutput3 = New System.Windows.Forms.ListBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.txtOutputNeurons = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtHiddenLayers = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtHiddenNeurons = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtInputNeurons = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.lbDesiredStockOutput3 = New System.Windows.Forms.ListBox
        Me.lbDesiredStockOutput2 = New System.Windows.Forms.ListBox
        Me.lbDesiredStockOutput1 = New System.Windows.Forms.ListBox
        Me.lbDataFileInputs = New System.Windows.Forms.ListBox
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.OpenFileDialog2 = New System.Windows.Forms.OpenFileDialog
        Me.btnCalculate = New System.Windows.Forms.Button
        Me.lbStockOutput4 = New System.Windows.Forms.ListBox
        Me.lbDesiredStockOutput4 = New System.Windows.Forms.ListBox
        Me.lbStockOutput5 = New System.Windows.Forms.ListBox
        Me.lbDesiredStockOutput5 = New System.Windows.Forms.ListBox
        Me.OpenFileDialog3 = New System.Windows.Forms.OpenFileDialog
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.HowToUseThisProgramToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnStockFile)
        Me.GroupBox1.Controls.Add(Me.txtStockFile)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.btnDataFileLocation)
        Me.GroupBox1.Controls.Add(Me.txtDataFileLocation)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.btnANNFileLocation)
        Me.GroupBox1.Controls.Add(Me.txtANNFileLocation)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 27)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(541, 96)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Data Sources"
        '
        'btnStockFile
        '
        Me.btnStockFile.Enabled = False
        Me.btnStockFile.Location = New System.Drawing.Point(507, 40)
        Me.btnStockFile.Name = "btnStockFile"
        Me.btnStockFile.Size = New System.Drawing.Size(25, 23)
        Me.btnStockFile.TabIndex = 8
        Me.btnStockFile.Text = "..."
        Me.btnStockFile.UseVisualStyleBackColor = True
        '
        'txtStockFile
        '
        Me.txtStockFile.Location = New System.Drawing.Point(67, 42)
        Me.txtStockFile.Name = "txtStockFile"
        Me.txtStockFile.Size = New System.Drawing.Size(434, 20)
        Me.txtStockFile.TabIndex = 7
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(6, 45)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(64, 13)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "Header File:"
        '
        'btnDataFileLocation
        '
        Me.btnDataFileLocation.Enabled = False
        Me.btnDataFileLocation.Location = New System.Drawing.Point(507, 69)
        Me.btnDataFileLocation.Name = "btnDataFileLocation"
        Me.btnDataFileLocation.Size = New System.Drawing.Size(25, 23)
        Me.btnDataFileLocation.TabIndex = 5
        Me.btnDataFileLocation.Text = "..."
        Me.btnDataFileLocation.UseVisualStyleBackColor = True
        '
        'txtDataFileLocation
        '
        Me.txtDataFileLocation.Location = New System.Drawing.Point(67, 71)
        Me.txtDataFileLocation.Name = "txtDataFileLocation"
        Me.txtDataFileLocation.Size = New System.Drawing.Size(434, 20)
        Me.txtDataFileLocation.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 74)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Data File:"
        '
        'btnANNFileLocation
        '
        Me.btnANNFileLocation.Location = New System.Drawing.Point(507, 11)
        Me.btnANNFileLocation.Name = "btnANNFileLocation"
        Me.btnANNFileLocation.Size = New System.Drawing.Size(25, 23)
        Me.btnANNFileLocation.TabIndex = 2
        Me.btnANNFileLocation.Text = "..."
        Me.btnANNFileLocation.UseVisualStyleBackColor = True
        '
        'txtANNFileLocation
        '
        Me.txtANNFileLocation.Location = New System.Drawing.Point(67, 13)
        Me.txtANNFileLocation.Name = "txtANNFileLocation"
        Me.txtANNFileLocation.Size = New System.Drawing.Size(434, 20)
        Me.txtANNFileLocation.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(52, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "ANN File:"
        '
        'lbStockOutput1
        '
        Me.lbStockOutput1.FormattingEnabled = True
        Me.lbStockOutput1.Location = New System.Drawing.Point(559, 27)
        Me.lbStockOutput1.Name = "lbStockOutput1"
        Me.lbStockOutput1.Size = New System.Drawing.Size(90, 264)
        Me.lbStockOutput1.TabIndex = 1
        '
        'lbStockOutput2
        '
        Me.lbStockOutput2.Enabled = False
        Me.lbStockOutput2.FormattingEnabled = True
        Me.lbStockOutput2.Location = New System.Drawing.Point(655, 27)
        Me.lbStockOutput2.Name = "lbStockOutput2"
        Me.lbStockOutput2.Size = New System.Drawing.Size(90, 264)
        Me.lbStockOutput2.TabIndex = 2
        '
        'lbStockOutput3
        '
        Me.lbStockOutput3.Enabled = False
        Me.lbStockOutput3.FormattingEnabled = True
        Me.lbStockOutput3.Location = New System.Drawing.Point(751, 27)
        Me.lbStockOutput3.Name = "lbStockOutput3"
        Me.lbStockOutput3.Size = New System.Drawing.Size(90, 264)
        Me.lbStockOutput3.TabIndex = 3
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtOutputNeurons)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.txtHiddenLayers)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.txtHiddenNeurons)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.txtInputNeurons)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 129)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(403, 46)
        Me.GroupBox2.TabIndex = 4
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Network Information"
        '
        'txtOutputNeurons
        '
        Me.txtOutputNeurons.Enabled = False
        Me.txtOutputNeurons.Location = New System.Drawing.Point(348, 16)
        Me.txtOutputNeurons.Name = "txtOutputNeurons"
        Me.txtOutputNeurons.Size = New System.Drawing.Size(41, 20)
        Me.txtOutputNeurons.TabIndex = 7
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(295, 19)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(47, 13)
        Me.Label6.TabIndex = 6
        Me.Label6.Text = "Outputs:"
        '
        'txtHiddenLayers
        '
        Me.txtHiddenLayers.Enabled = False
        Me.txtHiddenLayers.Location = New System.Drawing.Point(248, 16)
        Me.txtHiddenLayers.Name = "txtHiddenLayers"
        Me.txtHiddenLayers.Size = New System.Drawing.Size(41, 20)
        Me.txtHiddenLayers.TabIndex = 5
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(190, 16)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(52, 13)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "H-Layers:"
        '
        'txtHiddenNeurons
        '
        Me.txtHiddenNeurons.Enabled = False
        Me.txtHiddenNeurons.Location = New System.Drawing.Point(143, 13)
        Me.txtHiddenNeurons.Name = "txtHiddenNeurons"
        Me.txtHiddenNeurons.Size = New System.Drawing.Size(41, 20)
        Me.txtHiddenNeurons.TabIndex = 3
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(98, 16)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(44, 13)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Hidden:"
        '
        'txtInputNeurons
        '
        Me.txtInputNeurons.Enabled = False
        Me.txtInputNeurons.Location = New System.Drawing.Point(51, 13)
        Me.txtInputNeurons.Name = "txtInputNeurons"
        Me.txtInputNeurons.Size = New System.Drawing.Size(41, 20)
        Me.txtInputNeurons.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(39, 13)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Inputs:"
        '
        'lbDesiredStockOutput3
        '
        Me.lbDesiredStockOutput3.Enabled = False
        Me.lbDesiredStockOutput3.FormattingEnabled = True
        Me.lbDesiredStockOutput3.Location = New System.Drawing.Point(751, 297)
        Me.lbDesiredStockOutput3.Name = "lbDesiredStockOutput3"
        Me.lbDesiredStockOutput3.Size = New System.Drawing.Size(90, 264)
        Me.lbDesiredStockOutput3.TabIndex = 7
        '
        'lbDesiredStockOutput2
        '
        Me.lbDesiredStockOutput2.Enabled = False
        Me.lbDesiredStockOutput2.FormattingEnabled = True
        Me.lbDesiredStockOutput2.Location = New System.Drawing.Point(655, 297)
        Me.lbDesiredStockOutput2.Name = "lbDesiredStockOutput2"
        Me.lbDesiredStockOutput2.Size = New System.Drawing.Size(90, 264)
        Me.lbDesiredStockOutput2.TabIndex = 6
        '
        'lbDesiredStockOutput1
        '
        Me.lbDesiredStockOutput1.FormattingEnabled = True
        Me.lbDesiredStockOutput1.Location = New System.Drawing.Point(559, 297)
        Me.lbDesiredStockOutput1.Name = "lbDesiredStockOutput1"
        Me.lbDesiredStockOutput1.Size = New System.Drawing.Size(90, 264)
        Me.lbDesiredStockOutput1.TabIndex = 5
        '
        'lbDataFileInputs
        '
        Me.lbDataFileInputs.FormattingEnabled = True
        Me.lbDataFileInputs.Location = New System.Drawing.Point(12, 179)
        Me.lbDataFileInputs.Name = "lbDataFileInputs"
        Me.lbDataFileInputs.Size = New System.Drawing.Size(532, 381)
        Me.lbDataFileInputs.TabIndex = 8
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.Filter = "ANN Files|*.ANN"
        '
        'OpenFileDialog2
        '
        Me.OpenFileDialog2.Filter = "Data Files|*.DATA"
        '
        'btnCalculate
        '
        Me.btnCalculate.Enabled = False
        Me.btnCalculate.Location = New System.Drawing.Point(421, 134)
        Me.btnCalculate.Name = "btnCalculate"
        Me.btnCalculate.Size = New System.Drawing.Size(123, 40)
        Me.btnCalculate.TabIndex = 8
        Me.btnCalculate.Text = "Calculate"
        Me.btnCalculate.UseVisualStyleBackColor = True
        '
        'lbStockOutput4
        '
        Me.lbStockOutput4.Enabled = False
        Me.lbStockOutput4.FormattingEnabled = True
        Me.lbStockOutput4.Location = New System.Drawing.Point(847, 27)
        Me.lbStockOutput4.Name = "lbStockOutput4"
        Me.lbStockOutput4.Size = New System.Drawing.Size(90, 264)
        Me.lbStockOutput4.TabIndex = 9
        '
        'lbDesiredStockOutput4
        '
        Me.lbDesiredStockOutput4.Enabled = False
        Me.lbDesiredStockOutput4.FormattingEnabled = True
        Me.lbDesiredStockOutput4.Location = New System.Drawing.Point(847, 297)
        Me.lbDesiredStockOutput4.Name = "lbDesiredStockOutput4"
        Me.lbDesiredStockOutput4.Size = New System.Drawing.Size(90, 264)
        Me.lbDesiredStockOutput4.TabIndex = 10
        '
        'lbStockOutput5
        '
        Me.lbStockOutput5.Enabled = False
        Me.lbStockOutput5.FormattingEnabled = True
        Me.lbStockOutput5.Location = New System.Drawing.Point(943, 27)
        Me.lbStockOutput5.Name = "lbStockOutput5"
        Me.lbStockOutput5.Size = New System.Drawing.Size(90, 264)
        Me.lbStockOutput5.TabIndex = 11
        '
        'lbDesiredStockOutput5
        '
        Me.lbDesiredStockOutput5.Enabled = False
        Me.lbDesiredStockOutput5.FormattingEnabled = True
        Me.lbDesiredStockOutput5.Location = New System.Drawing.Point(943, 297)
        Me.lbDesiredStockOutput5.Name = "lbDesiredStockOutput5"
        Me.lbDesiredStockOutput5.Size = New System.Drawing.Size(90, 264)
        Me.lbDesiredStockOutput5.TabIndex = 12
        '
        'OpenFileDialog3
        '
        Me.OpenFileDialog3.Filter = "Header Files|*.Header"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1044, 24)
        Me.MenuStrip1.TabIndex = 13
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HowToUseThisProgramToolStripMenuItem, Me.ExitToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FileToolStripMenuItem.Text = "&File"
        '
        'HowToUseThisProgramToolStripMenuItem
        '
        Me.HowToUseThisProgramToolStripMenuItem.Name = "HowToUseThisProgramToolStripMenuItem"
        Me.HowToUseThisProgramToolStripMenuItem.Size = New System.Drawing.Size(212, 22)
        Me.HowToUseThisProgramToolStripMenuItem.Text = "&How To Use This Program"
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(212, 22)
        Me.ExitToolStripMenuItem.Text = "E&xit"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1044, 572)
        Me.Controls.Add(Me.lbDesiredStockOutput5)
        Me.Controls.Add(Me.lbStockOutput5)
        Me.Controls.Add(Me.lbDesiredStockOutput4)
        Me.Controls.Add(Me.lbStockOutput4)
        Me.Controls.Add(Me.btnCalculate)
        Me.Controls.Add(Me.lbDataFileInputs)
        Me.Controls.Add(Me.lbDesiredStockOutput3)
        Me.Controls.Add(Me.lbDesiredStockOutput2)
        Me.Controls.Add(Me.lbDesiredStockOutput1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.lbStockOutput3)
        Me.Controls.Add(Me.lbStockOutput2)
        Me.Controls.Add(Me.lbStockOutput1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Form1"
        Me.Text = "Testing Verification App"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnDataFileLocation As System.Windows.Forms.Button
    Friend WithEvents txtDataFileLocation As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnANNFileLocation As System.Windows.Forms.Button
    Friend WithEvents txtANNFileLocation As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lbStockOutput1 As System.Windows.Forms.ListBox
    Friend WithEvents lbStockOutput2 As System.Windows.Forms.ListBox
    Friend WithEvents lbStockOutput3 As System.Windows.Forms.ListBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents lbDesiredStockOutput3 As System.Windows.Forms.ListBox
    Friend WithEvents lbDesiredStockOutput2 As System.Windows.Forms.ListBox
    Friend WithEvents lbDesiredStockOutput1 As System.Windows.Forms.ListBox
    Friend WithEvents lbDataFileInputs As System.Windows.Forms.ListBox
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents OpenFileDialog2 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents txtOutputNeurons As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtHiddenLayers As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtHiddenNeurons As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtInputNeurons As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnCalculate As System.Windows.Forms.Button
    Friend WithEvents lbStockOutput4 As System.Windows.Forms.ListBox
    Friend WithEvents lbDesiredStockOutput4 As System.Windows.Forms.ListBox
    Friend WithEvents lbStockOutput5 As System.Windows.Forms.ListBox
    Friend WithEvents lbDesiredStockOutput5 As System.Windows.Forms.ListBox
    Friend WithEvents btnStockFile As System.Windows.Forms.Button
    Friend WithEvents txtStockFile As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents OpenFileDialog3 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HowToUseThisProgramToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

End Class
