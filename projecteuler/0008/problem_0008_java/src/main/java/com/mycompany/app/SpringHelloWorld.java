package com.mycompany.app;

/**
 * Created by igor-z on 10-Mar-17.
 */
public class SpringHelloWorld {
    private String name;

    public void setName(String name) {
        this.name = name;
    }

    public void printHello() {
        System.out.println("Hello ! " + name);
    }
}