# 
# https://stackoverflow.com/questions/49796024/python-tkinter-filedialog-askfolder-interfering-with-clr
# 

# import clr #pythonnet 2.3.0
import tkinter as tk
from tkinter.filedialog import (askdirectory, askopenfilename)

print("start")

root = tk.Tk()
root.withdraw()
selected_path = askdirectory(title="Please select your installation folder location", initialdir=r"C:\Program Files\\")
print("selected path: ", selected_path)

print("finish")