# 
# conda install progressbar2
# 
# https://stackoverflow.com/questions/3002085/python-to-print-out-status-bar-and-percentage
# https://anaconda.org/conda-forge/progressbar2
# 

import numpy as np
import progressbar
from time import sleep

def show_progress_bar():
    with progressbar.ProgressBar(maxval=20, widgets=[progressbar.Bar('=', '[', ']'), ' ', progressbar.Percentage()]) as bar:
        for i in range(20):
            bar.update(i+1)
            sleep(0.1)

if __name__ == "__main__":
    print("[main] start")
    show_progress_bar()
    print("[main] finish")
