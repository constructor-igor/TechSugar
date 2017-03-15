package com.mycompany.app.calculator;

/**
 * Created by igor-z on 15-Mar-17.
 */
public class UnitCalculatorImpl implements UnitCalculator {
    public double kilogramToPound(double kilogram) {
        double pound = kilogram * 2.2;
        System.out.println(kilogram + " kilogram = " + pound + " pound");
        return pound;
    }
    public double kilometerToMile(double kilometer) {
        double mile = kilometer * 0.62;
        System.out.println(kilometer + " kilometer = " + mile + " mile");
        return mile;
    }
}
