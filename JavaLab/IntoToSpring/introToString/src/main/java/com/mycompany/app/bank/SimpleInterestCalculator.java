package com.mycompany.app.bank;

/**
 * Created by igor-z on 16-Mar-17.
 */
public class SimpleInterestCalculator implements InterestCalculator  {
    private double rate;
    public void setRate(double rate) {
        this.rate = rate;
    }
    public double calculate(double amount, double year) {
        if (amount < 0 || year < 0) {
            throw new IllegalArgumentException("Amount or year must be positive");
        }
        return amount * year * rate;
    }
}
