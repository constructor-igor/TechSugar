$ip = Invoke-RestMethod -Uri http://checkip.amazonaws.com/
$geo = Invoke-RestMethod -Uri "http://geoip.nekudo.com/api/$IP"
$latitude = $geo.Location.Latitude
$longitude = $geo.Location.Longitude
 
$url = "https://www.google.com/maps/preview/@$latitude,$longitude,8z"
Start-Process -FilePath $url