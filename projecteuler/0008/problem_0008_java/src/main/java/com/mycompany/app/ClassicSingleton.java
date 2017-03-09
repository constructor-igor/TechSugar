package com.mycompany.app;

import java.io.IOException;
import java.util.logging.FileHandler;
import java.util.logging.Logger;
import java.util.logging.SimpleFormatter;

/**
 * Created by igor-z on 10-Mar-17.
 */
public class ClassicSingleton {
    private static ClassicSingleton instance = null;
    protected ClassicSingleton() throws IOException {

        logger = Logger.getLogger(Logger.GLOBAL_LOGGER_NAME);

        final int logFileLimit = 1024*1024;
        FileHandler fileTxt = new FileHandler("Logging.txt", logFileLimit, 1, true);
        SimpleFormatter formatterTxt = new SimpleFormatter();
        fileTxt.setFormatter(formatterTxt);
        logger.addHandler(fileTxt);
    }

    private Logger logger;
    public Logger getLogger() {
        return logger;
    }

    public static ClassicSingleton getInstance() throws IOException{
        if(instance == null) {
            instance = new ClassicSingleton();
        }
        return instance;
    }
}