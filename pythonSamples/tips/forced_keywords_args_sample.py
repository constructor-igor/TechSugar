#
# https://www.getdrip.com/deliveries/etps5bpph5gkhpayxo67?__s=mysqo5rgfat5kha72eve
#

# In Python 3 you can use a bare "*" asterisk
# in function parameter lists to force the
# caller to use keyword arguments for certain
# parameters:

def f(a, b, *, c='x', d='y', e='z'):
    return 'Hello'

if __name__ == "__main__":
    # To pass the value for c, d, and e you 
    # will need to explicitly pass it as 
    # "key=value" named arguments:
    r = f(1, 2, c='p', d='q',e='v')
    print(r)

    r = f(1, 2, 'p', 'q', 'v')
    print(r)
