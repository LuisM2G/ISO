```Powershell
$agrupar = foreach($conexion in Get-NetTCPConnection)
{
    #"-----------------------------"
    #(Get-Process -id $conexion.OwningProcess).Name
    foreach($descripcion in (Get-Process -id $conexion.OwningProcess).Modules)
    {
        $descripcion.description
    }
}

$agruparm = foreach($linea in ($agrupar | Group-Object).Name)
{
    foreach($salendepartir in $linea.split(" "))
    {
        if($salendepartir.length -ge 3)
        {
            $salendepartir
        }
    }
}

$agruparm | Group-Object | sort count -Descending | select -First 4l


########## Listar los ficheros DLL de los procesos con conexión TCP cuyas DLL contentan la palabra web

foreach($conexion in Get-NetTCPConnection)
{
    "-----------------------------"
    (Get-Process -id $conexion.OwningProcess).Name
    foreach($descripcion in (Get-Process -id $conexion.OwningProcess).Modules)
    {
        if($descripcion.description -match "web")
        {
            Get-FileHash $descripcion.FileName
        }
    }
}

