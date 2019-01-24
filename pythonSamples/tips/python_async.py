# 
# https://realpython.com/async-io-python/
# https://stackoverflow.com/questions/52796630/python3-6-attributeerror-module-asyncio-has-no-attribute-run
# 
import time
import datetime
import asyncio

async def count(s):
    print(f"start({s}) {datetime.datetime.now().time()}")
    await asyncio.sleep(s)
    print(f"finish({s}) {datetime.datetime.now().time()}")

if __name__ == "__main__":    
    input = [0, 1, 4]
    
    s = time.perf_counter()
    future = [count(1), count(2), count(3), count(1)]    
    loop = asyncio.get_event_loop()
    loop.run_until_complete(asyncio.wait(future))
    loop.close()
    elapsed = time.perf_counter() - s
    print(f"{__file__} executed in {elapsed:0.2f} seconds.")