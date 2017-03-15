package com.mycompany.app;

import org.springframework.context.ApplicationContext;
import org.springframework.context.support.ClassPathXmlApplicationContext;

/**
 * Hello world!
 *
 */
public class App 
{
    public static void main( String[] args )
    {
        System.out.println( "Hello World!" );

        ApplicationContext context = new ClassPathXmlApplicationContext("beans.xml");

        SequenceGenerator generator1 = (SequenceGenerator) context.getBean("sequenceGenerator");
        System.out.println("sequenceGenerator");
        System.out.println(generator1.getSequence());
        System.out.println(generator1.getSequence());
        System.out.println(generator1.getSequence());

        System.out.println("sequenceGeneratorCtor");
        SequenceGenerator generator2 = (SequenceGenerator) context.getBean("sequenceGeneratorCtor");
        System.out.println(generator2.getSequence());
        System.out.println(generator2.getSequence());
        System.out.println(generator2.getSequence());

        System.out.println("sequenceGeneratorShort");
        SequenceGenerator generator3 = (SequenceGenerator) context.getBean("sequenceGeneratorShort");
        System.out.println(generator3.getSequence());
        System.out.println(generator3.getSequence());
        System.out.println(generator3.getSequence());

        System.out.println("sequenceGeneratorList");
        SequenceGenerator generator4 = (SequenceGenerator) context.getBean("sequenceGeneratorList");
        System.out.println(generator4.getSequence());
        System.out.println(generator4.getSequence());
        System.out.println(generator4.getSequence());

        System.out.println("sample of inheritance classes");
        Product aaa = (Product) context.getBean("aaa");
        Product cdrw = (Product) context.getBean("cdrw");
        System.out.println(aaa);
        System.out.println(cdrw);
    }
}
