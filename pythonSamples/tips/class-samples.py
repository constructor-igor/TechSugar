from abc import ABC, abstractmethod

class Parent(ABC):
    def __init__(self):
        print("Parent.__init__")

    @abstractmethod
    def run(self): pass

class Child1(Parent):
    def __init__(self):
        print("Child1.__init__()")
        
    def run(self):
        print("Child1.run()")
        return "Child1"

class Child2(Parent):
    def __init__(self):
        print("Child2.__init__")

class Orphan():
    def __init__(self):
        print("Orphan.__init__()")
    def run(self):
        print("Orphan.run()")
        return "Orphan"

def run(parent_instance):
    if not isinstance(parent_instance, Parent.__class__):
        print("[Warning #1] parameter parent_instance doesn't created as 'Parent'")
    if not issubclass(type(parent_instance), Parent):
        print("[Warning #2] parameter parent_instance doesn't support interface 'Parent")
    print("run result: ", parent_instance.run())

if __name__ == "__main__":
    print("[main] start")
    
    try:
        parent = Parent()
    except Exception as error:
        print(str(error))

    child1 = Child1()
    print("type(child1): ", type(child1))
    run(child1)

    try:
        child2 = Child2()
        run(child2)
    except Exception as error:
        print(str(error))

    orphan = Orphan()
    run(orphan)

    print("[main] finish")