package com.mycompany.app;

import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.Date;

/**
 * Created by igor-z on 15-Mar-17.
 */
public class DatePrefixGenerator implements PrefixGenerator {
    private DateFormat formatter;
    public void setPattern(String pattern) {
        this.formatter = new SimpleDateFormat(pattern);
    }
    public String getPrefix() {
        return formatter.format(new Date());
    }
}
