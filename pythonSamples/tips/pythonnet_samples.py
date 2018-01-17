import clr
import os
import sys
import System
from System import Array
from System import Object
from System import Activator
from System import Type
from System import Double

def constructor_default(net_assembly, element_type):
    print("[FooClass()] created instance by type: ", Activator.CreateInstance(element_type))
    print("[FooClass()] created instance by string type: ", net_assembly.CreateInstance('pythonnet_csharp_sample_library.FooClass'))

def constructor_string(net_assembly, element_type):
    print("[FooClass(string)] created instance by type: ", Activator.CreateInstance(element_type, ["name1"]))
    print("[FooClass(string)] created instance by string type: ", net_assembly.CreateInstance('pythonnet_csharp_sample_library.FooClass', ["name2"]))

def constructor_string_double(net_assembly, element_type):
    print("[FooClass(string, double)] created instance by type: ", Activator.CreateInstance(element_type, ["name3", 1.1]))
    print("[FooClass(string, double)] created instance by string type: ", net_assembly.CreateInstance('pythonnet_csharp_sample_library.FooClass', ["name4", 1.2]))

def constructor_double(net_assembly, element_type):
    print("[FooClass(double)] created instance by type: ", Activator.CreateInstance(element_type, [10.0]))
    print("[FooClass(double)] created instance by string type: ", net_assembly.CreateInstance('pythonnet_csharp_sample_library.FooClass', [10.0]))

def constructor_array_double(net_assembly, element_type):
    data = Array.CreateInstance(Double, 2)
    data[0] = 9.1
    data[1] = 9.2
    # data = [7.1, 8.1]
    print("[FooClass(double[])] created instance by type: ", Activator.CreateInstance(element_type, [data]))
    print("[FooClass(double[])] created instance by string type: ", net_assembly.CreateInstance('pythonnet_csharp_sample_library.FooClass', [data]))

def constructor_2Darray_double(net_assembly, element_type):
    data = Array.CreateInstance(Double, 2, 3)
    data[0, 0] = 100.0
    data[0, 1] = 0.1
    data[0, 2] = 0.2
    data[1, 0] = 1.0
    data[1, 1] = 1.1
    data[1, 2] = 1.2
    # data = [7.1, 8.1]
    print("[FooClass(double[,])] created instance by type: ", Activator.CreateInstance(element_type, [data]))
    print("[FooClass(double[,])] created instance by string type: ", net_assembly.CreateInstance('pythonnet_csharp_sample_library.FooClass', [data]))


if __name__ == "__main__":
    print("[main] start")

    sys.path.append(r".")
    net_assembly = clr.AddReference('pythonnet_csharp_library')
    print("created net_assembly: ", net_assembly)

    element_type = net_assembly.GetType('pythonnet_csharp_sample_library.FooClass')
    print("element_type: ", element_type)

    constructor_default(net_assembly, element_type)
    constructor_string(net_assembly, element_type)
    constructor_string_double(net_assembly, element_type)
    constructor_double(net_assembly, element_type)
    constructor_array_double(net_assembly, element_type)
    constructor_2Darray_double(net_assembly, element_type)

    # print("[FooClass(double[])] created instance by type: ", Activator.CreateInstance(element_type, data))
    # print("[FooClass(double[])] created instance by type: ", net_assembly.CreateInstance(element_type, data))

    print("[main] finish")