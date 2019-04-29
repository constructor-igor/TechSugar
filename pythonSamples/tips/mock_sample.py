from unittest.mock import Mock

mock = Mock()
print(mock)

json = Mock()
print(json.dumps())

json = Mock()
print(json.dumps())
print(json.loads('{"key": "value"}').get('key'))

print("loads:")
json.loads.assert_called()
json.loads.assert_called_once()
json.loads.assert_called_once_with('{"key": "value"}')
print(f"loads count: {json.loads.call_count}")