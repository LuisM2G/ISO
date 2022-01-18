```powershell
Get-Printer
- - -
Get-PrintJob 
- - -
"hola" | Out-Printer "Samsung ML-2580 Series PCL6"
- - -
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
- - -
for(1){
  $id = ""
  $id = Get-PrintJob -PrinterName "Samsung ML-2580 Series PCL6"
  $id.id
  $id.Size
    if ($id.size -eq "42406")
      {
        Remove-PrintJob -id $id.id -PrinterName "Samsung ML-2580 Series PCL6"
      }
    }
```
