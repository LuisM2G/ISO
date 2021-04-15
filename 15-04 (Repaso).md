# Jueves
### REPASO
```Powershell
Get-Psprovider   #declarantes
--
Get-Psdrive      #mapea
--
Get-WmiObject -class Win32_process  #llamada wmi
--
Get-Process | Get-Member  #variantes
--
$error[0]
--
Start-Job -Scriptblock {ps}   #tarea 
Get-Job -id 1
Receive-Job -id 1
--

```

