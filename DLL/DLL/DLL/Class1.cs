using System;
using System.Management.Automation;  // Windows PowerShell namespace.

namespace PowerSh
{
    public class PowerSh
    {
        public static void CrearUsuario()
        {

            //Create a PowerShell object.
            PowerShell ps = PowerShell.Create();

            //New-LocalUser usuario -Password (ConvertTo-SecureString "11234aaadsf" -asplaintext -force)
            ps.AddScript("New-LocalUser usuario -Password (ConvertTo-SecureString '11234aaadsf' -asplaintext -force)");

            IAsyncResult result = ps.BeginInvoke();

            // do something else until execution has completed.
            // this could be sleep/wait, or perhaps some other work
            while (result.IsCompleted == false)
            {
            }

        } // End Main.
        public static void ListarUsuario()
        {

            //Create a PowerShell object.
            PowerShell ps = PowerShell.Create();

            //New-LocalUser usuario -Password (ConvertTo-SecureString "11234aaadsf" -asplaintext -force)
            ps.AddScript("Get-LocalUser");

            IAsyncResult result = ps.BeginInvoke();

            // do something else until execution has completed.
            // this could be sleep/wait, or perhaps some other work
            while (result.IsCompleted == false)
            {
            }
            foreach (PSObject result2 in ps.EndInvoke(result))
            {
                Console.WriteLine("{0}",
                        result2.Members["Name"].Value);
            } // End foreach.
            System.Console.WriteLine("Hit any key to exit.");
        } // End Main.
    } // End HostPs1.
}