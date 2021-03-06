package com.mycompany.app;

import org.springframework.beans.factory.annotation.Required;

/**
 * Created by igor-z on 14-Mar-17.
 */
public abstract class Product {
    private String name;
    private double price;

    public Product() {}
    public Product(String name, double price) {
        this.name = name;
        this.price = price;
    }

    public void setName(String name) {
        this.name = name;
    }
    @Required
    public void setPrice(double price) {
        this.price = price;
    }


    public String toString() {
        return name + " " + price;
    }
}

