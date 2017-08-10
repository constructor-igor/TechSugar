# 
# https://github.com/andymccurdy/redis-py
# 
import configparser
import redis

def read_host_name():
    config = configparser.ConfigParser()
    config['General'] = {'host': 'localhost'}
    config.read('user.ini')
    host = config['General']['host']
    return host

def connect_to_redis(host):
    redis_connection = redis.StrictRedis(host=host, port=6379, db=0)
    return redis_connection

if __name__ == "__main__":
    print("main started")  
    host = read_host_name()
    print('host:', host)

    redis_connection = connect_to_redis(host)

    simple_key = 'igor:simple'

    result = redis_connection.set(simple_key, 'intro')
    print('set: ', result)

    result = redis_connection.get(simple_key)
    print('get: ', result)

    redis_connection.delete(simple_key)
    print('get (after delete): ', redis_connection.get(simple_key))

    print("main completed")