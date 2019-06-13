import logging
import elasticsearch 

if __name__ == "__main__":
    logging.basicConfig(format='%(asctime)s %(levelname)s: %(message)s', level=logging.INFO)
    logging.info("[main] started")

    host = 'http://localhost:9200/'
    es = elasticsearch.Elasticsearch(host)
    connected = es.ping()
    logging.info(f'connected = {connected}')

    if connected:
        response = es.search(index='accounts', body={"query": {"match": {'name':'John'}}})
        logging.info(f"response = {response}")

        index_name = 'commands'
        doc_type = 'command'
        command_1 = {
            'name': 'add',
            'parameter1': '7',
            'parameter2': '8',
        }
        res = es.index(index=index_name, doc_type=doc_type, id=10, body=command_1)
        logging.info(res['result'])
        res = es.index(index=index_name, doc_type=doc_type, id=11, body={'name': 'sub', 'parameter1': '5', 'parameter2': '6'})
        logging.info(res['result'])
        res = es.index(index=index_name, doc_type=doc_type, id=12, body={'name': 'sub', 'parameter1': '9', 'parameter2': '1'})
        logging.info(res['result'])

        response = es.search(index=index_name, body={"query": {"match": {'name':'sub'}}})
        logging.info(f"response = {response}")

        # es_logger = logging.getLogger('elasticsearch')
        # handler = logging.StreamHandler()
        # es_logger.addHandler(handler)
        # es_logger.info("created es-logger")
        # es_logger_trace = logging.getLogger('elasticsearch.trace')    
        # es_logger_trace.propogate = True
        # es_logger_trace.info("created es_logger_trace")    

    logging.info("[main] finished")
