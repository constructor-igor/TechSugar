import pandas as pd
import matplotlib.pyplot as plt
import matplotlib.dates as mdates
from matplotlib.ticker import FormatStrFormatter, LinearLocator, FuncFormatter

cityname = 'jerusalem'
df = pd.read_csv(f"{cityname}.csv", sep=',', encoding='utf-8')
# d = {999.9:0.0}
# df = df.replace(d)
# df = df[(df['Year'] >= 1900)]

plt.rcParams["figure.figsize"] = (8, 5)
fig, ax = plt.subplots()

def neg_tick(x, pos):
    return '%.1f' % (x)
    return '%.1f' % (-x if x else 0)

for month, color in [
        ('Dec', 'b-'), ('Jan', 'b+'), ('Feb', 'b*'), 
        ('Mar', 'y-'), ('Apr', 'y+'), ('May', 'y*'), 
        ('Jun', 'r-'), ('Jul', 'r+'), ('Aug', 'r*'), 
        ('Sep', 'g-'), ('Oct', 'g+'), ('Nov', 'g*')]:
    df[month] = df[month].where(df[month]<100)
    plt.plot(df['Year'].values, df[month].rolling(window=20, min_periods=1).mean(), color)

# df['Jan'] = df['Jan'].where(df['Jan']<100)
# df['Aug'] = df['Aug'].where(df['Aug']<100)

# # plt.bar(df['Year'].values, df['Jan'].values, label=f'{cityname} - January Temperature, C')
# plt.plot(df['Year'].values, df['Jan'].rolling(window=20, min_periods=1).mean(), 'r-')
# plt.plot(df['Year'].values, df['Aug'].rolling(window=20, min_periods=1).mean(), 'g-')
ax.yaxis.set_major_formatter(FuncFormatter(neg_tick))

plt.legend(loc='best')
plt.tight_layout()
plt.title(f"Temperature in {cityname}")
plt.show()