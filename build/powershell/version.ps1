<#
.SYNOPSIS
Automatically sets versions for an entire source code repository, as appropriate.

.PARAMETER RootDirectory
The root directory of the repository.

.PARAMETER FirstDottedComponentOverride
A value that will override the first dotted version component.

.PARAMETER SecondDottedComponentOverride
A value that will override the second dotted version component.

.PARAMETER ThirdDottedComponentOverride
A value that will override the third dotted version component.

.PARAMETER FourthDottedComponentOverride
A value that will override the fourth dotted version component.

.PARAMETER SuffixOverride
A value that will override the suffix.

.PARAMETER CommitHash
A commit hash.
#>
[CmdletBinding(SupportsShouldProcess)]
[OutputType([hashtable])]
param(
    [string] $RootDirectory = (Join-PathEx $PSScriptRoot .. ..),
    [string] $FirstDottedComponentOverride,
    [string] $SecondDottedComponentOverride,
    [string] $ThirdDottedComponentOverride,
    [string] $FourthDottedComponentOverride,
    [string] $SuffixOverride,
    [string] $CommitHash
)

$RootDirectory = Resolve-Path $RootDirectory

return Set-Versions `
    -RootDirectory $RootDirectory `
    -FirstDottedComponentOverride $FirstDottedComponentOverride `
    -SecondDottedComponentOverride $SecondDottedComponentOverride `
    -ThirdDottedComponentOverride $ThirdDottedComponentOverride `
    -FourthDottedComponentOverride $FourthDottedComponentOverride `
    -SuffixOverride $SuffixOverride `
    -CommitHash $CommitHash `
    -Verbose:($PSBoundParameters["Verbose"] -eq $true) `
    -Confirm:($PSBoundParameters["Confirm"] -eq $true) `
    -WhatIf:($PSBoundParameters["WhatIf"] -eq $true)