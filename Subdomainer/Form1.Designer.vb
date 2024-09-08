<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        txtDomain = New TextBox()
        RichTextBox1 = New RichTextBox()
        btnScan = New Button()
        ProgressBar1 = New ProgressBar()
        SuspendLayout()
        ' 
        ' txtDomain
        ' 
        txtDomain.BorderStyle = BorderStyle.FixedSingle
        txtDomain.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        txtDomain.ForeColor = Color.Red
        txtDomain.Location = New Point(12, 17)
        txtDomain.Name = "txtDomain"
        txtDomain.Size = New Size(489, 27)
        txtDomain.TabIndex = 0
        txtDomain.TextAlign = HorizontalAlignment.Center
        ' 
        ' RichTextBox1
        ' 
        RichTextBox1.BorderStyle = BorderStyle.None
        RichTextBox1.Location = New Point(12, 113)
        RichTextBox1.Name = "RichTextBox1"
        RichTextBox1.Size = New Size(631, 280)
        RichTextBox1.TabIndex = 2
        RichTextBox1.Text = ""
        ' 
        ' btnScan
        ' 
        btnScan.FlatStyle = FlatStyle.Flat
        btnScan.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnScan.ForeColor = Color.Lime
        btnScan.Location = New Point(513, 15)
        btnScan.Name = "btnScan"
        btnScan.Size = New Size(130, 29)
        btnScan.TabIndex = 3
        btnScan.Text = "Get Subdomain"
        btnScan.UseVisualStyleBackColor = True
        ' 
        ' ProgressBar1
        ' 
        ProgressBar1.Location = New Point(12, 62)
        ProgressBar1.Name = "ProgressBar1"
        ProgressBar1.Size = New Size(631, 29)
        ProgressBar1.TabIndex = 4
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(8.0F, 20.0F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.Black
        ClientSize = New Size(655, 405)
        Controls.Add(ProgressBar1)
        Controls.Add(btnScan)
        Controls.Add(RichTextBox1)
        Controls.Add(txtDomain)
        FormBorderStyle = FormBorderStyle.FixedToolWindow
        Name = "Form1"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Mini Domain Tool - LSDeep"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents txtDomain As TextBox
    Friend WithEvents RichTextBox1 As RichTextBox
    Friend WithEvents btnScan As Button
    Friend WithEvents ProgressBar1 As ProgressBar

End Class
