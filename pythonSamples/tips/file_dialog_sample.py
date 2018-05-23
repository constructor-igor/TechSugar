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

    title = arg.Dequeue()
    initial_folder = arg.Dequeue()
    print(f"title: {title}, initial_folder: {initial_folder}")
    folder_dialog.Description = title
    folder_dialog.SelectedPath = initial_folder
    folder_dialog_result = folder_dialog.ShowDialog()
    file_dialog_result = file_dialog.ShowDialog()

    selected_folder = folder_dialog.SelectedPath
    selected_file = file_dialog.FileName
    win_dialog_result = selected_folder, selected_file
    arg.Enqueue(win_dialog_result)
    print(f"win_dialog_thread completed, {win_dialog_result}")

def open_win_dialog(title, initial_folder):
    #   start dialogs via CLR
    out_queue = Queue()
    out_queue.Enqueue(title)
    out_queue.Enqueue(initial_folder)
    start = ParameterizedThreadStart(win_dialog_thread)
    thread = Thread(start)
    thread.SetApartmentState(ApartmentState.STA)
    thread.Start(out_queue)
    thread.Join()
    final_result = out_queue.Dequeue()
    return final_result[0], final_result[1]


def tkinter_dialog_thread(arg):
    print(f"dialog_thread started")
    title = arg.Dequeue()
    initial_folder = arg.Dequeue()

    root = tk.Tk()
    root.withdraw()
    selected_folder = askdirectory(title=title, initialdir=initial_folder)
    selected_file = ""

    # folder_dialog = FolderBrowserDialog()
    # file_dialog = OpenFileDialog()

    # print(f"title: {title}, initial_folder: {initial_folder}")
    # folder_dialog.Description = title
    # folder_dialog.SelectedPath = initial_folder
    # folder_dialog_result = folder_dialog.ShowDialog()
    # file_dialog_result = file_dialog.ShowDialog()

    # selected_folder = folder_dialog.SelectedPath
    # selected_file = file_dialog.FileName
    win_dialog_result = selected_folder, selected_file
    arg.Enqueue(win_dialog_result)
    print(f"tkinter_dialog_thread completed, {win_dialog_result}")


def open_tkinter_dialog(title, initial_folder):
    out_queue = Queue()
    out_queue.Enqueue(title)
    out_queue.Enqueue(initial_folder)
    start = ParameterizedThreadStart(tkinter_dialog_thread)
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

    result = open_win_dialog("select folder", "d:\\")
    print(f"open_win_dialog: {result}")

    result = open_tkinter_dialog("select folder", "d:\\")
    print(f"open_tkinter_dialog: {result}")

    print("main finished")