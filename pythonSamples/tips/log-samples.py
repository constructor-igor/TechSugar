import logging
import logging.config
import yaml


class LoggerFactory():
    def __init__(self):
        return

    @staticmethod
    def create_logger(level=logging.INFO):
        # https://docs.python.org/2/library/logging.html#logrecord-attributes
        logging.basicConfig(level=level, format='%(asctime)s %(name)s %(levelname)-5s [%(module)s.%(funcName)s()] %(message)s')
        logger = logging.getLogger()
        logger.setLevel(level)
        return logger

    @staticmethod
    def create_logger_by_config_file(config_log_file_name):
        with open(config_log_file_name) as logging_config_file:
            logging_config = yaml.load(logging_config_file)
        logging.config.dictConfig(logging_config)
        logger = logging.getLogger()
        return logger


class Logic():
    def __init__(self, logger):
        self.logger = logger

    def run(self, x, y):
        self.logger.info("Logic.run() started")
        if (y==0):
            self.logger.warn("y==0")
        r = x*y
        self.logger.info("Logic.run() x*y = %s", r)
        if self.logger.isEnabledFor(logging.DEBUG):
            self.logger.debug("Logic.run() x = %s", x)
            self.logger.debug("Logic.run() y = %s", y)
        self.logger.info("Logic.run() completed")
        return r


if __name__ == "__main__":   
    main_logger = LoggerFactory.create_logger(level=logging.WARNING)
    main_logger.info("short log")
    logic = Logic(main_logger)
    logic.run(2, 0)
    
    main_logger = LoggerFactory.create_logger(level=logging.INFO)
    main_logger.info("info log")
    logic = Logic(main_logger)
    logic.run(2, 0)

    main_logger = LoggerFactory.create_logger_by_config_file(r".\debug-logging-config.yml")
    main_logger.info("detailed log, created by config file")
    logic = Logic(main_logger)
    logic.run(2, 0)

