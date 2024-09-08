Imports System
Imports System.Windows.Forms
Imports System.Net
Imports System.Threading.Tasks
Imports System.Net.NetworkInformation
Imports System.Text.RegularExpressions
Imports System.Net.Sockets

' Dev By: @LSDeep
' Tele: https://t.me/lsd33p

Public Class Form1
    Private cancellationTokenSource As New Threading.CancellationTokenSource()
    Private totalSubdomains As Integer = 0
    Private processedSubdomains As Integer = 0

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ProgressBar1.Minimum = 0
        ProgressBar1.Maximum = 100
        ProgressBar1.Value = 0

        txtDomain.Text = "Enter domain to scan"
        txtDomain.ForeColor = System.Drawing.Color.Gray


        RichTextBox1.ReadOnly = True
    End Sub
    Private Sub txtDomain_Enter(sender As Object, e As EventArgs) Handles txtDomain.Enter
        If txtDomain.Text = "Enter domain to scan" Then
            txtDomain.Text = ""
            txtDomain.ForeColor = System.Drawing.Color.Black
        End If
    End Sub

    Private Sub txtDomain_Leave(sender As Object, e As EventArgs) Handles txtDomain.Leave
        If String.IsNullOrWhiteSpace(txtDomain.Text) Then
            txtDomain.Text = "Enter domain to scan"
            txtDomain.ForeColor = System.Drawing.Color.Gray
        End If
    End Sub

    Private Async Sub btnScan_Click(sender As Object, e As EventArgs) Handles btnScan.Click
        If btnScan.Text = "Start Scan" Then
            Dim domain As String = txtDomain.Text.Trim()
            If String.IsNullOrEmpty(domain) Then
                MessageBox.Show("Please enter a domain to scan.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            btnScan.Text = "Stop Scan"
            RichTextBox1.Clear()
            ProgressBar1.Value = 0
            cancellationTokenSource = New Threading.CancellationTokenSource()
            Await ScanSubdomainsAsync(domain, cancellationTokenSource.Token)
            ProgressBar1.Value = 100
            btnScan.Text = "Start Scan"
        Else
            cancellationTokenSource.Cancel()
            btnScan.Text = "Start Scan"
        End If
    End Sub

    Private Async Function ScanSubdomainsAsync(domain As String, cancellationToken As Threading.CancellationToken) As Task
        Try

            Dim nameservers = Await Dns.GetHostAddressesAsync(domain)
            Dim subdomains = Await PerformZoneTransferAsync(domain, nameservers)

            If subdomains.Count = 0 Then
                subdomains = Await BruteforceSubdomainsAsync(domain, cancellationToken)
            End If

            totalSubdomains = subdomains.Count
            processedSubdomains = 0

            For Each subdomain In subdomains
                If cancellationToken.IsCancellationRequested Then
                    Exit For
                End If

                Dim fullDomain = $"{subdomain}.{domain}"
                Dim ipAddresses = Await Dns.GetHostAddressesAsync(fullDomain)

                If ipAddresses.Length > 0 Then
                    Dim ipRange = GetIPRange(ipAddresses(0))
                    Dim isOnline = Await IsSubdomainOnlineAsync(fullDomain)
                    Dim status = If(isOnline, "Online", "Offline")
                    Dim result = $"Subdomain: {fullDomain} | IP: {ipAddresses(0)} | Range: {ipRange} | Status: {status}"
                    UpdateUI(result)
                End If

                processedSubdomains += 1
                UpdateProgressBar()
            Next
        Catch ex As Exception
            UpdateUI($"Error: {ex.Message}")
        End Try
    End Function

    Private Async Function PerformZoneTransferAsync(domain As String, nameservers As IPAddress()) As Task(Of List(Of String))
        Dim subdomains As New List(Of String)

        Return subdomains
    End Function

    Private Async Function BruteforceSubdomainsAsync(domain As String, cancellationToken As Threading.CancellationToken) As Task(Of List(Of String))
        Dim subdomains As New List(Of String)
        Dim commonPrefixes = {"www", "mail", "ftp", "smtp", "pop", "m", "blog", "dev", "stage", "api", "admin", "test", "portal", "secure", "vpn", "remote", "auth", "web", "intranet", "extranet", "apps", "cdn", "adm", "cpanel", "erp", "forum", "news"}
        For Each prefix In commonPrefixes
            If cancellationToken.IsCancellationRequested Then
                Exit For
            End If

            Dim fullDomain = $"{prefix}.{domain}"
            Try
                Dim ipAddresses = Await Dns.GetHostAddressesAsync(fullDomain)
                If ipAddresses.Length > 0 Then
                    subdomains.Add(prefix)
                End If
            Catch

            End Try
        Next

        Return subdomains
    End Function

    Private Function GetIPRange(ip As IPAddress) As String
        Dim octets = ip.ToString().Split("."c)
        Return $"{octets(0)}.{octets(1)}.{octets(2)}.0/24"
    End Function

    Private Async Function IsSubdomainOnlineAsync(subdomain As String) As Task(Of Boolean)
        Try
            Using client As New TcpClient()
                Await client.ConnectAsync(subdomain, 80)
                Return True
            End Using
        Catch
            Return False
        End Try
    End Function

    Private Sub UpdateUI(message As String)
        If RichTextBox1.InvokeRequired Then
            RichTextBox1.Invoke(Sub() UpdateUI(message))
        Else
            RichTextBox1.AppendText(message & Environment.NewLine)
        End If
    End Sub

    Private Sub UpdateProgressBar()
        If ProgressBar1.InvokeRequired Then
            ProgressBar1.Invoke(Sub() UpdateProgressBar())
        Else
            Dim percentComplete As Integer = CInt((processedSubdomains / totalSubdomains) * 100)
            ProgressBar1.Value = Math.Min(percentComplete, 100)
        End If
    End Sub
End Class