from datetime import datetime, timezone 
now = datetime.now(timezone.utc)

print(now)

print(datetime.utcnow().strftime('%Y-%m-%d_%H-%M-%S.%f'))

utc_dt = datetime.now(timezone.utc) # UTC time
dt = utc_dt.astimezone() # local time
print(dt.strftime('%Y-%m-%d_%H-%M-%S.%f'))
