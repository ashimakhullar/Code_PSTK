#######################################################################################################################
# This file will be removed when PowerCLI is uninstalled. To make your own scripts run when PowerCLI starts, create a
# file named "Initialize-PowerCLIEnvironment_Custom.ps1" in the same directory as this file, and place your scripts in
# it. The "Initialize-PowerCLIEnvironment_Custom.ps1" is not automatically deleted when PowerCLI is uninstalled.
#######################################################################################################################

$productName = "Cisco Powershell Toolkit"


# Launch text
write-host "          Welcome to $productName!"
write-host ""
write-host "Log in to a HXConnect Cluster:              " -NoNewLine
write-host "                   Connect-HXServer" -foregroundcolor yellow
write-host "Once you've connected, display all Protected virtual machines: " -NoNewLine
write-host "Get-ProtectedVM" -foregroundcolor yellow
write-host "If you need more help, visit the : " -NoNewLine
write-host "                            Get-help" -foregroundcolor yellow
write-host ""
write-host "       Copyright (C) Cisco, Inc. All rights reserved."
write-host ""
write-host ""


import-module .\SP_powershell.dll
