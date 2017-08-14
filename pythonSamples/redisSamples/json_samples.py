import json


json_str_dat = json.dumps({'name': 'name_value', 'alias': 'alias_name', 'data': {'v1': 10, 'v2':20}})
print(json_str_dat)

imported_object = json.loads(json_str_dat)
print(imported_object)

print(imported_object['name'])
print(imported_object['alias'])
print(imported_object['data']['v1'])
if 'fake' in imported_object:
    print(imported_object['fake'])
