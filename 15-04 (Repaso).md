# Jueves
### REPASO
```Powershell
Get-Psprovider   #declarantes
--
Get-Psdrive      #mapea
--
Get-WmiObject -class win32_process  #llamada wmi
--
Get-process | Get-member  #variantes
--
$error[0]
--
start-job -scriptblock {ps}   #tarea 
get-job -id 1
receive-job -id 1
--

```

