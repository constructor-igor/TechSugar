import logging
import logging.config
import yaml


class LoggerFactory():
    def __init__(self):
        return

    @staticmethod
    def create_log(level=logging.INFO):
        # https://docs.python.org/2/library/logging.html#logrecord-attributes
        logging.basicConfig(level=level, format='%(asctime)s %(levelname)-5s [log-samples].[%(funcName)s()] %(message)s')
        logger = logging.getLogger()
        logger.setLevel(level)
        return logger


class Logic():
    def __init__(self, logger):
        self.logger = logger

    def run(self, x, y):
        self.logger.info("Logic.run() started")
        r = x*y
        self.logger.info("Logic.run() x*y = %s", r)
        if self.logger.isEnabledFor(logging.DEBUG):
            self.logger.debug("Logic.run() x = %s", x)
            self.logger.debug("Logic.run() y = %s", y)
        self.logger.info("Logic.run() completed")
        return r


if __name__ == "__main__":   
    main_logger = LoggerFactory.create_log(level=logging.INFO)
    main_logger.info("short log")
    logic = Logic(main_logger)
    logic.run(2, 3)
    
    main_logger = LoggerFactory.create_log(level=logging.DEBUG)
    main_logger.info("detailed log")
    logic = Logic(main_logger)
    logic.run(2, 3)

