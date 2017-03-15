package com.mycompany.app;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by igor-z on 13-Mar-17.
 */
public class SequenceGenerator {
    private String prefix;
    private List<Object> suffixes;
    private int initial;
    private int counter;
    public SequenceGenerator() { }
    public SequenceGenerator(String prefix, List<Object> suffixes, int initial) {
        this.prefix = prefix;
        this.suffixes = suffixes;
        this.initial = initial;
    }
    public SequenceGenerator(String prefix, String suffix, int initial) {
        this.prefix = prefix;
        this.suffixes = new ArrayList<Object>();
        this.suffixes.add(suffix);
        this.initial = initial;
    }
    public void setPrefix(String prefix) {
        this.prefix = prefix;
    }

    public void setSuffixes(List<Object> suffixes) {
        this.suffixes = suffixes;
    }

    public void setInitial(int initial) {
        this.initial = initial;
    }
    public synchronized String getSequence() {
        StringBuffer buffer = new StringBuffer();
        buffer.append(prefix);
        buffer.append(initial + "-" + counter++);
        for (Object suffix : suffixes) {
            buffer.append("-");
            buffer.append(suffix);
        }
        return buffer.toString();
    }
}