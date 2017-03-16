package com.mycompany.app.bank;

/**
 * Created by igor-z on 16-Mar-17.
 */
public interface InterestCalculator {
    public void setRate(double rate);
    public double calculate(double amount, double year);
}