import java.io.Console;
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
    }
    public Program(String developer){
        this.developer = developer;
        Logger l = Logger.getLogger(getClass().getName());
        System.out.println("Program");
        l.log(Level.INFO, "Program started");
    }
}