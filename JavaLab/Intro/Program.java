import java.io.Console;
import java.util.logging.Level;
import java.util.logging.LogRecord;
import java.util.logging.Logger;

public class Program{
    public static void main(String[] args) {
        System.out.println("Hello");
        Program program = new Program();
    }
    public Program(){
        Logger l = Logger.getLogger(getClass().getName());
        System.out.println("Program");
        l.log(Level.INFO, "Program started");
    }
}