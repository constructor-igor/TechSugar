import time
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
    print("[publisher to redis] main started")
    host = read_host_name()
    print('host:', host)
    redis_connection = connect_to_redis(host)

    channel_key = 'igor:my-3-channel'
    redis_connection.publish(channel_key, 'test data1')
    redis_connection.publish(channel_key, 'test data2')
    redis_connection.publish(channel_key, 'finish')

    print("[publisher to redis] main completed")
