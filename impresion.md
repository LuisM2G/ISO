```powershell

foreach($archivos in (Get-ChildItem "D:\xampp\htdocs\uploads").name)
{
Start-Process -FilePath "D:\xampp\htdocs\uploads\$archivos" -Verb Print | Out-Printer "Microsoft Print to PDF"
start-sleep -Milliseconds 2000
[System.Windows.Forms.SendKeys]::SendWait("{ENTER}")
start-sleep -Milliseconds 2000
[System.Windows.Forms.SendKeys]::SendWait($archivos)
start-sleep -Milliseconds 2000
[System.Windows.Forms.SendKeys]::SendWait("{ENTER}")
start-sleep -Milliseconds 2000
}

```
