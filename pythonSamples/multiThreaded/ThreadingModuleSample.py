''' Threading sample'''
#
#   https://www.tutorialspoint.com/python3/python_multithreading.htm
#
import threading
import time

exitFlag = 0

class WorkingThread(threading.Thread):
    '''working thread'''
    def __init__(self, threadid, name, counter):
        threading.Thread.__init__(self)
        self.threadid = threadid
        self.name = name
        self.counter = counter
    def run(self):
        print("Starting " + self.name)
        print_time(self.name, self.counter, 5)
        print("Exiting " + self.name)

def print_time(threadname, delay, counter):
    '''print time'''
    while counter:
        if exitFlag:
            threadname.exit()
        time.sleep(delay)
        print("%s: %s" % (threadname, time.ctime(time.time())))
        counter -= 1

# Create new threads
thread1 = WorkingThread(1, "Thread-1", 1)
thread2 = WorkingThread(2, "Thread-2", 2)

# Start new Threads
thread1.start()
thread2.start()
thread1.join()
thread2.join()
print("Exiting Main Thread")
