Set-StrictMode -Version 2

$RootDirectory = $env:BUILD_REPOSITORY_LOCALPATH
$VersionPath = Join-PathEx $RootDirectory build powershell version.ps1 -Resolve
$CommitHash = (git log -n 1 --pretty=format:'%H' $RootDirectory).Trim().ToLowerInvariant()
$VersionInformation = & $VersionPath `
    -RootDirectory $RootDirectory `
    -FourthDottedComponentOverride $env:BUILD_BUILDID `
    -CommitHash $CommitHash `
    -Verbose

# Update Azure Pipelines variables with calculated version information

Write-Host "##vso[task.setvariable variable=VersionInformation.FullVersionString]$($VersionInformation.FullVersionString))"
Write-Host "##vso[task.setvariable variable=VersionInformation.FullVersionStringWithSuffix]$($VersionInformation.FullVersionStringWithSuffix))"
Write-Host "##vso[task.setvariable variable=VersionInformation.FullVersionStringWithSuffixAndCommitHash]$($VersionInformation.FullVersionStringWithSuffixAndCommitHash))"
Write-Host "##vso[task.setvariable variable=VersionInformation.Suffix]$($VersionInformation.Suffix))"
Write-Host "##vso[task.setvariable variable=VersionInformation.VersionString]$($VersionInformation.VersionString))"
Write-Host "##vso[task.setvariable variable=VersionInformation.VersionStringWithSuffix]$($VersionInformation.VersionStringWithSuffix))"
Write-Host "##vso[task.setvariable variable=VersionInformation.VersionStringWithSuffixAndCommitHash]$($VersionInformation.VersionStringWithSuffixAndCommitHash))"