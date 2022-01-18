```powershell

foreach($archivos in (Get-ChildItem "D:\xampp\htdocs\uploads").name)
{
$archivo = $archivos.Split(".")[0]
Start-Process -FilePath "D:\xampp\htdocs\uploads\$archivos" -Verb Print | Out-Printer "Microsoft Print to PDF"
start-sleep -Milliseconds 2000
[System.Windows.Forms.SendKeys]::SendWait("{ENTER}")
start-sleep -Milliseconds 2000
[System.Windows.Forms.SendKeys]::SendWait("C:\Users\PHINX\Desktop\$archivo")
start-sleep -Milliseconds 2000
[System.Windows.Forms.SendKeys]::SendWait("{ENTER}")
start-sleep -Milliseconds 2000
}

```
