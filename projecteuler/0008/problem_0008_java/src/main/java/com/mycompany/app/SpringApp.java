package com.mycompany.app;

import org.springframework.context.ApplicationContext;
import org.springframework.context.support.ClassPathXmlApplicationContext;

/**
 * Created by igor-z on 10-Mar-17.
 *
 * The sample created from https://www.mkyong.com/spring/quick-start-maven-spring-example/
 */
public class SpringApp {
    public static void main( String[] args )  {

        ApplicationContext context = new ClassPathXmlApplicationContext(
            "Spring-Module.xml");

        SpringHelloWorld obj = (SpringHelloWorld) context.getBean("helloBean");
        obj.printHello();
    }
}
