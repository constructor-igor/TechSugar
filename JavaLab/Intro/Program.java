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

        List<Program> programs1 = new ArrayList<Program>();
        List<Program> programs2 = new ArrayList<>();
        programs2.add(new Program("Monika"));
        programs2.add(new Program("Ross"));

        l.info("Programs2 developers list:");
        for (Program p: programs2){
            l.info(p.getDeveloper());
        }

        System.out.println("programs1: " + Integer.toString(programs1.size()));
        System.out.println(String.format("programs2: %d", programs2.size()));

        Set<Integer> setOfIntegers = new HashSet<Integer>();
        setOfIntegers.add(Integer.valueOf(10));
        setOfIntegers.add(11);
        setOfIntegers.add(10);
        setOfIntegers.add(12);
        for (Integer i : setOfIntegers) {
            l.info("Integer value is: " + i);
        }
        l.info("hashset contains 12: " + setOfIntegers.contains(11));
        l.info("hashset contains 12: " + setOfIntegers.contains(12));
        l.info(String.format("hashset contains %d: %b", 10, setOfIntegers.contains(10)));

        Map<String, Integer>mapOfIntegers = createMapOfIntegers();
        Integer oneHundred68 = mapOfIntegers.get("168");
        System.out.println("mapOfIntegers[168]:" + oneHundred68);

        for (String key : mapOfIntegers.keySet()) {
            Integer  value = mapOfIntegers.get(key);
            l.info("Value keyed by '" + key + "' is '" + value + "'");
        }
    }
    public Program(String developer){
        this.developer = developer;
        Logger l = Logger.getLogger(getClass().getName());
        System.out.println("Program");
        l.log(Level.INFO, "Program started");
    }

    public static Map<String, Integer> createMapOfIntegers() {
        Map<String, Integer> mapOfIntegers = new HashMap<>();
        mapOfIntegers.put("1", Integer.valueOf(1));
        mapOfIntegers.put("2", Integer.valueOf(2));
        mapOfIntegers.put("3", Integer.valueOf(3));
        //...
        mapOfIntegers.put("168", Integer.valueOf(168));
        return mapOfIntegers;
    }
}