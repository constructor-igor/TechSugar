#   importing ctypes stuff
import ctypes
get_apartment = ctypes.windll.ole32.CoGetApartmentType

#   comment/uncomment this import to see the difference
# import clr

apt_type = ctypes.c_uint(0)
apt_qualifier = ctypes.c_uint(0)

if get_apartment(ctypes.byref(apt_type), ctypes.byref(apt_qualifier)) == 0:
    #   APPTYPE enum: https://msdn.microsoft.com/en-us/library/windows/desktop/ms693793(v=vs.85).aspx
    #   APTTYPEQUALIFIER enum: https://msdn.microsoft.com/en-us/library/windows/desktop/dd542638(v=vs.85).aspx
    print('APTTYPE = %d\tAPTTYPEQUALIFIER = %d' % (apt_type.value, apt_qualifier.value))
else:
    print('COM model not initialized!')