# 
# https://github.com/andymccurdy/redis-py
# 
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

def set_get_delete(redis_connection):
    print('set_get_delete <<<<<<<<<<<<<<<<<<<<<<')
    simple_key = 'igor:simple'
    result = redis_connection.set(simple_key, 'intro')
    print('set: ', result)

    result = redis_connection.get(simple_key)
    print('get: ', result)

    redis_connection.delete(simple_key)
    print('get (after delete): ', redis_connection.get(simple_key))
    print('set_get_delete >>>>>>>>>>>>>>>>>>>>>>')
    return

def pub_sub_1(redis_connection):
    print('pub_sub1 <<<<<<<<<<<<<<<<<<<<<<')
    channel_key = 'igor:my-first-channel'
    p = redis_connection.pubsub()
    p.subscribe(channel_key)
    time.sleep(2)
    msg = p.get_message()
    print('get_message', msg)

    number_of_subscribers = redis_connection.publish(channel_key, 'some data')
    print('number_of_subscribers: ', number_of_subscribers)
    msg = p.get_message()
    print('get_message', msg)

    p.unsubscribe(channel_key)
    print('pub_sub1 >>>>>>>>>>>>>>>>>>>>>>')
    return

def my_handler(message):
    print('MY HANDLER: ', message['data'])
    return

def pub_sub_2(redis_connection):
    print('pub_sub2 <<<<<<<<<<<<<<<<<<<<<<')
    channel_key = 'igor:my-first-channel'
    p = redis_connection.pubsub()
    p.subscribe(**{channel_key: my_handler})
    time.sleep(2)
    msg = p.get_message()
    print('get_message', msg)

    number_of_subscribers = redis_connection.publish(channel_key, 'awesome data')
    print('number_of_subscribers: ', number_of_subscribers)
    msg = p.get_message()
    print('get_message', msg)

    p.unsubscribe(channel_key)
    print('pub_sub2 >>>>>>>>>>>>>>>>>>>>>>')
    return

if __name__ == "__main__":
    print("main started")  
    host = read_host_name()
    print('host:', host)

    redis_connection = connect_to_redis(host)

    set_get_delete(redis_connection)

    pub_sub_1(redis_connection)
    pub_sub_2(redis_connection)

    print("main completed")