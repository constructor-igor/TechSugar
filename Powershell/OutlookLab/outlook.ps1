#
#
#
$contactsFolder = [Microsoft.Office.Interop.Outlook.OlDefaultFolders]::olFolderContacts #10
$Outlook=NEW-OBJECT –comobject Outlook.Application
$Contacts=$Outlook.session.GetDefaultFolder($contactsFolder).items
$Contacts | Format-Table FullName,MobileTelephoneNumber,Email1Address
#
$Contacts = $Outlook.session.GetGlobalAddressList().AddressEntries
$Contacts | Format-Table Name
$Contacts | ForEach-Object {$_.GetExchangeUser().PrimarySmtpAddress, $_.GetExchangeUser().Alias, $_.GetExchangeUser().MobileTelephoneNumber}
#
#
#
<#
	References:
	- http://www.sqlservercentral.com/blogs/adventuresinsql/2011/01/20/looking-up-email-addresses-with-powershell/
	- https://gallery.technet.microsoft.com/office/Access-Outlook-Contacts-7c2feb65
#>	