import logging
import logging.config
import yaml

with open('./logging-config.yml') as logging_config_file:
    logging_config = yaml.load(logging_config_file)

logging.config.dictConfig(logging_config)

root_logger = logging.getLogger()
root_logger.info('This is the root logger.')

diddly_logger = logging.getLogger('diddly')
diddly_logger.info('This is the diddly logger.')