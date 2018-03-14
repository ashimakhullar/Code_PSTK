#######################################################################################################################
# This file will be removed when PowerCLI is uninstalled. To make your own scripts run when PowerCLI starts, create a
# file named "Initialize-PowerCLIEnvironment_Custom.ps1" in the same directory as this file, and place your scripts in
# it. The "Initialize-PowerCLIEnvironment_Custom.ps1" is not automatically deleted when PowerCLI is uninstalled.
#######################################################################################################################
param([bool]$promptForCEIP = $false)

# List of modules to be loaded
$moduleList = @(
    "VMware.VimAutomation.Core",
    ".\SP_powershell.dll",
    "VMware.VimAutomation.Cloud",
    "VMware.VimAutomation.License"
    )

$productName = "Cisco Powershell Toolkit"
$productShortName = "Cisco Powershell Toolkit"

$loadingActivity = "Loading $productName"
$script:completedActivities = 0
$script:percentComplete = 0
$script:currentActivity = ""
$script:totalActivities = `
   $moduleList.Count + 1

function ReportStartOfActivity($activity) {
   $script:currentActivity = $activity
   Write-Progress -Activity $loadingActivity -CurrentOperation $script:currentActivity -PercentComplete $script:percentComplete
}
function ReportFinishedActivity() {
   $script:completedActivities++
   $script:percentComplete = (100.0 / $totalActivities) * $script:completedActivities
   $script:percentComplete = [Math]::Min(99, $percentComplete)
   
   Write-Progress -Activity $loadingActivity -CurrentOperation $script:currentActivity -PercentComplete $script:percentComplete
}

# Load modules
function LoadModules(){
   ReportStartOfActivity "Searching for $productShortName module components..."
   
   $loaded = Get-Module -Name $moduleList -ErrorAction Ignore | % {$_.Name}
   $registered = Get-Module -Name $moduleList -ListAvailable -ErrorAction Ignore | % {$_.Name}
   $notLoaded = $registered | ? {$loaded -notcontains $_}
   
   ReportFinishedActivity
   
   foreach ($module in $registered) {
      if ($loaded -notcontains $module) {
		 ReportStartOfActivity "Loading module $module"
         
		 Import-Module $module
		 
		 ReportFinishedActivity
      }
   }
}

LoadModules


# Launch text
write-host "          Welcome to $productName!"
write-host ""
write-host "Log in to a HXConnect Cluster:              " -NoNewLine
write-host "Connect-HXServer" -foregroundcolor yellow
write-host "Once you've connected, display all Protected virtual machines: " -NoNewLine
write-host "Get-ProtectedVM" -foregroundcolor yellow
write-host "If you need more help, visit the : " -NoNewLine
write-host "Get-help" -foregroundcolor yellow
write-host ""
write-host "       Copyright (C) Cisco, Inc. All rights reserved."
write-host ""
write-host ""


Write-Progress -Activity $loadingActivity -Completed

cd \

