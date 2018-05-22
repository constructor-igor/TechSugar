# 
# https://stackoverflow.com/questions/49796024/python-tkinter-filedialog-askfolder-interfering-with-clr
# 
# works without 'import clr' and stuck with 'import clr'

import clr
from System.Threading import Thread, ThreadStart, ParameterizedThreadStart, ApartmentState
from System.Windows.Forms import OpenFileDialog, FolderBrowserDialog
from System.Collections import Queue

import tkinter as tk
from tkinter.filedialog import (askdirectory, askopenfilename)

import queue, threading

def open_dialog():
    print("open_dialog started")
    root = tk.Tk()
    root.withdraw()
    selected_path = askdirectory(title="Please select your installation folder location", initialdir=r"C:\Program Files\\")
    print("selected path: ", selected_path)
    print("open_dialog completed")
    return selected_path

#   WinForms thread function example
def win_dialog_thread(arg):
    print(f"dialog_thread started")
    folder_dialog = FolderBrowserDialog()
    file_dialog = OpenFileDialog()

    folder_dialog_result = folder_dialog.ShowDialog()
    file_dialog_result = file_dialog.ShowDialog()

    selected_folder = folder_dialog.SelectedPath
    selected_file = file_dialog.FileName
    win_dialog_result = selected_folder, selected_file
    arg.Enqueue(win_dialog_result)
    print(f"dialog_thread completed, {win_dialog_result}")

def open_win_dialog():
    #   start dialogs via CLR
    out_queue = Queue()    
    start = ParameterizedThreadStart(win_dialog_thread)
    thread = Thread(start)
    thread.SetApartmentState(ApartmentState.STA)
    thread.Start(out_queue)
    thread.Join()
    final_result = out_queue.Dequeue()
    return final_result[0], final_result[1]


if __name__ == "__main__":
    print("main started")

    # the method stuck when 'import clr' works before 
    # open_dialog()

    result = open_win_dialog()
    print(f"open_win_dialog: {result}")

    print("main finished")