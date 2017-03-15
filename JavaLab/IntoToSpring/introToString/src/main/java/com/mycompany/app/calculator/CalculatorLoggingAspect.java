package com.mycompany.app.calculator;

import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;
import org.aspectj.lang.JoinPoint;
import org.aspectj.lang.annotation.Aspect;
import org.aspectj.lang.annotation.Before;
import org.aspectj.lang.annotation.After;
import org.aspectj.lang.annotation.AfterReturning;

import java.util.Arrays;

/**
 * Created by igor-z on 15-Mar-17.
 */

@Aspect
public class CalculatorLoggingAspect {
    private Log log = LogFactory.getLog(this.getClass());
    @Before("execution(* ArithmeticCalculator.add(..))")
    public void logBefore() {
        log.info("The method add() begins");
    }
    @Before("execution(* ArithmeticCalculator.*(..))")
    public void logBefore(JoinPoint joinPoint) {
        log.info(String.format("The method %s() begins with %s", joinPoint.getSignature().getName(), Arrays.toString(joinPoint.getArgs())));
    }

    @After("execution(* ArithmeticCalculator.*(..))")
    public void logAfter(JoinPoint joinPoint) {
        log.info(String.format("The method %s() ends", joinPoint.getSignature().getName()));
    }

    @AfterReturning(pointcut ="execution(* ArithmeticCalculator.*(..))", returning = "result")
    public void logAfterReturning(JoinPoint joinPoint, Object result) {
        log.info(String.format("The method %s(%s) ends with %s", joinPoint.getSignature().getName(), Arrays.toString(joinPoint.getArgs()), result));
    }
}
