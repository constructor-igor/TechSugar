#
# http://www.programmableweb.com/news/how-to-consume-github-api-powershell/how-to/2016/04/23
#
# token
# name
# location

$Token = 'constructor-igor:token'
$Base64Token = [System.Convert]::ToBase64String([char[]]$Token);

$Headers = @{
    Authorization = 'Basic {0}' -f $Base64Token;
};

$Body = @{
    name = 'name';
    location = 'location';
} | ConvertTo-Json;
Invoke-RestMethod -Headers $Headers -Uri https://api.github.com/user -Body $Body -Method Patch
