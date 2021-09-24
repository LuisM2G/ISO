# SCRIPT DE ESTRUCTURA DE UN POSIBLE HOSPITAL Y GPOS
### CON MEZCLA DE VARIABLES, FICHEROS Y MENU
```Powershell
#VARIABLES
$dominio = "dc=ad,dc=luis,dc=com,dc=ar"
$dominio2 = "ou=HOSPITALHM,dc=ad,dc=luis,dc=com,dc=ar"
$primeraplanta = "PRIMERAPLANTA"
$segundaplanta = "SEGUNDAPLANTA"
$terceraplanta = "TERCERAPLANTA"
$bajoplanta = "BAJOPLANTA"
$sotano = "SOTANO"
$hospital = "HOSPITALHM"
$mostradores = "MOSTRADORES"
$informacion = "INFORMACION"
$medicos = "MEDICOS"
$wifi =  "Wi-Fi"
$practicas = "PRACTICAS"


#ME UBICO EN UNA CARPETA LLAMADA HM DONDE IRAN TODOS LOS ARCHIVOS A LEER, QUE SE UBICA EN EL ESCRITORIO
SET-LOCATION C:\Users\Administrador\Desktop\HM

#METO EN UN FICHERO EL MUNICIPIO DE LOS HOSPITALES HM
$primeraplanta,$segundaplanta,$terceraplanta,$bajoplanta,$sotano > HospitalHM.txt

#CREO UNA UNIDAD ORGANIZATIVA LLAMADA HOSPITAL HM ,DE MOSTOLES
New-ADOrganizationalUnit -Name "$hospital" -Path "$dominio"

#FOREACH PARA CREAR LA ESTRUCTURA, DESHABILITANDO EL BORRADO ACCIDENTAL
foreach($uno in gc .\HospitalHM.txt)
{
    $dos = ($uno).split(",")
    New-ADOrganizationalUnit -Name "$uno" -Path "$dominio2" -ProtectedFromAccidentalDeletion:$false
}

#SECCIONES
$mostradores,$informacion,$medicos,$wifi,$practicas > SECCIONES.txt

##CREAMOS GRUPOS DEL HOSPITAL, SECCIONES LEYENDO DE UN FICHERO
foreach($secciones in gc .\SECCIONES.txt)
{
New-ADGroup -GroupScope DomainLocal -DisplayName "$secciones" -Name "$secciones" -path "$dominio2"
}

##CREO PARA CADA PLANTA UN TIPO DE USUARIO PARA CADA TIPO DE TRABAJADOR, Y EN FICHEROS INDEPENDIENTES SEGUN LA PLANTA
foreach($numero in 1..5)
{
 "$medicos" + "$numero" | out-file usuarios"$numero".txt -Append
 "$informacion" + "$numero" | out-file usuarios"$numero".txt -Append
 "$mostradores" + "$numero" | out-file usuarios"$numero".txt -Append
 "$wifi" + "$numero" | out-file usuarios"$numero".txt -Append
 "$practicas" + "$numero" | out-file usuarios"$numero".txt -Append
}


##PARA CADA PLANTA HAY UN GRUPO DE TRABAJADORES, CON UN SWITCH METO CADA FICHERO CON SUS TRABAJADORES INDEPENDIENTEMENTE DE OTRAS PLANTAS.
while($opciones -ne 0)
{
    $opciones = Read-Host{
    "0, para abortar operacion"
    "1, para usuarios de la primera planta"
    "2, para usuarios de la segunda planta"
    "3, para usuarios de la tercera planta"
    "4, para usuarios de la bajo planta"
    "5, para usuarios de la sotano"
    }
     
    switch($opciones)
    {
    "1"{
        foreach($ADusuarios in gc .\usuarios1.txt)
            {
             New-ADUser -DisplayName "$ADusuarios" -Name "$ADusuarios" -Enabled:$true -Path "ou=PRIMERAPLANTA,ou=HOSPITALHM,$dominio" -AccountPassword (ConvertTo-SecureString -string "P@ssw0rd" -AsPlainText -Force)
            }
       }


    "2"{
        foreach($ADusuarios in gc .\usuarios2.txt)
            {
            New-ADUser -DisplayName "$ADusuarios" -Name "$ADusuarios" -Enabled:$true -Path "OU=SEGUNDAPLANTA,ou=HOSPITALHM,$dominio" -AccountPassword (ConvertTo-SecureString -string "P@ssw0rd" -AsPlainText -Force)
            }
        }


    "3"{
        foreach($ADusuarios in gc .\usuarios3.txt)
            {
            New-ADUser -DisplayName "$ADusuarios" -Name "$ADusuarios" -Enabled:$true -Path "OU=TERCERAPLANTA,ou=HOSPITALHM,$dominio" -AccountPassword (ConvertTo-SecureString -string "P@ssw0rd" -AsPlainText -Force)
            }
        }


    "4"{
        foreach($ADusuarios in gc .\usuarios4.txt)
            {
            New-ADUser -DisplayName "$ADusuarios" -Name "$ADusuarios" -Enabled:$true -Path "OU=BAJOPLANTA,ou=HOSPITALHM,$dominio" -AccountPassword (ConvertTo-SecureString -string "P@ssw0rd" -AsPlainText -Force)
            }
        }

    "5"{
        foreach($ADusuarios in gc .\usuarios5.txt)
            {
            New-ADUser -DisplayName "$ADusuarios" -Name "$ADusuarios" -Enabled:$true -Path "OU=SOTANO,ou=HOSPITALHM,$dominio" -AccountPassword (ConvertTo-SecureString -string "P@ssw0rd" -AsPlainText -Force)
            }
        }
    }
}

## cojo una lista de las unidades organizativas que hay
Get-ADOrganizationalunit -filter * | select name

##
while($pregunta -ne 0)
{
    $pregunta = Read-Host{
   
    "0, para abortar operacion"
    "1, Desactivar CMD"
    "2, Desactivar control de estado de equipo"
    "3, Desactivar internet"
    "4, Fondo especifico"
    "5, Prohibir la conexion y desconexion del acceso remoto"
    "6, Quitar panel y configuracion"
   
    }
     
    switch($pregunta)
    {
    "1"{ #### desabilito el cmd en algunas plantas para que nadie toque posibles configuraciones o vea informacion
        $PREGUNTA1 = Read-Host "selecciona unidad organizativa?"
        New-GPO -Name "DESACTIVACION CMD" -Comment "Deshabilita el CMD"
        Set-GPRegistryValue -Name "DESACTIVACION CMD" -Key "HKCU\Software\Policies\Microsoft\Windows\System" -ValueName DisableCMD -Type DWord -Value 00000002
        New-GPLink -Name "DESACTIVACION CMD" -Target "ou= $PREGUNTA1,ou=HOSPITALHM,$dominio"
        }
       
    "2"{ #### deniego cualquier posibilidad que no sea que los ordenadores esten encendidos en la primera planta (Urgencias)
        $PREGUNTA2 = Read-Host "selecciona unidad organizativa?"
        New-GPO -Name "CONTROL DE ESTADO DE EQUIPO" -Comment "Deshabilita el control de estado de equipo"
        Set-GPRegistryValue -Name "CONTROL DE ESTADO DE EQUIPO" -Key "HKLM\Software\Microsoft\Windows\CurrentVersion\Policies\Explorer" -ValueName HidePowerOptions -Type DWord -Value 00000001
        New-GPLink -Name "CONTROL DE ESTADO DE EQUIPO" -Target "ou= $PREGUNTA2,ou=HOSPITALHM,$dominio"
        }

    "3"{ #### deniego la conexion de internet en el sotano debido a la presencia de maquinas magneticas muy potentes y asi evitar interferencias
        $PREGUNTA3 = Read-Host "selecciona unidad organizativa?"
        New-GPO -Name "DESACTIVACION DE INTERNET" -Comment "Deshabilita internet"
        Set-GPRegistryValue -Name "DESACTIVACION DE INTERNET" -Key "HKCU\Software\Policies\Microsoft\InternetManagement" -ValueName RestrictCommunication -Type DWord -Value 00000001
        New-GPLink -Name "DESACTIVACION DE INTERNET" -Target "ou= $PREGUNTA3,ou=HOSPITALHM,$dominio"
        }

    "4"{ #### se aplica el mismo fondos a todos los pcs del hospital con el logo de este
        $PREGUNTA4 = Read-Host "selecciona unidad organizativa?"
        New-GPO -Name "FONDO ESPECIFICO" -Comment "FONDO ESPECIFICO"
        Set-GPRegistryValue -Name "FONDO ESPECIFICO" -Key "HKLM\Software\Policies\Microsoft\Windows\Personalization" -ValueName ForceStartBackground -Type DWord -Value 00000000
        New-GPLink -Name "FONDO ESPECIFICO" -Target "ou= $PREGUNTA4,ou=HOSPITALHM,$dominio"
        }

    "5"{ #### en plantas criticas en donde tiene que haber gran presencia de seguridad desabilito el acceso remoto
        $PREGUNTA5 = Read-Host "selecciona unidad organizativa?"
        New-GPO -Name "PROHIBIR CONEXIONES REMOTAS" -Comment "PROHIBIR CONEXIONES REMOTAS"
        Set-GPRegistryValue -Name "PROHIBIR CONEXIONES REMOTAS" -Key "HKCU\Software\Policies\Microsoft\Windows\Network  Connections" -ValueName NC_RasConnect -Type DWord -Value 00000000
        New-GPLink -Name "PROHIBIR CONEXIONES REMOTAS" -Target "ou= $PREGUNTA5,ou=HOSPITALHM,$dominio"
        }

    "6"{ #### desactivo el panel de control en todas las plantas para evitar
        $PREGUNTA6 = Read-Host "selecciona unidad organizativa?"
        New-GPO -Name "DESACTIVACION PANEL DE CONTROL" -Comment "DESACTIVACION PANEL DE CONTROL"
        Set-GPRegistryValue -Name "DESACTIVACION PANEL DE CONTROL" -Key "HKCU\Software\Microsoft\Windows\CurrentVersion\Policies\Explorer" -ValueName NoControlPanel -Type DWord -Value 00000001
        New-GPLink -Name "DESACTIVACION PANEL DE CONTROL" -Target "ou= $PREGUNTA6,ou=HOSPITALHM,$dominio"
        }
    }
}

```


