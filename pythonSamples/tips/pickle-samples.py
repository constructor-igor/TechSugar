import pickle


original_object = { "abc" : [1, 2, 3], "qwerty" : [4, 5, 6, 7] }
with open('d.pkl', 'wb') as file_handle_write:
    pickle.dump(original_object, file_handle_write)
print('original object: ', original_object)

#reload object from file
with open('d.pkl', 'rb') as file_handle_read:
    loaded_object = pickle.load(file_handle_read)

#print object loaded from file
print('loaded_object: ', loaded_object)
