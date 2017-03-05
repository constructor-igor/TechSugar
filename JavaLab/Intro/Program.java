import java.io.Console;
import java.util.*;
import java.util.logging.Level;
import java.util.logging.LogRecord;
import java.util.logging.Logger;

public class Program{
    private String developer;
    public String getDeveloper(){
        return developer;
    }
    public void setDeveloper(String developer){
        this.developer = developer;
    }
    public static void main(String[] args) {
        System.out.println("Hello");
        Program program = new Program("Joe");
        System.out.println("Developer: " + program.getDeveloper());
        program.setDeveloper("Chandler");
        System.out.println("Developer: " + program.getDeveloper());

        Logger l = Logger.getLogger(Program.class.getName());
        l.info("Developer Name: " + program.getDeveloper());



    }
    public Program(String developer){
        this.developer = developer;
        Logger l = Logger.getLogger(getClass().getName());
        System.out.println("Program");
        l.log(Level.INFO, "Program started");

        List<Program> programs1 = new ArrayList<Program>();
        List<Program> programs2 = new ArrayList<>();

        System.out.println("programs1: " + Integer.toString(programs1.size()));
        System.out.println(String.format("programs2: %d", programs2.size()));
    }
}