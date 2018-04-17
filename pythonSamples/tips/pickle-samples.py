import pickle

class Animal:
    def __init__(self, name):
        self.name = name
    def print_status(self):
        print("[Animal] name: {0}".format(self.name))
    @staticmethod
    def print_status_static(name):
        print("[Animal(static)] name: {0}".format(name))

original_object = { "abc" : [1, 2, 3], "qwerty" : [4, 5, 6, 7] }
with open('d.pkl', 'wb') as file_handle_write:
    pickle.dump(original_object, file_handle_write)
print('original object: ', original_object)

#reload object from file
with open('d.pkl', 'rb') as file_handle_read:
    loaded_object = pickle.load(file_handle_read)

#print object loaded from file
print('loaded_object: ', loaded_object)

dog = Animal("dog")
dog.print_status()

animal_serialized = pickle.dumps(dog)
animal_restored = pickle.loads(animal_serialized)
animal_restored.print_status()

method = Animal.print_status_static
method("cat")

method_serialized = pickle.dumps(method)
method_restored = pickle.loads(method_serialized)
method_restored("camel")