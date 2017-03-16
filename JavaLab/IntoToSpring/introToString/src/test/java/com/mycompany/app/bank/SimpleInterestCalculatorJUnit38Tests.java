package com.mycompany.app.bank;

import junit.framework.TestCase;

/**
 * Created by igor-z on 16-Mar-17.
 */
public class SimpleInterestCalculatorJUnit38Tests extends TestCase{
    private InterestCalculator interestCalculator;
    protected void setUp() throws Exception {
        interestCalculator = new SimpleInterestCalculator();
        interestCalculator.setRate(0.05);
    }
    public void testCalculate() {
        double interest = interestCalculator.calculate(10000, 2);
        assertEquals(interest, 1000.0);
    }
    public void testIllegalCalculate() {
        try {
            interestCalculator.calculate(-10000, 2);
            fail("No exception on illegal argument");
        }
        catch (IllegalArgumentException e) {}
    }
}
